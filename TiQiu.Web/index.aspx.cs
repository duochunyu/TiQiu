using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.Biz;
using TiQiu.DAL;

namespace TiQiu.Web
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FIELD_ITEM field2 = new FIELD_ITEM()
            {
                NAME = "22222",
                FIELD_ID = 2
            };

            //int totalCount = 0;
            //Expression<Func<field, bool>> express = PredicateExtensionses.True<field>();

            //DateTime starttime = DateTime.Now;
            //DateTime endtime = new DateTime();
            //endtime = starttime.AddDays(7);
            //List<EnumFieldType> selectedType = new List<EnumFieldType>();
            //selectedType.Add(EnumFieldType.Five);
            //var list = FieldManager.GetFieldList(null, null,
            //    selectedType, starttime, endtime, 0, 99999, "ID", false, 0, 5, out totalCount);
            //this.fieldsinfos.DataSource = list;
        }
    }
}