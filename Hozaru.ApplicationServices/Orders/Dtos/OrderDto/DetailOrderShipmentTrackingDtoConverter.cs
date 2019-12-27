using AutoMapper;
using Hozaru.ApplicationServices.Expeditions.Dtos;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class DetailOrderShipmentTrackingDtoConverter : ITypeConverter<Order, DetailOrderShipmentTrackingDto>
    {
        public DetailOrderShipmentTrackingDto Convert(ResolutionContext context)
        {
            if (context == null)
                return null;

            var order = (Order)context.SourceValue;
            return new DetailOrderShipmentTrackingDto()
            {
                Id = order.Id,
                AirWayBill = order.Shipment.AirWaybill,
                EstimatedTimeDeliverySentence = order.Shipment.ShipmentDate.HasValue ? order.Shipment.EstimatedTimeDelivery.GetEstimatedTimeDeliverySentence(order.Shipment.ShipmentDate.Value) : "Pesanan belum dikirim",
                OrderNumber = order.OrderNumber,
                ShippingCost = order.Shipment.ShippingCost,
                ExpeditionService = Mapper.Map<ExpeditionServiceDto>(order.Shipment.ExpeditionService),
                ShipmentTrackings = Mapper.Map<IList<OrderShipmentTrackingDto>>(order.Shipment.Trackings.OrderByDescending(i => i.TrackingDate))
            };
        }
    }
}
