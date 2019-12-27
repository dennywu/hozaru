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
            Map(i => i.OrderNumber).Length(24).Not.Nullable();
            Map(i => i.TransactionDate).Length(64).Not.Nullable();
            Map(i => i.DueDateConfirmation).Length(64).Not.Nullable();

            Component(i => i.Customer, e =>
            {
                e.Map(i => i.CustomerName).Length(64).Not.Nullable();
                e.Map(i => i.Email).Length(64).Not.Nullable();
                e.Map(i => i.WhatsappNumber).Length(32).Not.Nullable();
                e.Map(i => i.Address).Length(255).Not.Nullable();
                e.References(i => i.Districts).Column("Districts_Id").Index("order_districts_id");
            });

            Component(i => i.Shipment, e =>
            {
                e.References(i => i.ExpeditionService).Column("ExpeditionService_Id").Index("order_expedition_service_id");
                e.Map(i => i.AirWaybill).Length(32).Not.Nullable();
                e.Map(i => i.ShippingCost).Not.Nullable();
                e.Map(i => i.ShipmentDate).Nullable();
                e.Component(x => x.EstimatedTimeDelivery, z =>
                {
                    z.Map(i => i.EstimatedTimeDeliveryMin);
                    z.Map(i => i.EstimatedTimeDeliveryMax);
                });
                e.Map(i => i.ShipmentStatus).Length(32).Not.Nullable();
                e.Map(i => i.ProofOfDeliveryReceiver).Length(64).Not.Nullable();
                e.Map(i => i.ProofOfDeliveryDate).Nullable();
                e.HasMany(i => i.Trackings)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .KeyColumn("Order_Id")
                .ForeignKeyConstraintName("fk_ordershipmenttracking_order");
            });

            Component(i => i.Payment, e =>
                {
                    e.Map(i => i.LastPaymentDate).Nullable();
                    e.References(i => i.PaymentMethod).Column("PaymentMethod_Id").Index("order_paymentmethod_id");
                    e.HasMany(i => i.PaymentHistories)
                    .Cascade.AllDeleteOrphan()
                    .Inverse()
                    .KeyColumn("Order_Id")
                    .ForeignKeyConstraintName("fk_orderpayment_order");
                });

            Component(i => i.Summary, e =>
            {
                e.Map(i => i.SubTotal);
                e.Map(i => i.TotalShippingCost);
                e.Map(i => i.NetTotal);
            });

            HasMany(i => i.Items)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .KeyColumn("Order_Id")
                .ForeignKeyConstraintName("fk_orderitem_order");


            Map(i => i.Note).Length(255).Not.Nullable();
            Map(i => i.Status).Not.Nullable();
            this.MapAudited();
        }
    }
}
