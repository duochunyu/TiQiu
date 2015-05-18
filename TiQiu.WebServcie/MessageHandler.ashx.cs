using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using TiQiu.Biz;
using TiQiu.Model;
namespace TiQiu.WebServcie
{
    /// <summary>
    /// MessageHandler 的摘要说明
    /// </summary>
    public class MessageHandler : IHttpHandler, IRequiresSessionState
    {
        HttpProcessor apj;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            apj = new HttpProcessor();
            apj.ActionHandlerMaps.Add("GetOrderMessage", GetOrderMessage);
            apj.ActionHandlerMaps.Add("CheckMessage", CheckMessage);
            apj.ActionWithManager.Add("GetOrderMessage");
            apj.ActionWithManager.Add("CheckMessage");


            apj.ProcessRequestHandler<ApplicationException>(context, ex => Log.WriteException(ex));
        }

        private void GetOrderMessage(HttpContext context, ref object userData)
        {
            
            //string[] ids = fields.Split(new string[]{","},StringSplitOptions.RemoveEmptyEntries);
            
            var stamp = CacheManager.GetTimeStamp(apj.CurToken);
            var list = CacheManager.GetFieldIds(apj.CurToken);
            userData = ModelMapping.InitOrderLogList(OrderManager.GetOrderLog(stamp, list));
            
        }

        private void CheckMessage(HttpContext context, ref object userData)
        {
            var id = Util.GetParamter<int>(context.Request, "msgid");
            OrderManager.CheckMessage(id);
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