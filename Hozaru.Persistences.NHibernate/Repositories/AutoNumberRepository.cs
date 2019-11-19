using Hozaru.Domain;
using Hozaru.NHibernate;
using Hozaru.NHibernate.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FluentNHibernate;

namespace Hozaru.Persistences.NHibernate.Repositories
{
    public class AutoNumberRepository : NhRepositoryBase<AutoNumber, Guid>, IAutoNumberRepository
    {
        public AutoNumberRepository(ISessionProvider sessionProvider) : base(sessionProvider)
        {
        }

        public AutoNumber GetAutoNumber(DateTime transactionDate)
        {
            return Session.Query<AutoNumber>().FirstOrDefault(i => i.Date == transactionDate.ToString("yyMMdd"));
        }
    }
}
