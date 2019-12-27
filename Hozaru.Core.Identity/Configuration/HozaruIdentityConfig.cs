using Hozaru.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Identity.Configuration
{
    internal class HozaruIdentityConfig : IHozaruIdentityConfig
    {
        public IRoleManagementConfig RoleManagement
        {
            get { return _roleManagementConfig; }
        }
        private readonly IRoleManagementConfig _roleManagementConfig;

        public IUserManagementConfig UserManagement
        {
            get { return _userManagementConfig; }
        }
        private readonly IUserManagementConfig _userManagementConfig;

        //public ILanguageManagementConfig LanguageManagement
        //{
        //    get { return _languageManagement; }
        //}
        //private readonly ILanguageManagementConfig _languageManagement;


        public HozaruIdentityConfig(
            IRoleManagementConfig roleManagementConfig,
            IUserManagementConfig userManagementConfig
            /*ILanguageManagementConfig languageManagement*/)
        {
            _roleManagementConfig = roleManagementConfig;
            _userManagementConfig = userManagementConfig;
            //_languageManagement = languageManagement;
        }
    }
}
