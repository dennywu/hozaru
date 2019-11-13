using Hozaru.Core.Configurations.Startup;
using Hozaru.Core.Dependency;
using Hozaru.Core.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core
{
    /// <summary>
    /// This is the main class that is responsible to start entire Hozaru system.
    /// Prepares dependency injection and registers core components needed for startup.
    /// It must be instantiated and initialized (see <see cref="Initialize"/>) first in an application.
    /// </summary>
    public class HozaruBootstrapper : IDisposable
    {
        /// <summary>
        /// Gets IIocManager object used by this class.
        /// </summary>
        public IIocManager IocManager { get; private set; }

        /// <summary>
        /// Is this object disposed before?
        /// </summary>
        protected bool IsDisposed;

        private IHozaruModuleManager _moduleManager;

        /// <summary>
        /// Creates a new <see cref="HozaruBootstrapper"/> instance.
        /// </summary>
        public HozaruBootstrapper()
            : this(Dependency.IocManager.Instance)
        {

        }

        /// <summary>
        /// Creates a new <see cref="HozaruBootstrapper"/> instance.
        /// </summary>
        /// <param name="iocManager">IIocManager that is used to bootstrap the Hozaru system</param>
        public HozaruBootstrapper(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        /// <summary>
        /// Initializes the Hozaru system.
        /// </summary>
        public virtual void Initialize()
        {
            IocManager.IocContainer.Install(new HozaruCoreInstaller());

            IocManager.Resolve<HozaruStartupConfiguration>().Initialize();

            _moduleManager = IocManager.Resolve<IHozaruModuleManager>();
            _moduleManager.InitializeModules();
        }

        /// <summary>
        /// Disposes the Hozaru system.
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (_moduleManager != null)
            {
                _moduleManager.ShutdownModules();
            }
        }
    }
}
