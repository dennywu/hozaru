using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Auditing
{
    /// <summary>
    /// Null implementation of <see cref="IAuditInfoProvider"/>.
    /// </summary>
    internal class NullAuditInfoProvider : IAuditInfoProvider
    {
        public void Fill(AuditInfo auditInfo)
        {

        }
    }
}
