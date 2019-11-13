using Hozaru.Core.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Immutable;

namespace Hozaru.Core.Configurations.Startup
{
    public class CachingConfiguration : ICachingConfiguration
    {
        public IReadOnlyList<ICacheConfigurator> Configurators
        {
            get { return _configurators.ToImmutableList(); }
        }
        private readonly List<ICacheConfigurator> _configurators;

        public CachingConfiguration()
        {
            _configurators = new List<ICacheConfigurator>();
        }

        public void ConfigureAll(Action<ICache> initAction)
        {
            _configurators.Add(new CacheConfigurator(initAction));
        }

        public void Configure(string cacheName, Action<ICache> initAction)
        {
            _configurators.Add(new CacheConfigurator(cacheName, initAction));
        }
    }
}
