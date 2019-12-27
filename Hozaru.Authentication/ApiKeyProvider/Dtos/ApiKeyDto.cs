using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Authentication.ApiKeyProvider.Dtos
{
    public class ApiKeyDto
    {
        public string TenancyName { get; set; }
        public string ApiKey { get; set; }
    }
}
