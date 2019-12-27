using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hozaru.Web.Configurations
{
    public static class HttpHeaderHelper
    {
        public static string GetTenancyName(this HttpContext httpContext)
        {
            var tenancyName = httpContext.Items.FirstOrDefault(i => i.Key.ToString() == HttpContextConstant.TENANCY_NAME).Value.ToString();
            return tenancyName;
        }
    }
}
