using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.Web.WebPages.Utilities;

namespace TiQiu.Web
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageBase.RemoveCurrUserAuthCache();

            FormsAuthentication.SignOut();
            SessionUtil.AbandonSession();

            Response.Redirect("~/index.aspx");
        }
    }
}