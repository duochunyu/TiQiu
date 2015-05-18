using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.Biz;
using TiQiu.DAL;

namespace TiQiu.Web.UserControl
{
    public partial class AddTeamerListUserControl : System.Web.UI.UserControl
    {
        public List<MEMBER> DataSource { get; set; }
        public List<ACCOUNT> DataSource_Add { get; set; } 
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            Expression<Func<ACCOUNT, bool>> expression = PredicateExtensionses.True<ACCOUNT>();
               expression = expression.And(s => s.NAME.Contains(this.txtAccountName.Value.Trim()));
            int totolCount = 0;
            DataSource_Add = AccountManager.GetAccountList(expression, "ID", false, 0, 5, out totolCount);
        }
    }
}