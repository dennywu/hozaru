using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Modules
{
    internal interface IHozaruModuleManager
    {
        void InitializeModules();

        void ShutdownModules();
    }
}
