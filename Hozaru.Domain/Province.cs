using Hozaru.Core.Domain.Entities;
using Hozaru.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class Province : AuditedEntity<Guid>
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }

        protected Province() { }
    }
}
