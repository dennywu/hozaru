using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Identity.Application.Editions
{
    [Serializable]
    public class EditionfeatureCacheItem
    {
        public const string CacheStoreName = "HozaruIdentityEditionFeatures";

        public IDictionary<string, string> FeatureValues { get; set; }

        public EditionfeatureCacheItem()
        {
            FeatureValues = new Dictionary<string, string>();
        }
    }
}
