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
using System.Drawing.Imaging;
using Hozaru.Core.Application.Services.Dto;
using System.IO;
using Hozaru.Core.Configurations;

namespace Hozaru.ApplicationServices.Orders
{
    public class OrderAppService : IOrderAppService
    {
        private IRepository<Districts> _districtRepository;
        private IRepository<Product> _productRepository;
        private IRepository<Freight> _freightRepository;
        private IRepository<PaymentType> _paymentTypeRepository;
        private IRepository<Order> _orderRepository;
        private IAutoNumberGenerator _autoNumberGenerator;
        private IImageGenerator _imageGenerator;

        public OrderAppService(IRepository<Districts> districtRepository, IRepository<Product> productRepository, IRepository<Freight> freightRepository, IRepository<PaymentType> paymentTypeRepository, IRepository<Order> orderRepository, IAutoNumberGenerator autoNumberGenerator, IImageGenerator imageGenerator)
        {
            _districtRepository = districtRepository;
            _productRepository = productRepository;
            _freightRepository = freightRepository;
            _paymentTypeRepository = paymentTypeRepository;
            _orderRepository = orderRepository;
            _autoNumberGenerator = autoNumberGenerator;
            _imageGenerator = imageGenerator;
        }

        public OrderDto CreateOrder(CreateOrderInputDto inputDto)
        {
            var districts = _districtRepository.FirstOrDefault(i => i.Code == inputDto.DistrictCode);
            Validate.Found(districts, "Kecamatan", inputDto.DistrictCode);

            var order = Order.Create(inputDto.Name, inputDto.Email, inputDto.Whatsapp, districts, inputDto.Address, inputDto.Note);
            foreach (var itemInputDto in inputDto.Items)
            {
                var product = _productRepository.Get(itemInputDto.ProductId);
                Validate.Found(product, "Produk");
                order.AddItem(product, itemInputDto.Quantity, itemInputDto.Note);
            }

            var freight = _freightRepository.FirstOrDefault(i => i.DestinationDistricts.Code == districts.Code);
            Validate.Found(freight, "Ongkos Kirim", districts.Code);

            var expedition = freight.Items.FirstOrDefault(i => i.Expedition.Code == inputDto.ExpeditionCode);
            Validate.Found(expedition, "Ekpedisi", inputDto.ExpeditionCode);
            order.Shipping(freight, expedition.Expedition);

            var paymentType = _paymentTypeRepository.FirstOrDefault(i => i.Code == inputDto.PaymentTypeCode);
            Validate.Found(expedition, "Tipe Pembayaran", inputDto.PaymentTypeCode);
            order.ChangePaymentType(paymentType);

            var orderNumber = _autoNumberGenerator.GenerateOrderNumber(order.TransactionDate);
            order.ChangeOrderNumber(orderNumber);

            _orderRepository.Insert(order);

            var message = NotificationMessageHelper.GenerateDraftMessage(order);
            WhatsappAPI.SendMessage(order.WhatsappNumber, message);

            return Mapper.Map<OrderDto>(order);
        }

        public OrderDto Get(Guid id)
        {
            var order = _orderRepository.Get(id);
            Validate.Found(order, "Orderan");
            return Mapper.Map<OrderDto>(order);
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

            var payment = order.PaymentHistories.FirstOrDefault(i => i.Id == paymentId);
            Validate.Found(payment, "Bukti Pembayaran");

            var pathFileDirectory = AppSettingConfigurationHelper.GetSection("PathFileStorageDirectory").Value;
            var filePath = Path.Combine(pathFileDirectory, payment.ReceiptImageUrl);
            return File.OpenRead(filePath);
        }

        public Guid Confirmation(ConfirmationOrderInputDto inputDto)
        {
            var order = _orderRepository.Get(inputDto.Id);
            Validate.Found(order, "Orderan");
            var fileName = string.Format("{0}_{1}", order.OrderNumber, order.PaymentHistories.Count + 1);
            var filePath = _imageGenerator.SavePaymentReceipt(inputDto.generateImage(), fileName, ImageFormat.Jpeg);

            order.Confirmation(inputDto.BankName, inputDto.AccountName, inputDto.AccountNumber, filePath);
            _orderRepository.Update(order);

            var message = NotificationMessageHelper.GenerateConfirmationMessage(order);
            WhatsappAPI.SendMessage(order.WhatsappNumber, message);

            return order.Id;
        }

        public void UpdateAirWaybill(UpdateAirWaybillInputDto inputDto)
        {
            var order = _orderRepository.Get(inputDto.Id);
            Validate.Found(order, "Orderan");

            order.UpdateAirWaybill(inputDto.AirWaybill);
            _orderRepository.Update(order);
        }

        public void Approve(Guid id)
        {
            var order = _orderRepository.Get(id);
            Validate.Found(order, "Orderan");
            order.Approve();
            _orderRepository.Update(order);

            var message = NotificationMessageHelper.GenerateApprovedMessage(order);
            WhatsappAPI.SendMessage(order.WhatsappNumber, message);
        }

        public void Reject(RejectPaymentInputDto inputDto)
        {
            var order = _orderRepository.Get(inputDto.Id);
            Validate.Found(order, "Orderan");
            order.Reject();
            _orderRepository.Update(order);

            var message = NotificationMessageHelper.GenerateRejectPaymentMessage(order, inputDto.Reason);
            WhatsappAPI.SendMessage(order.WhatsappNumber, message);
        }
    }
}
