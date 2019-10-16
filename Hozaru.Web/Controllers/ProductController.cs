using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hozaru.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "SHAMPOO & CONDITIONER 2 in 1 LONGRICH", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var rng = new Random();
            var products = Enumerable.Range(1, 7).Select(index => new Product
            {
                Id = Guid.NewGuid(),
                Name = Summaries[rng.Next(Summaries.Length)],
                Description = "",
                Price = 185000
            })
            .ToArray();
            return products;
        }
    }
}