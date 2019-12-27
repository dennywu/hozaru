using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using AutoMapper;
using Hozaru.ApplicationServices.PaymentMethods.Dtos;
using Hozaru.Core.Configurations;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;
using Hozaru.Core.Application.Services.Dto;
using Hozaru.Core;

namespace Hozaru.ApplicationServices.PaymentMethods
{
    public class PaymentMethodAppService : IPaymentMethodAppService
    {
        private IRepository<PaymentMethod> _paymentMethodRepository;
        private IRepository<Bank> _bankRepository;

        public PaymentMethodAppService(IRepository<PaymentMethod> paymentMethodRepository, IRepository<Bank> bankRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _bankRepository = bankRepository;
        }

        public PaymentMethodFullDto Create(CreatePaymentMethodInputDto inputDto)
        {
            var bank = _bankRepository.FirstOrDefault(i => i.Code == inputDto.BankCode);
            Validate.Found(bank, "Bank");

            if (_paymentMethodRepository.Exist(i => i.Bank.Code == inputDto.BankCode))
                throw new HozaruException(string.Format("{0} sudah terdaftar. Anda hanya diperbolehkan menggunakan 1 Nomor Rekening untuk {0}.", inputDto.BankCode));

            var paymentMethod = new PaymentMethod(bank, inputDto.BankBranch, inputDto.AccountName, inputDto.AccountNumber);
            _paymentMethodRepository.Insert(paymentMethod);
            return Mapper.Map<PaymentMethodFullDto>(paymentMethod);
        }

        public PaymentMethodFullDto Edit(EditPaymentMethodInputDto inputDto)
        {
            var paymentMethod = _paymentMethodRepository.Get(inputDto.Id);
            Validate.Found(paymentMethod, "Metode Pembayaran");

            paymentMethod.Update(inputDto.BankBranch, inputDto.AccountName, inputDto.AccountNumber, inputDto.IsDisabled);
            _paymentMethodRepository.Update(paymentMethod);
            return Mapper.Map<PaymentMethodFullDto>(paymentMethod);
        }

        public PaymentMethodFullDto Get(Guid id)
        {
            var paymentMethod = _paymentMethodRepository.Get(id);
            Validate.Found(paymentMethod, "Metode Pembayaran");

            return Mapper.Map<PaymentMethodFullDto>(paymentMethod);
        }

        public IList<PaymentMethodDto> GetAll()
        {
            var paymentMethods = _paymentMethodRepository.GetAllList(i => i.Disabled == false).OrderBy(i => i.Bank.BankName).ToList();
            return Mapper.Map<IList<PaymentMethod>, IList<PaymentMethodDto>>(paymentMethods);
        }

        public PagedResultOutput<PaymentMethodFullDto> GetAll(GetListPaymentMethodInputDto inputDto)
        {
            var result = _paymentMethodRepository.GetAll().OrderBy(i => i.Bank.Name).PageBy(inputDto).ToList();
            var count = _paymentMethodRepository.GetAll().Count();

            return new PagedResultOutput<PaymentMethodFullDto>(count, Mapper.Map<List<PaymentMethodFullDto>>(result));
        }

        public Stream GetImage(string code)
        {
            var bank = _bankRepository.FirstOrDefault(i => i.Code == code);
            Validate.Found(bank, "Tipe Pembayaran", code);

            var pathFileDirectory = AppSettingConfigurationHelper.GetSection("PathFileStorageDirectory").Value;
            var filePath = Path.Combine(pathFileDirectory, bank.ImageUrl);

            return File.OpenRead(filePath);
        }
    }
}
