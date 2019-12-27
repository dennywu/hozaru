using Hozaru.ApplicationServices.Districtses.Dtos;
using Hozaru.ApplicationServices.Expeditions.Dtos;
using Hozaru.ApplicationServices.PaymentMethods.Dtos;
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
        public OrderCustomerDto Customer { get; set; }
        public IList<OrderItemDto> Items { get; set; }
        public OrderShipmentDto Shipment { get; set; }
        public OrderPaymentDto Payment { get; set; }
        public OrderSummaryDto Summary { get; set; }
        public OrderStatus Status { get; set; }
        public string StatusText { get; set; }
        public string Note { get; set; }
        public string OrderUrl { get; set; }
    }
}
