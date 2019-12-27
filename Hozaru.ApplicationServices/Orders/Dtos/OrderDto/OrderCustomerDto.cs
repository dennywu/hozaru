using Hozaru.ApplicationServices.Districtses.Dtos;
using Hozaru.AutoMapper;
using Hozaru.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class OrderCustomerDto
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string WhatsappNumber { get; set; }
        public string WhatsappUrl { get; set; }
        public string Address { get; set; }
        public DistrictDto Districts { get; set; }
    }
}
