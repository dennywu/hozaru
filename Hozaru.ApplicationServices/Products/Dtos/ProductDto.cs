using Hozaru.AutoMapper;
using Hozaru.Core.Application.Services.Dto;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Products.Dtos
{
    public class ProductDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public ProductStatus Status { get; set; }
        public IList<ProductImageDto> Images { get; set; }
        public ProductImageDto FirstProductImage { get; set; }
    }
}
