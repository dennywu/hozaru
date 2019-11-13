using Hozaru.Domain;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.Freights
{
    public class FreightMap : EntityMap<Freight, Guid>
    {
        public FreightMap() : base("Freights")
        {
            References(i => i.OriginCity).Column("Origin_City_Id").Index("freight_origin_city_id");
            References(i => i.OriginDistricts).Column("Origin_Districts_Id").Index("freight_origin_districts_id");
            References(i => i.DestinationCity).Column("Destination_City_Id").Index("freight_destination_city_id");
            References(i => i.DestinationDistricts).Column("Destination_Districts_Id").Index("freight_destination_districts_id");
            HasMany(i => i.Items)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .KeyColumn("Freight_Id")
                .ForeignKeyConstraintName("fk_freightitem_freight");
        }
    }
}
