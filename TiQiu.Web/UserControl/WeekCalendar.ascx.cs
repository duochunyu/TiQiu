using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TiQiu.Web.UserControl
{
    public partial class WeekCalendar : System.Web.UI.UserControl
    {

        public string ClientCalendarDivID { get; set; }
        

        protected void Page_Load(object sender, EventArgs e)
        {
            //register css/script resoures into the <header>
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "calendar"))
            {
                if (this.Page.Header != null)
                {
                    this.Page.Header.Controls.Add(new LiteralControl(
                        string.Format("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" /> {1}",
                        "http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/smoothness/jquery-ui.css", "\r\n")));
                    this.Page.Header.Controls.Add(new LiteralControl(
                       string.Format("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />{1}",
                       "../Styles/jquery.weekcalendar.css", "\r\n")));
                    
                    this.Page.Header.Controls.Add(new LiteralControl(
                        string.Format("<script type=\"text/javascript\" language=\"javascript\" src=\"{0}\"></script>{1}",
                        "http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js", "\r\n")));
                    this.Page.Header.Controls.Add(new LiteralControl(
                        string.Format("<script type=\"text/javascript\" language=\"javascript\" src=\"{0}\"></script>{1}",
                        "../Scripts/calendar/jquery.weekcalendar.js", "\r\n")));
                }
            }

        }


    }
}