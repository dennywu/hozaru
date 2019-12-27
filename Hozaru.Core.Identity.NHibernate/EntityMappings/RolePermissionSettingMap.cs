using FluentNHibernate.Mapping;
using Hozaru.Core.Identity.Authorization.Roles;

namespace Hozaru.Core.Identity.NHibernate.EntityMappings
{
    public class RolePermissionSettingMap : SubclassMap<RolePermissionSetting>
    {
        public RolePermissionSettingMap()
        {
            DiscriminatorValue("RolePermissionSetting");

            Map(x => x.RoleId).Not.Nullable();
        }
    }
}