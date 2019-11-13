using Hozaru.Domain;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.Districtses
{
    public class DistrictMap : EntityMap<Districts, Guid>
    {
        public DistrictMap():
            base("Districtses")
        {
            Map(i => i.Code).Length(12).Not.Nullable();
            Map(i => i.Name).Length(64).Not.Nullable();
            References(i => i.City).Column("City_Id").Index("districts_city_id");
        }
    }
}
