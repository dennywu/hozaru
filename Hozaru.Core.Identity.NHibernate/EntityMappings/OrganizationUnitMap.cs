using Hozaru.Core.Identity.Organizations;
using Hozaru.NHibernate.EntityMappings;

namespace Hozaru.Core.Identity.NHibernate.EntityMappings
{
    public class OrganizationUnitMap : EntityMap<OrganizationUnit, long>
    {
        public OrganizationUnitMap()
            : base("HozaruOrganizationUnits")
        {
            //Map(x => x.TenantId);
            References(x => x.Parent).Column("ParentId").Nullable();
            //Map(x => x.ParentId);
            Map(x => x.Code);
            Map(x => x.DisplayName);
            
            this.MapFullAudited();
        }
    }
}