using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Hozaru.ApplicationServices.RajaOngkir.Dtos
{
    public class ApiRajaOngkirProvinceResponseDto
    {
        [JsonProperty(PropertyName = "Province_Id")]
        public int ProvinceId { get; set; }

        [JsonProperty(PropertyName = "Province")]
        public string ProvinceName { get; set; }
    }
}
