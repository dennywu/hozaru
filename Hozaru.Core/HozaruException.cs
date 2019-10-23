using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Hozaru.Core
{
    [Serializable]
    public class HozaruException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="HozaruException"/> object.
        /// </summary>
        public HozaruException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="HozaruException"/> object.
        /// </summary>
        public HozaruException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="HozaruException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public HozaruException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="HozaruException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public HozaruException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
