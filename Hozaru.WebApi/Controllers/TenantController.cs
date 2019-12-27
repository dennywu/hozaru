using Hozaru.ApplicationServices.Tenants;
using Hozaru.ApplicationServices.Tenants.Dtos;
using Hozaru.Authentication;
using Hozaru.Authentication.ApiKeyProvider;
using Hozaru.Authentication.ApiKeyProvider.Dtos;
using Hozaru.Core.Dependency;
using Hozaru.Core.Runtime.Security;
using Hozaru.Core.Runtime.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Hozaru.WebApi.Controllers
{
    [Route("api/tenants")]
    [ApiController]
    [Authorize]
    public class TenantController : HozaruApiController
    {
        private ITenantAppService _tenantAppService;
        private IApiKeyService _apiKeyService;

        public TenantController()
        {
            _tenantAppService = IocManager.Instance.Resolve<ITenantAppService>();
            _apiKeyService = IocManager.Instance.Resolve<IApiKeyService>();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("new")]
        public async Task<IActionResult> New(CreateTenantInputDto inputDto)
        {
            await _tenantAppService.CreateTenant(inputDto);
            return Ok();
        }

        [HttpPut]
        public TenantDto Edit([FromForm] EditTenantInputDto inputDto)
        {
            _tenantAppService.Edit(inputDto);
            return _tenantAppService.Get();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.AllScheme)]
        [Route("info")]
        public TenantInformationDto GetInformation()
        {
            var tenantInformation = _tenantAppService.GetInformation();
            return tenantInformation;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.AllScheme)]
        public TenantDto Get()
        {
            var tenant = _tenantAppService.Get();
            return tenant;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("exist/{tenancyName}")]
        public async Task<bool> IsExist(string tenancyName)
        {
            return await _tenantAppService.Exist(tenancyName);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("apikey/{tenancyName}")]
        public async Task<ApiKeyDto> GetApiKey(string tenancyName)
        {
            return await _apiKeyService.GetApiKey(tenancyName);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("externalDomain/{externalDomain}")]
        public TenantInformationDto GetTenantByExternalDomain(string externalDomain)
        {
            return _tenantAppService.GetByExternalDomain(externalDomain);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("favicon/{tenancyName}")]
        public async Task<IActionResult> GetFavicon(string tenancyName)
        {
            Response.Headers.Add("cache-control", new[] { "public,max-age=31536000" });
            Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") });
            var faviconImageStream = await _tenantAppService.GetFaviconImage(tenancyName);
            return File(faviconImageStream, "image/png");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("brand/{tenancyName}")]
        public async Task<IActionResult> GetBrand(string tenancyName)
        {
            var faviconImageStream = await _tenantAppService.GetBrandImage(tenancyName);
            return File(faviconImageStream, "image/png");
        }
    }
}
