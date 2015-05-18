using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.Biz;
using TiQiu.DAL;
using TiQiu.Web.UserControl;
using TiQiu.Web.WebPages.Utilities;

namespace TiQiu.Web.WebPages
{
    public partial class PersonalInfo : PageBase
    {

        //TODO:获取MemberID
        public int memberID = 1;
        public string headerPath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetAccountInfo();
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void GetAccountInfo()
        {
            //memberID = currentuser.ID;
        }

        protected void lbtnUpdateImage_Click(object sender, EventArgs e)
        {
            //btnUpdateImage.Visible = false;
            // dUpload.Visible = true;
        }

        protected void lbtnCancelImage_Click(object sender, EventArgs e)
        {
            //  lbtnUpdateImage.Visible = true;
            //  dUpload.Visible = false;

        }

        protected void uploadImage_UploadImageCompleted(object sender, UploadImageEventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "tip", "alert('上传成功');", true);
            lbtnCancelImage_Click(null, null);
        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            ControlModify(false);
        }

        protected void lbtnOK_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string nickName = txtNickName.Text;
            // string email = txtEmail.Text;
            string cellPhone = txtCellPhone.Text;
            string position = txtPosition.Text;
            int foot = int.Parse(ddlUserFoot.Value);
            string likeTeam = txtLikeTeam.Text;
            string likeStar = txtLikeStar.Text;
            MEMBER m = new MEMBER
            {
                ID = memberID,
                NAME = name,
                NICK_NAME = nickName,
                //  EMAIL = email,
                CELLPHONE = cellPhone,
                POSITION = position,
                //FAV_FOOT = foot,
                FAV_TEAM = likeTeam,
                FAV_STAR = likeStar
                //Brithday = DateTime.Parse(tbBrithday.Text),
                //Work = tbWork.Text,
                //PalyingAge = int.Parse(tbPlayAge.Text),
                //Feature = tbFeature.InnerText,
                //Intro = tbIntro.InnerText

            };
            try
            {
                MemberManager.UpdateMember(m);
                ControlModify(true);
            }
            catch (ApplicationException ex)
            {
                Alert(ex.Message);
            }
            catch (Exception)
            {
                Alert("系统发生异常，请稍后再试！");
            }
        }

        private void Alert(string message)
        {
            //ScriptManager.RegisterStartupScript(this.ScriptManagerProxy1, this.GetType(), "alert", string.Format("alert('{0}');", message), true);
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            ControlModify(true);
        }

        private void ControlModify(bool isShowUpdate)
        {
            bool showText = isShowUpdate;
            bool showBox = !isShowUpdate;

            lbtnUpdate.Visible = showText;
            lbtnOK.Visible = showBox;
            lbtnCancel.Visible = showBox;
            // lbtnUpdateImage.Visible = showText;
            //dUpload.Visible = showBox;
            lblName.Visible = showText;
            txtName.Visible = showBox;
            lblNickName.Visible = showText;
            txtNickName.Visible = showBox;
            // lblEmail.Visible = showText;
            // txtEmail.Visible = showBox;
            lblCellPhone.Visible = showText;
            txtCellPhone.Visible = showBox;
            lblBelongTeam.Visible = showText;
            hlJoinTeam.Visible = showBox;
            lblPosition.Visible = showText;
            txtPosition.Visible = showBox;
            lblUseFoot.Visible = showText;
            ddlUserFoot.Visible = showBox;
            lblLikeTeam.Visible = showText;
            txtLikeTeam.Visible = showBox;
            lblLikeStar.Visible = showText;
            txtLikeStar.Visible = showBox;
            lblGender.Visible = showText;
            tbGender.Visible = showBox;
            litBrithday.Visible = showText;
            tbBrithday.Visible = showBox;

            litWork.Visible = showText;
            tbWork.Visible = showBox;

            litPlayAge.Visible = showText;
            tbPlayAge.Visible = showBox;

            labFeature.Visible = showText;
            tbFeature.Visible = showBox;

            litIntro.Visible = showText;
            tbIntro.Visible = showBox;

            lblUseFoot.Visible = showText;
            tbUserFoot.Visible = showBox;
        }

        private void BindData()
        {
            int totalCount = 0;
            var memberList = MemberManager.GetMemberList(s => s.ID == memberID, 1, 1, out totalCount);
            var memberInfo = memberList.FirstOrDefault();
            if (memberInfo != null)
            {
                var teamList = TeamManager.GetTeamByMemberId(memberID);
                string belongTeam = string.Join("<br/>", teamList.Select(s => s.NAME).ToArray());
                lblName.Text = txtName.Text = memberInfo.NAME;
                lblNickName.Text = txtNickName.Text = memberInfo.NICK_NAME;
                //lblEmail.Text = txtEmail.Text = memberInfo.EMAIL;
                lblCellPhone.Text = txtCellPhone.Text = memberInfo.CELLPHONE;
                lblBelongTeam.Text = belongTeam;
                lblPosition.Text = txtPosition.Text = memberInfo.POSITION;

                //ddlUserFoot.Value = memberInfo.FAV_FOOT.GetValueOrDefault(0).ToString();

                //int foot = memberInfo.FAV_FOOT.GetValueOrDefault(0);
                //if (foot == 0)
                //{
                //    lblUseFoot.Text = "保密";
                //}
                //else if (foot == 1)
                //{
                //    lblUseFoot.Text = "右脚";
                //}
                //else if (foot == 2)
                //{
                //    lblUseFoot.Text = "左脚";
                //}
                lblLikeTeam.Text = txtLikeTeam.Text = memberInfo.FAV_TEAM;
                lblLikeStar.Text = txtLikeStar.Text = memberInfo.FAV_STAR;
                var list = FileManager.GetFileList(EnumFileType.Member_Portrait, memberID);
                if (list.Count > 0)
                {
                    headerPath = "http://" + Request.Url.Authority + list[0].PATH;
                    Image1.ImageUrl = headerPath;
                }
                //litBrithday.Text = tbBrithday.Text = memberInfo.Brithday.HasValue ? memberInfo.Brithday.Value.ToString("yyyy-MM-dd") : string.Empty;
                //litWork.Text = tbWork.Text = memberInfo.Work;
                //litPlayAge.Text = tbPlayAge.Text = memberInfo.PalyingAge.HasValue ? memberInfo.PalyingAge.Value.ToString() : string.Empty;
                //labFeature.Text = tbFeature.InnerText = memberInfo.Feature;
                //litIntro.Text = tbIntro.InnerText = memberInfo.Intro;
            }
        }
    }
}