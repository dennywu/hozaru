using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Hozaru.ApplicationServices.Cities.Dtos;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;
using Hozaru.Core;
using Hozaru.Core.Domain.Uow;

namespace Hozaru.ApplicationServices.Cities
{
    public class CityAppService : ICityAppService
    {
        private IRepository<City> _cityRepository;
        private IRepository<Province> _provinceRepository;

        public CityAppService(IRepository<City> cityRepository, IRepository<Province> provinceRepository)
        {
            _cityRepository = cityRepository;
            _provinceRepository = provinceRepository;
        }

        public void Create(CreateCityInputDto inputDto)
        {
            if(_cityRepository.Exist(i => i.IdRajaOngkir == inputDto.IdRajaOngkir))
                throw new HozaruException(string.Format("Kota {0} sudah terdaftar.", inputDto.Name));

            var province = _provinceRepository.FirstOrDefault(i => i.IdRajaOngkir.Value == inputDto.IdProvinceRajaOngkir);
            Validate.Found(province, "Province");

            var city = new City(inputDto.IdRajaOngkir, inputDto.Code, inputDto.Name, inputDto.Type, inputDto.PostalCode, province);
            _cityRepository.Insert(city);
        }

        public bool Exist(int idCityRajaOngkir)
        {
            return _cityRepository.Exist(i => i.IdRajaOngkir == idCityRajaOngkir);
        }

        public IList<CityDto> Search()
        {
            var cities = _cityRepository.GetAllList();
            return Mapper.Map<IList<City>, IList<CityDto>>(cities);
        }

        public IList<CityDto> Search(string searchKey)
        {
            var cities = _cityRepository.GetAll()
                .Where(i => i.Name.ToLower().Contains(searchKey.ToLower()))
                .Take(5)
                .ToList();
            return Mapper.Map<IList<City>, IList<CityDto>>(cities);
        }
    }
}
