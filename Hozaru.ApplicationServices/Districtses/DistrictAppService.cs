using System;
using System.Collections.Generic;
using System.Text;
using Hozaru.ApplicationServices.Districtses.Dtos;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;
using System.Linq;
using Hozaru.Core;
using AutoMapper;

namespace Hozaru.ApplicationServices.Districtses
{
    public class DistrictAppService : IDistrictAppService
    {
        private IRepository<City> _cityRepository;
        private IRepository<Districts> _districtRepository;

        public DistrictAppService(IRepository<City> cityRepository,IRepository<Districts> districtRepository)
        {
            _cityRepository = cityRepository;
            _districtRepository = districtRepository;
        }

        public IList<DistrictDto> GetAll(string cityCode)
        {
            if (!_cityRepository.Exist(i => i.Code == cityCode))
                return new List<DistrictDto>();

            var city = _cityRepository.FirstOrDefault(i => i.Code == cityCode);
            var districtses = _districtRepository.GetAll()
                .Where(i => i.City.Code == city.Code)
                .ToList();
            return Mapper.Map<IList<Districts>, IList<DistrictDto>>(districtses);
        }

        public IList<DistrictDto> Search(string cityCode, string searchKey)
        {
            if (!_cityRepository.Exist(i => i.Code == cityCode))
                return new List<DistrictDto>();

            var city = _cityRepository.FirstOrDefault(i => i.Code == cityCode);

            var districtses = _districtRepository.GetAll()
                .Where(i => i.City.Code == city.Code && i.Name.ToLower().Contains(searchKey.ToLower()))
                .Take(5)
                .ToList();
            return Mapper.Map<IList<Districts>, IList<DistrictDto>>(districtses);
        }
    }
}
