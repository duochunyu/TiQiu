<%@ Page Title="" Language="C#" MasterPageFile="~/BG/MasterPages/Common.Master" AutoEventWireup="true" CodeBehind="OrderInfoManagerment.aspx.cs" Inherits="TiQiu.Web.BG.BaseData.OrderInfoManagerment" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href="../../Scripts/JQueryUI/css/smoothness/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" />
    <style type="text/css">
        body, div, dl, dt, dd, ul, ol, li, h1, h2, h3, h4, h5, h6, pre, form, fieldset, input, textarea, p, blockquote, th, td
        {
            padding: 0;
            margin: 0;
        }

        body
        {
            color: #5e5d5d;
            font: 12px/1.5 tahoma,arial,"宋体";
            margin: 0 auto;
        }

        a
        {
            color: #464646;
            outline-style: none;
            text-decoration: none;
        }

            a:hover
            {
                color: #FF4D00;
                cursor: pointer;
                text-decoration: none;
            }

            a.B
            {
                color: #0065CD;
            }

                a.B:hover, a.B:active
                {
                    color: #FF4D00;
                    text-decoration: none;
                }

        .form-layout p
        {
            padding: 5px 0px;
        }

        .box3
        {
            margin-left: 20px;
            margin-top: 20px;
            width: 580px;
        }

        .tableContent
        {
            width: 100%;
            margin-bottom: 20px;
        }

            .tableContent th
            {
                width: 100px;
                height: 30px;
                font-size: 12px;
                font-weight: bold;
                text-align: center;
                padding: 5px 0px;
            }

            .tableContent td
            {
                border: 1px solid #d9d7d7;
                text-align: center;
                height: 20px;
            }


                .tableContent td.booked
                {
                    background-color: #5fca15;
                    cursor: pointer;
                }

                .tableContent td.apply
                {
                    background-color: #aaca15;
                    cursor: pointer;
                }

                .tableContent td.solo
                {
                    background-color: #ff6a00;
                    cursor: pointer;
                }

                .tableContent td.rv
                {
                    background-color: #0094ff;
                    cursor: pointer;
                }

                .tableContent td.accept
                {
                    background-color: #0026ff;
                    cursor: pointer;
                }

                .tableContent td.canbook
                {
                    cursor: pointer;
                }

                .tableContent td.disable
                {
                    background-color: #bebdbd;
                }

        .tab-content
        {
            padding-left: 20px;
            padding-right: 20px;
        }

        .lb-booked
        {
            display: inline-block;
            height: 15px;
            width: 40px;
            background-color: #5fca15;
            margin-bottom: -3px;
            margin-right: 5px;
        }

        .lb-solo
        {
            display: inline-block;
            height: 15px;
            width: 40px;
            margin-left: 20px;
            margin-bottom: -3px;
            margin-right: 5px;
            background-color: #ff6a00;
        }

        .lb-rv
        {
            display: inline-block;
            height: 15px;
            width: 40px;
            margin-left: 20px;
            margin-bottom: -3px;
            margin-right: 5px;
            background-color: #0094ff;
        }

        .lb-accept
        {
            display: inline-block;
            height: 15px;
            width: 40px;
            margin-left: 20px;
            margin-bottom: -3px;
            margin-right: 5px;
            background-color: #0026ff;
        }

        .lb-disable
        {
            display: inline-block;
            height: 15px;
            width: 40px;
            margin-left: 20px;
            margin-bottom: -3px;
            margin-right: 5px;
            background-color: #bebdbd;
        }

        .lb-canbook
        {
            display: inline-block;
            height: 13px;
            width: 40px;
            border: 1px solid #d9d7d7;
            margin-left: 20px;
            margin-bottom: -3px;
            margin-right: 5px;
        }

        .lb-apply
        {
            display: inline-block;
            height: 15px;
            width: 40px;
            margin-left: 20px;
            margin-bottom: -3px;
            margin-right: 5px;
            background-color: #aaca15;
        }

        table
        {
            border-collapse: collapse;
            border-spacing: 0;
        }

        table
        {
            font-size: inherit;
            font: 100%;
        }

        #simplemodal-overlay
        {
            background-color: #000000;
        }

        .actionbutton
        {
            background: no-repeat url(../../Images/create-btn.png);
            width: 87px;
            height: 25px;
            text-align: center;
            vertical-align: middle;
            font-size: 12px;
            line-height: 25px;
            color: #fff;
            cursor: pointer;
        }

        p
        {
            text-align: left;
        }
    </style>

    <script type="text/javascript" src="../../Scripts/JQueryUI/js/jquery-ui-1.10.3.custom.min.js"></script>
    <link href="../../styles/calendar.css" rel="stylesheet" />
    <script src="../../Scripts/jquery.simplemodal.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend>筛选</legend>
        <div class="groupBoxContent">
            <table class="Search_table">
                <tr>
                    <td>
                        <asp:CheckBoxList ID="chklStatusList" runat="server" CellPadding="10" Font-Bold="true" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="chklStatusList_SelectedIndexChanged">
                            <asp:ListItem Text="申请中" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="已预订" Value="2"></asp:ListItem>
                            <asp:ListItem Text="用户已确认" Value="3"></asp:ListItem>
                            <asp:ListItem Text="用户已到达" Value="10"></asp:ListItem>
                            <asp:ListItem Text="比赛结束" Value="20"></asp:ListItem>
                            <asp:ListItem Text="已取消" Value="60"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <fieldset>
        <legend>预订信息</legend>
        <div class="groupBoxContent">
            <asp:Repeater ID="gvOrderList" runat="server">
                <HeaderTemplate>
                    <table class="ui_table" cellpadding="0" cellspacing="1">
                        <thead>
                            <tr class="titletr">
                                <th>场地名称
                                </th>
                                <th>场地类型
                                </th>
                                <th>预订日期
                                </th>
                                <th>
                                    预订类型
                                </th>
                                <th>预订场次
                                </th>
                                <th>预订人
                                </th>
                                <th>当前状态
                                </th>
                                <th>操作
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                             <%#Eval("FIELD_NAME")%> - <%#Eval("FIELD_ITEM_NAME")%>
                        </td>
                      
                        <td>
                            <%#GetFieldType((int)Eval("FIELD_ITEM_TYPE")) %>
                        </td>
                        <td>
                            <%#Eval("ORDER_DATE","{0:D}")%>
                        </td>
                          <td>
                            <%#Eval("TYPE")%>
                        </td>
                        <td>
                            <%#DateTime.Parse(Eval("START_TIME").ToString()).ToString("HH:mm")+" - "+DateTime.Parse(Eval("END_TIME").ToString()).ToString("HH:mm") %>
                        </td>
                        <td>
                            <%#Eval("MEMBER_NAME")%>
                        </td>
                        <td>
                            <%#GetStatusByValue((int)Eval("STATUS")) %>
                        </td>
                        <td>
                            <%#(Eval("STATUS").ToString() == "1" && (Eval("TYPE").ToString()=="普通" || Eval("TYPE").ToString() == "约战"))? "确认":"取消" %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                     </table>
                </FooterTemplate>
            </asp:Repeater>
            <webdiyer:AspNetPager ID="pager" runat="server" OnPageChanged="pager_PageChanged" AlwaysShow="true" CurrentPageButtonPosition="Center"
                Width="100%" HorizontalAlign="center" AlwaysShowFirstLastPageNumber="true" PagingButtonSpacing="5" FirstPageText="首页"
                LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="跳转到: ">
            </webdiyer:AspNetPager>
        </div>
    </fieldset>
    <fieldset>
        <legend id="handleTitle" runat="server">球场</legend>
        <div style="text-align: center; margin: 20px 0px;">
            <asp:LinkButton ID="btnLeft" runat="server" OnClick="btnLeft_Click"><img src="../../Images/point-left.png" style="margin-bottom: -6px;" /></asp:LinkButton>
            <span style="display: inline-block; color: rgb(88, 135, 1); font-size: 14px; margin-top: 0px;">
                <asp:Literal ID="ltlDate" runat="server"></asp:Literal></span>
            <asp:LinkButton ID="btnRight" runat="server" OnClick="btnRight_Click"> <img src="../../Images/point-right.png" style="margin-bottom: -6px;" onclick=""  /></asp:LinkButton>
        </div>
        <div id="tabs">
            <ul>
                <asp:Repeater ID="rtField" runat="server">
                    <ItemTemplate>
                        <li id="field-<%#Eval("ID") %>" tag="field"><a href="#tabs-<%#Container.ItemIndex %>" onclick="__doPostBack('fieldClick','<%#Eval("ID") %>');"><%#Eval("NAME") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <asp:Repeater ID="rtFieldDetail" runat="server" OnItemDataBound="rtFieldDetail_ItemDataBound">
                <ItemTemplate>
                    <div id="tabs-<%#Container.ItemIndex %>" tag="fieldItem">
                        <div style="padding-bottom: 10px; border-bottom: 1px solid #d9d7d7;">
                            <span class="lb-canbook"></span>可预订 <span class="lb-apply"></span>预订申请 <span class="lb-booked"></span>已预订 <span class="lb-solo"></span>单飞 <span class="lb-rv"></span>约战申请(无应战球队) <span class="lb-accept"></span>约战申请(有应战球队) <span class="lb-disable"></span>不可用
                        </div>
                        <ul>
                            <asp:Repeater ID="rtFieldItem" runat="server">
                                <ItemTemplate>
                                    <li id="fieldItem-<%#Eval("ID") %>" tag="fieldItem"><a href="#tabsItem-<%#Container.ItemIndex %>" onclick="__doPostBack('fieldItemClick','<%#Eval("ID") %>');"><%#Eval("BRIEF") %></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <asp:Repeater ID="rtFieldItemDetail" runat="server" OnItemDataBound="rtFieldItemDetail_ItemDataBound">
                            <ItemTemplate>
                                <table id="tabsItem-<%#Container.ItemIndex %>" class="tableContent">
                                    <tr>
                                        <th style="height: 30px; width: 100px;"></th>
                                        <th>周一</th>
                                        <th>周二</th>
                                        <th>周三</th>
                                        <th>周四</th>
                                        <th>周五</th>
                                        <th>周六</th>
                                        <th>周日</th>
                                    </tr>
                                    <asp:Repeater ID="rtScheduled" runat="server" OnItemDataBound="rtScheduled_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("EventTimeRange") %></td>
                                                <asp:Repeater ID="rtInner" runat="server">
                                                    <ItemTemplate>
                                                        <td tid="order" <%#Eval("HTMLAttributeString") %> ><%#Eval("HTMLDisplayString")%></td>
                                                        <%--<td tid="order" class='<%# bool.Parse(Eval("IsOrder").ToString())?"off":"on" %>'
                                                            onclick="ProcessOrder('<%#Eval("ScheduledID")+"','"+Eval("Price")+"','"+Eval("Time")+"','"+Eval("StrDate") %>')"></td>--%>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div id="NoDataForm" style="background-color: rgb(255, 255, 255); width: 500px; padding: 10px; border: 4px solid #AACA15; display: none;">
            <h2 id="h1" runat="server" style="font-size: 14px; color: #AACA15; padding-bottom: 10px;">预订</h2>
            <a href="javascript:void(0);" onclick="javascript: $.modal.close();" style="float: right; font-weight: bold; font-size: 14px; margin-top: -35px;">X</a>
            <div id="Div4" runat="server" class="form-layout">
                <p>
                    场地:<asp:Label ID="lblNoDataForm_Field" runat="server" Text=""></asp:Label>&nbsp;&nbsp;时间:<label id="lblNoDataForm_OrderDate"></label>
                </p>
                <div id="btnOrder" class="actionbutton">预订</div>
                <div id="btnSoloflight" class="actionbutton">单飞</div>
            </div>
        </div>
        <div id="HasForm" style="background-color: rgb(255, 255, 255); width: 500px; padding: 10px; border: 4px solid #AACA15; display: none;">
            <h2 id="h2" runat="server" style="font-size: 14px; color: #AACA15; padding-bottom: 10px;">操作</h2>
            <a href="javascript:void(0);" onclick="javascript: $.modal.close();" style="float: right; font-weight: bold; font-size: 14px; margin-top: -35px;">X</a>
            <div id="Div2" runat="server" class="form-layout">
                <p>
                    场地:<asp:Label ID="lblHasForm_Field" runat="server" Text=""></asp:Label>&nbsp;&nbsp;时间:<label id="lblHasFrom_OrderDate"></label>
                </p>
                <div id="btnHasForm_AcceptBattle" class="actionbutton">应战</div>
                <div id="btnHasForm_AcceptApply" class="actionbutton">接受申请</div>
                <div id="btnHasForm_CancelOrder" class="actionbutton">取消预订</div>
                <div id="btnHasForm_ResultCheckIn" class="actionbutton">结果录入</div>
            </div>
        </div>
        <div id="SoloFlightForm" style="background-color: rgb(255, 255, 255); width: 500px; padding: 10px; border: 4px solid #AACA15; display: none;">
            <h2 id="h10" runat="server" style="font-size: 14px; color: #AACA15; padding-bottom: 10px;">单飞</h2>
            <a href="javascript:void(0);" onclick="javascript: $.modal.close();" style="float: right; font-weight: bold; font-size: 14px; margin-top: -35px;">X</a>
            <div id="Div11" runat="server" class="form-layout">
                <p>
                    场地:<asp:Label ID="lblSoloFlight_Field" runat="server" Text=""></asp:Label>&nbsp;&nbsp;时间:<label id="lblSoloFlight_OrderDate"></label>
                </p>
                <p>
                    单飞成团人数:
                    <select id="stSoloFlight_PeopleNumber">
                        <option value="1">1人</option>
                        <option value="2">2人</option>
                        <option value="3">3人</option>
                        <option value="4">4人</option>
                        <option value="5">5人</option>
                        <option value="6">6人</option>
                        <option value="7">7人</option>
                        <option value="8">8人</option>
                        <option value="9">9人</option>
                        <option value="10">10人</option>
                        <option value="11">11人</option>
                        <option value="12">12人</option>
                        <option value="13">13人</option>
                        <option value="14">14人</option>
                        <option value="15">15人</option>
                        <option value="16">16人</option>
                        <option value="17">17人</option>
                        <option value="18">18人</option>
                        <option value="19">19人</option>
                        <option value="20">20人</option>
                        <option value="21">21人</option>
                        <option value="22">22人</option>
                    </select>
                </p>
                <p>
                    价格:
                    <select id="stSoloFlight_Type">
                        <option value="0">每人</option>
                        <option value="1">每场/AA</option>
                    </select>
                    <input id="txtSoloFlight_Price" type="text" />
                    元
                </p>
                <div id="btnSoloFlight_Publish" class="actionbutton">确认发布</div>
            </div>
        </div>
        <div id="NoDataOrderForm" style="background-color: rgb(255, 255, 255); width: 500px; padding: 10px; border: 4px solid #AACA15; display: none;">
            <h2 id="h5" runat="server" style="font-size: 14px; color: #AACA15; padding-bottom: 10px;">预订</h2>
            <a href="javascript:void(0);" onclick="javascript: $.modal.close();" style="float: right; font-weight: bold; font-size: 14px; margin-top: -35px;">X</a>
            <div id="Div7" runat="server" class="form-layout">
                <p>
                    场地:<asp:Label ID="lblNoData_Field" runat="server" Text=""></asp:Label>&nbsp;&nbsp;时间:<label id="lblNoData_OrderDate"></label>
                </p>
                <p>
                    价格：<label id="lblNoData_Price"></label>
                </p>
                
                <p>
                    预订人联系电话：<input id="txtNoData_RvPerson" type="text" />&nbsp;&nbsp;&nbsp;&nbsp;联系电话：<label id="lblNoData_RvPhone"></label>
                </p>
                <p>
                    约战球队:
                    <select id="stNoData_RvTeam">
                        <option value="0">无</option>
                    </select>
                    &nbsp;&nbsp;&nbsp;&nbsp; 球衣颜色：
                    <select id="stNoData_RvClothColor" tag="clothColor">
                        <option>黄色</option>
                        <option>白色</option>
                        <option>红色</option>
                        <option>黑色</option>
                        <option>橙色</option>
                        <option>绿色</option>
                        <option>蓝色</option>
                        <option>紫色</option>
                        <option>灰色</option>
                    </select>
                </p>
                <%--<p>
                    应战球队联系人:<input id="txtNoData_CgPerson" type="text" />&nbsp;&nbsp;&nbsp;&nbsp;联系电话：<label id="lblNoData_CgPhone"></label>
                </p>
                <p>
                    应战球队:<select id="stNoData_CgTeam">
                        <option value="0">无</option>
                    </select>
                    &nbsp;&nbsp;&nbsp;&nbsp; 球衣颜色：
                    <select id="stNoData_CgClothColor" tag="clothColor">
                        <option>黄色</option>
                        <option>白色</option>
                        <option>红色</option>
                        <option>黑色</option>
                        <option>橙色</option>
                        <option>绿色</option>
                        <option>蓝色</option>
                        <option>紫色</option>
                        <option>灰色</option>
                    </select>
                </p>--%>
                <div id="btnNoData_PublishOrder" class="actionbutton">发布预订</div>
            </div>
        </div>
        <div id="AcceptBattleForm" style="background-color: rgb(255, 255, 255); width: 500px; padding: 10px; border: 4px solid #AACA15; display: none;">
            <h2 id="h3" runat="server" style="font-size: 14px; color: #AACA15; padding-bottom: 10px;">应战</h2>
            <a href="javascript:void(0);" onclick="javascript: $.modal.close();" style="float: right; font-weight: bold; font-size: 14px; margin-top: -35px;">X</a>
            <div id="Div3" runat="server" class="form-layout">
                <p>
                    场地:<asp:Label ID="lblAcceptBattleForm_Field" runat="server" Text=""></asp:Label>&nbsp;&nbsp;时间:<label id="lblAcceptBattleForm_OrderDate"></label>
                </p>
                <p>
                    价格：<label id="lblAcceptBattleForm_Price"></label>
                </p>
                <p>
                    约战预订人：<label id="lblAcceptBattleForm_RvName"></label>&nbsp;&nbsp;联系方式：<label id="lblAcceptBattleForm_RvPhone"></label>
                </p>
                <p>
                    约战球队：<label id="lblAcceptBattleForm_RvTeam"></label>&nbsp;&nbsp;球衣颜色：<label id="lblAcceptBattleForm_ColthColor"></label>
                </p>
                <p>
                    应战联系人：<input id="txtAcceptBattleForm_CgName" type="text" />&nbsp;&nbsp;联系电话：<label id="lblAcceptBattleForm_CgPhone"></label>
                </p>
                <p>
                    应战球队：<select id="stAcceptBattleForm_CgTeam">
                        <option value="0">无</option>
                    </select>
                    <asp:HyperLink ID="HyperLink1" runat="server">创建</asp:HyperLink>
                </p>
                <p>
                    球衣颜色：
                    <select id="stCgColthColor" tag="clothColor">
                        <option>黄色</option>
                        <option>白色</option>
                        <option>红色</option>
                        <option>黑色</option>
                        <option>橙色</option>
                        <option>绿色</option>
                        <option>蓝色</option>
                        <option>紫色</option>
                        <option>灰色</option>
                    </select>
                </p>
                <p>
                    付款类型：<label id="lblAcceptBattleForm_PaymentType"></label>
                </p>
                <div id="btnAccpetBattleForm_PublishOrder" class="actionbutton">发布预订</div>
            </div>
        </div>
        <div id="AcceptApplyForm" style="background-color: rgb(255, 255, 255); width: 500px; padding: 10px; border: 4px solid #AACA15; display: none;">
            <h2 id="h6" runat="server" style="font-size: 14px; color: #AACA15; padding-bottom: 10px;">应战</h2>
            <a href="javascript:void(0);" onclick="javascript: $.modal.close();" style="float: right; font-weight: bold; font-size: 14px; margin-top: -35px;">X</a>
            <div id="Div8" runat="server" class="form-layout">
                <p>
                    约战球队:<label id="lblAcceptApply_RvTeam"></label>&nbsp;&nbsp;<label id="lblAcceptApply_RvClothColor"></label>&nbsp;&nbsp;VS&nbsp;&nbsp;应战球队:<label id="lblAcceptApply_CgTeam"></label>&nbsp;&nbsp;<label id="lblAcceptApply_CgClothColor"></label>
                </p>
                <p>
                    约战方预订人：<label id="lblAcceptApply_RvName"></label>&nbsp;&nbsp;联系人方式：<label id="lblAcceptApply_RvPhone"></label>
                </p>
                <p>
                    应战方预订人：<label id="lblAcceptApply_CgName"></label>&nbsp;&nbsp;联系人方式：<label id="lblAcceptApply_CgPhone"></label>
                </p>
                <p>
                    场地：<asp:Label ID="lblAcceptApply_Field" runat="server" Text=""></asp:Label>&nbsp;&nbsp;时间：<label id="lblAcceptApply_OrderDate"></label>
                </p>
                <p>
                    价格：<label id="lblAcceptApply_Price"></label>
                </p>
                <p>
                    付款类型：<label id="lblAcceptApply_PaymentType"></label>
                </p>
                <div id="btnAcceptApply_ConfirmApply" class="actionbutton">确认申请</div>
                <div id="btnAcceptApply_RejectApply" class="actionbutton">拒绝申请</div>
                <div id="btnAcceptApply_CancelOrder" class="actionbutton" style="display: none;">取消预订</div>
                <div id="btnAcceptApply_CheckInResult" class="actionbutton" style="display: none;">录入结果</div>
            </div>
        </div>
        <div id="ConfirmApplyForm" style="background-color: rgb(255, 255, 255); width: 500px; padding: 10px; border: 4px solid #AACA15; display: none;">
            <h2 id="h7" runat="server" style="font-size: 14px; color: #AACA15; padding-bottom: 10px;">申请确认</h2>
            <a href="javascript:void(0);" onclick="javascript: $.modal.close();" style="float: right; font-weight: bold; font-size: 14px; margin-top: -35px;">X</a>
            <div id="Div9" runat="server" class="form-layout">
                <p>
                    场地：<asp:Label ID="lblConfirmApplyForm_Field" runat="server" Text=""></asp:Label>&nbsp;&nbsp;时间：<label id="lblConfirmApplyForm_OrderDate"></label>
                </p>
                <p>
                    球队:<label id="lblConfirmApplyForm_RvTeam"></label>
                </p>
                <p>
                    预订人：<label id="lblConfirmApplyForm_OrderName"></label>
                </p>
                <p>
                    联系电话：<label id="lblConfirmApplyForm_Phone"></label>
                </p>

                <p>
                    价格：<label id="lblConfirmApplyForm_Price"></label>
                </p>
                <div id="btnConfirmApplyForm_ConfirmApply" class="actionbutton">确认申请</div>
                <div id="btnConfirmApplyForm_RejectApply" class="actionbutton">拒绝申请</div>
                <div id="btnConfirmApplyForm_CancelApply" class="actionbutton">取消</div>
            </div>
        </div>
        <div id="CancelOrderForm" style="background-color: rgb(255, 255, 255); width: 500px; padding: 10px; border: 4px solid #AACA15; display: none;">
            <h2 id="h4" runat="server" style="font-size: 14px; color: #AACA15; padding-bottom: 10px;">取消预订</h2>
            <a href="javascript:void(0);" onclick="javascript: $.modal.close();" style="float: right; font-weight: bold; font-size: 14px; margin-top: -35px;">X</a>
            <div id="Div5" runat="server" class="form-layout">
                <p>
                    场地:<asp:Label ID="lblCancelOrderForm_Field" runat="server" Text=""></asp:Label>&nbsp;&nbsp;时间:<label id="lblCancelOrderForm_OrderDate"></label>
                </p>
                <p>
                    价格：<label id="lblCancelOrderForm_Price"></label>
                </p>
                <p>
                    取消原因：<input id="txtCancelOrderForm_Reson" type="text" />
                </p>
                <div id="btnCancelOrderForm_OK" class="actionbutton">确认</div>
            </div>
        </div>
        <div id="CheckInForm" style="background-color: rgb(255, 255, 255); width: 500px; padding: 10px; border: 4px solid #AACA15; display: none;">
            <h2 id="h8" runat="server" style="font-size: 14px; color: #AACA15; padding-bottom: 10px;">录入结果</h2>
            <a href="javascript:void(0);" onclick="javascript: $.modal.close();" style="float: right; font-weight: bold; font-size: 14px; margin-top: -35px;">X</a>
            <div id="Div6" runat="server" class="form-layout">
                <p>
                    场地:<asp:Label ID="lblCheckInForm_Field" runat="server" Text=""></asp:Label>&nbsp;&nbsp;时间:<label id="lblCheckInForm_OrderDate"></label>
                </p>
                <p>
                    预订人：<label id="lblCheckInForm_OrderName"></label>
                </p>
                <p>
                    联系电话：<label id="lblCheckInForm_ContactPhone"></label>
                </p>
                <p>
                    价格：<label id="lblCheckInForm_Price"></label>
                </p>
                <p>
                    比分： 
                    <label id="lblCheckInForm_RvTeam"></label>
                    &nbsp;&nbsp;
                    <input id="txtCheckInForm_RvScore" type="text" style="width: 30px;" />
                    VS
                    <label id="lblCheckInForm_CgTeam"></label>
                    &nbsp;&nbsp;
                    <input id="txtCheckInForm_CgScore" type="text" style="width: 30px;" />
                </p>
                <p>
                    实收金额：<input id="txtCheckInForm_GetPrice" type="text" style="width: 50px;" />
                    元
                </p>
                <div id="btnCheckInForm_Save" class="actionbutton">保存</div>
            </div>
        </div>
        <div id="CheckInAloneForm" style="background-color: rgb(255, 255, 255); width: 500px; padding: 10px; border: 4px solid #AACA15; display: none;">
            <h2 id="h9" runat="server" style="font-size: 14px; color: #AACA15; padding-bottom: 10px;">单飞</h2>
            <a href="javascript:void(0);" onclick="javascript: $.modal.close();" style="float: right; font-weight: bold; font-size: 14px; margin-top: -35px;">X</a>
            <div id="Div10" runat="server" class="form-layout">
                <p>
                    场地:<asp:Label ID="lblAloneForm_Field" runat="server" Text=""></asp:Label>&nbsp;&nbsp;时间:<label id="lblAloneForm_OrderDate"></label>
                </p>
                <p>
                    参与人数：<input id="txtAloneForm_PersonNumber" type="text" />人
                </p>
                <p>
                    实收金额：<input id="txtAloneForm_GetPrice" type="text" />元
                </p>
                <div id="btnAloneForm_Save" class="actionbutton">保存</div>
            </div>
        </div>
    </fieldset>
    <input id="hdDate" type="hidden" runat="server" />
    <input id="hdCurrentFiledID" type="hidden" runat="server" />
    <input id="hdCurrentItemID" type="hidden" runat="server" />
    <input id="hdItemCurrentPage" type="hidden" runat="server" value="1" />
    <input id="hdItemTotal" type="hidden" runat="server" />
    <input id="hdFieldData" type="hidden" runat="server" />
    <input id="hdScheduledData" type="hidden" runat="server" />
    <input id="hdOrderData" type="hidden" runat="server" />
    <script type="text/javascript">
        var orderID;
        var scheduledID;
        var memberAID;
        var memberBID;
        function ProcessOrder(sid, oid) {
            orderID = oid;
            var fieldID = $("#<%=hdCurrentFiledID.ClientID%>").val();
            var fieldItemID = $("#<%=hdCurrentItemID.ClientID%>").val();
            var fieldName;
            var fieldItemName;
            var field = GetField(fieldID);
            if (field != null) {
                fieldName = field.Name;
                var fieldItem = GetFieldItem(fieldItemID, field.FieldItemList);
                if (fieldItem != null) {
                    fieldItemName = fieldItem.BRIEF;
                }
            }
            scheduledID = sid;
            var s = GetSchedule(sid);
            if (s != null) {
                var price = s.Price;
                var time = s.Date + "    " + s.Time;
                if (oid == 0) {
                    $("#lblNoDataForm_OrderDate").text(time);
                    $("#lblNoData_OrderDate").text(time);
                    $("#lblNoData_Price").text(price);
                    $("#lblAloneForm_OrderDate").text(time);
                    $("#lblSoloFlight_OrderDate").text(time);
                    $("#NoDataForm").modal();
                }
                else {
                    var o = GetOrder(oid);
                    if (o != null) {
                        orderID = oid;
                        var teamInfo = o.TeamScore;
                        if (o.OrderType == 0) {
                            $("#lblConfirmApplyForm_OrderDate").text(time);
                            $("#lblConfirmApplyForm_Price").text(price);
                            if (teamInfo != null) {
                                $("#lblConfirmApplyForm_RvTeam").text(teamInfo.TeamAName);
                                $("#lblConfirmApplyForm_OrderName").text(teamInfo.TeamAContactName);
                                $("#lblConfirmApplyForm_Phone").text(teamInfo.TeamAPhone);
                            }
                            $("#ConfirmApplyForm").modal();
                        }
                        else if (o.OrderType == 1) {
                            $("#lblAcceptBattleForm_OrderDate").text(time);
                            $("#lblAcceptApply_OrderDate").text(time);
                            $("#lblAcceptBattleForm_Price").text(price);
                            $("#lblAcceptApply_Price").text(price);
                            $("#lblAcceptBattleForm_PaymentType").text(o.PaymentType);
                            $("#lblAcceptApply_PaymentType").text(o.PaymentType);
                            $("#lblCheckInForm_OrderDate").text(time);
                            $("#lblCheckInForm_Price").text(price);
                            if (teamInfo != null) {
                                $("#lblAcceptBattleForm_RvTeam").text(teamInfo.TeamAName);
                                $("#lblAcceptBattleForm_ColthColor").text(teamInfo.TeamAColor);
                                $("#lblAcceptBattleForm_RvName").text(teamInfo.TeamAContactName);
                                $("#lblAcceptBattleForm_RvPhone").text(teamInfo.TeamAPhone);
                                $("#lblAcceptApply_RvTeam").text(teamInfo.TeamAName);
                                $("#lblAcceptApply_RvClothColor").text(teamInfo.TeamAColor);
                                $("#lblAcceptApply_RvName").text(teamInfo.TeamAContactName);
                                $("#lblAcceptApply_RvPhone").text(teamInfo.TeamAPhone);
                                $("#lblCheckInForm_OrderName").text(teamInfo.TeamAContactName);
                                $("#lblCheckInForm_ContactPhone").text(teamInfo.TeamAPhone);
                                $("#lblCheckInForm_RvTeam").text(teamInfo.TeamAName);
                                $("#lblCheckInForm_CgTeam").text(teamInfo.TeamBName);
                                if (teamInfo.TeamBID == 0) {
                                    $("#AcceptBattleForm").modal();
                                }
                                else {
                                    $("#lblAcceptApply_CgTeam").text(teamInfo.TeamBName);
                                    $("#lblAcceptApply_CgName").text(teamInfo.TeamBContactName);
                                    $("#lblAcceptApply_CgPhone").text(teamInfo.TeamBPhone);
                                    if (o.OrderStatus == 2 ||
                                        o.OrderStatus == 3 ||
                                        o.OrderStatus == 10) {
                                        $("#btnAcceptApply_ConfirmApply").hide();
                                        $("#btnAcceptApply_RejectApply").hide();
                                        $("#btnAcceptApply_CancelOrder").show();
                                        $("#btnAcceptApply_CheckInResult").show();
                                    }
                                    else if (o.OrderStatus == 1) {
                                        $("#btnAcceptApply_ConfirmApply").show();
                                        $("#btnAcceptApply_RejectApply").show();
                                        $("#btnAcceptApply_CancelOrder").hide();
                                        $("#btnAcceptApply_CheckInResult").hide();
                                    }
                                    $("#AcceptApplyForm").modal();
                                }
                            }
                        }
                        else if (o.OrderType == 2) {
                            $("#lblCancelOrderForm_OrderDate").text(time);
                            $("#lblCancelOrderForm_Price").text(price);
                        }
                    }
                }
            }
        }

        function GetField(fieldID) {
            if (fieldJson != null) {
                for (var i = 0; i < fieldJson.length; i++) {
                    var f = fieldJson[i];
                    if (f.ID == fieldID) {
                        return f;
                    }
                }
            }
            return null;
        }

        function GetFieldItem(fieldItemID, items) {
            if (items != null) {
                for (var i = 0; i < items.length; i++) {
                    var fi = items[i];
                    if (fi.ID == fieldItemID) {
                        return fi;
                    }
                }
            }
            return null;
        }

        function GetSchedule(sid) {
            if (scheduledJson != null) {
                for (var i = 0; i < scheduledJson.length; i++) {
                    var s = scheduledJson[i];
                    if (s.ID == sid) {
                        return s;
                    }
                }
            }
            return null;
        }

        function GetOrder(oid) {
            if (orderJson != null) {
                for (var i = 0; i < orderJson.length; i++) {
                    var o = orderJson[i];
                    if (o.ID == oid) {
                        return o;
                    }
                }
            }
            return null;
        }

        $(function () {
            $("#btnOrder").live("click", function () {
                $.modal.close();
                $("#NoDataOrderForm").modal();
            });
            $("#btnSoloflight").live("click", function () {
                $.modal.close();
                $("#SoloFlightForm").modal();
            });
            $("#btnNoData_PublishOrder").live("click", function () {
                if (memberAID == null) {
                    alert("请选择约战球队联系人!");
                    return;
                }
                var rvTeamID = $("#stNoData_RvTeam").val();
                var cgTeamID = $("#stNoData_CgTeam").val();
                var rvClothColor = $("#stNoData_RvClothColor option:selected").text();
                var cgClothColor = $("#stNoData_CgClothColor option:selected").text();
                var data = memberAID + "$" + memberBID + "$" + rvTeamID + "$" + cgTeamID + "$" + scheduledID + "$" + rvClothColor + "$" + cgClothColor;
                $.modal.close();
                __doPostBack("NoDataOrder", data);
            });
            $("#btnSoloFlight_Publish").live("click", function () {
                var price = $("#txtSoloFlight_Price").val();
                if (price.length == 0) {
                    alert("请输入价格!");
                    return;
                }
                if (isNaN(price)) {
                    alert("请输入数字!");
                    return;
                }
                var peopleNum = $("#stSoloFlight_PeopleNumber").val();
                var type = $("#stSoloFlight_Type").val();
                var data = peopleNum + "$" + price + "$" + type + "$" + scheduledID;
                $.modal.close();
                __doPostBack("PublishSoloFlight", data);
            });
            $("#btnAccpetBattleForm_PublishOrder").live("click", function () {
                if (memberBID == null) {
                    alert("请选择应战球队联系人!");
                    return;
                }
                var cgTeamID = $("#stAcceptBattleForm_CgTeam").val();
                if (cgTeamID == 0) {
                    alert("请选择应战球队!");
                    return;
                }
                var clothColor = $("#stCgColthColor option:selected").text();
                var data = orderID + "$" + scheduledID + "$" + memberBID + "$" + cgTeamID + "$" + clothColor;
                $.modal.close();
                __doPostBack("AccpetBattle", data);
            });
            $("#btnAcceptApply_ConfirmApply,#btnConfirmApplyForm_ConfirmApply").live("click", function () {
                var data = orderID;
                $.modal.close();
                __doPostBack("AccpetApply_ConfirmApply", data);
            });
            $("#btnAcceptApply_RejectApply,#btnConfirmApplyForm_RejectApply").live("click", function () {
                var data = orderID;
                $.modal.close();
                __doPostBack("AccpetApply_RejectApply", data);
            });
            $("#btnAcceptApply_CancelOrder").live("click", function () {
                $.modal.close();
                $("#CancelOrderForm").modal();
            });
            $("#btnAcceptApply_CheckInResult").live("click", function () {
                $.modal.close();
                $("#CheckInForm").modal();
            });
            $("#btnCancelOrderForm_OK").live("click", function () {
                var remark = $("#txtCancelOrderForm_Reson").val();
                var data = orderID + "$" + remark;
                $.modal.close();
                __doPostBack("CancelOrder", data);
            });
            $("#btnCheckInForm_Save").live("click", function () {
                var rvScore = $("#txtCheckInForm_RvScore").val();
                var cgScore = $("#txtCheckInForm_CgScore").val();
                var amount = $("#txtCheckInForm_GetPrice").val();
                if (rv.length == 0) {
                    alert("请输入约战队分数!");
                    return;
                }
                if (cgScore.length == 0) {
                    alert("请输入应战队分数!");
                    return;
                }
                if (amount.length == 0) {
                    alert("请输入价格!");
                    return;
                }
                var data = orderID + "$" + rvScore + "$" + cgScore + "$" + amount;
                $.modal.close();
                __doPostBack("CheckInResult", data);
            });
            $("#btnConfirmApplyForm_CancelApply").live("click", function () {
                $.modal.close();
            });

            var cache = {};
            var current;
            var currentValue;
            function GetMember(id) {
                for (var i = 0; i < current.length; i++) {
                    if (current[i].ID == id) {
                        return current[i];
                    }
                }
            }
            $("#txtNoData_RvPerson,#txtNoData_CgPerson,#txtAcceptBattleForm_CgName").live("focus", function () {
                $(this).autocomplete({
                    source: function (request, response) {
                        var term = request.term;
                        if (term in cache) {
                            data = cache[term];
                            response($.map(data.citylist, function (item) {
                                return { label: item.city, value: item.city }
                            }));
                        } else {
                            $.ajax({
                                url: "../../AjaxHandler/GetMemberInfos.ashx",
                                dataType: "json",
                                data: {
                                    Key: term
                                },
                                success: function (data) {
                                    cache[term] = data;
                                    current = data;
                                    response($.map(data, function (item) {
                                        return {
                                            value: item.Name
                                            , label: item.Name + "(" + item.Phone + ")"
                                            , id: item.ID
                                        }
                                    }));
                                }
                            });
                        }
                    },
                    select: function (event, ui) {
                        var actionID = $(this).attr("id");
                        var member = GetMember(ui.item.id);
                        if (member != null) {
                            if (actionID == "txtNoData_RvPerson") {
                                memberAID = ui.item.id;
                                for (var i = 0; i < member.TeamList.length; i++) {
                                    var team = member.TeamList[i];
                                    $("#stNoData_RvTeam").append("<option value='" + team.ID + "'>" + team.Name + "</option>");
                                }
                                $("#lblNoData_RvPhone").text(member.Phone);
                            }
                            else if (actionID == "txtNoData_CgPerson") {
                                memberBID = ui.item.id;
                                for (var i = 0; i < member.TeamList.length; i++) {
                                    var team = member.TeamList[i];
                                    var option = "<option value='" + team.ID + "'>" + team.Name + "</option>";
                                    $("#stNoData_CgTeam").append(option);
                                }
                                $("#lblNoData_CgPhone").text(member.Phone);
                            }
                            else if (actionID == "txtAcceptBattleForm_CgName") {
                                memberBID = ui.item.id;
                                for (var i = 0; i < member.TeamList.length; i++) {
                                    var team = member.TeamList[i];
                                    var option = "<option value='" + team.ID + "'>" + team.Name + "</option>";
                                    $("#stAcceptBattleForm_CgTeam").append(option);
                                }
                                $("#lblAcceptBattleForm_CgPhone").text(member.Phone);
                            }
                        }
                    },
                    //minLength: 1,
                    minChars: 0,
                    autoFocus: true,
                    delay: 500
                }).live("blur", function () {
                    var actionID = $(this).attr("id");
                    if ((actionID == "txtNoData_RvPerson" && memberAID == null) ||
                        (actionID == "txtNoData_CgPerson" && memberBID == null) ||
                        (actionID == "txtAcceptBattleForm_CgName" && memberBID == null)) {
                        ClearNoData(actionID);
                        $(this).val("");
                    }
                }).live("keydown", function () {
                    var value = $(this).val();
                    if (value != currentValue) {
                        var actionID = $(this).attr("id");
                        ClearNoData(actionID);
                    }
                    currentValue = value;
                });
                function ClearNoData(actionID) {
                    if (actionID == "txtNoData_RvPerson") {
                        memberAID = null;
                        $("#stNoData_RvTeam option[value!='0']").remove();
                        $("#lblNoData_RvPhone").text("");
                    }
                    else if (actionID == "txtNoData_CgPerson") {
                        memberBID = null;
                        $("#stNoData_CgTeam option[value!='0']").remove();
                        $("#lblNoData_CgPhone").text("");
                    }
                    else if (actionID == "txtAcceptBattleForm_CgName") {
                        memberBID = null;
                        $("#stAcceptBattleForm_CgTeam option[value!='0']").append(option);
                        $("#lblAcceptBattleForm_CgPhone").text("");
                    }
                }
            })
        });
    </script>
</asp:Content>
