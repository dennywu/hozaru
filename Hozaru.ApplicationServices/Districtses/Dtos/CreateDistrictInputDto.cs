using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Districtses.Dtos
{
    public class CreateDistrictInputDto
    {
        public int? IdRajaOngkir { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? IdCityRajaOngkir { get; set; }
    }
}
