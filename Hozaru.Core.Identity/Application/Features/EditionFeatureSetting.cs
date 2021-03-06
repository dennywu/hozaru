﻿using Hozaru.Core.Application.Editions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Identity.Application.Features
{
    /// <summary>
    /// Feature setting for an <see cref="Edition"/>.
    /// </summary>
    public class EditionFeatureSetting : FeatureSetting
    {
        /// <summary>
        /// Gets or sets the edition.
        /// </summary>
        /// <value>
        /// The edition.
        /// </value>
        public virtual Edition Edition { get; set; }

        /// <summary>
        /// Gets or sets the edition Id.
        /// </summary>
        /// <value>
        /// The edition Id.
        /// </value>
        public virtual int EditionId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditionFeatureSetting"/> class.
        /// </summary>
        public EditionFeatureSetting()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditionFeatureSetting"/> class.
        /// </summary>
        /// <param name="editionId">The edition Id.</param>
        /// <param name="name">Feature name.</param>
        /// <param name="value">Feature value.</param>
        public EditionFeatureSetting(int editionId, string name, string value)
            : base(name, value)
        {
            EditionId = editionId;
        }
    }
}
