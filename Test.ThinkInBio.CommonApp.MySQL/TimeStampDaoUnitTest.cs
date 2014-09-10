using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ThinkInBio.CommonApp;
using ThinkInBio.CommonApp.DAL;
using ThinkInBio.CommonApp.MySQL;

namespace Test.ThinkInBio.CommonApp.MySQL
{
    [TestClass]
    public class TimeStampDaoUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {

            TimeStampDao dao = new TimeStampDao(Configs.DataSource);
            DateTime? timeStamp = dao.Next();
            Console.WriteLine(timeStamp.Value);

        }
    }
}
