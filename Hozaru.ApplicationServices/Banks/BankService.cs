using AutoMapper;
using Hozaru.ApplicationServices.Banks.Dtos;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hozaru.ApplicationServices.Banks
{
    public class BankService : IBankService
    {
        private readonly IRepository<Bank> _bankRepository;

        public BankService(IRepository<Bank> bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public IList<BankDto> GetAll()
        {
            var banks = _bankRepository.GetAllList();
            return Mapper.Map<IList<BankDto>>(banks);
        }

        public IList<BankDto> Search(string searchKey)
        {
            var banks = _bankRepository.GetAll()
                .Where(i => i.Name.ToLower().Contains(searchKey.ToLower()))
                .Take(5)
                .ToList();
            return Mapper.Map<IList<BankDto>>(banks);
        }
    }
}
