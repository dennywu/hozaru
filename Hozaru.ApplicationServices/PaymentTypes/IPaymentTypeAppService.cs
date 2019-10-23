using Hozaru.ApplicationServices.PaymentTypes.Dtos;
using Hozaru.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hozaru.ApplicationServices.PaymentTypes
{
    public interface IPaymentTypeAppService: IApplicationService
    {
        IList<PaymentTypeDto> GetAll();
        Stream GetImage(string code);
    }
}
