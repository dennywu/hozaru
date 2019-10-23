using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Freights.Dtos
{
    public class FreightShoppingCartItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
