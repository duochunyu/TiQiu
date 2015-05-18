<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TeamListUserControl.ascx.cs" Inherits="TiQiu.Web.UserControl.TeamInfoUserControl.TeamListUserControl" %>
<%@ Import Namespace="TiQiu.Web.WebPages.Utilities" %>
<div style="width: 253px; border: 1px solid #D9D7D7; float: left; margin-left: 25px; display: inline; padding: 10px;">
    <h2 class="title" style="border-bottom: 1px solid #D9D7D7">球队列表</h2>
    <a class="create-btn" href="#" style="margin-top: -32px; float: right;">创建球队</a>
    <ul class="list">
        <% HtmlHelper.Repeater(this.DataSource, d =>
            {%>
        <li>
            <a href="../../WebPages/MyTeam.aspx/{<%=d.ID %>}">
                <img width="135" height="82" src="<%=d.DRAW %>"></a>
            <p>
                <%=d.NAME %>
            </p>
            <p>
                <%=d.WIN %>胜 &nbsp;&nbsp; <%=d.BRIEF %> 平&nbsp;&nbsp;<%=d.LOSE %>负
            </p>

        </li>
        <%},()=>{%>
        抱歉，暂无球队！
       
        <%});  %>
    </ul>
</div>
