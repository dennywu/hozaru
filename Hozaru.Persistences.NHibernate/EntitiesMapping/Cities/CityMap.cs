using Hozaru.Domain;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.Cities
{
    public class CityMap : EntityMap<City, Guid>
    {
        public CityMap() 
            : base("Cities")
        {
            Map(i => i.IdRajaOngkir).Nullable();
            Map(i => i.Code).Length(64).Not.Nullable();
            Map(i => i.Name).Length(64).Not.Nullable();
            Map(i => i.Type).Length(32).Not.Nullable();
            Map(i => i.PostalCode).Length(16).Not.Nullable();
            References(i => i.Province).Column("Province_Id").Index("city_province_id");
            this.MapAudited();
        }
    }
}
