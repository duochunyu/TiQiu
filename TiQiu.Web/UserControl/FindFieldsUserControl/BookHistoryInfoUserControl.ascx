<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookHistoryInfoUserControl.ascx.cs" Inherits="TiQiu.Web.UserControl.FindFieldsUserControl.BookHistoryInfoUserControl" %>
<div style="float: left; margin-left: 25px;height:100%;width:250px;" class="history">
    <h2 class="block-title">预定历史</h2>
    <ul>
        <asp:Repeater ID="rtHistory" runat="server">
            <ItemTemplate>
                <li></li>
            </ItemTemplate>
        </asp:Repeater>
        
    </ul>
    <div style="text-align: center; margin-right:20px;margin-bottom:0px;">
        <asp:LinkButton ID="btnItemPrePage" runat="server" OnClick="btnItemPrePage_Click"><img src="../Images/point-left.png" style="margin-bottom: -6px;" /></asp:LinkButton>
        <span style="display: inline-block; color: rgb(88, 135, 1); font-size: 14px; margin-top: 0px;">
            <asp:Literal ID="ltlPageIndex" runat="server"></asp:Literal></span>
        <asp:LinkButton ID="btnItemNextPage" runat="server" OnClick="btnItemNextPage_Click"> <img src="../Images/point-right.png" style="margin-bottom: -6px;" /></asp:LinkButton>
    </div>
</div>
<input id="hdOrderCurrentPage" type="hidden" runat="server" value="1" />
<input id="hdOrderTotal" type="hidden" runat="server" />
