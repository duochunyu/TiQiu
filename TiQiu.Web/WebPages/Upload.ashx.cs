using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using TiQiu.Web.WebPages.XiuXiuStream;

namespace TiQiu.Web.WebPages
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class Upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //config 配置节点可以将图片保存至指定目录，未配置将保存至 /XiuXiuUpload
            //<appSettings>
            //  <add key="XiuXiuImageSavePath" value="/upload"/>
            //</appSettings>
            string name = null;
            if (context.Request.TotalBytes > 0)
            {
                XiuXiuStreamImage img = new XiuXiuStreamImage(context);
                name = img.Save();
            }
            else
            {
                name = "非法访问";
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(name);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    namespace XiuXiuStream
    {

        /// <summary>
        /// 上传抽象类
        /// </summary>
        public abstract class XiuXiuImage
        {
            /// <summary>
            /// 基类保存
            /// </summary>
            /// <returns>返回保存路径+文件名</returns>
            public virtual string Save()
            {
                string fileName = this.GetFileName();
                if (null == fileName) return null;

                string root = HttpContext.Current.Server.MapPath(path);

                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }

                this.FileName = Path.Combine(root, fileName);
                string[] paths = { path, fileName };
                return string.Join("/", paths);
            }

            public XiuXiuImage()
            {
                path = path == null ? "/XiuXiuUpload" : path;
            }

            /// <summary>
            /// 确定上传类型
            /// </summary>
            protected bool IsUplodType
            {
                get
                {
                    string extension = this.GetExtension();
                    return ".jpg .gif .png .icon .bmp .tiff .wmf .emf .exif".IndexOf(extension) >= 0 ? true : false;
                }
            }
            private string _fileName = null;
            /// <summary>
            /// 最终保存路径
            /// </summary>
            protected string FileName
            {
                set { _fileName = value; }
                get { return _fileName; }
            }

            /// <summary>
            /// 配置文件路径 无配置上传到XiuXiuUpload
            /// </summary>
            protected string path = ConfigurationManager.AppSettings["FileVirtualPath"].TrimEnd('/');

            /// <summary>
            /// 获取拓展名
            /// </summary>
            /// <returns></returns>
            protected abstract string GetExtension();

            /// <summary>
            /// 获取最终保存文件名
            /// </summary>
            /// <returns></returns>
            protected string GetFileName()
            {
                string extension = this.GetExtension();
                if (null == extension) return null;
                else
                {
                    string name = this.GetName();
                    string[] imgName = { "XiuXiu", name, extension };
                    return string.Join("", imgName);
                }
            }
            /// <summary>
            /// 获取保存文件名
            /// </summary>
            /// <returns></returns>
            private string GetName()
            {
                DateTime uploadTime = DateTime.Now;
                string[] times = { uploadTime.Year.ToString(), uploadTime.Month.ToString(), uploadTime.Day.ToString(),
                                 uploadTime.Hour.ToString(), uploadTime.Millisecond.ToString(), uploadTime.Second.ToString() };
                return string.Join("", times);
            }
        }
        /// <summary>
        /// Stream接收
        /// </summary>
        public sealed class XiuXiuStreamImage : XiuXiuImage
        {
            private MemoryStream stream = null;

            public XiuXiuStreamImage(HttpContext context)
            {
                int count = context.Request.TotalBytes;
                if (count > 0)
                {
                    byte[] bytes = context.Request.BinaryRead(context.Request.TotalBytes);
                    this.stream = new MemoryStream(bytes);
                }
            }

            private Image File
            {
                get
                {
                    return this.stream == null ? null : Image.FromStream(this.stream);
                }
            }
            /// <summary>
            /// 保存图片,成功返回文件路径,失败null
            /// 非图片格式返回错误信息
            /// </summary>
            /// <returns>成功返回文件路径 失败null</returns>
            public override string Save()
            {
                if (!this.IsUplodType)
                {
                    this.stream.Dispose();
                    return "Only allowed to upload pictures.";
                }
                string returnName = base.Save();
                if (this.FileName != null)
                {
                    this.File.Save(this.FileName);
                    this.stream.Dispose();
                    return returnName;
                }
                return null;
            }

            protected override string GetExtension()
            {
                if (this.File != null)
                {
                    string fileExtension = this.File.RawFormat.ToString().Substring(14),
                           jpgExtension = System.Drawing.Imaging.ImageFormat.Jpeg.Guid.ToString(),
                           gifExtension = System.Drawing.Imaging.ImageFormat.Gif.Guid.ToString(),
                           pngExtension = System.Drawing.Imaging.ImageFormat.Png.Guid.ToString(),
                           iconExtension = System.Drawing.Imaging.ImageFormat.Icon.Guid.ToString(),
                           bmpExtension = System.Drawing.Imaging.ImageFormat.Bmp.Guid.ToString(),
                           tiffExtension = System.Drawing.Imaging.ImageFormat.Tiff.Guid.ToString(),
                           wmfExtension = System.Drawing.Imaging.ImageFormat.Wmf.Guid.ToString(),
                           emfExtension = System.Drawing.Imaging.ImageFormat.Emf.Guid.ToString(),
                           exifExtension = System.Drawing.Imaging.ImageFormat.Exif.Guid.ToString();
                    fileExtension = fileExtension.Substring(0, fileExtension.Length - 1);
                    if (fileExtension == jpgExtension)
                    {
                        return ".jpg";
                    }
                    else if (fileExtension == gifExtension)
                    {
                        return ".gif";
                    }
                    else if (fileExtension == pngExtension)
                    {
                        return ".png";
                    }
                    else if (fileExtension == iconExtension)
                    {
                        return ".icon";
                    }
                    else if (fileExtension == bmpExtension)
                    {
                        return ".bmp";
                    }
                    else if (fileExtension == tiffExtension)
                    {
                        return ".tiff";
                    }
                    else if (fileExtension == wmfExtension)
                    {
                        return ".wmf";
                    }
                    else if (fileExtension == emfExtension)
                    {
                        return ".emf";
                    }
                    else if (fileExtension == exifExtension)
                    {
                        return ".exif";
                    }
                }
                return null;
            }
        }
    }
}