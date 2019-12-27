using Hozaru.AutoMapper;
using Hozaru.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    [AutoMapFrom(typeof(OrderShipmentTracking))]
    public class OrderShipmentTrackingDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime TrackingDate { get; set; }
        public string CityName { get; set; }
    }
}
