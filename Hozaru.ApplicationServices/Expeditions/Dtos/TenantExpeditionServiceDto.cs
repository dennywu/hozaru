using Hozaru.AutoMapper;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Expeditions.Dtos
{
    [AutoMapFrom(typeof(TenantExpeditionService))]
    public class TenantExpeditionServiceDto
    {
        public Guid Id { get; set; }
        public ExpeditionServiceDto ExpeditionService { get; set; }
        public bool IsActive { get; set; }
    }
}
