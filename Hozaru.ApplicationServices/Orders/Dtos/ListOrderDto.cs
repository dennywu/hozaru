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
    public class ListOrderDto : EntityDto<Guid>
    {
        public string OrderNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CustomerName { get; set; }
        public string City { get; set; }
        public OrderStatus Status { get; set; }
        public string StatusText { get; set; }
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
        public string ExpeditionServiceFullName { get; set; }
        public decimal TotalSummary { get; set; }
    }
}
