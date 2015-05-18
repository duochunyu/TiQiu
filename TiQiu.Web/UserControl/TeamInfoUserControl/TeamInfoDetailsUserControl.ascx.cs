using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.DAL;

namespace TiQiu.Web.UserControl.TeamInfoUserControl
{
    public partial class TeamInfoDetailsUserControl : System.Web.UI.UserControl
    {
        public List<TEAM> entity { get; set; }


        public string Name { get; set; }

        public string BRIEF { get; set; }

        public string ImageUrl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
          
        }
    }
}