using AutoMapper;
using Hozaru.ApplicationServices.Roles.Dtos;
using Hozaru.Core;
using Hozaru.Identity.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Hozaru.ApplicationServices.Roles
{
    public class RoleAppService : HozaruApplicationService, IRoleAppService
    {
        private readonly RoleManager _roleManager;

        public RoleAppService(RoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IList<Role>> CreateDefaultRoles(int? tenantId)
        {
            IList<Role> roles = new List<Role>();
            foreach(var roleInputDto in new DefaultRoles().Roles)
            {
                roles.Add(await CreateNewRole(roleInputDto, tenantId));
            }
            return roles;
        }

        public async Task<Role> CreateNewRole(AddNewRoleInput input, int? tenantId)
        {
            var role = new Role(HozaruSession.TenantId, input.Name, input.DisplayName);
            role.TenantId = tenantId;
            role.IsDefault = input.IsDefault;
            var createRoleResult = await _roleManager.CreateAsync(role);
            if (!createRoleResult.Succeeded)
                throw new HozaruException(createRoleResult.Errors.JoinAsString(" "));
            return role;
        }

        public IList<RoleDto> GetAll()
        {
            var roles = _roleManager.Roles.Where(i => i.TenantId == HozaruSession.TenantId);
            return Mapper.Map<IList<RoleDto>>(roles);
        }
    }
}
