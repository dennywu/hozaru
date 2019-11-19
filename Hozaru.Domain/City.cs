using Hozaru.Core.Domain.Entities;
using Hozaru.Core.Domain.Entities.Auditing;
using System;

namespace Hozaru.Domain
{
    public class City : AuditedEntity<Guid>
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual Province Province { get; set; }

        protected City() { }

        public City(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }
    }
}
