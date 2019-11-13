using Hozaru.Core.Domain.Entities;
using System;

namespace Hozaru.Domain
{
    public class City : Entity<Guid>
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }

        protected City() { }

        public City(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }
    }
}
