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
    public class CategoryDaoUnitTest
    {

        private CategoryDao categoryDao;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            categoryDao = new CategoryDao(Configs.DataSource);
        }

        [TestMethod]
        public void Demo4Activity()
        {
            Category entity = null;

            entity = new Category();
            entity.Code = "normal";
            entity.Name = "日常";
            entity.Scope = "activity";
            entity.Icon = "fa fa-bug";
            entity.Sequence = 0;
            entity.Save(null, (e) =>
            {
                categoryDao.Save(e);
            });

            entity = new Category();
            entity.Code = "finance";
            entity.Name = "财务";
            entity.Scope = "activity";
            entity.Icon = "fa fa-money";
            entity.Sequence = 1;
            entity.Save(null, (e) =>
            {
                categoryDao.Save(e);
            });

            entity = new Category();
            entity.Code = "transfer";
            entity.Name = "物流";
            entity.Scope = "activity";
            entity.Icon = "fa fa-truck";
            entity.Sequence = 2;
            entity.Save(null, (e) =>
            {
                categoryDao.Save(e);
            });

            entity = new Category();
            entity.Code = "shopping";
            entity.Name = "采购";
            entity.Scope = "activity";
            entity.Icon = "fa fa-shopping-cart";
            entity.Sequence = 3;
            entity.Save(null, (e) =>
            {
                categoryDao.Save(e);
            });

            entity = new Category();
            entity.Code = "travel";
            entity.Name = "差旅";
            entity.Scope = "activity";
            entity.Icon = "fa fa-plane";
            entity.Sequence = 4;
            entity.Save(null, (e) =>
            {
                categoryDao.Save(e);
            });

            entity = new Category();
            entity.Code = "exhib";
            entity.Name = "展会";
            entity.Scope = "activity";
            entity.Icon = "fa fa-institution";
            entity.Sequence = 5;
            entity.Save(null, (e) =>
            {
                categoryDao.Save(e);
            });

        }

        [TestMethod]
        public void Demo4Log()
        {
            Category entity = null;

            entity = new Category();
            entity.Code = "dairy";
            entity.Name = "日志";
            entity.Scope = "log";
            entity.Icon = "fa fa-tencent-weibo";
            entity.Sequence = 0;
            entity.Save(null, (e) =>
            {
                categoryDao.Save(e);
            });

            entity = new Category();
            entity.Code = "test";
            entity.Name = "实验";
            entity.Scope = "log";
            entity.Icon = "fa fa-flask";
            entity.Sequence = 1;
            entity.Save(null, (e) =>
            {
                categoryDao.Save(e);
            });

            entity = new Category();
            entity.Code = "production";
            entity.Name = "生产";
            entity.Scope = "log";
            entity.Icon = "fa fa-gears";
            entity.Sequence = 2;
            entity.Save(null, (e) =>
            {
                categoryDao.Save(e);
            });

            entity = new Category();
            entity.Code = "sale";
            entity.Name = "销售";
            entity.Scope = "log";
            entity.Icon = "fa fa-rmb";
            entity.Sequence = 3;
            entity.Save(null, (e) =>
            {
                categoryDao.Save(e);
            });

            entity = new Category();
            entity.Code = "meeting";
            entity.Name = "会议";
            entity.Scope = "log";
            entity.Icon = "fa fa-coffee";
            entity.Sequence = 4;
            entity.Save(null, (e) =>
            {
                categoryDao.Save(e);
            });

            entity = new Category();
            entity.Code = "memo";
            entity.Name = "备忘录";
            entity.Scope = "log";
            entity.Icon = "fa fa-list-alt";
            entity.Sequence = 5;
            entity.Save(null, (e) =>
            {
                categoryDao.Save(e);
            });

            entity = new Category();
            entity.Code = "ref";
            entity.Name = "引用";
            entity.Scope = "log";
            entity.Icon = "fa fa-quote-left";
            entity.Sequence = 6;
            entity.Save(null, (e) =>
            {
                categoryDao.Save(e);
            });

        }

        [TestMethod]
        public void TestMethod1()
        {
            Category entity = new Category();
            entity.Code = "test";
            entity.Name = "test";
            entity.Scope = "test";
            entity.Save(null, (e) =>
            {
                categoryDao.Save(e);
            });
            Assert.IsTrue(entity.Id > 0);

            Category entity2 = categoryDao.Get(entity.Id);
            Assert.IsTrue(entity.Equals(entity2));

            categoryDao.Delete(entity);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Category entity = new Category();
            entity.Code = "test";
            entity.Name = "test";
            entity.Scope = "test";
            entity.Save(null, (e) =>
            {
                categoryDao.Save(e);
            });
            Assert.IsTrue(entity.Id > 0);

            Category entity2 = new Category();
            entity2.Code = "test2";
            entity2.Name = "test2";
            entity2.Scope = "test";
            entity2.Save(null, (e) =>
            {
                categoryDao.Save(e);
            });
            Assert.IsTrue(entity2.Id > 0);

            Category entityGet = categoryDao.Get("test", "test2");
            Assert.AreEqual("test2", entityGet.Code);
            Assert.IsTrue(entity2.Equals(entityGet));

            IList<Category> list = categoryDao.GetList("test", null, null, null);
            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count);

            categoryDao.Delete(entity);
            categoryDao.Delete(entity2);
        }

        [TestMethod]
        public void TestMethod3()
        {
            Category entity = new Category();
            entity.Code = "test";
            entity.Name = "test";
            entity.Scope = "test";
            entity.Save(null, (e) =>
            {
                categoryDao.Save(e);
            });
            Assert.IsTrue(entity.Id > 0);

            Category entity2 = new Category();
            entity2.Code = "test2";
            entity2.Name = "test2";
            entity2.Scope = "test";
            entity2.ParentId = entity.Id;
            entity2.Save(null, (e) =>
            {
                categoryDao.Save(e);
            });
            Assert.IsTrue(entity2.Id > 0);

            IList<Category> list = categoryDao.GetList("test", null, null, null);
            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count);

            list = categoryDao.GetList("test", 0, null, null);
            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(entity, list[0]);

            list = categoryDao.GetList("test", entity.Id, null, null);
            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(entity2, list[0]);

            categoryDao.Delete(entity2);
            categoryDao.Delete(entity);
        }

    }
}
