using Hozaru.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Application.Features
{
    public class FeatureConfiguration : IFeatureConfiguration
    {
        public ITypeList<FeatureProvider> Providers { get; private set; }

        public FeatureConfiguration()
        {
            Providers = new TypeList<FeatureProvider>();
        }
    }
}
