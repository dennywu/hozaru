using Hozaru.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Hozaru.Domain
{
    public class OrderSummary : Entity<Guid>
    {
        public virtual decimal SubTotal { get; set; }
        public virtual decimal ShippingCost { get; set; }
        public virtual decimal Total { get; set; }

        public OrderSummary() { }

        public virtual void Calculate(IList<OrderItem> items, decimal shipingRatePerKG)
        {
            var totalWeightInKG = getTotalWeight(items);
            var shippingCost = totalWeightInKG * shipingRatePerKG;

            SubTotal = items.Sum(i => i.Total);
            ShippingCost = shippingCost;
            Total = SubTotal + ShippingCost;
        }

        private decimal getTotalWeight(IList<OrderItem> items)
        {
            decimal weightShoppingCart = 0;
            foreach (var item in items)
            {
                var weight = item.Product.Weight * item.Quantity;
                weightShoppingCart += weight;
            }

            var weightInKG = Math.Ceiling(weightShoppingCart / 1000);
            return weightInKG;
        }
    }
}
