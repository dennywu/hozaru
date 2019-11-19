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
            var order = Order.Create(inputDto.Name, inputDto.Email, inputDto.Whatsapp, districts, inputDto.Address, inputDto.Note);
            foreach(var itemInputDto in inputDto.Items)
            {
                var product = _productRepository.Get(itemInputDto.ProductId);
                order.AddItem(product, itemInputDto.Quantity, itemInputDto.Note);
            }

            var freight = _freightRepository.FirstOrDefault(i => i.DestinationDistricts.Code == districts.Code);
            var expedition = freight.Items.FirstOrDefault(i => i.Expedition.Code == inputDto.ExpeditionCode);
            order.Shipping(freight, expedition.Expedition);

            var paymentType = _paymentTypeRepository.FirstOrDefault(i => i.Code == inputDto.PaymentTypeCode);
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
            if (order.IsNull())
                throw new HozaruException("Orderan tidak ditemukan.");
            return Mapper.Map<OrderDto>(order);
        }

        public Guid Confirmation(ConfirmationOrderInputDto inputDto)
        {
            var order = _orderRepository.Get(inputDto.Id);
            var fileName = string.Format("{0}_{1}", order.OrderNumber, order.PaymentHistories.Count + 1);
            var filePath = _imageGenerator.SavePaymentReceipt(inputDto.generateImage(), fileName);

            order.Confirmation(inputDto.BankName, inputDto.AccountName, inputDto.AccountNumber, filePath);
            _orderRepository.Update(order);

            var message = NotificationMessageHelper.GenerateConfirmationMessage(order);
            WhatsappAPI.SendMessage(order.WhatsappNumber, message);

            return order.Id;
        }
    }
}
