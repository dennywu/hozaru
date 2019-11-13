using Hozaru.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class Product : Entity<Guid>
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal Price { get; set; }
        public virtual decimal Weight { get; set; }
        public virtual string ImageUrl { get; set; }

        protected Product() { }

        public Product(string name, string description, decimal price, decimal weight, string imageUrl)
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Weight = weight;
            this.ImageUrl = imageUrl;
        }
    }
}
