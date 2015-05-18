using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using TiQiu.Biz;

namespace TiQiu.WebServcie
{
    /// <summary>
    /// GetFileHandler 的摘要说明
    /// </summary>
    public class GetFileHandler : IHttpHandler
    {
        private static string FILE_DOMAIN = ConfigurationManager.AppSettings["FILE_DOMAIN"];

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string type = Util.GetParamter<string>(context.Request, "type");
            int fkId = Util.GetParamter<int>(context.Request, "fkId");
            int memberId = Util.GetParamter<int>(context.Request, "member", false);
            string url = string.Empty;
            switch (type.ToUpper())
            {
                case "PORTRAIT":
                    url = FileManager.GetMemberPic(fkId);
                    break;
                case "TEAMLOGO":
                    url = FileManager.GetTeamLogo(fkId);
                    break;
                default:
                    url = FILE_DOMAIN + "default.png";
                    break;
            }
            //context.Response.Clear();
            context.Response.Redirect(url,true);
            //context.RewritePath(url, false);
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