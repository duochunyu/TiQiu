using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.Biz;
using TiQiu.DAL;

namespace TiQiu.Web.UserControl.FindTeamsUserControl
{
    public partial class SearchTeamInfoListControl : System.Web.UI.UserControl
    {

        public List<TEAM> DataSource { get; set; }

        public string _teamName { get; set; }
        public string _memberName { get; set; }

        private string TeamName
        {
            get { return _teamName; }
            set { _teamName = value; }
        }
        private string MemberName
        {
            get { return _memberName; }
            set { _memberName = value; }
        }

        private int orderPage;
        private int orderTotal;

        protected void Page_Load(object sender, EventArgs e)
        {
            GetData();
            if (!IsPostBack)
            {
                SearchData();
            }
        }

        private void GetData()
        {
            string currentOrderPage = hdTeamCurrentPage.Value.Trim();
            int.TryParse(currentOrderPage, out orderPage);
            ltlPageIndex.Text = currentOrderPage.ToString();

            string strOrderTotal = hdTeamTotal.Value.Trim();
            int.TryParse(strOrderTotal, out orderTotal);
        }

        private void SearchData()
        {
            int totalCount = 0;
            var list = TeamManager.GetTeamList(s => s.NAME == TeamName, "TeamName", false, orderPage, 6, out totalCount);
            hdTeamTotal.Value = totalCount.ToString();
            this.rtTeamList.DataSource = list;
            rtTeamList.DataBind();
        }

        protected void btnItemPrePage_Click(object sender, EventArgs e)
        {
            if (orderPage == 1)
            {
                return;
            }

            orderPage--;
            hdTeamCurrentPage.Value = ltlPageIndex.Text = orderPage.ToString();
            SearchData();
        }

        protected void btnItemNextPage_Click(object sender, EventArgs e)
        {
            if (orderPage == Math.Ceiling((double)orderTotal / 5))
            {
                return;
            }
            orderPage++;
            hdTeamCurrentPage.Value = ltlPageIndex.Text = orderPage.ToString();
            SearchData();
        }
    }
}