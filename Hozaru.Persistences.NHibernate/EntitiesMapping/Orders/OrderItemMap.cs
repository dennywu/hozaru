using Hozaru.Domain;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.Orders
{
    public class OrderItemMap : EntityMap<OrderItem, Guid>
    {
        public OrderItemMap()
            : base("OrderItems")
        {
            Map(i => i.Price).Length(12).Not.Nullable();
            Map(i => i.Quantity).Length(64).Not.Nullable();
            Map(i => i.Note).Length(256);
            Map(i => i.Total).Length(64).Not.Nullable();
            References(i => i.Product).Column("Product_Id").Index("orderitem_product_id");
        }
    }
}
