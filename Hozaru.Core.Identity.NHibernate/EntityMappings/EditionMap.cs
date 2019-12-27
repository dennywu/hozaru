using Hozaru.Core.Application.Editions;
using Hozaru.NHibernate.EntityMappings;

namespace Hozaru.Core.Identity.NHibernate.EntityMappings
{
    public class EditionMap : EntityMap<Edition, int>
    {
        public EditionMap()
            : base("HozaruEditions")
        {
            Map(x => x.Name);
            Map(x => x.DisplayName);
            
            this.MapFullAudited();
        }
    }
}