<%@ Page Title="" Language="C#" MasterPageFile="~/BG/MasterPages/Common.Master" AutoEventWireup="true" CodeBehind="ReportManagement.aspx.cs" Inherits="TiQiu.Web.BG.BaseData.ReportManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend>经营数据统计</legend>
        <div class="groupBoxContent">
            <table class="ui_table" cellpadding="0" cellspacing="1">
                <thead>
                    <tr class="titletr">
                        <th>
                            场地名称
                        </th>
                        <th>
                            场地类型
                        </th>
                        <th>
                            统计时间段
                        </th>
                        <th>
                            预订场次（总场次）
                        </th>
                        <th>
                            预订率
                        </th>
                        <th>
                            应收金额
                        </th>
                        <th>
                            实收金额
                        </th>
                        <th>
                            场均
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td colspan="8">
                            暂无数据!
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </fieldset>
</asp:Content>
