using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiQiu.DAL;

namespace TiQiu.Biz
{
    public static class BusinessesManager
    {
        public static BUSINESSES CreateBusinesses(string name, string brief, int level)
        {
            BUSINESSES b;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                b = new BUSINESSES() { NAME = name, STATUS = 1, BRIEF = brief, LEVEL = level };
                context.BUSINESSES.Add(b);
                context.SaveChanges();
            }
            return b;
        }

        public static void UpdateBusinesses(int id,string name, string brief, int level,int status)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {
                BUSINESSES b = context.BUSINESSES.Single(a => a.ID == id);
                b.NAME = name;
                b.BRIEF = brief;
                b.STATUS = status;
                b.LEVEL = level;
                context.SaveChanges();
            }
        }

        public static List<BUSINESSES> GetBusinessList(System.Linq.Expressions.Expression<Func<BUSINESSES, bool>> condition, string orderBy, bool ascending, int pageIdx, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<BUSINESSES> rel = new List<BUSINESSES>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = PagingQuery<BUSINESSES>.GetPagingList(context.BUSINESSES.AsQueryable(), condition, orderBy, ascending, pageIdx, pageSize, out totalCount);
           
            }
            return rel;
        }
    }
}
