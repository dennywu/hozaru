using Hozaru.ApplicationServices.Expeditions.Dtos;
using Hozaru.Core.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class DetailOrderShipmentTrackingDto : EntityDto<Guid>
    {
        public string OrderNumber { get; set; }
        public ExpeditionServiceDto ExpeditionService { get; set; }
        public string EstimatedTimeDeliverySentence { get; set; }
        public string AirWayBill { get; set; }
        public decimal ShippingCost { get; set; }
        public IList<OrderShipmentTrackingDto> ShipmentTrackings { get; set; }
    }
}
