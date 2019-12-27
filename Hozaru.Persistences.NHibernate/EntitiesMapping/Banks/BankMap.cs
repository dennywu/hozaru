using Hozaru.Domain;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.Banks
{
    public class BankMap : EntityMap<Bank, Guid>
    {
        public BankMap()
            : base("Banks")
        {
            Map(i => i.Code).Length(32).Not.Nullable();
            Map(i => i.Name).Length(64).Not.Nullable();
            Map(i => i.IsManualConfirmation).Not.Nullable();
            Map(i => i.BankName).Length(64).Not.Nullable();
            Map(i => i.ImageUrl).Length(64).Not.Nullable();

            this.MapAudited();
        }
    }
}
