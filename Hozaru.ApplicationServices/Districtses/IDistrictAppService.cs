using Hozaru.ApplicationServices.Districtses.Dtos;
using Hozaru.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Districtses
{
    public interface IDistrictAppService : IApplicationService
    {
        IList<DistrictDto> GetAll(Guid cityId);
        IList<DistrictDto> Search(Guid cityId, string searchKey);
        bool Exist(int idRajaOngkir);
        void Create(CreateDistrictInputDto inputDto);
    }
}
