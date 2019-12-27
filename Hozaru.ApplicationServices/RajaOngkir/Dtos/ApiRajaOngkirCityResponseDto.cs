using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Hozaru.ApplicationServices.RajaOngkir.Dtos
{
    public class ApiRajaOngkirCityResponseDto
    {
        [JsonProperty(PropertyName = "Province_Id")]
        public int ProvinceId { get; set; }

        [JsonProperty(PropertyName = "City_Id")]
        public int CityId { get; set; }

        [JsonProperty(PropertyName = "City_Name")]
        public string CityName { get; set; }

        [JsonProperty(PropertyName = "Type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "Postal_Code")]
        public string PostalCode { get; set; }
    }
}
