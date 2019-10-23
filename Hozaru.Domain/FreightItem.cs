using Hozaru.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class FreightItem : Entity<Guid>
    {
        public Expedition Expedition { get; set; }
        public decimal Rate { get; set; }
        public int EstimatedTimeDeparture { get; set; }

        public FreightItem(Expedition expedition, decimal rate, int estimatedTimeDeparture)
        {
            this.Expedition = expedition;
            this.Rate = rate;
            this.EstimatedTimeDeparture = estimatedTimeDeparture;
        }

        public string GetEstimatedTimeDepartureInString()
        {
            var now = DateTime.Now;
            var minTimeDeparture = now.AddDays(EstimatedTimeDeparture).ToString("dd MMM");
            var maxTimeDeparture = now.AddDays(EstimatedTimeDeparture + 4).ToString("dd MMM");
            return string.Format("Akan diterima pada tanggal {0} - {1}", minTimeDeparture, maxTimeDeparture);
        }
    }
}
