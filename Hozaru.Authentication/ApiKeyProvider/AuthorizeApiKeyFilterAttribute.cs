//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Authorization;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace Hozaru.Authentication.ApiKeyProvider
//{
//    public class AuthorizeApiKeyFilterAttribute : TypeFilterAttribute
//    {
//        public AuthorizeApiKeyFilterAttribute() : base(typeof(ApiKeyAuthFilter))
//        { }
//    }

//    public class ApiKeyAuthFilter : AuthorizeFilter
//    {
//        public ApiKeyAuthFilter(IAuthorizationPolicyProvider provider)
//            : base(provider, new[] { new AuthorizeData }) { }

//        public override async Task OnAuthorizationAsync(Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext context)
//        {
//            await base.OnAuthorizationAsync(context);
//        }
//    }
//}
