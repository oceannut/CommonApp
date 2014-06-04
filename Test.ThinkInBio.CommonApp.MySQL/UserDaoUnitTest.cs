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
    public class UserDaoUnitTest
    {

        private UserDao userDao;

        [TestInitialize()]
        public void MyTestInitialize() 
        {
            userDao = new UserDao(Configs.DataSource);
        }

        [TestCleanup()]
        public void MyTestCleanup() { }

        [TestMethod]
        public void TestMethod1()
        {
            User user = new User("temp", "temp", new PlainPasswordProvider());
            user.Save(
                (e) =>
                {
                    return userDao.IsExist("temp");
                },
                (e) =>
                {
                    userDao.Save(e);
                });
            bool isExisted = userDao.IsExist("temp");
            Assert.IsTrue(isExisted);
            userDao.Delete(user);
            isExisted = userDao.IsExist("temp");
            Assert.IsFalse(isExisted);
        }

        [TestMethod]
        public void TestMethod2()
        {
            IList<User> list = userDao.GetList();
            if (list != null && list.Count > 0)
            {
                foreach (User item in list)
                {
                    Console.WriteLine(item.Name);
                }
            }
        }

    }
}
