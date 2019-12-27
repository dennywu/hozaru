using Hozaru.Core.Domain.Entities;
using Hozaru.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class OrderPaymentHistory : AuditedEntity<Guid>
    {
        public virtual DateTime PaymentDate { get; set; }
        public virtual string ReceiptImageUrl { get; set; }
        public virtual string PaymentBankName { get; set; }
        public virtual string PaymentAccountName { get; set; }
        public virtual string PaymentAccountNumber { get; set; }
        public virtual Order Order { get; set; }

        protected OrderPaymentHistory()
        {
            this.PaymentDate = DateTime.Now;
        }

        protected OrderPaymentHistory(Order order)
            : this()
        {
            this.Order = order;
        }

        public OrderPaymentHistory(Order order, string receiptImageUrl, string bankName, string accountName, string accountNumber)
            : this(order)
        {
            this.ReceiptImageUrl = receiptImageUrl;
            this.PaymentAccountName = accountName;
            this.PaymentAccountNumber = accountNumber;
            this.PaymentBankName = bankName;
        }
    }
}
