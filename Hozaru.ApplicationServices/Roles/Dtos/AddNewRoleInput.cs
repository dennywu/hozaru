using Hozaru.Core.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hozaru.ApplicationServices.Roles.Dtos
{
    public class AddNewRoleInput : IInputDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string DisplayName { get; set; }
        public bool IsDefault { get; set; }
    }
}
