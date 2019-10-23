using Hozaru.Core.Domain.Entities;
using System;

namespace Hozaru.Domain
{
    public class City : Entity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public City(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }
    }
}
