using Hozaru.Domain;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.Expeditions
{
    public class TenantExpeditionMap : EntityMap<TenantExpeditionService>
    {
        public TenantExpeditionMap()
            : base("TenantExpeditions")
        {
            Map(i => i.IsActive).Not.Nullable();
            References(i => i.ExpeditionService).Column("Expedition_Id").Index("tenantexpedition_expedition_id");
        }
    }
}
