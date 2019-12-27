using Hozaru.Core.Domain.Entities.Auditing;
using Hozaru.Identity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Authentication
{
    public class RefreshToken : AuditedEntity<Guid>
    {
        public virtual string Token { get; set; }
        public virtual DateTime Expires { get; set; }
        public virtual User User { get; set; }
        public virtual string RemoteIpAddress { get; set; }
        public virtual bool Active => DateTime.UtcNow < Expires;

        protected RefreshToken()
        {
        }

        public RefreshToken(string token, DateTime expires, User user, string remoteIpAddress)
        {
            this.Token = token;
            this.Expires = expires;
            this.User = user;
            this.RemoteIpAddress = remoteIpAddress;
        }
    }
}
