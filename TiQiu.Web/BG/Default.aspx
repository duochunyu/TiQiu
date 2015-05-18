<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ShiverVin.ECP.WebUI._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html runat="server" xmlns="http://www.w3.org/1999/xhtml" style="overflow: visible;">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>管理中心</title>
    <link href="CSS/DWZ/default/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/DWZ/css/core.css" rel="stylesheet" type="text/css" />
    <link href="Css/Dialog.css" rel="stylesheet" type="text/css" />
    <link href="Css/GroupBox.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/DWZ/speedup.js" type="text/javascript"></script>
    <script src="Scripts/JQuery/jquery-1.4.4.js" type="text/javascript"></script>
    <script src="Scripts/JQuery/jquery.cookie.js" type="text/javascript"></script>
    <script src="Scripts/JQuery/jquery.validate.js" type="text/javascript"></script>
    <script src="Scripts/JQuery/jquery.bgiframe.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.core.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.util.date.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.validate.method.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.regional.zh.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.barDrag.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.drag.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.tree.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.accordion.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.ui.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.theme.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.switchEnv.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.alertMsg.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.contextmenu.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.navTab.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.tab.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.resize.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.jDialog.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.dialogDrag.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.cssTable.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.stable.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.taskBar.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.ajax.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.pagination.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.database.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.datepicker.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.effects.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.panel.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.checkbox.js" type="text/javascript"></script>
    <%--    <script src="Scripts/DWZ/dwz.history.js" type="text/javascript"></script>--%>
    <script src="Scripts/DWZ/dwz.combox.js" type="text/javascript"></script>
    <script src="Scripts/DWZ/dwz.regional.zh.js" type="text/javascript"></script>
    <script src="Scripts/dialog.js" type="text/javascript"></script>
    <script src="Scripts/jquery.messager.js" type="text/javascript"></script>
    <%-- <script src="Scripts/FastServiceTips.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        $(function () {
            DWZ.init("Scripts/DWZ/dwz.frag.xml", {
                loginUrl: "Login.aspx",
                statusCode: { ok: 200, error: 300, timeout: 301 },
                pageInfo: { pageNum: "pageNum", numPerPage: "numPerPage", orderField: "orderField", orderDirection: "orderDirection" },
                debug: false,
                callback: function () {
                    initEnv();
                    $("#themeList").theme({ themeBase: "Css/DWZ" });
                }
            });

        });
        document.oncontextmenu = function (e) { return false; }
    </script>
    <script type="text/javascript">

        function showOrderTips() {
            $.ajax({
                url: "GetRemindList.ashx?DateTime=" + new Date(),
                global: false,
                success: function (data) {
                    if (data != "0") {
                        $.messager.lays(300, 180);
                        $.messager.show('系统提示', '您有' + data + '条需要处理的订单!<a href="main.html" target="navTab" rel="main">查看</a>', 0);
                        document.getElementById('bgsound').play();
                    }
                }
            });
        };

        $(document).ready(function () {
            setInterval(showOrderTips, 5000);
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <embed id="bgsound" loop="0" autostart="false" style="display:none" src="images/notice.mp3"></embed>
        <div id="layout">
            <div id="header">
                <div class="headerNav">
                    <a class="logo" href="">LOGO</a>
                    <ul class="nav">
                        <li>
                            <asp:Label runat="server" ID="lblUserName"></asp:Label></li>
                        <li>
                            <asp:LinkButton ID="lnkBtnLogout" runat="server" OnClick="lnkBtnLogout_Click">安全退出</asp:LinkButton></li>


                    </ul>
                </div>
            </div>
            <div id="leftside">
                <div id="sidebar_s">
                    <div class="collapse">
                        <div class="toggleCollapse">
                            <div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="sidebar">
                    <div class="toggleCollapse">
                        <h2>主菜单</h2>
                        <div>
                            收缩
                        </div>
                    </div>
                    <div class="accordion" fillspace="sidebar">
                        <p id="pNoMenu" runat="server" visible="false">
                            <div class="accordionHeader">
                                <h2>暂无菜单</h2>
                            </div>
                        </p>
                        <!--循环生成菜单-->
                        <div class="accordionContent">
                            <ul class="tree treeFolder">
                                <li><a href="BaseData/MerChantInfoManagerMent.aspx" id="repRootMenu_ctl00_repMenuItem_ctl00_anchorUrl"
                                    target="navTab" rel="main" external="true">场地管理</a></li>
                                <li><a href="" id="A2"
                                    target="navTab" rel="main" external="true">场次规则</a></li>
                                <%--                            <li><a href="BaseData/BookInfoManagerMent.aspx"
                                id="repRootMenu_ctl00_repMenuItem_ctl01_anchorUrl" target="navTab" rel="main"
                                external="true">预订管理</a></li>--%>
                                <li><a href="BaseData/OrderInfoManagerment.aspx"
                                    id="A4" target="navTab" rel="main"
                                    external="true">预订管理</a></li>
                                <li><a href="BaseData/ReportManagement.aspx"
                                    id="repRootMenu_ctl00_repMenuItem_ctl02_anchorUrl" target="navTab" rel="main"
                                    external="true">报表中心</a></li>
                                <li><a href=""
                                    id="A1" target="navTab" rel="main"
                                    external="true">经营分析</a></li>
                                <li><a href=""
                                    id="A3" target="navTab" rel="main"
                                    external="true">系统设置</a></li>

                                <%--li><a href=""
                                id="A1" target="navTab" rel="main" external="true">菜单4</a></li>
                            <li><a href=""
                                id="A2" target="navTab" rel="main" external="true">菜单5</a></li>
                            <li><a href=""
                                id="A3" target="navTab" rel="main" external="true">菜单6</a></li>--%>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div id="container">
                <div id="navTab" class="tabsPage">
                    <div class="tabsPageHeader">
                        <div class="tabsPageHeaderContent">
                            <!-- 显示左右控制时添加 class="tabsPageHeaderMargin" -->
                            <ul class="navTab-tab">
                                <li tabid="main" class="main"><a runat="server" id="LkDefaultPage"><span><span class="home_icon">系统主页</span></span></a></li>
                            </ul>
                        </div>
                        <div class="tabsLeft">
                            left
                        </div>
                        <div class="tabsRight">
                            right
                        </div>
                        <div class="tabsMore">
                            more
                        </div>
                    </div>
                    <ul class="tabsMoreList">
                        <li><a href="javascript:;">我的主页</a></li>
                    </ul>
                    <div class="navTab-panel tabsPageContent">
                        <div class="page">
                            <div style="text-align: center; vertical-align: middle; font-size: 20px; width: 100%; height: 700px; background-image: url('Images/bg.JPG'); background-repeat: repeat;">
                                <div style="text-align: center; margin-top: 150px;">
                                    <table cellpadding="0" cellspacing="0" id="tb_Announcement" style="padding-left: 300px"
                                        valign="top">
                                        <tr>
                                            <td style="height: 10px; text-align: center; width: 600px" valign="top">
                                                <asp:Label ID="lblTitle" runat="server" Font-Size="X-Large"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px; text-align: center; width: 600px" valign="top">&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 400px; text-align: left; width: 600px; text-indent: 2em;" valign="top">
                                                <asp:Label ID="lblContent" runat="server" Font-Size="Large"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--anchorBox 用于实现Tab页打开新的Tab页-->
            <div id="anchorBox" style="display: none">
                <a id="targetAnchor" target="navTab" external="true"></a>
            </div>
            <div id="messageBox" style="display: none">
                <input id="btnShowMessage" type="button" onclick="ShowDwzMessage();" value="ShowMessage" />
            </div>
            <div id="taskbar" style="left: 0px; display: none;">
                <div class="taskbarContent">
                    <ul>
                    </ul>
                </div>
                <div class="taskbarLeft taskbarLeftDisabled" style="display: none;">
                    taskbarLeft
                </div>
                <div class="taskbarRight" style="display: none;">
                    taskbarRight
                </div>
            </div>
            <div id="splitBar">
            </div>
            <div id="splitBarProxy">
            </div>
        </div>
        <div id="footer">
        </div>
        <!--拖动效果-->
        <div class="resizable">
        </div>
        <!--阴影-->
        <div class="shadow" style="width: 508px; top: 148px; left: 296px;">
            <div class="shadow_h">
                <div class="shadow_h_l">
                </div>
                <div class="shadow_h_r">
                </div>
                <div class="shadow_h_c">
                </div>
            </div>
            <div class="shadow_c">
                <div class="shadow_c_l" style="height: 296px;">
                </div>
                <div class="shadow_c_r" style="height: 296px;">
                </div>
                <div class="shadow_c_c" style="height: 296px;">
                </div>
            </div>
            <div class="shadow_f">
                <div class="shadow_f_l">
                </div>
                <div class="shadow_f_r">
                </div>
                <div class="shadow_f_c">
                </div>
            </div>
        </div>
        <!--遮盖屏幕-->
        <div id="alertBackground" class="alertBackground">
        </div>
        <div id="dialogBackground" class="dialogBackground">
        </div>
        <div id='background' class='background'>
        </div>
        <div id='progressBar' class='progressBar'>
            数据加载中，请稍等...
        </div>
        <div id="tipsBar">
        </div>
        <script type="text/javascript">
       
        </script>
    </form>
</body>
</html>
