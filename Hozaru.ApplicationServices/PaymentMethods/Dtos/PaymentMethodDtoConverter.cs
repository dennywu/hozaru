using AutoMapper;
using Hozaru.Core.Configurations;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.PaymentMethods.Dtos
{
    public class PaymentMethodDtoConverter : ITypeConverter<PaymentMethod, PaymentMethodDto>
    {
        public PaymentMethodDto Convert(ResolutionContext context)
        {
            if (context == null)
                return null;

            var apiDomainName = AppSettingConfigurationHelper.GetSection("APIDomainName").Value;
            var paymentMethod = (PaymentMethod)context.SourceValue;
            return new PaymentMethodDto()
            {
                Id = paymentMethod.Id,
                BankName = paymentMethod.Bank.BankName,
                Code = paymentMethod.Bank.Code,
                IsManualConfirmation = paymentMethod.Bank.IsManualConfirmation,
                Name = paymentMethod.Bank.Name,
                ImageUrl = string.Format("{0}/api/paymentmethods/image/{1}?v={2}", apiDomainName, paymentMethod.Bank.Code, paymentMethod.LastModificationTime.Value.ToString("ddMMyyyHHmmss"))
            };
        }
    }
}
