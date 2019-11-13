using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Modules
{
    /// <summary>
    /// This interface is responsible to find all modules (derived from <see cref="HozaruModule"/>)
    /// in the application.
    /// </summary>
    public interface IModuleFinder
    {
        /// <summary>
        /// Finds all modules.
        /// </summary>
        /// <returns>List of modules</returns>
        ICollection<Type> FindAll();
    }
}
