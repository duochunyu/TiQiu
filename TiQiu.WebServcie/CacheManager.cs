using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Caching;
using TiQiu.Common.Cryptography;
using TiQiu.Model;

namespace TiQiu.WebServcie
{
    public static class CacheManager
    {
        private static string PRIVATE_KEY = ConfigurationManager.AppSettings["PRIVATE_KEY"];

        public static string GenerateToken<T>(HttpContext context,T tokenEntity) where T : class
        {

            string token = context.Session.SessionID;//CryptoManager.GetCrypto(CryptoAlgorithm.SHA1).Encrypt(context.Session.SessionID + PRIVATE_KEY);
            DateTime absDate = DateTime.MaxValue;
            TimeSpan expireTime = new TimeSpan(12,0,0);
            SetCache(token, tokenEntity, absDate, expireTime);
            return token;
        }

        public static bool CheckToken(string token,bool isAccount)
        {
            var entity = GetCache<TokenEntity>(token);
            if (entity != null)
            {
                
                SetCache(token, entity,DateTime.MaxValue, new TimeSpan(12, 0, 0));
            }
            return entity != null && entity.IsAccount == isAccount;            
        }

        public static DateTime GetTimeStamp(string token)
        {
            var entity = GetCache<TokenEntity>(token);
            var stamp = DateTime.Now;
            if (entity != null)
            {
                stamp = entity.TimeStamp;
                entity.TimeStamp = DateTime.Now;
                SetCache(token, entity, DateTime.MaxValue, new TimeSpan(0, 10, 0));
            }
            return stamp;
        }

        public static List<int> GetFieldIds(string token)
        {
            var entity = GetCache<TokenEntity>(token);
            List<int> rlist = new List<int>();
            if (entity != null)
            {
                rlist = entity.Fields;
            }
            return rlist;
        }

        public static void SetCache(string key, object value,DateTime absDateTime,TimeSpan expireTime)
        {
            HttpRuntime.Cache.Insert(key, value, null, absDateTime, expireTime);
        }

        public static void RemoveCache(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        public static T GetCache<T>(string token)
        {
            return (T)HttpRuntime.Cache.Get(token);
        }
    }
}