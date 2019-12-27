using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;
using Hozaru.Repositories.MockUp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hozaru.Repositories.MockUp
{
    public class PaymentMethodMockUpRepository : HozaruRepositoryBase<PaymentMethod, Guid>, IRepository<PaymentMethod>
    {
        public override void Delete(PaymentMethod entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<PaymentMethod> GetAll()
        {
            return PaymentMethods.GetAll();
        }

        public override PaymentMethod Insert(PaymentMethod entity)
        {
            throw new NotImplementedException();
        }

        public override PaymentMethod Update(PaymentMethod entity)
        {
            throw new NotImplementedException();
        }
    }
}
