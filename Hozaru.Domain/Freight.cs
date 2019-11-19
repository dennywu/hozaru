using Hozaru.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Hozaru.Core;
using Hozaru.Core.Domain.Entities.Auditing;

namespace Hozaru.Domain
{
    public class Freight : AuditedEntity<Guid>
    {
        public virtual City OriginCity { get; set; }
        public virtual Districts OriginDistricts { get; set; }
        public virtual City DestinationCity { get; set; }
        public virtual Districts DestinationDistricts { get; set; }
        public virtual IList<FreightItem> Items { get; set; }

        protected Freight()
        {
            this.Items = new List<FreightItem>();
        }

        public Freight(City originCity, City destinationCity, Districts destinationDistrict) : this()
        {
            this.OriginCity = originCity;
            this.DestinationCity = destinationCity;
            this.DestinationDistricts = destinationDistrict;
        }

        public virtual FreightItem AddItem(Expedition expedition, decimal rate, int estimatedTimeDepartureMin, int estimatedTimeDepartureMax)
        {
            if (Items.Any(i => i == expedition))
                throw new HozaruException("Expedition already exist");

            var item = new FreightItem(this, expedition, rate, estimatedTimeDepartureMin, estimatedTimeDepartureMax);
            this.Items.Add(item);
            return item;
        }
    }
}
