using Hozaru.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class Districts : Entity<Guid>
    {
        public virtual City City { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }

        protected Districts() { }

        public Districts(City city, string code, string name)
        {
            this.City = city;
            this.Code = code;
            this.Name = name;
        }
    }
}
