using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Authentication
{
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "API Key";
        public string Scheme => DefaultScheme;
        public string AuthenticationType = DefaultScheme;
    }
}
