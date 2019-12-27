using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Identity.Configuration
{
    public static class HozaruIdentitySettingNames
    {
        public static class UserManagement
        {
            /// <summary>
            /// "Hozaru.Identity.UserManagement.IsEmailConfirmationRequiredForLogin".
            /// </summary>
            public const string IsEmailConfirmationRequiredForLogin = "Hozaru.Identity.UserManagement.IsEmailConfirmationRequiredForLogin";
        }

        public static class OrganizationUnits
        {
            /// <summary>
            /// "Hozaru.Identity.OrganizationUnits.MaxUserMembershipCount".
            /// </summary>
            public const string MaxUserMembershipCount = "Hozaru.Identity.OrganizationUnits.MaxUserMembershipCount";
        }
    }
}
