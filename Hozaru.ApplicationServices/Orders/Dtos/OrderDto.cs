using Hozaru.ApplicationServices.Districtses.Dtos;
using Hozaru.ApplicationServices.Expeditions.Dtos;
using Hozaru.ApplicationServices.PaymentTypes.Dtos;
using Hozaru.Core.Application.Services.Dto;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class OrderDto : EntityDto<Guid>
    {
        public string OrderNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime DueDateConfirmation { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string WhatsappNumber { get; set; }
        public string WhatsappUrl { get; set; }
        public DistrictDto Districts { get; set; }
        public OrderStatus Status { get; set; }
        public string StatusText { get; set; }
        public string Address { get; set; }
        public ExpeditionDto Expedition { get; set; }
        public PaymentTypeFullDto PaymentType { get; set; }
        public string Note { get; set; }
        public string AirWaybill { get; set; }
        public OrderPaymentDto LastPayment { get; set; }
        public IList<OrderItemDto> Items { get; set; }
        public OrderSummaryDto Summary { get; set; }
    }
}
