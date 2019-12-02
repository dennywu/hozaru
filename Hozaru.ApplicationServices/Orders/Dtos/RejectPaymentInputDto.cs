using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class RejectPaymentInputDto
    {
        public Guid Id { get; set; }
        public string Reason { get; set; }
    }
}
