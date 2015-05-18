<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddBookInfo.ascx.cs" Inherits="TiQiu.Web.UserControl.MangermentSystem.AddBookInfo" %>
<asp:Panel ID="addbookinfos" runat="server" GroupingText="添加预定信息" Height="71px" Width="300px">
    <table>
        <tr>
            <td>
                <label>添加预定人：</label><asp:TextBox runat="server"> 12码足球俱乐部</asp:TextBox>
                <button>查找</button></td>
        </tr>
        <tr>
            <td><button style="text-align:right;" >提交</button></td>
        </tr>
    </table>

</asp:Panel>
