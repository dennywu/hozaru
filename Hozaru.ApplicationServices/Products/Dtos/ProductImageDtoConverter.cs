using AutoMapper;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Products.Dtos
{
    public class ProductImageDtoConverter : ITypeConverter<ProductImage, ProductImageDto>
    {
        public ProductImageDto Convert(ResolutionContext context)
        {
            if (context == null)
                return null;

            var productImage = (ProductImage)context.SourceValue;
            return new ProductImageDto()
            {
                Id = productImage.Id,
                Priority = productImage.Priority,
                Url = string.Format("/api/product/image/{0}/{1}?v={2}", productImage.Product.Id, productImage.Id, productImage.LastModificationTime.Value.ToString("ddMMyyyHHmmss"))
            };
        }
    }
}
