using FluentNHibernate.Mapping;
using Hozaru.Core.Identity.MultiTenancy;

namespace Hozaru.Core.Identity.NHibernate.EntityMappings
{
    public class TenantFeatureSettingMap : SubclassMap<TenantFeatureSetting>
    {
        public TenantFeatureSettingMap()
        {
            DiscriminatorValue("TenantFeatureSetting");

            Map(x => x.TenantId);
        }
    }
}