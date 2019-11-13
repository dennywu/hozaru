using Hozaru.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Hozaru.Core.Modules
{
    internal class DefaultModuleFinder : IModuleFinder
    {
        private readonly ITypeFinder _typeFinder;

        public DefaultModuleFinder(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public ICollection<Type> FindAll()
        {
            return _typeFinder.Find(HozaruModule.IsHozaruModule).ToList();
        }
    }
}
