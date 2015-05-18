using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.DAL;
using TiQiu.Web.WebPages.Utilities;

namespace TiQiu.Web.MasterPage
{
    public partial class Master : System.Web.UI.MasterPage
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string isBusiness;
        public string IsBusiness
        {
            get { return isBusiness; }
            set { isBusiness = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           name= this.Context.User.Identity.Name;
           isBusiness = SessionUtil.GetSession<string>("IsBusiness");
          
        }
    }
}