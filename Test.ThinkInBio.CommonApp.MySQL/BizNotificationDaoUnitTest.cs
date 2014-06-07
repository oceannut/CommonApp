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
    public class BizNotificationDaoUnitTest
    {

        private BizNotificationDao bizNotificationDao;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            bizNotificationDao = new BizNotificationDao(Configs.DataSource);
        }

        [TestCleanup()]
        public void MyTestCleanup() { }

        [TestMethod]
        public void TestMethod1()
        {
            BizNotification entity = new BizNotification("zsp", "lj");
            entity.Resource = "test";
            entity.ResourceId = "1";
            entity.Send((e) =>
            {
                bizNotificationDao.Save((BizNotification)e);
            });
            Assert.IsTrue(entity.Id > 0);
            Assert.IsFalse(entity.Review.HasValue);
            entity.Receive("lj",
                (e) =>
                {
                    bizNotificationDao.Update((BizNotification)e);
                });
            Assert.IsTrue(entity.Review.HasValue);
            bizNotificationDao.Delete(entity);
        }

        [TestMethod]
        public void TestMethod2()
        {
            BizNotification entity1 = new BizNotification("zsp", "lj");
            entity1.Resource = "test";
            entity1.ResourceId = "1";
            entity1.Send((e) =>
            {
                bizNotificationDao.Save((BizNotification)e);
            });
            BizNotification entity2 = new BizNotification("zsp", "lj");
            entity2.Resource = "test";
            entity2.ResourceId = "2";
            entity2.Send((e) =>
            {
                bizNotificationDao.Save((BizNotification)e);
            });
            BizNotification entity3 = new BizNotification("zsp", "lj");
            entity3.Resource = "test";
            entity3.ResourceId = "3";
            entity3.Send((e) =>
            {
                bizNotificationDao.Save((BizNotification)e);
            });

            int count = bizNotificationDao.GetCount(null, null, null, null, null, null);
            Assert.AreEqual(3, count);
            IList<BizNotification> list = bizNotificationDao.GetList(null, null, null, null, null, null, true, 0, 2);
            Assert.AreEqual(2, list.Count);
            list = bizNotificationDao.GetList(null, null, null, null, null, null, true, 2, 2);
            Assert.AreEqual(1, list.Count);

            count = bizNotificationDao.GetCount(null, null, null, "zsp", null, null);
            Assert.AreEqual(3, count);
            list = bizNotificationDao.GetList(null, null, null, "zsp", null, null, true, 0, 2);
            Assert.AreEqual(2, list.Count);
            list = bizNotificationDao.GetList(null, null, null, "zsp", null, null, true, 2, 2);
            Assert.AreEqual(1, list.Count);

            count = bizNotificationDao.GetCount(null, null, null, "zsp", "lj", null);
            Assert.AreEqual(3, count);
            list = bizNotificationDao.GetList(null, null, null, "zsp", "lj", null, true, 0, 2);
            Assert.AreEqual(2, list.Count);
            list = bizNotificationDao.GetList(null, null, null, "zsp", "lj", null, true, 2, 2);
            Assert.AreEqual(1, list.Count);

            count = bizNotificationDao.GetCount(null, null, null, "zsp", "ljj", null);
            Assert.AreEqual(0, count);
            list = bizNotificationDao.GetList(null, null, null, "zsp", "ljj", null, true, 0, 2);
            Assert.AreEqual(0, list.Count);

            count = bizNotificationDao.GetCount(null, null, false, "zsp", "lj", null);
            Assert.AreEqual(3, count);
            list = bizNotificationDao.GetList(null, null, false, "zsp", "lj", null, true, 0, 2);
            Assert.AreEqual(2, list.Count);

            count = bizNotificationDao.GetCount(null, null, true, "zsp", "lj", null);
            Assert.AreEqual(0, count);
            list = bizNotificationDao.GetList(null, null, true, "zsp", "lj", null, true, 0, 2);
            Assert.AreEqual(0, list.Count);

            bizNotificationDao.Delete(entity1);
            bizNotificationDao.Delete(entity2);
            bizNotificationDao.Delete(entity3);
        }

    }
}
