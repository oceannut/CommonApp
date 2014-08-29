using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Scheduling
{
    public class MockExceptionJob : GenericJob
    {
        protected override void Execute()
        {
            System.Threading.Thread.Sleep(1000);
            throw new Exception("Throws the exception when scheduling a job.");
        }
    }
}
