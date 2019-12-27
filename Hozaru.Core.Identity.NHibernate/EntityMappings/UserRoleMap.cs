using Hozaru.Core.Identity.Authorization.Users;
using Hozaru.NHibernate.EntityMappings;

namespace Hozaru.Core.Identity.NHibernate.EntityMappings
{
    public class UserRoleMap : EntityMap<UserRole, long>
    {
        public UserRoleMap()
            : base("UserRoles")
        {
            Map(x => x.UserId);
            Map(x => x.RoleId);
            Map(x => x.RoleName);
            
            this.MapCreationAudited();
        }
    }
}
