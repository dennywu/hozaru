﻿using Hozaru.Core.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.PaymentTypes.Dtos
{
    public class PaymentTypeDto : EntityDto<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsManualConfirmation { get; set; }
        public string BankName { get; set; }
    }
}