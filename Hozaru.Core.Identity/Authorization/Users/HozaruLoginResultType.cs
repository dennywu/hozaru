using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Identity.Authorization.Users
{
    public enum HozaruLoginResultType
    {
        Success = 1,

        InvalidUserNameOrEmailAddress,

        InvalidPassword,

        UserIsNotActive,

        InvalidTenancyName,

        TenantIsNotActive,

        UserEmailIsNotConfirmed,

        UnknownExternalLogin
    }
}
