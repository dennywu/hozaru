using Hozaru.AutoMapper;
using Hozaru.Core.Application.Services.Dto;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Products.Dtos
{
    [AutoMapFrom(typeof(ProductImage))]
    public class ProductImageDto : AuditedEntityDto<Guid>
    {
        public int Priority { get; set; }
        public string Url { get; set; }
    }
}
