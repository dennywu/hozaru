using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.AutoNumbers
{
    public class AutoNumberGenerator : IAutoNumberGenerator
    {
        private IAutoNumberRepository _autoNumberRepository;

        public AutoNumberGenerator(IAutoNumberRepository autoNumberRepository)
        {
            _autoNumberRepository = autoNumberRepository;
        }

        public string GenerateOrderNumber(DateTime transactionDate)
        {
            var autoNumber = _autoNumberRepository.GetAutoNumber(transactionDate);
            if (autoNumber == null)
                autoNumber = new AutoNumber(transactionDate);
            autoNumber.Next();
            _autoNumberRepository.InsertOrUpdate(autoNumber);
            return autoNumber.GetOrderNumber();
        }
    }
}
