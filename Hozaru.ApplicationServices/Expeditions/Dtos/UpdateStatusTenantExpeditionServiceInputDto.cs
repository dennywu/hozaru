using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Expeditions.Dtos
{
    public class UpdateStatusTenantExpeditionServiceInputDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}
