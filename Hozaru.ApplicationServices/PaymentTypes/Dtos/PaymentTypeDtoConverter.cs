using AutoMapper;
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

            var paymentType = (PaymentType)context.SourceValue;
            return new PaymentTypeDto()
            {
                Id = paymentType.Id,
                BankName = paymentType.BankName,
                Code = paymentType.Code,
                IsManualConfirmation = paymentType.IsManualConfirmation,
                Name = paymentType.Name,
                ImageUrl = string.Format("/api/paymenttype/image/{0}?v={1}", paymentType.Code, paymentType.LastModificationTime.Value.ToString("ddMMyyyHHmmss"))
            };
        }
    }
}
