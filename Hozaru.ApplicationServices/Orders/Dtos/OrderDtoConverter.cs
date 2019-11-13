using AutoMapper;
using Hozaru.ApplicationServices.Districtses.Dtos;
using Hozaru.ApplicationServices.Expeditions.Dtos;
using Hozaru.ApplicationServices.PaymentTypes.Dtos;
using Hozaru.Domain;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class OrderDtoConverter : ITypeConverter<Order, OrderDto>
    {

        public OrderDto Convert(ResolutionContext context)
        {
            if (context == null)
                return null;

            var order = (Order)context.SourceValue;

            return new OrderDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                CustomerName = order.CustomerName,
                Address = order.Address,
                Email = order.Email,
                WhatsappNumber = order.WhatsappNumber,
                TransactionDate = order.TransactionDate,
                DueDateConfirmation = order.DueDateConfirmation,
                Note = order.Note,
                Status = order.Status,
                StatusText = ((OrderStatus)order.Status).ToString(),
                Districts = Mapper.Map<DistrictDto>(order.Districts),
                Expedition = Mapper.Map<ExpeditionDto>(order.Expedition),
                PaymentType = Mapper.Map<PaymentTypeFullDto>(order.PaymentType),
                Summary = Mapper.Map<OrderSummaryDto>(order.Summary),
                Items = Mapper.Map<IList<OrderItemDto>>(order.Items),
                LastPayment = Mapper.Map<OrderPaymentDto>(order.GetLastPayment())
            };
        }
    }
}
