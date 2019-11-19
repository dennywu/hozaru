using Hozaru.Core.Domain.Entities;
using Hozaru.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class ProductImage : AuditedEntity<Guid>
    {
        public virtual Product Product { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual int Priority { get; set; }

        protected ProductImage()
        {
        }

        public ProductImage(Product product, string imageUrl, int priority) : this()
        {
            this.Product = product;
            this.ImageUrl = imageUrl;
            this.Priority = priority;
        }
    }
}
