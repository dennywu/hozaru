using Hozaru.Core.Identity.MultiTenancy;
using Hozaru.Domain;
using Hozaru.Identity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Identity.MultiTenancy
{
    public class Tenant : HozaruTenant<Tenant, User>
    {
        private string _whatsappNumber;
        private string _phone;
        private string _address;
        private string _externalDomain;
        private Districts _district;

        #region Prop's

        //public virtual string Website { get { return _website; } }
        public virtual string WhatsappNumber { get { return _whatsappNumber; } }
        public virtual string Phone { get { return _phone; } }
        public virtual string Address { get { return _address; } }
        //public virtual string City { get { return _city; } }
        //public virtual string Country { get { return _country; } }
        public virtual Districts District { get { return _district; } }
        //public virtual string Notes { get { return _notes; } }
        public virtual string ExternalDomain { get { return _externalDomain; } }

        #endregion

        protected Tenant() { }

        public Tenant(string tenancyName, string name, string whatsapp, string address, string phone, Districts district)
            : base(tenancyName.ToLower(), name)
        {
            _whatsappNumber = whatsapp;
            _address = address;
            _phone = phone;
            _district = district;
            _externalDomain = string.Empty;
        }

        public virtual void Update(string name, string whatsapp, string address, string phone, Districts district)
        {
            Name = name;
            _whatsappNumber = whatsapp;
            _address = address;
            _phone = phone;
            _district = district;
        }
    }
}
