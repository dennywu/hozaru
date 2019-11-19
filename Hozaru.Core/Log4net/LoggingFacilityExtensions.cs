using Castle.Facilities.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Log4net
{
    public static class LoggingFacilityExtensions
    {
        public static LoggingFacility UseHozaruLog4Net(this LoggingFacility loggingFacility)
        {
            return loggingFacility.LogUsing<Log4NetLoggerFactory>();
        }
    }
}
