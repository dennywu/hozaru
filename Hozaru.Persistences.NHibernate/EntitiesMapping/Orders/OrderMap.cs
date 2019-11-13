using Hozaru.Domain;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.Orders
{
    public class OrderMap : EntityMap<Order, Guid>
    {
        public OrderMap()
            : base("Orders")
        {
            Map(i => i.OrderNumber).Length(12).Not.Nullable();
            Map(i => i.TransactionDate).Length(64).Not.Nullable();
            Map(i => i.DueDateConfirmation).Length(64).Not.Nullable();
            Map(i => i.CustomerName).Length(64).Not.Nullable();
            Map(i => i.Email).Not.Nullable();
            Map(i => i.WhatsappNumber).Not.Nullable();
            References(i => i.Districts).Column("Districts_Id").Index("order_districts_id");
            Map(i => i.Address).Not.Nullable();
            References(i => i.Expedition).Column("Expedition_Id").Index("order_expedition_id");
            References(i => i.PaymentType).Column("PaymentType_Id").Index("order_paymenttype_id");
            Map(i => i.Note).Not.Nullable();
            Map(i => i.ShipingRatePerKG).Not.Nullable();
            Map(i => i.Status).Not.Nullable();
            HasMany(i => i.Items)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .KeyColumn("Order_Id")
                .ForeignKeyConstraintName("fk_orderitem_order");
            HasMany(i => i.PaymentHistories)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .KeyColumn("Order_Id")
                .ForeignKeyConstraintName("fk_orderpayment_order");
            Component(i => i.Summary,
                m =>
                {
                    m.Map(e => e.SubTotal);
                    m.Map(e => e.ShippingCost);
                    m.Map(e => e.Total);
                });
        }
    }
}
