<%@ page language="C#" autoeventwireup="true" inherits="Index, App_Web_qaw3j10d" %>

<!DOCTYPE html>

<html ng-app="tiqiu">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>后台系统</title>
    <!-- <link rel="stylesheet" type="text/css" href="http://g.tbcdn.cn/thx/cube/1.2.1/??cube-min.css,type-min.css"> -->
    <link rel="stylesheet" type="text/css" href="css/bootstrap.css">
    <link rel="stylesheet" type="text/css" href="css/loading-bar.min.css">
    <link rel="stylesheet" type="text/css" href="css/calendar.css">
    <link rel="stylesheet" type="text/css" href="css/font-awesome.css">
    <link rel="stylesheet" type="text/css" href="css/notification.css">
    <link rel="stylesheet" type="text/css" href="css/switch.css">
    <link rel="stylesheet" type="text/css" href="css/style.css">

    <script type="text/javascript" src="lib/jquery-2.1.0.min.js"></script>
    <script type="text/javascript" src="lib/angular.js"></script>
    <script type="text/javascript" src="lib/angular-ui-router.js"></script>
    <script type="text/javascript" src="lib/ui-bootstrap-tpls-0.10.0.min.js"></script>
    <script type="text/javascript" src="lib/loading-bar.min.js"></script>
    <script type="text/javascript" src="lib/lodash.min.js"></script>
    <script type="text/javascript" src="lib/moment-with-langs.min.js"></script>
    <script type="text/javascript" src="src/app.js"></script>
    <script type="text/javascript" src="src/controller.js"></script>
    <script type="text/javascript" src="src/service.js"></script>
    <script type="text/javascript" src="src/directive.js"></script>
    <style type="text/css">
    /*#container {
        position: absolute;
        width: 100%;
        height: 100%;
    }*/
    </style>

</head>

<body>

    <div id="container" data-ui-view="">
    </div>

    <div notifications="bottom right"></div>

</body>

</html>
