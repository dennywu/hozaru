using Hozaru.Core.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.PaymentMethods.Dtos
{
    public class GetListPaymentMethodInputDto : IInputDto, IPagedResultRequest
    {
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; }
    }
}
