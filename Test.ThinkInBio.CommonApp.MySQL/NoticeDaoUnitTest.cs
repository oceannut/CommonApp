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
    public class NoticeDaoUnitTest
    {

        private NoticeDao noticeDao;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            noticeDao = new NoticeDao(Configs.DataSource);
        }

        [TestCleanup()]
        public void MyTestCleanup() { }

        [TestMethod]
        public void TestMethod1()
        {
            Notice entity = new Notice();
            entity.Title = "测试";
            entity.Content = "测试";
            entity.Creator = "zsp";
            entity.Save((e) =>
            {
                noticeDao.Save(e);
            });
            Assert.IsTrue(entity.Id > 0);
            noticeDao.Delete(entity);
        }

    }
}
