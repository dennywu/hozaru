using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain.Orders
{
    public class OrderCustomer
    {
        public virtual string CustomerName { get; set; }
        public virtual string Email { get; set; }
        public virtual string WhatsappNumber { get; set; }
        public virtual Districts Districts { get; set; }
        public virtual string Address { get; set; }

        private OrderCustomer() { }

        public OrderCustomer(string name, string email, string whatsapp, string address, Districts districts)
        {
            this.CustomerName = name;
            this.Email = email;
            this.WhatsappNumber = whatsapp;
            this.Districts = districts;
            this.Address = address;
        }

        public virtual string GetCustomerFullAddress()
        {
            return string.Format("{0}, {1}, {2}", this.Address, this.Districts.Name, this.Districts.City.Name);
        }
    }
}
