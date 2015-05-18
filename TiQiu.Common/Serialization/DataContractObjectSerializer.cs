using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using System.IO;

namespace TiQiu.Common.Serialization
{
    public static class DataContractObjectSerializer
    {
        public static string Serialize<T>(T obj) where T : class
        {
            DataContractSerializer dcs = new DataContractSerializer(typeof(T));
            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                dcs.WriteObject(writer, obj);
            }
            return sb.ToString();

        }

        public static string Serialize(object instance)
        {
            DataContractSerializer dcs = new DataContractSerializer(instance.GetType());
            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                dcs.WriteObject(writer, instance);
            }
            return sb.ToString();
        }

        public static T Deserialize<T>(string xmlStr) where T : class
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            StringBuilder sb = new StringBuilder();
            object result;
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlStr)))
            {
                result = serializer.ReadObject(reader);
            }
            if (result != null)
            {
                return result as T;
            }
            return null;
        }
    }
}
