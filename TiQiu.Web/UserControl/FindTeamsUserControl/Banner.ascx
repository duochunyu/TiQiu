<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Banner.ascx.cs" Inherits="TiQiu.Web.UserControl.FindTeamsUserControl.Banner" %>
<%@ Import Namespace="TiQiu.Web.WebPages.Utilities" %>
<div style="width: 290px; height: 300px; float: left;margin-top:-105px;margin-right:20px;margin-left:00px;"> 
    <ul class="list">
        <% HtmlHelper.Repeater(this.DataSource, d =>
            {%>
        <li>
            <img width="135" height="82" src="http://images.ciotour.com/neweggpic2/BU/1/p800/dcd01e12-16d8-47d9-84a4-861283936273.jpg">
        </li>
        <%},()=>{%>
      <%-- 抱歉，查询结果为空！--%>
          <li>
           <img width="135" height="82" src="http://images.ciotour.com/neweggpic2/BU/1/p800/dcd01e12-16d8-47d9-84a4-861283936273.jpg">
        </li>
        

        

        <%});  %>
    </ul>
</div>
