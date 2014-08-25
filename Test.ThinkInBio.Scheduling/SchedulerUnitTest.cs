using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

using S = ThinkInBio.Scheduling;

namespace Test.ThinkInBio.Scheduling
{
    [TestClass]
    public class SchedulerUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine("================start=================");

            SimpleJob job= new SimpleJob();
            job.Running += () =>
            {
                Console.WriteLine("before job run");
            };
            job.Completed += () =>
            {
                Console.WriteLine("after job run");
            };
            S.Scheduler scheduler = new S.Scheduler(new SimpleSchedule(1, job), job);
            scheduler.StateChanged += (e) =>
            {
                Console.WriteLine("current schedule state: " + e);
            };

            int count = 0;

            Task.Factory.StartNew(() =>
            {
                scheduler.Start();
            });

            while (count < 4)
            {
                System.Threading.Thread.Sleep(1000);
                count++;
            }

            scheduler.Stop();

            Console.WriteLine("start: " + scheduler.LastStartTime + "  end: " + scheduler.LastStopTime);

            Console.WriteLine("================end=================");
            Console.WriteLine("\n");

        }

        

    }
}
