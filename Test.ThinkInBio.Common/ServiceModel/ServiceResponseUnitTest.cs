using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ThinkInBio.Common.ServiceModel;

namespace Test.ThinkInBio.Common.ServiceModel
{
    [TestClass]
    public class ServiceResponseUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {

            ServiceResponse response1 = ServiceResponse.BuildNormal();
            Assert.IsNotNull(response1);
            Assert.AreEqual(ServiceResponseCode.Normal, response1.Code);
            Assert.IsNull(response1.Message);

            ServiceResponse response2 = ServiceResponse.Build(ServiceResponseCode.Normal, "Hello World");
            Assert.IsNotNull(response2);
            Assert.AreEqual(ServiceResponseCode.Normal, response2.Code);
            Assert.AreEqual("Hello World", response2.Message);

            ServiceResponse<string> response3 = ServiceResponse<string>.BuildResult("Hello World");
            Assert.IsNotNull(response3);
            Assert.AreEqual(ServiceResponseCode.Normal, response3.Code);
            Assert.IsNull(response3.Message);
            Assert.AreEqual("Hello World", response3.Result);

            ServiceResponse<string> response4 = ServiceResponse<string>.Build(ServiceResponseCode.Normal, "Hello World");
            Assert.IsNotNull(response4);
            Assert.AreEqual(ServiceResponseCode.Normal, response4.Code);
            Assert.AreEqual("Hello World", response4.Message);
            Assert.IsNull(response4.Result);

        }
    }
}
