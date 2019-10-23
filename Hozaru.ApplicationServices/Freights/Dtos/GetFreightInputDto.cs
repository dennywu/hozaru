using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Freights.Dtos
{
    public class GetFreightInputDto
    {
        public string City { get; set; }
        public string Districts { get; set; }
        public IList<FreightShoppingCartItem> Items { get; set; }
    }
}
