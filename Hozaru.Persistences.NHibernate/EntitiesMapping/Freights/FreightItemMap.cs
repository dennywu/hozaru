using Hozaru.Domain;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.Freights
{
    public class FreightItemMap : EntityMap<FreightItem, Guid>
    {
        public FreightItemMap() 
            : base("FreightItems")
        {
            Map(i => i.Rate).Not.Nullable();
            Map(i => i.EstimatedTimeDepartureMin).Not.Nullable();
            Map(i => i.EstimatedTimeDepartureMax).Not.Nullable();
            References(i => i.Expedition).Column("Expedition_Id").Index("freightitem_expedition_id");
            References(i => i.Freight).Column("Freight_Id").ForeignKey("fk_freight_freightitem");
            this.MapAudited();
        }
    }
}
