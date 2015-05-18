<%@ Page Language="C#" MasterPageFile="../MasterPages/Common.Master" AutoEventWireup="true"
    CodeBehind="MerChantInfoManagerMent.aspx.cs" Inherits="ShiverVin.ECP.WebUI.BaseData.MerChantInfoManagerMent"
    Title="�̼ҹ���" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend>�̼һ�����Ϣ</legend>
        <div class="groupBoxContent">
            <table class="Search_table">
                <tr>
                    <td>�̼�����:
                        <asp:Literal ID="ltlBusinessName" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnAddFiled" runat="server" Style="color: #0072bc;" OnClick="lbtnAddFiled_Click">��������̼�</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <fieldset>
        <legend>�����̼��б�</legend>
        <div class="groupBoxContent">
            <asp:Repeater ID="rtFieldList" runat="server" OnItemCommand="rtFieldList_ItemCommand">
                <HeaderTemplate>
                    <table class="ui_table" cellpadding="0" cellspacing="1">
                        <thead>
                            <tr class="titletr">
                                <th>����
                                </th>
                                <th>ϵͳ���
                                </th>
                                <th>������
                                </th>
                                <th>�绰
                                </th>
                                <th>�ֻ�
                                </th>
                                <th>����
                                </th>
                                <th>��ַ
                                </th>
                                <th>�鿴����
                                </th>
                                <th>��������
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>&nbsp;<asp:LinkButton ID="lkBtnEdit" CssClass="edit" runat="server" CommandName="edit"
                            CommandArgument='<%#Eval("ID") %>'><img src="../Images/System/EDIT.gif" />&nbsp;�༭</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lkBtnResetPWD" CssClass="delete" runat="server"
                                OnClientClick="if(!confirm('ȷ��ɾ����?')){return false;}" CommandArgument='<%#Eval("ID") %>'><img src="../Images/System/Permissions.png" />&nbsp; ɾ��</asp:LinkButton>
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
                                CommandArgument='<%#Eval("ID") %>'><img src="../Images/System/EDIT.gif" />&nbsp;�鿴����</asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton1" CssClass="edit" runat="server" CommandName="add"
                                CommandArgument='<%#Eval("ID") %>'><img src="../Images/System/EDIT.gif" />&nbsp;��������</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <webdiyer:AspNetPager ID="pager" runat="server" OnPageChanged="pager_PageChanged" AlwaysShow="true" CurrentPageButtonPosition="Center"
                Width="100%" HorizontalAlign="center" AlwaysShowFirstLastPageNumber="true" PagingButtonSpacing="10" FirstPageText="��ҳ"
                LastPageText="βҳ" NextPageText="��һҳ" PrevPageText="��һҳ" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="��ת��: ">
            </webdiyer:AspNetPager>
        </div>
    </fieldset>
    <fieldset>
        <legend id="handleTitle" runat="server">�������༭�������̼���Ϣ</legend>
        <div class="groupBoxContent">
            <table class="Search_table">
                <tr>
                    <td>ϵͳ���:
                    </td>
                    <td>
                        <asp:Label ID="ltlFieldID" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="100">
                        ������
                    </td>
                    <td>
                        <asp:TextBox ID="txtFieldName" runat="server" MaxLength="40"></asp:TextBox>
                    </td>
                    <td>�绰:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTel" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                    <td>�ֻ�:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPhone" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>����:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlArea" runat="server"></asp:DropDownList>
                    </td>
                    <td>��ַ:
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
                   <span>����</span></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lkBtn_FieldCancel" runat="server" OnClick="lkBtn_FieldCancel_Click"
                            CssClass="button" ValidationGroup="0"><span>ȡ��</span></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <fieldset>
        <legend>�����б�</legend>
        <div class="groupBoxContent">
            <asp:Repeater ID="rtFieldItemList" runat="server" OnItemCommand="rtFieldItemList_ItemCommand">
                <HeaderTemplate>
                    <table class="ui_table" cellpadding="0" cellspacing="1">
                        <thead>
                            <tr class="titletr">
                                <th>����
                                </th>
                                <th>ϵͳ���
                                </th>
                                <th>��
                                </th>
                                <th>�绰
                                </th>
                                <th>�ֻ�
                                </th>
                                <th>������
                                </th>
                                <th>��ģ
                                </th>
                                <th>����
                                </th>
                                <th>��ַ
                                </th>
                                <th>�鿴���ع���
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>&nbsp;<asp:LinkButton ID="lkBtnEdit" CssClass="edit" runat="server" CommandName="edit"
                            CommandArgument='<%#Eval("ID") %>'><img src="../Images/System/EDIT.gif" />&nbsp;�༭</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lkBtnResetPWD" CssClass="edit" runat="server" User_ID='<%#Eval("ID")%>'
                                OnClientClick="if(!confirm('ȷ��ɾ����?')){return false;}" CommandArgument='<%#Eval("ID") %>'><img src="../Images/System/Permissions.png" />&nbsp; ɾ��</asp:LinkButton>
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
                                CommandArgument='<%#Eval("ID") %>'><img src="../Images/System/EDIT.gif" />&nbsp;�鿴����</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <webdiyer:AspNetPager ID="pagerFieldItem" runat="server" OnPageChanged="pagerFieldItem_PageChanged" AlwaysShow="true" CurrentPageButtonPosition="Center"
                Width="100%" HorizontalAlign="center" AlwaysShowFirstLastPageNumber="true" PagingButtonSpacing="10" FirstPageText="��ҳ"
                LastPageText="βҳ" NextPageText="��һҳ" PrevPageText="��һҳ" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="��ת��: ">
            </webdiyer:AspNetPager>
        </div>
    </fieldset>
    <fieldset>
        <legend id="legendNewField" runat="server">�༭��������������Ϣ</legend>
        <div class="groupBoxContent">
            <table class="Search_table">
                <tr>
                    <td style="width: 400px;">ϵͳ���: &nbsp;&nbsp;<asp:Label ID="ltlFieldItemID" runat="server" Text=""></asp:Label></td>
                    <td rowspan="4"></td>
                </tr>
                <tr>
                    <td>������: &nbsp;&nbsp;<asp:Label ID="ltlFieldName" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>��ַ: &nbsp;&nbsp;<asp:Label ID="ltlFieldAddress" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>��: &nbsp;&nbsp;<asp:TextBox ID="txtFieldItemName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>��ģ: &nbsp;&nbsp;<asp:DropDownList ID="ddlFieldItemType" runat="server">
                        <asp:ListItem Text="5����" Value="5" />
                        <asp:ListItem Text="7����" Value="7" />
                        <asp:ListItem Text="9����" Value="9" />
                        <asp:ListItem Text="11����" Value="11" />
                    </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>�ϴ�ͼƬ:</td>
                    <td>
                        <asp:LinkButton ID="lbtnSaveFieldItem"
                            Visible="false"
                            runat="server" CssClass="button"
                            OnClick="lbtnSaveFieldItem_Click" Style="margin-right: 20px;">
                   <span>����</span></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="HyperLink1" runat="server">�༭(����)���ع���</asp:HyperLink></td>
                </tr>
            </table>

        </div>
    </fieldset>
    <asp:HiddenField ID="hdFieldID" runat="server" Value="0" />
    <asp:HiddenField ID="hdFieldItemID" runat="server" />
</asp:Content>
