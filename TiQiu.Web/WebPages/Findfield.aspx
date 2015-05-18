<%@ Page Language="C#" ResponseEncoding="UTF-8" MasterPageFile="../MasterPages/Content.master" AutoEventWireup="true" CodeBehind="Findfield.aspx.cs" Inherits="TiQiu.Web.WebPages.Findfield" %>

<%@ Register Src="~/UserControl/FindFieldsUserControl/BookHistoryInfoUserControl.ascx"
    TagName="ucBookHistoryInfo" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/FindFieldsUserControl/FieldInfoDetailsUserControl.ascx"
    TagName="ucFieldInfoDetails" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/FindFieldsUserControl/FieldsInfoListUserControl.ascx"
    TagName="ucFieldsInfoList" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/FindFieldsUserControl/SearchFieldsUserControl.ascx"
    TagName="ucSearchField" TagPrefix="uc4" %>
<asp:Content ID="Contetnt1" ContentPlaceHolderID="CPHead" runat="server">
    <link href="../Scripts/JQueryUI/css/smoothness/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" />
    <link href="../styles/calendar.css" rel="stylesheet" />
    <script src="../BG/Scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="../Scripts/jquery.simplemodal.js"></script>
    <script src="../Scripts/JQueryUI/js/jquery-ui-1.10.3.custom.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" runat="server">
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="upContent" runat="server" RenderMode="Block">
            <ContentTemplate>
                <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        更新中...
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
                <div id="content">
                    <div class="clearfix" style=" height:180px;float:left;width: 550px;margin-left:50px;">
                        <h2 class="block-title">搜索条件</h2>
                        
                            <table style="empty-cells: show;  font-family: 微软雅黑; border-spacing: 2px; line-height: normal; vertical-align: middle; width: 100%; height: 150px;">
                                <tr>
                                    <td>
                                        <label class="lable-filter ">场地名称：</label>
                                        <input class="input_text" id="txtFieldName" type="text" runat="server" />
                                    </td>
                                    <td>
                                        <label class="lable-filter ">区域：</label>
                                        <asp:DropDownList ID="ddlArea" runat="server" CssClass="ddl-area"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label class="lable-filter ">球场类型：</label>
                                        <asp:CheckBoxList ID="chkFieldType" CssClass="chkbox" CellPadding="5" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                    </td>
                                    <td>
                                        <label class="lable-filter ">价格：</label>
                                        <input class="input_text" id="txtStartPrice" type="text" maxlength="4" runat="server" size="4" />
                                        至
                                        <input class="input_text" id="txtEndPrice" type="text" maxlength="4" runat="server" size="4" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label class="lable-filter ">有空时间：</label>
                                        <input class="input_text Wdate" id="txtStartTime" type="text" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />
                                        至
                                        <input class="input_text Wdate" id="txtEndTime" type="text" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />

                                        <asp:LinkButton ID="LinkButton1" CssClass="btn-search" runat="server" OnClick="lbtnSearch_Click" OnClientClick="return CheckPrice();"><img src="../Images/search-btn.png" /></asp:LinkButton>
                                    </td>

                                </tr>
                                
                            </table>   
                        
                    </div>
                    <div style="height:180px;">                        
                        <uc1:ucBookHistoryInfo ID="ucHistory" runat="server" Visible="false" />
                    </div>
                    <div class="hr" style="margin-left:50px;margin-right:50px;"></div>

                    <div class="clearfix">
                        <div class="field-list">
                            <ul class="tab clearfix">
                                <li class="here">列表</li>
                                <%--<li>地图</li>--%>
                            </ul>
                            <ul class="field-tab-content">

                                <asp:Repeater ID="rtList" runat="server">
                                    <ItemTemplate>
                                        <li <%#CurFieldID.ToString() == Eval("ID").ToString()? "class=\"here\"" :""%>>
                                            <%--<img src="<%#Eval("ADRESS") %>" width="135" height="82" />--%>
                                            
                                            <asp:LinkButton ID="lbtnField" runat="server" OnClick="lbtnField_Click"  CommandArgument='<%#Eval("ID") %>'>
                                                <img src="<%#FileRoot + Eval("PIC_PATH") %>" onerror="this.src='../Images/logo.png'" width="144" height="81" />
                                                <p><%#Eval("NAME") %> </p>
                                            </asp:LinkButton>
                                            <p>
                                                <label id="lblItemCount" runat="server"></label>
                                            </p>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div style="text-align: center; margin: 20px 0px;">
                                    <asp:LinkButton ID="btnItemPrePage" runat="server" OnClick="btnItemPrePage_Click"><img src="../Images/point-left.png" style="margin-bottom: -6px;" /></asp:LinkButton>
                                    <span style="display: inline-block; color: rgb(88, 135, 1); font-size: 14px; margin-top: 0px;">
                                        <asp:Literal ID="ltlPageIndex" runat="server"></asp:Literal></span>
                                    <asp:LinkButton ID="btnItemNextPage" runat="server" OnClick="btnItemNextPage_Click"> <img src="../Images/point-right.png" style="margin-bottom: -6px;" /></asp:LinkButton>
                                </div>
                            </ul>

                        </div>

                        <div class="field-content">
                            <div class="field-info">
                                <img width="256" height="144" src="" onerror="this.src='../Images/logo.png'" id="sellerImg" runat="server" />
                                <div class="field-info-right">
                                    <h2 class="fz-14 mb-10"><strong id="fieldName" runat="server"></strong></h2>
                                    <p>
                                        <label>地址：</label><label id="lblAddress" runat="server"></label>
                                    </p>
                                    <p>
                                        <label>区域：</label><label id="lblArea" runat="server"></label>
                                    </p>
                                    <p>
                                        <label>电话：</label><label id="lblTel" runat="server"></label>
                                    </p>
                                    <p>
                                        <label>手机：</label><label id="lblPhone" runat="server"></label>
                                    </p>
                                </div>
                            </div>
                            <div class="field-scheduled">
                                <ul class="tab clearfix">
                                    <asp:Repeater ID="rtFieldItem" runat="server">
                                        <ItemTemplate>
                                            <li <%#Eval("ID").ToString()!=CurFieldItemID.ToString()?"onclick=\"__doPostBack('li','"+Eval("ID")+"$"+Eval("FIELD_ID")+"')\"":string.Empty %> <%#Eval("ID").ToString()==CurFieldItemID.ToString()?"class=\"here\"":""%>><%#Eval("NAME") %></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <div class="tab-content">

                                    <div style="text-align: center; margin: 20px 0px;">
                                        <asp:LinkButton ID="btnLeft" runat="server" OnClick="btnLeft_Click"><img src="../Images/point-left.png" style="margin-bottom: -6px;" /></asp:LinkButton>
                                        <span style="display: inline-block; color: rgb(88, 135, 1); font-size: 14px; margin-top: 0px;">
                                            <asp:Literal ID="ltlDate" runat="server"></asp:Literal></span>
                                        <asp:LinkButton ID="btnRight" runat="server" OnClick="btnRight_Click"> <img src="../Images/point-right.png" style="margin-bottom: -6px;" onclick=""  /></asp:LinkButton>
                                    </div>
                                    <div class="scheduled-legend">
                                        <span class="lb-on"></span>可预订<span class="lb-disable"></span>已占用<span class="lb-pk"></span>约战中<span class="lb-solo"></span>单飞召集<span class="lb-mine"></span>我的预订
                                    </div>
                                    <table id="order-table">
                                        <tr>
                                            <th width="100px"></th>
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
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                 <div id="OrderRequestForm" style="display: none;">
                    <h2 id="h3" runat="server" >预订类型选择</h2>
                    <a href="javascript:void(0);" onclick="javascript: $.modal.close();" style="float: right; font-weight: bold; font-size: 14px; margin-top: -35px;">X</a>
                    <div id="dOrderForm" runat="server" class="form-layout">
                        <p style="text-align: center; vertical-align: middle; width: 300px;">
                            <div id="dOrderRequestForm_Order" style="background: no-repeat url(../Images/create-btn.png); float:right;margin-left:150px; width: 87px; height: 25px; text-align: center; vertical-align: middle; font-size: 12px; line-height: 25px; color: #fff; cursor: pointer;">预订</div>
                            <div id="dOrderRequestForm_Rvservation" style="background: no-repeat url(../Images/create-btn.png); width: 87px; height: 25px; text-align: center; vertical-align: middle; font-size: 12px; line-height: 25px; color: #fff; cursor: pointer;">约战</div>                           
                        </p>
                    </div>
                    <div id="dLoginForm" runat="server" class="form-layout">
                        <p>
                            <label>用户名：</label><asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                        </p>
                        <p>
                            <label>密&nbsp;&nbsp;&nbsp;码：</label><asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
                        </p>
                        <p style="text-align: center; vertical-align: middle; width: 500px; float: inherit;">
                            <div id="dLoginOK" style="background: no-repeat url(../Images/create-btn.png); width: 87px; height: 25px; text-align: center; vertical-align: middle; font-size: 12px; line-height: 25px; color: #fff; cursor: pointer;">登陆</div>
                            <div id="dLoginCancel" style="background: no-repeat url(../Images/create-btn.png); width: 87px; height: 25px; text-align: center; vertical-align: middle; font-size: 12px; line-height: 25px; color: #fff; cursor: pointer;">取消</div>
                            <p>
                            </p>
                            <p>
                            </p>
                        </p>
                    </div>
                </div>
               <%-- <div id="OrderRequestForm" style="background-color: rgb(255, 255, 255); width: 300px; padding: 10px; border: 4px solid #AACA15; display: none;">
                    <h2 id="h3" runat="server" style="font-size: 14px; color: #AACA15; padding-bottom: 10px;">预订类型选择</h2>
                    <a href="javascript:void(0);" onclick="javascript: $.modal.close();" style="float: right; font-weight: bold; font-size: 14px; margin-top: -35px;">X</a>
                    <div id="dOrderForm" runat="server" class="form-layout">
                        <p style="text-align: center; vertical-align: middle; width: 300px;">
                            <div id="dOrderRequestForm_Order" style="background: no-repeat url(../Images/create-btn.png); float:right;margin-left:150px; width: 87px; height: 25px; text-align: center; vertical-align: middle; font-size: 12px; line-height: 25px; color: #fff; cursor: pointer;">预订</div>
                            <div id="dOrderRequestForm_Rvservation" style="background: no-repeat url(../Images/create-btn.png); width: 87px; height: 25px; text-align: center; vertical-align: middle; font-size: 12px; line-height: 25px; color: #fff; cursor: pointer;">约战</div>                           
                        </p>
                    </div>
                    <div id="dLoginForm" runat="server" class="form-layout">
                        <p>
                            <label>用户名：</label><asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                        </p>
                        <p>
                            <label>密&nbsp;&nbsp;&nbsp;码：</label><asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
                        </p>
                        <p style="text-align: center; vertical-align: middle; width: 500px; float: inherit;">
                            <div id="dLoginOK" style="background: no-repeat url(../Images/create-btn.png); width: 87px; height: 25px; text-align: center; vertical-align: middle; font-size: 12px; line-height: 25px; color: #fff; cursor: pointer;">登陆</div>
                            <div id="dLoginCancel" style="background: no-repeat url(../Images/create-btn.png); width: 87px; height: 25px; text-align: center; vertical-align: middle; font-size: 12px; line-height: 25px; color: #fff; cursor: pointer;">取消</div>
                            <p>
                            </p>
                            <p>
                            </p>
                        </p>
                    </div>
                </div>--%>
                <div id="OrderImmediateForm" style="background-color: rgb(255, 255, 255); width: 500px; padding: 10px; border: 4px solid #AACA15; display: none;">
                    <h2 id="hTitle" runat="server" style="font-size: 14px; color: #AACA15; padding-bottom: 10px;">预订申请</h2>
                    <a href="javascript:void(0);" onclick="javascript: $.modal.close();" style="float: right; font-weight: bold; font-size: 14px; margin-top: -35px;">X</a>
                    <div class="form-layout">
                        <p>
                            <label>场地：</label><asp:Label ID="lblFieldName" runat="server" Text=""></asp:Label>
                            <label style="margin-left: 20px;">时间：</label><span id="sTime"></span>
                        </p>
                        <p>
                            <label>预订人：</label><span id="sOrderContactName" runat="server"></span>
                        </p>
                        <p>
                            <label>联系电话：</label><span id="sContactPhone" runat="server"></span>
                        </p>
                        <p>
                            <label>球队：</label><asp:DropDownList ID="ddlMyTeam" runat="server" tag="myTeam"></asp:DropDownList><asp:HyperLink ID="HyperLink1" runat="server">添加</asp:HyperLink>
                        </p>
                        <p>
                            <label>球衣颜色：</label>
                            <select id="stImClothColor" tag="clothColor">
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
                            <label>价格：</label><span id="sPrice"></span>元
                        </p>
                        <p id="pIsNeedReferee" runat="server">
                            <label>
                                <input id="chkNeedReferee" type="checkbox" tag="needReferee" />
                                需要裁判</label>
                        </p>
                        <p style="text-align: center; vertical-align: middle; width: 500px;">
                            <div id="dSubmitOrder" style="background: no-repeat url(../Images/create-btn.png); width: 87px; height: 25px; text-align: center; vertical-align: middle; font-size: 12px; line-height: 25px; color: #fff; cursor: pointer;">直接预订</div>
                            <p>
                            </p>
                            <p>
                            </p>
                        </p>
                    </div>
                </div>
                <div id="RvForm" style="background-color: rgb(255, 255, 255); width: 500px; padding: 10px; border: 4px solid #AACA15; display: none;">
                    <h2 id="h1" runat="server" style="font-size: 14px; color: #AACA15; padding-bottom: 10px;">约战申请</h2>
                    <a href="javascript:void(0);" onclick="javascript: $.modal.close();" style="float: right; font-weight: bold; font-size: 14px; margin-top: -35px;">X</a>
                    <div id="Div4" runat="server" class="form-layout">
                        <p>
                            <label>场地：</label><asp:Label ID="lblRvFieldName" runat="server" Text=""></asp:Label>
                            <label style="margin-left: 20px;">时间：</label><span id="sRvTime"></span>
                        </p>
                        <p>
                            <label>价格：</label><span id="sRvPrice"></span>元
                        </p>
                        <p>
                            <label>预订人：</label><span id="sRvContactName" runat="server"></span>
                        </p>
                        <p>
                            <label>联系电话：</label><span id="sRvContactPhone" runat="server"></span>
                        </p>
                        <p>
                            <label>约战球队：</label><asp:DropDownList ID="ddlRvMyTeam" runat="server" tag="myTeam"></asp:DropDownList><asp:HyperLink ID="HyperLink3" runat="server" Style="margin-left: 10px;">添加</asp:HyperLink>
                        </p>
                        <p>
                            <label>球衣颜色：</label>
                            <select id="stRvClothColor" tag="clothColor">
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
                            <input id="rdoAA" type="radio" value="1" checked="checked" name="payment" />
                            <label for="rdoAA">AA费用</label>
                            <input id="rdoIPayment" type="radio" value="2" name="payment" style="margin-left: 20px;" />
                            <label for="rdoIPayment">我方付费</label>
                            <input id="rdoLostPayment" type="radio" value="3" name="payment" style="margin-left: 20px;" />
                            <label for="rdoLostPayment">输家付费</label>
                        </p>
                        <p id="pIsNeedReferee2" runat="server">
                            <input id="chkNeedReferee2" type="checkbox" tag="needReferee" />
                            <label for="chkNeedReferee2">
                                需要裁判</label>
                        </p>
                        <p style="text-align: center; vertical-align: middle; width: 500px;">
                            <div id="dSendRv" style="background: no-repeat url(../Images/create-btn.png); width: 87px; height: 25px; text-align: center; vertical-align: middle; font-size: 12px; line-height: 25px; color: #fff; cursor: pointer;">约战申请</div>
                            <div id="dCancel" style="background: no-repeat url(../Images/create-btn.png); width: 87px; height: 25px; text-align: center; vertical-align: middle; font-size: 12px; line-height: 25px; color: #fff; cursor: pointer;">取消</div>
                            <p>
                            </p>
                            <p>
                            </p>
                        </p>
                    </div>
                </div>
                <div id="AcceptBattleForm" style="background-color: rgb(255, 255, 255); width: 500px; padding: 10px; border: 4px solid #AACA15; display: none;">
                    <h2 id="h4" runat="server" style="font-size: 14px; color: #AACA15; padding-bottom: 10px;">应战</h2>
                    <a href="javascript:void(0);" onclick="javascript: $.modal.close();" style="float: right; font-weight: bold; font-size: 14px; margin-top: -35px;">X</a>
                    <div id="Div7" runat="server" class="form-layout">
                        <p>
                            <label>场地：</label><asp:Label ID="lblAcceptFieldName" runat="server" Text=""></asp:Label>
                            <label style="margin-left: 20px;">时间：</label><span id="sAcceptTime"></span>
                        </p>
                        <p>
                            <label>价格：</label><span id="sAcceptPrice"></span>元
                        </p>
                        <p>
                            <table>
                                <tr>
                                    <td>约战球队: <span id="sAcceptRvTeam"></span></td>
                                    <td></td>
                                    <td>应战球队:
                                        <asp:DropDownList ID="ddlAcceptTeam" runat="server" tag="myTeam"></asp:DropDownList><asp:HyperLink ID="HyperLink2" runat="server" Style="margin-left: 10px;">添加</asp:HyperLink></td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: center; vertical-align: middle;">VS</td>
                                </tr>
                                <tr>
                                    <td>球衣颜色：<span id="sAcceptRvClothColor"></span></td>
                                    <td></td>
                                    <td>球衣颜色:<select id="stAcceptCgClothColor" tag="clothColor">
                                        <option>黄色</option>
                                        <option>白色</option>
                                        <option>红色</option>
                                        <option>黑色</option>
                                        <option>橙色</option>
                                        <option>绿色</option>
                                        <option>蓝色</option>
                                        <option>紫色</option>
                                        <option>灰色</option>
                                    </select></td>
                                </tr>
                                <tr>
                                    <td>联系人：<span id="sAcceptRvName"></span>    联系电话：<span id="sAcceptRvPhone"></span></td>
                                    <td></td>
                                    <td>联系人：<span id="sAcceptCgName" runat="server"></span>    联系电话：<span id="sAcceptCgPhone" runat="server"></span></td>
                                </tr>
                            </table>
                        </p>
                        <p>
                            <label>付款方式：</label><span id="sAcceptPaymentType"></span>
                        </p>
                        <p>
                            <label>
                                需要裁判:
                            </label>
                            <span id="sAcceptIsNeedReferee"></span>
                        </p>
                        <p style="text-align: center; vertical-align: middle; width: 500px;">
                            <div id="dAcceptSubmitApply" style="background: no-repeat url(../Images/create-btn.png); width: 87px; height: 25px; text-align: center; vertical-align: middle; font-size: 12px; line-height: 25px; color: #fff; cursor: pointer;">约战申请</div>
                            <p>
                            </p>
                            <p>
                            </p>
                        </p>
                    </div>
                </div>
                <div id="SoloFlight" style="background-color: rgb(255, 255, 255); width: 500px; padding: 10px; border: 4px solid #AACA15; display: none;">
                    <h2 id="h5" runat="server" style="font-size: 14px; color: #AACA15; padding-bottom: 10px;">单飞</h2>
                    <a href="javascript:void(0);" onclick="javascript: $.modal.close();" style="float: right; font-weight: bold; font-size: 14px; margin-top: -35px;">X</a>
                    <div id="Div8" runat="server" class="form-layout">
                        <p>
                            <label>场地：</label><asp:Label ID="lblSoloFlightFieldName" runat="server" Text=""></asp:Label>
                            <label style="margin-left: 20px;">时间：</label><span id="sSoloFlightTime"></span>
                        </p>
                        <p>
                            <label>参与人数：</label>:<select id="stSoloFlightNumber">
                                <option>1</option>
                                <option>2</option>
                                <option>3</option>
                                <option>4</option>
                                <option>5</option>
                                <option>6</option>
                                <option>7</option>
                                <option>8</option>
                                <option>9</option>
                                <option>10</option>
                                <option>11</option>
                                <option>12</option>
                                <option>13</option>
                                <option>14</option>
                                <option>15</option>
                                <option>16</option>
                                <option>17</option>
                                <option>18</option>
                                <option>19</option>
                                <option>20</option>
                            </select>
                        </p>
                        <p>
                            <label>价格：</label><span id="sSoloFlightPrice"></span>元/人
                        </p>
                        <p style="text-align: center; vertical-align: middle; width: 500px;">
                            <div id="dSoloFlightSubmit" style="background: no-repeat url(../Images/create-btn.png); width: 87px; height: 25px; text-align: center; vertical-align: middle; font-size: 12px; line-height: 25px; color: #fff; cursor: pointer;">提交申请</div>
                            <p>
                            </p>
                            <p>
                            </p>
                        </p>
                    </div>
                </div>
                <div id="dSendPhone" style="background-color: rgb(255, 255, 255); width: 500px; padding: 10px; border: 4px solid #AACA15; display: none; z-index: 9999;">
                    <h2 id="h2" runat="server" style="font-size: 14px; color: #AACA15; padding-bottom: 10px;">约战申请</h2>
                    <a href="javascript:void(0);" onclick="javascript: $('#dSendPhone').hide();" style="float: right; font-weight: bold; font-size: 14px; margin-top: -35px;">X</a>
                    <div id="Div5" runat="server" class="form-layout">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </div>
                </div>
                <input id="hdDate" type="hidden" runat="server" />
                <input id="hdCurrentFiledID" type="hidden" runat="server" />
                <input id="hdCurrentItemID" type="hidden" runat="server" />
                <input id="hdItemCurrentPage" type="hidden" runat="server" value="1" />
                <input id="hdItemTotal" type="hidden" runat="server" />

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script>
        var orderForm = $("#<%=dOrderForm.ClientID%>");
        var loginForm = $("#<%=dLoginForm.ClientID%>");
        var orderDate;
        var scheduledID;
        var rivalTeamID;
        var needReferee = false;
        var orderID;
        function CancelOrder(sid,oid){
            if (confirm("是否取消预订？")) {

            }
        }
        function ProcessOrder(sid, oid) {
            if (orderForm.length == 1) {
                var fieldName = $("#<%=fieldName.ClientID%>").text();
                var fieldItemName = $("li.here").text();
                var s = GetSchedule(sid);
                if (s != null) {
                    var price = s.Price;
                    $("#sPrice").text(price);
                    $("#sRvPrice").text(price);
                    $("#sAcceptPrice").text(price);
                    $("#sSoloFlightPrice").text(price);
                    var time = s.Date + "    " + s.Time;
                    $("#sTime").text(time);
                    $("#sRvTime").text(time);
                    $("#sAcceptTime").text(time);
                    $("#sSoloFlightTime").text(time);
                    orderDate = s.Date;
                }
                scheduledID = sid;
                if (oid > 0) {
                    var o = GetOrder(oid);
                    if (o != null) {
                        orderID = oid;
                        if (o.OrderType == 1) {
                            $("#sAcceptPaymentType").text(o.PaymentType);
                            if (o.NeedReferee) {
                                $("#sAcceptIsNeedReferee").text("是");
                            }
                            else {
                                $("#sAcceptIsNeedReferee").text("否");
                            }
                            var teamInfo = o.TeamScore;
                            if (teamInfo != null) {
                                $("#sAcceptRvTeam").text(teamInfo.TeamAName);
                                $("#sAcceptRvClothColor").text(teamInfo.TeamAColor);
                                $("#sAcceptRvName").text(teamInfo.TeamAContactName);
                                $("#sAcceptRvPhone").text(teamInfo.TeamAPhone);
                            }
                            $("#AcceptBattleForm").modal();
                            return;
                        }
                        else if (o.OrderType == 2) {
                            $("#SoloFlight").modal();
                            return;
                        }
                    }
                }
                $("#OrderRequestForm").modal();
            }
            else if (loginForm.length == 1) {
                $("#OrderRequestForm").modal();
            }
        }

        function CheckPrice() {
            var startPrice = $("#<%=txtStartPrice.ClientID%>").val();
            var endPrice = $("#<%=txtEndPrice.ClientID%>").val();
            var pt = /^[1-9]\d?$/;
            if (startPrice.length > 0) {
                if (startPrice.match(pt) == null) {
                    alert("价格请输入数字!");
                    $("#<%=txtStartPrice.ClientID%>").focus();
                    return false;
                }
            }
            if (endPrice.length > 0) {
                if (endPrice.match(pt) == null) {
                    alert("价格请输入数字!");
                    $("#<%=txtEndPrice.ClientID%>").focus();
                    return false;
                }
            }

            return true;
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
            jQuery.fn.center = function () {
                this.css("position", "absolute");
                this.css("top", ($(window).height() - this.height()) / 2 + $(window).scrollTop() + "px");
                this.css("left", ($(window).width() - this.width()) / 2 + $(window).scrollLeft() + "px");
                return this;
            }
            var needReferee1 = $("#<%=pIsNeedReferee.ClientID%>");
            var needReferee2 = $("#<%=pIsNeedReferee2.ClientID%>");
            $(":checkbox[tag='needReferee']").live("change", function () {
                needReferee = $(this).attr("checked");
                $(":checkbox[tag='needReferee']").attr("checked", needReferee);
            });
            $("select[tag='clothColor']").change(function () {
                var value = $(this).find("option[selected]").text();
                var count = $("select[tag='clothColor']").find("option").length;
                for (var i = 0; i < count; i++) {
                    if ($("select[tag='clothColor']").get(0).options[i].text == text) {
                        $("select[tag='clothColor']").get(0).options[i].selected = true;
                        break;
                    }
                }
            });
            $("#dLoginOK").live("click", function () {
                var username = $("#<%=txtUserName.ClientID%>").val();
                var password = $("#<%=txtPassword.ClientID%>").val();
                if (username.length == 0) {
                    alert("请输入用户名!");
                    $("#<%=txtUserName.ClientID%>").focus();
                    return;
                }
                if (password.length == 0) {
                    alert("请输入密码!");
                    $("#<%=txtPassword.ClientID%>").focus();
                    return;
                }
                $.modal.close();
                var data = username + "$" + password;
                __doPostBack("login", data);
            });

            $("#dOrderRequestForm_Order").live("click", function () {
                $.modal.close();
                $("#OrderImmediateForm").modal();
            });

            $("#dOrderRequestForm_Rvservation").live("click", function () {
                $.modal.close();
                $("#RvForm").modal();
            });
            $("#dSoloFlightSubmit").live("click", function () {
                if (orderID != null) {
                    var number = $("#stSoloFlightNumber option:selected").text();
                    var data = orderID + "$" + number;
                    $.modal.close();
                    __doPostBack("soloOrder", data);
                }
            });
            $("#dCancel,#dLoginCancel").live("click", function () {
                $.modal.close();
            });
            $("#dSendRv").live("click", function () {
                var refereeValue = 0;
                if (needReferee) {
                    refereeValue = 1;
                }
                var paymentValue = $(":radio[name='payment']:checked").val();
                var clothColor = $("#stRvClothColor option:selected").text();
                var data = scheduledID + "$" + refereeValue + "$" + paymentValue + "$" + clothColor;
                $("#dSendPhone").hide();
                $.modal.close();
                __doPostBack("sendRv", data);
            });
            $("#dSubmitOrder").live("click", function () {
                var refereeValue = 0;
                if (needReferee) {
                    refereeValue = 1;
                }
                var clothColor = $("#stImClothColor option:selected").text();
                var data = scheduledID + "$" + refereeValue + "$" + clothColor;
                $("#dSendPhone").hide();
                $.modal.close();
                __doPostBack("normalOrder", data);
            });
            $("#dAcceptSubmitApply").live("click", function () {
                if (orderID != null) {
                    var teamID = $("#<%=ddlAcceptTeam.ClientID%>").val();
                    if (teamID == 0) {
                        alert("必须选择一个应战球队！");
                        return;
                    }
                    var clothColor = $("#stAcceptCgClothColor option:selected").text();
                    var data = orderID + "$" + clothColor;
                    $.modal.close();
                    __doPostBack("acceptOrder", data);
                }
            });

            setInterval("refresh();", 5 * 60 * 1000);
            function refresh() {
                $("li.here").click();
            }
        });

    </script>
</asp:Content>
