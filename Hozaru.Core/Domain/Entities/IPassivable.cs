using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Domain.Entities
{
    public interface IPassivable
    {
        /// <summary>
        /// True: This entity is active.
        /// False: This entity is not active.
        /// </summary>
        bool IsActive { get; set; }
    }
}
