using Hozaru.ApplicationServices.Products.Dtos;
using Hozaru.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hozaru.ApplicationServices.Products
{
    public interface IProductAppService : IApplicationService
    {
        IList<ProductDto> GetAll();
        Stream GetImage(Guid productId, Guid productImageId);
    }
}
