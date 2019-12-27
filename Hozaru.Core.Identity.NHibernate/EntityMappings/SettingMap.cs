using Hozaru.Core.Identity.Configuration;
using Hozaru.NHibernate.EntityMappings;

namespace Hozaru.Core.Identity.NHibernate.EntityMappings
{
    public class SettingMap : EntityMap<Setting, long>
    {
        public SettingMap()
            : base("HozaruSettings")
        {
            Map(x => x.TenantId);
            Map(x => x.UserId);
            Map(x => x.Name);
            Map(x => x.Value);

            this.MapAudited();
        }
    }
}