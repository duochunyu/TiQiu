using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.Biz;
using TiQiu.Common.Util;
using TiQiu.DAL;
using TiQiu.Web.WebPages;

namespace TiQiu.Web.BG.BaseData
{
    public partial class OrderInfoManagerment : System.Web.UI.Page
    {
        public int FieldItemID { get; set; }
        public int FieldID { get; set; }
        private TiQiu.BG.MasterPages.Common commonMaster;
        private List<CustomField> fieldList = new List<CustomField>();
        private List<CustomScheduled> scheduledList = new List<CustomScheduled>();
        private List<CustomOrder> orderList = new List<CustomOrder>();
        private int accountID = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            commonMaster = this.Master as TiQiu.BG.MasterPages.Common;
            if (!IsPostBack)
            {
                SearchData();
                SetWeekTime(0);
                SearchOrder();
            }
            ProcessEvent();
        }

        private void ProcessEvent()
        {
            string eventTarget = Request.Form["__EVENTTARGET"];
            string arg = Request.Form["__EVENTARGUMENT"];
            if (eventTarget == "fieldItemClick")
            {
                hdCurrentItemID.Value = arg;
                SearchData();
            }
            else if (eventTarget == "fieldClick")
            {
                hdCurrentFiledID.Value = arg;
                SearchData();
            }
            else if (eventTarget == "NoDataOrder")
            {
                //没有预定信息时
                var array = arg.Split('$');
                if (array.Length == 7)
                {
                    int memberAID = 0;
                    int memberBID = 0;
                    int rvTeamID = 0;
                    int cgTeamID = 0;
                    int scheduleID = 0;
                    int.TryParse(array[0], out memberAID);
                    int.TryParse(array[1], out memberBID);
                    int.TryParse(array[2], out rvTeamID);
                    int.TryParse(array[3], out cgTeamID);
                    int.TryParse(array[4], out scheduleID);
                    string rvClothColor = array[5];
                    string cgClothColor = array[6];
                    if (memberAID == 0 || scheduleID == 0)
                    {
                        Alert("数据有错误，请重试！");
                        SearchData();
                        return;
                    }
                    //if (cgTeamID > 0)
                    //{
                    //    OrderManager.OrderPK(scheduleID, false, memberAID, rvTeamID, string.Empty, EnumPKPayType.AA, cgTeamID);
                    //}
                    //else
                    //{
                    OrderManager.OrderNormal(scheduleID, memberAID, false, memberBID, rvTeamID, cgTeamID, rvClothColor, cgClothColor);
                    //}
                    SearchData();
                }
            }
            else if (eventTarget == "PublishSoloFlight")
            {
                //发布单飞
                var array = arg.Split('$');
                if (array.Length == 4)
                {
                    int scheduleID = 0;
                    int.TryParse(array[3], out scheduleID);
                    decimal price = 0;
                    decimal.TryParse(array[1], out price);
                    if (scheduleID == 0 || price == 0)
                    {
                        Alert("数据有错误，请重试！");
                        SearchData();
                        return;
                    }
                    int peopleNumber = 0;
                    int.TryParse(array[0], out peopleNumber);
                    EnumPriceUnit type = (EnumPriceUnit)int.Parse(array[2]);
                    OrderManager.OrderFreeTeam(scheduleID, accountID, price, type, peopleNumber);
                    SearchData();
                }
            }
            else if (eventTarget == "AccpetBattle")
            {
                //选择应战人后
                var array = arg.Split('$');
                if (array.Length == 5)
                {
                    int orderID = 0;
                    int.TryParse(array[0], out orderID);
                    int scheduleID = 0;
                    int.TryParse(array[1], out scheduleID);
                    int memberBID = 0;
                    int.TryParse(array[2], out memberBID);
                    int cgTeamID = 0;
                    int.TryParse(array[3], out cgTeamID);
                    string clothColor = array[4];
                    if (orderID == 0 || scheduleID == 0 || memberBID == 0 || cgTeamID == 0)
                    {
                        Alert("数据有错误，请重试！");
                        SearchData();
                        return;
                    }
                    OrderManager.AcceptPK(orderID, memberBID, cgTeamID, accountID, clothColor);
                    SearchData();
                }
            }
            else if (eventTarget == "AccpetApply_ConfirmApply")
            {
                //确认申请
                int orderID = 0;
                int.TryParse(arg, out orderID);
                if (orderID == 0)
                {
                    Alert("数据有错误，请重试！");
                    SearchData();
                    return;
                }
                OrderManager.AcceptOrder(orderID, accountID, string.Empty);
                SearchData();
            }
            else if (eventTarget == "AccpetApply_RejectApply")
            {
                //拒绝申请
                int orderID = 0;
                int.TryParse(arg, out orderID);
                if (orderID == 0)
                {
                    Alert("数据有错误，请重试！");
                    SearchData();
                    return;
                }
                OrderManager.CancelOrderByManager(orderID, accountID, string.Empty);
                SearchData();
            }
            else if (eventTarget == "CancelOrder")
            {
                //取消已预订
                var array = arg.Split('$');
                if (array.Length == 2)
                {
                    int orderID = 0;
                    int.TryParse(array[0], out orderID);
                    if (orderID == 0)
                    {
                        Alert("数据有错误，请重试！");
                        SearchData();
                        return;
                    }
                    string remark = array[1];
                    OrderManager.CancelOrderByManager(orderID, accountID, remark);
                    SearchData();
                }
            }
            else if (eventTarget == "CheckInResult")
            {
                //录入结果
                var array = arg.Split('$');
                if (array.Length == 4)
                {
                    int orderID = 0;
                    int.TryParse(array[0], out orderID);
                    if (orderID == 0)
                    {
                        Alert("数据有错误，请重试！");
                        SearchData();
                        return;
                    }
                    int rvScore = 0;
                    if (!int.TryParse(array[1], out rvScore))
                    {
                        Alert("数据有错误，请重试！");
                        SearchData();
                        return;
                    }
                    int cgScore = 0;
                    if (!int.TryParse(array[2], out cgScore))
                    {
                        Alert("数据有错误，请重试！");
                        SearchData();
                        return;
                    }
                    decimal amount = 0;
                    if (!decimal.TryParse(array[3], out amount))
                    {
                        Alert("数据有错误，请重试！");
                        SearchData();
                        return;
                    }
                    OrderManager.CheckInOrder(orderID, accountID, amount, rvScore, cgScore);
                    SearchData();
                }
            }
        }

        private void SearchData()
        {
            if (hdDate.Value.Trim() == string.Empty)
            {
                DateTime monday = GetMondayDate(DateTime.Now);
                hdDate.Value = monday.ToString("yyyy-MM-dd");
            }
            int totalCount = 0;
            //var list = FieldManager.GetFieldList(s => s.BUSINESSES_ID == BusinessID, "ID", false, 1, 99, out totalCount);
            int fID = 0;
            int.TryParse(hdCurrentFiledID.Value.Trim(), out fID);
            //TODO 商家Id
            var list = FieldManager.GetFieldList(s => true, "ID", false, 1, 99, out totalCount);
            var firstField = list.FirstOrDefault();
            if (fID == 0 && firstField != null)
            {
                fID = firstField.ID;
                hdCurrentFiledID.Value = firstField.ID.ToString();
            }
            var currentField = list.FirstOrDefault(s => s.ID == fID);
            if (currentField != null)
            {
                lblAcceptApply_Field.Text = lblAcceptBattleForm_Field.Text = lblAloneForm_Field.Text =
                lblCancelOrderForm_Field.Text = lblCheckInForm_Field.Text = lblConfirmApplyForm_Field.Text =
                lblHasForm_Field.Text = lblNoData_Field.Text = lblNoDataForm_Field.Text = lblSoloFlight_Field.Text = currentField.NAME;
            }

            foreach (var item in list)
            {
                int fieldID = item.ID;
                var customFiled = new CustomField
                {
                    ID = item.ID,
                    NAME = item.NAME
                };
                fieldList.Add(customFiled);
                if (fID != fieldID)
                {
                    continue;
                }
                var fieldItemList = FieldManager.GetFieldItemList(s => s.FIELD_ID == fieldID, "ID", false, 1, 999, out totalCount);
                if (fieldItemList != null)
                {
                    int fiID = 0;
                    int.TryParse(hdCurrentItemID.Value.Trim(), out fiID);
                    var firstItem = fieldItemList.FirstOrDefault();
                    if ((fiID == 0 && firstItem != null) || !fieldItemList.Any(s => s.ID == fiID))
                    {
                        fiID = firstItem.ID;
                        hdCurrentItemID.Value = firstItem.ID.ToString();
                    }
                    foreach (var fieldItem in fieldItemList)
                    {
                        var customFieldItem = new CustomFieldItem
                        {
                            ID = fieldItem.ID,
                            BRIEF = fieldItem.BRIEF,
                            FieldID = fieldItem.FIELD_ID
                        };
                        customFiled.FieldItemList.Add(customFieldItem);
                        if (fieldItem.ID != fiID)
                        {
                            continue;
                        }

                        DateTime startTime = GetMondayDate();
                        var sdList = FieldManager.GetScheduledList(fieldItem.ID, startTime, startTime.AddDays(6)).OrderBy(s => s.START_TIME.Hours).ToList();
                        if (sdList != null)
                        {
                            foreach (var s in sdList)
                            {
                                TimeSpan start = s.START_TIME;
                                TimeSpan end = s.END_TIME;
                                var csScheduled = new CustomScheduled
                                {
                                    ID = s.ID,
                                    Date = s.SCHEDULED_DATE.ToString("yyyy-MM-dd"),
                                    FieldID = s.FIELD_ID,
                                    FieldItemBrief = fieldItem.BRIEF,
                                    FieldItemID = fieldItem.ID,
                                    FieldName = item.NAME,
                                    Price = s.PRICE,
                                    Time = (start.Hours < 10 ? "0" + start.Hours.ToString() : start.Hours.ToString()) + ":" + (start.Minutes < 10 ? "0" + start.Minutes.ToString() : start.Minutes.ToString()) + "-" +
                                            (end.Hours < 10 ? "0" + end.Hours.ToString() : end.Hours.ToString()) + ":" + (end.Minutes < 10 ? "0" + end.Minutes.ToString() : end.Minutes.ToString())
                                };
                                scheduledList.Add(csScheduled);
                            }
                        }
                        var oList = OrderManager.GetOrderList(fieldItem.ID, startTime, startTime.AddDays(6));
                        if (oList != null)
                        {
                            foreach (var o in oList)
                            {
                                if (o.STATUS == (int)EnumFieldStatus.Canceled ||
                                    o.STATUS == (int)EnumFieldStatus.Void)
                                {
                                    continue;
                                }
                                var csOrder = new CustomOrder
                                {
                                    ID = o.ID,
                                    FieldID = item.ID,
                                    FieldItemID = fieldItem.ID,
                                    ScheduledID = o.FIELD_SCHEDULED_ID,
                                    MemberID = o.MEMBER_ID,
                                    NeedReferee = o.NEED_REFEREE,
                                    OrderStatus = o.STATUS,
                                    OrderType = o.TYPE
                                };
                                if (o.PK_PAY_TYPE == 1)
                                {
                                    csOrder.PaymentType = "AA制";
                                }
                                else if (o.PK_PAY_TYPE == 2)
                                {
                                    csOrder.PaymentType = "约战方付费";
                                }
                                else if (o.PK_PAY_TYPE == 3)
                                {
                                    csOrder.PaymentType = "输家付款";
                                }
                                if (o.TEAM_SCORE != null)
                                {
                                    var ts = o.TEAM_SCORE;
                                    int teamAID = ts.TEAM_A_ID;
                                    int teamBID = ts.TEAM_B_ID.GetValueOrDefault(0);
                                    //TODO：根据TeamID获取电话信息
                                    var memberA = GetMember(o.MEMBER_ID);
                                    var memberB = GetMember(o.MEMBERB_ID.GetValueOrDefault(0));
                                    csOrder.TeamScore = new CustomTeamScore
                                    {
                                        OrderID = o.ID,
                                        TeamAColor = ts.TEAM_A_COLOR,
                                        TeamAID = ts.TEAM_A_ID,
                                        TeamAName = ts.TEAM_A_NAME,
                                        TeamAPhone = memberA.CELLPHONE,
                                        TeamAContactName = memberA.NAME,
                                        TeamBID = ts.TEAM_B_ID.GetValueOrDefault(0),
                                        TeamBColor = ts.TEAM_B_COLOR,
                                        TeamBName = ts.TEAM_B_NAME,
                                    };
                                    if (memberB != null)
                                    {
                                        csOrder.TeamScore.TeamBContactName = memberB.NAME;
                                        csOrder.TeamScore.TeamBPhone = memberB.CELLPHONE;
                                    }
                                }
                                orderList.Add(csOrder);
                            }
                        }
                    }
                }
            }
            rtField.DataSource = rtFieldDetail.DataSource = fieldList;
            rtField.DataBind();
            rtFieldDetail.DataBind();

            JavaScriptSerializer jsonSerial = new JavaScriptSerializer();
            string fieldJson = jsonSerial.Serialize(fieldList);
            string scheduledJson = jsonSerial.Serialize(scheduledList);
            string orderJson = jsonSerial.Serialize(orderList);
            commonMaster.RegisterScript("field", string.Format("var fieldJson={0};", fieldJson));
            commonMaster.RegisterScript("scheduled", string.Format("var scheduledJson={0};", scheduledJson));
            commonMaster.RegisterScript("order", string.Format("var orderJson={0};", orderJson));
            BindJavascript();
        }

        private void BindJavascript()
        {
            commonMaster.RegisterScript("tabs", " $(function () {var findex=$(\"li[tag='field']\").index($(\"li[id='field-" + hdCurrentFiledID.Value + "']\"));" +
                                                "var fItemindex=$(\"li[tag='fieldItem']\").index($(\"li[id='fieldItem-" + hdCurrentItemID.Value + "']\"));" +
                                                "$(\"#tabs\").tabs({ active: findex }); $(\"div[tag='fieldItem']\").tabs({ active: fItemindex }); });");
        }

        private void SearchOrder()
        {
            List<int> selectedValue = new List<int>();
            foreach (ListItem item in chklStatusList.Items)
            {
                if (item.Selected)
                {
                    selectedValue.Add(int.Parse(item.Value));
                }
            }
            Expression<Func<V_FIELD_ORDER, bool>> express = PredicateExtensionses.True<V_FIELD_ORDER>();
            //express = express.And(s => s.BUSINESSES_ID == BusinessID);
            express = express.And(s => selectedValue.Contains(s.STATUS));
            int totalCount = 0;
            var list = OrderManager.GetOrderViewList(express, "ID", false, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            gvOrderList.DataSource = list;
            gvOrderList.DataBind();
        }

        protected string GetStatusByValue(int status)
        {
            switch (status)
            {
                case 1:
                    return "申请中";
                case 2:
                    return "已预订";
                case 3:
                    return "用户已确认";
                case 10:
                    return "用户已到达";
                case 20:
                    return "比赛结束";
                case 50:
                    return "预订过期";
                case 60:
                    return "已取消";
                case -1:
                    return "不可用";
                default:
                    return "无";
            }
        }

        protected string GetFieldType(int type)
        {
            switch (type)
            {
                case 5:
                    return "5人制";
                case 7:
                    return "7人制";
                case 9:
                    return "9人制";
                case 11:
                    return "11人制";
                default:
                    return string.Empty;
            }
        }

        private void Alert(string message)
        {
            commonMaster.RegisterScript("message", string.Format("alert('{0}')", message));
        }

        private MEMBER GetMember(int memberID)
        {
            int totalCount = 0;
            var memberList = MemberManager.GetMemberList(s => s.ID == memberID, 1, 99, out totalCount);
            if (memberList != null)
            {
                return memberList.FirstOrDefault();
            }
            return null;
        }

        protected void btnLeft_Click(object sender, EventArgs e)
        {
            SetWeekTime(-7);
            //int fieldItemID = int.Parse(hdCurrentItemID.Value);
            SearchData();
        }

        protected void btnRight_Click(object sender, EventArgs e)
        {
            SetWeekTime(7);
            //int fieldItemID = int.Parse(hdCurrentItemID.Value);
            SearchData();
        }

        protected void rtScheduled_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var rt = e.Item.FindControl("rtInner") as Repeater;
            var weekInfo = e.Item.DataItem as FieldWeekInfo;
            rt.DataSource = weekInfo.ScheduledInfos;
            rt.DataBind();
        }

        private void SetWeekTime(int day)
        {
            DateTime dt;
            if (DateTime.TryParse(hdDate.Value, out dt))
            {
                dt = GetMondayDate(dt);
                dt = dt.AddDays(day);
                hdDate.Value = dt.ToString("yyyy-MM-dd");
                ltlDate.Text = dt.ToString("yyyy-MM-dd") + "至" + dt.AddDays(6).ToString("yyyy-MM-dd");
            }
        }

        private DateTime GetMondayDate()
        {
            DateTime dt = GetMondayDate(DateTime.Now);
            if (hdDate.Value.Trim() != string.Empty)
            {
                DateTime tmp = DateTime.Now;
                if (DateTime.TryParse(hdDate.Value.Trim(), out tmp))
                {
                    dt = tmp;
                }
            }

            return dt;
        }

        private DateTime GetMondayDate(DateTime someDate)
        {
            int i = someDate.DayOfWeek - DayOfWeek.Monday;
            if (i == -1) i = 6;// i值 > = 0 ，因为枚举原因，Sunday排在最前，此时Sunday-Monday=-1，必须+7=6。 
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return someDate.Subtract(ts);
        }

        protected void rtFieldDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var field = e.Item.DataItem as CustomField;
            if (field == null)
            {
                return;
            }
            //int totalCount = 0;
            //var list = FieldManager.GetFieldItemList(s => s.FIELD_ID == field.ID, "ID", true, 1, 999, out totalCount);
            var list = field.FieldItemList;
            var rtFieldItem = e.Item.FindControl("rtFieldItem") as Repeater;
            var rtFieldItemDetail = e.Item.FindControl("rtFieldItemDetail") as Repeater;
            if (rtFieldItem != null)
            {
                rtFieldItem.DataSource = list;
                rtFieldItem.DataBind();
            }
            if (rtFieldItemDetail != null)
            {
                rtFieldItemDetail.DataSource = list;
                rtFieldItemDetail.DataBind();
            }
        }

        protected void rtFieldItemDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var fieldItem = e.Item.DataItem as CustomFieldItem;
            if (fieldItem == null)
            {
                return;
            }
            var rtScheduled = e.Item.FindControl("rtScheduled") as Repeater;
            if (rtScheduled != null)
            {
               // GetScheduledList(fieldItem.ID, rtScheduled);
                rtScheduled.DataSource = GetScheduledList(fieldItem.ID);
                rtScheduled.DataBind();
            }
        }
        private List<FieldWeekInfo> GetScheduledList(int fieldItemID)
        {
            DateTime startTime = GetMondayDate();
            var list = OrderManager.GetFieldScheduledInfoList(fieldItemID, startTime, startTime.AddDays(6));
            List<FieldWeekInfo> fieldWeekInfoList = new List<FieldWeekInfo>();
            foreach (var item in list)
            {
                //item.IsMyOrder = IsLogin
                //    && item.OrderID.HasValue
                //    && (item.MemberID.GetValueOrDefault(0) == CurMember.ID
                //        || (item.SoloLogList != null && item.SoloLogList.Exists(s => s.MemberID == CurMember.ID)));
                //item.HTMLAttributeString = GetAttrStringForFieldCell(item);
                //item.HTMLDisplayString = GetDisplayInfo(item);
                if (item.SoloLogList != null) item.SoloLogList.ForEach(s => item.SoloPalyerCount += s.CountMember);
                InitHTMLAttribute(item);

                var csScheduled = new CustomScheduled
                {
                    ID = item.ScheduledID,
                    Date = item.ScheduledDate.ToString("yyyy-MM-dd"),
                    FieldID = item.FieldID,
                    FieldItemID = item.FieldItemID,
                    Price = item.Price,
                    Time = item.StartTime.ToString(@"hh\:mm") + "-" + item.EndTime.ToString(@"hh\:mm")
                    //Time = (item.StartTime.Hours < 10 ? "0" + item.StartTime.Hours.ToString() : item.StartTime.Hours.ToString()) + ":" + (item.StartTime.Minutes < 10 ? "0" + item.StartTime.Minutes.ToString() : item.StartTime.Minutes.ToString()) + "-" +
                    //           (item.EndTime.Hours < 10 ? "0" + item.EndTime.Hours.ToString() : item.EndTime.Hours.ToString()) + ":" + (item.EndTime.Minutes < 10 ? "0" + item.EndTime.Minutes.ToString() : item.EndTime.Minutes.ToString())

                };
                scheduledList.Add(csScheduled);
                FieldWeekInfo curWeekInfo = fieldWeekInfoList.SingleOrDefault(w => w.EventTimeRange == csScheduled.Time);
                if (curWeekInfo == null)
                {
                    curWeekInfo = new FieldWeekInfo();
                    fieldWeekInfoList.Add(curWeekInfo);
                }
                curWeekInfo.ScheduledInfos.Add(item);
                curWeekInfo.EventTimeRange = csScheduled.Time;


                try
                {
                    if (item.OrderID.HasValue)
                    {
                        var csOrder = new CustomOrder
                        {
                            ID = item.OrderID.GetValueOrDefault(0),
                            ScheduledID = item.ScheduledID,
                            MemberID = item.MemberID.GetValueOrDefault(0),
                            NeedReferee = item.NeedReferee.GetValueOrDefault(false),
                            OrderType = (int)item.OrderType.GetValueOrDefault(0)
                        };
                        if (item.PkPayType == 1)
                        {
                            csOrder.PaymentType = "AA制";
                        }
                        else if (item.PkPayType == 2)
                        {
                            csOrder.PaymentType = "约战方付费";
                        }
                        else if (item.PkPayType == 3)
                        {
                            csOrder.PaymentType = "输家付款";
                        }
                        if (item.OrderType.Value == (int)EnumOrderType.PK)
                        {

                            csOrder.TeamScore = new CustomTeamScore
                            {
                                OrderID = item.OrderID.Value,
                                TeamAColor = item.TeamColor,
                                TeamAID = item.TeamID.Value,
                                TeamAName = item.TeamName,
                                TeamAPhone = "",
                                TeamAContactName = item.MemberName
                            };

                        }
                        orderList.Add(csOrder);
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                }


            }

            for (int i = 0; i < 7; i++)
            {
                var dt = startTime.AddDays(i);

                foreach (var item in fieldWeekInfoList)
                {
                    if (!item.ScheduledInfos.Exists(s => s.ScheduledDate == dt.Date))
                    {
                        FieldScheduledInfo scheduleInfo = new FieldScheduledInfo
                        {
                            ScheduledDate = dt,
                            Status = (int)EnumFieldStatus.Void
                        };
                        InitHTMLAttribute(scheduleInfo);
                        item.ScheduledInfos.Add(scheduleInfo);
                    }
                }
            }
            return fieldWeekInfoList;
        }

        protected void InitHTMLAttribute(FieldScheduledInfo scheduled)
        {
            List<string> cssClass = new List<string>();
            string displayString = string.Empty;
            string tooltip = string.Empty;
            string clickEvent = string.Empty;
            if (scheduled.ScheduledDate < DateTime.Now.Date ||
                (scheduled.ScheduledDate == DateTime.Now.Date && scheduled.StartTime.Hours <= DateTime.Now.Hour))
            {
                cssClass.Add("disable");
            }
            else
            {
                displayString = scheduled.Price.ToString("###");
                tooltip = string.Format("场次：{0} {1}-{2}\n费用：{3}",
                    scheduled.ScheduledDate.ToShortDateString(),
                    scheduled.StartTime.ToString(@"hh\:mm"),
                    scheduled.EndTime.ToString(@"hh\:mm"),
                    scheduled.Price.ToString("###"));

                switch (scheduled.Status)
                {
                    case (int)EnumFieldStatus.Void:
                        cssClass.Add("disable");
                        displayString = string.Empty;
                        break;
                    case (int)EnumFieldStatus.Available:
                        cssClass.Add("on");
                        tooltip = "空闲 \n" + tooltip;
                        clickEvent = string.Format("ProcessOrder('{0}','{1}')", scheduled.ScheduledID, 0);
                        break;
                    case (int)EnumFieldStatus.Booking:
                        cssClass.Add("booking");
                        if (scheduled.IsMyOrder) cssClass.Add("mine");
                        clickEvent = string.Format("ProcessOrder('{0}','{1}')", scheduled.ScheduledID, scheduled.OrderID);
                        switch (scheduled.OrderType)
                        {
                            case (int)EnumOrderType.Normal:
                                cssClass.Add("normal");
                                displayString = "已申请！";
                                
                                tooltip = string.Format("会员[{0}]发起预订申请! \n", scheduled.MemberName) + tooltip;
                                break;
                            case (int)EnumOrderType.PK:
                                cssClass.Add("pk");
                                displayString = "PK！";
                                //clickEvent = scheduled.IsMyOrder ? string.Format("CancelOrder('{0}','{1}')", scheduled.ScheduledID, scheduled.OrderID) :
                                //    string.Format("ProcessOrder('{0}','{1}')", scheduled.ScheduledID, scheduled.OrderID);
                                tooltip = string.Format("会员[{0}]发起约战! \n", scheduled.MemberName) + (scheduled.TeamID.HasValue ? scheduled.TeamName + "\n" : string.Empty) + tooltip;
                                break;
                            case (int)EnumOrderType.FreeTeam:
                                cssClass.Add("solo");
                                displayString = "单飞！";
                                //clickEvent = scheduled.IsMyOrder ? string.Format("CancelOrder('{0}','{1}')", scheduled.ScheduledID, scheduled.OrderID) :
                                //    string.Format("ProcessOrder('{0}','{1}')", scheduled.ScheduledID, scheduled.OrderID);
                                tooltip = string.Format("单飞:人数限制[{0}]!当前人数[{1}] \n", scheduled.SoloMinPlayer.GetValueOrDefault(0), scheduled.SoloPalyerCount) + tooltip;
                                break;
                            default:
                                break;
                        }
                        break;
                    case (int)EnumFieldStatus.Booked:

                        cssClass.Add("booked");
                        if (scheduled.IsMyOrder) cssClass.Add("mine");
                        clickEvent = string.Format("ProcessOrder('{0}','{1}')", scheduled.ScheduledID, scheduled.OrderID);
                        switch (scheduled.OrderType)
                        {
                            case (int)EnumOrderType.Normal:
                                cssClass.Add("normal");
                                displayString = "已预订";
                                //clickEvent = scheduled.IsMyOrder ? string.Format("CancelOrder('{0}','{1}')", scheduled.ScheduledID, scheduled.OrderID) : string.Empty;
                                tooltip = string.Format("会员[{0}]已预订! \n", scheduled.MemberName) + tooltip;
                                break;
                            case (int)EnumOrderType.PK:
                                cssClass.Add("pk");
                                displayString = "PK！";
                                //clickEvent = scheduled.IsMyOrder ? string.Format("CancelOrder('{0}','{1}')", scheduled.ScheduledID, scheduled.OrderID) : string.Empty;
                                tooltip = string.Format("会员[{0}]发起约战! \n", scheduled.MemberName) + (scheduled.TeamID.HasValue ? string.Format("应战球队：{0}\n", scheduled.TeamName) : string.Empty)
                                    + (scheduled.MemberBID.HasValue ? string.Format("会员[{0}]应战!\n", scheduled.MemberName) + (scheduled.TeamBID.HasValue ? string.Format("应战球队：{0}\n", scheduled.TeamBName) : string.Empty) : string.Empty)
                                    + tooltip;
                                break;
                            case (int)EnumOrderType.FreeTeam:
                                cssClass.Add("solo");
                                displayString = "单飞！";
                                //clickEvent = !scheduled.IsMyOrder ? string.Format("ProcessOrder('{0}','{1}')", scheduled.ScheduledID, scheduled.OrderID) : string.Empty;
                                tooltip = string.Format("单飞:人数限制[{0}]!当前人数[{1}] \n", scheduled.SoloMinPlayer.GetValueOrDefault(0), scheduled.SoloPalyerCount) + tooltip;
                                break;
                            default:
                                break;
                        }
                        break;
                    case (int)EnumFieldStatus.Canceled:
                        cssClass.Add("canceled");
                        displayString = "取消！";
                        tooltip = string.Format("场次已被取消！ \n 原因：{0}", scheduled.ScheduledRemark);
                        break;
                    default:
                        cssClass.Add("disable");
                        displayString = string.Empty;
                        break;
                }

            }
            scheduled.HTMLAttributeString = string.Format(" class=\"{0}\" title=\"{1}\"", string.Join(" ", cssClass), tooltip);
            scheduled.HTMLAttributeString += clickEvent.Length > 0 ? string.Format(" onclick=\"{0}\"", clickEvent) : string.Empty;
            scheduled.HTMLDisplayString = displayString;
        }


        //private void GetScheduledList(int fieldItemID, Repeater repeater)
        //{
        //    //DateTime startTime = GetMondayDate();
        //    //var list = FieldManager.GetScheduledList(fieldItemID, startTime, startTime.AddDays(6));
        //    //var orderList = OrderManager.GetOrderList(fieldItemID, startTime, startTime.AddDays(6));
        //    var scheduleList = scheduledList.Where(s => s.FieldItemID == fieldItemID).ToList();
        //    var oList = orderList.Where(s => s.FieldItemID == fieldItemID);
        //    //list = list.Where(s => s.FIELD_ITEM_ID == fieldItemID).ToList();
        //    var fieldWeekInfoList = new List<FieldWeekInfoOld>();
        //    foreach (var schedule in scheduleList)
        //    {
        //        string time = schedule.Time;
        //        FieldWeekInfoOld weekInfo = null;
        //        if (!fieldWeekInfoList.Any(s => s.Time == time))
        //        {
        //            weekInfo = new FieldWeekInfoOld();
        //            weekInfo.Time = time;
        //            fieldWeekInfoList.Add(weekInfo);
        //        }
        //        else
        //        {
        //            weekInfo = fieldWeekInfoList.FirstOrDefault(s => s.Time == time);
        //        }

        //        FieldScheduledInfoOld scheduleInfo = new FieldScheduledInfoOld
        //        {
        //            Date = DateTime.Parse(schedule.Date),
        //            Price = schedule.Price,
        //            ScheduledID = schedule.ID,
        //            Time = weekInfo.Time
        //        };
        //        var order = orderList.FirstOrDefault(s => s.ScheduledID == schedule.ID);
        //        if (order != null)
        //        {
        //            scheduleInfo.Status = (EnumFieldStatus)order.OrderStatus;
        //            scheduleInfo.Type = (EnumOrderType)order.OrderType;
        //            scheduleInfo.MemberID = order.MemberID;
        //            scheduleInfo.IsNeedReferee = order.NeedReferee ? 1 : 0;
        //            scheduleInfo.OrderID = order.ID;
        //            if (order.TeamScore != null && order.TeamScore.TeamBID > 0)
        //            {
        //                scheduleInfo.IsAccept = true;
        //            }
        //        }
        //        weekInfo.ScheduledInfos.Add(scheduleInfo);
        //    }

        //    DateTime startTime = GetMondayDate();
        //    //循环每一天，如果不存在数据，则添加一条不可用的数据
        //    for (int i = 0; i < 7; i++)
        //    {
        //        var dt = startTime.AddDays(i);
        //        string strDt = dt.ToString("yyyy-MM-dd");
        //        foreach (var item in fieldWeekInfoList)
        //        {
        //            if (!item.ScheduledInfos.Exists(s => s.Date.ToString("yyyy-MM-dd") == strDt))
        //            {
        //                FieldScheduledInfoOld scheduleInfo = new FieldScheduledInfoOld
        //                {
        //                    Date = dt,
        //                    Status = EnumFieldStatus.Void,
        //                };
        //                item.ScheduledInfos.Add(scheduleInfo);
        //            }
        //        }
        //    }

        //    //fieldWeekInfoList = fieldWeekInfoList.OrderBy(s => s.Time).ToList();
        //    foreach (var item in fieldWeekInfoList)
        //    {
        //        item.ScheduledInfos = item.ScheduledInfos.OrderBy(s => s.Date).ToList();
        //    }
        //    repeater.DataSource = fieldWeekInfoList;
        //    repeater.DataBind();
        //}

        protected string GetAttrString(FieldScheduledInfoOld info)
        {
            string classHtml = " class =\"canbook\"";
            string onclick = string.Empty;
            if (info.Status == EnumFieldStatus.Void)
            {
                classHtml = " class =\"disable\"";
            }
            else
            {
                if (info.Status == EnumFieldStatus.Available)
                {
                    classHtml = " class =\"canbook\"";
                }
                else if (info.Status == EnumFieldStatus.Booking)
                {
                    if (info.Type == 0)
                    {
                        classHtml = " class =\"apply\"";
                    }
                    else if (info.Type == EnumOrderType.PK)
                    {
                        if (!info.IsAccept)
                        {
                            classHtml = " class =\"rv\"";
                        }
                        else
                        {
                            classHtml = " class =\"accept\"";
                        }
                    }
                    else if (info.Type == EnumOrderType.FreeTeam)
                    {
                        classHtml = " class =\"solo\"";
                    }
                }
                else if (info.Status == EnumFieldStatus.Booked)
                {
                    classHtml = " class =\"booked\"";
                }
                else
                {
                }
                onclick = string.Format(" onclick=\"ProcessOrder('{0}','{1}')\"", info.ScheduledID, info.OrderID);
            }
            return string.Concat(classHtml, onclick);
        }

        protected void chklStatusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchOrder();
            BindJavascript();
        }
        
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            SearchOrder();
            BindJavascript();
        }
    }

    public class FieldWeekInfoOld
    {
        public string Time { get; set; }
        public List<FieldScheduledInfoOld> ScheduledInfos { get; set; }

        public FieldWeekInfoOld()
        {
            ScheduledInfos = new List<FieldScheduledInfoOld>();
        }
    }
    public class FieldScheduledInfoOld
    {
        public int ScheduledID { get; set; }
        public string Time { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int MemberID { get; set; }
        public List<int> SoloMemberList { get; set; }
        public EnumFieldStatus Status { get; set; }
        public EnumOrderType Type { get; set; }
        public int OrderID { get; set; }
        public int IsNeedReferee { get; set; }
        //For PK
        public bool IsAccept { get; set; }
        public string Title { get; set; }
        public bool IsMyOrder { get; set; }
        public string StrDate
        {
            get
            {
                return Date.ToString("yyyy-MM-dd");
            }
        }
    }
}