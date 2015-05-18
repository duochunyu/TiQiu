using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiQiu.Biz;
using TiQiu.DAL;
using System.Linq.Expressions;



namespace TiQiu.Biz.Tests
{
    [TestClass]
    public class FieldManagerTest
    {
        [TestMethod]
        public void CreateFieldTest()
        {
            string name = DateTime.Now.ToString("yyMMddHHmmss");
            var b = BusinessesManager.CreateBusinesses("test" + name, "asfsdf", 1);
            var f = FieldManager.CreateField("test" + name, "brief~!@#$%^&", "dadfadf", "100010",
            1, (decimal)300.00, (decimal)200.00, new TimeSpan(7, 30, 0), new TimeSpan(22, 0, 0), 4, 2,
            (float)32.09,(float)120.4344,true,b.ID);
            for (int i = 0; i < 5; i++)
            {
                var item = FieldManager.CreateField_Item("test","test" + name + i, 1, 1, f.ID);
                
            }
            int totalCount;
            Expression<Func<FIELD_ITEM, bool>> ex = PredicateExtensionses.True<FIELD_ITEM>();
            ex.And(s => s.FIELD_ID == f.ID);
            ex.And(s=>s.BRIEF.Contains("test"));
            List<FIELD_ITEM> fs = FieldManager.GetFieldItemList(ex, "BRIEF", false, 2, 2, out totalCount);
            foreach (FIELD_ITEM it in fs)
            {
                Console.WriteLine(it.BRIEF);
            }
            Assert.IsInstanceOfType(f, typeof(FIELD));
        }
        [TestMethod]
        public void GetTeamListTest()
        {
             int totalCount;
             Expression<Func<TEAM, bool>> ex = PredicateExtensionses.True<TEAM>();
            ex.And(s => s.TEAM_MEMBER.OfType<TEAM_MEMBER>().All(t=>t.MEMBER.NAME.Contains("test")));

            List<TEAM> ts = TeamManager.GetTeamByMemberId(0);
          //  List<team> ts = TeamManager.GetTeamList(ex, "TEAM.ID", false, 2, 2, out totalCount);
            foreach (TEAM it in ts)
            {
                Console.WriteLine(it.ID);
            }
        }

        [TestMethod]
        public void GetFieldListTest()
        {

            List<FIELD> ts = FieldManager.GetFieldList(2);
            //  List<team> ts = TeamManager.GetTeamList(ex, "TEAM.ID", false, 2, 2, out totalCount);
            foreach (FIELD it in ts)
            {
                Console.WriteLine(it.ID);
            }
        }

        [TestMethod]
        public void GetFieldScheduledInfoListTest()
        {

            //List<FIELD> ts = FieldManager.GetFieldScheduledInfoList();
            //List<team> ts = TeamManager.GetTeamList(ex, "TEAM.ID", false, 2, 2, out totalCount);
            //foreach (FIELD it in ts)
            //{
            //    Console.WriteLine(it.ID);
            //}
        }
    }
}
