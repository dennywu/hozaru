using Hozaru.ApplicationServices.RajaOngkir.Dtos;
using Hozaru.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.RajaOngkir
{
    public interface IRajaOngkirService : IApplicationService
    {
        void CollectOrUpdateProvinceCityAndSubDistrict();
        IList<ApiRajaOngkirShippingCostResponseDto> GetShippingCost(GetShippingCostInputDto inputDto);
        ApiRajaOngkirTrackingDto Tracking(GetTrackingInputDto inputDto);
    }
}
