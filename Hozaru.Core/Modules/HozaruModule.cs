using Hozaru.Core.Configurations.Startup;
using Hozaru.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Hozaru.Core.Modules
{
    /// <summary>
    /// This class must be implemented by all module definition classes.
    /// </summary>
    /// <remarks>
    /// A module definition class is generally located in it's own assembly
    /// and implements some action in module events on application startup and shotdown.
    /// It also defines depended modules.
    /// </remarks>
    public abstract class HozaruModule
    {
        /// <summary>
        /// Gets a reference to the IOC manager.
        /// </summary>
        protected internal IIocManager IocManager { get; internal set; }

        /// <summary>
        /// Gets a reference to the Hozaru configuration.
        /// </summary>
        protected internal IHozaruStartupConfiguration Configuration { get; internal set; }

        /// <summary>
        /// This is the first event called on application startup. 
        /// Codes can be placed here to run before dependency injection registrations.
        /// </summary>
        public virtual void PreInitialize()
        {

        }

        /// <summary>
        /// This method is used to register dependencies for this module.
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// This method is called lastly on application startup.
        /// </summary>
        public virtual void PostInitialize()
        {

        }

        /// <summary>
        /// This method is called when the application is being shutdown.
        /// </summary>
        public virtual void Shutdown()
        {

        }

        /// <summary>
        /// Checks if given type is an Hozaru module class.
        /// </summary>
        /// <param name="type">Type to check</param>
        public static bool IsHozaruModule(Type type)
        {
            return
                type.IsClass &&
                !type.IsAbstract &&
                typeof(HozaruModule).IsAssignableFrom(type);
        }

        /// <summary>
        /// Finds depended modules of a module.
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsHozaruModule(moduleType))
            {
                throw new HozaruInitializationException("This type is not an Hozaru module: " + moduleType.AssemblyQualifiedName);
            }

            var list = new List<Type>();

            if (moduleType.IsDefined(typeof(DependsOnAttribute), true))
            {
                var dependsOnAttributes = moduleType.GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }

            return list;
        }
    }
}
