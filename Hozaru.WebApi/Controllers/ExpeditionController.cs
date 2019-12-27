using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hozaru.ApplicationServices.Expeditions;
using Hozaru.ApplicationServices.Expeditions.Dtos;
using Hozaru.Core.Dependency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hozaru.WebApi.Controllers
{
    [Route("api/expeditions")]
    [ApiController]
    [Authorize]
    public class ExpeditionController : ControllerBase
    {
        private IExpeditionAppService _expeditionService;

        public ExpeditionController()
        {
            _expeditionService = IocManager.Instance.Resolve<IExpeditionAppService>();
        }

        [HttpGet]
        [Route("tenantexpeditionservices")]
        public IList<TenantExpeditionServiceDto> GetAll()
        {
            var result = _expeditionService.GetAllTenantExpeditionService();
            return result;
        }

        [HttpPut]
        [Route("tenantexpeditionservices")]
        public TenantExpeditionServiceDto UpdateStatusTenantExpeditionService(UpdateStatusTenantExpeditionServiceInputDto inputDto)
        {
            var result = _expeditionService.UpdateStatusTenantExpeditionService(inputDto);
            return result;
        }
    }
}