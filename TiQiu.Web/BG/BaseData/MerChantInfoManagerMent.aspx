<%@ Page Language="C#" MasterPageFile="../MasterPages/Common.Master" AutoEventWireup="true"
    CodeBehind="MerChantInfoManagerMent.aspx.cs" Inherits="ShiverVin.ECP.WebUI.BaseData.MerChantInfoManagerMent"
    Title="商家管理" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend>商家基本信息</legend>
        <div class="groupBoxContent">
            <table class="Search_table">
                <tr>
                    <td>商家名称:
                        <asp:Literal ID="ltlBusinessName" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnAddFiled" runat="server" Style="color: #0072bc;" OnClick="lbtnAddFiled_Click">添加下属商家</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <fieldset>
        <legend>下属商家列表</legend>
        <div class="groupBoxContent">
            <asp:Repeater ID="rtFieldList" runat="server" OnItemCommand="rtFieldList_ItemCommand">
                <HeaderTemplate>
                    <table class="ui_table" cellpadding="0" cellspacing="1">
                        <thead>
                            <tr class="titletr">
                                <th>操作
                                </th>
                                <th>系统编号
                                </th>
                                <th>球场名称
                                </th>
                                <th>电话
                                </th>
                                <th>手机
                                </th>
                                <th>区域
                                </th>
                                <th>地址
                                </th>
                                <th>查看场地
                                </th>
                                <th>新增场地
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>&nbsp;<asp:LinkButton ID="lkBtnEdit" CssClass="edit" runat="server" CommandName="edit"
                            CommandArgument='<%#Eval("ID") %>'><img src="../Images/System/EDIT.gif" />&nbsp;编辑</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lkBtnResetPWD" CssClass="delete" runat="server"
                                OnClientClick="if(!confirm('确认删除吗?')){return false;}" CommandArgument='<%#Eval("ID") %>'><img src="../Images/System/Permissions.png" />&nbsp; 删除</asp:LinkButton>
                        </td>
                        <td>
                            <%#Eval("ID")%>
                        </td>
                        <td>
                            <%# Eval("NAME")%>
                        </td>
                        <td>
                            <%#Eval("TEL")%>
                        </td>
                        <td>
                            <%#Eval("PHONE")%>
                        </td>
                        <td>
                            <%#GetAreaValue(Eval("AREA_CODE").ToString())%>
                        </td>
                        <td>
                            <%#Eval("ADRESS")%>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton2" CssClass="edit" runat="server" CommandName="look"
                                CommandArgument='<%#Eval("ID") %>'><img src="../Images/System/EDIT.gif" />&nbsp;查看场地</asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton1" CssClass="edit" runat="server" CommandName="add"
                                CommandArgument='<%#Eval("ID") %>'><img src="../Images/System/EDIT.gif" />&nbsp;新增场地</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <webdiyer:AspNetPager ID="pager" runat="server" OnPageChanged="pager_PageChanged" AlwaysShow="true" CurrentPageButtonPosition="Center"
                Width="100%" HorizontalAlign="center" AlwaysShowFirstLastPageNumber="true" PagingButtonSpacing="10" FirstPageText="首页"
                LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="跳转到: ">
            </webdiyer:AspNetPager>
        </div>
    </fieldset>
    <fieldset>
        <legend id="handleTitle" runat="server">新增（编辑）下属商家信息</legend>
        <div class="groupBoxContent">
            <table class="Search_table">
                <tr>
                    <td>系统编号:
                    </td>
                    <td>
                        <asp:Label ID="ltlFieldID" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="100">
                        球场名称
                    </td>
                    <td>
                        <asp:TextBox ID="txtFieldName" runat="server" MaxLength="40"></asp:TextBox>
                    </td>
                    <td>电话:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTel" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                    <td>手机:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPhone" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>区域:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlArea" runat="server"></asp:DropDownList>
                    </td>
                    <td>地址:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                    <td></td>
                    <td>&nbsp;</td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="8" style="text-align: center;">
                        <asp:LinkButton ID="lkBtn_FieldSave" runat="server" OnClick="lkBtn_FieldSave_Click"
                            CssClass="button" ValidationGroup="1" Style="margin-right: 20px;">
                   <span>保存</span></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lkBtn_FieldCancel" runat="server" OnClick="lkBtn_FieldCancel_Click"
                            CssClass="button" ValidationGroup="0"><span>取消</span></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <fieldset>
        <legend>场地列表</legend>
        <div class="groupBoxContent">
            <asp:Repeater ID="rtFieldItemList" runat="server" OnItemCommand="rtFieldItemList_ItemCommand">
                <HeaderTemplate>
                    <table class="ui_table" cellpadding="0" cellspacing="1">
                        <thead>
                            <tr class="titletr">
                                <th>操作
                                </th>
                                <th>系统编号
                                </th>
                                <th>球场
                                </th>
                                <th>电话
                                </th>
                                <th>手机
                                </th>
                                <th>所属球场
                                </th>
                                <th>规模
                                </th>
                                <th>区域
                                </th>
                                <th>地址
                                </th>
                                <th>查看场地规则
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>&nbsp;<asp:LinkButton ID="lkBtnEdit" CssClass="edit" runat="server" CommandName="edit"
                            CommandArgument='<%#Eval("ID") %>'><img src="../Images/System/EDIT.gif" />&nbsp;编辑</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lkBtnResetPWD" CssClass="edit" runat="server" User_ID='<%#Eval("ID")%>'
                                OnClientClick="if(!confirm('确认删除吗?')){return false;}" CommandArgument='<%#Eval("ID") %>'><img src="../Images/System/Permissions.png" />&nbsp; 删除</asp:LinkButton>
                        </td>
                        <td>
                            <%# Eval("ID")%>
                        </td>
                        <td>
                            <%# Eval("BRIEF")%>
                        </td>
                        <td>
                            <%#Eval("field.TEL")%>
                        </td>
                        <td>
                            <%#Eval("field.PHONE")%>
                        </td>
                        <td>
                            <%#Eval("field.NAME")%>
                        </td>
                        <td>
                            <%#GetFieldItemType(Eval("TYPE").ToString())%>
                        </td>
                        <td>
                            <%#GetAreaValue(Eval("field.AREA_CODE").ToString())%>
                        </td>
                        <td>
                            <%#Eval("field.ADRESS")%>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton1" CssClass="edit" runat="server"
                                CommandArgument='<%#Eval("ID") %>'><img src="../Images/System/EDIT.gif" />&nbsp;查看规则</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <webdiyer:AspNetPager ID="pagerFieldItem" runat="server" OnPageChanged="pagerFieldItem_PageChanged" AlwaysShow="true" CurrentPageButtonPosition="Center"
                Width="100%" HorizontalAlign="center" AlwaysShowFirstLastPageNumber="true" PagingButtonSpacing="10" FirstPageText="首页"
                LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="跳转到: ">
            </webdiyer:AspNetPager>
        </div>
    </fieldset>
    <fieldset>
        <legend id="legendNewField" runat="server">编辑（新增）场地信息</legend>
        <div class="groupBoxContent">
            <table class="Search_table">
                <tr>
                    <td style="width: 400px;">系统编号: &nbsp;&nbsp;<asp:Label ID="ltlFieldItemID" runat="server" Text=""></asp:Label></td>
                    <td rowspan="4"></td>
                </tr>
                <tr>
                    <td>所属球场: &nbsp;&nbsp;<asp:Label ID="ltlFieldName" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>地址: &nbsp;&nbsp;<asp:Label ID="ltlFieldAddress" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>球场: &nbsp;&nbsp;<asp:TextBox ID="txtFieldItemName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>规模: &nbsp;&nbsp;<asp:DropDownList ID="ddlFieldItemType" runat="server">
                        <asp:ListItem Text="5人制" Value="5" />
                        <asp:ListItem Text="7人制" Value="7" />
                        <asp:ListItem Text="9人制" Value="9" />
                        <asp:ListItem Text="11人制" Value="11" />
                    </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>上传图片:</td>
                    <td>
                        <asp:LinkButton ID="lbtnSaveFieldItem"
                            Visible="false"
                            runat="server" CssClass="button"
                            OnClick="lbtnSaveFieldItem_Click" Style="margin-right: 20px;">
                   <span>保存</span></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="HyperLink1" runat="server">编辑(新增)场地规则</asp:HyperLink></td>
                </tr>
            </table>

        </div>
    </fieldset>
    <asp:HiddenField ID="hdFieldID" runat="server" Value="0" />
    <asp:HiddenField ID="hdFieldItemID" runat="server" />
</asp:Content>
