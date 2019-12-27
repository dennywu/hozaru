using Hozaru.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Domain
{
    public class EstimatedTimeDelivery
    {
        public int EstimatedTimeDeliveryMin { get; private set; }
        public int EstimatedTimeDeliveryMax { get; private set; }

        protected EstimatedTimeDelivery() { }

        /// <summary>
        /// param format "1-1"
        /// </summary>
        /// <param name="estimatedTimeDelivery">param format "1-1"</param>
        public EstimatedTimeDelivery(string estimatedTimeDelivery)
        {
            if (estimatedTimeDelivery.Contains("-"))
            {
                var estimatedDeliverySplited = estimatedTimeDelivery.Split("-");

                EstimatedTimeDeliveryMin = Convert.ToInt32(estimatedDeliverySplited[0]);
                EstimatedTimeDeliveryMax = Convert.ToInt32(estimatedDeliverySplited[1]);
            }
            else if (estimatedTimeDelivery.Length == 1)
            {
                EstimatedTimeDeliveryMin = Convert.ToInt32(estimatedTimeDelivery);
                EstimatedTimeDeliveryMax = Convert.ToInt32(estimatedTimeDelivery);
            }
            else
            {
                EstimatedTimeDeliveryMin = 1;
                EstimatedTimeDeliveryMax = 1;
            }
        }

        public string GetEstimatedTimeDeliverySentence(DateTime dateTime)
        {
            var minTimeDeparture = dateTime.AddDays(EstimatedTimeDeliveryMin).ToString("dd MMM");
            var maxTimeDeparture = dateTime.AddDays(EstimatedTimeDeliveryMax).ToString("dd MMM");
            return string.Format("Akan diterima pada tanggal {0} - {1}", minTimeDeparture, maxTimeDeparture);
        }
    }
}
