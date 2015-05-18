using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiQiu.Biz;
using TiQiu.Common.Util;
using TiQiu.DAL;
using TiQiu.Web.WebPages.Utilities;

namespace TiQiu.Web.WebPages
{
    public partial class Findfield : PageBase
    {
        public int CurFieldItemID { get; set; }
        public int CurFieldID { get; set; }
        //public string VirPath = FileRoot;
        private int itemPage;
        private int itemTotal;
        private List<CustomScheduled> scheduledList = new List<CustomScheduled>();
        private List<CustomOrder> orderList = new List<CustomOrder>();
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindControlData();
            }
            ReceiveParam();
            InitForm();
            GetData();

            if (!IsPostBack)
            {
                SearchFieldData();
                            
            }

            ProcessEvent();
        }

        private void ReceiveParam()
        {
            if (Request.UrlReferrer != null && Request.UrlReferrer.ToString() != Request.Url.ToString())
            {
                string type = Request.QueryString["type"];
                string fieldName = Request.QueryString["fieldname"];
                string area = Request.QueryString["area"];
                string startdate = Request.QueryString["startdate"];
                string enddate = Request.QueryString["enddate"];
                if (!string.IsNullOrEmpty(type))
                {
                    chkFieldType.SelectedValue = type;
                }
                if (!string.IsNullOrEmpty(fieldName))
                {
                    txtFieldName.Value = fieldName;
                }
                if (!string.IsNullOrEmpty(area))
                {
                    ddlArea.SelectedValue = area;
                }

                DateTime dt = DateTime.Now;
                if (DateTime.TryParse(startdate, out dt))
                {
                    txtStartTime.Value = dt.ToString("yyyy-MM-dd HH:mm");
                    DateTime monday = GetMondayDate(dt);
                    hdDate.Value = monday.ToString("yyyy-MM-dd");
                }
                if (DateTime.TryParse(enddate, out dt))
                {
                    txtEndTime.Value = dt.ToString("yyyy-MM-dd HH:mm");
                }
            }
        }

        private void InitForm()
        {

            if (IsLogin)
            {                
                h3.InnerText = "预订类型选择";
                dOrderForm.Visible = true;
                dLoginForm.Visible = false;
                ucHistory.MemberID = CurAccount.MEMBER_ID;
                ucHistory.Visible = true;
                BindMyTeam();   
            }
            else
            {
                h3.InnerText = "登陆";
                dOrderForm.Visible = false;
                dLoginForm.Visible = true;
            }
        }

        private void ProcessEvent()
        {
            string eventTarget = Request.Form["__EVENTTARGET"];
            string arg = Request.Form["__EVENTARGUMENT"];
            if (eventTarget == "li")
            {
                var arrary = arg.Split('$');
                if (arrary.Length == 2)
                {
                    CurFieldItemID = int.Parse(arrary[0]);
                    CurFieldID = int.Parse(arrary[1]);
                    ProcessItem(GetField(CurFieldID));
                }
            }
            else if (eventTarget == "sendRv")
            {
                var array = arg.Split('$');
                if (array.Length == 4)
                {
                    CurFieldID = int.Parse(hdCurrentFiledID.Value);
                    CurFieldItemID = int.Parse(hdCurrentItemID.Value);
                    int scheduleID = int.Parse(array[0]);
                    int rvTeamID = 0;
                    //int.TryParse(array[1], out rvTeamID);
                    int myTeamID = int.Parse(ddlRvMyTeam.SelectedValue);
                    int needferee = 0;
                    int.TryParse(array[1], out needferee);
                    int pkType = 0;
                    int.TryParse(array[2], out pkType);
                    string colthColor = array[3];
                    try
                    {
                        OrderManager.OrderPK(scheduleID, needferee == 1, CurAccount.MEMBER_ID, myTeamID, colthColor, (EnumPKPayType)pkType, rvTeamID);
                    }
                    catch (ApplicationException ex)
                    {
                        Alert(ex.Message);
                    }
                    catch (Exception)
                    {
                        Alert("系统发生异常，请稍后再试！");
                    }
                    ProcessItem(GetField(CurFieldID));
                }
            }
            else if (eventTarget == "normalOrder")
            {
                var array = arg.Split('$');
                if (array.Length == 3)
                {
                    CurFieldID = int.Parse(hdCurrentFiledID.Value);
                    CurFieldItemID = int.Parse(hdCurrentItemID.Value);
                    int scheduleID = int.Parse(array[0]);
                    int myTeamID = int.Parse(ddlRvMyTeam.SelectedValue);
                    int needferee = 0;
                    int.TryParse(array[1], out needferee);
                    string colthColor = array[2];
                    try
                    {
                        OrderManager.OrderNormal(scheduleID, CurAccount.MEMBER_ID, needferee == 1, 0, myTeamID, 0, colthColor, string.Empty);
                    }
                    catch (ApplicationException ex)
                    {
                        Alert(ex.Message);
                    }
                    catch (Exception)
                    {
                        Alert("系统发生异常，请稍后再试！");
                    }
                    ProcessItem(GetField(CurFieldID));
                }
            }
            else if (eventTarget == "soloOrder")
            {
                var array = arg.Split('$');
                if (array.Length == 2)
                {
                    CurFieldID = int.Parse(hdCurrentFiledID.Value);
                    CurFieldItemID = int.Parse(hdCurrentItemID.Value);
                    int orderID = int.Parse(array[0]);
                    int numberOfPeople = int.Parse(array[1]);
                    try
                    {
                        OrderManager.JoinFreeTeam(orderID, CurAccount.MEMBER_ID, numberOfPeople);
                    }
                    catch (ApplicationException ex)
                    {
                        Alert(ex.Message);
                    }
                    catch (Exception)
                    {
                        Alert("系统发生异常，请稍后再试！");
                    }
                    ProcessItem(GetField(CurFieldID));
                }
            }
            else if (eventTarget == "acceptOrder")
            {
                var array = arg.Split('$');
                if (array.Length == 2)
                {
                    CurFieldID = int.Parse(hdCurrentFiledID.Value);
                    CurFieldItemID = int.Parse(hdCurrentItemID.Value);
                    int orderID = int.Parse(array[0]);
                    string clothColor = array[1];
                    int teamId = 0;
                    int.TryParse(ddlAcceptTeam.SelectedValue, out teamId);
                    try
                    {
                        OrderManager.AcceptPK(orderID, CurAccount.MEMBER_ID, teamId);
                    }
                    catch (ApplicationException ex)
                    {
                        Alert(ex.Message);
                    }
                    catch (Exception)
                    {
                        Alert("系统发生异常，请稍后再试！");
                    }
                    ProcessItem(GetField(CurFieldID));
                }
            }
            else if (eventTarget == "login")
            {
                var array = arg.Split('$');
                if (array.Length == 2)
                {
                    string username = array[0];
                    string pwd = array[1];
                    try
                    {
                        AccountManager.Login(username, pwd);
                        FormsAuthentication.SetAuthCookie(username, true);
                        GetCurrAccount();
                       // SessionUtil.SetSession(IsBusiness, "false");
                        Log.WriteBizLog(string.Format("普通用户{0}登录踢球去", username));
                        InitForm();
                        CurFieldID = int.Parse(hdCurrentFiledID.Value);
                        CurFieldItemID = int.Parse(hdCurrentItemID.Value);
                        Response.Redirect(Request.Url.ToString()); 
                        //ProcessItem(GetField(CurFieldID));
                    }
                    catch (ApplicationException ex)
                    {
                        Alert(ex.Message);
                    }
                    catch (Exception)
                    {
                        Alert("系统发生异常，请稍后再试！");
                    }
                }
                else
                {
                    Alert("数据不正常，请重试！");
                }
            }
        }

        private void Alert(string message)
        {
            ScriptManager.RegisterStartupScript(this.upContent, this.GetType(), "alert", string.Format("alert('{0}');", message), true);
        }

        private void GetData()
        {
            string currentPage = hdItemCurrentPage.Value.Trim();
            int.TryParse(currentPage, out itemPage);
            ltlPageIndex.Text = currentPage.ToString();

            string strItemTotal = hdItemTotal.Value.Trim();
            int.TryParse(strItemTotal, out itemTotal);

            
            if (IsLogin)
            {
                sOrderContactName.InnerText = sRvContactName.InnerText = sAcceptCgName.InnerText = CurMember.NAME;
                sContactPhone.InnerText = sRvContactPhone.InnerText = sAcceptCgPhone.InnerText = CurMember.CELLPHONE;
            }
        }

        private void BindMyTeam()
        {
            if (!IsLogin || ddlMyTeam.DataSource != null) return;
            var list = TeamManager.GetTeamByMemberId(CurAccount.MEMBER_ID);
            if (list.Count == 0)
            {
                list.Add(new TEAM
                {
                    NAME = "无",
                    ID = 0
                });
            }
            ddlMyTeam.DataSource = ddlRvMyTeam.DataSource = ddlAcceptTeam.DataSource = list;
            ddlMyTeam.DataTextField = ddlRvMyTeam.DataTextField = ddlAcceptTeam.DataTextField = "NAME";
            ddlMyTeam.DataValueField = ddlRvMyTeam.DataValueField = ddlAcceptTeam.DataValueField = "ID";
            ddlMyTeam.DataBind();
            ddlRvMyTeam.DataBind();
            ddlAcceptTeam.DataBind();
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            SearchFieldData();
        }

        private void SearchFieldData()
        {
            if (hdDate.Value.Trim() == string.Empty)
            {
                DateTime monday = GetMondayDate(DateTime.Now);
                hdDate.Value = monday.ToString("yyyy-MM-dd");
            }

            string filedName = txtFieldName.Value.Trim();
            string area = ddlArea.SelectedValue.Trim();
            string strStartTime = txtStartTime.Value.Trim();
            string strEndTime = txtEndTime.Value.Trim();
            string strStartPrice = txtStartPrice.Value.Trim();
            string strEndPrice = txtEndPrice.Value.Trim();
            DateTime? startTime = null;
            DateTime? endTime = null;
            if (!string.IsNullOrWhiteSpace(strStartTime))
            {
                startTime = DateTime.Parse(strStartTime);
            }
            if (!string.IsNullOrWhiteSpace(strEndTime))
            {
                endTime = DateTime.Parse(strEndTime);
            }
            decimal startPrice, endPrice;
            bool blStartPrice = decimal.TryParse(strStartPrice, out startPrice);
            bool blEndPrice = decimal.TryParse(strEndPrice, out endPrice);
            int totalCount = 0;

            List<EnumFieldType> selectedType = new List<EnumFieldType>();
            foreach (ListItem item in chkFieldType.Items)
            {
                int t = 0;
                if (item.Selected && int.TryParse(item.Value, out t))
                {
                    selectedType.Add((EnumFieldType)t);
                }
                
            }                   
            
            var list = FieldManager.GetFieldList(filedName, area, selectedType, startTime, endTime, startPrice, endPrice, "ID", false, itemPage,0,0, 5, out totalCount);
            hdItemTotal.Value = totalCount.ToString();
            rtList.DataSource = list;            
            rtList.DataBind();

            
            SetFieldInfo(list.FirstOrDefault());
        }

        private void BindControlData()
        {
           
            ddlArea.DataSource = ConstValue.GetAreaList(true); ;
            ddlArea.DataValueField = "CODE";
            ddlArea.DataTextField = "AREA_NAME";
            ddlArea.DataBind();
           
            chkFieldType.DataSource = ConstValue.GetFieldType();
            chkFieldType.DataTextField = "Key";
            chkFieldType.DataValueField = "Value";
            chkFieldType.DataBind();
        }

        protected void lbtnField_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;
            CurFieldID = int.Parse(lbtn.CommandArgument);

            SetFieldInfo(GetField(CurFieldID));
        }

        private void SetFieldInfo(FIELD curField)
        {
            if (curField == null)
            {
            }
            else
            {
                sellerImg.Src = FileRoot + curField.PIC_PATH;
                fieldName.InnerText = curField.NAME;
                lblAddress.InnerText = curField.ADRESS;
                lblArea.InnerText = curField.AREA_CODE;
                lblFieldName.Text = lblRvFieldName.Text = lblAcceptFieldName.Text = lblSoloFlightFieldName.Text = curField.NAME;
                hdCurrentFiledID.Value = curField.ID.ToString();
                lblTel.InnerText = curField.TEL;
                lblPhone.InnerText = curField.PHONE;
                lblArea.InnerText = GetAreaValue(curField.AREA_CODE);
                SetWeekTime(0);
                ProcessItem(curField);
            }
        }

        private string GetAreaValue(string areaCode)
        {
            var list = ConstValue.GetAreaList(false);
            foreach (var item in list)
            {
                if (item.CODE == areaCode)
                {
                    return item.AREA_NAME;
                }
            }

            return string.Empty;
        }

        private FIELD GetField(int id)
        {
            
            return FieldManager.GetField(id);
            
        }

        private void ProcessItem(FIELD curField)
        {
            //int totalCount = 0;
            //var list = FieldManager.GetFieldItemList(s => s.FIELD_ID == field_Id, "ID", true, 1, 99, out totalCount);
            if (CurFieldItemID == 0)
            {
                var first = curField.FIELD_ITEM.First();
                if (first != null)
                {
                    if (first.TYPE == (int)EnumFieldType.Eleven)
                    {
                        pIsNeedReferee.Visible = pIsNeedReferee2.Visible = true;
                    }
                    else
                    {
                        pIsNeedReferee.Visible = pIsNeedReferee2.Visible = false;
                    }
                    CurFieldItemID = first.ID;
                    CurFieldID = first.FIELD_ID;
                }
            }
            rtFieldItem.DataSource = curField.FIELD_ITEM.ToList();
            rtFieldItem.DataBind();

            GetScheduledList(CurFieldItemID);
            hdCurrentItemID.Value = CurFieldItemID.ToString();
            hdCurrentFiledID.Value = CurFieldID.ToString();
        }

        private void GetScheduledList(int fieldItemID){
            DateTime startTime = GetMondayDate();
            var list = OrderManager.GetFieldScheduledInfoList(fieldItemID, startTime, startTime.AddDays(6));
            List<FieldWeekInfo> fieldWeekInfoList = new List<FieldWeekInfo>();
            foreach (var item in list)
            {
                item.IsMyOrder = IsLogin
                    && item.OrderID.HasValue
                    && (item.MemberID.GetValueOrDefault(0) == CurMember.ID
                        || (item.SoloLogList != null && item.SoloLogList.Exists(s => s.MemberID == CurMember.ID)));
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
            rtScheduled.DataSource = fieldWeekInfoList;
            rtScheduled.DataBind();

            ResponseJson();
        }
        #region
        //private void GetScheduledList_old(int fieldItemID)
        //{
        //    DateTime startTime = GetMondayDate();
        //    var list = FieldManager.GetScheduledList(fieldItemID, startTime, startTime.AddDays(6)).OrderBy(s => s.START_TIME.Hours).ToList();
        //    var oList = OrderManager.GetOrderList(fieldItemID, startTime, startTime.AddDays(6));//.Where(s => s.STATUS == (int)EnumFieldStatus.Booking);
        //    var fieldWeekInfoList = new List<FieldWeekInfo>();
        //    foreach (var item in list)
        //    {
        //        var start = item.START_TIME;
        //        var end = item.END_TIME;
        //        string time = (start.Hours < 10 ? "0" + start.Hours.ToString() : start.Hours.ToString()) + ":" + (start.Minutes < 10 ? "0" + start.Minutes.ToString() : start.Minutes.ToString()) + "-" +
        //                       (end.Hours < 10 ? "0" + end.Hours.ToString() : end.Hours.ToString()) + ":" + (end.Minutes < 10 ? "0" + end.Minutes.ToString() : end.Minutes.ToString());

        //        FieldWeekInfo weekInfo = null;
        //        if (!fieldWeekInfoList.Any(s => s.Time == time))
        //        {
        //            weekInfo = new FieldWeekInfo();
        //            fieldWeekInfoList.Add(weekInfo);
        //        }
        //        else
        //        {
        //            weekInfo = fieldWeekInfoList.FirstOrDefault(s => s.Time == time);
        //        }
        //        weekInfo.Time = time;

        //        FieldScheduledInfo scheduleInfo = new FieldScheduledInfo
        //        {
        //            Date = item.SCHEDULED_DATE,
        //            Price = item.PRICE,
        //            ScheduledID = item.ID,
        //            Time = weekInfo.Time
        //        };
        //        var f = oList.FirstOrDefault(s => s.FIELD_SCHEDULED_ID == item.ID);
        //        if (f != null)
        //        {
        //            if(f.TYPE == (int)EnumOrderType.FreeTeam){
        //                //List<free_team_log> ftls = OrderManager.GetOrderList
        //            }
        //            scheduleInfo.Status = (EnumFieldStatus)f.STATUS;
        //            scheduleInfo.Type = (EnumOrderType)f.TYPE;
        //            scheduleInfo.MemberID = f.MEMBER_ID;
        //            scheduleInfo.IsNeedReferee = f.NEED_REFEREE ? 1 : 0;
        //            scheduleInfo.OrderID = f.ID;
        //            scheduleInfo.IsMyOrder = f.MEMBER_ID == CurMember.ID || f..Exists(m=>m == CurMember.ID;
        //            if (f.team_score != null && f.team_score.TEAM_B_ID.GetValueOrDefault(0) > 0)
        //            {
        //                scheduleInfo.IsAccept = true;
        //            }
        //        }
        //        weekInfo.ScheduledInfos.Add(scheduleInfo);

        //        var csScheduled = new CustomScheduled
        //        {
        //            ID = item.ID,
        //            Date = item.SCHEDULED_DATE.ToString("yyyy-MM-dd"),
        //            FieldID = item.FIELD_ID,
        //            FieldItemID = item.FIELD_ITEM_ID,
        //            Price = item.PRICE,
        //            Time = weekInfo.Time
        //        };

        //        scheduledList.Add(csScheduled);
        //    }
        //    //循环每一天，如果不存在数据，则添加一条不可用的数据
        //    for (int i = 0; i < 7; i++)
        //    {
        //        var dt = startTime.AddDays(i);
        //        string strDt = dt.ToString("yyyy-MM-dd");
        //        foreach (var item in fieldWeekInfoList)
        //        {
        //            if (!item.ScheduledInfos.Exists(s => s.Date.ToString("yyyy-MM-dd") == strDt))
        //            {
        //                FieldScheduledInfo scheduleInfo = new FieldScheduledInfo
        //                {
        //                    Date = dt,
        //                    Status = EnumFieldStatus.Void,
        //                };
        //                item.ScheduledInfos.Add(scheduleInfo);
        //            }
        //        }
        //    }

        //foreach (var o in oList)
        //{
        //    var csOrder = new CustomOrder
        //    {
        //        ID = o.ID,
        //        ScheduledID = o.FIELD_SCHEDULED_ID,
        //        MemberID = o.MEMBER_ID,
        //        NeedReferee = o.NEED_REFEREE,
        //        OrderType = (int)o.TYPE
        //    };
        //    if (o.PK_PAY_TYPE == 1)
        //    {
        //        csOrder.PaymentType = "AA制";
        //    }
        //    else if (o.PK_PAY_TYPE == 2)
        //    {
        //        csOrder.PaymentType = "约战方付费";
        //    }
        //    else if (o.PK_PAY_TYPE == 3)
        //    {
        //        csOrder.PaymentType = "输家付款";
        //    }
        //    if (o.team_score != null)
        //    {
        //        var ts = o.team_score;
        //        if (IsLogin)
        //        {
        //            csOrder.TeamScore = new CustomTeamScore
        //                {
        //                    OrderID = o.ID,
        //                    TeamAColor = ts.TEAM_A_COLOR,
        //                    TeamAID = ts.TEAM_A_ID,
        //                    TeamAName = ts.TEAM_A_NAME,
        //                    TeamAPhone = CurMember.CELLPHONE,
        //                    TeamAContactName = CurMember.NAME
        //                };
        //        }
        //    }
        //    orderList.Add(csOrder);
        //}

        ////fieldWeekInfoList = fieldWeekInfoList.OrderBy(s => s.Time).ToList();
        //foreach (var item in fieldWeekInfoList)
        //{
        //    item.ScheduledInfos = item.ScheduledInfos.OrderBy(s => s.Date).ToList();
        //}
        //rtScheduled.DataSource = fieldWeekInfoList;
        //rtScheduled.DataBind();

        //ResponseJson();
        //}
        #endregion
        private void ResponseJson()
        {
            JavaScriptSerializer jsonSerial = new JavaScriptSerializer();
            string scheduledJson = jsonSerial.Serialize(scheduledList);
            string orderJson = jsonSerial.Serialize(orderList);
            RegisterScript("scheduled", string.Format("var scheduledJson={0};", scheduledJson));
            RegisterScript("order", string.Format("var orderJson={0};", orderJson));
        }

        private void RegisterScript(string key, string funcationName)
        {
            ScriptManager.RegisterStartupScript(this.upContent, this.GetType(), key, funcationName, true);
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

        protected void rtScheduled_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var rt = e.Item.FindControl("rtInner") as Repeater;
            var weekInfo = e.Item.DataItem as FieldWeekInfo;
            rt.DataSource = weekInfo.ScheduledInfos;
            rt.DataBind();
        }

        protected void btnLeft_Click(object sender, EventArgs e)
        {
            DateTime nowMonday = GetMondayDate(DateTime.Now);
            DateTime monday = GetMondayDate();
            if (monday <= nowMonday)
            {
                return;
            }

            SetWeekTime(-7);
            int fieldItemID = int.Parse(hdCurrentItemID.Value);
            GetScheduledList(fieldItemID);
        }

        protected void btnRight_Click(object sender, EventArgs e)
        {
            SetWeekTime(7);
            int fieldItemID = int.Parse(hdCurrentItemID.Value);
            GetScheduledList(fieldItemID);
        }

        protected void btnItemPrePage_Click(object sender, EventArgs e)
        {
            if (itemPage == 1)
            {
                return;
            }

            itemPage--;
            hdItemCurrentPage.Value = ltlPageIndex.Text = itemPage.ToString();
            SearchFieldData();
        }

        protected void btnItemNextPage_Click(object sender, EventArgs e)
        {
            if (itemPage == Math.Ceiling((double)itemTotal / 5))
            {
                return;
            }
            itemPage++;
            hdItemCurrentPage.Value = ltlPageIndex.Text = itemPage.ToString();
            SearchFieldData();
        }

        protected string GetDisplayInfo(FieldScheduledInfo info)
        {
            string rel = string.Empty;
            try
            {
                if (info.OrderID.HasValue)
                {
                    var type = info.OrderType.GetValueOrDefault(-1);
                    if (type == (int)EnumOrderType.FreeTeam)
                    {
                        rel = "单飞";
                    }
                    else if (type == (int)EnumOrderType.PK)
                    {
                        rel = "PK";
                    }
                    else
                    {

                    }
                }
                else
                {
                    rel = info.Price.ToString("###");
                }
                
            }
            catch(Exception ex)
            {
                Log.WriteException(ex);
            }
            return rel;
        }

        protected void InitHTMLAttribute(FieldScheduledInfo scheduled)
        {
            List<string> cssClass = new List<string>();
            string displayString = string.Empty;
            string tooltip = string.Empty;
            string clickEvent = string.Empty;
            if (scheduled.ScheduledDate < DateTime.Now.Date || 
                (scheduled.ScheduledDate == DateTime.Now.Date && scheduled.StartTime.Hours <= DateTime.Now.Hour ))
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
                        switch (scheduled.OrderType)
                        {
                            case (int)EnumOrderType.Normal:
                                cssClass.Add("normal");
                                displayString = "已申请！";
                                clickEvent = scheduled.IsMyOrder ? string.Format("CancelOrder('{0}','{1}')", scheduled.ScheduledID, scheduled.OrderID) :
                                    string.Empty;
                                tooltip = string.Format("会员[{0}]发起预订申请! \n", scheduled.MemberName) + tooltip;
                                break;
                            case (int)EnumOrderType.PK:
                                cssClass.Add("pk");
                                displayString = "PK！";
                                clickEvent = scheduled.IsMyOrder ? string.Format("CancelOrder('{0}','{1}')", scheduled.ScheduledID, scheduled.OrderID) :
                                    string.Format("ProcessOrder('{0}','{1}')", scheduled.ScheduledID, scheduled.OrderID);
                                tooltip = string.Format("会员[{0}]发起约战! \n", scheduled.MemberName) + (scheduled.TeamID.HasValue ? scheduled.TeamName + "\n" : string.Empty) + tooltip;
                                break;
                            case (int)EnumOrderType.FreeTeam:
                                cssClass.Add("solo");
                                displayString = "单飞！";
                                clickEvent = scheduled.IsMyOrder ? string.Format("CancelOrder('{0}','{1}')", scheduled.ScheduledID, scheduled.OrderID) :
                                    string.Format("ProcessOrder('{0}','{1}')", scheduled.ScheduledID, scheduled.OrderID);
                                tooltip = string.Format("单飞:人数限制[{0}]!当前人数[{1}] \n", scheduled.SoloMinPlayer.GetValueOrDefault(0), scheduled.SoloPalyerCount) + tooltip;
                                break;
                            default:
                                break;
                        }
                        break;
                    case (int)EnumFieldStatus.Booked:

                        cssClass.Add("booked");
                        if (scheduled.IsMyOrder) cssClass.Add("mine");
                        switch (scheduled.OrderType)
                        {
                            case (int)EnumOrderType.Normal:
                                cssClass.Add("normal");
                                displayString = "已预订";
                                clickEvent = scheduled.IsMyOrder ? string.Format("CancelOrder('{0}','{1}')", scheduled.ScheduledID, scheduled.OrderID) : string.Empty;
                                tooltip = string.Format("会员[{0}]已预订! \n", scheduled.MemberName) + tooltip;
                                break;
                            case (int)EnumOrderType.PK:
                                cssClass.Add("pk");
                                displayString = "PK！";
                                clickEvent = scheduled.IsMyOrder ? string.Format("CancelOrder('{0}','{1}')", scheduled.ScheduledID, scheduled.OrderID) : string.Empty;
                                tooltip = string.Format("会员[{0}]发起约战! \n", scheduled.MemberName) + (scheduled.TeamID.HasValue ? string.Format("应战球队：{0}\n", scheduled.TeamName) : string.Empty)
                                    + (scheduled.MemberBID.HasValue ? string.Format("会员[{0}]应战!\n", scheduled.MemberName) + (scheduled.TeamBID.HasValue ? string.Format("应战球队：{0}\n", scheduled.TeamBName) : string.Empty) : string.Empty)
                                    + tooltip;
                                break;
                            case (int)EnumOrderType.FreeTeam:
                                cssClass.Add("solo");
                                displayString = "单飞！";
                                clickEvent = !scheduled.IsMyOrder ? string.Format("ProcessOrder('{0}','{1}')", scheduled.ScheduledID, scheduled.OrderID) : string.Empty;
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

        protected string GetAttrStringForFieldCell(FieldScheduledInfo info)
        {
            string classHtml = " class =\"off\"";
            string onclick = string.Empty;
            string title = string.Empty;
            var now = DateTime.Now;
            var today = new DateTime(now.Year, now.Month, now.Day);
            if (info.ScheduledDate < today)
            {
                classHtml = " class =\"disable\"";
            }
            else
            {
                switch (info.Status)
                {
                    case (int)EnumFieldStatus.Void:
                        classHtml = " class =\"disable\"";
                        break;
                    case (int)EnumFieldStatus.Available:
                       
                        classHtml = " class =\"on\"";
                        onclick = string.Format(" onclick=\"ProcessOrder('{0}','{1}')\"", info.ScheduledID, "");
                        title = " title=\"可预订！\"";
                        break;
                    case (int)EnumFieldStatus.Booking:
                        if (info.OrderType.Value == (int)EnumOrderType.FreeTeam)
                        {
                            
                            classHtml = " class =\"solo\"";
                            onclick = string.Format(" onclick=\"ProcessOrder('{0}','{1}')\"", info.ScheduledID, info.OrderID);
                            title = string.Format(" title=\"单飞场，当前人数{0}({1}-{2}),可加入！\"",0,0,info.SoloMinPlayer);
                           
                        }
                        else if (info.OrderType.Value == (int)EnumOrderType.PK)
                        {
                            if (!info.TeamBID.HasValue)
                            {
                                classHtml = " class =\"rv\"";
                                onclick = string.Format(" onclick=\"ProcessOrder('{0}','{1}')\"", info.ScheduledID, info.OrderID);
                                title = string.Format(" title=\"PK，约战方[{0}],Come on！\"", info.TeamName);
                            }
                        }
                        else
                        {
                            classHtml = " class =\"normal\"";
                            title = string.Format(" title=\"会员{0}发起预订申请！\"",info.MemberName);
                        }
                       
                        break;
                    default :
                        classHtml = " class =\"confirm\"";

                        if (info.OrderType.Value == (int)EnumOrderType.FreeTeam)
                        {
                            onclick = string.Format(" onclick=\"ProcessOrder('{0}','{1}')\"", info.ScheduledID, info.OrderID);
                            title = " title=\"单飞场，当前人数{0}({1}-{2})\"";

                        }
                        else if (info.OrderType.Value == (int)EnumOrderType.PK)
                        {
                            if (info.TeamBID.HasValue)
                            {
                                onclick = string.Format(" onclick=\"CancelOrder('{0}','{1}')\"", info.ScheduledID, info.OrderID);
                                title = string.Format(" title=\"PK，约战方[{0}] VS [{1}]！\"",info.TeamName,info.TeamBName);
                            }
                        }
                        else
                        {
                            title = string.Format(" title=\"预订成功，预订人{0}\"", info.MemberName);
                        }
                       
                        break;
                }
            }
            return string.Concat(classHtml, onclick, title);
            
        }

        //protected string GetAttrString(FieldScheduledInfo info)
        //{
        //    string classHtml = " class =\"off\"";
        //    string onclick = string.Empty;
        //    var now = DateTime.Now;
        //    var today = new DateTime(now.Year, now.Month, now.Day);
        //    if (info.Status == EnumFieldStatus.Void)
        //    {
        //        classHtml = " class =\"disable\"";
        //    }
        //    else if (info.Status == EnumFieldStatus.Available)
        //    {
        //        if (info.Date >= today)
        //        {
        //            classHtml = " class =\"on\"";
        //            onclick = string.Format(" onclick=\"ProcessOrder('{0}','{1}')\"", info.ScheduledID, info.OrderID);
        //        }
        //        else
        //        {
        //            classHtml = " class =\"disable\"";
        //        }
        //    }
        //    else if (info.Status == EnumFieldStatus.Booking)
        //    {
        //        if (info.Date >= today)
        //        {
        //            if (info.Type == EnumOrderType.PK)
        //            {
        //                if (!info.IsAccept)
        //                {
        //                    classHtml = " class =\"rv\"";
        //                    onclick = string.Format(" onclick=\"ProcessOrder('{0}','{1}')\"", info.ScheduledID, info.OrderID);
        //                }
        //            }
        //            else if (info.Type == EnumOrderType.FreeTeam)
        //            {
        //                classHtml = " class =\"solo\"";
        //                onclick = string.Format(" onclick=\"ProcessOrder('{0}','{1}')\"", info.ScheduledID, info.OrderID);
        //            }
        //        }
        //        else
        //        {
        //            classHtml = " class =\"disable\"";
        //        }
        //    }
        //    return string.Concat(classHtml, onclick);
        //}
    }

    #region 自定义类，为了页面控制和显示

    public class FieldWeekInfo
    {
        public string EventTimeRange { get; set; }
        public List<FieldScheduledInfo> ScheduledInfos { get; set; }

        public FieldWeekInfo()
        {
            ScheduledInfos = new List<FieldScheduledInfo>();
        }
    }

    //public class FieldScheduledInfo
    //{
    //    public int ScheduledID { get; set; }
    //    public string Time { get; set; }
    //    public DateTime Date { get; set; }
    //    public decimal Price { get; set; }
    //    public int MemberID { get; set; }
    //    public List<int> SoloMemberList { get; set; }
    //    public EnumFieldStatus Status { get; set; }
    //    public EnumOrderType Type { get; set; }
    //    public int OrderID { get; set; }
    //    public int IsNeedReferee { get; set; }
    //    //For PK
    //    public bool IsAccept { get; set; }
    //    public string Title { get; set; }
    //    public bool IsMyOrder { get;set;}
    //    public string StrDate
    //    {
    //        get
    //        {
    //            return Date.ToString("yyyy-MM-dd");
    //        }
    //    }
    //}

    #endregion
}