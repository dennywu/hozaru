using Hozaru.Domain.Orders;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.Orders
{
    public class OrderShipmentTrackingMap : EntityMap<OrderShipmentTracking>
    {
        public OrderShipmentTrackingMap() : base("OrderShipmentTrackings")
        {
            Map(i => i.Code).Length(6).Not.Nullable();
            Map(i => i.Description).Length(255).Not.Nullable();
            Map(i => i.TrackingDate).Not.Nullable();
            Map(i => i.CityName).Length(128).Not.Nullable();
            References(i => i.Order).Column("Order_Id").Index("ordershipmenttracking_order_id");

            this.MapAudited();
        }
    }
}
