using Hozaru.Identity.MultiTenancy;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hozaru.ApplicationServices.Tenants.Dtos
{
    public class EditTenantInputDto
    {
        [Required]
        [StringLength(Tenant.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public string WhatsappNumber { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        [Required]
        public Guid DistrictId { get; set; }

        public IFormFile Image { get; set; }
    }
}
