using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TiQiu.Biz;
using TiQiu.Web.Model;

namespace TiQiu.Web.AjaxHandler
{
    /// <summary>
    /// GetMemberInfos 的摘要说明
    /// </summary>
    public class updateMemberAvatr : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int id = int.Parse(context.Request.QueryString["id"]);

            FileManager.UpdateMeberPic(EnumFileType.Member_Portrait, id, context.Request.QueryString["file"]);
            context.Response.End();
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