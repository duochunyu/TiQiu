using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace TiQiu.Common.Util
{
    public class StringHelper
    {
        public static string MakeSafeSql(string inputSQL)
        {
            string s = inputSQL;
            s = inputSQL.Replace("'", "''");
            s = s.Replace("[", "[[]");
            s = s.Replace("%", "[%]");
            //s = s.Replace("_", "[_]");
            return s;
        }

        public static bool IsNullOrEmpty(string str)
        {
            return (str == null || str.Trim() == string.Empty);
        }

        
        // Created By Swika 2008-6-5
        /// <summary>
        /// Concatenates a specified separator System.String between each 
        /// element of a specified array, yielding a single concatenated 
        /// string.
        /// </summary>
        /// <typeparam name="T">Array member type.</typeparam>
        /// <param name="separator">A System.String</param>
        /// <param name="value">An array</param>
        /// <returns>A System.String consisting of the elements of value 
        /// interspersed with the separator string.</returns>
        public static string Join<T>(string separator, T[] value) 
        {
            if(value==null|| value.Length==0)
                return string.Empty;

            StringBuilder sbOut = new StringBuilder();

            for (int i = 0; i < value.Length; i++)
            {
                sbOut.Append(value[i].ToString());
                // If current not last postion, then append separator.
                if (i < value.Length - 1)
                    sbOut.Append(separator);
            }

            return sbOut.ToString();
        }

        public static string DecodeBase64(string codeType, string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.GetEncoding(codeType).GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }

        public static string ConvertToSplitString(IEnumerable source, string separator)
        {
            if (source != null)
            {
                StringBuilder output = new StringBuilder();
                IEnumerator enumerator = source.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    output.AppendFormat("{0}{1}",enumerator.Current.ToString(),separator);
                }
                if (output.Length > 0)
                {
                    output.Remove(output.Length - separator.Length, separator.Length);
                }
                return output.ToString();
            }
            return string.Empty;
            

        }

        public static bool CompareArray<T>(T[] arry1,T[] arry2) 
        {
            if (arry1.Length != arry2.Length)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < arry1.Length; i++)
                {
                    if (arry1[i].ToString() != arry2[i].ToString())
                    {
                        return false;
                    }
                }
                return true;
            }

        }
        
        public static bool CompareArray(Array array1,Array array2)
        {
            //比较类型是否一样 
            if (!Object.ReferenceEquals(array1.GetType(), array2.GetType()))
            {
                return false;
            }
            //比较长度是否一样 
            if (array1.GetLength(0) != array2.GetLength(0))
            {
                return false;
            }
            //比较成员是否对应相等 
            ValueType v1, v2;
            for (int i = 0; i < array1.GetLength(0); i++)
            {
                v1 = (ValueType)array1.GetValue(i);
                v2 = (ValueType)array2.GetValue(i);
                if (!v1.Equals(v2))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 将javascript中的特殊字符进行处理
        /// </summary>
        /// <param name="s">输入字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string TransferJavascriptChar(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            StringBuilder ret = new StringBuilder();
            foreach (char c in s)
            {
                switch (c)
                {
                    case '\r':
                    case '\t':
                    case '\n':
                    case '\f':
                    case '\v':
                    case '\"':
                    case '\\':
                    case '\'':
                    case '<':
                    case '>':
                        ret.AppendFormat("\\u{0:X4}", (int)c);
                        break;
                    default:
                        ret.Append(c);
                        break;
                }
            }
            return ret.ToString();
        }
    
    }


}
