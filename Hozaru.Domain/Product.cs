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
        public virtual ProductStatus Status { get; set; }
        public virtual IList<ProductImage> Images { get; set; }

        protected Product()
        {
            this.Images = new List<ProductImage>();
            this.Status = ProductStatus.ACTIVE;
        }

        public Product(string name, string description, decimal price, decimal weight)
            :this()
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Weight = weight;
        }

        public static Product Create(string name, string description, decimal price, decimal weight)
        {
            return new Product(name, description, price, weight);
        }

        public virtual void Activate()
        {
            this.Status = ProductStatus.ACTIVE;
        }

        public virtual void Archive()
        {
            this.Status = ProductStatus.ARCHIVE;
        }

        public virtual void Update(string name, string description, decimal price, decimal weight)
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Weight = weight;
        }

        public virtual ProductImage AddImage(string imageUrl, string fileName, int priority)
        {
            var productImage = new ProductImage(this, imageUrl, priority, fileName);
            this.Images.Add(productImage);
            return productImage;
        }

        public virtual void RemoveImage(ProductImage productImage)
        {
            this.Images.Remove(productImage);
        }
    }
}
