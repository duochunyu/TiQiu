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
    public partial class FindTeam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            int totalCount = 0;
            Expression<Func<TEAM, bool>> condition = PredicateExtensionses.True<TEAM>();
            condition = condition.And(s => s.NAME.Contains(this.txtTeamName.Value.Trim()));
            this.TeamInfoList.DataSource = TeamManager.GetteamListByTeamName(condition, "ID", true, 0, 5, out totalCount);
        }
    }
}