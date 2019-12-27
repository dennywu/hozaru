using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.RajaOngkir.Dtos
{
    public class GetShippingCostByServiceInputDto
    {
        public Districts Origin { get; set; }
        public Districts Destination { get; set; }
        public decimal Weight { get; set; }
        public ExpeditionService ExpeditionService { get; set; }
    }
}
