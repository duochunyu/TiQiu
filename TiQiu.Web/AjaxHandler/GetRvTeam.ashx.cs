using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TiQiu.DAL;

namespace TiQiu.Web.AjaxHandler
{
    /// <summary>
    /// GetRvTeam 的摘要说明
    /// </summary>
    public class GetRvTeam : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string key = context.Request.QueryString["Key"];
            var list = GetTeamList().Where(s => s.NAME.Contains(key));
            var result = from item in list
                         select new TeamInfo
                         {
                             ID = item.ID,
                             Name = item.NAME
                         };
            context.Response.ContentType = "application/json";
            System.Web.Script.Serialization.JavaScriptSerializer jsonSerial = new System.Web.Script.Serialization.JavaScriptSerializer();
            string message = jsonSerial.Serialize(result.Take(10));
            context.Response.Write(message);
        }

        private List<TEAM> GetTeamList()
        {
            List<TEAM> list = new List<TEAM>();
            for (int i = 0; i < 100; i++)
            {
                TEAM t = new TEAM
                {
                    ID = i + 1,
                    NAME = "球队" + i
                };
                list.Add(t);
            }
            return list;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class TeamInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}