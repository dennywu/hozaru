using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;
using Hozaru.Repositories.MockUp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hozaru.Repositories.MockUp
{
    public class DistrictMockUpRepository : HozaruRepositoryBase<Districts, Guid>, IRepository<Districts>
    {
        public override void Delete(Districts entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<Districts> GetAll()
        {
            return Districtses.GetAll();
        }

        public override Districts Insert(Districts entity)
        {
            throw new NotImplementedException();
        }

        public override Districts Update(Districts entity)
        {
            throw new NotImplementedException();
        }
    }
}
