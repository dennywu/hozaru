using Hozaru.Core.Domain.Entities;
using Hozaru.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class FreightItem : AuditedEntity<Guid>
    {
        public virtual Expedition Expedition { get; set; }
        public virtual decimal Rate { get; set; }
        public virtual int EstimatedTimeDepartureMin { get; set; }
        public virtual int EstimatedTimeDepartureMax { get; set; }
        public virtual Freight Freight { get; set; }

        protected FreightItem() { }

        private FreightItem(Freight freight)
        {
            this.Freight = freight;
        }

        public FreightItem(Freight freight, Expedition expedition, decimal rate, int estimatedTimeDepartureMin, int estimatedTimeDepartureMax)
            :this(freight)
        {
            this.Expedition = expedition;
            this.Rate = rate;
            this.EstimatedTimeDepartureMin = estimatedTimeDepartureMin;
            this.EstimatedTimeDepartureMax = estimatedTimeDepartureMax;
        }

        public virtual string GetEstimatedTimeDepartureInString()
        {
            var now = DateTime.Now;
            var minTimeDeparture = now.AddDays(EstimatedTimeDepartureMin).ToString("dd MMM");
            var maxTimeDeparture = now.AddDays(EstimatedTimeDepartureMax).ToString("dd MMM");
            return string.Format("Akan diterima pada tanggal {0} - {1}", minTimeDeparture, maxTimeDeparture);
        }
    }
}
