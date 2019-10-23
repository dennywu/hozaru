using Hozaru.Core.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Cities.Dtos
{
    public class CityDto : EntityDto<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
