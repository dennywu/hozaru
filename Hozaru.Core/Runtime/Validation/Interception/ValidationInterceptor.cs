using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Runtime.Validation.Interception
{
    /// <summary>
    /// This interceptor is used intercept method calls for classes which's methods must be validated.
    /// </summary>
    public class ValidationInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            //new MethodInvocationValidator(
            //    invocation.Method,
            //    invocation.Arguments
            //    ).Validate();

            //invocation.Proceed();
        }
    }
}
