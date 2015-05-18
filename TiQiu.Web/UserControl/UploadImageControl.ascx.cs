using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.Biz;

namespace TiQiu.Web.UserControl
{

    public partial class UploadImageControl : System.Web.UI.UserControl
    {
        public event UploadImageCompleted UploadImageCompleted;
        public EnumFileType FileType { get; set; }
        public string FilePath { get; set; }
        private string path;
        public double Width
        {
            get
            {
                return fuImage.Width.Value + btnUpload.Width.Value;
            }
            set
            {
                fuImage.Width = new Unit(value - btnUpload.Width.Value);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilePath))
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp");
                FilePath = "Temp/";
            }
            else
            {
                path = Server.MapPath(FilePath);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fuImage.HasFile)
            {
                try
                {
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(fuImage.FileName);
                    string fullFile = string.Concat(fileName, ".", extension);
                    fuImage.SaveAs(Path.Combine(path, fullFile));
                    int fileID = 0;
                    //FileManager.
                    if (UploadImageCompleted != null)
                    {
                        UploadImageCompleted(this, new UploadImageEventArgs
                        {
                            FileID = fileID,
                            FilePath = string.Concat(FilePath, ".", extension)
                        });
                    }
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "alert('上传失败');", true);
                }

            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "alert('请选择上传的文件');", true);
            }
        }
    }

    public delegate void UploadImageCompleted(object sender, UploadImageEventArgs e);

    public class UploadImageEventArgs : EventArgs
    {
        public int FileID { get; set; }
        public string FilePath { get; set; }
    }

}