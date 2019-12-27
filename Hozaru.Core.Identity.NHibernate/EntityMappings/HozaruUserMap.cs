using Hozaru.Core.Identity.Authorization.Users;
using Hozaru.Core.Identity.MultiTenancy;
using Hozaru.NHibernate.EntityMappings;

namespace Hozaru.Core.Identity.NHibernate.EntityMappings
{
    public abstract class HozaruUserMap<TTenant, TUser> : EntityMap<TUser, long>
        where TUser : HozaruUser<TTenant, TUser>
        where TTenant : HozaruTenant<TTenant, TUser>
    {
        protected HozaruUserMap()
            : base("Users")
        {
            //Map(x => x.TenantId);
            Map(x => x.UserName);
            Map(x => x.Name);
            Map(x => x.Surname);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.EmailAddress);
            Map(x => x.IsEmailConfirmed);
            Map(x => x.EmailConfirmationCode);
            Map(x => x.Password);
            Map(x => x.PasswordResetCode);
            Map(x => x.LastLoginTime);
            Map(x => x.IsActive);
            Map(x => x.AuthenticationSource);

            //HasMany(i => i.Roles)
            //    .Cascade.AllDeleteOrphan()
            //    .Inverse()
            //    .KeyColumn("UserId")
            //    .ForeignKeyConstraintName("fk_roles_user");

            HasOne<UserRole>(i => i.Role).Cascade.All();

            this.MapFullAudited();

            Polymorphism.Explicit();
        }
    }
}
