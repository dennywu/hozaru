using AutoMapper;
using Hozaru.ApplicationServices.Districtses.Dtos;
using Hozaru.ApplicationServices.Expeditions.Dtos;
using Hozaru.ApplicationServices.PaymentMethods.Dtos;
using Hozaru.Domain;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Hozaru.Whatsapp;
using Hozaru.Core.Configurations;
using Hozaru.Core.Dependency;
using Hozaru.Identity.MultiTenancy;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class OrderDtoConverter : ITypeConverter<Order, OrderDto>
    {

        public OrderDto Convert(ResolutionContext context)
        {
            if (context == null)
                return null;

            var order = (Order)context.SourceValue;
            var tenantManager = IocManager.Instance.Resolve<TenantManager>();
            var apiDomainName = AppSettingConfigurationHelper.GetSection("APIDomainName").Value;
            var tenant = tenantManager.FindByIdAsync(order.TenantId).Result;

            var httpProtocol = AppSettingConfigurationHelper.GetSection("MultiTenancyHttpProtocol").Value;
            var domainName = AppSettingConfigurationHelper.GetSection("MultiTenancyDomainName").Value;
            string orderUrl = string.Format("{0}://{1}.{2}/order/{3}", httpProtocol, tenant.TenancyName, domainName, order.Id);

            if (!tenant.ExternalDomain.IsNullOrWhiteSpace())
            {
                orderUrl = string.Format("{0}://{1}/order/{2}", "http", tenant.ExternalDomain, order.Id);
            }

            return new OrderDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                TransactionDate = order.TransactionDate,
                DueDateConfirmation = order.DueDateConfirmation,
                Customer = Mapper.Map<OrderCustomerDto>(order.Customer),
                Shipment = Mapper.Map<OrderShipmentDto>(order.Shipment),
                Payment = Mapper.Map<OrderPaymentDto>(order.Payment),
                Note = order.Note,
                Status = order.Status,
                StatusText = ((OrderStatus)order.Status).ToString(),
                Summary = Mapper.Map<OrderSummaryDto>(order.Summary),
                Items = Mapper.Map<IList<OrderItemDto>>(order.Items),
                OrderUrl = orderUrl
            };
        }
    }
}
