using Hozaru.Domain;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.PaymentTypes
{
    public class PaymentTypeMap : EntityMap<PaymentType, Guid>
    {
        public PaymentTypeMap()
            : base("PaymentTypes")
        {
            Map(i => i.Code).Length(32).Not.Nullable();
            Map(i => i.Name).Length(64).Not.Nullable();
            Map(i => i.IsManualConfirmation).Not.Nullable();
            Map(i => i.BankName).Length(64).Not.Nullable();
            Map(i => i.BankBranch).Length(64).Not.Nullable();
            Map(i => i.AccountName).Length(64).Not.Nullable();
            Map(i => i.AccountNumber).Length(64).Not.Nullable();
            Map(i => i.ImageUrl).Length(64).Not.Nullable();
            Map(i => i.Disabled).Not.Nullable();
            this.MapAudited();
        }
    }
}
