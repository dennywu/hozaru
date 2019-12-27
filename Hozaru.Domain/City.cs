using Hozaru.Core.Domain.Entities;
using Hozaru.Core.Domain.Entities.Auditing;
using System;

namespace Hozaru.Domain
{
    public class City : AuditedEntity<Guid>
    {
        public virtual int? IdRajaOngkir { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual Province Province { get; set; }

        protected City() { }

        public City(int? idRajaOngkir, string code, string name, string type, string postalCode, Province province)
        {
            this.IdRajaOngkir = idRajaOngkir;
            this.Code = code;
            this.Name = name;
            this.Type = type;
            this.PostalCode = postalCode;
            this.Province = province;
        }
    }
}
