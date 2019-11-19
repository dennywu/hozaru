using Castle.Core.Logging;
using Hozaru.Core;
using Hozaru.Core.Dependency;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Hozaru.Web
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var logger = IocManager.Instance.Resolve<ILogger>();
                logger.Error(ex.GetBaseException().Message, ex);
                //_logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorMessage = "Terjadi sesuatu yang tidak terduga sehingga tidak bisa menyelesaikan permintaan Anda. Kami mohon maaf.";
            if(exception is HozaruException)
            {
                errorMessage = exception.Message;
            }
            return context.Response.WriteAsync(new ErrorDetails()
            {
                IsError = true,
                StatusCode = context.Response.StatusCode,
                Message = errorMessage
            }.ToString());
        }
    }
}
