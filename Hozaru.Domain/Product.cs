using Hozaru.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class Product : Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public string ImageUrl { get; set; }

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
