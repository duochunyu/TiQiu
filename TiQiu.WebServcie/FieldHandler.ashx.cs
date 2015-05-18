using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using TiQiu.Biz;
using TiQiu.Model;
using System.Configuration;
using Newtonsoft.Json;
namespace TiQiu.WebServcie
{
    /// <summary>
    /// FieldHandler 的摘要说明
    /// </summary>
    public class FieldHandler : IHttpHandler, IRequiresSessionState 
    {
        private string FILE_DOMAIN = ConfigurationManager.AppSettings["FILE_DOMAIN"]; //@"http://file.tiqiu365.com/";

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            HttpProcessor apj = new HttpProcessor();
            apj.ActionHandlerMaps.Add("GetFieldList", GetFieldList);
            apj.ActionHandlerMaps.Add("GetFieldInfo", GetFieldInfo);
            apj.ActionHandlerMaps.Add("GetScheduledList", GetScheduledList);
            apj.ActionHandlerMaps.Add("GetFieldScheduledList", GetFieldScheduledList);
            apj.ActionHandlerMaps.Add("GetFieldItemScheduledList", GetFieldItemScheduledList);
            apj.ActionHandlerMaps.Add("GetFieldItemList", GetFieldItemList);
            apj.ActionHandlerMaps.Add("GetFieldRuleList", GetFieldRuleList);
            apj.ActionHandlerMaps.Add("CreateFieldRuleList", CreateFieldRuleList);
            apj.ActionHandlerMaps.Add("BuildFieldScheduled", BuildFieldScheduled);
            

            apj.ActionWithManager.Add("GetFieldItemList");
            apj.ActionWithManager.Add("BuildFieldScheduled");
            apj.ActionWithManager.Add("GetFieldItemScheduledList");
            apj.ActionWithManager.Add("GetFieldRuleList");
            apj.ActionWithManager.Add("CreateFieldRuleList");
            
            apj.ProcessRequestHandler<ApplicationException>(context, ex => Log.WriteException(ex));
        }

        private void GetFieldList(HttpContext context, ref object userData)
        {
            var pageIdx = Util.GetParamter<int>(context.Request, "pageindex");
            var pageSize = Util.GetParamter<int>(context.Request, "pagesize");

            var name = Util.GetParamter<string>(context.Request,"name",false);
            var areaCode = Util.GetParamter<string>(context.Request, "areacode", false);
            var fieldType = Util.GetParamter<int>(context.Request,"fieldtype",false);
            var start = Util.GetParamter<DateTime>(context.Request,"start",false);
            var end = Util.GetParamter<DateTime>(context.Request, "end", false);
            var l = Util.GetParamter<double>(context.Request, "l", false);
            var b = Util.GetParamter<double>(context.Request, "b", false);
           
            List<EnumFieldType> selectedType = new List<EnumFieldType>();
            int ft;
            if (fieldType > 0) selectedType.Add((EnumFieldType)fieldType);
              
            int totalCount = 0; 
            var list =  FieldManager.GetFieldList(name, areaCode,selectedType,start,end,0,0, "ID", false, pageIdx, pageSize,l,b, out totalCount);
            userData = new PageInfo<Field>() { ItemList = ModelMapping.InitFields(list), Total = totalCount };         

        }

        private void GetFieldInfo(HttpContext context, ref object userData)
        {
            var id = Util.GetParamter<int>(context.Request, "id");
            userData = ModelMapping.InitField(FieldManager.GetField(id));
        }

        //private void GetScheduledList(HttpContext context, ref object userData)
        //{
        //    var id = int.Parse(Util.GetParamter<int>(context.Request,"id"]);
        //    var start = DateTime.Parse(Util.GetParamter<int>(context.Request,"start"]);
        //    var end = DateTime.Parse(Util.GetParamter<int>(context.Request,"end"]);      
            
        //    var list = FieldManager.GetScheduledList(id, start, end);
        //    userData = InitScheduled(list);

        //}

        private void GetScheduledList(HttpContext context, ref object userData)
        {
            var id = Util.GetParamter<int>(context.Request,"id");
            var start = Util.GetParamter<DateTime>(context.Request,"start");
            var end = Util.GetParamter<DateTime>(context.Request,"end");

            userData = OrderManager.GetFieldScheduledInfoList(id, start, end);            

        }

        private void GetFieldScheduledList(HttpContext context, ref object userData)
        {
            var fieldId = Util.GetParamter<int>(context.Request, "fieldId");
            var start = Util.GetParamter<DateTime>(context.Request, "start");
            var end = Util.GetParamter<DateTime>(context.Request, "end");

            //userData = OrderManager.GetFieldScheduledInfoList(id, start, end);

        }

        private void BuildFieldScheduled(HttpContext context, ref object userData)
        {
            var fieldId = Util.GetParamter<int>(context.Request, "fieldId");
            //var start = Util.GetParamter<DateTime>(context.Request, "start");
            //var end = Util.GetParamter<DateTime>(context.Request, "end");

            FieldManager.CreateFieldScheduled(fieldId, DateTime.Now, DateTime.Now.AddDays(14));

        }

        

        private void GetFieldItemList(HttpContext context, ref object userData)
        {
            var id = Util.GetParamter<int>(context.Request, "AccountBID");
            
            
            userData = ModelMapping.InitFields(FieldManager.GetFieldList(id));

        }

        private void GetFieldItemScheduledList(HttpContext context, ref object userData)
        {
            var id = Util.GetParamter<int>(context.Request, "id");
            var accid = Util.GetParamter<int>(context.Request, "AccountBID");
            var start = Util.GetParamter<DateTime>(context.Request, "start");
            var end = Util.GetParamter<DateTime>(context.Request, "end");

            userData = OrderManager.GetFieldScheduledInfoList(id, start, end,accid);  

        }

        private void GetFieldRuleList(HttpContext context, ref object userData)
        {
            var id = Util.GetParamter<int>(context.Request, "fieldItemId");

            userData = ModelMapping.InitFieldWeekRule(FieldManager.GetFieldRule(id));

        }

        private void CreateFieldRuleList(HttpContext context, ref object userData)
        {
            var id = Util.GetParamter<int>(context.Request, "fieldItemId");

            var list = Util.GetParamter<string>(context.Request, "ruleItems");
            List<FieldWeekRule> item = JsonConvert.DeserializeObject<List<FieldWeekRule>>(list);
            List<DAL.FIELD_RULE> rules = new List<DAL.FIELD_RULE>();
            var curNow = DateTime.Now.Date;
            item.ForEach(i =>
            {
                rules.Add(new DAL.FIELD_RULE()
                {
                    DESCRIPTION = i.Description,
                    PRICE = i.Price,
                    FIELD_ITEM_ID = id,
                    FIELD_ID = i.FieldId,
                    FIELD_TYPE = i.FieldType,
                    SCHEDULE_TYPE = i.DayOfWeek,
                    START_TIME = i.Start,
                    END_TIME = i.End,
                    STATUS = i.Status,
                    TYPE = i.Type,
                    START_DATE = curNow,
                    END_DATE = DateTime.Now.AddYears(5).Date,
                    RULE_NAME = string.Empty

                });
            });

            FieldManager.CreateFieldRule(id,rules);

        }


        

        private List<Scheduled> InitScheduled(List<DAL.FIELD_SCHEDULED> scheduleds)
        {
            var list = new List<Scheduled>();
            foreach (var item in scheduleds)
            {
                list.Add(new Scheduled()
                {
                    ID = item.ID,
                    CurDate = item.SCHEDULED_DATE.Date,
                    StartTime = item.START_TIME,
                    EndTime = item.END_TIME,
                    Status = item.STATUS,
                    StatusText = ConstValue.GetStatusText(item.STATUS),
                    Price = item.PRICE,
                    FieldID = item.FIELD_ID,
                    FieldItemID = item.FIELD_ITEM_ID,
                    Remark = item.REMARK,
                    RuleType = item.RULE_TYPE
                });
            }
            return list;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}