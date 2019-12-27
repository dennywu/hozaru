using Hozaru.Authentication;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.ApiKesys
{
    public class ApiKeyMap : EntityMap<ApiKey, Guid>
    {
        public ApiKeyMap()
            : base("ApiKeys")
        {
            Map(i => i.Key).Length(64).Not.Nullable();
            Map(i => i.TenantId).Not.Nullable();
            this.MapAudited();
        }
    }
}
