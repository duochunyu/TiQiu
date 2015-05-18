using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.Biz;
using TiQiu.DAL;

namespace TiQiu.Web.WebPages
{
    public partial class FieldRuleManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Expression<Func<FIELD_ITEM, bool>> condition = PredicateExtensionses.True<FIELD_ITEM>();
                //condition = condition.And(f=>f.FIELD_ID == 
                int totalCount;
                List<FIELD_ITEM> fi = FieldManager.GetFieldItemList(condition, "NAME", true, 1, 100, out totalCount);
                fi.ForEach(f => { menuField.Items.Add(new MenuItem(f.NAME, f.ID.ToString())); 
                    });
                menuField.MenuItemClick +=menuField_MenuItemClick;
                menuField.Items[0].Selected = true;
            }
        }

        private void menuField_MenuItemClick(object sender, MenuEventArgs e)
        {
            WeekCalendar1.ClientCalendarDivID = ((MenuItem)sender).Text + ((MenuItem)sender).Value;
        }
    }
}