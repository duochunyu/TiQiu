<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MatchInfosindexControl.ascx.cs" Inherits="TiQiu.Web.UserControl.MatchInfosindexControl" %>
<%@ Import Namespace="TiQiu.Web.WebPages.Utilities" %>
<div >
    <table class="info-table" style="border: 1px solid #D9D7D7; height: 250px;">
        <tr>
            <td class="info-title" >
                <span>赛事信息</span>
                <input type="button" class="info-more" />
            </td>
        </tr>
        <% HtmlHelper.Repeater(this.DataSource, d =>
            {%>
   <tr  style="text-align:start;">
    <td >
       <a title="<%=d.NAME %>" style="float: left; padding-left: 5px;" href="http://www.baidu.com"><%=d.NAME %></a>
    </td>
</tr>
<%},()=>{%>
<tr style="text-align: center;">
    <td colspan="11" >
        抱歉，暂无赛事信息！
    </td>
</tr>
<%});  %>
    </table>
</div>