using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hozaru.ApplicationServices.Tenants.Dtos;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;
using Hozaru.Identity.Editions;
using Hozaru.Identity.MultiTenancy;
using Hozaru.Core.Identity.IdentityFramework;
using Hozaru.Core.Domain.Uow;
using Hozaru.ApplicationServices.Roles;
using Hozaru.ApplicationServices.Roles.Dtos;
using Hozaru.Identity.Roles;
using Hozaru.Identity.Users;
using System.Linq;
using Hozaru.Core;
using Hozaru.Authentication.ApiKeyProvider;
using Hozaru.Core.Threading;
using Hozaru.Whatsapp;
using System.IO;
using Hozaru.Core.Configurations;
using AutoMapper;
using SixLabors.ImageSharp;
using Hozaru.ApplicationServices.ImagesGenerator;
using SixLabors.ImageSharp.Formats.Png;

namespace Hozaru.ApplicationServices.Tenants
{
    public class TenantAppService : HozaruApplicationService, ITenantAppService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly EditionManager _editionManager;
        private readonly TenantManager _tenantManager;
        private readonly IRoleAppService _roleAppService;
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IApiKeyService _apiKeyService;
        private readonly IRepository<Districts> _districtRepository;
        private IImageGenerator _imageGenerator;

        public TenantAppService(IRepository<Order> orderRepository, IRepository<Product> productRepository, EditionManager editionManager, TenantManager tenantManager, IRoleAppService roleAppService, RoleManager roleManager, UserManager userManager, IApiKeyService apiKeyService, IRepository<Districts> districtRepository, IImageGenerator imageGenerator)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _editionManager = editionManager;
            _tenantManager = tenantManager;
            _roleAppService = roleAppService;
            _roleManager = roleManager;
            _userManager = userManager;
            _apiKeyService = apiKeyService;
            _districtRepository = districtRepository;
            _imageGenerator = imageGenerator;
        }

        public async Task CreateTenant(CreateTenantInputDto inputDto)
        {
            if ((await _tenantManager.FindByTenancyNameAsync(inputDto.TenancyName)).IsNotNull())
                throw new HozaruException(string.Format("Toko Id {0} tidak bisa digunakan. Silahkan gunakan Toko Id lain.", inputDto.TenancyName));

            var district = _districtRepository.Get(inputDto.DistrictId);
            Validate.Found(district, "Kecamatan");

            var tenant = new Tenant(inputDto.TenancyName, inputDto.Name, inputDto.WhatsappNumber, inputDto.Address, inputDto.Phone, district);
            var defaultEdition = await _editionManager.FindByNameAsync(EditionManager.DefaultEditionName);
            if (defaultEdition != null)
            {
                tenant.EditionId = defaultEdition.Id;
            }

            var createTenantResult = await _tenantManager.CreateAsync(tenant);
            createTenantResult.CheckErrors();
            await CurrentUnitOfWork.SaveChangesAsync(); //To get new tenant's id.

            CurrentUnitOfWork.DisableFilter(HozaruDataFilters.MayHaveTenant);
            CurrentUnitOfWork.DisableFilter(HozaruDataFilters.MustHaveTenant);
            CurrentUnitOfWork.EnableFilter(HozaruDataFilters.MayHaveTenant);
            using (CurrentUnitOfWork.SetFilterParameter(HozaruDataFilters.MayHaveTenant, HozaruDataFilters.Parameters.TenantId, tenant.Id))
            {
                IList<Role> createdRoles = await _roleAppService.CreateDefaultRoles(tenant.Id);
                foreach (var role in createdRoles)
                {
                    await _roleManager.GrantAllPermissionsAsync(role);
                }

                await CurrentUnitOfWork.SaveChangesAsync();

                //Create admin user for the tenant
                var adminUser = User.CreateTenantAdminUser(tenant, inputDto.AdminEmailAddress, inputDto.Password, inputDto.FirstName, inputDto.LastName);
                var createUserResult = await _userManager.CreateAsync(adminUser);
                createUserResult.CheckErrors();
                await CurrentUnitOfWork.SaveChangesAsync(); //To get admin user's id

                var adminRole = _roleManager.Roles.Single(r => r.Name == "Admin");
                //Assign admin user to role!
                var addRoleResult = await _userManager.AddToRoleAsync(adminUser.Id, adminRole.Name);
                addRoleResult.CheckErrors();

                //await _tenantConfigurationAppService.CreateNew(tenant);

                await CurrentUnitOfWork.SaveChangesAsync();
            }

            _apiKeyService.CreateApiKey(tenant.Id);
        }

        public void Edit(EditTenantInputDto inputDto)
        {
            var district = _districtRepository.Get(inputDto.DistrictId);
            Validate.Found(district, "Kecamatan");

            var tenant = GetCurrentTenant();
            tenant.Update(inputDto.Name, inputDto.WhatsappNumber, inputDto.Address, inputDto.Phone, district);
            AsyncHelper.RunSync(() => _tenantManager.UpdateAsync(tenant));

            if (inputDto.Image.IsNotNull())
            {
                var fileName = string.Format("{0}", tenant.TenancyName);
                var imageStream = inputDto.Image.OpenReadStream();
                var imageObj = Image.Load(imageStream);
                _imageGenerator.SaveBrandImage(imageObj, PngFormat.Instance, tenant);
            }

            CurrentUnitOfWork.SaveChanges();
        }

        public async Task<bool> Exist(string tenancyName)
        {
            if (await _tenantManager.FindByTenancyNameAsync(tenancyName) == null)
                return false;
            return true;
        }

        public TenantDto Get()
        {
            var tenant = GetCurrentTenant();
            return Mapper.Map<TenantDto>(tenant);
        }

        public async Task<Stream> GetBrandImage(string tenancyName)
        {
            var tenant = await _tenantManager.FindByTenancyNameAsync(tenancyName);
            Validate.Found(tenant, "Tenant");
            var pathFileDirectory = AppSettingConfigurationHelper.GetSection("PathFileStorageDirectory").Value;
            var imageFileUrl = Path.Combine("Images", "Tenants", tenant.TenancyName, "brand.png");
            var filePath = Path.Combine(pathFileDirectory, imageFileUrl);
            if (File.Exists(filePath))
                return File.OpenRead(filePath);
            else
            {
                var defaultFaviconImageUrl = Path.Combine("Images", "Tenants", "default-brand.png");
                return File.OpenRead(Path.Combine(pathFileDirectory, defaultFaviconImageUrl));
            }
        }

        public TenantInformationDto GetByExternalDomain(string externalDomain)
        {
            var tenant = _tenantManager.Tenants.FirstOrDefault(i => i.ExternalDomain == externalDomain);
            Validate.Found(tenant, "Tenant");
            return new TenantInformationDto()
            {
                TenancyName = tenant.TenancyName,
                Name = tenant.Name
            };
        }

        public async Task<Stream> GetFaviconImage(string tenancyName)
        {
            var tenant = await _tenantManager.FindByTenancyNameAsync(tenancyName);
            Validate.Found(tenant, "Tenant");
            var pathFileDirectory = AppSettingConfigurationHelper.GetSection("PathFileStorageDirectory").Value;
            var imageFileUrl = Path.Combine("Images", "Tenants", tenant.TenancyName, "favicon.png");
            var filePath = Path.Combine(pathFileDirectory, imageFileUrl);
            if (File.Exists(filePath))
                return File.OpenRead(filePath);
            else
            {
                var defaultFaviconImageUrl = Path.Combine("Images", "Tenants", "default-favicon.png");
                return File.OpenRead(Path.Combine(pathFileDirectory, defaultFaviconImageUrl));
            }
        }

        public TenantInformationDto GetInformation()
        {
            var totalOrder = _orderRepository.Count(i => i.Status == OrderStatus.DONE);
            var totalProduct = _productRepository.Count(i => i.Status == ProductStatus.ACTIVE);
            var tenant = AsyncHelper.RunSync(() => _tenantManager.GetByIdAsync(HozaruSession.TenantId.Value));

            var info = new TenantInformationDto()
            {
                Name = tenant.Name,
                TotalOrder = 123 + totalOrder,
                TotalProduct = totalProduct,
                Whatsapp = tenant.WhatsappNumber,
                WhatsappUrl = WhatsappNumberGeneratorHelper.GenerateWhatsappUrl(tenant.WhatsappNumber),
                TenancyName = tenant.TenancyName
            };

            return info;
        }
    }
}
