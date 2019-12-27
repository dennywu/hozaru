using Hozaru.Core.Application.Features;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Core.Domain.Services;
using Hozaru.Core.Domain.Uow;
using Hozaru.Core.Identity.Application.Editions;
using Hozaru.Core.Identity.Authorization.Roles;
using Hozaru.Core.Identity.Authorization.Users;
using Hozaru.Core.Identity.IdentityFramework;
using Hozaru.Core.Runtime.Caching;
using Hozaru.Core.Identity.Runtime.Caching;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hozaru.Core.Identity.MultiTenancy
{
    public abstract class HozaruTenantManager<TTenant, TRole, TUser> : IDomainService
        where TTenant : HozaruTenant<TTenant, TUser>
        where TRole : HozaruRole<TTenant, TUser>
        where TUser : HozaruUser<TTenant, TUser>
    {
        public HozaruEditionManager EditionManager { get; set; }

        //public ILocalizationManager LocalizationManager { get; set; }

        public ICacheManager CacheManager { get; set; }

        protected IRepository<TTenant, int> TenantRepository { get; set; }

        protected IRepository<TenantFeatureSetting, long> TenantFeatureRepository { get; set; }

        public IFeatureManager FeatureManager { get; set; }

        protected HozaruTenantManager(
            IRepository<TTenant, int> tenantRepository,
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
            HozaruEditionManager editionManager)
        {
            TenantRepository = tenantRepository;
            TenantFeatureRepository = tenantFeatureRepository;
            EditionManager = editionManager;
            //LocalizationManager = NullLocalizationManager.Instance;
        }

        public virtual IQueryable<TTenant> Tenants { get { return TenantRepository.GetAll(); } }

        public virtual async Task<IdentityResult> CreateAsync(TTenant tenant)
        {
            if (await TenantRepository.FirstOrDefaultAsync(t => t.TenancyName == tenant.TenancyName) != null)
            {
                return HozaruIdentityResult.Failed(string.Format(MessagesCoreIdentity.TenancyNameIsAlreadyTaken, tenant.TenancyName));
            }

            var validationResult = await ValidateTenantAsync(tenant);
            if (!validationResult.Succeeded)
            {
                return validationResult;
            }

            await TenantRepository.InsertAsync(tenant);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(TTenant tenant)
        {
            if (await TenantRepository.FirstOrDefaultAsync(t => t.TenancyName == tenant.TenancyName && t.Id != tenant.Id) != null)
            {
                return HozaruIdentityResult.Failed(string.Format(MessagesCoreIdentity.TenancyNameIsAlreadyTaken, tenant.TenancyName));
            }

            await TenantRepository.UpdateAsync(tenant);
            return IdentityResult.Success;
        }

        public virtual async Task<TTenant> FindByIdAsync(int id)
        {
            return await TenantRepository.FirstOrDefaultAsync(id);
        }

        public virtual async Task<TTenant> GetByIdAsync(int id)
        {
            var tenant = await FindByIdAsync(id);
            if (tenant == null)
            {
                throw new HozaruException("There is no tenant with id: " + id);
            }

            return tenant;
        }

        public virtual Task<TTenant> FindByTenancyNameAsync(string tenancyName)
        {
            return TenantRepository.FirstOrDefaultAsync(t => t.TenancyName.ToLower() == tenancyName.ToLower());
        }

        public virtual async Task<IdentityResult> DeleteAsync(TTenant tenant)
        {
            await TenantRepository.DeleteAsync(tenant);
            return IdentityResult.Success;
        }

        public async Task<string> GetFeatureValueOrNullAsync(int tenantId, string featureName)
        {
            var cacheItem = await GetTenantFeatureCacheItemAsync(tenantId);
            var value = cacheItem.FeatureValues.GetOrDefault(featureName);
            if (value != null)
            {
                return value;
            }

            if (cacheItem.EditionId.HasValue)
            {
                value = await EditionManager.GetFeatureValueOrNullAsync(cacheItem.EditionId.Value, featureName);
                if (value != null)
                {
                    return value;
                }
            }

            return null;
        }

        public virtual async Task<IReadOnlyList<NameValue>> GetFeatureValuesAsync(int tenantId)
        {
            var values = new List<NameValue>();

            foreach (var feature in FeatureManager.GetAll())
            {
                values.Add(new NameValue(feature.Name, await GetFeatureValueOrNullAsync(tenantId, feature.Name) ?? feature.DefaultValue));
            }

            return values;
        }

        public virtual async Task SetFeatureValuesAsync(int tenantId, params NameValue[] values)
        {
            if (values.IsNullOrEmpty())
            {
                return;
            }

            foreach (var value in values)
            {
                await SetFeatureValueAsync(tenantId, value.Name, value.Value);
            }
        }

        [UnitOfWork]
        public virtual async Task SetFeatureValueAsync(int tenantId, string featureName, string value)
        {
            await SetFeatureValueAsync(await GetByIdAsync(tenantId), featureName, value);
        }

        [UnitOfWork]
        public virtual async Task SetFeatureValueAsync(TTenant tenant, string featureName, string value)
        {
            //No need to change if it's already equals to the current value
            if (await GetFeatureValueOrNullAsync(tenant.Id, featureName) == value)
            {
                return;
            }

            //Get the current feature setting
            var currentSetting = await TenantFeatureRepository.FirstOrDefaultAsync(f => f.TenantId == tenant.Id && f.Name == featureName);

            //Get the feature
            var feature = FeatureManager.GetOrNull(featureName);
            if (feature == null)
            {
                if (currentSetting != null)
                {
                    await TenantFeatureRepository.DeleteAsync(currentSetting);
                }

                return;
            }

            //Determine default value
            var defaultValue = tenant.EditionId.HasValue
                ? (await EditionManager.GetFeatureValueOrNullAsync(tenant.EditionId.Value, featureName) ?? feature.DefaultValue)
                : feature.DefaultValue;

            //No need to store value if it's default
            if (value == defaultValue)
            {
                if (currentSetting != null)
                {
                    await TenantFeatureRepository.DeleteAsync(currentSetting);
                }

                return;
            }

            //Insert/update the feature value
            if (currentSetting == null)
            {
                await TenantFeatureRepository.InsertAsync(new TenantFeatureSetting(tenant.Id, featureName, value));
            }
            else
            {
                currentSetting.Value = value;
            }
        }

        /// <summary>
        /// Resets all custom feature settings for a tenant.
        /// Tenant will have features according to it's edition.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        public async Task ResetAllFeaturesAsync(int tenantId)
        {
            await TenantFeatureRepository.DeleteAsync(f => f.TenantId == tenantId);
        }

        private async Task<TenantFeatureCacheItem> GetTenantFeatureCacheItemAsync(int tenantId)
        {
            return await CacheManager.GetTenantFeatureCache().GetAsync(tenantId, async () =>
            {
                var tenant = await GetByIdAsync(tenantId);

                var newCacheItem = new TenantFeatureCacheItem { EditionId = tenant.EditionId };

                var featureSettings = await TenantFeatureRepository.GetAllListAsync(f => f.TenantId == tenantId);
                foreach (var featureSetting in featureSettings)
                {
                    newCacheItem.FeatureValues[featureSetting.Name] = featureSetting.Value;
                }

                return newCacheItem;
            });
        }

        protected virtual async Task<IdentityResult> ValidateTenantAsync(TTenant tenant)
        {
            var nameValidationResult = await ValidateTenancyNameAsync(tenant.TenancyName);
            if (!nameValidationResult.Succeeded)
            {
                return nameValidationResult;
            }

            return IdentityResult.Success;
        }

        protected virtual async Task<IdentityResult> ValidateTenancyNameAsync(string tenancyName)
        {
            if (!Regex.IsMatch(tenancyName, HozaruTenant<TTenant, TUser>.TenancyNameRegex))
            {
                return HozaruIdentityResult.Failed(MessagesCoreIdentity.InvalidTenancyName);
            }

            return IdentityResult.Success;
        }
    }
}
