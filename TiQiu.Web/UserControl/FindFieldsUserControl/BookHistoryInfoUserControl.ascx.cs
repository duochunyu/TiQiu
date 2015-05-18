using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.Biz;

namespace TiQiu.Web.UserControl.FindFieldsUserControl
{
    public partial class BookHistoryInfoUserControl : System.Web.UI.UserControl
    {
        private int orderPage;
        private int orderTotal;

        public int MemberID { get; set; }

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
            string currentOrderPage = hdOrderCurrentPage.Value.Trim();
            int.TryParse(currentOrderPage, out orderPage);
            ltlPageIndex.Text = currentOrderPage.ToString();

            string strOrderTotal = hdOrderTotal.Value.Trim();
            int.TryParse(strOrderTotal, out orderTotal);
        }

        private void SearchData()
        {
            int totalCount = 0;
            var list = OrderManager.GetOrderList(s => s.MEMBER_ID == MemberID, "ORDER_DATE", false, orderPage, 4, out totalCount);
            hdOrderTotal.Value = totalCount.ToString();
            rtHistory.DataSource = list;
            rtHistory.DataBind();
        }

        protected void btnItemPrePage_Click(object sender, EventArgs e)
        {
            if (orderPage == 1)
            {
                return;
            }

            orderPage--;
            hdOrderCurrentPage.Value = ltlPageIndex.Text = orderPage.ToString();
            SearchData();
        }

        protected void btnItemNextPage_Click(object sender, EventArgs e)
        {
            if (orderPage == Math.Ceiling((double)orderTotal / 5))
            {
                return;
            }
            orderPage++;
            hdOrderCurrentPage.Value = ltlPageIndex.Text = orderPage.ToString();
            SearchData();
        }
    }
}