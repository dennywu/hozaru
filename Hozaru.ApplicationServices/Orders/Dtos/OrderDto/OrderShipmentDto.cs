using Hozaru.ApplicationServices.Expeditions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class OrderShipmentDto
    {
        public ExpeditionServiceDto ExpeditionService { get; set; }
        public string EstimatedTimeDeliverySentence { get; set; }
        public string AirWayBill { get; set; }
        public decimal ShippingCost { get; set; }
        public virtual DateTime? ShipmentDate { get; set; }
        public virtual DateTime? ProofOfDeliveryDate { get; set; }
        public OrderShipmentTrackingDto LastShipmentTracking { get; set; }
    }
}
