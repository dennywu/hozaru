using AutoMapper;
using Hozaru.ApplicationServices.Cities.Dtos;
using Hozaru.ApplicationServices.Districtses.Dtos;
using Hozaru.ApplicationServices.Expeditions.Dtos;
using Hozaru.ApplicationServices.Orders.Dtos;
using Hozaru.ApplicationServices.PaymentMethods.Dtos;
using Hozaru.ApplicationServices.Products.Dtos;
using Hozaru.ApplicationServices.Tenants.Dtos;
using Hozaru.Domain;
using Hozaru.Domain.Orders;
using Hozaru.Identity.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices
{
    public static class DtoMapper
    {
        public static void Map()
        {
            Mapper.CreateMap<Order, OrderDto>().ConvertUsing<OrderDtoConverter>();
            Mapper.CreateMap<OrderCustomer, OrderCustomerDto>().ConvertUsing<OrderCustomerDtoConverter>();
            Mapper.CreateMap<OrderShipment, OrderShipmentDto>().ConvertUsing<OrderShipmentDtoConverter>();
            Mapper.CreateMap<OrderPayment, OrderPaymentDto>().ConvertUsing<OrderPaymentDtoConverter>();
            Mapper.CreateMap<OrderPaymentHistory, OrderPaymentHistoryDto>().ConvertUsing<OrderPaymentHistoryDtoConverter>();
            
            Mapper.CreateMap<Order, ListOrderDto>().ConvertUsing<ListOrderDtoConverter>();
            Mapper.CreateMap<Order, DetailOrderShipmentTrackingDto>().ConvertUsing<DetailOrderShipmentTrackingDtoConverter>();

            Mapper.CreateMap<Product, ProductDto>().ConvertUsing<ProductDtoConverter>();
            Mapper.CreateMap<ProductImage, ProductImageDto>().ConvertUsing<ProductImageDtoConverter>();
            Mapper.CreateMap<PaymentMethod, PaymentMethodDto>().ConvertUsing<PaymentMethodDtoConverter>();
            Mapper.CreateMap<PaymentMethod, PaymentMethodFullDto>().ConvertUsing<PaymentMethodFullDtoConverter>();

            Mapper.CreateMap<Tenant, TenantDto>().ConvertUsing<TenantDtoConverter>();
        }
    }
}
