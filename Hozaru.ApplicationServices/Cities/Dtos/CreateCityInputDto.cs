using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Cities.Dtos
{
    public class CreateCityInputDto
    {
        public virtual int? IdRajaOngkir { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual int IdProvinceRajaOngkir { get; set; }
    }
}
