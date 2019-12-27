using AutoMapper;
using Hozaru.ApplicationServices.Districtses.Dtos;
using Hozaru.Core.Configurations;
using Hozaru.Domain.Orders;
using Hozaru.Whatsapp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class OrderCustomerDtoConverter : ITypeConverter<OrderCustomer, OrderCustomerDto>
    {
        public OrderCustomerDto Convert(ResolutionContext context)
        {
            if (context == null)
                return null;

            var orderCustomer = (OrderCustomer)context.SourceValue;
            return new OrderCustomerDto()
            {
                CustomerName = orderCustomer.CustomerName,
                Address = orderCustomer.GetCustomerFullAddress(),
                Email = orderCustomer.Email,
                Districts = Mapper.Map<DistrictDto>(orderCustomer.Districts),
                WhatsappNumber = orderCustomer.WhatsappNumber,
                WhatsappUrl = WhatsappNumberGeneratorHelper.GenerateWhatsappUrl(orderCustomer.WhatsappNumber),
            };
        }
    }
}
