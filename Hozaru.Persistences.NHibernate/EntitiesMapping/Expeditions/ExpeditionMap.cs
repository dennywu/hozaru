using Hozaru.Domain;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.Expeditions
{
    public class ExpeditionMap : EntityMap<Expedition, Guid>
    {
        public ExpeditionMap() :
            base("Expeditions")
        {
            Map(i => i.Code).Length(12).Not.Nullable();
            Map(i => i.Name).Length(64).Not.Nullable();
            Map(i => i.CompanyCode).Length(64).Not.Nullable();
            Map(i => i.CompanyName).Length(64).Not.Nullable();
            Map(i => i.Disabled).Not.Nullable();
            this.MapAudited();
        }
    }
}
