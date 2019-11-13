using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.NHibernate
{
    public interface ISessionProvider
    {
        ISession Session { get; }
    }
}
