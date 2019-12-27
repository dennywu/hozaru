using Hozaru.Core.Application.Services;
using Hozaru.Core.Runtime.Session;
using Hozaru.Identity.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.ApplicationServices
{
    public abstract class HozaruApplicationService : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        //public UserManager UserManager { get; set; }

        protected HozaruApplicationService()
        {
            //LocalizationSourceName = AbpProjectNameConsts.LocalizationSourceName;
        }

        //protected virtual Task<User> GetCurrentUserAsync()
        //{
        //    var user = UserManager.FindByIdAsync(HozaruSession.GetUserId());
        //    if (user == null)
        //    {
        //        throw new ApplicationException("There is no current user!");
        //    }

        //    return user;
        //}

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(HozaruSession.GetTenantId());
        }

        protected virtual Tenant GetCurrentTenant()
        {
            return TenantManager.GetByIdAsync(HozaruSession.GetTenantId()).Result;
        }

        //protected virtual void CheckErrors(IdentityResult identityResult)
        //{
        //    identityResult.CheckErrors();
        //}
    }
}
