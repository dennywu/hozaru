using Hozaru.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Tenants.Dtos
{
    public class TenantInformationDto
    {
        public string Name { get; set; }
        public int TotalProduct { get; set; }
        public int TotalOrder { get; set; }
    }
}
