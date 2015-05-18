using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace TiQiu.Common.Serialization
{
    public static class JsonObjectSerializer
    {
        public static T FromJson<T>(string input)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(input);
        }

        public static string ToJsonString(this object entity)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(entity);
        }
    }
}
