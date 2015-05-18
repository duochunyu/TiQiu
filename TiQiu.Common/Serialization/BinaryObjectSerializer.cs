using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TiQiu.Common.Serialization
{
    public static class BinaryObjectSerializer
    {
        //public static T LoadFromFile<T>(string filePath) where T : class
        //{
        //    FileStream fs = null;
        //    try
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(T));
        //        fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        //        return (T)serializer.Deserialize(fs);
        //    }
        //    finally
        //    {
        //        if (fs != null)
        //        {
        //            fs.Close();
        //            fs.Dispose();
        //        }
        //    }
        //}

        //public static void SaveAsFile<T>(T obj, string filePath) where T : class
        //{
        //    FileStream fs = null;
        //    try
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(T));
        //        fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        //        serializer.Serialize(fs, obj);
        //        fs.Flush();
        //    }
        //    finally
        //    {
        //        if (fs != null)
        //        {
        //            fs.Close();
        //            fs.Dispose();
        //        }
        //    }
        //}

        public static Stream Serialize(object obj)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            formatter.Serialize(stream, obj);
            stream.Position = 0;
            return stream;
        }

        public static T Deserialize<T>(Stream stream) where T : class
        {
            stream.Position = 0;
            IFormatter formatter = new BinaryFormatter();
            return (T)formatter.Deserialize(stream);
        }

        public static T LoadFromFile<T>(string filePath) where T : class
        {
            FileStream fs = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return (T)formatter.Deserialize(fs);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        public static void SaveAsFile<T>(T obj, string filePath) where T : class
        {
            FileStream fs = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                formatter.Serialize(fs, obj);
                fs.Flush();
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }
    }
}
