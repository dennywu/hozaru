using AutoMapper;
using Hozaru.Domain;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Products.Dtos
{
    public class ProductDtoConverter : ITypeConverter<Product, ProductDto>
    {
        public ProductDto Convert(ResolutionContext context)
        {
            if (context == null)
                return null;

            var product = (Product)context.SourceValue;
            return new ProductDto()
            {
                Id = product.Id,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                FirstProductImage = Mapper.Map<ProductImageDto>(product.Images.FirstOrDefault()),
                Images = Mapper.Map<IList<ProductImageDto>>(product.Images.OrderBy(i => i.Priority))
            };
        }
    }
}
