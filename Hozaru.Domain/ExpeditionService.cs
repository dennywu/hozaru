using Hozaru.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class ExpeditionService : AuditedEntity<Guid>
    {
        public virtual string RajaOngkirCode { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string AliasName { get; set; }
        public virtual string GroupName { get; set; }
        public virtual string FullName
        {
            get { return string.Format("{0} {1}", Expedition.AliasName, AliasName); }
        }

        public virtual string OriginalFullName
        {
            get { return string.Format("{0} {1}", Expedition.AliasName, Code); }
        }

        public virtual Expedition Expedition { get; set; }

        protected ExpeditionService() { }

        public ExpeditionService(string code, string aliasCode, string name, Expedition expedition)
        {
            this.Expedition = expedition;
            this.Code = code;
            this.Name = name;
        }
    }
}
