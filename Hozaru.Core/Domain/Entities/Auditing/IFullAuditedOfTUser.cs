using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Domain.Entities.Auditing
{
    public interface IFullAudited<TUser> : IAudited<TUser>, IFullAudited, IDeletionAudited<TUser>
        where TUser : IEntity<long>
    {

    }
}
