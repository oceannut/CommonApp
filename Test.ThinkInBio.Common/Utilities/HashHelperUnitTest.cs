using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ThinkInBio.Common.Utilities;

namespace Test.ThinkInBio.Common.Utilities
{
    [TestClass]
    public class HashHelperUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {

            string s = "Hello World";
            string t = HashHelper.Encrypt(s);
            Console.WriteLine(t);
            Assert.AreNotEqual(s, t);
            string t2 = HashHelper.Encrypt(s);
            Assert.AreEqual(t, t2);
            string s2 = "Hello Zsp";
            string t3 = HashHelper.Encrypt(s2);
            Console.WriteLine(t3);
            Assert.AreNotEqual(t, t3);
            Console.WriteLine("=======================================");
        }
    }
}
