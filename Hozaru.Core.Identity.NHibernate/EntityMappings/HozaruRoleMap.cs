using Hozaru.Core.Identity.Authorization.Roles;
using Hozaru.Core.Identity.Authorization.Users;
using Hozaru.Core.Identity.MultiTenancy;
using Hozaru.NHibernate.EntityMappings;

namespace Hozaru.Core.Identity.NHibernate.EntityMappings
{
    /// <summary>
    /// Base class for role mapping.
    /// </summary>
    public abstract class HozaruRoleMap<TTenant, TRole, TUser> : EntityMap<TRole, int>
        where TRole : HozaruRole<TTenant, TUser>
        where TUser : HozaruUser<TTenant, TUser>
        where TTenant : HozaruTenant<TTenant, TUser>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        protected HozaruRoleMap()
            : base("HozaruRoles")
        {
            //References(x => x.Tenant).Column("TenantId").Nullable();
            
            Map(x => x.Name);
            Map(x => x.DisplayName);
            Map(x => x.IsStatic);
            Map(x => x.IsDefault);
            
            this.MapFullAudited();

            Polymorphism.Explicit();
        }
    }
}