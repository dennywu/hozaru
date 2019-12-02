using Hozaru.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Hozaru.Core;
using Hozaru.Core.Domain.Entities.Auditing;

namespace Hozaru.Domain
{
    public class Order : AuditedEntity<Guid>
    {
        public virtual string OrderNumber { get; set; }
        public virtual DateTime TransactionDate { get; set; }
        public virtual DateTime DueDateConfirmation { get; set; }
        public virtual string CustomerName { get; set; }
        public virtual string Email { get; set; }
        public virtual string WhatsappNumber { get; set; }
        public virtual Districts Districts { get; set; }
        public virtual string Address { get; set; }
        public virtual Expedition Expedition { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual string Note { get; set; }
        public virtual IList<OrderItem> Items { get; set; }
        public virtual decimal ShipingRatePerKG { get; set; }
        public virtual IList<OrderPayment> PaymentHistories { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual string AirWaybill { get; set; }
        public virtual OrderSummary Summary { get; set; }

        protected Order()
        {
            this.Items = new List<OrderItem>();
            this.Summary = new OrderSummary();
            this.PaymentHistories = new List<OrderPayment>();
        }

        private Order(string name, string email, string whatsapp, Districts districts, string address, string note)
            : this()
        {
            this.CustomerName = name;
            this.Email = email;
            this.WhatsappNumber = whatsapp;
            this.Districts = districts;
            this.Address = address;
            this.Note = note;
            this.TransactionDate = DateTime.Now;
            this.DueDateConfirmation = TransactionDate.AddDays(1);
            this.Status = OrderStatus.DRAFT;
        }

        public static Order Create(string name, string email, string whatsapp, Districts districts, string address, string note)
        {
            return new Order(name, email, whatsapp, districts, address, note);
        }

        public virtual void AddItem(Product product, int quantity, string note)
        {
            var orderItem = new OrderItem(this, product, quantity, note);
            this.Items.Add(orderItem);

            calculateSummary();
        }

        public virtual void Shipping(Freight freight, Expedition expedition)
        {
            var freightItem = freight.Items.FirstOrDefault(i => i.Expedition.Code == expedition.Code);
            if (freightItem == null)
                throw new HozaruException("Expedition not Found");
            Expedition = freightItem.Expedition;
            ShipingRatePerKG = freightItem.Rate;

            calculateSummary();
        }

        public virtual void ChangeOrderNumber(string orderNumber)
        {
            this.OrderNumber = orderNumber;
        }

        public virtual void ChangePaymentType(PaymentType paymentType)
        {
            this.PaymentType = paymentType;
        }

        public virtual void Confirmation(string bankName, string accountName, string accountNumber, string imageFileName)
        {
            var newPayment = new OrderPayment(this, imageFileName, bankName, accountName, accountNumber);
            this.PaymentHistories.Add(newPayment);
            this.Status = OrderStatus.WAITINGFORPAYMENT;
        }

        public virtual string GetCustomerAddress()
        {
            return string.Format("{0}, {1}, {2}", Address, Districts.Name, Districts.City.Name);
        }

        private void calculateSummary()
        {
            Summary.Calculate(Items, ShipingRatePerKG);
        }

        public virtual OrderPayment GetLastPayment()
        {
            return this.PaymentHistories.OrderByDescending(i => i.PaymentDate).FirstOrDefault();
        }

        public virtual void Approve()
        {
            this.Status = OrderStatus.PACKAGING;
        }

        public virtual void Reject()
        {
            this.Status = OrderStatus.PAYMENTREJECTED;
        }

        public virtual void UpdateAirWaybill(string airWaybill)
        {
            this.AirWaybill = airWaybill;
            this.Status = OrderStatus.SHIPPING;
        }
    }
}
