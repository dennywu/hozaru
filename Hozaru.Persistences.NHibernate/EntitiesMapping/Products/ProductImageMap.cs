using Hozaru.Domain;
using Hozaru.NHibernate.EntityMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Persistences.NHibernate.EntitiesMapping.Products
{
    public class ProductImageMap : EntityMap<ProductImage, Guid>
    {
        public ProductImageMap()
            : base("ProductImages")
        {
            Map(i => i.ImageUrl).Length(255).Not.Nullable();
            Map(i => i.Priority).Not.Nullable();
            References(i => i.Product).Column("Product_Id").ForeignKey("fk_product_productimage");
            this.MapAudited();
        }
    }
}
