using Hozaru.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public static class Validate
    {
        public static void NotNull(object obj, string objName)
        {
            if (obj.IsNull())
                throw new HozaruException(string.Format(Messages.Required, objName));
        }

        public static void NotNull(object obj, string objName, string objValue)
        {
            if (obj.IsNull())
                throw new HozaruException(string.Format(Messages.NotFoundWithValue, objName, objValue));
        }

        public static void Found(object obj, string objName)
        {
            if (obj.IsNull())
                throw new HozaruException(string.Format(Messages.NotFound, objName));
        }

        public static void Found(object obj, string objName, string objValue)
        {
            if (obj.IsNull())
                throw new HozaruException(string.Format(Messages.NotFound, objName, objValue));
        }

        public static void NotNullOrWhiteSpace(string stringObj, string objName)
        {
            if (stringObj.IsNullOrWhiteSpace())
                throw new HozaruException(string.Format(Messages.Required, objName));
        }

        public static void ValidEmail(string stringObj, string objName)
        {
            if (!stringObj.IsValidEmail())
                throw new HozaruException(string.Format(Messages.InvalidEmail, objName));
        }
        public static void NotZero(decimal theObj, string objName)
        {
            if (theObj == decimal.Zero)
                throw new HozaruException(string.Format(Messages.CanNotZero, objName));
        }

        public static void NotExist(bool isExist, params string[] objs)
        {
            if (isExist)
                throw new HozaruException(string.Format(Messages.AlreadyBeenUsed, objs));
        }
    }
}
