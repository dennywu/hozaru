using FluentNHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.NHibernate.Configuration
{
    internal class HozaruNHibernateModuleConfiguration : IHozaruNHibernateModuleConfiguration
    {
        public FluentConfiguration FluentConfiguration { get; private set; }

        public HozaruNHibernateModuleConfiguration()
        {
            FluentConfiguration = Fluently.Configure();
        }
    }
}
