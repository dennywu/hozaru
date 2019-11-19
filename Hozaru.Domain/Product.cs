using Hozaru.Core.Domain.Entities;
using Hozaru.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class Product : AuditedEntity<Guid>
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal Price { get; set; }
        public virtual decimal Weight { get; set; }
        public virtual IList<ProductImage> Images { get; set; }

        protected Product()
        {
            this.Images = new List<ProductImage>();
        }

        public Product(string name, string description, decimal price, decimal weight)
            :this()
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Weight = weight;
        }

        public virtual ProductImage AddImage(string imageUrl)
        {
            var nextPriority = Images.Count + 1;
            var productImage = new ProductImage(this, imageUrl, nextPriority);
            this.Images.Add(productImage);
            return productImage;
        }
    }
}
