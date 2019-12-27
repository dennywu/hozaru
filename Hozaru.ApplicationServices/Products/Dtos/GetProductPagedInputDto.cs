using Hozaru.Core.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Products.Dtos
{
    public class GetProductPagedInputDto : IInputDto, IPagedResultRequest
    {
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; }
        public ProductStatusInputDto Status { get; set; }
    }
}
