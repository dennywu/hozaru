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
    public class ListOrderDtoConverter : ITypeConverter<Order, ListOrderDto>
    {

        public ListOrderDto Convert(ResolutionContext context)
        {
            if (context == null)
                return null;

            var order = (Order)context.SourceValue;

            return new ListOrderDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                CustomerName = order.CustomerName,
                Address = order.Address,
                TransactionDate = order.TransactionDate,
                Status = order.Status,
                StatusText = ((OrderStatus)order.Status).ToString(),
                City = order.Districts.City.Name,
                ExpeditionFullName = order.Expedition.FullName,
                PaymentType = order.PaymentType.Name,
                TotalSummary = order.Summary.Total
            };
        }
    }
}
