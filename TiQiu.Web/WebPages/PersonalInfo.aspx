<%@ Page Language="C#" MasterPageFile="../MasterPages/Content.master" AutoEventWireup="true" CodeBehind="PersonalInfo.aspx.cs" Inherits="TiQiu.Web.WebPages.PersonalInfo" %>

<%@ Register Src="~/UserControl/UploadImageControl.ascx" TagPrefix="uc1" TagName="UploadImageControl" %>
<%@ Register Src="~/UserControl/FindFieldsUserControl/BookHistoryInfoUserControl.ascx" TagPrefix="uc1" TagName="BookHistoryInfoUserControl" %>



<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" runat="server">
    <style type="text/css">
        .history {
            background-color: #ededed;
            padding: 10px;
            width: 281px;
        }

            .history ul li {
                line-height: 28px;
                height: 28px;
                overflow: hidden;
            }

        .personalTable {
            line-height: 30px;
        }

            .personalTable input {
                border: 1px solid #CCCCCC;
                border-radius: 3px 3px 3px 3px;
                box-shadow: 2px 2px 2px #EFEFEF inset;
                color: #999999;
                font-size: 14px;
                height: 24px;
                outline: medium none;
                padding: 0 5px 0 0;
                width: 150px;
            }

            .personalTable .photo {
                width: 250px;
            }

            .personalTable .tit {
                width: 70px;
            }

            .personalTable .inputs {
                width: 250px;
            }

        a.btn {
            background: no-repeat url(/Images/create-btn.png);
            width: 87px;
            height: 25px;
            text-align: center;
            vertical-align: middle;
            font-size: 12px;
            line-height: 25px;
            color: #fff;
            cursor: pointer;
            float: left;
            margin-right: 10px;
        }
    </style>
    <form id="form1" runat="server">
        <div style="margin: 10px;">
            <h2 style="font-size: 14px; border-bottom: 1px solid #000; padding-bottom: 5px; margin-bottom: 10px">基本信息</h2>
            <table class="personalTable" style="width: 100%;">
                <tr>
                    <td rowspan="9" class="photo" style="vertical-align: top">
                        <div style="width: 240px; height: 240px; background: url(/images/dh.jpg)">
                            <asp:Image ID="Image1" runat="server" Width="240" Height="240" />
                        </div>
                        <div id="editAvatr"
                            style="background: no-repeat url(../Images/create-btn.png); margin-top: 10px; width: 87px; height: 25px; text-align: center; vertical-align: middle; font-size: 12px; line-height: 25px; color: #fff; cursor: pointer;">
                            修改头像
                        </div>
                    </td>
                    <td class="tit">昵称：</td>
                    <td class="inputs">
                        <asp:Literal ID="lblNickName" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtNickName" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td class="tit">电话号码：</td>
                    <td class="inputs">
                        <asp:Literal ID="lblCellPhone" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtCellPhone" runat="server" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>性别：</td>
                    <td>
                        <asp:Literal runat="server" ID="lblGender"></asp:Literal>
                        <asp:HiddenField runat="server" ID="hidGender" />
                        <asp:Panel runat="server" ID="tbGender" Visible="false">
                            <dl name="<%=hidGender.ClientID %>" class="select" style="z-index: 0;">
                                <dt><a href="javascript:void(0);">保密</a></dt>
                                <dd style="width: 100px; display: none;">
                                    <ul style="width: 100px;">
                                        <li><a value="0" href="javascript:void(0);">保密</a></li>

                                        <li><a value="1" href="javascript:void(0);">男</a></li>

                                        <li><a value="2" href="javascript:void(0);">女</a></li>

                                    </ul>
                                </dd>
                            </dl>
                        </asp:Panel>
                    </td>
                    <td>出生日期：</td>
                    <td>
                        <asp:Literal runat="server" ID="litBrithday"></asp:Literal>
                        <asp:TextBox ID="tbBrithday" runat="server" Visible="false" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>姓名：</td>
                    <td>
                        <asp:Literal ID="lblName" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtName" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td>职业：</td>
                    <td>
                        <asp:Literal ID="litWork" runat="server"></asp:Literal>
                        <asp:TextBox ID="tbWork" runat="server" Visible="false"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>球龄：</td>
                    <td>
                        <asp:Literal ID="litPlayAge" runat="server"></asp:Literal>
                        <asp:TextBox ID="tbPlayAge" runat="server" Visible="false"></asp:TextBox></td>
                    <td>擅长位置：</td>
                    <td>
                        <asp:Literal ID="lblPosition" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtPosition" runat="server" Visible="false"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>惯用脚：</td>
                    <td>
                        <asp:Literal ID="lblUseFoot" runat="server"></asp:Literal>
                        <asp:HiddenField runat="server" ID="ddlUserFoot" />
                        <asp:Panel runat="server" ID="tbUserFoot" Visible="false">
                            <dl name="<%=ddlUserFoot.ClientID %>" class="select" style="z-index: 0;">
                                <dt><a href="javascript:void(0);">保密</a></dt>
                                <dd style="width: 100px; display: none;">
                                    <ul style="width: 100px;">
                                        <li><a value="0" href="javascript:void(0);">保密</a></li>

                                        <li><a value="1" href="javascript:void(0);">右脚</a></li>

                                        <li><a value="2" href="javascript:void(0);">左脚</a></li>

                                    </ul>
                                </dd>
                            </dl>
                        </asp:Panel>
                    </td>
                    <td>所属球队：</td>
                    <td>
                        <asp:Literal ID="lblBelongTeam" runat="server"></asp:Literal>
                        <asp:HyperLink ID="hlJoinTeam" runat="server" Visible="false" Style="color: cadetblue;">加入球队</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>偶像：</td>
                    <td>
                        <asp:Literal ID="lblLikeStar" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtLikeStar" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td>喜欢球队：</td>
                    <td>
                        <asp:Literal ID="lblLikeTeam" runat="server"></asp:Literal>
                        <asp:TextBox ID="txtLikeTeam" runat="server" Visible="false"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>技术特点：</td>
                    <td colspan="3">
                        <asp:Literal ID="labFeature" runat="server"></asp:Literal>
                        <textarea id="tbFeature" runat="server" visible="false" cols="20" name="S1" rows="2" style="height: 100px; width: 500px;"></textarea></td>
                </tr>
                <tr>
                    <td>个人简介：</td>
                    <td colspan="3">
                        <asp:Literal ID="litIntro" runat="server"></asp:Literal>
                        <textarea id="tbIntro" runat="server" visible="false" cols="20" name="S1" rows="2" style="height: 100px; width: 500px;"></textarea></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:LinkButton ID="lbtnUpdate" runat="server" OnClick="lbtnUpdate_Click" CssClass="btn">修改</asp:LinkButton>
                        <asp:LinkButton ID="lbtnOK" runat="server" Style="margin-right: 10px;" OnClick="lbtnOK_Click" CssClass="btn" Visible="false">确定</asp:LinkButton>
                        <asp:LinkButton ID="lbtnCancel" runat="server" OnClick="lbtnCancel_Click" Visible="false" CssClass="btn">取消</asp:LinkButton>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <h2 style="font-size: 14px; border-bottom: 1px solid #000; padding-bottom: 5px; margin-bottom: 10px; margin-top: 20px">预定历史</h2>
            <uc1:BookHistoryInfoUserControl runat="server" ID="BookHistoryInfoUserControl" />

        </div>
        <div id="imageAvatr" class="popWin pop_winB" style="display: none; width: 600px; height: 400px">
            <div style="background: url(/Images/here-bg.png); text-align: right; height: 30px; padding-right: 5px;"><span style="color: white; width: 30px; line-height: 30px; text-align: center; font-size: 16px; font-weight: 600; cursor: pointer" onclick="imageAvatr.fn.popOut();">×</span></div>
            <div id="avatrEditArea"></div>
        </div>
    </form>

</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="CPHead">
    <script src="../BG/Scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="http://open.web.meitu.com/sources/xiuxiu.js" type="text/javascript"></script>

    <script type="text/javascript">
        var imageAvatr;
        var avatrPath = '<%=ConfigurationManager.AppSettings["FileVirtualPath"] %>';
        var postUrl = "http://<%=Request.Url.Authority %>/WebPages/Upload.ashx";
        var id = '<%=memberID%>';
        $(function () {

            imageAvatr = PopWin("#imageAvatr", {
                animate: true,
                olOpacity: 0.4
            });

            $("#editAvatr").click(function () {
                imageAvatr.fn.popIn();
                xiuxiu.embedSWF("avatrEditArea", 5, "600px", "400px");
                /*第1个参数是加载编辑器div容器，第2个参数是编辑器类型，第3个参数是div容器宽，第4个参数是div容器高*/
                xiuxiu.setUploadURL(postUrl);//修改为上传接收图片程序地址
                xiuxiu.onInit = function () {
                    xiuxiu.loadPhoto("<%=headerPath%>");//修改为要处理的图片url
                }
                xiuxiu.onUploadResponse = function (r) {
                    //更新db

                    $.get("/AjaxHandler/updateMemberAvatr.ashx?id=" + id + "&file=" + r, function (result) {
                        imageAvatr.fn.popOut();
                        $("#<%=Image1.ClientID%>").attr("src", "http://<%=Request.Url.Authority %>" + r);
                    });

                }
            })
        })
    </script>
</asp:Content>
