using Hozaru.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.AutoNumbers
{
    public interface IAutoNumberGenerator : IApplicationService
    {
        string GenerateOrderNumber(DateTime transactionDate);
    }
}
