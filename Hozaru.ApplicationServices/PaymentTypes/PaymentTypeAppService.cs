using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Hozaru.ApplicationServices.PaymentTypes.Dtos;
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
            var paymentTypes = _paymentTypeRepository.GetAllList();
            return Mapper.Instance.Map<IList<PaymentType>, IList<PaymentTypeDto>>(paymentTypes);
        }

        public Stream GetImage(string code)
        {
            var paymentType = _paymentTypeRepository.FirstOrDefault(i => i.Code == code);
            return File.OpenRead(paymentType.ImageUrl);
        }
    }
}
