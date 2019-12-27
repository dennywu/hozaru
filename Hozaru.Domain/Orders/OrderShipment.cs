using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Hozaru.Domain.Orders
{
    public class OrderShipment
    {
        public virtual ExpeditionService ExpeditionService { get; set; }
        public virtual EstimatedTimeDelivery EstimatedTimeDelivery { get; set; }
        public virtual string AirWaybill { get; set; }
        public virtual decimal ShippingCost { get; set; }
        public virtual DateTime? ShipmentDate { get; set; }
        public virtual bool IsDelivered { get { return ShipmentStatus == "DELIVERED"; } }
        public virtual string ShipmentStatus { get; set; }
        public virtual string ProofOfDeliveryReceiver { get; set; }
        public virtual DateTime? ProofOfDeliveryDate { get; set; }
        public virtual IList<OrderShipmentTracking> Trackings { get; set; }

        public OrderShipment()
        {
            this.AirWaybill = string.Empty;
            this.Trackings = new List<OrderShipmentTracking>();
            this.ShipmentStatus = string.Empty;
            this.ProofOfDeliveryReceiver = string.Empty;
            this.ProofOfDeliveryDate = null;
        }

        public OrderShipment(ExpeditionService expeditionService, decimal shippingCost, EstimatedTimeDelivery estimatedDelivery)
            : this()
        {
            this.ExpeditionService = expeditionService;
            this.ShippingCost = shippingCost;
            this.EstimatedTimeDelivery = estimatedDelivery;
        }

        public virtual void AddAirWayBill(string airWaybill)
        {
            this.AirWaybill = airWaybill;
        }

        public virtual void UpdateTrackingInfo(DateTime shipmentDate, string shipmentStatus, string podReceiver, DateTime? podDate)
        {
            this.ShipmentDate = shipmentDate;
            this.ShipmentStatus = shipmentStatus;
            this.ProofOfDeliveryReceiver = podReceiver;
            this.ProofOfDeliveryDate = podDate;
        }

        public virtual void AddDetailTrackingInfo(Order order, string code, string description, DateTime trackingDate, string cityName)
        {
            if (this.Trackings.Count(i => i.Description == description) == 0)
            {
                var orderShipmentTracking = new OrderShipmentTracking(order, code, description, trackingDate, cityName);
                this.Trackings.Add(orderShipmentTracking);
            }
        }

        public virtual OrderShipmentTracking GetLastTracking()
        {
            return Trackings.OrderByDescending(i => i.TrackingDate).FirstOrDefault();
        }

        public virtual void ClearTrackings()
        {
            Trackings.Clear();
            this.ShipmentDate = null;
            this.ShipmentStatus = string.Empty;
            this.ProofOfDeliveryReceiver = string.Empty;
            this.ProofOfDeliveryDate = null;
        }
    }
}
