using Hozaru.Core.Domain.Entities;
using Hozaru.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class AutoNumber : AuditedEntity<Guid>, IMustHaveTenant
    {
        public virtual string Date { get; set; }
        public virtual int Number { get; set; }
        public virtual int TenantId { get; set; }

        protected AutoNumber() { }

        public AutoNumber(DateTime transactionDate)
        {
            Date = transactionDate.ToString("yyMMdd");
            Number = 0;
        }

        public virtual void Next()
        {
            Number++;
        }

        public virtual string GetOrderNumber()
        {
            return string.Format("{0}{1}{2}", Date, TenantId.ToString("000"), Number.ToString("0000"));
        }
    }
}
