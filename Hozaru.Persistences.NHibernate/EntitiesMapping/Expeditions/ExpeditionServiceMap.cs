using Hozaru.Domain;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.Expeditions
{
    public class ExpeditionServiceMap : EntityMap<ExpeditionService>
    {
        public ExpeditionServiceMap() : base("ExpeditionServices")
        {
            Map(i => i.RajaOngkirCode).Length(64).Not.Nullable();
            Map(i => i.Code).Length(64).Not.Nullable();
            Map(i => i.Name).Length(64).Not.Nullable();
            Map(i => i.AliasName).Length(64).Not.Nullable();
            Map(i => i.GroupName).Length(64).Not.Nullable();
            References(i => i.Expedition).Column("Expedition_Id").Index("expeditionservice_expedition_id");

            this.MapAudited();
        }
    }
}
