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
    /// FileHandler 的摘要说明
    /// </summary>
    public class FileHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            HttpProcessor apj = new HttpProcessor();
            apj.ActionHandlerMaps.Add("GetIndexSliderList", GetIndexSliderList);            
            apj.ProcessRequestHandler<ApplicationException>(context, ex => Log.WriteException(ex));
        }

        private void GetIndexSliderList(HttpContext context, ref object userData)
        {
            var clientType = Util.GetParamter<int>(context.Request, "clientType", false);

            userData = ModelMapping.InitFileList(FileManager.GetIndexSliderList());
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