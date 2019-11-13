using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class CreateOrderItemInputDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
    }
}
