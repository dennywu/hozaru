using AutoMapper;
using Hozaru.Core.Configurations;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.PaymentTypes.Dtos
{
    public class PaymentTypeDtoConverter : ITypeConverter<PaymentType, PaymentTypeDto>
    {
        public PaymentTypeDto Convert(ResolutionContext context)
        {
            if (context == null)
                return null;

            var apiDomainName = AppSettingConfigurationHelper.GetSection("APIDomainName").Value;
            var paymentType = (PaymentType)context.SourceValue;
            return new PaymentTypeDto()
            {
                Id = paymentType.Id,
                BankName = paymentType.BankName,
                Code = paymentType.Code,
                IsManualConfirmation = paymentType.IsManualConfirmation,
                Name = paymentType.Name,
                ImageUrl = string.Format("{0}/api/paymenttype/image/{1}?v={2}", apiDomainName, paymentType.Code, paymentType.LastModificationTime.Value.ToString("ddMMyyyHHmmss"))
            };
        }
    }
}
