using Hozaru.AutoMapper;
using Hozaru.Core.Application.Services.Dto;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.PaymentTypes.Dtos
{
    [AutoMapFrom(typeof(PaymentType))]
    public class PaymentTypeFullDto : EntityDto<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsManualConfirmation { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
    }
}
