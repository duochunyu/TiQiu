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
using TiQiu.Web.WebPages.Utilities;

namespace TiQiu.BG.MasterPages
{
    public partial class Common : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string isBusiness = SessionUtil.GetSession<string>("IsBusiness");
            if (isBusiness != "true")
            {
                Response.Redirect("../Login.aspx");
                return;
            }
            this.RegisterScript("uiStyle", "InitFunc();");
            this.RegisterScript("glabalStyle", "initUI();");
            this.RegisterScript("InputEnter", "glabalEnter();");
        }



        /// <summary>
        /// 注册Javascript代码
        /// </summary>
        /// <param name="key"></param>
        /// <param name="funcationName"></param>
        public void RegisterScript(string key, string funcationName)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), key, funcationName, true);
        }
    }
}
