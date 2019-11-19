using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hozaru.ApplicationServices.Freights;
using Hozaru.ApplicationServices.Freights.Dtos;
using Hozaru.Core.Dependency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hozaru.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FreightController : HozaruApiController
    {
        private readonly IFreightAppService _freigthAppService;

        public FreightController()
        {
            _freigthAppService = IocManager.Instance.Resolve<IFreightAppService>();
        }

        [HttpPost]
        public IEnumerable<FreightDto> Get(GetFreightInputDto inputDto)
        {
            var freights = _freigthAppService.GetFreight(inputDto);
            return freights;
        }
    }
}