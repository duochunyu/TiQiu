using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TiQiu.WebServcie
{

    /// <summary>
    /// Ajax请求在服务端的处理方法代理
    /// </summary>
    /// <param name="context">Http请求上下文</param>
    /// <param name="userData">如果需要有返回数据让客户端做回调处理，请申明方法变量userData，web端将通过userData来获取返回数据</param>
    public delegate void ActionRequestHandler(HttpContext context, ref object userData);

    

    public class HttpProcessor
    {
        /// <summary>
        /// Ajax请求ActionType与对应处理方法的字典表，key为actionType，value为处理方法
        /// </summary>
        public Dictionary<string, ActionRequestHandler> ActionHandlerMaps = new Dictionary<string, ActionRequestHandler>();
        public List<string> ActionWithMember = new List<string>();
        public List<string> ActionWithManager = new List<string>();
        public string CurToken;

        private void ReturnError(HttpContext context, string msg,int errorCode = 0)
        {
            HttpResponse response = context.Response;
            response.Clear();
            response.Write(JsonConvert.SerializeObject(new Message { Result = errorCode, HelpMessage = msg }));
            response.End();
        }

        /// <summary>
        /// 服务端处理Ajax请求的通用方法，注意：web端ajax请求的js中action名必须并且只能为"action"
        /// </summary>
        /// <typeparam name="BE">自定义的业务异常类型</typeparam>
        /// <param name="context">Http请求上下文</param>
        /// <param name="exceptionHandler">对于系统异常的处理方法，如果为null，则不提供额外的处理（如记录Log等）</param>
        public void ProcessRequestHandler<BE>(HttpContext context, Action<Exception> exceptionHandler) where BE : Exception
        {
            Log.WriteBizLog(string.Format("Platform:{0}-Query:{1}", context.Request.Browser.Platform, context.Request.QueryString));
            context.Response.ContentType = "application/json";
            HttpRequest Request = context.Request;
            HttpResponse Response = context.Response;

            string action = Request.Form["action"] ?? Request.QueryString["action"];
            if (action == null || action.Trim().Length == 0)
            {
                ReturnError(context, "未定义数据处理请求的action参数！");
                
            }
            action = action.Trim();

            if ((ActionWithMember.Contains<string>(action) && !ValidateToken(context, false))
                || (ActionWithManager.Contains<string>(action) && !ValidateToken(context, true)))
            {
                ReturnError(context, "当前未登录或Token失效，请重新登录！",999);
                
            }
            

            if (!ActionHandlerMaps.Keys.Contains<string>(action))
            {
                if (action.ToUpper() == "DIR")
                {
                    string method,withMember, withManager;
                    method = withManager = withMember = string.Empty;
                    ActionHandlerMaps.ToList().ForEach(a => method += string.Format("{0} ", a.Key));
                    ActionWithMember.ForEach(m => withMember += string.Format(" {0} ", m));
                    ActionWithManager.ForEach(m => withMember += string.Format(" {0} ", m));
                    ReturnError(context, "[Action List]: " + method + " [Member]: " + withMember + " [Manager]: " + withManager);
                    
                }else
                    ReturnError(context, "对于请求为" + action + "的数据处理请求，请先在ActionHandlerMaps中加入action及对应的处理方法！");
            }

            if (ActionHandlerMaps[action] == null)
            {
                ReturnError(context, "对于请求为" + action + "的数据处理请求，服务端没有定义对应的处理方法！");
            }

            

            try
            {
                ExecActionHandler(context, action);
            }
            catch (BE be)
            {
                ReturnError(context, be.Message);
            }
            catch (Exception ex)
            {
                if (exceptionHandler != null)
                {
                    exceptionHandler(ex);
                }
                ReturnError(context, "服务器端发生异常，请重试或联系管理员！");
            }
        }

        private bool ValidateToken(HttpContext context, bool isAccount)
        {
            CurToken = context.Request.Form["token"] ?? context.Request.QueryString["token"];
            if (string.IsNullOrEmpty(CurToken))
            {
                CurToken = context.Request.Cookies["token"] != null ? context.Request.Cookies["token"].ToString() : string.Empty;
            }
            CurToken = CurToken.Trim();
            return CacheManager.CheckToken(CurToken, isAccount);
            //var tk = CacheManager.GetCache<TokenEntity>(token);
            //if (tk == null || tk.IsAccount != isAccount)
            //{               
            //    return false;
            //}
            //return true;

        }
        private void ExecActionHandler(HttpContext context, string action)
        {
            ActionRequestHandler handler = ActionHandlerMaps[action];
            HttpResponse response = context.Response;
            object userData = null;
            var sets = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};
            handler(context, ref userData);
           
            response.Write(JsonConvert.SerializeObject(new Message
            {
                Result = 1,
                Data = userData
            }, sets));
            
        }
    }
}