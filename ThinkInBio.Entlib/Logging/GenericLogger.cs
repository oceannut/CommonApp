using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;

using ThinkInBio.Common.Logging;

namespace ThinkInBio.Entlib.Logging
{

    public class GenericLogger : ILogger
    {

        internal string LogCategory { get; set; }

        private static LogWriter logWriter = EnterpriseLibraryContainer.Current.GetInstance<LogWriter>();

        public void Log(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(LogCategory))
            {
                logWriter.Write(message);
            }
            else
            {
                logWriter.Write(message, LogCategory);
            }
        }

    }

}
