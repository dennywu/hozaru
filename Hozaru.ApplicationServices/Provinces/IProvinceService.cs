using Hozaru.ApplicationServices.Provinces.Dtos;
using Hozaru.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Provinces
{
    public interface IProvinceService : IApplicationService
    {
        void Create(CreateProvinceInputDto inputDto);
        bool Exist(int idRajaOngkir);
    }
}
