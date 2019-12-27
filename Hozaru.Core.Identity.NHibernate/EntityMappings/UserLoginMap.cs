using Hozaru.Core.Identity.Authorization.Users;
using Hozaru.NHibernate.EntityMappings;

namespace Hozaru.Core.Identity.NHibernate.EntityMappings
{
    public class UserLoginMap : EntityMap<UserLogin, long>
    {
        public UserLoginMap()
            : base("UserLogins")
        {
            Map(x => x.UserId);
            Map(x => x.LoginProvider);
            Map(x => x.ProviderKey);
        }
    }
}