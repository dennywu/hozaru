using System;
using System.Collections.Generic;
using System.Text;
using Hozaru.ApplicationServices.Orders.Dtos;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;
using System.Linq;
using Hozaru.Whatsapp;
using Hozaru.ApplicationServices.AutoNumbers;
using AutoMapper;
using Hozaru.ApplicationServices.ImagesGenerator;
using Hozaru.Core;
using Hozaru.Core.Application.Services.Dto;
using System.IO;
using Hozaru.Core.Configurations;
using SixLabors.ImageSharp.Formats.Jpeg;
using Hozaru.Identity.MultiTenancy;
using Hozaru.ApplicationServices.RajaOngkir;
using Hozaru.ApplicationServices.Freights;
using Hozaru.ApplicationServices.Freights.Dtos;
using Hozaru.ApplicationServices.RajaOngkir.Dtos;
using Hozaru.Core.Domain.Uow;

namespace Hozaru.ApplicationServices.Orders
{
    public class OrderAppService : HozaruApplicationService, IOrderAppService
    {
        private IRepository<Districts> _districtRepository;
        private IRepository<Product> _productRepository;
        private IRepository<PaymentMethod> _paymentMethodRepository;
        private IRepository<Order> _orderRepository;
        private IAutoNumberGenerator _autoNumberGenerator;
        private IImageGenerator _imageGenerator;
        private TenantManager _tenantManager;
        private IRajaOngkirService _rajaOngkirService;
        private IRepository<ExpeditionService> _expeditionServiceRepository;
        private IFreightAppService _freightAppService;

        public OrderAppService(IRepository<Districts> districtRepository, IRepository<Product> productRepository, IRepository<PaymentMethod> paymentMethodRepository, IRepository<Order> orderRepository, IAutoNumberGenerator autoNumberGenerator, IImageGenerator imageGenerator, TenantManager tenantManager, IRajaOngkirService rajaOngkirService, IRepository<ExpeditionService> expeditionServiceRepository, IFreightAppService freightAppService)
        {
            _districtRepository = districtRepository;
            _productRepository = productRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _orderRepository = orderRepository;
            _autoNumberGenerator = autoNumberGenerator;
            _imageGenerator = imageGenerator;
            _tenantManager = tenantManager;
            _rajaOngkirService = rajaOngkirService;
            _expeditionServiceRepository = expeditionServiceRepository;
            _freightAppService = freightAppService;
        }

        public OrderDto CreateOrder(CreateOrderInputDto inputDto)
        {
            var tenant = GetCurrentTenant();
            var district = _districtRepository.FirstOrDefault(i => i.Id == inputDto.DistrictId);
            Validate.Found(district, "Kecamatan");

            var paymentMethod = _paymentMethodRepository.FirstOrDefault(i => i.Bank.Code == inputDto.PaymentMethodCode);
            Validate.Found(paymentMethod, "Metode Pembayaran", inputDto.PaymentMethodCode);

            var order = Order.Create(inputDto.Name, inputDto.Email, inputDto.Whatsapp, district, inputDto.Address, inputDto.Note, paymentMethod, (transactionDate) =>
            {
                return _autoNumberGenerator.GenerateOrderNumber(transactionDate);
            });

            foreach (var itemInputDto in inputDto.Items)
            {
                var product = _productRepository.Get(itemInputDto.ProductId);
                Validate.Found(product, "Produk");
                order.AddItem(product, itemInputDto.Quantity, itemInputDto.Note);
            }

            var expeditionService = _expeditionServiceRepository.FirstOrDefault(i => i.Id == inputDto.ExpeditionServiceId);
            Validate.Found(expeditionService, "Ekpedisi");

            order.AddShipment(expeditionService, (totalWeight) =>
            {
                var getFreightByServiceInputDto = new GetFreightByServiceInputDto(expeditionService, tenant.District, district, totalWeight);
                var freightResult = _freightAppService.GetFreightByExpeditionService(getFreightByServiceInputDto);
                return Tuple.Create(freightResult.Cost, freightResult.EstimatedTimeDelivery);
            });

            _orderRepository.Insert(order);

            var message = NotificationMessageHelper.GenerateDraftMessage(order, tenant);
            WhatsappAPI.SendMessage(order.Customer.WhatsappNumber, message);

            return Mapper.Map<OrderDto>(order);
        }

        public OrderDto Get(Guid id)
        {
            var order = _orderRepository.Get(id);
            Validate.Found(order, "Orderan");
            return Mapper.Map<OrderDto>(order);
        }

        public DetailOrderShipmentTrackingDto GetShipmentTracking(Guid orderId)
        {
            var order = _orderRepository.Get(orderId);
            Validate.Found(order, "Orderan");
            return Mapper.Map<DetailOrderShipmentTrackingDto>(order);
        }

        public PagedResultOutput<ListOrderDto> GetAll(GetListOrderInputDto inputDto)
        {
            IQueryable<Order> query;
            var orders = _orderRepository.GetAll();
            switch (inputDto.Status)
            {
                case OrderStatusInputDto.DRAFT:
                    query = orders.Where(i => i.Status == OrderStatus.DRAFT);
                    break;
                case OrderStatusInputDto.WAITINGFORPAYMENT:
                    query = orders.Where(i => i.Status == OrderStatus.WAITINGFORPAYMENT);
                    break;
                case OrderStatusInputDto.PAYMENTREJECTED:
                    query = orders.Where(i => i.Status == OrderStatus.PAYMENTREJECTED);
                    break;
                case OrderStatusInputDto.PACKAGING:
                    query = orders.Where(i => i.Status == OrderStatus.PACKAGING);
                    break;
                case OrderStatusInputDto.SHIPPING:
                    query = orders.Where(i => i.Status == OrderStatus.SHIPPING);
                    break;
                case OrderStatusInputDto.DONE:
                    query = orders.Where(i => i.Status == OrderStatus.DONE);
                    break;
                case OrderStatusInputDto.VOID:
                    query = orders.Where(i => i.Status == OrderStatus.VOID);
                    break;
                case OrderStatusInputDto.RETURNED:
                    query = orders.Where(i => i.Status == OrderStatus.RETURNED);
                    break;
                default:
                    query = orders;
                    break;
            }

            var result = query.OrderByDescending(i => i.TransactionDate).PageBy(inputDto).ToList();
            var resultCount = query.Count();
            return new PagedResultOutput<ListOrderDto>(resultCount, Mapper.Map<List<ListOrderDto>>(result));
        }

        public Stream GetReceiptImage(Guid id, Guid paymentId)
        {
            var order = _orderRepository.Get(id);
            Validate.Found(order, "Orderan");

            var payment = order.Payment.PaymentHistories.FirstOrDefault(i => i.Id == paymentId);
            Validate.Found(payment, "Bukti Pembayaran");

            var pathFileDirectory = AppSettingConfigurationHelper.GetSection("PathFileStorageDirectory").Value;
            var filePath = Path.Combine(pathFileDirectory, payment.ReceiptImageUrl);
            return File.OpenRead(filePath);
        }

        public Guid AddPayment(ConfirmationOrderInputDto inputDto)
        {
            var order = _orderRepository.Get(inputDto.Id);
            Validate.Found(order, "Orderan");
            var fileName = string.Format("{0}_{1}", order.OrderNumber, order.Payment.PaymentHistories.Count + 1);
            var filePath = _imageGenerator.SavePaymentReceipt(inputDto.generateImage(), fileName, JpegFormat.Instance);

            order.AddPayment(inputDto.BankName, inputDto.AccountName, inputDto.AccountNumber, filePath);
            _orderRepository.Update(order);

            var tenant = GetCurrentTenant();
            var message = NotificationMessageHelper.GenerateConfirmationMessage(order, tenant);
            WhatsappAPI.SendMessage(order.Customer.WhatsappNumber, message);

            return order.Id;
        }

        public void UpdateAirWaybill(UpdateAirWaybillInputDto inputDto)
        {
            var order = _orderRepository.Get(inputDto.Id);
            Validate.Found(order, "Orderan");

            order.ChangeAirWaybill(inputDto.AirWaybill);
            UpdateTrackingInfo(order.Id);

            _orderRepository.Update(order);
        }

        public void UpdateTrackingInfo(Guid orderId)
        {
            var order = _orderRepository.Get(orderId);
            Validate.Found(order, "Orderan");

            var getTrackingInputDto = new GetTrackingInputDto() { AirWayBill = order.Shipment.AirWaybill, ExpeditionService = order.Shipment.ExpeditionService };
            var result = _rajaOngkirService.Tracking(getTrackingInputDto);

            var shipmentDateSplited = result.WayBillDate.Split("-");
            var shipmentTimeSplited = result.WayBillTime.Split(":");
            var shipmentDateTime = new DateTime(Convert.ToInt32(shipmentDateSplited[0]), Convert.ToInt32(shipmentDateSplited[1]), Convert.ToInt32(shipmentDateSplited[2]), Convert.ToInt32(shipmentTimeSplited[0]), Convert.ToInt32(shipmentTimeSplited[1]), 0);

            if (result.ProofOfDeliveryDate.IsNull())
            {
                order.UpdateTrackingInfo(shipmentDateTime, result.Status, "", null);
            }
            else
            {
                var podDateSplited = result.ProofOfDeliveryDate.Split("-");
                var podTimeSplited = result.ProofOfDeliveryTime.Split(":");
                var proofOfDeliveryDateTime = new DateTime(Convert.ToInt32(podDateSplited[0]), Convert.ToInt32(podDateSplited[1]), Convert.ToInt32(podDateSplited[2]), Convert.ToInt32(podTimeSplited[0]), Convert.ToInt32(podTimeSplited[1]), 0);
                order.UpdateTrackingInfo(shipmentDateTime, result.Status, result.ProofOfDeliveryReceiver, proofOfDeliveryDateTime);
            }

            foreach(var trackingDetail in result.Details)
            {
                var trackingDateplited = trackingDetail.TrackingDate.Split("-");
                var trackingTimeSplited = trackingDetail.TrackingTime.Split(":");
                var trackingDateTime = new DateTime(Convert.ToInt32(trackingDateplited[0]), Convert.ToInt32(trackingDateplited[1]), Convert.ToInt32(trackingDateplited[2]), Convert.ToInt32(trackingTimeSplited[0]), Convert.ToInt32(trackingTimeSplited[1]), 0);
                order.AddDetailTrackingInfo(trackingDetail.Code, trackingDetail.Description, trackingDateTime, trackingDetail.CityName);
            }

            if (order.Shipment.IsDelivered)
            {
                this.CompleteOrder(order.Id);
            }

            _orderRepository.Update(order);
        }

        public void Approve(Guid id)
        {
            var order = _orderRepository.Get(id);
            Validate.Found(order, "Orderan");
            order.Approve();
            _orderRepository.Update(order);

            var tenant = GetCurrentTenant();
            var message = NotificationMessageHelper.GenerateApprovedMessage(order, tenant);
            WhatsappAPI.SendMessage(order.Customer.WhatsappNumber, message);
        }

        public void Reject(RejectPaymentInputDto inputDto)
        {
            var order = _orderRepository.Get(inputDto.Id);
            Validate.Found(order, "Orderan");
            order.Reject();
            _orderRepository.Update(order);

            var tenant = GetCurrentTenant();
            var message = NotificationMessageHelper.GenerateRejectPaymentMessage(order, inputDto.Reason, tenant);
            WhatsappAPI.SendMessage(order.Customer.WhatsappNumber, message);
        }

        public void Cancel(CancelPaymentInputDto inputDto)
        {
            var order = _orderRepository.Get(inputDto.Id);
            Validate.Found(order, "Orderan");
            order.Cancel();
            _orderRepository.Update(order);

            var tenant = GetCurrentTenant();
            var message = NotificationMessageHelper.GenerateCancelOrderMessage(order, inputDto.Reason, tenant);
            WhatsappAPI.SendMessage(order.Customer.WhatsappNumber, message);
        }

        public void CompleteOrder(Guid orderId)
        {
            var order = _orderRepository.Get(orderId);
            Validate.Found(order, "Orderan");
            order.Complete();
            _orderRepository.Update(order);

            var tenant = _tenantManager.FindByIdAsync(order.TenantId).Result;
            var message = NotificationMessageHelper.GenerateCompleteMessage(order,  tenant);
            WhatsappAPI.SendMessage(order.Customer.WhatsappNumber, message);
        }
    }
}
