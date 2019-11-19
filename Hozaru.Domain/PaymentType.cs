using Hozaru.Core.Domain.Entities;
using Hozaru.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class PaymentType : AuditedEntity<Guid>
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsManualConfirmation { get; set; }
        public virtual string BankName { get; set; }
        public virtual string BankBranch { get; set; }
        public virtual string AccountName { get; set; }
        public virtual string AccountNumber { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual bool Disabled { get; set; }

        protected PaymentType() { }

        public PaymentType(string code, string name, string bankName, string bankBranch, string accountName, string accountNo, bool isManualConfirmation, string imageUrl)
        {
            this.Code = code;
            this.Name = name;
            this.BankName = bankName;
            this.BankBranch = bankBranch;
            this.AccountName = accountName;
            this.AccountNumber = accountNo;
            this.ImageUrl = imageUrl;
            this.IsManualConfirmation = isManualConfirmation;
            this.Disabled = false;
        }
    }
}
