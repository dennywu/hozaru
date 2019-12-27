using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hozaru.ApplicationServices.Configurations;
using Hozaru.ApplicationServices.RajaOngkir;
using Hozaru.Core.Dependency;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hozaru.WebApi.Controllers
{
    [Route("api/configurations")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private IRajaOngkirService _rajaOngkirService;
        private IConfigurationTenantService _configurationTenantService;

        public ConfigurationController()
        {
            _rajaOngkirService = IocManager.Instance.Resolve<IRajaOngkirService>();
            _configurationTenantService = IocManager.Instance.Resolve<IConfigurationTenantService>();
        }

        [HttpPost]
        [Route("collectMasterDataRajaOngkir")]
        public IActionResult CollectOrUpdateProvinceCityAndSubDistrict()
        {
            _rajaOngkirService.CollectOrUpdateProvinceCityAndSubDistrict();
            return Ok();
        }

        [HttpPost]
        [Route("configureTenant")]
        public IActionResult configureTenant()
        {
            _configurationTenantService.Configure();
            return Ok();
        }
    }
}