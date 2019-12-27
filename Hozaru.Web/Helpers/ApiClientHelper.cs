using Hozaru.Web.Configurations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Hozaru.Web.Helpers
{
    public static class ApiClientHelper
    {
        public static async Task<bool> TenantIsExist(string tenancyName)
        {
            using (var client = new HttpClient())
            {
                var url = ConfigurationHelper.GetSection("APIDomainName").Value;
                client.BaseAddress = new Uri(url);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage response = client.GetAsync("api/tenants/exist/" + tenancyName).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                var isExist = await response.Content.ReadAsStringAsync();
                return Convert.ToBoolean(isExist);
            }
        }

        public static async Task<string> GetApiKey(string tenancyName)
        {
            using (var client = new HttpClient())
            {
                var url = ConfigurationHelper.GetSection("APIDomainName").Value;
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage response = client.GetAsync("api/tenants/apikey/" + tenancyName).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return "";
                }

                var apiKey = await response.Content.ReadAsStringAsync();
                return apiKey;
            }
        }

        internal static async Task<TenantInfo> GetTenantFromExternalDomain(string externalHostName)
        {
            using (var client = new HttpClient())
            {
                var url = ConfigurationHelper.GetSection("APIDomainName").Value;
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage response = client.GetAsync("api/tenants/externalDomain/" + externalHostName).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var tenantInfo = JsonConvert.DeserializeObject<TenantInfo>(await response.Content.ReadAsStringAsync());
                return tenantInfo;
            }
        }
    }
}
