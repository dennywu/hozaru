using FluentNHibernate.Mapping;
using Hozaru.Core.Identity.Application.Features;

namespace Hozaru.Core.Identity.NHibernate.EntityMappings
{
    public class EditionFeatureSettingMap : SubclassMap<EditionFeatureSetting>
    {
        public EditionFeatureSettingMap()
        {
            DiscriminatorValue("EditionFeatureSetting");

            References(x => x.Edition).Column("EditionId").Not.Nullable(); //TODO: Need to Map EditionId column?
        }
    }
}