using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Domain.Entities.Auditing
{
    public interface IAudited : ICreationAudited, IModificationAudited
    {

    }
}
