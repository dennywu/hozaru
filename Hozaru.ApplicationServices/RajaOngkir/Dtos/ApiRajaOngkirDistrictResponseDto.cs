using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Hozaru.ApplicationServices.RajaOngkir.Dtos
{
    public class ApiRajaOngkirDistrictResponseDto
    {
        [JsonProperty(PropertyName = "City_Id")]
        public int CityId { get; set; }

        [JsonProperty(PropertyName = "subdistrict_id")]
        public int DistrictId { get; set; }

        [JsonProperty(PropertyName = "subdistrict_name")]
        public string DistrictName { get; set; }
    }
}
