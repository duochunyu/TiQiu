using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Text;

namespace TiQiu.WebServcie
{
    public static class Log
    {
        private static string s_folderPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Log");
        private static string m_ExceptionLogFilePrefix = "ExceptionLog_";
        private static string m_BizLogFilePrefix = "BizLog_";
        private static readonly object obj = new object();

        public static void ClearHistoryLog(double days)
        {
            try
            {
                string folderPath = s_folderPath;
                if (!Directory.Exists(folderPath))
                {
                    return;
                }
                DirectoryInfo directory = new DirectoryInfo(folderPath);
                FileInfo[] files = directory.GetFiles();
                foreach (FileInfo file in files)
                {
                    if (file.LastWriteTime.Date.AddDays(days) < DateTime.Today)
                    {
                        file.Delete();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 写入Windows Event Log
        /// </summary>
        /// <param name="systemlogSource">Windows Log事件源</param>
        /// <param name="systemlogName">log名</param>
        /// <param name="log">log内容</param>
        public static void WriteWindowsEventLog(string systemlogSource, string systemlogName, string log)
        {
            if (!string.IsNullOrEmpty(systemlogSource) && !string.IsNullOrEmpty(systemlogName))
            {
                if (!EventLog.Exists(systemlogName))
                {
                    EventLog.CreateEventSource(systemlogSource, systemlogName);
                }
                EventLog elog = new EventLog();
                elog.Log = systemlogName;
                elog.Source = systemlogSource;
                elog.WriteEntry(log, EventLogEntryType.Information);
                elog.Close();
            }
        }

        /// <summary>
        /// 记录异常Log到ExceptionLog_yyyyMMdd.txt文件
        /// </summary>
        /// <param name="errorMsg">log内容</param>
        public static void WriteException(string errorMsg)
        {
            try
            {
                if (string.IsNullOrEmpty(errorMsg))
                {
                    return;
                }
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("\r\n[{0}]: Error Message Begin  -------------------\r\n", DateTime.Now.ToString());
                sb.AppendFormat("Message : {0}\r\n", errorMsg);
                sb.AppendFormat("----------- Error Message End  -------------------------------------\r\n");
                WriteToFile(sb.ToString(), EventLogEntryType.Error);
            }
            catch { }
        }

        /// <summary>
        /// 记录异常Log到ExceptionLog_yyyyMMdd.txt文件
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static void WriteException(Exception ex)
        {
            try
            {
                if (ex == null)
                {
                    return;
                }
                WriteToFile(GetExceptionMessage(ex), EventLogEntryType.Error);
            }
            catch { }
        }

        /// <summary>
        /// 记录一些业务Log到BizLog_yyyyMMdd.txt文件
        /// </summary>
        /// <param name="errorMsg">log内容</param>
        public static void WriteBizLog(string logMsg)
        {
            try
            {
                if (string.IsNullOrEmpty(logMsg))
                {
                    return;
                }
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}]: {1}  \r\n", DateTime.Now.ToString(), logMsg);
                WriteToFile(sb.ToString(), EventLogEntryType.Information);
            }
            catch { }
        }

        /// <summary>
        /// 获取异常内容
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <returns>异常内容</returns>
        public static string GetExceptionMessage(Exception ex)
        {
            if (ex == null)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("\r\n[{0}]: Error Message Begin  -------------------\r\n", DateTime.Now.ToString());
            sb.AppendFormat("ExceptionType : {0}\r\n", ex.GetType().FullName);
            sb.AppendFormat("Message : {0}\r\n", ex.Message);
            sb.AppendFormat("StackTrace :\r\n{0}\r\n", ex.StackTrace);
            if (ex.InnerException != null)
            {
                AppendInnerExceptionInfo(sb, ex.InnerException, "");
            }

            sb.AppendFormat("\r\n--------- Exception Information [End] ----------------------\r\n");
            return sb.ToString();
        }

        private static void AppendInnerExceptionInfo(StringBuilder sb, Exception ex, string prefix)
        {
            sb.AppendFormat("{0}InnerException :\r\n", prefix);
            prefix = prefix + "  ";
            sb.AppendFormat("{0}ExceptionType : {1}\r\n", prefix, ex.GetType().FullName);
            sb.AppendFormat("{0}Message : {1}\r\n", prefix, ex.Message);
            sb.AppendFormat("{0}StackTrace :\r\n{1}\r\n", prefix, ex.StackTrace);
            if (ex.InnerException != null)
            {
                AppendInnerExceptionInfo(sb, ex.InnerException, prefix);
            }
        }

        #region WriteToFile

        /// <summary>
        /// 写入文件log
        /// </summary>
        /// <param name="log">log内容</param>
        /// <param name="type">log类型</param>
        private static void WriteToFile(string log, EventLogEntryType type)
        {
            string filePath = string.Empty;
            if (type == EventLogEntryType.Error)
            {
                filePath = Path.Combine(s_folderPath, m_ExceptionLogFilePrefix + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
            }
            else
            {
                filePath = Path.Combine(s_folderPath, m_BizLogFilePrefix + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
            }

            if (!Directory.Exists(s_folderPath))
            {
                Directory.CreateDirectory(s_folderPath);
            }
            byte[] textByte = System.Text.Encoding.UTF8.GetBytes(log);
            lock (obj)
            {
                using (FileStream logStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.Write))
                {
                    logStream.Write(textByte, 0, textByte.Length);
                    logStream.Close();
                }
            }
        }


        #endregion
    }
}