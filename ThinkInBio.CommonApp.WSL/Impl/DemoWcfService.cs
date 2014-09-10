using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.CommonApp.WSL.Impl
{
    public class DemoWcfService : IDemoWcfService
    {
        public string Echo4Post(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException();
            }
            return string.Format("Hello {0}, when at {1}.", name, DateTime.Now);
        }

        public string Echo4Get(string name)
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
