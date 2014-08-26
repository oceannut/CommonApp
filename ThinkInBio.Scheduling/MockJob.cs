using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThinkInBio.Common.Logging;

namespace ThinkInBio.Scheduling
{
    public class MockJob : GenericJob
    {

        internal string Name { get; set; }
        internal ILogger Logger { get; set; }

        protected override void Execute()
        {
            string name = string.IsNullOrWhiteSpace(Name) ? "MockJob" : Name;
            Logger.Log(string.Format("{0} is beign running.", name));
            System.Threading.Thread.Sleep(5000);
            Logger.Log(string.Format("{0} is finished after 5 seconds computing.", name));
        }

    }
}
