﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Configurations
{
    /// <summary>
    /// Represents value of a setting.
    /// </summary>
    public interface ISettingValue
    {
        /// <summary>
        /// Unique name of the setting.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Value of the setting.
        /// </summary>
        string Value { get; }
    }
}
