using Hozaru.Core.Identity.Authorization.Users;
using Hozaru.Core.Identity.MultiTenancy;
using Hozaru.NHibernate.EntityMappings;

namespace Hozaru.Core.Identity.NHibernate.EntityMappings
{
    /// <summary>
    /// Base class to map classes derived from <see cref="HozaruTenant{TTenant,TUser}"/>
    /// </summary>
    /// <typeparam name="TTenant">Tenant type</typeparam>
    /// <typeparam name="TUser">User type</typeparam>
    public abstract class HozaruTenantMap<TTenant, TUser> : EntityMap<TTenant, int>
        where TTenant : HozaruTenant<TTenant, TUser>
        where TUser : HozaruUser<TTenant, TUser>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        protected HozaruTenantMap()
            : base("Tenants")
        {
            References(x => x.Edition).Column("EditionId").Nullable();

            Map(x => x.TenancyName);
            Map(x => x.Name);
            Map(x => x.IsActive);

            this.MapFullAudited();

            Polymorphism.Explicit();
        }
    }
}