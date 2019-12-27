using AutoMapper;
using Hozaru.ApplicationServices.Districtses.Dtos;
using Hozaru.ApplicationServices.Expeditions.Dtos;
using Hozaru.ApplicationServices.PaymentMethods.Dtos;
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
                CustomerName = order.Customer.CustomerName,
                Address = order.Customer.GetCustomerFullAddress(),
                TransactionDate = order.TransactionDate,
                Status = order.Status,
                StatusText = ((OrderStatus)order.Status).ToString(),
                City = order.Customer.Districts.City.Name,
                ExpeditionServiceFullName = order.Shipment.ExpeditionService.FullName,
                PaymentMethod = order.Payment.PaymentMethod.Bank.Name,
                TotalSummary = order.Summary.NetTotal
            };
        }
    }
}
