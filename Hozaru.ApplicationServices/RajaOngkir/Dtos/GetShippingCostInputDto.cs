using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.RajaOngkir.Dtos
{
    public class GetShippingCostInputDto
    {
        public Districts Origin { get; set; }
        public Districts Destination { get; set; }
        public decimal Weight { get; set; }
        public IList<Expedition> Expeditions { get; set; }
    }
}
