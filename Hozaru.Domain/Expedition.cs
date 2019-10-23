using Hozaru.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class Expedition : Entity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", CompanyName, Name);
            }
        }

        public Expedition(string code, string name, string companyCode, string companyName)
        {
            this.Code = code;
            this.Name = name;
            this.CompanyCode = companyCode;
            this.CompanyName = companyName;
        }
    }
}
