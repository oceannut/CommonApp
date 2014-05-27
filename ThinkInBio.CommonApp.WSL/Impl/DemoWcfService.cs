using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp.WSL.Impl
{
    public class DemoWcfService : IDemoWcfService
    {
        public string Echo(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException();
            }
            return string.Format("Hello {0}", name);
        }

        public string GetServicePath()
        {
            return Environment.CurrentDirectory;
        }
    }
}
