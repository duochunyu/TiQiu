<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<html ng-app="tiqiu">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>踢球去后台管理系统</title>
<%--<link href="//maxcdn.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css" rel="stylesheet">
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script> --%>

    <link rel="stylesheet" type="text/css" href="css/bootstrap.css">
    <link rel="stylesheet" type="text/css" href="css/loading-bar.min.css">
    <link rel="stylesheet" type="text/css" href="css/calendar.css">
    <link rel="stylesheet" type="text/css" href="css/font-awesome.css">
    <link rel="stylesheet" type="text/css" href="css/notification.css">
    <link rel="stylesheet" type="text/css" href="css/switch.css">
    <link rel="stylesheet" type="text/css" href="css/ng-table.min.css">
    <link rel="stylesheet" type="text/css" href="css/style.css">

    <script type="text/javascript" src="lib/jquery-2.1.0.min.js"></script>
    <script type="text/javascript" src="lib/angular.js"></script>
    <script type="text/javascript" src="lib/angular-ui-router.js"></script>
    <script type="text/javascript" src="lib/ui-bootstrap-tpls-0.10.0.min.js"></script>
    <script type="text/javascript" src="lib/ng-table.js"></script>
    <script type="text/javascript" src="lib/loading-bar.min.js"></script>
    <script type="text/javascript" src="lib/lodash.min.js"></script>
    <script type="text/javascript" src="lib/moment-with-langs.min.js"></script>
    <script type="text/javascript" src="lib/swfobject.js"></script>
    <script type="text/javascript">
        var domain = "<%= ConfigurationManager.AppSettings["API_HOST"]%>";
       
    </script>
    <script type="text/javascript" src="src/app.js?t=<%=version %>"></script>
    <script type="text/javascript" src="src/controller.js?t=<%=version %>"></script>
    <script type="text/javascript" src="src/service.js?t=<%=version %>"></script>
    <script type="text/javascript" src="src/directive.js?t=<%=version %>"></script>
    <script type="text/javascript" src="src/filter.js?t=<%=version %>"></script>
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
    
    
</body>

</html>