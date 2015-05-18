using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TiQiu.DAL;

namespace TiQiu.Biz.Tests
{
    [TestClass]
    public class MemberManagerTest
    {
        [TestMethod]
        public void CreateMemberTest()
        {
            MEMBER m = MemberManager.CreateMember("admin", "12345678900");
            Assert.AreEqual("admin", m.NAME);
        }

        [TestMethod]
        public void BindAccountTest()
        {
            var acct = AccountManager.NewAccount("12345678900", "1");
            MEMBER m = MemberManager.CreateMember("test2", "12345678900");
            //MemberManager.BindAccount(m, acct.ID);
            
        }
        
    }
}
