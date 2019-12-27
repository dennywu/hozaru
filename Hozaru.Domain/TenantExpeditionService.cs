using Hozaru.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class TenantExpeditionService : AuditedEntity<Guid>, IMustHaveTenant
    {
        public virtual ExpeditionService ExpeditionService { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual int TenantId { get; set; }

        protected TenantExpeditionService() { }

        public TenantExpeditionService(ExpeditionService expeditionService)
        {
            this.ExpeditionService = expeditionService;
            this.IsActive = true;
        }

        public virtual void Deactivate()
        {
            this.IsActive = false;
        }

        public virtual void Activate()
        {
            this.IsActive = true;
        }
    }
}
