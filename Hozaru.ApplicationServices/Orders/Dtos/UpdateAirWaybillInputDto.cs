﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class UpdateAirWaybillInputDto
    {
        public Guid Id { get; set; }
        public string AirWaybill { get; set; }
    }
}
