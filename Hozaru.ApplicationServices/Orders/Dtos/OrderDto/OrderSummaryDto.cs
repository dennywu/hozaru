using Hozaru.AutoMapper;
using Hozaru.Core.Application.Services.Dto;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    [AutoMapFrom(typeof(OrderSummary))]
    public class OrderSummaryDto
    {
        public decimal SubTotal { get; set; }
        public decimal TotalShippingCost { get; set; }
        public decimal NetTotal { get; set; }
    }
}
