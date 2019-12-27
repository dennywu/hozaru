using Hozaru.Core.Domain.Entities.Auditing;
using Hozaru.Identity.MultiTenancy;
using System;

namespace Hozaru.Authentication
{
    public class ApiKey : AuditedEntity<Guid>
    {
        public virtual int TenantId { get; set; }
        public virtual string Key { get; set; }

        protected ApiKey() { }

        public ApiKey(int tenantId, string key)
        {
            TenantId = tenantId;
            Key = key ?? throw new ArgumentNullException(nameof(key));
        }
    }
}
