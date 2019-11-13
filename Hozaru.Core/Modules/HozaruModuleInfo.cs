using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hozaru.Core.Modules
{
    /// <summary>
    /// Used to store all needed information for a module.
    /// </summary>
    internal class HozaruModuleInfo
    {
        /// <summary>
        /// The assembly which contains the module definition.
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// Type of the module.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Instance of the module.
        /// </summary>
        public HozaruModule Instance { get; private set; }

        /// <summary>
        /// All dependent modules of this module.
        /// </summary>
        public List<HozaruModuleInfo> Dependencies { get; private set; }

        /// <summary>
        /// Creates a new HozaruModuleInfo object.
        /// </summary>
        /// <param name="instance"></param>
        public HozaruModuleInfo(HozaruModule instance)
        {
            Dependencies = new List<HozaruModuleInfo>();
            Type = instance.GetType();
            Instance = instance;
            Assembly = Type.Assembly;
        }

        public override string ToString()
        {
            return string.Format("{0}", Type.AssemblyQualifiedName);
        }
    }
}
