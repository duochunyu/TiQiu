<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TeamerInfoListUserControl.ascx.cs" Inherits="TiQiu.Web.UserControl.TeamInfoUserControl.TeamerInfoListUserControl" %>
<%@ Import Namespace="TiQiu.Web.WebPages.Utilities" %>
<div style="width: 253px; border: 1px solid #D9D7D7; float: left; margin-top: 25px; margin-left: 2px; display: inline; padding: 10px;">

    <h2 style="font-size: 14px; padding-bottom: 5px; padding-left: 5px;">队员列表 <input style="margin-left: 130px;" type="button" value="添加球员"/></h2>
    <ul>
        <asp:Repeater ID="rtMemberList" runat="server">
            <ItemTemplate>
                <li>
                    <img width="135" height="82" src="<%#Eval("accountReference") %>">
                    <p>
                        <%#Eval("NAME") %>
                    </p>
                    <p>
                        司业： <%#Eval("AREA_CODE") %>
                    </p>

                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div style="text-align: center; margin: 20px 0px;">
        <asp:LinkButton ID="btnItemPrePage" runat="server" OnClick="btnItemPrePage_Click"><img src="../Images/point-left.png" style="margin-bottom: -6px;" /></asp:LinkButton>
        <span style="display: inline-block; color: rgb(88, 135, 1); font-size: 14px; margin-top: 0px;">
            <asp:Literal ID="ltlPageIndex" runat="server"></asp:Literal></span>
        <asp:LinkButton ID="btnItemNextPage" runat="server" OnClick="btnItemNextPage_Click"> <img src="../Images/point-right.png" style="margin-bottom: -6px;" /></asp:LinkButton>
    </div>

</div>
<input id="hdTeamCurrentPage" type="hidden" runat="server" value="1" />
<input id="hdTeamTotal" type="hidden" runat="server" />


<%--<div style="width: 253px; border: 1px solid #D9D7D7; float: left; margin-top: 25px; margin-left: 2px; display: inline; padding: 10px;">
    <h2 class="title" style="border-bottom: 1px solid #D9D7D7">队员列表</h2>
    <ul class="list">
        <% HtmlHelper.Repeater(this.DataSource, d =>
            {%>
        <li>
            <img width="135" height="82" src="<%=d.accountReference%>">
            <p>
                <%=d.NAME %>
            </p>
            <p>
                司业： <%=d.AREA_CODE %>
            </p>

        </li>
        <%},()=>{%>
       抱歉，查询结果为空！
        
        <%});  %>
    </ul>
</div>--%>
