using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hozaru.Web.Configurations;
using Hozaru.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Hozaru.Web.Controllers
{
    public class ApiKeyController : Controller
    {
        public async Task<string> Index()
        {
            var tenancyName = HttpContext.GetTenancyName();
            return await ApiClientHelper.GetApiKey(tenancyName);
        }
    }
}