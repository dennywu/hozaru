using Hozaru.Core.Identity.MultiTenancy;
using Hozaru.Core.Identity.NHibernate.EntityMappings;
using Hozaru.Identity.MultiTenancy;
using Hozaru.Identity.Users;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.Identity
{
    public class TenantMap : EntityMap<Tenant, int>
    {
        public TenantMap()
            : base("Tenants")
        {
            References(x => x.Edition).Column("EditionId").Nullable();

            Map(x => x.WhatsappNumber);
            Map(x => x.TenancyName);
            Map(x => x.Name);
            Map(x => x.IsActive);
            Map(x => x.Address);
            Map(x => x.Phone);
            Map(x => x.ExternalDomain);
            References(i => i.District).Column("District_Id").Index("tenant_district_id");

            this.MapFullAudited();

            Polymorphism.Explicit();
        }
    }
}
