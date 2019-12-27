using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Hozaru.Domain.Orders
{
    public class OrderPayment
    {
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual IList<OrderPaymentHistory> PaymentHistories { get; set; }
        public virtual DateTime? LastPaymentDate { get; set; }

        private OrderPayment()
        {
            PaymentHistories = new List<OrderPaymentHistory>();
        }

        public OrderPayment(PaymentMethod paymentMethod)
            :this()
        {
            PaymentMethod = paymentMethod;
        }

        public virtual void AddPayment(Order order, string bankName, string accountName, string accountNumber, string imageFileName)
        {
            var newPayment = new OrderPaymentHistory(order, imageFileName, bankName, accountName, accountNumber);
            this.PaymentHistories.Add(newPayment);
            this.LastPaymentDate = DateTime.Now;
        }

        public virtual OrderPaymentHistory GetLastPayment()
        {
            return this.PaymentHistories.OrderByDescending(i => i.PaymentDate).FirstOrDefault();
        }
    }
}
