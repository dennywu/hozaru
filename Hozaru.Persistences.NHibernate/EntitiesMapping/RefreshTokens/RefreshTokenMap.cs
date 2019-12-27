using Hozaru.Authentication;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.RefreshTokens
{
    public class RefreshTokenMap : EntityMap<RefreshToken, Guid>
    {
        public RefreshTokenMap()
            : base("RefreshTokens")
        {
            Map(i => i.Token).Length(255).Not.Nullable();
            Map(i => i.Expires).Not.Nullable();
            Map(i => i.RemoteIpAddress).Length(32).Nullable();
            References(i => i.User).Column("User_Id").Index("refreshtoken_user_id");
            this.MapAudited();
        }
    }
}
