using Hozaru.ApplicationServices.Products.Dtos;
using Hozaru.AutoMapper;
using Hozaru.Core.Application.Services.Dto;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    [AutoMapFrom(typeof(OrderItem))]
    public class OrderItemDto : EntityDto<Guid>
    {
        public ProductDto Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
        public decimal Total { get; private set; }
    }
}
