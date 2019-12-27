using Hozaru.ApplicationServices.Roles.Dtos;
using Hozaru.Core.Application.Services;
using Hozaru.Identity.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.ApplicationServices.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task<IList<Role>> CreateDefaultRoles(int? tenantId);
        Task<Role> CreateNewRole(AddNewRoleInput addNewRoleInput, int? tenantId);
        IList<RoleDto> GetAll();
    }
}
