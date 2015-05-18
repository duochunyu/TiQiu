using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.Biz;
using TiQiu.DAL;
using TiQiu.Web.UserControl;
using TiQiu.Web.WebPages.Utilities;

namespace TiQiu.Web.WebPages
{
    public partial class MyTeam : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IsLogin)
                {
                    BindData();
                }
            }
        }

        #region 球队详情

        private void BindData()
        {
            ////根据用户名获取球队信息

            string userName = CurAccount.NAME;
            TEAM entity = new TEAM();// TeamManager.GetTeamDetailInfo()
            this.txtTeanName.Text = entity.NAME;//球队名称
            this.txtCreateYear.Text = null;//球队创建时间
            this.TeamImage.ImageUrl = null;//球队队徽
            this.txtDescription.Text = null;//球队简介
            int totalCount = 0;
            //获取球队队员
            Expression<Func<MEMBER, bool>> express = PredicateExtensionses.True<MEMBER>();
            if (entity != null)
            {
                express = express.And(s => s.ID == entity.ID);
                this.TeamerInfoList.DataSource = MemberManager.GetMemberList(express, 0, 5, out totalCount);
            }


        }

        protected void btnSave_Click(object sener, EventArgs e)
        {
            string userName = CurAccount.NAME;
            TEAM entity = null;
            TEAM entityCcreate = new TEAM();
            entityCcreate.NAME = this.txtTeanName.Text.Trim();
            entityCcreate.BRIEF = this.txtDescription.Text.Trim();

            //创建球队

            if (entity == null)
            {
                TeamManager.CreateTeam(entityCcreate);
            }
            //编辑球队信息
            else
            {
                TeamManager.UpdateTeamInfoDetails(entityCcreate);
            }

        }

        #endregion

        protected void lbtnUpdateImage_Click(object sender, EventArgs e)
        {
            lbtnUpdateImage.Visible = true;
            dUpload.Visible = false;
        }


        protected void uploadImage_UploadImageCompleted(object sender, UploadImageEventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "tip", "alert('上传成功');", true);
            lbtnCancelImage_Click(null, null);
        }

        protected void lbtnCancelImage_Click(object sender, EventArgs e)
        {
            lbtnUpdateImage.Visible = true;
            dUpload.Visible = false;

        }
    }
}