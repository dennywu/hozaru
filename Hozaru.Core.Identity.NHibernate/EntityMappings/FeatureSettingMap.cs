using Hozaru.Core.Identity.Application.Features;
using Hozaru.NHibernate.EntityMappings;

namespace Hozaru.Core.Identity.NHibernate.EntityMappings
{
    public class FeatureSettingMap : EntityMap<FeatureSetting, long>
    {
        public FeatureSettingMap()
            : base("HozaruFeatures")
        {
            DiscriminateSubClassesOnColumn("Discriminator");

            Map(x => x.Name);
            Map(x => x.Value);
            
            this.MapCreationAudited();
        }
    }
}