using AutoMapper;
using Hozaru.ApplicationServices.Cities.Dtos;
using Hozaru.ApplicationServices.Districtses.Dtos;
using Hozaru.ApplicationServices.PaymentTypes.Dtos;
using Hozaru.ApplicationServices.Products.Dtos;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices
{
    public class Mapper
    {
        private IMapper _imapper;
        private static Mapper _instance;

        public Mapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<City, CityDto>();
                cfg.CreateMap<Districts, DistrictDto>();
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<PaymentType, PaymentTypeDto>();
            });
            _imapper = config.CreateMapper();
        }

        public static IMapper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Mapper();
                return _instance._imapper;
            }
        }
    }
}
