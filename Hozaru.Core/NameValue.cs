using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core
{
    /// <summary>
    /// Can be used to store Name/Value (or Key/Value) pairs.
    /// </summary>
    [Serializable]
    public class NameValue
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Creates a new <see cref="NameValue"/>.
        /// </summary>
        public NameValue()
        {

        }

        /// <summary>
        /// Creates a new <see cref="NameValue"/>.
        /// </summary>
        public NameValue(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
