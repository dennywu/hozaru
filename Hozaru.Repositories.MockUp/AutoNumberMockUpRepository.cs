using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;
using Hozaru.Repositories.MockUp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hozaru.Repositories.MockUp
{
    public class AutoNumberMockUpRepository : HozaruRepositoryBase<AutoNumber, Guid>, IAutoNumberRepository
    {
        public override void Delete(AutoNumber entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<AutoNumber> GetAll()
        {
            throw new NotImplementedException();
        }

        public AutoNumber GetAutoNumber(DateTime transactionDate)
        {
            var autoNumbers = AutoNumbers.GetAll();
            if (autoNumbers == null)
                return null;
            return autoNumbers.FirstOrDefault(i => i.Date == transactionDate.ToString("yyMMdd"));
        }

        public override AutoNumber Insert(AutoNumber entity)
        {
            return AutoNumbers.Add(entity);
        }

        public override AutoNumber Update(AutoNumber entity)
        {
            throw new NotImplementedException();
        }

        public override AutoNumber InsertOrUpdate(AutoNumber entity)
        {
            var autoNumber = AutoNumbers.GetAll().FirstOrDefault(i => i.Date == entity.Date);
            if(autoNumber == null)
            {
                autoNumber = AutoNumbers.Add(autoNumber);
            }
            else
            {
                AutoNumbers.Remove(autoNumber);
                autoNumber = AutoNumbers.Add(entity);
            }
            return autoNumber;
        }
    }
}
