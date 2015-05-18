using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TiQiu.Biz;
using TiQiu.Model;
namespace TiQiu.WebServcie
{
    /// <summary>
    /// TeamHandler 的摘要说明
    /// </summary>
    public class TeamHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            HttpProcessor apj = new HttpProcessor();
            apj.ActionHandlerMaps.Add("GetTeamInfo", GetTeamInfo);
            apj.ActionHandlerMaps.Add("GetTeamScoreList", GetTeamScoreList);
            apj.ActionHandlerMaps.Add("GetMyTeamList", GetMyTeamList);
            apj.ActionHandlerMaps.Add("GetTeamList", GetTeamList);
            apj.ActionHandlerMaps.Add("CreateTeam", CreateTeam);
            apj.ActionHandlerMaps.Add("DelTeam", DelTeam);
            apj.ActionHandlerMaps.Add("UpdateTeam", UpdateTeam);
            apj.ActionHandlerMaps.Add("JoinTeam", JoinTeam);
            apj.ActionHandlerMaps.Add("FireTeamMember", FireTeamMember);

            apj.ActionWithMember.Add("GetMyTeamList");
            apj.ActionWithMember.Add("FireTeamMember");
            apj.ActionWithMember.Add("CreateTeam");
            apj.ActionWithMember.Add("DelTeam");
            apj.ActionWithMember.Add("UpdateTeam");
            apj.ActionWithMember.Add("JoinTeam");
            
            apj.ProcessRequestHandler<ApplicationException>(context, ex => Log.WriteException(ex));
        }

        private void GetTeamInfo(HttpContext context, ref object userData)
        {
            var teamId = Util.GetParamter<int>(context.Request, "teamId");

            userData = ModelMapping.InitTeamInfo(TeamManager.GetTeamDetailInfo(teamId));

        }

        private void GetMyTeamList(HttpContext context, ref object userData)
        {
            var memberId = Util.GetParamter<int>(context.Request, "memberId");

            userData = ModelMapping.InitTeamInfo(TeamManager.GetMyTeamList(memberId));
        }

        private void GetTeamList(HttpContext context, ref object userData)
        {
            var pageIndex = Util.GetParamter<int>(context.Request, "pageIndex");
            var pageSize = Util.GetParamter<int>(context.Request, "pageSize");
            var name = Util.GetParamter<string>(context.Request, "name");
            int total = 0;
            var teams = ModelMapping.InitTeamInfo(TeamManager.GetTeamList(name, "BUILD_DATE", false, pageIndex, pageSize, out total));
            userData = new PageInfo<Team>() { ItemList = teams, Total = total };
        }

        private void GetTeamScoreList(HttpContext context, ref object userData)
        {
            var teamId = Util.GetParamter<int>(context.Request, "teamId");
            var pageIndex = Util.GetParamter<int>(context.Request, "pageIndex");
            var pageSize = Util.GetParamter<int>(context.Request, "pageSize");
            var start = Util.GetParamter<DateTime>(context.Request, "start", false);
            var end = Util.GetParamter<DateTime>(context.Request, "end", false);
            int total = 0;
            List<DAL.TEAM_SCORE> score = TeamManager.GetTeamScoreList(teamId, "ORDER_DATE", false, start, end, pageIndex, pageSize, out total);
            userData = new PageInfo<TeamScore>() { ItemList = ModelMapping.InitTeamScoreInfo(score), Total = total };

        }

        private void CreateTeam(HttpContext context, ref object userData)
        {
            var memberId = Util.GetParamter<int>(context.Request, "memberId");
            var teamName = Util.GetParamter<string>(context.Request, "teamname");
            var brief = Util.GetParamter<string>(context.Request, "brief",false);
            var feature = Util.GetParamter<string>(context.Request, "featrue", false);//参数名错误：feat   ru  e  暂时不修改
            var buildDate = Util.GetParamter<DateTime>(context.Request, "buildDate", false);
            if (buildDate == null || buildDate < DateTime.Parse("1900-01-01")) buildDate = DateTime.Now;
            userData = TeamManager.CreateTeam(teamName, brief, feature, buildDate, memberId).ID;
        }

        private void DelTeam(HttpContext context, ref object userData)
        {
            var memberId = Util.GetParamter<int>(context.Request, "memberId");
            var teamId = Util.GetParamter<int>(context.Request, "teamId");

            TeamManager.DelTeam(memberId, teamId);
        }

        private void UpdateTeam(HttpContext context, ref object userData)
        {
            var teamId = Util.GetParamter<int>(context.Request, "teamId");
            var memberId = Util.GetParamter<int>(context.Request, "memberId");
            var teamName = Util.GetParamter<string>(context.Request, "teamname",false);
            var brief = Util.GetParamter<string>(context.Request, "brief",false);
            var feature = Util.GetParamter<string>(context.Request, "feature", false);
            var buildDate = Util.GetParamter<DateTime>(context.Request, "buildDate",false);

            TeamManager.UpdateTeam(memberId, teamId, teamName, brief, feature, buildDate);
        }

        private void JoinTeam(HttpContext context, ref object userData)
        {
            var memberId = Util.GetParamter<int>(context.Request, "memberId");
            var teamId = Util.GetParamter<int>(context.Request, "teamId");
            var roleId = Util.GetParamter<int>(context.Request, "roleId");

            var number = Util.GetParamter<int>(context.Request, "number");
            var position = Util.GetParamter<string>(context.Request, "position");

            TeamManager.JoinTeam(memberId, teamId, number, position, roleId);
        }

        private void FireTeamMember(HttpContext context, ref object userData)
        {
            var memberId = Util.GetParamter<int>(context.Request, "memberId");
            var fireId = Util.GetParamter<int>(context.Request, "fireId");
            var teamId = Util.GetParamter<int>(context.Request, "teamId");

            TeamManager.FireTeamMember(memberId, teamId, fireId);
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