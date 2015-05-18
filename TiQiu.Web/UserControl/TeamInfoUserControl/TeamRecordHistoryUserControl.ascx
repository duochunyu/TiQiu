<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TeamRecordHistoryUserControl.ascx.cs" Inherits="TiQiu.Web.UserControl.TeamInfoUserControl.TeamRecordHistoryUserControl" %>
<%@ Import Namespace="TiQiu.Web.WebPages.Utilities" %>
<div style="width: 245px; border: 1px solid #D9D7D7; float: left; margin-top: 23px; margin-left: 10px; display: inline; padding: 10px;">
    <h2 class="title" style="border-bottom: 1px solid #D9D7D7; margin-bottom: 5px;">球队战绩</h2>

    <ul class="list2">
        <% HtmlHelper.Repeater(this.DataSource, d =>
            {%>
        <li>
            <span>2013-5-12 </span><span>5：0</span><span> 球队球队球队</span>
        </li>
        <%},()=>{%>
       抱歉，查询结果为空！
        
        <%});  %>
    </ul>
</div>
