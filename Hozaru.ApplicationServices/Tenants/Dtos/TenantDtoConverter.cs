using AutoMapper;
using Hozaru.ApplicationServices.Districtses.Dtos;
using Hozaru.Core.Configurations;
using Hozaru.Identity.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Tenants.Dtos
{
    public class TenantDtoConverter : ITypeConverter<Tenant, TenantDto>
    {
        public TenantDto Convert(ResolutionContext context)
        {
            if (context == null)
                return null;

            var apiDomainName = AppSettingConfigurationHelper.GetSection("APIDomainName").Value;
            var tenant = (Tenant)context.SourceValue;
            return new TenantDto()
            {
                Id = tenant.Id,
                Address = tenant.Address,
                BrandUrl =string.Format("{0}/api/tenants/brand/{1}?v={2}", apiDomainName, tenant.TenancyName, tenant.LastModificationTime.Value.ToString("ddMMyyyHHmmss")),
                District = Mapper.Map<DistrictDto>(tenant.District),
                ExternalDomain = tenant.ExternalDomain,
                Name = tenant.Name,
                TenancyName = tenant.TenancyName,
                Phone = tenant.Phone,
                WhatsappNumber = tenant.WhatsappNumber
            };
        }
    }
}
