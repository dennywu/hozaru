using Hozaru.Core.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.RajaOngkir.Dtos
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class ApiRajaOngkirTrackingDto
    {
        [JsonProperty("details.waybill_date")]
        public string WayBillDate { get; set; }

        [JsonProperty("details.waybill_time")]
        public string WayBillTime { get; set; }

        [JsonProperty("delivery_status.status")]
        public string Status { get; set; }

        [JsonProperty("delivery_status.pod_receiver")]
        public string ProofOfDeliveryReceiver { get; set; }

        [JsonProperty("delivery_status.pod_date")]
        public string ProofOfDeliveryDate { get; set; }

        [JsonProperty("delivery_status.pod_time")]
        public string ProofOfDeliveryTime { get; set; }

        [JsonProperty("manifest")]
        public IList<ApiRajaOngkirTrackingDetailDto> Details { get; set; }
    }

    public class ApiRajaOngkirTrackingDetailDto
    {
        [JsonProperty("manifest_code")]
        public string Code { get; set; }

        [JsonProperty("manifest_description")]
        public string Description { get; set; }

        [JsonProperty("manifest_date")]
        public string TrackingDate { get; set; }

        [JsonProperty("manifest_time")]
        public string TrackingTime { get; set; }

        [JsonProperty("city_name")]
        public string CityName { get; set; }
    }
}
