using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    public string version = ConfigurationManager.AppSettings["VERSION"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}