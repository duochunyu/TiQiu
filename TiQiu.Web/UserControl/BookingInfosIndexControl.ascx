<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookingInfosIndexControl.ascx.cs" Inherits="TiQiu.Web.UserControl.BookingInfosIndexControl" %>
<%@ Import Namespace="TiQiu.Web.WebPages.Utilities" %>

<div >
    <table class="info-table" style="border: 1px solid #D9D7D7; height: 250px;">
        <tr>
            <td class="info-title" >
                <span>约战信息</span>
                <input type="button" class="info-more" />
            </td>
        </tr>
        <% HtmlHelper.Repeater(this.DataSource, d =>
            {%>
   <tr  style="padding-top: 5px;">
    <td>
       <a title="<%=d.FIELD_ORDER_LOG %>" style="float: left; padding-left: 5px;" href="http://www.baidu.com"><%=d.FIELD_ORDER_LOG %></a>
    </td>
</tr>
<%},()=>{%>
<tr  style="padding-top:5px;">
    <td colspan="11">
         抱歉，暂无约战信息！
    </td>
</tr>
<%});  %>
    </table>
</div>
