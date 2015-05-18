using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TiQiu.Common.Util
{
    public class HttpHelper
    {
        public static string Post(string URI, byte[] postData)
        {
            HttpWebRequest request = WebRequest.Create(URI) as HttpWebRequest;
            request = WebRequest.Create(URI) as HttpWebRequest;
            request.Method = "POST";
            request.KeepAlive = true;
            request.ContentType = "	application/x-www-form-urlencoded; charset=UTF-8";
            request.ContentLength = postData.Length;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:20.0) Gecko/20100101 Firefox/20.0";

            // 提交请求数据
            System.IO.Stream outputStream = request.GetRequestStream();
            outputStream.Write(postData, 0, postData.Length);
            outputStream.Close();
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    System.IO.Stream responseStream = response.GetResponseStream();
                    System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, Encoding.GetEncoding("GB2312"));
                    return reader.ReadToEnd();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public static string GetString(string url)
        {

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.KeepAlive = false;
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        System.IO.Stream responseStream = response.GetResponseStream();
                        System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, Encoding.GetEncoding("GB2312"));
                        return reader.ReadToEnd();
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

    }
}
