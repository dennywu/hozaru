using Hozaru.Core.Domain.Repositories;
using Hozaru.Core.Identity.MultiTenancy;
using Hozaru.Identity.Editions;
using Hozaru.Identity.Roles;
using Hozaru.Identity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Identity.MultiTenancy
{
    public class TenantManager : HozaruTenantManager<Tenant, Role, User>
    {
        public TenantManager(
            IRepository<Tenant, int> tenantRepository,
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
            EditionManager editionManager)
            : base(
                tenantRepository,
                tenantFeatureRepository,
                editionManager
            )
        {
        }
    }
}
