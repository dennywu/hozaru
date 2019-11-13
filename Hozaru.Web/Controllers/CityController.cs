using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hozaru.ApplicationServices.Cities;
using Hozaru.ApplicationServices.Cities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hozaru.Core.Extensions;
using Hozaru.Core.Dependency;

namespace Hozaru.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private ICityAppService _cityAppService;

        public CityController()
        {
            _cityAppService = IocManager.Instance.Resolve<ICityAppService>();
        }

        [HttpGet]
        public IEnumerable<CityDto> Get(string searchKey = "")
        {
            searchKey = searchKey.IsNullOrWhiteSpace() ? string.Empty : searchKey;
            var cities = _cityAppService.Search(searchKey);
            return cities;
        }
    }
}