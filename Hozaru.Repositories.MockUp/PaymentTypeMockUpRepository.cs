using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;
using Hozaru.Repositories.MockUp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hozaru.Repositories.MockUp
{
    public class PaymentTypeMockUpRepository : HozaruRepositoryBase<PaymentType, Guid>, IRepository<PaymentType>
    {
        public override void Delete(PaymentType entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<PaymentType> GetAll()
        {
            return PaymentTypes.GetAll();
        }

        public override PaymentType Insert(PaymentType entity)
        {
            throw new NotImplementedException();
        }

        public override PaymentType Update(PaymentType entity)
        {
            throw new NotImplementedException();
        }
    }
}
