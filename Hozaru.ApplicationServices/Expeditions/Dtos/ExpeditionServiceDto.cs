using Hozaru.AutoMapper;
using Hozaru.Core.Application.Services.Dto;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Expeditions.Dtos
{
    [AutoMapFrom(typeof(ExpeditionService))]
    public class ExpeditionServiceDto : EntityDto<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string OriginalFullName { get; set; }
    }
}
