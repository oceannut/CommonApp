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
    public class IdiomDaoUnitTest
    {

        private IdiomDao idiomDao;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            idiomDao = new IdiomDao(Configs.DataSource);
        }

        [TestMethod]
        public void TestMethod1()
        {
            Idiom entity = new Idiom();
            entity.Scope = "test";
            entity.Content = "测试";
            entity.Save((e) =>
            {
                idiomDao.Save(e);
            });
            Assert.IsTrue(entity.Id > 0);
            Assert.AreEqual("cs", entity.Spell);
            idiomDao.Delete(entity);
        }

    }
}
