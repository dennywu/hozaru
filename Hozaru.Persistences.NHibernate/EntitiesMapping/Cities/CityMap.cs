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
            Map(i => i.Code).Length(12).Not.Nullable();
            Map(i => i.Name).Length(64).Not.Nullable();
        }
    }
}
