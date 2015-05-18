using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.Biz;
using TiQiu.Common.Util;
using TiQiu.DAL;
using TiQiu.Web.WebPages.Utilities;
using System.Configuration;
namespace TiQiu.Web
{
    public partial class Login : System.Web.UI.Page
    {
        private string isBusiness="IsBusiness";//是否商家登录
        private string businessName = "BusinessName";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.litErrorMsg.Text = "";
            //uu_email.Value = "用户名";
           
        }


        protected void ibtnLogin_Click(object sender, EventArgs e)
        {
            
            string userName = uu_email.Value.Trim();
            string userPwd = uu_password.Value.Trim();
            this.pMsg.Visible = false;
            if (String.IsNullOrEmpty(userName))
            {
                this.pMsg.Visible = true;
                this.litErrorMsg.Text = "请输入用户名";
                return;
            }
            if (String.IsNullOrEmpty(userPwd))
            {
                this.litErrorMsg.Text = "请输入密码";
                pMsg.Visible = true;
                return;
            }

            //普通用户登录
            if (radioNormalLogin.Checked == true)
            {
                try
                {
                    AccountManager.Login(userName, userPwd);
                    FormsAuthentication.SetAuthCookie(userName, true);
                    TiQiu.Biz.SessionUtil.SetSession(isBusiness, "false");
                    TiQiu.Biz.SessionUtil.SetSession(businessName, "");
                    Log.WriteBizLog(string.Format("普通用户{0}登录踢球去", userName));

                    //Response.Redirect("~/index.aspx");
                    string returnUrl = FormsAuthentication.GetRedirectUrl(userName, true);

                    Response.Redirect(returnUrl);
                }
                catch (Exception)
                {
                    this.litErrorMsg.Text = "登录失败，请检查用户名或密码";
                    pMsg.Visible = true;
                }
            }
            //商家登录
            if (this.radioMerchantLogin.Checked == true)
            {
                try
                {
                    AccountManager.LoginB(userName, userPwd);
                    FormsAuthentication.SetAuthCookie("", true);
                    TiQiu.Biz.SessionUtil.SetSession(isBusiness, "true");
                    TiQiu.Biz.SessionUtil.SetSession(businessName, userName);
                    HttpContext.Current.Session["IsBusiness"] = "true";
                    Log.WriteBizLog(string.Format("商家{0}登录踢球去", userName));
                                
                    Response.Redirect("~/BG/Default.aspx");
                }
                catch (Exception ex)
                {

                    this.litErrorMsg.Text = "登录失败，请检查用户名或密码";
                    pMsg.Visible = true;
                }
            }
        }
    }
}