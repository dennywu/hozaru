using Hozaru.ApplicationServices.Cities.Dtos;
using Hozaru.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Cities
{
    public interface ICityAppService : IApplicationService
    {
        IList<CityDto> Search(string searchKey);
        bool Exist(int idCityRajaOngkir);
        void Create(CreateCityInputDto inputDto);
    }
}
