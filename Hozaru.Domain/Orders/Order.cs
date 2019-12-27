using Hozaru.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Hozaru.Core;
using Hozaru.Core.Domain.Entities.Auditing;
using Hozaru.Domain.Orders;

namespace Hozaru.Domain
{
    public class Order : AuditedEntity<Guid>, IMustHaveTenant
    {
        public virtual string OrderNumber { get; set; }
        public virtual DateTime TransactionDate { get; set; }
        public virtual DateTime DueDateConfirmation { get; set; }
        public virtual OrderCustomer Customer { get; set; }
        public virtual IList<OrderItem> Items { get; set; }
        public virtual OrderShipment Shipment { get; set; }
        public virtual OrderPayment Payment { get; set; }
        public virtual OrderSummary Summary { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual string Note { get; set; }
        public virtual int TenantId { get; set; }

        protected Order()
        {
            this.Items = new List<OrderItem>();
            this.Shipment = new OrderShipment();
            this.Summary = new OrderSummary();
        }

        private Order(string name, string email, string whatsapp, Districts districts, string address, string note, PaymentMethod paymentMethod, Func<DateTime, string> orderNumberGenerator)
            : this()
        {
            this.Customer = new OrderCustomer(name, email, whatsapp, address, districts);
            this.TransactionDate = DateTime.Now;
            this.OrderNumber = orderNumberGenerator.Invoke(this.TransactionDate);
            this.Payment = new OrderPayment(paymentMethod);
            this.Note = note;
            this.DueDateConfirmation = TransactionDate.AddDays(1);
            this.Status = OrderStatus.DRAFT;
        }

        public static Order Create(string name, string email, string whatsapp, Districts districts, string address, string note, PaymentMethod paymentMethod, Func<DateTime, string> orderNumberGenerator)
        {
            return new Order(name, email, whatsapp, districts, address, note, paymentMethod, orderNumberGenerator);
        }

        public virtual void AddItem(Product product, int quantity, string note)
        {
            var orderItem = new OrderItem(this, product, quantity, note);
            this.Items.Add(orderItem);

            calculateSummary();
        }

        public virtual void AddShipment(ExpeditionService expeditionService, Func<decimal, Tuple<decimal, EstimatedTimeDelivery>> getShippingCost)
        {
            var totalWeight = this.Items.Sum(i => i.Product.Weight * i.Quantity);
            var result = getShippingCost(totalWeight);
            var shippingCost = result.Item1;
            var estimatedTimeDelivery = result.Item2;

            this.Shipment = new OrderShipment(expeditionService, shippingCost, estimatedTimeDelivery);
            calculateSummary();
        }

        public virtual void ChangeAirWaybill(string airWaybill)
        {
            this.Shipment.AddAirWayBill(airWaybill);
            this.Shipment.ClearTrackings();
            this.Status = OrderStatus.SHIPPING;
        }

        public virtual void UpdateTrackingInfo(DateTime shipmentDate, string shipmentStatus, string podReceiver, DateTime? podDate)
        {
            this.Shipment.UpdateTrackingInfo(shipmentDate, shipmentStatus, podReceiver, podDate);
        }

        public virtual void AddDetailTrackingInfo(string code, string description, DateTime trackingDate, string cityName)
        {
            this.Shipment.AddDetailTrackingInfo(this, code, description, trackingDate, cityName);
        }

        public virtual void AddPayment(string bankName, string accountName, string accountNumber, string imageFileName)
        {
            this.Payment.AddPayment(this, bankName, accountName, accountNumber, imageFileName);
            this.Status = OrderStatus.WAITINGFORPAYMENT;
        }

        private void calculateSummary()
        {
            this.Summary.Calculate(this);
        }

        public virtual void Approve()
        {
            if (this.Status != OrderStatus.WAITINGFORPAYMENT)
                throw new HozaruException("Mensetujui Pembayaran hanya boleh dari status pesanan 'Verifikasi Pembayaran'.");

            this.Status = OrderStatus.PACKAGING;
        }

        public virtual void Reject()
        {
            if (this.Status != OrderStatus.WAITINGFORPAYMENT)
                throw new HozaruException("Mensetujui Pembayaran hanya boleh dari status pesanan 'Verifikasi Pembayaran'.");

            this.Status = OrderStatus.PAYMENTREJECTED;
        }

        public virtual void Cancel()
        {
            if (this.Status == OrderStatus.DONE)
                throw new HozaruException("Pesanan sudah Selesai, Maaf Anda tidak bisa membatalkan pesanan ini.");
            this.Status = OrderStatus.VOID;
        }

        public virtual void Complete()
        {
            if (this.Status == OrderStatus.SHIPPING || this.Status == OrderStatus.PACKAGING)
            {
                this.Status = OrderStatus.DONE;
            }
            else
            {
                throw new HozaruException("Pesanan bisa Selesai hanya dari status 'Perlu Dikirim' atau 'Sedang Dikirim'.");
            }
        }
    }
}
