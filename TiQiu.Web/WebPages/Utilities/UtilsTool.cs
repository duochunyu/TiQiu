using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TiQiu.Web.WebPages.Utilities
{
    public class UtilsTool
    {
        public static string StringSplit(string detail, int len)
        {
            if (detail.Length > len)
            {
                return detail.Substring(0, len) + "...";
            }
            return detail;
        }

        public static string FomatDate(DateTime? date)
        {
            return date.Value.ToString("yyyy-MM-dd");
        }

        public static DateTime FilterDate(DateTime date)
        {
            return Convert.ToDateTime(date.ToShortDateString() + " 11:59:59");
        }

        private static byte[] s_DesIV = new byte[] { 0x1d, 0x87, 0x34, 9, 0x41, 3, 0x61, 0x62 };
        private static byte[] s_DesKey = new byte[] { 1, 0x4d, 0x54, 0x22, 0x45, 90, 0x17, 0x2c };

        public static string Decrypt(string encryptedBase64ConnectString)
        {
            if (string.IsNullOrEmpty(encryptedBase64ConnectString)) return string.Empty;
            MemoryStream stream = new MemoryStream(200);
            stream.SetLength(0L);
            byte[] buffer = Convert.FromBase64String(encryptedBase64ConnectString);
            DES des = new DESCryptoServiceProvider();
            des.KeySize = 0x40;
            CryptoStream stream2 = new CryptoStream(stream, des.CreateDecryptor(s_DesKey, s_DesIV), CryptoStreamMode.Write);

            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            stream.Flush();
            stream.Seek(0L, SeekOrigin.Begin);
            byte[] buffer2 = new byte[stream.Length];
            stream.Read(buffer2, 0, buffer2.Length);
            stream2.Close();
            stream.Close();
            return Encoding.Unicode.GetString(buffer2);
        }
    }
}