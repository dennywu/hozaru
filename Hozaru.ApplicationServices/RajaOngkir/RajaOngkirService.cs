using Hozaru.ApplicationServices.Cities;
using Hozaru.ApplicationServices.Cities.Dtos;
using Hozaru.ApplicationServices.Districtses;
using Hozaru.ApplicationServices.Districtses.Dtos;
using Hozaru.ApplicationServices.Provinces;
using Hozaru.ApplicationServices.Provinces.Dtos;
using Hozaru.ApplicationServices.RajaOngkir.Dtos;
using Hozaru.Core;
using Hozaru.Core.Configurations;
using Hozaru.Core.Domain.Uow;
using Hozaru.Core.Threading;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Hozaru.ApplicationServices.RajaOngkir
{
    public class RajaOngkirService : IRajaOngkirService
    {
        private readonly IProvinceService _provinceService;
        private readonly ICityAppService _cityService;
        private readonly IDistrictAppService _districtService;

        public RajaOngkirService(IProvinceService provinceService, ICityAppService cityService, IDistrictAppService districtService)
        {
            _provinceService = provinceService;
            _cityService = cityService;
            _districtService = districtService;
        }

        [UnitOfWork]
        public void CollectOrUpdateProvinceCityAndSubDistrict()
        {
            collectAndInsertOrUpdateProvinces();
            var cities = collectAndInsertOrUpdateCities();
            collectAndInsertOrUpdateDistricts(cities);
        }

        private void collectAndInsertOrUpdateProvinces()
        {
            var provinces = collectToRajaOngkir<IList<ApiRajaOngkirProvinceResponseDto>>("province");

            foreach (var provinceInputDto in provinces)
            {
                if (!_provinceService.Exist(provinceInputDto.ProvinceId))
                {
                    _provinceService.Create(new CreateProvinceInputDto { IdRajaOngkir = provinceInputDto.ProvinceId, Code = provinceInputDto.ProvinceName, Name = provinceInputDto.ProvinceName });
                }
            }
        }

        private IList<ApiRajaOngkirCityResponseDto> collectAndInsertOrUpdateCities()
        {
            var cities = collectToRajaOngkir<IList<ApiRajaOngkirCityResponseDto>>("city");

            foreach (var cityInputDto in cities)
            {
                if (!_cityService.Exist(cityInputDto.CityId))
                {
                    _cityService.Create(new CreateCityInputDto
                    {
                        IdRajaOngkir = cityInputDto.CityId,
                        Code = cityInputDto.CityName,
                        Name = cityInputDto.CityName,
                        PostalCode = cityInputDto.PostalCode,
                        Type = cityInputDto.Type,
                        IdProvinceRajaOngkir = cityInputDto.ProvinceId
                    });
                }
            }

            return cities;
        }

        private void collectAndInsertOrUpdateDistricts(IList<ApiRajaOngkirCityResponseDto> cities)
        {
            foreach (var city in cities)
            {
                var query = new Dictionary<string, string>
                {
                    ["city"] = city.CityId.ToString()
                };

                var districtses = collectToRajaOngkir<IList<ApiRajaOngkirDistrictResponseDto>>("subdistrict", query);

                foreach (var districtResponse in districtses)
                {
                    if (!_districtService.Exist(districtResponse.DistrictId))
                    {
                        _districtService.Create(new CreateDistrictInputDto
                        {
                            IdRajaOngkir = districtResponse.DistrictId,
                            IdCityRajaOngkir = districtResponse.CityId,
                            Code = districtResponse.DistrictName,
                            Name = districtResponse.DistrictName
                        });
                    }
                }
            }
        }

        private T collectToRajaOngkir<T>(string apiPath, Dictionary<string, string> query = null)
        {
            using (var client = new HttpClient())
            {
                var url = AppSettingConfigurationHelper.GetSection("APIUrlRajaOngkir").Value;
                var apiKey = AppSettingConfigurationHelper.GetSection("APIKeyRajaOngkir").Value;
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("key", apiKey);

                var urlQueryString = apiPath;

                if (query.IsNotNull())
                {
                    urlQueryString = QueryHelpers.AddQueryString(apiPath, query);
                }

                HttpResponseMessage response = client.GetAsync(urlQueryString).Result;

                var resultString = AsyncHelper.RunSync(() => response.Content.ReadAsStringAsync());
                JToken token = JObject.Parse(resultString);

                if ((string)token.SelectToken("rajaongkir").SelectToken("status").SelectToken("description") != HttpStatusCode.OK.ToString())
                    throw new HozaruException((string)token.SelectToken("rajaongkir").SelectToken("status").SelectToken("description"));

                var result = token.SelectToken("rajaongkir").SelectToken("results");
                return result.ToObject<T>();
            }
        }

        public IList<ApiRajaOngkirShippingCostResponseDto> GetShippingCost(GetShippingCostInputDto inputDto)
        {
            using (var client = new HttpClient())
            {
                var url = AppSettingConfigurationHelper.GetSection("APIUrlRajaOngkir").Value;
                var apiKey = AppSettingConfigurationHelper.GetSection("APIKeyRajaOngkir").Value;
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("key", apiKey);

                var couriers = inputDto.Expeditions.Select(i => i.Code.ToLower()).JoinAsString(":");

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("origin", inputDto.Origin.IdRajaOngkir.ToString()),
                    new KeyValuePair<string, string>("originType", "subdistrict"),
                    new KeyValuePair<string, string>("destination", inputDto.Destination.IdRajaOngkir.ToString()),
                    new KeyValuePair<string, string>("destinationType", "subdistrict"),
                    new KeyValuePair<string, string>("weight", (inputDto.Weight + 100).ToString()),
                    new KeyValuePair<string, string>("courier", couriers)
                });

                HttpResponseMessage response = client.PostAsync("cost", content).Result;

                var resultString = AsyncHelper.RunSync(() => response.Content.ReadAsStringAsync());
                JToken token = JObject.Parse(resultString);

                if ((string)token.SelectToken("rajaongkir").SelectToken("status").SelectToken("description") != HttpStatusCode.OK.ToString())
                    throw new HozaruException("Ongkos Kirim tidak ditemukan.");

                var result = token.SelectToken("rajaongkir").SelectToken("results");
                return result.ToObject<IList<ApiRajaOngkirShippingCostResponseDto>>();
            }
        }

        public ApiRajaOngkirTrackingDto Tracking(GetTrackingInputDto inputDto)
        {
            using (var client = new HttpClient())
            {
                var url = AppSettingConfigurationHelper.GetSection("APIUrlRajaOngkir").Value;
                var apiKey = AppSettingConfigurationHelper.GetSection("APIKeyRajaOngkir").Value;
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("key", apiKey);

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("waybill", inputDto.AirWayBill),
                    new KeyValuePair<string, string>("courier", inputDto.ExpeditionService.Expedition.RajaOngkirCode.ToLower())
                });

                HttpResponseMessage response = client.PostAsync("waybill", content).Result;

                var resultString = AsyncHelper.RunSync(() => response.Content.ReadAsStringAsync());
                JToken token = JObject.Parse(resultString);

                if ((string)token.SelectToken("rajaongkir").SelectToken("status").SelectToken("description") != HttpStatusCode.OK.ToString())
                    throw new HozaruException((string)token.SelectToken("rajaongkir").SelectToken("status").SelectToken("description"));

                var result = token.SelectToken("rajaongkir").SelectToken("result");
                return result.ToObject<ApiRajaOngkirTrackingDto>();
            }
        }
    }
}
