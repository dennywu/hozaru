using Hozaru.ApplicationServices.Tenants;
using Hozaru.ApplicationServices.Tenants.Dtos;
using Hozaru.Core.Dependency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hozaru.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TenantController : HozaruApiController
    {
        private ITenantAppService _tenantAppService;

        public TenantController()
        {
            _tenantAppService = IocManager.Instance.Resolve<ITenantAppService>();
        }

        [HttpGet]
        public TenantInformationDto Get()
        {
            var tenantInformation = _tenantAppService.GetInformation();
            return tenantInformation;
        }
    }
}
