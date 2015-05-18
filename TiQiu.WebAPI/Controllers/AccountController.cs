using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TiQiu.WebAPI.Models;
using TiQiu.Biz;
using TiQiu.DAL;

namespace TiQiu.WebAPI.Controllers
{
    public class AccountController : ApiController
    {
        [HttpPost]
        public Message CreateAccount( string name,string phone, string pwd)
        {
            Message rel = new Message();
            try
            {
                ACCOUNT acct = AccountManager.NewAccount(name, phone, pwd);
                rel.Result = 1;
                rel.ReturnData = acct;
            }
            catch (ApplicationException ax)
            {
                rel.Result = 0;
                rel.ErrorMessage = ax.Message;
            }
            catch (Exception ex)
            {
                rel.Result = 0;
                rel.ErrorMessage = "创建帐户失败！";
            }
            return rel;
        }
    }
}
