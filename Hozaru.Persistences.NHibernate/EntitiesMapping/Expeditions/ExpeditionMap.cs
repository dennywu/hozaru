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
            Map(i => i.RajaOngkirCode).Length(64).Not.Nullable();
            Map(i => i.Code).Length(64).Not.Nullable();
            Map(i => i.Name).Length(64).Not.Nullable();
            Map(i => i.AliasName).Length(64).Not.Nullable();
            HasMany(i => i.Services)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .KeyColumn("Expedition_Id")
                .ForeignKeyConstraintName("fk_expeditionservice_expedition");

            this.MapAudited();
        }
    }
}
