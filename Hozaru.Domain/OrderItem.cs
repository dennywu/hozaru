using Hozaru.Core.Domain.Entities;
using Hozaru.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class OrderItem : AuditedEntity<Guid>
    {
        public virtual Product Product { get; set; }
        public virtual decimal Price { get; set; }
        public virtual int Quantity { get; set; }
        public virtual string Note { get; set; }
        public virtual decimal Total { get; set; }
        public virtual Order Order { get; set; }

        protected OrderItem() { }

        private OrderItem(Order order)
            : this()
        {
            this.Order = order;
        }

        public OrderItem(Order order, Product product, int quantity, string note)
            : this(order)
        {
            this.Product = product;
            this.Quantity = quantity;
            this.Note = note;
            this.Price = product.Price;
            this.Total = Price * Quantity;
        }
    }
}
