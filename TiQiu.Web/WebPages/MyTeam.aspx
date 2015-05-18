<%@ Page Language="C#" MasterPageFile="../MasterPages/Content.master" AutoEventWireup="true" CodeBehind="MyTeam.aspx.cs" Inherits="TiQiu.Web.WebPages.MyTeam" %>

<%@ Register Src="~/UserControl/TeamInfoUserControl/TeamerInfoListUserControl.ascx"
    TagName="ucTeamerInfoList" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/UploadImageControl.ascx" TagPrefix="uc1" TagName="UploadImageControl" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" runat="server">

    <link href="../Scripts/JQueryUI/css/smoothness/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" />

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            height: 60px;
        }

        .box1 {
            width: 275px;
            margin-left: 15px;
            float: left;
        }

        .title {
            color: #6c9900;
            font-size: 14px;
            padding-bottom: 10px;
        }

        .list li {
            padding: 10px 0px;
            height: 82px;
        }

            .list li img {
                float: left;
            }

            .list li p {
                margin-left: 145px;
                padding: 5px 0px;
            }

        .box2 {
            border: 1px solid rgb(217, 215, 215);
            margin-left: 25px;
            padding: 15px;
            width: 520px;
            float: left;
            display: inline;
        }

            .box2 img {
                float: left;
            }

            .box2 h2 {
                font-size: 14px;
                margin-bottom: 10px;
            }

            .box2 h2, .box2 p {
                margin-left: 252px;
            }

        .list2 li {
            height: 30px;
            line-height: 30px;
            overflow: hidden;
        }

            .list2 li span {
                display: inline-block;
                margin: 0px 10px;
            }

        .create-btn {
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

            <div class="clearfix" style="">
                <div class="form-layout" style="margin-left: 15px; width: 490px; float: left;">
                    <h2 style="font-size: 14px; border-bottom: 1px solid #000; padding-bottom: 5px; margin-bottom: 10px;">基本信息</h2>
                    <p>
                        <label class="lable-normal">球队名称：</label>
                        <asp:TextBox Width="200" class="input_text" ID="txtTeanName" type="text" runat="server" />
                    </p>
                    <div class="clearfix">
                    </div>
                    <p style="margin-top: 2px;">
                        <label class="lable-normal">创建时间：</label>
                        <asp:TextBox Width="200" class="input_text" ID="txtCreateYear" type="text" runat="server" />
                    </p>
                    <div class="clearfix">
                    </div>
                    <div style="width: 40px; height: 40px; text-align: center;">
                        <label style="margin-top: 15px;" class="lable-normal">队徽：</label>
                        <asp:Image ID="TeamImage" runat="server" style="height: 50px;width: 50px;margin-left: 85px;margin-top: -30px;" />
                        <asp:LinkButton ID="lbtnUpdateImage" Visible="false" runat="server" OnClick="lbtnUpdateImage_Click">修改图片</asp:LinkButton>
                        <div id="dUpload" visible="false" runat="server">
                            <uc1:UploadImageControl runat="server" ID="uploadImage" Width="100" FileType="Member_Portrait" OnUploadImageCompleted="uploadImage_UploadImageCompleted" />
                        </div>
                    </div>
                    <p style="margin-top: 15px;">
                        <label class="lable-normal">球队简介：</label>
                        <asp:TextBox Width="200" Height="40" class="input_text" ID="txtDescription" type="text" runat="server" />
                    </p>
                    <asp:Button ID="btnSave" OnClick="btnSave_Click" Text="保存" Style="height: 30px; width: 60px; margin-left: 430px; margin-bottom: -10px; margin-top: -50px;" runat="server" />
                    <div class="hr"></div>
                </div>
                <div class="wrapper">
                    <uc1:ucTeamerInfoList ID="TeamerInfoList" runat="server" />
                </div>

            </div>

            <!----球队列表--->
            <div class="clearfix">
            </div>

        </div>


    </form>

</asp:Content>
