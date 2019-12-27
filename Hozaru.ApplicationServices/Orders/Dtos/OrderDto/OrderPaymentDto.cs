using Hozaru.ApplicationServices.PaymentMethods.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class OrderPaymentDto
    {
        public PaymentMethodFullDto PaymentMethod { get; set; }
        public virtual OrderPaymentHistoryDto LastPayment { get; set; }
        public DateTime? LastPaymentDate { get; set; }
    }
}
