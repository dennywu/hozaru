using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Auditing
{
    internal class AuditingConfiguration : IAuditingConfiguration
    {
        public bool IsEnabled { get; set; }

        public bool IsEnabledForAnonymousUsers { get; set; }

        //public IMvcControllersAuditingConfiguration MvcControllers { get; private set; }

        public IAuditingSelectorList Selectors { get; private set; }

        public AuditingConfiguration()
        {
            IsEnabled = true;
            IsEnabledForAnonymousUsers = false;
            Selectors = new AuditingSelectorList();
            //MvcControllers = new MvcControllersAuditingConfiguration();
        }
    }
}
