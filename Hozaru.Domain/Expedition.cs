using Hozaru.Core.Domain.Entities;
using Hozaru.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class Expedition : AuditedEntity<Guid>
    {
        public virtual string RajaOngkirCode { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string AliasName { get; set; }
        public virtual IList<ExpeditionService> Services { get; set; }

        protected Expedition()
        {
            this.Services = new List<ExpeditionService>();
        }

        public Expedition(string code, string name, string companyCode, string companyName)
            : this()
        {
            this.Code = code;
            this.Name = name;
        }
    }
}
