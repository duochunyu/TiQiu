using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;
using System.Security.Authentication;
using System.Web;
using TiQiu.Biz;
using TiQiu.DAL;
using TiQiu.Web.WebPages.Utilities;

namespace TiQiu.Web
{
    public class PageBase :System.Web.UI.Page
    {
        #region private fields

        private const string FILE_ROOT_KEY = "FileVirtualPath";

        private ACCOUNT curAccount;

        private MEMBER curMember;

        private string fileRoot;

       

        #endregion

        #region property

        public ACCOUNT CurAccount
        {
            get { return curAccount; }
        }

        public MEMBER CurMember
        {
            get { return curMember; }
            set { curMember = value; }
        }

        public string FileRoot
        {
            get { return fileRoot; }
        }

       

        public bool IsLogin
        {
            get { return curAccount != null && curAccount.MEMBER_ID > 0; }
        }

        #endregion

        public PageBase()
        {
            GetCurrAccount();
            
            fileRoot = ConfigurationManager.AppSettings[FILE_ROOT_KEY];
        }

        #region public function

        public void GetCurrAccount()
        {
            var context = HttpContext.Current;
            if (context == null)
                throw new ArgumentNullException("HttpContext");



            if (context.Request.IsAuthenticated &&
                !string.IsNullOrWhiteSpace(context.User.Identity.Name))
            {
                var cache = MemoryCache.Default;

                string cacheKey = GetCacheKey(context.User.Identity.Name);

                curAccount = cache[cacheKey] as ACCOUNT;
                if (curAccount == null)
                {
                    curAccount = AccountManager.GetAccountInfoByName(context.User.Identity.Name);
                    
                    if (curAccount != null)
                    {
                        cache.Set(cacheKey, curAccount, DateTimeOffset.Now.AddMinutes(10D));
                    }
                }
            }
            if (curAccount != null) CurMember = MemberManager.GetMemberById(curAccount.MEMBER_ID);
            //if (user == null)
            //{
            //    throw new AuthenticationException("抱歉，您的登录已经超时或者失效，请重新登录!");
            //}
            
            
        }

        public static void RemoveCurrUserAuthCache()
        {
            var context = HttpContext.Current;
            if (context == null)
                throw new ArgumentNullException("HttpContext");

            if (context.Request.IsAuthenticated &&
                           !string.IsNullOrWhiteSpace(context.User.Identity.Name))
            {
                var cache = MemoryCache.Default;

                string cacheKey = GetCacheKey(context.User.Identity.Name);

                if (cache.Contains(cacheKey))
                    cache.Remove(cacheKey);
            }
        }

        #endregion

        #region private function

        private static string GetCacheKey(string username)
        {
            return string.Format("TIQIU-{0}", username);
        }

        #endregion
    }
}