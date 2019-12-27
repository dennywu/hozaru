using Hozaru.Core.Application.Editions;
using Hozaru.Core.Application.Features;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Core.Domain.Services;
using Hozaru.Core.Domain.Uow;
using Hozaru.Core.Identity.Application.Features;
using Hozaru.Core.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Core.Identity.Runtime.Caching;

namespace Hozaru.Core.Identity.Application.Editions
{
    public abstract class HozaruEditionManager :
        IDomainService
    {
        public IQueryable<Edition> Editions { get { return EditionRepository.GetAll(); } }

        public ICacheManager CacheManager { get; set; }

        public IFeatureManager FeatureManager { get; set; }

        protected IRepository<Edition, int> EditionRepository { get; set; }

        protected IRepository<EditionFeatureSetting, long> EditionFeatureRepository { get; set; }

        protected HozaruEditionManager(
            IRepository<Edition, int> editionRepository,
            IRepository<EditionFeatureSetting, long> editionFeatureRepository
            )
        {
            EditionRepository = editionRepository;
            EditionFeatureRepository = editionFeatureRepository;
        }

        public virtual async Task<string> GetFeatureValueOrNullAsync(int editionId, string featureName)
        {
            var cacheItem = await GetEditionFeatureCacheItemAsync(editionId);
            return cacheItem.FeatureValues.GetOrDefault(featureName);
        }

        [UnitOfWork]
        public virtual async Task SetFeatureValueAsync(int editionId, string featureName, string value)
        {
            if (await GetFeatureValueOrNullAsync(editionId, featureName) == value)
            {
                return;
            }

            var currentSetting = await EditionFeatureRepository.FirstOrDefaultAsync(f => f.EditionId == editionId && f.Name == featureName);

            var feature = FeatureManager.GetOrNull(featureName);
            if (feature == null || feature.DefaultValue == value)
            {
                if (currentSetting != null)
                {
                    await EditionFeatureRepository.DeleteAsync(currentSetting);
                }

                return;
            }

            if (currentSetting == null)
            {
                await EditionFeatureRepository.InsertAsync(new EditionFeatureSetting(editionId, featureName, value));
            }
            else
            {
                currentSetting.Value = value;
            }
        }

        public virtual async Task<IReadOnlyList<NameValue>> GetFeatureValuesAsync(int editionId)
        {
            var values = new List<NameValue>();

            foreach (var feature in FeatureManager.GetAll())
            {
                values.Add(new NameValue(feature.Name, await GetFeatureValueOrNullAsync(editionId, feature.Name) ?? feature.DefaultValue));
            }

            return values;
        }

        public virtual async Task SetFeatureValuesAsync(int editionId, params NameValue[] values)
        {
            if (values.IsNullOrEmpty())
            {
                return;
            }

            foreach (var value in values)
            {
                await SetFeatureValueAsync(editionId, value.Name, value.Value);
            }
        }

        public virtual Task CreateAsync(Edition edition)
        {
            return EditionRepository.InsertAsync(edition);
        }

        public virtual Task<Edition> FindByNameAsync(string name)
        {
            return EditionRepository.FirstOrDefaultAsync(edition => edition.Name == name);
        }

        public virtual Task<Edition> FindByIdAsync(int id)
        {
            return EditionRepository.FirstOrDefaultAsync(id);
        }

        public virtual Task<Edition> GetByIdAsync(int id)
        {
            return EditionRepository.GetAsync(id);
        }

        public virtual Task DeleteAsync(Edition edition)
        {
            return EditionRepository.DeleteAsync(edition);
        }

        protected virtual async Task<EditionfeatureCacheItem> GetEditionFeatureCacheItemAsync(int editionId)
        {
            return await CacheManager
                .GetEditionFeatureCache()
                .GetAsync(
                    editionId,
                    async () => await CreateEditionFeatureCacheItem(editionId)
                );
        }

        protected virtual async Task<EditionfeatureCacheItem> CreateEditionFeatureCacheItem(int editionId)
        {
            var newCacheItem = new EditionfeatureCacheItem();

            var featureSettings = await EditionFeatureRepository.GetAllListAsync(f => f.EditionId == editionId);
            foreach (var featureSetting in featureSettings)
            {
                newCacheItem.FeatureValues[featureSetting.Name] = featureSetting.Value;
            }

            return newCacheItem;
        }
    }
}
