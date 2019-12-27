using System;
using System.Collections.Generic;
using System.Text;
using Hozaru.ApplicationServices.Provinces.Dtos;
using Hozaru.Core;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Core.Domain.Uow;
using Hozaru.Domain;

namespace Hozaru.ApplicationServices.Provinces
{
    public class ProvinceService : IProvinceService
    {
        private readonly IRepository<Province> _provinceRepo;

        public ProvinceService(IRepository<Province> provinceRepo)
        {
            _provinceRepo = provinceRepo;
        }

        public void Create(CreateProvinceInputDto inputDto)
        {
            if (_provinceRepo.Exist(i => i.Code == inputDto.Code))
                throw new HozaruException(string.Format("Provinsi {0} sudah terdaftar.", inputDto.Name));

            var province = new Province(inputDto.IdRajaOngkir, inputDto.Code, inputDto.Name);
            _provinceRepo.Insert(province);
        }

        public bool Exist(int idRajaOngkir)
        {
            return _provinceRepo.Exist(i => i.IdRajaOngkir == idRajaOngkir);
        }
    }
}
