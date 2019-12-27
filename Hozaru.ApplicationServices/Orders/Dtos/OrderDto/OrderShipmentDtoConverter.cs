using AutoMapper;
using Hozaru.ApplicationServices.Expeditions.Dtos;
using Hozaru.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class OrderShipmentDtoConverter : ITypeConverter<OrderShipment, OrderShipmentDto>
    {
        public OrderShipmentDto Convert(ResolutionContext context)
        {
            if (context == null)
                return null;

            var orderShipment = (OrderShipment)context.SourceValue;
            return new OrderShipmentDto()
            {
                AirWayBill = orderShipment.AirWaybill,
                ExpeditionService = Mapper.Map<ExpeditionServiceDto>(orderShipment.ExpeditionService),
                ShippingCost = orderShipment.ShippingCost,
                ShipmentDate = orderShipment.ShipmentDate,
                ProofOfDeliveryDate = orderShipment.ProofOfDeliveryDate,
                EstimatedTimeDeliverySentence = orderShipment.ShipmentDate.HasValue ? orderShipment.EstimatedTimeDelivery.GetEstimatedTimeDeliverySentence(orderShipment.ShipmentDate.Value) : "Pesanan belum dikirim",
                LastShipmentTracking = Mapper.Map<OrderShipmentTrackingDto>(orderShipment.GetLastTracking())
            };
        }
    }
}
