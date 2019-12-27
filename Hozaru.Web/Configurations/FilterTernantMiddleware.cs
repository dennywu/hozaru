using Hozaru.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Hozaru.Web.Configurations
{
    public class FilterTernantMiddleware
    {
        private readonly RequestDelegate _next;

        public FilterTernantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var filtered = new string[] { ".png", ".jpg", ".js", ".css", ".map", "sockjs-node" };
            foreach (var filter in filtered)
            {
                if (httpContext.Request.Path.Value.Contains(filter))
                {
                    await _next(httpContext);
                    return;
                }
            }

            var isFromProxtNginx = httpContext.Request.Headers["X-NginX-Proxy"].Any() ? Convert.ToBoolean(httpContext.Request.Headers["X-NginX-Proxy"]) : false;
            if (isFromProxtNginx)
            {
                var externalHostName = httpContext.Request.Headers["ExternalHost"].ToString().RemovePreFix("http://", "https://").RemovePostFix("/");
                var tenant = await ApiClientHelper.GetTenantFromExternalDomain(externalHostName);
                if (tenant == null)
                {
                    await httpContext.Response.WriteAsync("Pastikan Anda memasukan Url Website yang benar.");
                }
                httpContext.Items.Add(HttpContextConstant.TENANCY_NAME, tenant.TenancyName);
            }
            else
            {
                var domainFormatConfiguration = AppSettingConfigurationHelper.GetSection("DomainFormat").Value;
                var hostName = httpContext.Request.Headers["Host"].ToString().RemovePreFix("http://", "https://").RemovePostFix("/");
                var domainFormat = domainFormatConfiguration.RemovePreFix("http://", "https://").Split(':')[0].RemovePostFix("/");
                var result = new FormattedStringValueExtracter().Extract(hostName, domainFormat, true, '/');

                var tenancyName = string.Empty;
                if (!result.IsMatch || !result.Matches.Any())
                {
                    await httpContext.Response.WriteAsync("Pastikan Anda memasukan Url Website yang benar.");
                }

                tenancyName = result.Matches[0].Value;
                if (string.Equals(tenancyName, "www", StringComparison.OrdinalIgnoreCase))
                {
                    await httpContext.Response.WriteAsync("Pastikan Anda memasukan Url Website yang benar.");
                }

                if (await ApiClientHelper.TenantIsExist(tenancyName) == false)
                {
                    await httpContext.Response.WriteAsync("Pastikan Anda memasukan Url Website yang benar.");
                }

                httpContext.Items.Add(HttpContextConstant.TENANCY_NAME, tenancyName);
            }

            await _next(httpContext);
        }
    }
}
