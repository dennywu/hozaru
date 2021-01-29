﻿using Hozaru.Core.Authorization;
using Hozaru.Core.Configurations;
using Hozaru.Core.Configurations.Startup;
using Hozaru.Core.Dependency;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Core.Domain.Services;
using Hozaru.Core.Domain.Uow;
using Hozaru.Core.Identity.Application.Features;
using Hozaru.Core.Identity.Authorization.Roles;
using Hozaru.Core.Identity.MultiTenancy;
using Hozaru.Core.Identity.Organizations;
using Hozaru.Core.MultiTenancy;
using Hozaru.Core.Runtime.Caching;
using Hozaru.Core.Runtime.Session;
using Microsoft.AspNet.Identity;
using System;
using Hozaru.Core.Identity.Runtime.Caching;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Core.Timing;
using System.Security.Claims;
using Hozaru.Core.Runtime.Security;
using System.Globalization;
using Hozaru.Core.Identity.IdentityFramework;
using Hozaru.Core.Identity.Configuration;

namespace Hozaru.Core.Identity.Authorization.Users
{
    /// <summary>
    /// Extends <see cref="UserManager{TUser,TKey}"/> of ASP.NET Identity Framework.
    /// </summary>
    public abstract class HozaruUserManager<TTenant, TRole, TUser>
        : UserManager<TUser, long>,
        IDomainService
        where TTenant : HozaruTenant<TTenant, TUser>
        where TRole : HozaruRole<TTenant, TUser>, new()
        where TUser : HozaruUser<TTenant, TUser>
    {
        private IUserPermissionStore<TTenant, TUser> UserPermissionStore
        {
            get
            {
                if (!(Store is IUserPermissionStore<TTenant, TUser>))
                {
                    throw new HozaruException("Store is not IUserPermissionStore");
                }

                return Store as IUserPermissionStore<TTenant, TUser>;
            }
        }

        //public ILocalizationManager LocalizationManager { get; set; }

        public IHozaruSession HozaruSession { get; set; }

        public FeatureDependencyContext FeatureDependencyContext { get; set; }

        protected HozaruRoleManager<TTenant, TRole, TUser> RoleManager { get; private set; }

        protected ISettingManager SettingManager { get; private set; }

        protected HozaruUserStore<TTenant, TRole, TUser> HozaruStore { get; private set; }

        private readonly IPermissionManager _permissionManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IUserManagementConfig _userManagementConfig;
        private readonly IIocResolver _iocResolver;
        private readonly IRepository<TTenant, int> _tenantRepository;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        private readonly IOrganizationUnitSettings _organizationUnitSettings;

        protected HozaruUserManager(
            HozaruUserStore<TTenant, TRole, TUser> userStore,
            HozaruRoleManager<TTenant, TRole, TUser> roleManager,
            IRepository<TTenant, int> tenantRepository,
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
            : base(userStore)
        {
            HozaruStore = userStore;
            RoleManager = roleManager;
            SettingManager = settingManager;
            _tenantRepository = tenantRepository;
            _multiTenancyConfig = multiTenancyConfig;
            _permissionManager = permissionManager;
            _unitOfWorkManager = unitOfWorkManager;
            _userManagementConfig = userManagementConfig;
            _iocResolver = iocResolver;
            _cacheManager = cacheManager;
            _organizationUnitRepository = organizationUnitRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _organizationUnitSettings = organizationUnitSettings;

            //LocalizationManager = NullLocalizationManager.Instance;
        }

        public override async Task<IdentityResult> CreateAsync(TUser user)
        {
            var result = await CheckDuplicateUsernameOrEmailAddressAsync(user.Id, user.UserName, user.EmailAddress);
            if (!result.Succeeded)
            {
                return result;
            }

            if (HozaruSession.TenantId.HasValue)
            {
                user.TenantId = HozaruSession.TenantId.Value;
            }

            return await base.CreateAsync(user);
        }

        /// <summary>
        /// Check whether a user is granted for a permission.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="permissionName">Permission name</param>
        public virtual async Task<bool> IsGrantedAsync(long userId, string permissionName)
        {
            return await IsGrantedAsync(
                userId,
                _permissionManager.GetPermission(permissionName)
                );
        }

        /// <summary>
        /// Check whether a user is granted for a permission.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="permission">Permission</param>
        public virtual Task<bool> IsGrantedAsync(TUser user, Permission permission)
        {
            return IsGrantedAsync(user.Id, permission);
        }

        /// <summary>
        /// Check whether a user is granted for a permission.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="permission">Permission</param>
        public virtual async Task<bool> IsGrantedAsync(long userId, Permission permission)
        {
            //Check for multi-tenancy side
            if (!permission.MultiTenancySides.HasFlag(HozaruSession.MultiTenancySide))
            {
                return false;
            }

            //Check for depended features
            if (permission.FeatureDependency != null && HozaruSession.MultiTenancySide == MultiTenancySides.Tenant)
            {
                if (!await permission.FeatureDependency.IsSatisfiedAsync(FeatureDependencyContext))
                {
                    return false;
                }
            }

            //Get cached user permissions
            var cacheItem = await GetUserPermissionCacheItemAsync(userId);

            //Check for user-specific value
            if (cacheItem.GrantedPermissions.Contains(permission.Name))
            {
                return true;
            }

            if (cacheItem.ProhibitedPermissions.Contains(permission.Name))
            {
                return false;
            }

            //Check for roles
            foreach (var roleId in cacheItem.RoleIds)
            {
                if (await RoleManager.IsGrantedAsync(roleId, permission))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets granted permissions for a user.
        /// </summary>
        /// <param name="user">Role</param>
        /// <returns>List of granted permissions</returns>
        public virtual async Task<IReadOnlyList<Permission>> GetGrantedPermissionsAsync(TUser user)
        {
            var permissionList = new List<Permission>();

            foreach (var permission in _permissionManager.GetAllPermissions())
            {
                if (await IsGrantedAsync(user.Id, permission))
                {
                    permissionList.Add(permission);
                }
            }

            return permissionList;
        }

        /// <summary>
        /// Sets all granted permissions of a user at once.
        /// Prohibits all other permissions.
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="permissions">Permissions</param>
        public virtual async Task SetGrantedPermissionsAsync(TUser user, IEnumerable<Permission> permissions)
        {
            var oldPermissions = await GetGrantedPermissionsAsync(user);
            var newPermissions = permissions.ToArray();

            foreach (var permission in oldPermissions.Where(p => !newPermissions.Contains(p)))
            {
                await ProhibitPermissionAsync(user, permission);
            }

            foreach (var permission in newPermissions.Where(p => !oldPermissions.Contains(p)))
            {
                await GrantPermissionAsync(user, permission);
            }
        }

        /// <summary>
        /// Prohibits all permissions for a user.
        /// </summary>
        /// <param name="user">User</param>
        public async Task ProhibitAllPermissionsAsync(TUser user)
        {
            foreach (var permission in _permissionManager.GetAllPermissions())
            {
                await ProhibitPermissionAsync(user, permission);
            }
        }

        /// <summary>
        /// Resets all permission settings for a user.
        /// It removes all permission settings for the user.
        /// User will have permissions according to his roles.
        /// This method does not prohibit all permissions.
        /// For that, use <see cref="ProhibitAllPermissionsAsync"/>.
        /// </summary>
        /// <param name="user">User</param>
        public async Task ResetAllPermissionsAsync(TUser user)
        {
            await UserPermissionStore.RemoveAllPermissionSettingsAsync(user);
        }

        /// <summary>
        /// Grants a permission for a user if not already granted.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="permission">Permission</param>
        public virtual async Task GrantPermissionAsync(TUser user, Permission permission)
        {
            await UserPermissionStore.RemovePermissionAsync(user, new PermissionGrantInfo(permission.Name, false));

            if (await IsGrantedAsync(user.Id, permission))
            {
                return;
            }

            await UserPermissionStore.AddPermissionAsync(user, new PermissionGrantInfo(permission.Name, true));
        }

        /// <summary>
        /// Prohibits a permission for a user if it's granted.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="permission">Permission</param>
        public virtual async Task ProhibitPermissionAsync(TUser user, Permission permission)
        {
            await UserPermissionStore.RemovePermissionAsync(user, new PermissionGrantInfo(permission.Name, true));

            if (!await IsGrantedAsync(user.Id, permission))
            {
                return;
            }

            await UserPermissionStore.AddPermissionAsync(user, new PermissionGrantInfo(permission.Name, false));
        }

        public virtual async Task<TUser> FindByNameOrEmailAsync(string userNameOrEmailAddress)
        {
            return await HozaruStore.FindByNameOrEmailAsync(userNameOrEmailAddress);
        }

        public virtual Task<List<TUser>> FindAllAsync(UserLoginInfo login)
        {
            return HozaruStore.FindAllAsync(login);
        }

        [UnitOfWork]
        public virtual async Task<HozaruLoginResult> LoginAsync(UserLoginInfo login, string tenancyName = null)
        {
            if (login == null || login.LoginProvider.IsNullOrEmpty() || login.ProviderKey.IsNullOrEmpty())
            {
                throw new ArgumentException("login");
            }

            //Get and check tenant
            TTenant tenant = null;
            if (!_multiTenancyConfig.IsEnabled)
            {
                tenant = await GetDefaultTenantAsync();
            }
            else if (!string.IsNullOrWhiteSpace(tenancyName))
            {
                tenant = await _tenantRepository.FirstOrDefaultAsync(t => t.TenancyName == tenancyName);
                if (tenant == null)
                {
                    return new HozaruLoginResult(HozaruLoginResultType.InvalidTenancyName);
                }

                if (!tenant.IsActive)
                {
                    return new HozaruLoginResult(HozaruLoginResultType.TenantIsNotActive);
                }
            }

            using (_unitOfWorkManager.Current.DisableFilter(HozaruDataFilters.MayHaveTenant))
            {
                var user = await HozaruStore.FindAsync(tenant == null ? (int?)null : tenant.Id, login);
                if (user == null)
                {
                    return new HozaruLoginResult(HozaruLoginResultType.UnknownExternalLogin);
                }

                return await CreateLoginResultAsync(user);
            }
        }

        [UnitOfWork]
        public virtual async Task<HozaruLoginResult> LoginAsync(string userNameOrEmailAddress, string plainPassword, string tenancyName = null)
        {
            if (userNameOrEmailAddress.IsNullOrEmpty())
            {
                throw new ArgumentNullException("userNameOrEmailAddress");
            }

            if (plainPassword.IsNullOrEmpty())
            {
                throw new ArgumentNullException("plainPassword");
            }

            //Get and check tenant
            TTenant tenant = null;
            if (!_multiTenancyConfig.IsEnabled)
            {
                tenant = await GetDefaultTenantAsync();
            }
            else if (!string.IsNullOrWhiteSpace(tenancyName))
            {
                tenant = await _tenantRepository.FirstOrDefaultAsync(t => t.TenancyName.ToLower() == tenancyName.ToLower());
                if (tenant == null)
                {
                    return new HozaruLoginResult(HozaruLoginResultType.InvalidTenancyName);
                }

                if (!tenant.IsActive)
                {
                    return new HozaruLoginResult(HozaruLoginResultType.TenantIsNotActive);
                }
            }

            using (_unitOfWorkManager.Current.DisableFilter(HozaruDataFilters.MayHaveTenant))
            {
                var loggedInFromExternalSource = await TryLoginFromExternalAuthenticationSources(userNameOrEmailAddress, plainPassword, tenant);

                var user = await HozaruStore.FindByNameOrEmailAsync(tenant == null ? (int?)null : tenant.Id, userNameOrEmailAddress);
                if (user == null)
                {
                    return new HozaruLoginResult(HozaruLoginResultType.InvalidUserNameOrEmailAddress);
                }

                if (!loggedInFromExternalSource)
                {
                    var verificationResult = new PasswordHasher().VerifyHashedPassword(user.Password, plainPassword);
                    if (verificationResult != PasswordVerificationResult.Success)
                    {
                        return new HozaruLoginResult(HozaruLoginResultType.InvalidPassword);
                    }
                }

                return await CreateLoginResultAsync(user);
            }
        }

        private async Task<HozaruLoginResult> CreateLoginResultAsync(TUser user)
        {
            if (!user.IsActive)
            {
                return new HozaruLoginResult(HozaruLoginResultType.UserIsNotActive);
            }

            //if (await IsEmailConfirmationRequiredForLoginAsync(user.TenantId) && !user.IsEmailConfirmed)
            //{
            //    return new HozaruLoginResult(HozaruLoginResultType.UserEmailIsNotConfirmed);
            //}

            user.LastLoginTime = Clock.Now;

            await Store.UpdateAsync(user);

            await _unitOfWorkManager.Current.SaveChangesAsync();

            return new HozaruLoginResult(user, await CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie));
        }

        private async Task<bool> TryLoginFromExternalAuthenticationSources(string userNameOrEmailAddress, string plainPassword, TTenant tenant)
        {
            if (!_userManagementConfig.ExternalAuthenticationSources.Any())
            {
                return false;
            }

            foreach (var sourceType in _userManagementConfig.ExternalAuthenticationSources)
            {
                using (var source = _iocResolver.ResolveAsDisposable<IExternalAuthenticationSource<TTenant, TUser>>(sourceType))
                {
                    if (await source.Object.TryAuthenticateAsync(userNameOrEmailAddress, plainPassword, tenant))
                    {
                        var tenantId = tenant == null ? (int?)null : tenant.Id;

                        var user = await HozaruStore.FindByNameOrEmailAsync(tenantId, userNameOrEmailAddress);
                        if (user == null)
                        {
                            user = await source.Object.CreateUserAsync(userNameOrEmailAddress, tenant);

                            user.Tenant = tenant;
                            user.AuthenticationSource = source.Object.Name;
                            user.Password = new PasswordHasher().HashPassword(Guid.NewGuid().ToString("N").Left(16)); //Setting a random password since it will not be used

                            var role = RoleManager.Roles.FirstOrDefault(r => r.TenantId == tenantId && r.IsDefault);
                            user.Role = new UserRole { RoleId = role.Id, RoleName = role.Name };

                            //user.Roles = new List<UserRole>();
                            //foreach (var defaultRole in RoleManager.Roles.Where(r => r.TenantId == tenantId && r.IsDefault).ToList())
                            //{
                            //    user.Roles.Add(new UserRole { RoleId = defaultRole.Id });
                            //}

                            await Store.CreateAsync(user);
                        }
                        else
                        {
                            await source.Object.UpdateUserAsync(user, tenant);

                            user.AuthenticationSource = source.Object.Name;

                            await Store.UpdateAsync(user);
                        }

                        await _unitOfWorkManager.Current.SaveChangesAsync();

                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Gets a user by given id.
        /// Throws exception if no user found with given id.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User</returns>
        /// <exception cref="HozaruException">Throws exception if no user found with given id</exception>
        public virtual async Task<TUser> GetUserByIdAsync(long userId)
        {
            var user = await FindByIdAsync(userId);
            if (user == null)
            {
                throw new HozaruException("There is no user with id: " + userId);
            }

            return user;
        }

        public async override Task<ClaimsIdentity> CreateIdentityAsync(TUser user, string authenticationType)
        {
            var identity = await base.CreateIdentityAsync(user, authenticationType);
            if (user.TenantId.HasValue)
            {
                identity.AddClaim(new Claim(HozaruClaimTypes.TenantId, user.TenantId.Value.ToString(CultureInfo.InvariantCulture)));
            }

            return identity;
        }

        public async override Task<IdentityResult> UpdateAsync(TUser user)
        {
            var result = await CheckDuplicateUsernameOrEmailAddressAsync(user.Id, user.UserName, user.EmailAddress);
            if (!result.Succeeded)
            {
                return result;
            }

            var oldUserName = (await GetUserByIdAsync(user.Id)).UserName;
            if (oldUserName == HozaruUser<TTenant, TUser>.AdminUserName && user.UserName != HozaruUser<TTenant, TUser>.AdminUserName)
            {
                return HozaruIdentityResult.Failed(string.Format("CanNotRenameAdminUser"), HozaruUser<TTenant, TUser>.AdminUserName);
            }

            return await base.UpdateAsync(user);
        }

        public async override Task<IdentityResult> DeleteAsync(TUser user)
        {
            if (user.UserName == HozaruUser<TTenant, TUser>.AdminUserName)
            {
                //return HozaruIdentityResult.Failed(string.Format(L("CanNotDeleteAdminUser"), HozaruUser<TTenant, TUser>.AdminUserName));
                return HozaruIdentityResult.Failed(string.Format("CanNotDeleteAdminUser"), HozaruUser<TTenant, TUser>.AdminUserName);
            }

            return await base.DeleteAsync(user);
        }

        public virtual async Task<IdentityResult> ChangePasswordAsync(TUser user, string newPassword)
        {
            var result = await PasswordValidator.ValidateAsync(newPassword);
            if (!result.Succeeded)
            {
                return result;
            }

            await HozaruStore.SetPasswordHashAsync(user, PasswordHasher.HashPassword(newPassword));
            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> CheckDuplicateUsernameOrEmailAddressAsync(long? expectedUserId, string userName, string emailAddress)
        {
            var user = (await FindByNameAsync(userName));
            if (user != null && user.Id != expectedUserId)
            {
                return HozaruIdentityResult.Failed(string.Format("Identity.DuplicateName"), userName);
            }

            user = (await FindByEmailAsync(emailAddress));
            if (user != null && user.Id != expectedUserId)
            {
                return HozaruIdentityResult.Failed(string.Format("Identity.DuplicateEmail"), emailAddress);
            }

            return IdentityResult.Success;
        }

        //public virtual async Task<IdentityResult> SetRoles(TUser user, string[] roleNames)
        //{
        //    //Remove from removed roles
        //    foreach (var userRole in user.Roles.ToList())
        //    {
        //        var role = await RoleManager.FindByIdAsync(userRole.RoleId);
        //        if (roleNames.All(roleName => role.Name != roleName))
        //        {
        //            var result = await RemoveFromRoleAsync(user.Id, role.Name);
        //            if (!result.Succeeded)
        //            {
        //                return result;
        //            }
        //        }
        //    }

        //    //Add to added roles
        //    foreach (var roleName in roleNames)
        //    {
        //        var role = await RoleManager.GetRoleByNameAsync(roleName);
        //        if (user.Roles.All(ur => ur.RoleId != role.Id))
        //        {
        //            var result = await AddToRoleAsync(user.Id, roleName);
        //            if (!result.Succeeded)
        //            {
        //                return result;
        //            }
        //        }
        //    }

        //    return IdentityResult.Success;
        //}

        public virtual async Task<IdentityResult> SetRoles(TUser user, string roleName)
        {
            //Remove from removed roles

            //var role = await RoleManager.FindByIdAsync(user.Role.RoleId);
            //if (roleNames.All(roleName => role.Name != roleName))
            //{
            //    var result = await RemoveFromRoleAsync(user.Id, role.Name);
            //    if (!result.Succeeded)
            //    {
            //        return result;
            //    }
            //}

            //Add to added roles
            //foreach (var roleName in roleNames)
            //{
            var role = await RoleManager.GetRoleByNameAsync(roleName);
            //if (user.Roles.All(ur => ur.RoleId != role.Id))
            //{
            var result = await AddToRoleAsync(user.Id, roleName);
            if (!result.Succeeded)
            {
                return result;
            }
            //}
            //}

            return IdentityResult.Success;
        }

        public virtual async Task<bool> IsInOrganizationUnitAsync(long userId, long ouId)
        {
            return await IsInOrganizationUnitAsync(
                await GetUserByIdAsync(userId),
                await _organizationUnitRepository.GetAsync(ouId)
                );
        }

        public virtual async Task<bool> IsInOrganizationUnitAsync(TUser user, OrganizationUnit ou)
        {
            return await _userOrganizationUnitRepository.CountAsync(uou =>
                uou.UserId == user.Id && uou.OrganizationUnitId == ou.Id
                ) > 0;
        }

        public virtual async Task AddToOrganizationUnitAsync(long userId, long ouId)
        {
            await AddToOrganizationUnitAsync(
                await GetUserByIdAsync(userId),
                await _organizationUnitRepository.GetAsync(ouId)
                );
        }

        public virtual async Task AddToOrganizationUnitAsync(TUser user, OrganizationUnit ou)
        {
            var currentOus = await GetOrganizationUnitsAsync(user);

            if (currentOus.Any(cou => cou.Id == ou.Id))
            {
                return;
            }

            await CheckMaxUserOrganizationUnitMembershipCountAsync(user.TenantId, currentOus.Count + 1);

            await _userOrganizationUnitRepository.InsertAsync(new UserOrganizationUnit(user.TenantId, user.Id, ou.Id));
        }

        public virtual async Task RemoveFromOrganizationUnitAsync(long userId, long ouId)
        {
            await RemoveFromOrganizationUnitAsync(
                await GetUserByIdAsync(userId),
                await _organizationUnitRepository.GetAsync(ouId)
                );
        }

        public virtual async Task RemoveFromOrganizationUnitAsync(TUser user, OrganizationUnit ou)
        {
            await _userOrganizationUnitRepository.DeleteAsync(uou => uou.UserId == user.Id && uou.OrganizationUnitId == ou.Id);
        }

        public virtual async Task SetOrganizationUnitsAsync(long userId, params long[] organizationUnitIds)
        {
            await SetOrganizationUnitsAsync(
                await GetUserByIdAsync(userId),
                organizationUnitIds
                );
        }

        private async Task CheckMaxUserOrganizationUnitMembershipCountAsync(int? tenantId, int requestedCount)
        {
            var maxCount = await _organizationUnitSettings.GetMaxUserMembershipCountAsync(tenantId);
            if (requestedCount > maxCount)
            {
                throw new HozaruException(string.Format("Can not set more than {0} organization unit for a user!", maxCount));
            }
        }

        public virtual async Task SetOrganizationUnitsAsync(TUser user, params long[] organizationUnitIds)
        {
            if (organizationUnitIds == null)
            {
                organizationUnitIds = new long[0];
            }

            await CheckMaxUserOrganizationUnitMembershipCountAsync(user.TenantId, organizationUnitIds.Length);

            var currentOus = await GetOrganizationUnitsAsync(user);

            //Remove from removed OUs
            foreach (var currentOu in currentOus)
            {
                if (!organizationUnitIds.Contains(currentOu.Id))
                {
                    await RemoveFromOrganizationUnitAsync(user, currentOu);
                }
            }

            //Add to added OUs
            foreach (var organizationUnitId in organizationUnitIds)
            {
                if (currentOus.All(ou => ou.Id != organizationUnitId))
                {
                    await AddToOrganizationUnitAsync(
                        user,
                        await _organizationUnitRepository.GetAsync(organizationUnitId)
                        );
                }
            }
        }

        [UnitOfWork]
        public virtual Task<List<OrganizationUnit>> GetOrganizationUnitsAsync(TUser user)
        {
            var query = from uou in _userOrganizationUnitRepository.GetAll()
                        join ou in _organizationUnitRepository.GetAll() on uou.OrganizationUnitId equals ou.Id
                        where uou.UserId == user.Id
                        select ou;

            return Task.FromResult(query.ToList());
        }

        [UnitOfWork]
        public virtual Task<List<TUser>> GetUsersInOrganizationUnit(OrganizationUnit organizationUnit, bool includeChildren = false)
        {
            if (!includeChildren)
            {
                var query = from uou in _userOrganizationUnitRepository.GetAll()
                            join user in HozaruStore.Users on uou.UserId equals user.Id
                            where uou.OrganizationUnitId == organizationUnit.Id
                            select user;

                return Task.FromResult(query.ToList());
            }
            else
            {
                var query = from uou in _userOrganizationUnitRepository.GetAll()
                            join user in HozaruStore.Users on uou.UserId equals user.Id
                            join ou in _organizationUnitRepository.GetAll() on uou.OrganizationUnitId equals ou.Id
                            where ou.Code.StartsWith(organizationUnit.Code)
                            select user;

                return Task.FromResult(query.ToList());
            }
        }

        private async Task<bool> IsEmailConfirmationRequiredForLoginAsync(int? tenantId)
        {
            if (tenantId.HasValue)
            {
                return await SettingManager.GetSettingValueForTenantAsync<bool>(HozaruIdentitySettingNames.UserManagement.IsEmailConfirmationRequiredForLogin, tenantId.Value);
            }

            return await SettingManager.GetSettingValueForApplicationAsync<bool>(HozaruIdentitySettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);
        }

        private async Task<TTenant> GetDefaultTenantAsync()
        {
            var tenant = await _tenantRepository.FirstOrDefaultAsync(t => t.TenancyName == HozaruTenant<TTenant, TUser>.DefaultTenantName);
            if (tenant == null)
            {
                throw new HozaruException("There should be a 'Default' tenant if multi-tenancy is disabled!");
            }

            return tenant;
        }

        private async Task<UserPermissionCacheItem> GetUserPermissionCacheItemAsync(long userId)
        {
            return await _cacheManager.GetUserPermissionCache().GetAsync(userId, async () =>
            {
                var newCacheItem = new UserPermissionCacheItem(userId);

                foreach (var roleName in await GetRolesAsync(userId))
                {
                    newCacheItem.RoleIds.Add((await RoleManager.GetRoleByNameAsync(roleName)).Id);
                }

                foreach (var permissionInfo in await UserPermissionStore.GetPermissionsAsync(userId))
                {
                    if (permissionInfo.IsGranted)
                    {
                        newCacheItem.GrantedPermissions.Add(permissionInfo.Name);
                    }
                    else
                    {
                        newCacheItem.ProhibitedPermissions.Add(permissionInfo.Name);
                    }
                }

                return newCacheItem;
            });
        }

        //private string L(string name)
        //{
        //    return LocalizationManager.GetString(HozaruIdentityConsts.LocalizationSourceName, name);
        //}

        public class HozaruLoginResult
        {
            public HozaruLoginResultType Result { get; private set; }

            public TUser User { get; private set; }

            public ClaimsIdentity Identity { get; private set; }

            public HozaruLoginResult(HozaruLoginResultType result)
            {
                Result = result;
            }

            public HozaruLoginResult(TUser user, ClaimsIdentity identity)
                : this(HozaruLoginResultType.Success)
            {
                User = user;
                Identity = identity;
            }
        }
    }
}