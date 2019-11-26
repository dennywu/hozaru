using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Products.Dtos
{
    public class ProductImageInputDto
    {
        public int Priority { get; set; }
        public IFormFile Image { get; set; }
    }
}
