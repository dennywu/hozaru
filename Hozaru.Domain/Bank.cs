using Hozaru.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class Bank : AuditedEntity<Guid>
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string BankName { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual bool IsManualConfirmation { get; set; }

        protected Bank() { }

        public Bank(string code, string name, string bankName, string imageUrl, bool isManualConfirmation)
        {
            this.Code = code;
            this.Name = name;
            this.BankName = bankName;
            this.ImageUrl = imageUrl;
            this.IsManualConfirmation = isManualConfirmation;
        }
    }
}
