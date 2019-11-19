using Hozaru.Core.Reflection;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace Hozaru.WebApi
{
    /// <summary>
    /// This class is used to get all assemblies in bin folder of a web application.
    /// </summary>
    public class WebAssemblyFinder : IAssemblyFinder
    {
        /// <summary>
        /// The search option used to find assemblies in bin folder.
        /// </summary>
        public static SearchOption FindAssembliesSearchOption = SearchOption.TopDirectoryOnly;

        /// <summary>
        /// This return all assemblies in bin folder of the web application.
        /// </summary>
        /// <returns>List of assemblies</returns>
        public List<Assembly> GetAllAssemblies()
        {
            var assembliesInBinFolder = new List<Assembly>();


            //var allReferencedAssemblies = new List<Assembly>();
            //var refAssemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            //foreach(var asslembyName in refAssemblyNames)
            //{
            //    allReferencedAssemblies.Add(Assembly.Load(asslembyName));
            //}

            ////AppDomain.CurrentDomain.GetAssemblies()

            ////Assembly.GetExecutingAssembly().GetReferencedAssemblies().
            ////var allReferencedAssemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies().Cast<Assembly>().ToList();
            ////var allReferencedAssemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
            //var dllFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", FindAssembliesSearchOption).ToList();
            //foreach (string dllFile in dllFiles)
            //{
            //    var locatedAssembly = allReferencedAssemblies.FirstOrDefault(asm => AssemblyName.ReferenceMatchesDefinition(asm.GetName(), AssemblyName.GetAssemblyName(dllFile)));
            //    if (locatedAssembly != null)
            //    {
            //        assembliesInBinFolder.Add(locatedAssembly);
            //    }
            //}

            //return assembliesInBinFolder;

            var dlls = DependencyContext.Default.CompileLibraries
            .SelectMany(x => x.ResolveReferencePaths())
            .Distinct()
            .Where(x => x.Contains(AppDomain.CurrentDomain.BaseDirectory))
            .ToList();
            foreach (var item in dlls)
            {
                try
                {
                    assembliesInBinFolder.Add(AssemblyLoadContext.Default.LoadFromAssemblyPath(item));
                }
                catch (System.IO.FileLoadException loadEx)
                {
                } // The Assembly has already been loaded.
                catch (BadImageFormatException imgEx)
                {
                } // If a BadImageFormatException exception is thrown, the file is not an assembly.
                catch (Exception ex)
                {
                }
            }

            return assembliesInBinFolder;
        }
    }
}
