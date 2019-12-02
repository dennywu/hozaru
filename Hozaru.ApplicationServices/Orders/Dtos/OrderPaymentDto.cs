using Hozaru.AutoMapper;
using Hozaru.Core.Application.Services.Dto;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class OrderPaymentDto : EntityDto<Guid>
    {
        public DateTime PaymentDate { get; set; }
        public string PaymentBankName { get; set; }
        public string PaymentAccountName { get; set; }
        public string PaymentAccountNumber { get; set; }
        public string Url { get; set; }
    }
}
