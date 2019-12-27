using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Hozaru.Core.Runtime.Validation
{
    /// <summary>
    /// This exception type is used to throws validation exceptions.
    /// </summary>
    [Serializable]
    public class HozaruValidationException : HozaruException
    {
        /// <summary>
        /// Detailed list of validation errors for this exception.
        /// </summary>
        public List<ValidationResult> ValidationErrors { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public HozaruValidationException()
        {
            ValidationErrors = new List<ValidationResult>();
        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public HozaruValidationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
            ValidationErrors = new List<ValidationResult>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        public HozaruValidationException(string message)
            : base(message)
        {
            ValidationErrors = new List<ValidationResult>();
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="validationErrors">Validation errors</param>
        public HozaruValidationException(string message, List<ValidationResult> validationErrors)
            : base(message)
        {
            ValidationErrors = validationErrors;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public HozaruValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
            ValidationErrors = new List<ValidationResult>();
        }
    }
}
