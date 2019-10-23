using Hozaru.Core.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Districtses.Dtos
{
    public class DistrictDto : EntityDto<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
