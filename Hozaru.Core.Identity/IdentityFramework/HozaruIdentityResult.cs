using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Identity.IdentityFramework
{
    public class HozaruIdentityResult : IdentityResult
    {
        public HozaruIdentityResult()
        {

        }

        public HozaruIdentityResult(IEnumerable<string> errors)
            : base(errors)
        {

        }

        public HozaruIdentityResult(params string[] errors)
            : base(errors)
        {

        }

        public static HozaruIdentityResult Failed(params string[] errors)
        {
            return new HozaruIdentityResult(errors);
        }
    }
}
