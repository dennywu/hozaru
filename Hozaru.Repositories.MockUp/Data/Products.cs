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
            var shampoo = new Product("SHAMPOO & CONDITIONER 2 in 1 LONGRICH", "Shampoo Anti Ketombe", 185000, 300, @"D:\HozaruDevelopment\FileStorage\ProductImages\shampoo.jpg");
            shampoo.Id = Guid.Parse("00000000-0000-0000-0000-000000000001");
            products.Add(shampoo);

            var sabun = new Product("SABUN ARANG BAMBU", "SABUN", 165000, 300, @"D:\HozaruDevelopment\FileStorage\ProductImages\sabun.jpg");
            sabun.Id = Guid.Parse("00000000-0000-0000-0000-000000000002");
            products.Add(sabun);

            return products.AsQueryable();
        }
    }
}
