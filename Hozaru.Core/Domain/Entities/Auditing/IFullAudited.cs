using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Domain.Entities.Auditing
{
    public interface IFullAudited : IAudited, IDeletionAudited
    {

    }
}
