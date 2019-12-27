using Hozaru.Core.Authorization;
using Hozaru.Core.Configurations;
using Hozaru.Core.Configurations.Startup;
using Hozaru.Core.Dependency;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Core.Domain.Uow;
using Hozaru.Core.Identity.Authorization.Users;
using Hozaru.Core.Identity.Organizations;
using Hozaru.Core.Runtime.Caching;
using Hozaru.Identity.MultiTenancy;
using Hozaru.Identity.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Identity.Users
{
    public class UserManager : HozaruUserManager<Tenant, Role, User>
    {
        public UserManager(
            UserStore userStore,
            RoleManager roleManager,
            IRepository<Tenant, int> tenantRepository,
            IMultiTenancyConfig multiTenancyConfig,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            IUserManagementConfig userManagementConfig,
            IIocResolver iocResolver,
            ICacheManager cacheManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IOrganizationUnitSettings organizationUnitSettings)
            : base(
                userStore,
                roleManager,
                tenantRepository,
                multiTenancyConfig,
                permissionManager,
                unitOfWorkManager,
                settingManager,
                userManagementConfig,
                iocResolver,
                cacheManager,
                organizationUnitRepository,
                userOrganizationUnitRepository,
                organizationUnitSettings)
        {
        }
    }
}
