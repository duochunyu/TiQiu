using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TiQiu.Biz;
using TiQiu.DAL;

namespace TiQiu.WebServcie
{
    /// <summary>
    /// CommHandler 的摘要说明
    /// </summary>
    public class CommHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            HttpProcessor apj = new HttpProcessor();
            apj.ActionHandlerMaps.Add("GetAreaList", GetAreaList);
            apj.ActionHandlerMaps.Add("GetFieldType", GetFieldType);
            apj.ActionHandlerMaps.Add("GetTeamMemberRoleList", GetTeamMemberRoleList);
            apj.ActionHandlerMaps.Add("GetFavFootList", GetFavFootList);
            apj.ActionHandlerMaps.Add("CheckVersion",CheckVersion);
            apj.ProcessRequestHandler<ApplicationException>(context, ex => Log.WriteException(ex));
        }

        private void CheckVersion(HttpContext context, ref object userData)
        {
            var os = Util.GetParamter<int>(context.Request, "os", false);
            using (TIQIUEntities tiqiu = new TIQIUEntities())
            {
                userData =
                    tiqiu.VERSION.Where(v => v.OS == os).OrderByDescending(v => v.ReleaseDate).FirstOrDefault();
                
            }

        }

        private void GetAreaList(HttpContext context, ref object userData)
        {
            var needAll = Util.GetParamter<int>(context.Request,"needall",false);

            userData = ConstValue.GetAreaList(needAll == 1 ? true : false);
        }

        private void GetFavFootList(HttpContext context, ref object userData)
        {
            var needAll = Util.GetParamter<int>(context.Request, "needall", false);
            var list = new List<KeyValuePair<int,string>>();
            list.Add(new KeyValuePair<int,string>(0,"双脚"));
            list.Add(new KeyValuePair<int,string>(1,"右脚"));
            list.Add(new KeyValuePair<int,string>(2,"左脚"));
            userData = list;            
        }

        private void GetFieldType(HttpContext context, ref object userData)
        {
            var needAll = Util.GetParamter<bool>(context.Request, "needall", false);
            userData = ConstValue.GetFieldType(needAll);
        }

        private void GetTeamMemberRoleList(HttpContext context, ref object userData)
        {
            userData = ConstValue.GetTeamMemberRoleList();
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