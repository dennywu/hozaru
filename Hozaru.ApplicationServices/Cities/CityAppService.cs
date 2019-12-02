using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Hozaru.ApplicationServices.Cities.Dtos;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;

namespace Hozaru.ApplicationServices.Cities
{
    public class CityAppService : ICityAppService
    {
        private IRepository<City> _cityRepository;

        public CityAppService(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
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
