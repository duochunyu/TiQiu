<%@ Page Language="C#" MasterPageFile="../MasterPages/Content.master" AutoEventWireup="true" CodeBehind="FindTeam.aspx.cs" Inherits="TiQiu.Web.WebPages.FindTeam" %>

<%@ Register TagPrefix="uc1" TagName="ucTeamInfoList" Src="~/UserControl/FindTeamsUserControl/SearchTeamInfoListControl.ascx" %>
<%@ Register TagPrefix="uc2" TagName="ucBanner" Src="~/UserControl/FindTeamsUserControl/Banner.ascx" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" runat="server">
    <link href="../Scripts/JQueryUI/css/smoothness/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" />

    <style type="text/css">
        .auto-style1
        {
            width: 100%;
        }
         
        .auto-style2
        {
            height: 60px;
        }

        .box1
        {
            width: 275px;
            margin-left: 15px;
            float: left;
        }

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
    <form id="Form1" runat="server">

        <div id="content">
            <!---搜索---->
            <div class="clearfix" style="">
                <div class="form-layout" style="margin-left: 15px; width: 650px; float: left;">
                    <h2 style="font-size: 14px; border-bottom: 1px solid #000; padding-bottom: 5px; margin-bottom: 10px;">搜索条件</h2>
                    <p>
                        <label class="lable-normal">球队名称：</label><input width="200" class="input_text" id="txtTeamName" type="text" runat="server" />
                        <label class="lable-normal">球员名称：</label><input width="200" class="input_text" id="txtTeamMember" type="text" runat="server" />
                    </p>
                      <asp:LinkButton ID="lbtnSearch" runat="server" style="padding-left: 500px;" OnClick="lbtnSearch_Click"><img src="../Images/search-btn.png" /></asp:LinkButton>
                    <div class="hr"></div>
                </div>
               
                <div class="wrapper">
                    
                      <uc1:ucTeamInfoList ID="TeamInfoList" runat="server" />
                </div>
                
                <div class="wrapper">
                    
                     <uc2:ucBanner ID="Banner" runat="server" />

                </div>
              
            </div>

            <!----球队列表--->
            <div class="clearfix">
            </div>

        </div>


    </form>

</asp:Content>

