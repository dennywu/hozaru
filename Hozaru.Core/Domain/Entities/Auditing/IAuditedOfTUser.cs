using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Domain.Entities.Auditing
{
    public interface IAudited<TUser> : IAudited, ICreationAudited<TUser>, IModificationAudited<TUser>
        where TUser : IEntity<long>
    {

    }
}
