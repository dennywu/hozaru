﻿using Hozaru.Core.Application.Features;
using Hozaru.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Identity.Application.Features
{
    /// <summary>
    /// Implementation of <see cref="IFeatureDependencyContext"/>.
    /// </summary>
    public class FeatureDependencyContext : IFeatureDependencyContext, ITransientDependency
    {
        /// <inheritdoc/>
        public IIocResolver IocResolver { get; private set; }

        /// <inheritdoc/>
        public IFeatureChecker FeatureChecker { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureDependencyContext"/> class.
        /// </summary>
        /// <param name="iocResolver">The ioc resolver.</param>
        /// <param name="featureChecker">The feature checker.</param>
        public FeatureDependencyContext(IIocResolver iocResolver, IFeatureChecker featureChecker)
        {
            IocResolver = iocResolver;
            FeatureChecker = featureChecker;
        }
    }
}
