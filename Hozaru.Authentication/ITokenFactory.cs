using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Authentication
{
    public interface ITokenFactory
    {
        string GenerateToken(int size = 32);
    }
}
