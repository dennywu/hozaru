using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Domain.Entities
{
    [Serializable]
    public abstract class Entity : Entity<Guid>, IEntity
    {

    }
}
