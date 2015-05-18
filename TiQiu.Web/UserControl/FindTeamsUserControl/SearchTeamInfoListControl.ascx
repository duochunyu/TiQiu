<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchTeamInfoListControl.ascx.cs" Inherits="TiQiu.Web.UserControl.FindTeamsUserControl.SearchTeamInfoListControl" %>
<%@ Import Namespace="TiQiu.Web.WebPages.Utilities" %>
<div class="box2" style="margin-left: 15px;width:620px;">
    
         <h2 style="font-size: 14px; padding-bottom: 5px;padding-left: 5px;">球队列表</h2>
    <ul>
        <asp:Repeater ID="rtTeamList" runat="server">
            <ItemTemplate>
                <li>
            <img width="135" height="82" src="<%#Eval("BRIEF")%>"/>
            <p>
             球队名称：   <%#Eval("NAME")%>
            </p>
            <p>
                联系人：  <%#Eval("NAME")%>
            </p>
              <p>
                战绩： <%#Eval("WIN")%> 胜 <%#Eval("LOSE")%>负 <%#Eval("BRIFE")%>平 
            </p>

        </li>
            </ItemTemplate>
        </asp:Repeater>
        <div style="text-align: center; margin: 20px 0px;">
            <asp:LinkButton ID="btnItemPrePage" runat="server" OnClick="btnItemPrePage_Click"><img src="../Images/point-left.png" style="margin-bottom: -6px;margin-left:240px;" /></asp:LinkButton>
            <span style="display: inline-block; color: rgb(88, 135, 1); font-size: 14px; margin-top: 0px;margin-left:-380px;">
                <asp:Literal ID="ltlPageIndex" runat="server"></asp:Literal></span>
            <asp:LinkButton ID="btnItemNextPage" runat="server" OnClick="btnItemNextPage_Click"> <img src="../Images/point-right.png" style="margin-bottom: -6px;margin-left:30px;" /></asp:LinkButton>
        </div>
    </ul>
</div>
<input id="hdTeamCurrentPage" type="hidden" runat="server" value="1" />
<input id="hdTeamTotal" type="hidden" runat="server" />


   <%-- <ul class="list">
        <% HtmlHelper.Repeater(this.DataSource, d =>
            {%>
        <li>
            <img width="135" height="82" src="<%=d.BRIEF%>">
            <p>
             球队名次：   <%=d.NAME %>
            </p>
            <p>
                联系人： <%=d.team_member %>
            </p>
              <p>
                战绩： <%=d.WIN %> 胜<%=d.LOSE %>负<%=d.BRIEF %>平
            </p>

        </li>
        <%},()=>{%>
       抱歉，查询结果为空！
        
        <%});  %>
    </ul>--%>

