<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResearchIndexUserControl.ascx.cs" Inherits="TiQiu.Web.UserControl.ResearchIndexUserControl" %>
<div id="logindiv" style="height: 350px;">
    <table style="width: 100%; height: 300px; background: url('../Images/search.jpg') no-repeat scroll 0 0 transparent;">
        <tr>
            <td colspan="2" style="height: 90px">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: right; width: 100px;">
                <p style="color: #ffffff;">球场名称：</p>
            </td>
            <td style="text-align: left;">
                <input type="text" id="fieldname" class="ipt" runat="server" style="width: 150px;" /></td>
        </tr>

        <tr>
            <td style="text-align: right; width: 100px;">
                <p style="color: #ffffff;">区&nbsp;&nbsp; &nbsp;域：</p>
            </td>
            <td style="text-align: left;">
                <asp:HiddenField runat="server" ID="ddlArea" />
                <dl style="z-index: 0;" class="select" name="<%=ddlArea.ClientID %>">
                    <dt><a href="javascript:void(0);">全部</a></dt>
                    <dd style="width: 100px; display: none;">
                        <ul style="width: 100px;">
                            <asp:Repeater runat="server" ID="rptArea">
                                <ItemTemplate>
                                    <li><a href="javascript:void(0);" value='<%#Eval("CODE") %>'><%#Eval("AREA_NAME") %></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </dd>
                </dl>
        </tr>
        <tr>
            <td colspan="2">
                <asp:RadioButtonList ID="rblFieldType" runat="server" Style="margin-left: 50px; margin-right: 5px; color: #ffffff;" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="5" RepeatLayout="Flow">
                </asp:RadioButtonList>


            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:LinkButton ID="lbtnSearch" runat="server" Style="width: 153px; height: 43px; margin-left: 77px; border: 0px;" OnClick="lbtnSearch_Click">
                   <img src="../Images/search_btn.jpg" />
                </asp:LinkButton>
                <%--                <input type="submit" value="" style="background: url('../Images/search_btn.jpg') no-repeat scroll 0 0 transparent; width: 153px; height: 43px;margin-left:77px;border: 0px;" />--%>
            </td>
        </tr>
    </table>
</div>

