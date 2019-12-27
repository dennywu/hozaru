using Hozaru.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain.Orders
{
    public class OrderShipmentTracking : AuditedEntity<Guid>
    {
        private Order _order;
        private string _code;
        private string _description;
        private DateTime _trackingDate;
        private string _cityName;

        protected OrderShipmentTracking() { }

        public OrderShipmentTracking(Order order, string code, string description, DateTime trackingDate, string cityName)
        {
            _order = order;
            _code = code.ToString();
            _description = description;
            _trackingDate = trackingDate;
            _cityName = cityName;
        }

        public virtual Order Order { get { return _order; } }
        public virtual string Code { get { return _code; } }
        public virtual string Description { get { return _description; } }
        public virtual DateTime TrackingDate { get { return _trackingDate; } }
        public virtual string CityName { get { return _cityName; } }
    }
}
