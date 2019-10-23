using Hozaru.Core.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Freights.Dtos
{
    public class FreightDto : EntityDto<Guid>
    {
        public string ExpeditionCode { get; set; }
        public string ExpeditionName { get; set; }
        public string ExpeditionFullName { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalWeight { get; set; }
    }
}
