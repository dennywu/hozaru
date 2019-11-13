using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Hozaru.Whatsapp
{
    public class WhatsappAPI
    {
        private static string _domainAPI = "https://console.wablas.com";
        private static string _token = "0Pphj2VhsDMqFCUTL5abYhelzC2dKeH1RqZVK8UbeXiPUVLz9VkYEiT7dz0e7BGp";

        public static void SendMessage(string phone, string message)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_domainAPI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_token);

                var data = new
                {
                    phone = phone,
                    message = message
                };
                var dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                var content = new StringContent(dataJson, Encoding.UTF8, "application/json");

                //var response = httpClient.PostAsync("/api/send-message", content).Result;
            }
        }
    }
}
