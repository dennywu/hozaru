using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Authentication
{
    public class ForbiddenProblemDetails : ProblemDetails
    {
        public ForbiddenProblemDetails(string details = null)
        {
            Title = "Forbidden";
            Detail = details;
            Status = 403;
            Type = "https://httpstatuses.com/403";
        }
    }
}
