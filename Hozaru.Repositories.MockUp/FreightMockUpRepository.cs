using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;
using Hozaru.Repositories.MockUp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hozaru.Repositories.MockUp
{
    public class FreightMockUpRepository : HozaruRepositoryBase<Freight, Guid>, IRepository<Freight>
    {
        public override void Delete(Freight entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<Freight> GetAll()
        {
            return Freights.GetAll();
        }

        public override Freight Insert(Freight entity)
        {
            throw new NotImplementedException();
        }

        public override Freight Update(Freight entity)
        {
            throw new NotImplementedException();
        }
    }
}
