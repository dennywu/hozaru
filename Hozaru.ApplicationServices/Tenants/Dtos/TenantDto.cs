using Hozaru.ApplicationServices.Districtses.Dtos;
using Hozaru.AutoMapper;
using Hozaru.Core.Application.Services.Dto;
using Hozaru.Identity.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Tenants.Dtos
{
    public class TenantDto : EntityDto<int>
    {
        public string TenancyName { get; set; }
        public string Name { get; set; }
        public string WhatsappNumber { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DistrictDto District { get; set; }
        public string ExternalDomain { get; set; }
        public string BrandUrl { get; set; }
    }
}
