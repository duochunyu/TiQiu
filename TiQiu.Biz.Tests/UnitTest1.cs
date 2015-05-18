using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TiQiu.DAL;
using System.Linq;
using System.Linq.Expressions;
using TiQiu.Biz;
using System.Data;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TiQiu.PushJob;
using System.Configuration;
using Newtonsoft.Json;
using TiQiu.SMS;


namespace TiQiu.Biz.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AllTest()
        {
            //business b = BusinessesManager.CreateBusinesses("TestBusinesses", "test", 0);
            //field f = FieldManager.CreateField("TestBusinesses_Field1", "test", "test", "001", 0, 400, 200, new TimeSpan(9, 0, 0), new TimeSpan(24, 0, 0), 0, 0, 32.00f, 120.00f, true, b.ID);
            //FIELD_ITEM fi = FieldManager.CreateField_Item("test1", "test1", 0, (int)EnumFieldType.Five, f.ID);
            //FIELD_ITEM fi2 = FieldManager.CreateField_Item("test2", "test2", 0, (int)EnumFieldType.Eleven, f.ID);
            //field_rule fr = FieldManager.CreateFieldRule(f.ID, fi2.ID, EnumRuleType.Event, EnumScheduleType.Monday, DateTime.Now, DateTime.Now.AddDays(30), new TimeSpan(9, 0, 0), new TimeSpan(10, 30, 0), 200, "1", "");
            //field_rule fr1 = FieldManager.CreateFieldRule(f.ID, fi2.ID, EnumRuleType.Event, EnumScheduleType.Daily, DateTime.Now, DateTime.Now.AddDays(30), new TimeSpan(10, 30, 0), new TimeSpan(12, 0, 0), 200, "1", "");
            //field_rule fr2 = FieldManager.CreateFieldRule(f.ID, fi2.ID, EnumRuleType.Event, EnumScheduleType.Daily, DateTime.Now, DateTime.Now.AddDays(30), new TimeSpan(13, 30, 0), new TimeSpan(15, 0, 0), 200, "1", "");
            //field_rule fr3 = FieldManager.CreateFieldRule(f.ID, fi2.ID, EnumRuleType.Event, EnumScheduleType.Daily, DateTime.Now, DateTime.Now.AddDays(30), new TimeSpan(15, 0, 0), new TimeSpan(16, 30, 0), 200, "1", "");
            //field_rule fr4 = FieldManager.CreateFieldRule(f.ID, fi2.ID, EnumRuleType.Event, EnumScheduleType.Weekly, DateTime.Now.AddDays(3), DateTime.Now.AddDays(30), new TimeSpan(15, 0, 0), new TimeSpan(16, 30, 0), 300, "1", "");
            //field_rule fr5 = FieldManager.CreateFieldRule(f.ID, fi2.ID, EnumRuleType.Void, EnumScheduleType.Weekly, DateTime.Now.AddDays(4), DateTime.Now.AddDays(30), new TimeSpan(18, 0, 0), new TimeSpan(19, 30, 0), 300, "长期租用", "不可使用");

            //FieldManager.CreateFieldScheduled(32, DateTime.Now, DateTime.Now.AddDays(20));
            //using (TIQIUEntities context = new TIQIUEntities())
            //{

            //    List<FIELD> list = context.fields.Where(f => f.FIELD_ITEM.Select(i => i.TYPE == 5)).ToList(); //context.FIELD_ITEM.Where(i=>i.TYPE ==11).Select(i=>i.FIELD_ID).Contains(f.ID) ).ToList();
            //    string sql = "SELECT VALUE o FROM fields AS o WHERE o.FIELD_ITEM.TYPE = 0";
            //    ObjectQuery<FIELD> query = context.CreateQuery<FIELD>(sql);

            //    ObjectResult<FIELD> result = query.Execute(MergeOption.NoTracking);





            //    foreach (field o in list)
            //    {

            //        Console.WriteLine("{0},{1}", o.ID, o.FIELD_ITEM.Count);

            //    }



            //    Console.WriteLine(query.ToTraceString());
            //}
            //int totalCount;
            //List<FIELD> list= FieldManager.GetFieldList("星空", "", new List<EnumFieldType>(), null, null, 0, 0, "NAME", true, 1, 5, out totalCount);
            //Console.WriteLine("{0}", totalCount);
            //foreach (FIELD o in list)
            //{

            //    Console.WriteLine("{0},{1},{2}", o.ID, o.FIELD_ITEM.Count,totalCount);

            //}



           // Console.WriteLine(query.ToTraceString());

        }
        [TestMethod]
        public void OrderTest()
        {
            OrderManager.OrderNormal(8958, 2, false);
        }

        [TestMethod]
        public void GetFieldScheduledInfo()
        {
            var list = OrderManager.GetFieldScheduledInfoList(82, DateTime.Now, DateTime.Now.AddDays(10));
            Console.Write(list.Count);
        }

        [TestMethod]
        public void MySqlParamterTest()
        {
            //TIQIUEntities context = new TIQIUEntities();
            //MySqlParameter[] paras = new MySqlParameter[2];

            //MySqlParameter idParameter = new MySqlParameter("@Name", MySqlDbType.VarChar);
            //idParameter.Value = string.Format("%{0}%","星空");
            //paras[0] = idParameter;
            //MySqlParameter f = new MySqlParameter("@fid", MySqlDbType.Int16);
            //f.Value = 1;
            //paras[1] = f;

            //var sql = "select * from tiqiu.field where name like @Name ";
            //var result = context.Database.SqlQuery<FIELD>(sql, paras).ToList();
            //foreach (FIELD o in result)
            //{

            //    Console.WriteLine("{0},{1},{2}", o.ID, o.NAME, o.BRIEF);

            //}

        }

        [TestMethod]
        public void InitScheduled()
        {
            try
            {

                //CreateScheduledByFieldID(18);
                //CreateScheduledByFieldID(9);
                //CreateScheduledByFieldID(10);
                //11,12,20,14,15
                FieldManager.CreateFieldScheduled(53, DateTime.Now, DateTime.Now.AddDays(9));
                //FieldManager.CreateFieldScheduled(12, DateTime.Now, DateTime.Now.AddDays(30));
                //FieldManager.CreateFieldScheduled(20, DateTime.Now, DateTime.Now.AddDays(30));
                //FieldManager.CreateFieldScheduled(14, DateTime.Now, DateTime.Now.AddDays(30));
                //FieldManager.CreateFieldScheduled(15, DateTime.Now, DateTime.Now.AddDays(30));
                
                //FieldManager.CreateFieldScheduled(9, DateTime.Now, DateTime.Now.AddDays(30));
                //FieldManager.CreateFieldScheduled(10, DateTime.Now, DateTime.Now.AddDays(30));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        [TestMethod]
        public void InitScheduledWithSpan1()
        {
            try
            {/*
                int fieldId = 13;
                五人制上午100 下午200  晚上300
七人制上午400 下午500 晚上600
 
场次时段(周一至周天,固定场次)
上午09:00-10:30,10:30-12:00,
下午12:30-14:00,14:00-15:30,15:30-17:00,17:00-18:30,
晚上19:00-20:30,20:30-22:00,22:00-23:30.


                CreateScheduledByFieldID(fieldId, new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0), 100,EnumFieldType.Five, null);
                CreateScheduledByFieldID(fieldId, new TimeSpan(12, 30, 0), new TimeSpan(18, 30, 0), 200,EnumFieldType.Five, null);
                CreateScheduledByFieldID(fieldId, new TimeSpan(19, 00, 0), new TimeSpan(23, 30, 0), 300, EnumFieldType.Five, null);
                CreateScheduledByFieldID(fieldId, new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0), 400, EnumFieldType.Seven, null);
                CreateScheduledByFieldID(fieldId, new TimeSpan(12, 30, 0), new TimeSpan(18, 30, 0), 500, EnumFieldType.Seven, null);
                CreateScheduledByFieldID(fieldId, new TimeSpan(19, 00, 0), new TimeSpan(23, 30, 0), 600, EnumFieldType.Seven, null);
                FieldManager.CreateFieldScheduled(fieldId, DateTime.Now, DateTime.Now.AddDays(30));
                int fieldId = 23;
                CreateScheduledByFieldID(fieldId,1,3, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 300, null, null);
                CreateScheduledByFieldID(fieldId, 5, 7, new TimeSpan(9, 0, 0), new TimeSpan(22, 0, 0), 300, null, null);
                CreateScheduledByFieldID(fieldId, 1, 1, new TimeSpan(18, 0, 0), new TimeSpan(22, 0, 0), 300, null, null);
                CreateScheduledByFieldID(fieldId, 2, 3, new TimeSpan(18, 0, 0), new TimeSpan(20, 0, 0), 300, null, new TimeSpan(2,0,0));
                CreateScheduledByFieldID(fieldId, 2, 3, new TimeSpan(20, 0, 0), new TimeSpan(23, 0, 0), 300, null, null);
                CreateScheduledByFieldID(fieldId, 4, 4, new TimeSpan(10, 0, 0), new TimeSpan(22, 0, 0), 300, null, null);
                CreateScheduledByFieldID(fieldId, 5, 7, new TimeSpan(22, 30, 0), new TimeSpan(24, 0, 0), 300, null, null);
                FieldManager.CreateFieldScheduled(fieldId, DateTime.Now, DateTime.Now.AddDays(30));*/

                //fieldId = 8;
                //CreateScheduledByFieldID(fieldId, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 300, null, null);
                //CreateScheduledByFieldID(fieldId, 2, 7, new TimeSpan(22, 30, 0), new TimeSpan(24, 0, 0), 300, null, null);
                //CreateScheduledByFieldID(fieldId, 1, 1, new TimeSpan(18, 0, 0), new TimeSpan(20, 0, 0), 300, null, new TimeSpan(2, 0, 0));
                //CreateScheduledByFieldID(fieldId, 3, 3, new TimeSpan(18, 0, 0), new TimeSpan(20, 0, 0), 300, null, new TimeSpan(2, 0, 0));
                //CreateScheduledByFieldID(fieldId, 1, 1, new TimeSpan(20, 0, 0), new TimeSpan(21, 30, 0), 300, null, null);
                //CreateScheduledByFieldID(fieldId, 3, 3, new TimeSpan(20, 0, 0), new TimeSpan(21, 30, 0), 300, null, null);
                //CreateScheduledByFieldID(fieldId, 2, 2, new TimeSpan(18, 0, 0), new TimeSpan(22, 00, 0), 300, null, null);
                //CreateScheduledByFieldID(fieldId, 4, 7, new TimeSpan(18, 0, 0), new TimeSpan(22, 00, 0), 300, null, null);            


                //int fieldId = 9;
                //CreateScheduledByFieldID(fieldId,3,3, new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0), 300, null, null);
                //CreateScheduledByFieldID(fieldId, 3, 3, new TimeSpan(13, 0, 0), new TimeSpan(17, 30, 0), 300, null, null);
                //CreateScheduledByFieldID(fieldId, 3, 3, new TimeSpan(17, 30, 0), new TimeSpan(20, 0, 0), 300, null, new TimeSpan(2, 30, 0));
                //CreateScheduledByFieldID(fieldId, 3, 3, new TimeSpan(20, 0, 0), new TimeSpan(23, 0, 0), 300, null, null);

                //int fieldId = 12;
                //CreateScheduledByFieldID(fieldId, 1, 5, new TimeSpan(9, 0, 0), new TimeSpan(11, 0, 0), 80, null, new TimeSpan(2,0,0));
                //CreateScheduledByFieldID(fieldId, 1, 5, new TimeSpan(13, 0, 0), new TimeSpan(17, 0, 0), 200, null, new TimeSpan(2, 0, 0));
                //CreateScheduledByFieldID(fieldId, 1, 5, new TimeSpan(17, 0, 0), new TimeSpan(18, 30, 0), 320, null, null);
                //CreateScheduledByFieldID(fieldId, 1, 5, new TimeSpan(18, 30, 0), new TimeSpan(21, 30, 0), 360, null, null);
                //CreateScheduledByFieldID(fieldId, 1, 5, new TimeSpan(21, 30, 0), new TimeSpan(23, 0, 0), 320, null, null);
                //CreateScheduledByFieldID(fieldId, 6, 6, new TimeSpan(9, 0, 0), new TimeSpan(11, 0, 0), 150, null, new TimeSpan(2,0,0));
                //CreateScheduledByFieldID(fieldId, 7, 7, new TimeSpan(9, 0, 0), new TimeSpan(11, 0, 0), 120, null, new TimeSpan(2, 0, 0));
                //CreateScheduledByFieldID(fieldId, 6, 7, new TimeSpan(13, 0, 0), new TimeSpan(17, 0, 0), 180, null, new TimeSpan(2,0,0));
                //CreateScheduledByFieldID(fieldId, 6, 7, new TimeSpan(17, 0, 0), new TimeSpan(21, 30, 0), 320, null, null);
                //CreateScheduledByFieldID(fieldId, 6, 7, new TimeSpan(21, 30, 0), new TimeSpan(23, 0, 0), 300, null, null);
               
                //FieldManager.CreateFieldScheduled(fieldId, new DateTime(2014, 4, 22, 0, 0, 0), new DateTime(2014, 5, 22, 0, 0, 0));
                //fieldId = 11;
                //CreateScheduledByFieldID(fieldId, 1, 7, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), 260, null, new TimeSpan(2, 0, 0));
                //CreateScheduledByFieldID(fieldId, 1, 7, new TimeSpan(18, 30, 0), new TimeSpan(21, 30, 0), 350, null, null);
                //CreateScheduledByFieldID(fieldId, 1, 7, new TimeSpan(21, 30, 0), new TimeSpan(23, 0, 0), 300, null, null);

                //FieldManager.CreateFieldScheduled(fieldId, new DateTime(2014, 4, 22, 0, 0, 0), new DateTime(2014, 5, 22, 0, 0, 0));

                //int fieldId = 14;
                //CreateScheduledByFieldID(fieldId, 1, 5, new TimeSpan(10, 30, 0), new TimeSpan(18, 0, 0), 260, null, null);
                //CreateScheduledByFieldID(fieldId, 1, 7, new TimeSpan(18, 0, 0), new TimeSpan(22, 30, 0), 380, null, null);
                //CreateScheduledByFieldID(fieldId, 6, 7, new TimeSpan(10, 30, 0), new TimeSpan(12, 0, 0), 260, null, null);
                //CreateScheduledByFieldID(fieldId, 6, 7, new TimeSpan(12, 0, 0), new TimeSpan(18, 0, 0), 300, null, null);              

                //FieldManager.CreateFieldScheduled(fieldId, DateTime.Now.Date, DateTime.Now.AddMonths(1).Date);

                //fieldId = 15;
                //CreateScheduledByFieldID(fieldId, 1, 5, new TimeSpan(10, 30, 0), new TimeSpan(12, 0, 0), 180, null, null);
                //CreateScheduledByFieldID(fieldId, 1, 5, new TimeSpan(12, 0, 0), new TimeSpan(15, 0, 0), 280, null, null);
                //CreateScheduledByFieldID(fieldId, 1, 5, new TimeSpan(15, 0, 0), new TimeSpan(16, 30, 0), 260, null, null);
                //CreateScheduledByFieldID(fieldId, 1, 5, new TimeSpan(16, 30, 0), new TimeSpan(18, 0, 0), 280, null, null);
                //CreateScheduledByFieldID(fieldId, 1, 5, new TimeSpan(18, 0, 0), new TimeSpan(22, 30, 0), 400, null, null);

                //CreateScheduledByFieldID(fieldId, 6, 7, new TimeSpan(10, 30, 0), new TimeSpan(18, 0, 0), 320, null, null);
                //CreateScheduledByFieldID(fieldId, 6, 7, new TimeSpan(18, 0, 0), new TimeSpan(22, 30, 0), 400, null, null);
                List<FIELD> fields = new List<FIELD>();
                using (TIQIUEntities context = new TIQIUEntities())
                {
                    fields = context.FIELDs.Where(f => f.STATUS == 1).ToList();
                }
                fields.ForEach(f => FieldManager.CreateFieldScheduled(f.ID, DateTime.Now, DateTime.Now.AddDays(1)));
                

               
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void CreateScheduledByFieldID(int id)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {

                List<FIELD_ITEM> items = context.FIELD_ITEM.Where(i => i.FIELD_ID == id).ToList();
                items.ForEach(i =>
                {
                    for (int d = 1; d < 8; d++)
                    {
                        TimeSpan start = new TimeSpan(9, 0, 0);
                        TimeSpan sp = new TimeSpan(1, 30, 0);
                        TimeSpan end = start.Add(sp);
                        int c = 1;
                        while (end.TotalHours < 24)
                        {                            
                            FieldManager.CreateFieldRule(i.FIELD_ID, i.ID, EnumRuleType.Event, (EnumScheduleType)d, DateTime.Now, DateTime.Now.AddYears(1), start, end, (d>5 || start.TotalHours >= 18) ? 300:300, c.ToString(), "");
                            c++;
                            start = end;
                            end = start.Add(sp);
                            
                        }
                    }
                });
            }
        }

        private void CreateScheduledByFieldID(int id,TimeSpan start,TimeSpan end ,decimal price,EnumFieldType? fieldType, TimeSpan? span)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {

                List<FIELD_ITEM> items = context.FIELD_ITEM.Where(i => i.FIELD_ID == id && ((fieldType.HasValue && i.TYPE == (int)fieldType.Value) || !fieldType.HasValue)).ToList();
                List<DAL.FIELD_RULE> rules = new List<FIELD_RULE>();
                if (!span.HasValue ) span = TimeSpan.FromMinutes(90);
                items.ForEach(i =>
                {
                    for (int d = 1; d < 8; d++)
                    {
                        TimeSpan curStart = start;
                        TimeSpan curEnd = curStart.Add(span.Value);
                        while (curStart < end)
                        {
                            rules.Add(
                            new FIELD_RULE()
                            {
                                FIELD_ID = id,
                                FIELD_ITEM_ID = i.ID,
                                TYPE = (int)EnumRuleType.Event,
                                SCHEDULE_TYPE = (int)d,
                                START_DATE = DateTime.Now.Date,
                                END_DATE = DateTime.Now.AddYears(1),
                                START_TIME = curStart,
                                END_TIME = curEnd.TotalDays >= 1 ? curEnd.Add(new TimeSpan(-1, 0, 0, 0)) : curEnd,
                                PRICE = price,
                                RULE_NAME = "",
                                DESCRIPTION = start.ToString(@"h\:m") + "-" + curEnd.ToString(@"h\:m"),
                                STATUS = 1
                            });
                            
                            curStart = curEnd;
                            curEnd = curStart.Add(span.Value);

                        }
                    }
                });
                //FieldManager.CreateFieldRule(rules);
            }
        }

        private void CreateScheduledByFieldID(int id, int dayOfWeekStart,int dayOfWeekEnd,TimeSpan start, TimeSpan end, decimal price, EnumFieldType? fieldType, TimeSpan? span)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {

                List<FIELD_ITEM> items = context.FIELD_ITEM.Where(i => i.FIELD_ID == id && ((fieldType.HasValue && i.TYPE == (int)fieldType.Value) || !fieldType.HasValue)).ToList();
                List<DAL.FIELD_RULE> rules = new List<FIELD_RULE>();
                if (!span.HasValue) span = TimeSpan.FromMinutes(90);
                items.ForEach(i =>
                {
                    for (int d = dayOfWeekStart; d <= dayOfWeekEnd; d++)
                    {
                        TimeSpan curStart = start;
                        TimeSpan curEnd = curStart.Add(span.Value);
                        while (curStart < end)
                        {
                            rules.Add(
                            new FIELD_RULE()
                            {
                                FIELD_ID = id,
                                FIELD_ITEM_ID = i.ID,
                                TYPE = (int)EnumRuleType.Event,
                                SCHEDULE_TYPE = (int)d,
                                START_DATE = DateTime.Now.Date,
                                END_DATE = DateTime.Now.AddYears(1),
                                START_TIME = curStart,
                                END_TIME = curEnd.TotalDays >= 1 ? curEnd.Add(new TimeSpan(-1,0,0,0)) : curEnd,
                                PRICE = price,
                                RULE_NAME = "",
                                DESCRIPTION = start.ToString(@"h\:m") + "-" + curEnd.ToString(@"h\:m"),
                                STATUS = 1
                            });

                            curStart = curEnd;
                            curEnd = curStart.Add(span.Value);

                        }
                    }
                });
                //FieldManager.CreateFieldRule(rules);
            }
        }

        [TestMethod]
        public void IOSNotificationTest()
        {
            string apikey = "i6aOedizbxKhr6uK2Z2Gcm4R";
            string secretkey = "rqpVGG6nFxvVh1DfdcZlFTHiXGuSRpmn";

            string userid = "725591716807271383";

            string channelid = "5182738484625035365";
            BaiduPush Bpush = new BaiduPush("POST", secretkey);

            String method = "push_msg";
            TimeSpan ts = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            uint device_type = 4;//IOS
            uint unixTime = (uint)ts.TotalSeconds;
            string messages;

            string messageksy = unixTime.ToString();

           // IOSNotification notification = new IOSNotification();
            BaiduPushNotification notification = new BaiduPushNotification();
            notification.title = "123456";
            notification.description = "闯鬼了？";
            messages = JsonConvert.SerializeObject(notification);
            PushOptions pOpts;

            pOpts = new PushOptions(method, apikey, userid, channelid, device_type, messages, messageksy, unixTime);
            pOpts.message_type = 1;
            pOpts.deploy_status = 1;
            string response = Bpush.PushMessage(pOpts);

           // TextBoxResponse.Text = response;  
        }

        [TestMethod]
        public void SMSTest()
        {
            try
            {
               
               // Messager.SendSMS("13348967321", "测试短信发送5【踢球去】", DateTime.Now.AddHours(1));
                using (TIQIUEntities context = new TIQIUEntities())
                {
                    var msg = context.PushMessages.Where(p => p.SysNo == 1174).OrderByDescending(p => p.SysNo).FirstOrDefault();
                    if (msg != null)
                    {
                        Console.WriteLine("获取到消息：{0}！", msg.SysNo);
                        MessageObj obj = new MessageObj() { Content = msg.Message, OrderId = msg.PkId.GetValueOrDefault(0) };
                        string mes = JsonConvert.SerializeObject(obj);

                        BaiduPushNotification m = new BaiduPushNotification() { title = "来自测试", description = "这个能收到就好了！" };
                        string ms = JsonConvert.SerializeObject(m);
                        PushOptions option = new PushOptions("push_msg", "i6aOedizbxKhr6uK2Z2Gcm4R", msg.UserID, msg.ChannelID, (uint)4, ms, msg.SysNo.ToString(), (uint)DateTime.Now.Ticks);
                        //option.message_type = 1;



                        BaiduPush Bpush = new BaiduPush("POST", "rqpVGG6nFxvVh1DfdcZlFTHiXGuSRpmn");

                        String method = "push_msg";
                        TimeSpan ts = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
                        uint device_type = 4;//IOS
                        uint unixTime = (uint)ts.TotalSeconds;
                        string messages;

                        string messageksy = unixTime.ToString();


                        option.message_type = 1;
                        option.deploy_status = 1;



                       string rel = Bpush.PushMessage(option);//= new BaiduPush("POST", ConfigurationSettings.AppSettings["SecretKey"]).PushMessage(option);


                        JsonSerializerSettings js = new JsonSerializerSettings();
                        js.MissingMemberHandling = MissingMemberHandling.Ignore;
                        js.NullValueHandling = NullValueHandling.Ignore;
                        js.DefaultValueHandling = DefaultValueHandling.Ignore;
                        RelObj relobject = JsonConvert.DeserializeObject<RelObj>(rel,js);
                        if (relobject.error_code == null || relobject.error_code == "")
                        {
                            msg.Status = 1;
                        }
                        else
                        {
                            msg.RetryCount = msg.RetryCount + 1;
                        }
                        msg.RequestID = relobject.request_id;
                        //Log.WriteBizLog(rel);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }


        [TestMethod]
        public void SMSTest1()
        {
            try
            {
               
//                序列号：0SDK-EAA-6688-JEVRR
//密码：186490
//请注册后使用
//这个测试号报备的签名是【爱邻】

               
                
                
                
                
                var sm = new SMSTask();
                //sm.Register();
                sm.SendConfirmSMS();
                //Messager.SendSMS("13348967321", "【51足球】XXXX是您本次身份校验码，30分钟内有效，该校验码将在您使用后失效。", DateTime.Now);
                

                //SMSJOB.SMSTask ss = new SMSJOB.SMSTask();
                //ss.Register();
                //ss.SendSMS();
             
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
    public class RelObj
    {

        //   “request_id”:12394838223,
        //   “error_code”:30000,
        //   “error_msg”:”Request params not valid”
        //{"request_id":3263823361,"response_params":{"success_amount":1}}
        //}
        public string request_id { get; set; }
        public string error_code { get; set; }
        public string error_msg { get; set; }
    }

    class MessageObj
    {
        public string Content { get; set; }

        public int OrderId { get; set; }
    }

    class andriodContent{
        public string key1{get;set;}
        public string key2{get;set;}
    }

    class iosaps{
        public string alert {get;set;}
	    public string sound{get;set;}
	    public int badge{get;set;}
    }

    class iosMes{
        //android必选，ios可选
        public string title{get;set;}
        public string description {get;set;}

        //android特有字段，可选
        public int notification_builder_id {get;set;}
        public int notification_basic_style{get;set;}
        public int open_type {get;set;}
        public int net_support{get;set;}
        public int user_confirm{get;set;}
        public string url {get;set;}
        public string pkg_content{get;set;}
        public string pkg_name{get;set;}
        public string pkg_version{get;set;}

        //android自定义字段
        public andriodContent custom_content{get;set;}

        //ios特有字段，可选	  
        public string aps {get;set;}
        //ios的自定义字段
        //public string key1{get;set;}
        //public string key2{get;set;}
    }

    public class BaiduPushNotification
    {
        public string title { get; set; } //通知标题，可以为空；如果为空则设为appid对应的应用名;
        public string description { get; set; } //通知文本内容，不能为空;
        public int notification_builder_id { get; set; } //android客户端自定义通知样式，如果没有设置默认为0;
        public int notification_basic_style { get; set; } //只有notification_builder_id为0时才有效，才需要设置，如果notification_builder_id为0则可以设置通知的基本样式包括(响铃：0x04;振动：0x02;可清除：0x01;),这是一个flag整形，每一位代表一种样式;
        public int open_type { get; set; }//点击通知后的行为(打开Url：1; 自定义行为：2：其它值则默认打开应用;);
        public string url { get; set; } //只有open_type为1时才有效，才需要设置，如果open_type为1则可以设置需要打开的Url地址;
        public int user_confirm { get; set; } //只有open_type为1时才有效，才需要设置,(需要请求用户授权：1；默认直接打开：0), 如果open_type为1则可以设置打开的Url地址时是否请求用户授权;
        public string pkg_content { get; set; }//只有open_type为2时才有效，才需要设置, 如果open_type为2则可以设置自定义打开行为(具体参考管理控制台文档);
        public string custom_content { get; set; }// 自定义内容，键值对，Json对象形式(可选)；在android客户端，这些键值对将以Intent中的extra进行传递。

        public BaiduPushNotification()
        {
            notification_builder_id = 0;
            notification_basic_style = 0;

            url = "";
            user_confirm = 0;
            pkg_content = "";
            custom_content = "";
            open_type = 0;

        }
        public string aps { get; set; }

       
    }
}

