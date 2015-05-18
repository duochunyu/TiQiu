using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.Biz;
using TiQiu.DAL;

namespace TiQiu.Web.WebPages
{
    public partial class TeamInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //球队列表
            Expression<Func<TEAM, bool>> express = PredicateExtensionses.True<TEAM>();
            int outTotal = 10;
            List<TEAM> teams = new List<TEAM>();
            teams=TeamManager.GetTeamList(express, "ID", false, 0, 5, out outTotal).Take(5).ToList();
            this.TeamsInfo.DataSource = teams;

            string TeamId = string.Empty;
            if (string.IsNullOrEmpty(TeamId))
            {

            }
            else
            {
                int temp = 0;
                if (int.TryParse(TeamId, out temp))
                {
                    BindData(temp);
                }
            }
        }

        #region 球队详情

        private void BindData(int teamId)
        {
            //根据TeamId获取球队详情
            List<TEAM> list = new List<TEAM>();
            list.Add(TeamManager.GetTeamDetailInfo(teamId));
            this.TeamInfoDetails.entity = list;

            //根据Teamid获取球队队员列表

            // account entity = currentuser;
            int totalCount = 0;
            Expression<Func<MEMBER, bool>> express = PredicateExtensionses.True<MEMBER>();
            express = express.And(s => s.ID == teamId);
            this.TeamerInfoList.DataSource =MemberManager.GetMemberList(express,0,5,out totalCount);

        }

        #endregion
    }
}