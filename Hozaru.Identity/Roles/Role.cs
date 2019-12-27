using Hozaru.Core.Identity.Authorization.Roles;
using Hozaru.Identity.MultiTenancy;
using Hozaru.Identity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Identity.Roles
{
    public class Role : HozaruRole<Tenant, User>
    {
        public Role()
        {

        }

        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {

        }
    }
}
