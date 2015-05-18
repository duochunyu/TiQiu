using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.DAL;

namespace TiQiu.Web.UserControl
{
    public partial class BookingInfosIndexControl : System.Web.UI.UserControl
    {
        public List<FIELD_ORDER> DataSource { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}