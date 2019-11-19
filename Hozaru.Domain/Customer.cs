using Hozaru.Core.Domain.Entities;
using Hozaru.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class Customer : AuditedEntity<Guid>
    {
        public virtual string Name { get; set; }
        public virtual string Whatsapp { get; set; }
        public virtual string Email { get; set; }
        public virtual Districts Districts { get; set; }

        protected Customer() { }

        public Customer(string name, string whatsapp, string email, Districts districts)
        {
            this.Name = name;
            this.Whatsapp = whatsapp;
            this.Email = email;
            this.Districts = districts;
        }
    }
}
