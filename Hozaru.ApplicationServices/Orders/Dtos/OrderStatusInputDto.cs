using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public enum OrderStatusInputDto
    {
        ALL,
        DRAFT,
        WAITINGFORPAYMENT,
        PAYMENTREJECTED,
        PACKAGING,
        SHIPPING,
        DONE
    }
}
