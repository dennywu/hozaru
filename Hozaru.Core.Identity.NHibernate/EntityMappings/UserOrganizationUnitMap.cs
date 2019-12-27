using Hozaru.Core.Identity.Authorization.Users;
using Hozaru.NHibernate.EntityMappings;

namespace Hozaru.Core.Identity.NHibernate.EntityMappings
{
    public class UserOrganizationUnitMap : EntityMap<UserOrganizationUnit, long>
    {
        public UserOrganizationUnitMap()
            : base("UserOrganizationUnits")
        {
            //Map(x => x.TenantId);
            Map(x => x.UserId);
            Map(x => x.OrganizationUnitId);

            this.MapCreationAudited();
        }
    }
}