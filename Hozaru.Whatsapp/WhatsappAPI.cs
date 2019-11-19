using Castle.Core.Logging;
using Hozaru.Core;
using Hozaru.Core.Configurations;
using Hozaru.Core.Dependency;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Hozaru.Whatsapp
{
    public class WhatsappAPI
    {
        public static void SendMessage(string phone, string message)
        {
            if (Convert.ToBoolean(AppSettingConfigurationHelper.GetSection("SendWhatsapp").Value))
            {
                var domainAPI = AppSettingConfigurationHelper.GetSection("Wablas").GetSection("DomainAPI").Value;
                var token = AppSettingConfigurationHelper.GetSection("Wablas").GetSection("Token").Value;

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(domainAPI);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                    var data = new
                    {
                        phone = phone,
                        message = message
                    };
                    var dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                    var content = new StringContent(dataJson, Encoding.UTF8, "application/json");

                    var response = httpClient.PostAsync("/api/send-message", content).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        var logger = IocManager.Instance.Resolve<ILogger>();
                        logger.ErrorFormat("Failed send Whatstapp {0} with message: {1}. Error Message: {2}", phone, message, response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
        }
    }
}
