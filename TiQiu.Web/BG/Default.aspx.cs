using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Collections.Generic;
using TiQiu.Web.WebPages.Utilities;


namespace ShiverVin.ECP.WebUI
{
    public partial class _Default : Page
    {
        private string isBusiness;
        public string IsBusiness
        {
            get { return isBusiness; }
            set { isBusiness = value; }
        }

        private string name;
        public string UserName
        {
            get { return name; }
            set { name = value; }
        }

        string DefaultPage = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            isBusiness = SessionUtil.GetSession<string>("IsBusiness");
            if (isBusiness != "true")
            {
                Response.Redirect("Login.aspx");
                return;
            }
            name = SessionUtil.GetSession<string>("BusinessName");
            if (!string.IsNullOrEmpty(name))
            {
                this.lblUserName.Text = string.Format("欢迎您&nbsp;{0}&nbsp;登录", name);
            }
        }

        //安全退出
        protected void lnkBtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("../../Login.aspx");
        }

    }
}
