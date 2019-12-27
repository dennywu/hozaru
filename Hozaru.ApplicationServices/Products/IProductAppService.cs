using Hozaru.ApplicationServices.Products.Dtos;
using Hozaru.Core.Application.Services;
using Hozaru.Core.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hozaru.ApplicationServices.Products
{
    public interface IProductAppService : IApplicationService
    {
        Guid Create(CreateNewProductInputDto inputDto);
        Guid Edit(EditProductInputDto inputDto);
        void Archive(Guid id);
        void Activate(Guid id);
        void Delete(Guid id);
        IList<ProductDto> GetAll(ProductStatusInputDto inputDto);
        ProductDto Get(Guid id);
        Stream GetImage(Guid productId, Guid productImageId);
        PagedResultOutput<ProductDto> GetProductActive(GetProductPagedInputDto inputDto);
    }
}
