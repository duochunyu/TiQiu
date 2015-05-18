using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.DAL;
using TiQiu.Biz;
using System.Data.Entity;

namespace TiQiu.Web.UserControl.Banner
{
    public partial class IndexBanner : System.Web.UI.UserControl
    {
        public List<FILE> files = new List<FILE>();

        public string SliderID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                files = FileManager.GetFileList(EnumFileType.Slider_Pic, 0);
            }            

            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "indexslider"))
            {
                if (this.Page.Header != null)
                {
                    this.Page.Header.Controls.Add(new LiteralControl(
                        @"<link rel='stylesheet' id='cameracss' href='../Styles/slider/camera.css' type='text/css' media='all' />\r\n
    <script type='text/javascript' src='../Styles/slider/jquery.mobile.customized.min.js'></script>\r\n
    <script type='text/javascript' src='../Styles/slider/jquery.easing.1.3.js'></script>\r\n
    <script type='text/javascript' src='../Styles/slider/camera.min.js'></script>\r\n"));
                }
            }

                
        }
    }
}