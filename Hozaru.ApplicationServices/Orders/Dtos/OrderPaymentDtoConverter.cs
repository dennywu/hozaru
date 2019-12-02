using AutoMapper;
using Hozaru.AutoMapper;
using Hozaru.Core.Application.Services.Dto;
using Hozaru.Core.Configurations;
using Hozaru.Domain;
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
            var apiDomainName = AppSettingConfigurationHelper.GetSection("APIDomainName").Value;
            return new OrderPaymentDto()
            {
                Id = orderPayment.Id,
                PaymentAccountName = orderPayment.PaymentAccountName,
                PaymentAccountNumber = orderPayment.PaymentAccountNumber,
                PaymentBankName = orderPayment.PaymentBankName,
                PaymentDate = orderPayment.PaymentDate,
                Url = string.Format("{0}/api/orders/{1}/payments/image/{2}?v={3}", apiDomainName, orderPayment.Order.Id, orderPayment.Id, orderPayment.LastModificationTime.Value.ToString("ddMMyyyHHmmss"))
            };
        }
    }
}
