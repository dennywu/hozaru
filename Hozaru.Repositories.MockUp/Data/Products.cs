using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hozaru.Repositories.MockUp.Data
{
    public static class Products
    {
        public static IQueryable<Product> GetAll()
        {
            IList<Product> products = new List<Product>();
            //@"D:\HozaruDevelopment\FileStorage\ProductImages\shampoo.jpg"
            var shampoo = new Product("SHAMPOO & CONDITIONER 2 in 1 LONGRICH", "1", "Shampoo Anti Ketombe", 185000, 300);
            shampoo.Id = Guid.Parse("00000000-0000-0000-0000-000000000001");
            products.Add(shampoo);

            //, @"D:\HozaruDevelopment\FileStorage\ProductImages\sabun.jpg"
            var sabun = new Product("SABUN ARANG BAMBU", "1", "SABUN", 165000, 300);
            sabun.Id = Guid.Parse("00000000-0000-0000-0000-000000000002");
            products.Add(sabun);

            return products.AsQueryable();
        }
    }
}
