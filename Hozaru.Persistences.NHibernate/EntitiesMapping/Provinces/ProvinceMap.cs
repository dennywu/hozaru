using Hozaru.Domain;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.Provinces
{
    public class ProvinceMap : EntityMap<Province, Guid>
    {
        public ProvinceMap()
            : base("Provinces")
        {
            Map(i => i.IdRajaOngkir).Nullable();
            Map(i => i.Code).Length(64).Not.Nullable();
            Map(i => i.Name).Length(64).Not.Nullable();
            this.MapAudited();
        }
    }
}
