using Hozaru.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class Districts : Entity<Guid>
    {
        public City City { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public Districts(City city, string code, string name)
        {
            this.City = city;
            this.Code = code;
            this.Name = name;
        }
    }
}
