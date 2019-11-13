using Hozaru.AutoMapper;
using Hozaru.Core.Application.Services.Dto;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Expeditions.Dtos
{
    [AutoMapFrom(typeof(Expedition))]
    public class ExpeditionDto : EntityDto<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string FullName { get; set; }
    }
}
