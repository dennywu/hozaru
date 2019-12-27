using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Hozaru.ApplicationServices.RajaOngkir.Dtos
{
    public class ApiRajaOngkirShippingCostResponseDto
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("costs")]
        public IList<ApiRajaOngkirShippingCostByServiceResponseDto> Results { get; set; }
    }

    public class ApiRajaOngkirShippingCostByServiceResponseDto
    {
        [JsonProperty("service")]
        public string ServiceName { get; set; }

        [JsonProperty("description")]
        public string ServiceDescription { get; set; }

        [JsonIgnore]
        public decimal Cost { get { return Costs.FirstOrDefault().Cost; } }

        [JsonIgnore]
        public string EstimatedToDelivery { get { return Costs.FirstOrDefault().EstimatedToDelivery; } }

        [JsonProperty("cost")]
        public IList<ApiRajaOngkirShippingCostResultResponseDto> Costs { get; set; }
    }

    public class ApiRajaOngkirShippingCostResultResponseDto
    {
        [JsonProperty("value")]
        public decimal Cost { get; set; }

        [JsonProperty("etd")]
        public string EstimatedToDelivery { get; set; }
    }
}
