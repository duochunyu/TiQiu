using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using TiQiu.Biz;
using TiQiu.Common;
using TiQiu.Common.Util;
using System.Configuration;
using TiQiu.Model;
namespace TiQiu.WebServcie
{
    /// <summary>
    /// AccountHandler 的摘要说明
    /// </summary>
    public class AccountHandler : IHttpHandler, IRequiresSessionState 
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";            
            HttpProcessor apj = new HttpProcessor();
            apj.ActionHandlerMaps.Add("Login", Login);
            apj.ActionHandlerMaps.Add("NewAccount", NewAccount);
            apj.ActionHandlerMaps.Add("GetAccountByName", GetAccountByName);
            apj.ActionHandlerMaps.Add("GetAccountByID", GetAccountByID);
            apj.ActionHandlerMaps.Add("GetAccountByPhone", GetAccountByPhone);
            apj.ActionHandlerMaps.Add("GetVerifyCode", GetVerifyCode);
            apj.ActionHandlerMaps.Add("SendVerifyCode", SendVerifyCode);
            apj.ActionHandlerMaps.Add("LoginB", LoginB);
            apj.ActionHandlerMaps.Add("BindClientInfo", BindClientInfo);
            apj.ActionHandlerMaps.Add("UpdatePassword", UpdatePassword);
            apj.ActionHandlerMaps.Add("ResetPassword", ResetPassword);
            apj.ActionHandlerMaps.Add("UpdateAccountBPassword", UpdateAccountBPassword);

            apj.ActionWithMember.Add("BindClientInfo");
            apj.ActionWithMember.Add("UpdatePassword");            
            apj.ActionWithManager.Add("UpdateAccountBPassword");
 
            apj.ProcessRequestHandler<ApplicationException>(context, ex => Log.WriteException(ex));
        }

        private void SendVerifyCode(HttpContext context, ref object userDate)
        {
            var phone = Util.GetParamter<string>(context.Request, "phone");
            string verifyCode = Tools.GetRndOnlyNum(4);
            int timeout;
            if (!int.TryParse(ConfigurationManager.AppSettings["VERIFY_CODE_TIMEOUT"], out timeout))
                timeout = 600;
            ;
           // CacheManager.SetCache(phone, verifyCode, DateTime.UtcNow.AddSeconds(90), TimeSpan.Zero);
            try
            {
                Messager.SendSMS(new string[] {phone},
                    string.Format(ConfigurationManager.AppSettings["VERIFY_SMS"], verifyCode), DateTime.Now);
            }
            catch (Exception ex)
            {
                 throw new ApplicationException("发送验证码失败,请重试！");
            }
            CacheManager.SetCache(phone, verifyCode, DateTime.UtcNow.AddSeconds(timeout), TimeSpan.Zero);
        }
        
        private void GetVerifyCode(HttpContext context, ref object userData)
        {
            var phone = Util.GetParamter<string>(context.Request, "phone");
            userData = CacheManager.GetCache<string>(phone);
        }       

        private void Login(HttpContext context, ref object userData)
        {
            var userName = Util.GetParamter<string>(context.Request,"name");
            var password = Util.GetParamter<string>(context.Request,"pwd");
            var bduserid = Util.GetParamter<string>(context.Request, "bduserid");
            var bdchannelid = Util.GetParamter<string>(context.Request, "bdchannelid");
            var deviceType = Util.GetParamter<int>(context.Request, "devicetype");
            var version = Util.GetParamter<string>(context.Request, "version");  
            DAL.ACCOUNT acct = AccountManager.Login(userName, password);
            
            Account acc = ModelMapping.InitAcct(acct);
            TokenEntity token = new TokenEntity()
            {
                IsAccount = false,
                AccountId = acct.ID,
                AcccountName = acct.NAME,
                MemberId = acct.MEMBER_ID,
                MemberName = acct.MEMBER.NAME,
                NickName = acct.MEMBER.NICK_NAME,
                LoginDate = DateTime.Now,
                PortraitUrl = acc.Portrait,
                TimeStamp = DateTime.Now.AddDays(-3).Date
            };
            acc.Token = CacheManager.GenerateToken<TokenEntity>(HttpContext.Current, token);
            AccountManager.UpdateClientBind(acct.MEMBER_ID, deviceType, bdchannelid, bduserid,version,token.LoginDate);
            userData = acc;
              
        }

        private void BindClientInfo(HttpContext context, ref object userDate)
        {
            var bduserid = Util.GetParamter<string>(context.Request, "bduserid");
            var bdchannelid = Util.GetParamter<string>(context.Request, "bdchannelid");
            var deviceType = Util.GetParamter<int>(context.Request, "devicetype");
            var version = Util.GetParamter<string>(context.Request, "version");
            var memberid = Util.GetParamter<int>(context.Request, "memberid");
            AccountManager.UpdateClientBind(memberid, deviceType, bdchannelid, bduserid, version,DateTime.Now);
        } 

        private void NewAccount(HttpContext context, ref object userData)
        {
            var userName = Util.GetParamter<string>(context.Request,"name");
            var telphone = Util.GetParamter<string>(context.Request,"phone");
            var password = Util.GetParamter<string>(context.Request,"pwd");
            var code = Util.GetParamter<string>(context.Request, "code");
            if (code != CacheManager.GetCache<string>(telphone))
            {
                throw new ApplicationException("手机验证码不正确，请重新获取再注册！");
            }
            string msg = string.Empty;

            //check
            //if (AccountManager.CheckAcctExistByName(userName))
            //{
            //    throw new ApplicationException("当前账户已存在");
            //    // return;
            //}
            if (AccountManager.CheckAcctExistByPhone(telphone))
                {
                    throw new ApplicationException("当前手机号码已存在，请重新输入");
                    // return;
                }
            userData = ModelMapping.InitAcct(AccountManager.NewAccount(telphone, telphone, password));        
            
        }

        private void GetAccountByName(HttpContext context, ref object userData)
        {
            userData = ModelMapping.InitAcct(AccountManager.GetAccountInfoByName(Util.GetParamter<string>(context.Request, "name")));
        }

        private void GetAccountByID(HttpContext context, ref object userData)
        {
            userData = ModelMapping.InitAcct(AccountManager.GetAccountByID(Util.GetParamter<int>(context.Request, "id")));
        }

        private void GetAccountByPhone(HttpContext context, ref object userData)
        {
            userData = ModelMapping.InitAcct(AccountManager.GetAccountByPhone(Util.GetParamter<string>(context.Request, "phone")));
        }

        private void UpdatePassword(HttpContext context, ref object userData)
        {
            var userName = Util.GetParamter<string>(context.Request,"name");
            var curPassword = Util.GetParamter<string>(context.Request, "pwd");
            var newPassword = Util.GetParamter<string>(context.Request, "newpwd");
            AccountManager.UpdatePassword(userName, curPassword, newPassword);
        }

        private void ResetPassword(HttpContext context, ref object userData)
        {
            var userName = Util.GetParamter<string>(context.Request,"name");
            var newPassword = Util.GetParamter<string>(context.Request, "newpwd");
            var code = Util.GetParamter<string>(context.Request, "code");
            if (code != CacheManager.GetCache<string>(userName))
            {
                throw new ApplicationException("短信验证码不正确！");
            }
            AccountManager.UpdatePassword(userName,newPassword);
           // AccountManager.InitPassword(userName);
        }

        

        private void UpdateAccountBPassword(HttpContext context, ref object userData)
        {
            var userName = Util.GetParamter<string>(context.Request, "name");
            var curPassword = Util.GetParamter<string>(context.Request, "pwd");
            var newPassword = Util.GetParamter<string>(context.Request, "newpwd");
            AccountManager.UpdateBPassword(userName, curPassword, newPassword);
        }

        private void LoginB(HttpContext context, ref object userData)
        {
            var userName = Util.GetParamter<string>(context.Request, "name");
            var password = Util.GetParamter<string>(context.Request, "pwd");
            int error = 0;
            if (context.Session["ErrorCount"] != null
                && int.TryParse(context.Session["ErrorCount"].ToString(), out error)
                && error > 3
                )
            {
                string code = Util.GetParamter<string>(context.Request, "code",false);
                if (code == null || code.Length == 0 || code.ToUpper() != context.Session["ValidateCode"].ToString().ToUpper())
                    throw new ApplicationException("验证码错误！");

            }
           
           var acct = AccountManager.LoginB(userName, password);
           SessionUtil.RemoveSession("ErrorCount");
           SessionUtil.RemoveSession("ValidateCode");
           var list = FieldManager.GetFieldList(acct.ID);
           List<int> fieldIds = new List<int>();
           list.ForEach(l => { fieldIds.Add(l.ID); });
           AccountB acc = ModelMapping.InitAcct(acct);
           TokenEntity token = new TokenEntity()
            {
                IsAccount = true,
                AccountBId = acct.ID,
                AccountBName = acct.NAME,
                NickName = acct.NAME,
                LoginDate = DateTime.Now,
                TimeStamp = DateTime.Now,
                Fields = fieldIds
            };
            acc.Token = CacheManager.GenerateToken<TokenEntity>(HttpContext.Current, token);
           userData = acc;

        }       
         
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }  
}