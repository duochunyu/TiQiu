using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

using System.Collections.Generic;

using System.Text.RegularExpressions;
using TiQiu.Biz;
using TiQiu.DAL;


namespace ShiverVin.ECP.WebUI.BaseData
{
    public partial class MerChantInfoManagerMent : Page
    {
        private int businessID = 32;
        private TiQiu.BG.MasterPages.Common commonMaster;
        protected void Page_Load(object sender, EventArgs e)
        {
            commonMaster = this.Master as TiQiu.BG.MasterPages.Common;
            if (!IsPostBack)
            {
                BindBusiness();
                BindArea();
            }
        }

        private void BindBusiness()
        {
            int totalCount = 0;
            var list = BusinessesManager.GetBusinessList(s => s.ID == businessID, "ID", true, 1, 1, out totalCount);
            var business = list.FirstOrDefault();
            if (business != null)
            {
                ltlBusinessName.Text = business.NAME;
            }
            BindFieldList();
        }

        private void BindFieldList()
        {
            int totalCount = 0;
            var list = FieldManager.GetFieldList(s => s.BUSINESSES_ID == businessID, "ID", false, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            rtFieldList.DataSource = list;
            rtFieldList.DataBind();
        }

        private void BindFieldItemList()
        {
            int totalCount = 0;
            string strFieldItemID = hdFieldID.Value.ToString();
            if (!string.IsNullOrWhiteSpace(strFieldItemID))
            {
                int fieldID = 0;
                if (int.TryParse(strFieldItemID, out fieldID))
                {
                    var list = FieldManager.GetFieldItemList(s => s.FIELD_ID == fieldID, "ID", false, pagerFieldItem.CurrentPageIndex, pagerFieldItem.PageSize, out totalCount);
                    pagerFieldItem.RecordCount = totalCount;
                    rtFieldItemList.DataSource = list;
                    rtFieldItemList.DataBind();
                }
            }
        }

        private void BindArea()
        {
            var list = ConstValue.GetAreaList(false);
            ddlArea.DataSource = list;
            ddlArea.DataValueField = "CODE";
            ddlArea.DataTextField = "AREA_NAME";
            ddlArea.DataBind();
        }

        private FIELD GetFieldInfo(int fieldID)
        {
            int totalCount = 0;
            var list = FieldManager.GetFieldList(s => s.ID == fieldID, "ID", false, 1, 1, out totalCount);
            return list.FirstOrDefault();
        }

        private void BindFieldInfo(int fieldID)
        {
            var fieldInfo = GetFieldInfo(fieldID);
            if (fieldInfo != null)
            {
                ltlFieldID.Text = fieldInfo.ID.ToString();
                txtFieldName.Text = fieldInfo.NAME;
                txtTel.Text = fieldInfo.TEL;
                txtPhone.Text = fieldInfo.PHONE;
                if (ddlArea.Items.FindByValue(fieldInfo.AREA_CODE) != null)
                {
                    ddlArea.SelectedValue = fieldInfo.AREA_CODE;
                }
                txtAddress.Text = fieldInfo.ADRESS;
                ltlFieldName.Text = fieldInfo.NAME;
                ltlFieldAddress.Text = fieldInfo.ADRESS;
            }
        }

        private void BindFieldForItem()
        {
            if (string.IsNullOrWhiteSpace(hdFieldID.Value))
            {
                return;
            }

            int fieldID = int.Parse(hdFieldID.Value);
            var fieldInfo = GetFieldInfo(fieldID);
            if (fieldInfo != null)
            {
                ltlFieldName.Text = fieldInfo.NAME;
                ltlFieldAddress.Text = fieldInfo.ADRESS;
            }
        }

        private FIELD_ITEM GetFieldItemInfo(int fieldItemID)
        {
            int totalCount = 0;
            var list = FieldManager.GetFieldItemList(s => s.ID == fieldItemID, "ID", false, 1, 1, out totalCount);
            return list.FirstOrDefault();
        }

        private void BindFieldItemInfo(int fieldItemID)
        {
            var fieldItemInfo = GetFieldItemInfo(fieldItemID);
            if (fieldItemInfo != null)
            {
                ltlFieldItemID.Text = fieldItemInfo.ID.ToString();
                txtFieldItemName.Text = fieldItemInfo.NAME;
                var fieldInfo = GetFieldInfo(fieldItemInfo.FIELD_ID);
                if (fieldInfo != null)
                {
                    ltlFieldName.Text = fieldInfo.NAME;
                    ltlFieldAddress.Text = fieldInfo.ADRESS;

                }
                ddlFieldItemType.SelectedValue = fieldItemInfo.TYPE.ToString();
            }
        }

        protected void pager_PageChanged(object sender, EventArgs e)
        {
            BindFieldList();
        }

        protected void pagerFieldItem_PageChanged(object sender, EventArgs e)
        {
            BindFieldItemList();
        }

        protected string GetAreaValue(string areaCode)
        {
            var list = ConstValue.GetAreaList(false);
            foreach (var item in list)
            {
                if (item.CODE == areaCode)
                {
                    return item.AREA_NAME;
                }
            }

            return string.Empty;
        }

        protected string GetFieldItemType(string value)
        {
            switch (value)
            {
                case "5":
                    return "5人制";
                case "7":
                    return "7人制";
                case "9":
                    return "9人制";
                case "11":
                    return "11人制";
                default:
                    return string.Empty;
            }
        }

        protected void rtFieldList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                var ID = int.Parse(e.CommandArgument.ToString());
                if (ID > 0)
                {
                    hdFieldID.Value = ID.ToString();
                    BindFieldInfo(ID);
                    BindFieldItemList();
                }
            }
            else if (e.CommandName == "delete")
            {
                var ID = int.Parse(e.CommandArgument.ToString());
                if (ID > 0)
                {
                    FieldManager.DelField(ID);
                    BindFieldList();
                    if (hdFieldID.Value == ID.ToString())
                    {
                        ClearField();
                        hdFieldID.Value = string.Empty;
                    }
                }
            }
            else if (e.CommandName == "add")
            {
                ClearField();
                hdFieldID.Value = e.CommandArgument.ToString();
                BindFieldItemList();
                BindFieldForItem();
                hdFieldItemID.Value = "0";
            }
            else if (e.CommandName == "look")
            {
                hdFieldID.Value = e.CommandArgument.ToString();
                BindFieldItemList();
            }
        }

        protected void lkBtn_FieldSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(hdFieldID.Value))
            {
                return;
            }
            string fieldName = txtFieldName.Text;
            string fieldTel = txtTel.Text;
            string fieldPhone = txtPhone.Text;
            string areaCode = ddlArea.SelectedValue;
            string address = txtAddress.Text;
            int fieldID = int.Parse(hdFieldID.Value);
            FIELD field = null;
            if (fieldID == 0)
            {
                field = new FIELD();
            }
            else
            {
                field = GetFieldInfo(fieldID);
                if (field == null)
                {
                    Alert("不能更新，数据不存在!");
                    return;
                }
            }
            field.ID = fieldID;
            field.NAME = fieldName;
            field.TEL = fieldTel;
            field.PHONE = fieldPhone;
            field.AREA_CODE = areaCode;
            field.ADRESS = address;
            field.BUSINESSES_ID = businessID;
            try
            {
                if (fieldID > 0)
                {
                    FieldManager.UpdateField(field);
                }
                else
                {
                    FieldManager.CreateField(field);
                }
            }
            catch (ApplicationException ex)
            {
                Alert(ex.Message);
                return;
            }
            catch
            {
                Alert("操作失败!");
                return;
            }

            BindFieldList();
            ClearField();
            hdFieldID.Value = fieldID.ToString();
            Alert("操作成功!");
        }

        protected void lkBtn_FieldCancel_Click(object sender, EventArgs e)
        {
            ClearField();
        }

        private void ClearField()
        {
            hdFieldID.Value = string.Empty;
            ltlFieldID.Text = string.Empty;
            txtFieldName.Text = string.Empty;
            txtTel.Text = string.Empty;
            txtPhone.Text = string.Empty;
            ddlArea.SelectedIndex = 0;
            txtAddress.Text = string.Empty;

            hdFieldItemID.Value = string.Empty;
            rtFieldItemList.DataSource = null;
            pagerFieldItem.RecordCount = 0;
            rtFieldItemList.DataBind();
            ltlFieldItemID.Text = string.Empty;
            ltlFieldName.Text = string.Empty;
            ltlFieldAddress.Text = string.Empty;
            txtFieldItemName.Text = string.Empty;
            ddlFieldItemType.SelectedIndex = 0;
        }

        private void ClearFieldItem()
        {
            hdFieldItemID.Value = string.Empty;
            ltlFieldItemID.Text = string.Empty;
            ltlFieldName.Text = string.Empty;
            ltlFieldAddress.Text = string.Empty;
            txtFieldItemName.Text = string.Empty;
            ddlFieldItemType.SelectedIndex = 0;
        }

        protected void lbtnAddFiled_Click(object sender, EventArgs e)
        {
            ClearField();
            hdFieldID.Value = "0";
        }

        protected void rtFieldItemList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                lbtnSaveFieldItem.Visible = true;
                var ID = int.Parse(e.CommandArgument.ToString());
                if (ID > 0)
                {
                    hdFieldItemID.Value = ID.ToString();
                    BindFieldItemInfo(ID);
                }
            }
            else if (e.CommandName == "delete")
            {
                lbtnSaveFieldItem.Visible = true;
                var ID = int.Parse(e.CommandArgument.ToString());
                if (ID > 0)
                {
                    FieldManager.DelFieldItem(ID);
                    BindFieldItemList();
                    if (hdFieldItemID.Value == ID.ToString())
                    {
                        ClearFieldItem();
                        hdFieldItemID.Value = string.Empty;
                    }
                }
            }
        }

        private void Alert(string message)
        {
            commonMaster.RegisterScript("message", string.Format("alert('{0}')", message));
        }

        protected void lbtnSaveFieldItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(hdFieldItemID.Value) ||
                string.IsNullOrWhiteSpace(hdFieldID.Value))
            {
                return;
            }

            string fieldItemName = txtFieldItemName.Text;
            int fieldType = int.Parse(ddlFieldItemType.SelectedValue);
            int fieldID = int.Parse(hdFieldID.Value);
            int fieldItemID = int.Parse(hdFieldItemID.Value);
            FIELD_ITEM item = null;
            if (fieldItemID > 0)
            {
                item = GetFieldItemInfo(fieldItemID);
                if (item == null)
                {
                    Alert("不能更新，数据不存在!");
                    return;
                }
            }
            else
            {
                item = new FIELD_ITEM();
            }
            item.ID = fieldItemID;
            item.NAME = item.BRIEF = fieldItemName;
            item.FIELD_ID = fieldID;
            item.TYPE = fieldType;
            item.BUSINESSES_ID = businessID;
            try
            {
                if (fieldItemID > 0)
                {
                    FieldManager.UpdateFieldItem(item);
                }
                else
                {
                    FieldManager.CreateField_Item(item);
                }
            }
            catch (ApplicationException ex)
            {
                Alert(ex.Message);
                return;
            }
            catch
            {
                Alert("操作失败!");
                return;
            }

            BindFieldItemList();
            ClearFieldItem();
            hdFieldItemID.Value = fieldItemID.ToString();
            Alert("操作成功!");
        }
    }
}
