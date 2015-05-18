using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.DAL;

namespace TiQiu.Web.UserControl.TeamInfoUserControl
{
    public partial class TeamListUserControl : System.Web.UI.UserControl
    {
        public List<TEAM> DataSource { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}