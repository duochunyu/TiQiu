using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using TiQiu.Biz;
using TiQiu.DAL;
using TiQiu.Model;
namespace TiQiu.WebServcie
{
    /// <summary>
    /// MemberHandler 的摘要说明
    /// </summary>
    public class MemberHandler : IHttpHandler, IRequiresSessionState 
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            HttpProcessor apj = new HttpProcessor();
            apj.ActionHandlerMaps.Add("GetMemberList", GetMemberList);
            apj.ActionHandlerMaps.Add("GetMemberInfo", GetMemberInfo);
            apj.ActionHandlerMaps.Add("GetTeamList", GetTeamList);
            apj.ActionHandlerMaps.Add("UpdateMemberInfo", UpdateMemberInfo);
            apj.ActionHandlerMaps.Add("UpdateTeamMemberInfo", UpdateTeamMemberInfo);
            apj.ActionHandlerMaps.Add("AddFeedback", AddFeedback);

            apj.ActionWithMember.Add("UpdateMemberInfo");
            //apj.ActionWithMember.Add("GetMemberInfo");
            apj.ActionWithMember.Add("UpdateTeamMemberInfo");
            apj.ActionWithMember.Add("AddFeedback");

            apj.ProcessRequestHandler<ApplicationException>(context, ex => Log.WriteException(ex));
        }

        private void GetMemberList(HttpContext context, ref object userData)
        {
            string phone = Util.GetParamter<string>(context.Request, "phone");
            string token = Util.GetParamter<string>(context.Request, "token");
            List<DAL.MEMBER> mList = MemberManager.GetMemberByPhone(phone);

            userData = ModelMapping.InitMemberInfo(mList); 
        }

        private void GetMemberInfo(HttpContext context, ref object userData)
        {
            int memberId = Util.GetParamter<int>(context.Request, "memberId");
            userData = ModelMapping.InitMemberInfo(MemberManager.GetMemberById(memberId));
        }

        private void GetTeamList(HttpContext context, ref object userData)
        {
            int memberId = Util.GetParamter<int>(context.Request, "memberId");
            string token = Util.GetParamter<string>(context.Request, "token");
            List<DAL.TEAM> mList = TeamManager.GetTeamByMemberId(memberId);
            userData = ModelMapping.InitTeamInfo(mList);
        }

        private void UpdateMemberInfo(HttpContext context, ref object userData)
        {
            int memberId = Util.GetParamter<int>(context.Request, "memberId");
            var nickName = Util.GetParamter<string>(context.Request, "nickName");
            var name = Util.GetParamter<string>(context.Request, "name");
            var phone = Util.GetParamter<string>(context.Request, "phone");
            var position = Util.GetParamter<string>(context.Request, "position");
            var brithday = Util.GetParamter<DateTime>(context.Request, "brithday");
            var feature = Util.GetParamter<string>(context.Request, "feature");
            var sex = Util.GetParamter<int>(context.Request, "sex");
            var eMail = Util.GetParamter<string>(context.Request, "eMail");
            var title = Util.GetParamter<string>(context.Request, "title");
            MemberManager.UpdateMember(memberId, name, nickName, position, phone, feature, eMail,title, sex, brithday);
        }

        private void UpdateTeamMemberInfo(HttpContext context, ref object userData)
        {
            int memberId = Util.GetParamter<int>(context.Request, "memberId");
            var teamId = Util.GetParamter<int>(context.Request, "teamId");
            var position = Util.GetParamter<string>(context.Request, "position");
            var number = Util.GetParamter<int>(context.Request, "number");

            MemberManager.UpdateTeamMemberInfo(memberId, teamId, number, position);
        }


        private void AddFeedback(HttpContext context, ref object userData)
        {
            int memberId = Util.GetParamter<int>(context.Request, "memberId");
            var content = Util.GetParamter<string>(context.Request, "content");
            var memberName = Util.GetParamter<string>(context.Request, "name", false);
            var phone = Util.GetParamter<string>(context.Request, "phone",false);
            var eMail = Util.GetParamter<string>(context.Request, "mail", false);
            MemberManager.AddFeedback(memberId, memberName, phone, eMail, content);
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