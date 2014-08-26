using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

using Quartz;
using Quartz.Impl;

using ThinkInBio.Scheduling.Quartz;

namespace Test.ThinkInBio.Scheduling.Quartz
{
    [TestClass]
    public class QuartzScheduleUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

            SimpleJob job = new SimpleJob();

            QuartzSchedule schedule = new QuartzSchedule(schedulerFactory);
            Assert.AreEqual(0, schedule.RepeatSeconds);
            Assert.AreEqual(0, schedule.RepeatCount);
            Assert.AreEqual(null, schedule.Expression);

            Console.WriteLine("================start=================");

            Task.Factory.StartNew(() =>
            {
                schedule.Start(job);
            });
            Thread.Sleep(5000);
            schedule.Stop();

            Console.WriteLine("================end=================");
            Console.WriteLine("\n");

        }

        [TestMethod]
        public void TestMethod2()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

            SimpleJob job = new SimpleJob();

            QuartzSchedule schedule = new QuartzSchedule(schedulerFactory, 1);
            Assert.AreEqual(1, schedule.RepeatSeconds);
            Assert.AreEqual(0, schedule.RepeatCount);
            Assert.AreEqual(null, schedule.Expression);

            Console.WriteLine("================start=================");

            Task.Factory.StartNew(() =>
            {
                schedule.Start(job);
            });
            Thread.Sleep(5000);
            schedule.Stop();

            Console.WriteLine("================end=================");
            Console.WriteLine("\n");

        }

        [TestMethod]
        public void TestMethod3()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

            SimpleJob job = new SimpleJob();

            QuartzSchedule schedule = new QuartzSchedule(schedulerFactory, 1, 2);
            Assert.AreEqual(1, schedule.RepeatSeconds);
            Assert.AreEqual(2, schedule.RepeatCount);
            Assert.AreEqual(null, schedule.Expression);

            Console.WriteLine("================start=================");

            Task.Factory.StartNew(() =>
            {
                schedule.Start(job);
            });
            Thread.Sleep(5000);
            schedule.Stop();

            Console.WriteLine("================end=================");
            Console.WriteLine("\n");

        }

        [TestMethod]
        public void TestMethod4()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

            SimpleJob job = new SimpleJob();

            QuartzSchedule schedule = new QuartzSchedule(schedulerFactory, "0/2 * * * * ?");
            Assert.AreEqual(0, schedule.RepeatSeconds);
            Assert.AreEqual(0, schedule.RepeatCount);
            Assert.AreEqual("0/2 * * * * ?", schedule.Expression);

            Console.WriteLine("================start=================");

            Task.Factory.StartNew(() =>
            {
                schedule.Start(job);
            });
            Thread.Sleep(5000);
            schedule.Stop();

            Console.WriteLine("================end=================");
            Console.WriteLine("\n");

        }

        [TestMethod]
        public void TestMethod5()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

            SimpleJob job = new SimpleJob();
            job.Running += () =>
            {
                Console.WriteLine("before job run");
            };
            job.Completed += () =>
            {
                Console.WriteLine("after job run");
            };

            QuartzSchedule schedule = new QuartzSchedule(schedulerFactory, "0/2 * * * * ?");
            Assert.AreEqual(0, schedule.RepeatSeconds);
            Assert.AreEqual(0, schedule.RepeatCount);
            Assert.AreEqual("0/2 * * * * ?", schedule.Expression);

            Console.WriteLine("================start=================");

            Task.Factory.StartNew(() =>
            {
                schedule.Start(job);
            });
            Thread.Sleep(10000);
            schedule.Stop();

            Console.WriteLine("================end=================");
            Console.WriteLine("\n");

        }

    }
}
