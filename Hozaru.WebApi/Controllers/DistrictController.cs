using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hozaru.ApplicationServices.Districtses;
using Hozaru.ApplicationServices.Districtses.Dtos;
using Hozaru.Authentication;
using Hozaru.Core.Dependency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hozaru.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DistrictController : HozaruApiController
    {
        private readonly IDistrictAppService _districtAppService;
        public DistrictController()
        {
            _districtAppService = IocManager.Instance.Resolve<IDistrictAppService>();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.AllScheme)]
        public IEnumerable<DistrictDto> Get(Guid cityId, string searchKey)
        {
            searchKey = searchKey.IsNullOrWhiteSpace() ? string.Empty : searchKey;
            var districtses = _districtAppService.Search(cityId, searchKey);
            return districtses;
        }
    }
}