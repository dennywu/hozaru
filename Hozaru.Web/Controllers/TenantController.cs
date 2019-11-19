using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hozaru.ApplicationServices.Tenants;
using Hozaru.ApplicationServices.Tenants.Dtos;
using Hozaru.Core.Dependency;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hozaru.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
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