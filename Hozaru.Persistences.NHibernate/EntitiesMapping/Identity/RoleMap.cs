using Hozaru.Core.Identity.NHibernate.EntityMappings;
using Hozaru.Identity.MultiTenancy;
using Hozaru.Identity.Roles;
using Hozaru.Identity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.Identity
{
    public class RoleMap : HozaruRoleMap<Tenant, Role, User>
    {
    }
}
