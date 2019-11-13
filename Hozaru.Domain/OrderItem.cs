using Hozaru.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class OrderItem : Entity<Guid>
    {
        public virtual Product Product { get; set; }
        public virtual decimal Price { get; set; }
        public virtual int Quantity { get; set; }
        public virtual string Note { get; set; }
        public virtual decimal Total { get; set; }

        protected OrderItem() { }

        public OrderItem(Product product, int quantity, string note)
        {
            this.Product = product;
            this.Quantity = quantity;
            this.Note = note;
            this.Price = product.Price;
            this.Total = Price * Quantity;
        }
    }
}
