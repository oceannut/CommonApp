using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using S = ThinkInBio.Scheduling;

namespace Test.ThinkInBio.Scheduling
{

    internal class SimpleJob : S.GenericJob
    {

        protected override void Execute()
        {
            Console.WriteLine("Hello World once again!");
        }

    }

}
