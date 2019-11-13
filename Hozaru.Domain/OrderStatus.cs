using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public enum OrderStatus : int
    {
        DRAFT = 10,
        REVIEW = 20,
        PACKAGING = 30,
        SHIPPING = 50,
        DONE = 60
    }
}
