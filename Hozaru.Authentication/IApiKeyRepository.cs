using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.Authentication
{
    public interface IApiKeyRepository
    {
        Task<ApiKey> Get(string providedApiKey);
    }
}
