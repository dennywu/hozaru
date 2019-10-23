using Hozaru.ApplicationServices.Districtses.Dtos;
using Hozaru.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Districtses
{
    public interface IDistrictAppService : IApplicationService
    {
        IList<DistrictDto> GetAll(string cityCode);
        IList<DistrictDto> Search(string cityCode, string searchKey);
    }
}
