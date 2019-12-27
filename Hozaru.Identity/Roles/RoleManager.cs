using Hozaru.Core.Authorization;
using Hozaru.Core.Configurations;
using Hozaru.Core.Identity.Authorization.Roles;
using Hozaru.Core.Runtime.Caching;
using Hozaru.Identity.MultiTenancy;
using Hozaru.Identity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Identity.Roles
{
    public class RoleManager : HozaruRoleManager<Tenant, Role, User>
    {
        public RoleManager(
            RoleStore store,
            IPermissionManager permissionManager,
            IRoleManagementConfig roleManagementConfig,
            ICacheManager cacheManager)
            : base(
            store,
            permissionManager,
            roleManagementConfig,
            cacheManager)
        {
        }
    }
}
