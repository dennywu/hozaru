using Hozaru.ApplicationServices.Freights.Dtos;
using Hozaru.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Freights
{
    public interface IFreightAppService : IApplicationService
    {
        IList<FreightDto> GetFreight(GetFreightInputDto inputDto);
    }
}
