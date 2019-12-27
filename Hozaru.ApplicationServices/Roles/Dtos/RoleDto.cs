using Hozaru.AutoMapper;
using Hozaru.Identity.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Roles.Dtos
{
    [AutoMapFrom(typeof(Role))]
    public class RoleDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
