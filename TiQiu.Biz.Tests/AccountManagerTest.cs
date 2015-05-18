using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TiQiu.Biz;
using TiQiu.DAL;


namespace TiQiu.Biz.Tests
{
    [TestClass]
    public class AccountManagerTest
    {
        [TestMethod]
        public void LoingTest()
        {
            AccountManager.Login("admin", "123");
        }

        [TestMethod]
        public void NewAccountTest()
        {
            var acct = AccountManager.NewAccount("1232435", "1");
            Assert.AreEqual("1232435", acct.NAME);
        }

        [TestMethod]
        public void NewAccountBTest()
        {
            var acct = AccountManager.NewAccountB("2", "1",2);
            Assert.AreEqual("test", acct.NAME);
        }
    }
}
