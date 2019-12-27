using Hozaru.Core.Domain.Repositories;
using Hozaru.Core.Domain.Uow;
using Hozaru.Core.Identity.Authorization.Users;
using Hozaru.Identity.MultiTenancy;
using Hozaru.Identity.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Identity.Users
{
    public class UserStore : HozaruUserStore<Tenant, Role, User>
    {
        public UserStore(
            IRepository<User, long> userRepository,
            IRepository<UserLogin, long> userLoginRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<Role, int> roleRepository,
            IRepository<UserPermissionSetting, long> userPermissionSettingRepository,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                userRepository,
                userLoginRepository,
                userRoleRepository,
                roleRepository,
                userPermissionSettingRepository,
                unitOfWorkManager)
        {

        }
    }
}
