using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TiQiu.Biz
{
    public static class SessionUtil
    {
        public static T GetSession<T>(string sessionName) where T : class
        {
            return HttpContext.Current.Session[sessionName] as T;
        }

        public static void SetSession(string sessionName, object data)
        {
            HttpContext.Current.Session[sessionName] = data;
        }

        public static void RemoveSession(string sessionName)
        {
            HttpContext.Current.Session[sessionName] = null;
            HttpContext.Current.Session.Remove(sessionName);
        }

        public static void AbandonSession()
        {
            HttpContext.Current.Session.Abandon();
        }
    }
}
