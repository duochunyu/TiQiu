using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TiQiu.Biz;
using TiQiu.Common.Serialization;
using TiQiu.DAL;

namespace TiQiu.Web.AjaxHandler
{
    /// <summary>
    /// CreateNewAccount 的摘要说明
    /// </summary>
    public class CreateNewAccount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var userName = context.Request.QueryString["name"];
            var telphone = context.Request.QueryString["telphone"];
            var password = context.Request.QueryString["password"];
            //account entity = JsonObjectSerializer.FromJson<account>(queryacount);
            string msg = string.Empty;

            //check
            if (AccountManager.CheckAcctExistByName(userName))
            {
                msg = "当前账户已存在";
               // return;
            }
            else
            {
                if (AccountManager.CheckAcctExistByPhone(telphone))
                {
                    msg = "当前手机号码已存在，请重新输入";
                   // return;
                }
                else
                {
                    var resenity = AccountManager.NewAccount(userName, telphone, password);

                    msg = resenity != null ? "创建成功" : "注册失败";
                }
            }


            context.Response.ContentType = "application/json";
            System.Web.Script.Serialization.JavaScriptSerializer jsonSerial = new System.Web.Script.Serialization.JavaScriptSerializer();
            string message = jsonSerial.Serialize(msg);
            context.Response.Write(message);
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