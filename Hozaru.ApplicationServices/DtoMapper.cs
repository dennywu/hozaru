using AutoMapper;
using Hozaru.ApplicationServices.Cities.Dtos;
using Hozaru.ApplicationServices.Districtses.Dtos;
using Hozaru.ApplicationServices.Expeditions.Dtos;
using Hozaru.ApplicationServices.Orders.Dtos;
using Hozaru.ApplicationServices.PaymentTypes.Dtos;
using Hozaru.ApplicationServices.Products.Dtos;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices
{
    public static class DtoMapper
    {
        public static void Map()
        {
            Mapper.CreateMap<Order, OrderDto>().ConvertUsing<OrderDtoConverter>();
        }
    }
}
