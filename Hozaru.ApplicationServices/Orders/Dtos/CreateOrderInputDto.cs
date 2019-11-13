using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class CreateOrderInputDto
    {
        public string Name { get; set; }
        public string Whatsapp { get; set; }
        public string Email { get; set; }
        public string DistrictCode { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public IList<CreateOrderItemInputDto> Items { get; set; }
        public string ExpeditionCode { get; set; }
        public string PaymentTypeCode { get; set; }

        public CreateOrderInputDto()
        {
            this.Items = new List<CreateOrderItemInputDto>();
        }
    }
}
