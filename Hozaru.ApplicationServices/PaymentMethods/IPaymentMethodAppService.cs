using Hozaru.ApplicationServices.PaymentMethods.Dtos;
using Hozaru.Core.Application.Services;
using Hozaru.Core.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hozaru.ApplicationServices.PaymentMethods
{
    public interface IPaymentMethodAppService: IApplicationService
    {
        IList<PaymentMethodDto> GetAll();
        PagedResultOutput<PaymentMethodFullDto> GetAll(GetListPaymentMethodInputDto inputDto);
        Stream GetImage(string code);
        PaymentMethodFullDto Create(CreatePaymentMethodInputDto inputDto);
        PaymentMethodFullDto Get(Guid id);
        PaymentMethodFullDto Edit(EditPaymentMethodInputDto inputDto);
    }
}
