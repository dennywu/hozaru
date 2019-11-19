using AutoMapper;
using Hozaru.Core.Configurations;
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

            var apiDomainName = AppSettingConfigurationHelper.GetSection("APIDomainName").Value;
            var productImage = (ProductImage)context.SourceValue;
            return new ProductImageDto()
            {
                Id = productImage.Id,
                Priority = productImage.Priority,
                Url = string.Format("{0}/api/product/image/{1}/{2}?v={3}", apiDomainName, productImage.Product.Id, productImage.Id, productImage.LastModificationTime.Value.ToString("ddMMyyyHHmmss"))
            };
        }
    }
}
