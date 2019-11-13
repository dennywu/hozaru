using Hozaru.Domain;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.Products
{
    public class ProductMap : EntityMap<Product, Guid>
    {
        public ProductMap()
            :base("Products")
        {
            Map(i => i.Name).Length(128).Not.Nullable();
            Map(i => i.Description).Length(255).Not.Nullable();
            Map(i => i.Price).Not.Nullable();
            Map(i => i.Weight).Not.Nullable();
            Map(i => i.ImageUrl).Length(64).Not.Nullable();
        }
    }
}
