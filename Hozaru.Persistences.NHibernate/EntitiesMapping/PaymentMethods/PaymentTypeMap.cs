using Hozaru.Domain;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.PaymentMethods
{
    public class PaymentMethodMap : EntityMap<PaymentMethod, Guid>
    {
        public PaymentMethodMap()
            : base("PaymentMethods")
        {
            References(i => i.Bank).Column("Bank_Id").Index("paymentmethods_bank_id");
            Map(i => i.BankBranch).Length(64).Not.Nullable();
            Map(i => i.AccountName).Length(64).Not.Nullable();
            Map(i => i.AccountNumber).Length(64).Not.Nullable();
            Map(i => i.Disabled).Not.Nullable();
            this.MapAudited();
        }
    }
}
