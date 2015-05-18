<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="FieldRuleManager.aspx.cs" Inherits="TiQiu.Web.WebPages.FieldRuleManager" %>
<%@ Register src="../UserControl/WeekCalendar.ascx" tagname="WeekCalendar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mian" runat="server">

    <form id="form1" runat="server">
        <div>
            <asp:Menu ID="menuField" runat="server" Orientation="Horizontal" RenderingMode="List" BackColor="#E3EAEB" DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#666666" StaticSubMenuIndent="10px">
                <DynamicHoverStyle BackColor="#666666" ForeColor="White"></DynamicHoverStyle>

                <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px"></DynamicMenuItemStyle>

                <DynamicMenuStyle BackColor="#E3EAEB"></DynamicMenuStyle>

                <DynamicSelectedStyle BackColor="#1C5E55"></DynamicSelectedStyle>

                <StaticHoverStyle BackColor="#666666" ForeColor="White"></StaticHoverStyle>

                <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px"></StaticMenuItemStyle>

                <StaticSelectedStyle BackColor="#1C5E55"></StaticSelectedStyle>
            </asp:Menu>
        </div>
        <uc1:WeekCalendar ID="WeekCalendar1" ClientCalendarDivID="testCalendar" runat="server" ViewStateMode="Disabled"/>
    </form>

</asp:Content>
