using Hozaru.ApplicationServices.Banks.Dtos;
using Hozaru.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Banks
{
    public interface IBankService : IApplicationService
    {
        IList<BankDto> GetAll();
        IList<BankDto> Search(string searchKey);
    }
}
