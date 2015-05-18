<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddTeamerListUserControl.ascx.cs" Inherits="TiQiu.Web.UserControl.AddTeamerListUserControl" %>
<%@ Import Namespace="TiQiu.Web.WebPages.Utilities" %>
<div style="width: 253px; border: 1px solid #D9D7D7; float: left; margin-top: 23px; margin-left: 325px; display: inline; padding: 10px;">
    <h2 class="title" style="border-bottom: 1px solid #D9D7D7">队员列表</h2>
    <input type="button" id="btnaddTeamer" value="添加球员" />
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
</div>

<div id="addTeamers" style="width: 253px; border: 1px solid #D9D7D7; float: left; margin-top: 23px; margin-left: 325px; display: none; padding: 10px;">

    <h2 class="title" style="border-bottom: 1px solid #D9D7D7">添加球员</h2>
    <p>
        <label class="lable-normal">用户名：</label>
        <input width="200" class="input_text" id="txtAccountName" type="text" runat="server" />
        <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click"><img src="../Images/search-btn.png" /></asp:LinkButton>
    </p>
    <ul class="list">
        <% HtmlHelper.Repeater(this.DataSource_Add, d =>
            {%>
        <li>
            <img width="135" height="82" src="<%=d.NAME%>">
            <p>
                姓名:   <%=d.NAME %>
            </p>
        </li>
        <%},()=>{%>
       抱歉，查询结果为空！
        
        <%});  %>
    </ul>
</div>
<script>
    $("#btnaddTeamer").click(function () {
        $.modal.close();
        $("#addTeamers").modal();
    });

</script>

