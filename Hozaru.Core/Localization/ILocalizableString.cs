﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Hozaru.Core.Localization
{
    /// <summary>
    /// Represents a string that can be localized when needed.
    /// </summary>
    public interface ILocalizableString
    {
        /// <summary>
        /// Localizes the string in current culture.
        /// </summary>
        /// <returns>Localized string</returns>
        string Localize();

        /// <summary>
        /// Localizes the string in given culture.
        /// </summary>
        /// <param name="culture">culture</param>
        /// <returns>Localized string</returns>
        string Localize(CultureInfo culture);
    }
}
