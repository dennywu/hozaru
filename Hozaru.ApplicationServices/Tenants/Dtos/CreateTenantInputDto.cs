using Hozaru.Core.Application.Services.Dto;
using Hozaru.Identity.MultiTenancy;
using Hozaru.Identity.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hozaru.ApplicationServices.Tenants.Dtos
{
    public class CreateTenantInputDto : IInputDto
    {
        [Required]
        [StringLength(Tenant.MaxTenancyNameLength)]
        [RegularExpression(Tenant.TenancyNameRegex)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(Tenant.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public string WhatsappNumber { get; set; }

        [Required]
        [StringLength(User.MaxEmailAddressLength)]
        [EmailAddress]
        public string AdminEmailAddress { get; set; }

        [Required]
        [StringLength(User.MaxPlainPasswordLength)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public Guid DistrictId { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
