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
    public class GetMemberInfos : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string key = context.Request.QueryString["Key"];
            int totalCount = 0;
            var list = MemberManager.GetMemberList(s => s.CELLPHONE.Contains(key), 1, 10, out totalCount);

            var result = (from item in list
                          select new MemberInfo
                          {
                              ID = item.ID,
                              Name = item.NAME,
                              Phone = item.CELLPHONE
                          }).ToList();
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    if (item.TeamList == null)
                    {
                        item.TeamList = new List<Model.TeamInfo>();
                    }

                    var teamInfos = TeamManager.GetTeamByMemberId(item.ID);
                    foreach (var team in teamInfos)
                    {
                        item.TeamList.Add(new Model.TeamInfo
                        {
                            ID = team.ID,
                            Name = team.NAME
                        });
                    }
                }
            }
            context.Response.ContentType = "application/json";
            System.Web.Script.Serialization.JavaScriptSerializer jsonSerial = new System.Web.Script.Serialization.JavaScriptSerializer();
            string message = jsonSerial.Serialize(result.Take(10));
            context.Response.Write(message);
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