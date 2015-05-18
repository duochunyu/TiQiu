using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.DAL;

namespace TiQiu.Web.UserControl.TeamInfoUserControl
{
    public partial class TeamerInfoListUserControl : System.Web.UI.UserControl
    {
        public List<MEMBER> DataSource { get; set; }

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
          
            hdTeamTotal.Value = totalCount.ToString();
            this.rtMemberList.DataSource = DataSource;
            rtMemberList.DataBind();
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