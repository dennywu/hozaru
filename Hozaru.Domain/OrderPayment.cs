using Hozaru.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class OrderPayment : Entity<Guid>
    {
        public virtual DateTime PaymentDate { get; set; }
        public virtual string ReceiptImageUrl { get; set; }
        public virtual string PaymentBankName { get; set; }
        public virtual string PaymentAccountName { get; set; }
        public virtual string PaymentAccountNumber { get; set; }

        protected OrderPayment()
        {
            this.PaymentDate = DateTime.Now;
        }

        public OrderPayment(string receiptImageUrl, string bankName, string accountName, string accountNumber)
            :this()
        {
            this.ReceiptImageUrl = receiptImageUrl;
            this.PaymentAccountName = accountName;
            this.PaymentAccountNumber = accountNumber;
            this.PaymentBankName = bankName;
        }
    }
}
