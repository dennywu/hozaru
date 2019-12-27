using AutoMapper;
using Hozaru.ApplicationServices.PaymentMethods.Dtos;
using Hozaru.Core.Configurations;
using Hozaru.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class OrderPaymentDtoConverter : ITypeConverter<OrderPayment, OrderPaymentDto>
    {
        public OrderPaymentDto Convert(ResolutionContext context)
        {
            if (context == null)
                return null;

            var orderPayment = (OrderPayment)context.SourceValue;
            return new OrderPaymentDto()
            {
                PaymentMethod = Mapper.Map<PaymentMethodFullDto>(orderPayment.PaymentMethod),
                LastPaymentDate = orderPayment.LastPaymentDate,
                LastPayment = Mapper.Map<OrderPaymentHistoryDto>(orderPayment.GetLastPayment())
            };
        }
    }
}
