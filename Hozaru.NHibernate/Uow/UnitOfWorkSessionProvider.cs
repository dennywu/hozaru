using Hozaru.Core.Dependency;
using Hozaru.Core.Domain.Uow;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.NHibernate.Uow
{
    public class UnitOfWorkSessionProvider : ISessionProvider, ITransientDependency
    {
        public ISession Session
        {
            get { return _unitOfWorkProvider.Current.GetSession(); }
        }

        private readonly ICurrentUnitOfWorkProvider _unitOfWorkProvider;

        public UnitOfWorkSessionProvider(ICurrentUnitOfWorkProvider unitOfWorkProvider)
        {
            _unitOfWorkProvider = unitOfWorkProvider;
        }
    }
}
