using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Hozaru.Core
{
    /// <summary>
    /// This exception is thrown if a problem on Hozaru initialization progress.
    /// </summary>
    [Serializable]
    public class HozaruInitializationException : HozaruException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public HozaruInitializationException()
        {

        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public HozaruInitializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        public HozaruInitializationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public HozaruInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
