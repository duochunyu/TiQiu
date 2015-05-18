<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FieldInfosIndexControl.ascx.cs"
     Inherits="TiQiu.Web.UserControl.FieldInfosIndexControl" %>
<%@ Import Namespace="TiQiu.Web.WebPages.Utilities" %>

<div >
    <table class="info-table" style="border: 1px solid #D9D7D7; height: 250px;">
        <tr>
            <td class="info-title" >
                <span>场地信息</span>
                <a type="button" class="info-more" href="###"/>
            </td>
        </tr>
     
       

 <% HtmlHelper.Repeater(this.DataSource, d =>
            {%>
   <tr style="padding-top:5px;">
    <td>
       <a title="<%=d.NAME %>" style="float: left; padding-top: 5px;" href="http://www.baidu.com"><%=d.NAME %></a>
    </td>
</tr>
<%},()=>{%>
<tr style="text-align: center;" >
    <td colspan="11" >
        抱歉，查询结果为空！
    </td>
</tr>
<%});  %></table>
 </div>