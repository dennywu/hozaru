using Hozaru.Core.Domain.Entities;
using Hozaru.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class PaymentMethod : AuditedEntity<Guid>, IMustHaveTenant
    {
        public virtual Bank Bank { get; set; }
        public virtual string BankBranch { get; set; }
        public virtual string AccountName { get; set; }
        public virtual string AccountNumber { get; set; }
        public virtual bool Disabled { get; set; }
        public virtual int TenantId { get; set; }

        protected PaymentMethod() { }

        public PaymentMethod(Bank bank, string bankBranch, string accountName, string accountNo)
        {
            this.Bank = bank;
            this.BankBranch = bankBranch;
            this.AccountName = accountName;
            this.AccountNumber = accountNo;
            this.Disabled = false;
        }

        public virtual void Update(string bankBranch, string accountName, string accountNo, bool disabled)
        {
            this.BankBranch = bankBranch;
            this.AccountName = accountName;
            this.AccountNumber = accountNo;
            this.Disabled = disabled;
        }
    }
}
