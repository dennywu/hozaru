using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Hozaru.ApplicationServices.RajaOngkir.Dtos
{
    public class ApiRajaOngkirShippingCostServiceResponseDto
    {
        public string ServiceName { get; set; }

        public string ServiceDescription { get; set; }

        public decimal Cost { get; set; }

        public string EstimatedToDelivery { get; set; }
    }
}
