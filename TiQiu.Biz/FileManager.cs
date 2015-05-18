using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiQiu.DAL;
using System.Configuration;
using System.Web;
using System.IO;

namespace TiQiu.Biz
{
    public static class FileManager
    {
        public static string FILE_ROOT = ConfigurationSettings.AppSettings["FILE_ROOT"];

        public static string FILE_DOMAIN = ConfigurationSettings.AppSettings["FILE_DOMAIN"];

        public static string DEFAULT_IMAGE = ConfigurationSettings.AppSettings["DEFAULT_IMAGE"];
        
        public static List<FILE> GetFileList(EnumFileType fType, int fkId)
        {
            List<FILE> rel = new List<FILE>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = context.FILEs.Where(s => s.TYPE == (int)fType && s.FK_ID == fkId).OrderBy(s => s.ORDER).OrderByDescending(s=>s.UPLOAD_DATE).ToList();
            }
            return rel;
        }

        public static List<FILE> GetIndexSliderList()
        {
            List<FILE> rel = new List<FILE>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = context.FILEs.Where(s => s.TYPE == (int)EnumFileType.Slider_Pic ).OrderBy(s => s.ORDER).ToList();
            }
            return rel;
        }

        public static string GetMemberPic(int memberId)
        {
            string filePath = string.Empty;
            List<FILE> fl = GetFileList(EnumFileType.Member_Portrait, memberId);
            if (fl != null && fl.Count > 0)
            {
                filePath = FILE_DOMAIN + fl[0].PATH;
            }
            if (filePath.Length == 0) filePath = FILE_DOMAIN + DEFAULT_IMAGE;
            return filePath;
        }

        public static string GetTeamLogo(int teamId)
        {
            string filePath = string.Empty;
            List<FILE> fl = GetFileList(EnumFileType.Team_Pic, teamId);
            if (fl != null && fl.Count > 0)
            {
                filePath = FILE_DOMAIN + fl[0].PATH;
            }
            if (filePath.Length == 0) filePath = FILE_DOMAIN + DEFAULT_IMAGE;
            return filePath;
        }

        public static string GetGameLogo(int gameId)
        {
            string filePath = string.Empty;
            List<FILE> fl = GetFileList(EnumFileType.Game_Logo, gameId);
            if (fl != null && fl.Count > 0)
            {
                filePath = FILE_DOMAIN + fl[0].PATH;
            }
            if (filePath.Length == 0) filePath = FILE_DOMAIN + DEFAULT_IMAGE;
            return filePath;
        }

        public static string GetFieldLogo(int fieldId)
        {
            string filePath = string.Empty;
            List<FILE> fl = GetFileList(EnumFileType.Field_Logo, fieldId);
            if (fl != null && fl.Count > 0)
            {
                filePath = FILE_DOMAIN + fl[0].PATH;
            }
            if (filePath.Length == 0) filePath = FILE_DOMAIN + DEFAULT_IMAGE;
            return filePath; 
        }        
        
        public static string UploadFiled(HttpPostedFile httpPostFile,string fileExt, EnumFileType fType, int fkId,int memberId = 0,int order = 0)
        {
            
            bool check = false;
            string helpmsg = string.Empty;
            string name = DateTime.Now.ToString("yyyyMMddHHmmsss") + fileExt;
            string filePath = GenerateFilePath(fType, fkId);
            string fileNewName = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + fileExt;
            if (!Directory.Exists(FILE_ROOT + filePath))
            {
                Directory.CreateDirectory(FILE_ROOT + filePath);
            }
            httpPostFile.SaveAs(FILE_ROOT + filePath + fileNewName);
            using (TIQIUEntities context = new TIQIUEntities())
            {
                
                switch (fType)
                {
                    case EnumFileType.Member_Portrait :
                        var member = context.MEMBER.SingleOrDefault<MEMBER>(m => m.ID == fkId);
                        if (member == null)
                        {
                            helpmsg = "会员编码错误！";
                        }
                        else
                        {
                            check = true;
                        }
                        break;
                    case EnumFileType.Team_Pic :
                        var team = context.TEAMs.SingleOrDefault<TEAM>(t => t.ID == fkId);
                        if (team == null) { helpmsg = "球队编码错误！"; }
                        else
                        {
                            check = true;
                        }
                        break;
                    case EnumFileType.Game_Pic:
                        var game = context.GAME_EVENT.SingleOrDefault<GAME_EVENT>(t => t.ID == fkId);
                        if (game == null) { helpmsg = "比赛编码错误！"; }
                        else
                        {
                            check = true;
                        }
                        break;
                    default:
                        break;
                }
                if (check)
                {
                    FILE file = new FILE()
                    {
                        FILE_NAME = name,
                        FILE_EXT = fileExt,
                        FK_ID = fkId,
                        TYPE = (int)fType,
                        PATH = filePath + fileNewName,
                        ORDER = order,
                        SIZE = httpPostFile.ContentLength,
                        TITLE = httpPostFile.FileName,
                        UPLOAD_DATE = DateTime.Now,
                        UPLOAD_MEMBER_ID = memberId
                    };
                    context.FILEs.Add(file);
                    context.SaveChanges();
                }
            }
            if (!check) throw new ApplicationException(helpmsg);

            return FILE_DOMAIN + filePath + fileNewName;

        }

        private static string GenerateFilePath(EnumFileType fType, int fkId)
        {
            return fType.ToString()+ "/" + fkId + "/";
        }

        public static void UpdateMeberPic(EnumFileType fType, int fkId, string filePath)
        {

            using (TIQIUEntities context = new TIQIUEntities())
            {
                var list = context.FILEs.Where(s => s.TYPE == (int)fType && s.FK_ID == fkId).OrderBy(s => s.ORDER).ToList();

                if (list.Count == 0)
                {
                    FILE entity = new FILE();
                    entity.TYPE = (int)fType;
                    entity.FK_ID = fkId;
                    entity.PATH = filePath;
                    context.FILEs.Add(entity);
                    context.SaveChanges();
                }
                else
                {
                    FILE f = context.FILEs.Single<FILE>(a => a.ID == list[0].ID);
                    f.PATH = filePath;
                    context.SaveChanges();
                }
            }
        }

    }
}
