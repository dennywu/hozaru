﻿using Hozaru.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Application.Features
{
    /// <summary>
    /// Used in <see cref="IFeatureDependency.IsSatisfiedAsync"/> method.
    /// </summary>
    public interface IFeatureDependencyContext
    {
        /// <summary>
        /// Gets the <see cref="IIocResolver"/>.
        /// </summary>
        /// <value>
        /// The ioc resolver.
        /// </value>
        IIocResolver IocResolver { get; }

        /// <summary>
        /// Gets the <see cref="IFeatureChecker"/>.
        /// </summary>
        /// <value>
        /// The feature checker.
        /// </value>
        IFeatureChecker FeatureChecker { get; }
    }
}
