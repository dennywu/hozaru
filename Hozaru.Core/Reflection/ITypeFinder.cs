using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Reflection
{
    public interface ITypeFinder
    {
        Type[] Find(Func<Type, bool> predicate);

        Type[] FindAll();
    }
}
