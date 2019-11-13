using Hozaru.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class Expedition : Entity<Guid>
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string CompanyCode { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual bool Disabled { get; set; }
        public virtual string FullName
        {
            get
            {
                return string.Format("{0} {1}", CompanyName, Name);
            }
        }

        protected Expedition() { }

        public Expedition(string code, string name, string companyCode, string companyName)
        {
            this.Code = code;
            this.Name = name;
            this.CompanyCode = companyCode;
            this.CompanyName = companyName;
            this.Disabled = false;
        }
    }
}
