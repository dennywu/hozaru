using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;
using Hozaru.Repositories.MockUp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hozaru.Repositories.MockUp
{
    public class OrderMockUpRepository : HozaruRepositoryBase<Order, Guid>, IRepository<Order>
    {
        public override void Delete(Order entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<Order> GetAll()
        {
            return Orders.GetAll();
        }

        public override Order Insert(Order entity)
        {
            entity.Id = Guid.NewGuid();
            return Orders.Add(entity);
        }

        public override Order Update(Order entity)
        {
            var order = Orders.GetAll().FirstOrDefault(i => i.Id == entity.Id);
            Orders.Remove(order);
            Orders.Add(entity);
            return entity;
        }
    }
}
