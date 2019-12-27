using Hozaru.Core.Dependency;
using Hozaru.Core.Runtime.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Runtime.Caching
{
    /// <summary>
    /// Implements <see cref="ICacheManager"/> to work with <see cref="MemoryCache"/>.
    /// </summary>
    public class HozaruMemoryCacheManager : CacheManagerBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public HozaruMemoryCacheManager(IIocManager iocManager, ICachingConfiguration configuration)
            : base(iocManager, configuration)
        {
            IocManager.RegisterIfNot<HozaruMemoryCache>(DependencyLifeStyle.Transient);
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            return IocManager.Resolve<HozaruMemoryCache>(new { name });
        }
    }
}
