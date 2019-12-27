using FluentNHibernate.Mapping;
using Hozaru.Core.Identity.Authorization.Users;

namespace Hozaru.Core.Identity.NHibernate.EntityMappings
{
    public class UserPermissionSettingMap : SubclassMap<UserPermissionSetting>
    {
        public UserPermissionSettingMap()
        {
            DiscriminatorValue("UserPermissionSetting");

            Map(x => x.UserId).Not.Nullable();
        }
    }
}