using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.DAL;
using TiQiu.Web.WebPages.Utilities;

namespace TiQiu.Web.UserControl
{
    public partial class FieldInfosIndexControl : System.Web.UI.UserControl
    {
        public List<FIELD> DataSource { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }



    }
}