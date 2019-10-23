using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;
using Hozaru.Repositories.MockUp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hozaru.Repositories.MockUp
{
    public class CityMockUpRepository : HozaruRepositoryBase<City, Guid>, IRepository<City>
    {
        public override void Delete(City entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<City> GetAll()
        {
            return Cities.GetAll();
        }

        public override City Insert(City entity)
        {
            throw new NotImplementedException();
        }

        public override City Update(City entity)
        {
            throw new NotImplementedException();
        }
    }
}
