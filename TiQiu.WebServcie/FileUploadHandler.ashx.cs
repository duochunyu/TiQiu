using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TiQiu.Biz;


namespace TiQiu.WebServcie
{
    /// <summary>
    /// FileUpload 的摘要说明
    /// </summary>
    public class FileUploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Message msg = new Message(){ Result = 0 };
            try
            {

                context.Response.ContentType = "text/plain";
                string action = Util.GetParamter<string>(context.Request, "action");
                int fkId = Util.GetParamter<int>(context.Request, "fkId");
                int memberId = Util.GetParamter<int>(context.Request, "memberid",false);
                if (context.Request.Files.Count != 1) throw new ApplicationException("未提供文件或上传文件数量为多个，上传失败！"); 
                HttpPostedFile hpf = context.Request.Files[0];
                Log.WriteBizLog(string.Format("File upload:{0}-{1}-{2}-{3}",fkId,memberId,hpf.FileName,hpf.ContentLength));
                if (hpf == null) throw new ApplicationException("请提供上传文件对象：fileObject");
                string filePath = string.Empty;
                switch (action.ToUpper())
                {
                    case "PORTRAIT":
                        filePath = FileManager.UploadFiled(hpf, GetFileExt(hpf.FileName), EnumFileType.Member_Portrait, fkId, memberId);
                        break;
                    case "TEAMLOGO":
                        filePath = FileManager.UploadFiled(hpf, GetFileExt(hpf.FileName), EnumFileType.Team_Pic, fkId, memberId);
                        break;
                    case "GAMEPIC":
                        filePath = FileManager.UploadFiled(hpf, GetFileExt(hpf.FileName), EnumFileType.Game_Pic, fkId, memberId);
                        break;
                }
                msg = new Message
                {
                    Result = 1,
                    Data = filePath
                };
            }
            catch (ApplicationException ax)
            {
                msg = new Message
                {
                    Result = 0,
                    HelpMessage = ax.Message
                };
            }
            catch (Exception ex)
            {
                msg = new Message
                {
                    Result = 0,
                    HelpMessage = ex.Message
                };
            }
            context.Response.Write(JsonConvert.SerializeObject(msg));
        }

        private string GetFileExt(string fileName)
        {
            return fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf("."));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}