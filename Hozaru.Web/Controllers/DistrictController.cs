using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hozaru.ApplicationServices.Districtses;
using Hozaru.ApplicationServices.Districtses.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hozaru.Core.Extensions;

namespace Hozaru.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<DistrictDto> Get(string cityCode, string searchKey)
        {
            searchKey = searchKey.IsNullOrWhiteSpace() ? string.Empty : searchKey;
            var districtses = IoCManager.GetInstance<IDistrictAppService>().Search(cityCode, searchKey);
            return districtses;
        }
    }
}