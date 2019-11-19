using Hozaru.ApplicationServices.Cities;
using Hozaru.ApplicationServices.Cities.Dtos;
using Hozaru.Core.Dependency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Hozaru.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CityController : HozaruApiController
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