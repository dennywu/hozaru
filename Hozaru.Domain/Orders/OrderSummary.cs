using Hozaru.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Hozaru.Core.Domain.Entities.Auditing;

namespace Hozaru.Domain
{
    public class OrderSummary
    {
        public virtual decimal SubTotal { get; set; }
        public virtual decimal TotalShippingCost { get; set; }
        public virtual decimal NetTotal { get; set; }

        public OrderSummary() { }

        public virtual void Calculate(Order order)
        {
            this.SubTotal = order.Items.Sum(i => i.Total);
            this.TotalShippingCost = order.Shipment.ShippingCost;
            this.NetTotal = SubTotal + TotalShippingCost;
        }
    }
}
