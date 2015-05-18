using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.Biz;
using TiQiu.Common.Util;
using TiQiu.Web.WebPages.Utilities;

namespace TiQiu.Web.BG
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(tbName.Text) && !string.IsNullOrEmpty(tbPwd.Text) && !string.IsNullOrEmpty(tbVerifyCode.Text))
            {
                if (Session["Code"] == null || Session["Code"].ToString().ToLower() != tbVerifyCode.Text.ToLower())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), new Guid().ToString(), "alert('验证码错误，请重新输入')", true);
                    return;
                }
                try
                {
                    string isBusiness = "IsBusiness";//是否商家登录
                    string businessName = "BusinessName";
                    AccountManager.LoginB(tbName.Text.Trim(), tbPwd.Text.Trim());
                    FormsAuthentication.SetAuthCookie(tbName.Text.Trim(), true);
                    TiQiu.Biz.SessionUtil.SetSession(isBusiness, "true");
                    TiQiu.Biz.SessionUtil.SetSession(businessName, tbName.Text.Trim());
                    HttpContext.Current.Session["IsBusiness"] = "true";
                    Log.WriteBizLog(string.Format("商家{0}登录踢球去", tbName.Text.Trim()));
                    Response.Redirect("~/BG/Default.aspx");
                }
                catch (ApplicationException ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), new Guid().ToString(), string.Format("alert('{0}')", ex.Message), true);
                }
            }
        }
    }
}