using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using AutoMapper;
using Hozaru.ApplicationServices.PaymentTypes.Dtos;
using Hozaru.Core.Configurations;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;

namespace Hozaru.ApplicationServices.PaymentTypes
{
    public class PaymentTypeAppService : IPaymentTypeAppService
    {
        private IRepository<PaymentType> _paymentTypeRepository;

        public PaymentTypeAppService(IRepository<PaymentType> paymentTypeRepository)
        {
            _paymentTypeRepository = paymentTypeRepository;
        }

        public IList<PaymentTypeDto> GetAll()
        {
            var paymentTypes = _paymentTypeRepository.GetAllList(i => i.Disabled == false).OrderBy(i => i.BankName).ToList();
            return Mapper.Map<IList<PaymentType>, IList<PaymentTypeDto>>(paymentTypes);
        }

        public Stream GetImage(string code)
        {
            var paymentType = _paymentTypeRepository.FirstOrDefault(i => i.Code == code);
            Validate.Found(paymentType, "Tipe Pembayaran", code);

            var pathFileDirectory = AppSettingConfigurationHelper.GetSection("PathFileStorageDirectory").Value;
            var filePath = Path.Combine(pathFileDirectory, paymentType.ImageUrl);

            return File.OpenRead(filePath);
        }
    }
}
