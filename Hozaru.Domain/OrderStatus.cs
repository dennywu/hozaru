using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public enum OrderStatus : int
    {
        DRAFT = 10,
        WAITINGFORPAYMENT = 20,
        PAYMENTREJECTED = 30,
        PACKAGING = 40,
        SHIPPING = 50,
        DONE = 60,
        CANCELED = 70
    }
}
