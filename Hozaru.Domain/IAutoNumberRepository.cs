using Hozaru.Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public interface IAutoNumberRepository : IRepository<AutoNumber>
    {
        AutoNumber GetAutoNumber(DateTime transactionDate);
    }
}
