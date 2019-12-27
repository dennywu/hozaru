using Hozaru.ApplicationServices.Roles.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hozaru.ApplicationServices.Roles
{
    public class DefaultRoles
    {
        public readonly IList<AddNewRoleInput> Roles;

        public DefaultRoles()
        {
            Roles = new List<AddNewRoleInput>();

            Roles.Add(new AddNewRoleInput { Name = "Admin", DisplayName = "Administrator", IsDefault = true });
            Roles.Add(new AddNewRoleInput { Name = "Staff", DisplayName = "Staff", IsDefault = false });

            Roles.OrderBy(i => i.Name);
        }
    }
}
