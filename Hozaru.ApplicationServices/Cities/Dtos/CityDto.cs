using Hozaru.AutoMapper;
using Hozaru.Core.Application.Services.Dto;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Cities.Dtos
{
    [AutoMapFrom(typeof(City))]
    public class CityDto : EntityDto<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
