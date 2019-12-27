using Hozaru.Core.Domain.Repositories;
using Hozaru.Core.Identity.Authorization.Roles;
using Hozaru.Core.Identity.Authorization.Users;
using Hozaru.Identity.MultiTenancy;
using Hozaru.Identity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Identity.Roles
{
    public class RoleStore : HozaruRoleStore<Tenant, Role, User>
    {
        public RoleStore(
            IRepository<Role, int> roleRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<RolePermissionSetting, long> rolePermissionSettingRepository)
            : base(
                roleRepository,
                userRoleRepository,
                rolePermissionSettingRepository)
        {
        }
    }
}
