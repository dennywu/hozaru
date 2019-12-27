using Hozaru.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Tenants.Dtos
{
    public class TenantInformationDto
    {
        public string Name { get; set; }
        public string TenancyName { get; set; }
        public int TotalProduct { get; set; }
        public int TotalOrder { get; set; }
        public string Whatsapp { get; set; }
        public string WhatsappUrl { get; set; }
    }
}
