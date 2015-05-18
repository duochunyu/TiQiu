using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TiQiu.Common;
using TiQiu.DAL;
using TiQiu.Common.Util;
using System.Linq.Expressions;
using System.Web;

namespace TiQiu.Biz
{
    public static class AccountManager
    {
        #region For personal account

        public static ACCOUNT Login(string acct, string pwd)
        {               
            string enpwd = Common.Cryptography.CryptoManager.GetCrypto(Common.Cryptography.CryptoAlgorithm.MD5).Encrypt(pwd);
            ACCOUNT curAcct = null;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                curAcct = context.ACCOUNTs.Include("MEMBER").FirstOrDefault(a => a.NAME == acct && a.PWD == enpwd && a.STATUS == 1);
                if (curAcct == null) throw new ApplicationException("用户名、密码错误或账号已停用！");
                curAcct.LAST_LOGIN_DATE = DateTime.Now;
                context.SaveChanges();              
            }
            return curAcct;

        }

        public static void UpdatePassword(string acct,string newPwd)
        {
            
            using (TIQIUEntities context = new TIQIUEntities())
            {
                ACCOUNT curAcct = context.ACCOUNTs.Include("MEMBER").FirstOrDefault(a => a.NAME == acct && a.STATUS == 1);
                if (curAcct == null) throw new ApplicationException("用户名错误或账号已停用,修改失败！");
                curAcct.PWD = Common.Cryptography.CryptoManager.GetCrypto(Common.Cryptography.CryptoAlgorithm.MD5).Encrypt(newPwd);
                context.SaveChanges();
            }

        }

        public static void UpdatePassword(string acct, string curPwd, string newPwd)
        {
            string enpwd = Common.Cryptography.CryptoManager.GetCrypto(Common.Cryptography.CryptoAlgorithm.MD5).Encrypt(curPwd);
            ACCOUNT curAcct = null;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                curAcct = context.ACCOUNTs.Include("MEMBER").FirstOrDefault(a => a.NAME == acct && a.PWD == enpwd && a.STATUS == 1);
                if (curAcct == null) throw new ApplicationException("用户名、密码错误或账号已停用,修改失败！");
                curAcct.PWD = Common.Cryptography.CryptoManager.GetCrypto(Common.Cryptography.CryptoAlgorithm.MD5).Encrypt(newPwd);
                context.SaveChanges();
            }
            
        }

        public static void InitPassword(string acct)
        {
            string initPwd = Tools.GetRndOnlyNum(6);
            string enpwd = Common.Cryptography.CryptoManager.GetCrypto(Common.Cryptography.CryptoAlgorithm.MD5).Encrypt(initPwd);
            ACCOUNT curAcct = null;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                curAcct = context.ACCOUNTs.Include("MEMBER").FirstOrDefault(a => a.NAME == acct&& a.STATUS == 1);
                if (curAcct == null) throw new ApplicationException("用户名、密码错误或账号已停用,修改失败！");
                try
                {
                    Messager.SendSMS(curAcct.MEMBER.ID,
                        string.Format(ConfigurationManager.AppSettings["RESET_PWD_SMS"], initPwd));
                    
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("发送通知短信失败,修改失败！");
                }

                curAcct.PWD = enpwd;
                context.SaveChanges();


            }

        }

        public static ACCOUNT NewAccount(string name, string pwd)
        {
                 
            return NewAccount(name,name, pwd);
        }

        public static void UpdateClientBind(int memberId, int deviceType, string channelId, string userId,string version,DateTime loginDate)
        {
            if (channelId == "" || userId == "") return;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                var bind = context.BAIDU_USER_MEMBER.FirstOrDefault(a => a.MEMBER_ID == memberId && a.CHANNEL_ID == channelId && a.USER_ID == userId );
                if (bind == null)
                {
                    bind = new BAIDU_USER_MEMBER() { MEMBER_ID = memberId, USER_ID = userId, CHANNEL_ID = channelId, DEVICE_TYPE = deviceType, CREATE_DATE = loginDate};
                    context.BAIDU_USER_MEMBER.Add(bind);
                }
                bind.LAST_LOGIN_DATE = loginDate;
                bind.VERSION = version;
                context.SaveChanges();
            }
        }

        public static ACCOUNT GetAccountInfoByName(string name)
        {
            ACCOUNT curAcct = null;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                curAcct = context.ACCOUNTs.Include("MEMBER").FirstOrDefault(a => a.NAME == name && a.STATUS == 1);
            }
            
            return curAcct;
        }

        public static ACCOUNT GetAccountByID(int id)
        {
            ACCOUNT curAcct = null;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                curAcct = context.ACCOUNTs.Include("MEMBER").FirstOrDefault(a => a.ID == id && a.STATUS == 1);
            }

            return curAcct;
        }

        public static ACCOUNT GetAccountByPhone(string phone)
        {
            ACCOUNT curAcct = null;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                curAcct = context.ACCOUNTs.Include("MEMBER").FirstOrDefault(a => a.MEMBER.CELLPHONE == phone && a.STATUS == 1);
            }

            return curAcct;
        }

        /// <summary>
        /// 默认创建帐号如果有电话号码，自动生成会员数据。
        /// </summary>
        /// <param name="acct"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static ACCOUNT NewAccount(string name, string phone, string pwd)
        {
            if (CheckAcctExistByName(name)) throw new ApplicationException("存在同名账号，创建失败!");
            ACCOUNT curAcct = null;
            string enpwd = Common.Cryptography.CryptoManager.GetCrypto(Common.Cryptography.CryptoAlgorithm.MD5).Encrypt(pwd);
            using (TIQIUEntities context = new TIQIUEntities())
            {
                curAcct = new ACCOUNT() { NAME = name, PWD = enpwd, CREATE_DATE = DateTime.Now, STATUS = 1 };
                //if (!Tools.IsMobile(phone)) throw new ApplicationException("请提供手机号或以手机号作为用户名！");                
                if (phone.Length != 11) throw new ApplicationException("请提供手机号或以手机号作为用户名！");                
                if (context.MEMBER.Count(m => m.CELLPHONE == phone) > 0) throw new ApplicationException("电话号码已被注册!");
                MEMBER mb = new MEMBER() { NAME = name, CELLPHONE = phone };
                curAcct.MEMBER = mb;
                context.ACCOUNTs.Add(curAcct);
                context.SaveChanges();
                              
            }
            return curAcct;
            
        }

        public static void UpdateAccountStatus(int id, int status)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {
                ACCOUNT acct = context.ACCOUNTs.Single<ACCOUNT>(a => a.ID == id);
                acct.STATUS = status;
                context.SaveChanges();
            }
            
        }

        //用户名
        public static bool CheckAcctExistByName(string name)
        {
            bool exists = false;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                exists = context.ACCOUNTs.Count<ACCOUNT>(a => a.NAME == name) > 0; 
            }
            return exists;
        }

        //检查电话
        public static bool CheckAcctExistByPhone(string phone)
        {
            bool exists = false;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                exists = context.ACCOUNTs.Count<ACCOUNT>(a => a.NAME == phone) > 0;
            }
            return exists;
        }

        public static List<ACCOUNT> GetAccountList(Expression<Func<ACCOUNT, bool>> condition,string orderBy,bool ascending, int pageIdx, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<ACCOUNT> rel = new List<ACCOUNT>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = PagingQuery<ACCOUNT>.GetPagingList(context.ACCOUNTs.AsQueryable(), condition, orderBy, ascending, pageIdx, pageSize, out totalCount);
           
            }
            return rel;
        }

        #endregion

        #region For BUSINESSES ACCOUNT

        public static ACCOUNT_B LoginB(string acct, string pwd)
        {
            string enpwd = Common.Cryptography.CryptoManager.GetCrypto(Common.Cryptography.CryptoAlgorithm.MD5).Encrypt(pwd);
            ACCOUNT_B curAcct = null;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                curAcct = context.ACCOUNT_B.FirstOrDefault(a => a.NAME == acct && a.PWD == enpwd && a.STATUS == 1);
                if (curAcct == null)
                {
                    
                    int error = 0;
                    if(HttpContext.Current.Session["ErrorCount"] != null)int.TryParse(HttpContext.Current.Session["ErrorCount"].ToString(), out error);
                    if (error > 2)
                    {
                        throw new ApplicationException("用户名、密码错误已达3次以上，请输入验证码再提交！");
                    }
                    SessionUtil.SetSession("ErrorCount", error+ 1);
                    throw new ApplicationException("用户名、密码错误或账号已停用！");
                }
                curAcct.LAST_LOGIN_TIME = DateTime.Now;
                context.SaveChanges();
            }
            return curAcct;
        }

        public static void UpdateBPassword(string acct, string curPwd, string newPwd)
        {
            string enpwd = Common.Cryptography.CryptoManager.GetCrypto(Common.Cryptography.CryptoAlgorithm.MD5).Encrypt(curPwd);
            ACCOUNT_B curAcct = null;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                curAcct = context.ACCOUNT_B.FirstOrDefault(a => a.NAME == acct && a.PWD == enpwd && a.STATUS == 1);
                if (curAcct == null) throw new ApplicationException("用户名、密码错误或账号已停用,修改失败！");
                curAcct.PWD = Common.Cryptography.CryptoManager.GetCrypto(Common.Cryptography.CryptoAlgorithm.MD5).Encrypt(newPwd);
                context.SaveChanges();
            }

        }

        /// <summary>
        /// 通过名字获取商家信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ACCOUNT_B GetAccountBInfoByName(string name)
        {
            ACCOUNT_B curAcct = null;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                curAcct = context.ACCOUNT_B.FirstOrDefault(a => a.NAME == name && a.STATUS == 1);
            }

            return curAcct;
        }

        /// <summary>
        /// 必须先有商家数据,以便创建关联关系
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <param name="BUSINESSESId"></param>
        /// <returns></returns>
        public static ACCOUNT_B NewAccountB(string name,string pwd,int businessesId)
        {
            ACCOUNT_B acct = null;
            string enpwd = Common.Cryptography.CryptoManager.GetCrypto(Common.Cryptography.CryptoAlgorithm.MD5).Encrypt(pwd);
            using (TIQIUEntities context = new TIQIUEntities())
            {
                if (context.ACCOUNT_B.Count(a => a.NAME == name) > 0) throw new ApplicationException("Exists Same Account!");
                if (null == context.BUSINESSES.SingleOrDefault(b => b.ID == businessesId)) throw new ApplicationException("Not Exists Businesses.ID = " + businessesId.ToString());
                acct = new ACCOUNT_B() { NAME = name, CREATE_TIME = DateTime.Now, PWD = enpwd, STATUS = 1 };
                context.ACCOUNT_B.Add(acct);
                context.SaveChanges();
                context.ACCOUNT_B_BUSINESSES.Add(new ACCOUNT_B_BUSINESSES() { ACCOUNT_B_ID = acct.ID, BUSINESSESS_ID = businessesId });
                context.SaveChanges();

            }
            return acct;
        }

        public static void UpdateAccountBStatus(int id, int status)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {
                ACCOUNT_B acct = context.ACCOUNT_B.Single(a => a.ID == id);
                acct.STATUS = status;
                context.SaveChanges();

            }
        }

        public static bool CheckAcctBExist(string name)
        {
            bool exists = false;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                exists = context.ACCOUNT_B.Count<ACCOUNT_B>(a => a.NAME == name) > 0;
            }
            return exists;
        }

        public static List<ACCOUNT_B> GetAccountBList(Expression<Func<ACCOUNT_B, bool>> condition, string orderBy, bool ascending, int pageIdx, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<ACCOUNT_B> rel = new List<ACCOUNT_B>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                 rel = PagingQuery<ACCOUNT_B>.GetPagingList(context.ACCOUNT_B.AsQueryable(), condition, orderBy, ascending, pageIdx, pageSize, out totalCount);
           
            }
            return rel;
        }

        #endregion
    }
}
