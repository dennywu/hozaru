using Hozaru.Core.Identity.Authorization;
using Hozaru.NHibernate.EntityMappings;

namespace Hozaru.Core.Identity.NHibernate.EntityMappings
{
    public class PermissionSettingMap : EntityMap<PermissionSetting, long>
    {
        public PermissionSettingMap()
            : base("HozaruPermissions")
        {
            DiscriminateSubClassesOnColumn("Discriminator");

            Map(x => x.Name);
            Map(x => x.IsGranted);

            this.MapCreationAudited();
        }
    }
}