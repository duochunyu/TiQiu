<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="TiQiu.Web.BG.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Css/desktop.css" rel="stylesheet" />
    <link href="Css/login.css" rel="stylesheet" />
    <script type="text/javascript">
        function changeCode() {
            var imgNode = document.getElementById("vimg");
            imgNode.src = "VerifyCode.ashx?t=" + (new Date()).valueOf();  // 这里加个时间的参数是为了防止浏览器缓存的问题   
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="thead">
            <div class="bg1"></div>
            <div class="bg2"></div>
            <div class="bg3">
                <div class="nav">
                    <ul class="Form FancyForm">

                        <li>

                            <asp:TextBox ID="tbName" runat="server" CssClass="stext" MaxLength="30" />
                            <label id="lblAccount">输入登录账户</label><span
                                class="fff"></span></li>
                        <li>
                            <asp:TextBox ID="tbPwd" runat="server" CssClass="stext" MaxLength="30" TextMode="Password" />
                            <label id="lblPwd">输入登录密码</label><span
                                class="fff"></span></li>
                        <li>
                            <asp:TextBox ID="tbVerifyCode" runat="server" CssClass="stext" Width="100" MaxLength="5" />
                            <span style="width: 185px;"
                                class="fff"></span></li>
                    </ul>
                    <div class="s8">
                        <table>
                            <tbody>
                                <tr style="height: 20px;">
                                    <td>
                                        <img style="cursor: pointer;" src="VerifyCode.ashx" id="vimg" alt="不区分大小写,点击切换" onclick="changeCode()" width="80" height="28" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnLogin" CssClass="sign" runat="server"  OnClick="btnLogin_Click" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="load">
                        <img src="login_files/loading.gif">
                    </div>
                </div>
            </div>
            <div class="bg4">
                <p>
                </p>
            </div>
        </div>
        <div id="bottom"></div>
    </form>
</body>
</html>
