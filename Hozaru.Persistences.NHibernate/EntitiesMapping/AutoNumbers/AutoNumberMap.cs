using Hozaru.Domain;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.AutoNumbers
{
    public class AutoNumberMap : EntityMap<AutoNumber,Guid>
    {
        public AutoNumberMap()
            : base("AutoNumbers")
        {

            Map(i => i.Date).Length(6).Not.Nullable();
            Map(i => i.Number).Not.Nullable();
            this.MapAudited();
        }
    }
}
