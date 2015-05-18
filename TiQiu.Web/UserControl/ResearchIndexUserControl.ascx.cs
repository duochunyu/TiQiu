using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.Biz;

namespace TiQiu.Web.UserControl
{
    public partial class ResearchIndexUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rblFieldType.DataSource = ConstValue.GetFieldType();
                rblFieldType.DataTextField = "Key";
                rblFieldType.DataValueField = "Value";
                rblFieldType.DataBind();

                rptArea.DataSource = ConstValue.GetAreaList(true);
                rptArea.DataBind();
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            var type = rblFieldType.SelectedValue;
            var fieldName = fieldname.Value.Trim();
            var area = ddlArea.Value;
            Response.Redirect(string.Format("/WebPages/Findfield.aspx?type={0}&fieldname={1}&area={2}", type, fieldName, area));
        }
    }
}