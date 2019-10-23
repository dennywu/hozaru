using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hozaru.ApplicationServices.Freights;
using Hozaru.ApplicationServices.Freights.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hozaru.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreightController : ControllerBase
    {
        [HttpPost]
        public IEnumerable<FreightDto> Get(GetFreightInputDto inputDto)
        {
            var freights = IoCManager.GetInstance<IFreightAppService>().GetFreight(inputDto);
            return freights;
        }
    }
}