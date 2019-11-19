using Hozaru.Domain;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.Orders
{
    public class OrderPaymentMap : EntityMap<OrderPayment, Guid>
    {
        public OrderPaymentMap()
            : base("OrderPaymentHistories")
        {
            Map(i => i.PaymentDate).Not.Nullable();
            Map(i => i.ReceiptImageUrl).Length(256).Not.Nullable();
            Map(i => i.PaymentBankName).Length(64).Not.Nullable();
            Map(i => i.PaymentAccountName).Length(64).Not.Nullable();
            Map(i => i.PaymentAccountNumber).Length(32).Not.Nullable();
            References(i => i.Order).Column("Order_Id").ForeignKey("fk_order_orderpayment");
            this.MapAudited();
        }
    }
}
