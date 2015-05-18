<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookInfoManagerMent.aspx.cs"
    MasterPageFile="~/BG/MasterPages/Common.Master" Inherits="ShiverVin.ECP.WebUI.BG.BaseData.BookInfoManagerMent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var PWDReset = function (obj) {
            var m = Math.random()
            var User_ID = $(obj).attr("User_ID");
            ShowDialog("../Framework/ResetPassword.aspx?frm=true&User_ID=" + User_ID + "&m=" + m, "重置密码", 380, 220, null);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend>查找场地</legend>
        <div class="groupBoxContent">
            <table class="Search_table">
                <tr>
                    <td>
                        场地名称:
                    </td>
                    <td>
                        <input type="text" value="跃金" id="txtFieldName" class="intxt" />
                    </td>
                    <td>
                        所属商家:
                    </td>
                    <td>
                        <input type="text" value="跃金" id="txtMerchant" class="intxt" />
                    </td>
                    <td>
                        区域:
                    </td>
                    <td>
                        <select class="inselect" id="Selectarea">
                            <option value="">请选择</option>
                            <option value="true" selected="selected">武侯区</option>
                            <option value="false">锦江区</option>
                            <option value="false">成华区</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        场地地址:
                    </td>
                    <td>
                        <input type="text" value="成都桐梓林" id="txtFieldAddress" class="intxt" />
                    </td>
                    <td>
                        规模:
                    </td>
                    <td>
                        <select class="inselect" id="SelectFieldType">
                            <option value="">请选择</option>
                            <option value="true" selected="selected">5人制</option>
                            <option value="false">7人制</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                    </td>
                    <td colspan="1">
                        <asp:LinkButton ID="lkBtnSearch" CssClass="button" runat="server"><span>查询</span></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <fieldset>
        <legend>商家信息</legend>
        <div class="groupBoxContent">
            <table class="ui_table" cellpadding="0" cellspacing="1">
                <thead>
                    <tr class="titletr">
                        <th>
                            操作
                        </th>
                        <th>
                            商家编号
                        </th>
                        <th>
                            商家名称
                        </th>
                        <th>
                            商家电话
                        </th>
                        <th>
                            手机
                        </th>
                        <th>
                            地址
                        </th>
                        <th>
                            区域
                        </th>
                        <th>
                            新增场地
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            &nbsp;<asp:LinkButton ID="lkBtnEdit" CssClass="edit" runat="server" CommandName="edit"
                                CommandArgument='<%#Eval("Id") %>'><img src="../Images/System/EDIT.gif" />&nbsp;编辑</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lkBtnResetPWD" CssClass="edit" runat="server" User_ID='<%#Eval("Id")%>'
                                OnClientClick="PWDReset(this);return false;" CommandArgument='<%#Eval("Id") %>'><img src="../Images/System/Permissions.png" />&nbsp; 删除</asp:LinkButton>
                        </td>
                        <td>
                            <%#HttpUtility.HtmlEncode(Eval("loginName").ToString())%>
                        </td>
                        <td>
                            <%# Eval("RealName")%>
                        </td>
                        <td>
                            <%#(Eval("CompanySign") != null) ? HttpUtility.HtmlEncode(Eval("CompanySign").ToString()) : ""%>
                        </td>
                        <td>
                            <%#Eval("Name")%>
                        </td>
                        <td>
                            <%#Eval("ParentName")%>
                        </td>
                        <td>
                            <%#Eval("State")%>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton1" CssClass="edit" runat="server" CommandName="edit"
                                CommandArgument='<%#Eval("Id") %>'><img src="../Images/System/EDIT.gif" />&nbsp;新增场地</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="12">
                            暂无数据!
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </fieldset>
    <fieldset>
        <legend id="handleTitle" runat="server">新增（编辑）商家信息</legend>
        <div class="groupBoxContent">
            <table class="Search_table">
                <tr>
                    <td>
                        商家名称:
                    </td>
                    <td>
                        <asp:TextBox ID="txtLoginName" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                    <td>
                        商家电话:
                    </td>
                    <td>
                        <asp:TextBox ID="txtRealName" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        手机:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                    <td colspan="1">
                        地址:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                    <td>
                        区域:
                    </td>
                    <td>
                        <asp:TextBox ID="txtArea" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                    </td>
                    <td colspan="1">
                        <div>
                            <asp:LinkButton ID="lkBtn_Save" runat="server" CssClass="button" ValidationGroup="1">
                   <span>保存</span></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="lkBtn_Cancel" runat="server" CssClass="button" ValidationGroup="0"><span>取消</span></asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    
</asp:Content>
