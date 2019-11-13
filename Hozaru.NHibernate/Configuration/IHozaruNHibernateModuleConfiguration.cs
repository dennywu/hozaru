using FluentNHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.NHibernate.Configuration
{
    /// <summary>
    /// Used to configure Hozaru NHibernate module.
    /// </summary>
    public interface IHozaruNHibernateModuleConfiguration
    {
        /// <summary>
        /// Used to get and modify NHibernate fluent configuration.
        /// You can add mappings to this object.
        /// Do not call BuildSessionFactory on it.
        /// </summary>
        FluentConfiguration FluentConfiguration { get; }
    }
}
