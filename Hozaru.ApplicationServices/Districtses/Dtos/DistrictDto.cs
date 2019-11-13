using Hozaru.ApplicationServices.Cities.Dtos;
using Hozaru.AutoMapper;
using Hozaru.Core.Application.Services.Dto;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Districtses.Dtos
{
    [AutoMapFrom(typeof(Districts))]
    public class DistrictDto : EntityDto<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public CityDto City { get; set; }
    }
}
