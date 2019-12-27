using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.RajaOngkir.Dtos
{
    public class GetTrackingInputDto
    {
        public string AirWayBill { get; set; }
        public ExpeditionService ExpeditionService { get; set; }
    }
}
