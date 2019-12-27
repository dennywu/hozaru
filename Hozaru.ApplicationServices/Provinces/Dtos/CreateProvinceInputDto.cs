using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Provinces.Dtos
{
    public class CreateProvinceInputDto
    {
        public int? IdRajaOngkir { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
