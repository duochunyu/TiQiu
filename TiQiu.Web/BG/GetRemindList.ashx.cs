using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TiQiu.Biz;
using TiQiu.DAL;

namespace TiQiu.Web.BG
{
    /// <summary>
    /// Summary description for GetRemindList
    /// </summary>
    public class GetRemindList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            
            Expression<Func<V_FIELD_ORDER, bool>> express = PredicateExtensionses.True<V_FIELD_ORDER>();
            //express = express.And(s => s.BUSINESSES_ID == BusinessID);
            express = express.And(s => s.STATUS ==(int) EnumFieldStatus.Booking);
            int totalCount = 0;
            var list = OrderManager.GetOrderViewList(express, "ID", false, 1, 999, out totalCount);
            if (list!=null)
            {
                context.Response.Write(list.Count+1);
            }
            else
            {
                context.Response.Write(1);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}