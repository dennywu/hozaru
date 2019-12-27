using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Domain.Entities
{
    public interface IMayHaveTenant
    {
        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        int? TenantId { get; set; }
    }
}
