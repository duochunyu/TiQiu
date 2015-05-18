<%@ Page Language="C#" MasterPageFile="../MasterPages/Content.master" AutoEventWireup="true" CodeBehind="TeamInfo.aspx.cs" Inherits="TiQiu.Web.WebPages.TeamInfo" %>

<%@ Register Src="~/UserControl/TeamInfoUserControl/TeamerInfoListUserControl.ascx"
    TagName="TeamerInfoList" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/TeamInfoUserControl/TeamInfoDetailsUserControl.ascx"
    TagName="TeamInfoDetails" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/TeamInfoUserControl/TeamListUserControl.ascx"
    TagName="TeamInfoList" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/TeamInfoUserControl/TeamRecordHistoryUserControl.ascx"
    TagName="TeamRecordHistory" TagPrefix="uc4" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" runat="server">

    <style>
        .title
        {
            color: #6c9900;
            font-size: 14px;
            padding-bottom: 10px; 
        }

        .list li
        {
            padding: 10px 0px;
            height: 82px;
        }

            .list li img
            {
                float: left;
            }

            .list li p
            {
                margin-left: 145px;
                padding: 5px 0px;
            }

        .box2
        {
            border: 1px solid rgb(217, 215, 215);
            margin-left: 25px;
            padding: 15px;
            width: 520px;
            float: left;
            display: inline;
        }

            .box2 img
            {
                float: left;
            }

            .box2 h2
            {
                font-size: 14px;
                margin-bottom: 10px;
            }

            .box2 h2, .box2 p
            {
                margin-left: 252px;
            }

        .list2 li
        {
            height: 30px;
            line-height: 30px;
            overflow: hidden;
        }

            .list2 li span
            {
                display: inline-block;
                margin: 0px 10px;
            }

        .create-btn
        {
            background: url("../Images/create-btn.png") no-repeat scroll 0 0 transparent;
            color: #FFFFFF;
            display: inline-block;
            height: 25px;
            line-height: 25px;
            text-align: center;
            width: 87px;
        }
    </style>

    <div class="wrapper">
        <uc3:TeamInfoList runat="server" ID="TeamsInfo" />
    </div>
    <div class="wrapper">
        <uc2:TeamInfoDetails runat="server" ID="TeamInfoDetails" />
    </div>
    <div class="wrapper">
        <uc1:TeamerInfoList runat="server" ID="TeamerInfoList" />
    </div>
    <div class="wrapper">
        <uc4:TeamRecordHistory runat="server" ID="TeamRecordHistory" />
    </div>
</asp:Content>
