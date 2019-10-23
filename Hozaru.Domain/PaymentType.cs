using Hozaru.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class PaymentType : Entity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsManualConfirmation { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string ImageUrl { get; set; }

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
        }
    }
}
