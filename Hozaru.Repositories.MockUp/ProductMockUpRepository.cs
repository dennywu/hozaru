using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;
using Hozaru.Repositories.MockUp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hozaru.Repositories.MockUp
{
    public class ProductMockUpRepository : HozaruRepositoryBase<Product, Guid>, IRepository<Product>
    {
        public override void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<Product> GetAll()
        {
            return Products.GetAll();
        }

        public override Product Insert(Product entity)
        {
            throw new NotImplementedException();
        }

        public override Product Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
