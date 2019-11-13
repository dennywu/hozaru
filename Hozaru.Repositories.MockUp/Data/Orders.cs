using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hozaru.Repositories.MockUp.Data
{
    public static class Orders
    {
        static IList<Order> orders = new List<Order>();

        public static Order Add(Order order)
        {
            orders.Add(order);
            return order;
        }

        public static IQueryable<Order> GetAll()
        {
            return orders.AsQueryable();
        }

        public static void Remove(Order order)
        {
            orders.Remove(order);
        }
    }
}
