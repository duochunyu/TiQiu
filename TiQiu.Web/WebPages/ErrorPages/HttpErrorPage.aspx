<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HttpErrorPage.aspx.cs" Inherits="TiQiu.Web.WebPages.ErrorPages.HttpErrorPage" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>找场子 - 找场子</title>
  
    <style type="text/css">

    </style>
</head>
<body>
  <div class="">
        <a href="~/index.aspx" class="">找场子</a>
        <div class="">
            <ul>
                <li>
                    <%:this.message%></li>
                <li><a href="~/index.aspx"><span id="tS"></span>返回首页</a></li>
            </ul>
        </div>
    </div>
    <script type="text/javascript">
        function gid(id) { return document.getElementById ? document.getElementById(id) : null; }
        function timeDesc() {
            if (all <= 0) {
                self.location = "/index.aspx";
            }
            var obj = gid("tS");
            if (obj) obj.innerHTML = all + " 秒后";
            all--;
        }
        var all = 8;
        //if (all > 0) window.setInterval("timeDesc();", 1000);
    </script>
</body>
</html>