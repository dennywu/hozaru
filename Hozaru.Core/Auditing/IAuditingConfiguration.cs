﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Auditing
{
    /// <summary>
    /// Used to configure auditing.
    /// </summary>
    public interface IAuditingConfiguration
    {
        /// <summary>
        /// Used to enable/disable auditing system.
        /// Default: true. Set false to completely disable it.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Set true to enable saving audit logs if current user is not logged in.
        /// Default: false.
        /// </summary>
        bool IsEnabledForAnonymousUsers { get; set; }
        
        /// <summary>
        /// </summary>
        IAuditingSelectorList Selectors { get; }
    }
}
