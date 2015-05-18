using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TiQiu.Common.Cryptography;
namespace TiQiu.Biz
{
    public static class CookieUtil
    {
        private const string PRIVATE_KEY = "dfD$873-DX8@#d-dR5@#0-123B9DK-12D!F87d";       

        private static string GenerateSign(string plainText)
        {
            return CryptoManager.GetCrypto(CryptoAlgorithm.MD5).Encrypt(plainText + PRIVATE_KEY);
        }

        public static void SetCookie(string cookieName, string plainText)
        {
            SetCookie(cookieName, plainText, new DateTime?());
        }

        public static void SetCookie(string cookieName, string plainText, DateTime expireTime)
        {
            SetCookie(cookieName, plainText, new DateTime?(expireTime));
        }

        public static void SetCookie<T>(string cookieName, T entity) where T : class
        {
            string plainText = JsonConvert.SerializeObject(entity);
            SetCookie(cookieName, plainText);
        }

        public static void SetCookie<T>(string cookieName, T entity, DateTime expireTime) where T : class
        {
            string plainText = JsonConvert.SerializeObject(entity);
            SetCookie(cookieName, plainText, expireTime);
        }

        private static void SetCookie(string cookieName, string plainText, DateTime? expireTime)
        {
            CookieObj c = new CookieObj();
            c.Sign = GenerateSign(plainText);
            c.Content = CryptoManager.GetCrypto(CryptoAlgorithm.DES).Encrypt(plainText);

            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = JsonConvert.SerializeObject(c);
            if (expireTime.HasValue)
            {
                cookie.Expires = expireTime.Value;
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string GetCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null || cookie.Value == null || cookie.Value.Trim().Length <= 0)
            {
                return null;
            }
            try
            {
                CookieObj c = JsonConvert.DeserializeObject<CookieObj>(cookie.Value);
                string plainText = CryptoManager.GetCrypto(CryptoAlgorithm.DES).Decrypt(c.Content);
                if (c.Sign == GenerateSign(plainText))
                {
                    return plainText;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static T GetCookie<T>(string cookieName) where T : class
        {
            string plainText = GetCookie(cookieName);
            if (plainText == null || plainText.Trim().Length <= 0)
            {
                return null;
            }
            try
            {
                return JsonConvert.DeserializeObject<T>(plainText);
            }
            catch
            {
                return null;
            }
        }

        public static void RemoveCookie(string cookieName)
        {
            HttpCookie myCookie = new HttpCookie(cookieName);
            myCookie.Expires = DateTime.Now.AddDays(-10d);
            HttpContext.Current.Response.Cookies.Add(myCookie);
            HttpContext.Current.Response.AppendCookie(myCookie);
        }

        private class CookieObj
        {
            public string Content
            {
                get;
                set;
            }

            public string Sign
            {
                get;
                set;
            }
        }
    }
}
