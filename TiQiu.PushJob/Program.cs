using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using TiQiu.Common.Util;
using TiQiu.DAL;
using TiQiu.Biz;
using Newtonsoft.Json;

namespace TiQiu.PushJob
{
    class Program
    {
        const string apiKey = "i6aOedizbxKhr6uK2Z2Gcm4R";
        const string secKey = "rqpVGG6nFxvVh1DfdcZlFTHiXGuSRpmn";
        const string pushMethod = "push_msg";
        const int intervalSec = 500;

        static System.Timers.Timer timer;
        
        static void Main(string[] args)
        {
            Console.WriteLine("服务已启动！");

            timer = new System.Timers.Timer();
            timer.Interval = intervalSec;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
            Console.ReadLine();
        }

        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                List<PushMessages> msgList = new List<PushMessages>();
                var now = DateTime.Now.AddMinutes(1);
                using (TIQIUEntities context = new TIQIUEntities())
                {

                    msgList = context.PushMessages
                        .Where(p => p.Status == 0
                        && p.RetryCount < 5
                        && p.PlanSendDate.Value <= now)
                        .OrderByDescending(p => p.SysNo)
                        .Take(20)
                        .ToList();
                    if (msgList.Count == 0) return;
                    var baiduPush = new BaiduPush("POST", secKey);
                    msgList.ForEach(msg =>
                    {

                        Console.WriteLine("获取到消息：{0}！", msg.SysNo);
                        string message = string.Empty;
                        if (msg.DeviceType.GetValueOrDefault(0) == 3)
                        {
                            CustomMessage obj = new CustomMessage()
                            {
                                MessageType = msg.MessageType,
                                Content = msg.Message,
                                PkId = msg.PkId.GetValueOrDefault(0).ToString(),
                                LinkUrl = msg.LinkUrl
                            };
                            message = JsonConvert.SerializeObject(obj);
                        }
                        else if (msg.DeviceType.GetValueOrDefault(0) == 4)
                        {
                            IOSNotification obj = new IOSNotification()
                            {
                                description = msg.Message
                            };
                            message = JsonConvert.SerializeObject(obj);
                        }
                        else
                        {

                        }
                        PushOptions option;
                        if (msg.PushType == ((int)EnumPushType.TagGroup).ToString())
                        {
                            option = new PushOptions(pushMethod, apiKey, msg.Tag, (uint)msg.DeviceType.Value, message, msg.SysNo.ToString(), (uint)DateTime.Now.Ticks);
                        }
                        else if (msg.PushType == ((int)EnumPushType.All).ToString())
                        {
                            option = new PushOptions(pushMethod, apiKey, (uint)msg.DeviceType.Value, message, msg.SysNo.ToString(), (uint)DateTime.Now.Ticks);
                        }
                        else
                        {
                            option = new PushOptions(pushMethod, apiKey, msg.UserID, msg.ChannelID, (uint)msg.DeviceType.Value, message, msg.SysNo.ToString(), (uint)DateTime.Now.Ticks);
                            
                        }

                        if (msg.DeviceType.GetValueOrDefault(0) == 4)
                        {
                            option.message_type = 1;
                            option.deploy_status = 1;
                        }
                        try
                        {
                            string rel = baiduPush.PushMessage(option);
                            JsonSerializerSettings js = new JsonSerializerSettings();
                            js.MissingMemberHandling = MissingMemberHandling.Ignore;
                            js.NullValueHandling = NullValueHandling.Ignore;
                            js.DefaultValueHandling = DefaultValueHandling.Ignore;
                            BaiduResponse relobject = JsonConvert.DeserializeObject<BaiduResponse>(rel, js);
                            msg.SEND_DATE = DateTime.Now;
                            if (relobject.error_code == null || relobject.error_code == "")
                            {
                                msg.Status = 1;
                            }
                            else if (relobject.error_code == "30608")//error_msg":"bind relation not found.
                            {
                                msg.Status = -1;
                                var errBind = context.BAIDU_USER_MEMBER.SingleOrDefault(b => b.CHANNEL_ID == msg.ChannelID && b.USER_ID == msg.UserID && b.MEMBER_ID == msg.MemberId);
                                if (errBind != null) context.BAIDU_USER_MEMBER.Remove(errBind);
                            }
                            else
                            {
                                Console.WriteLine("发送消息失败：{0}-{1}-{2}！", msg.SysNo, relobject.error_code, relobject.error_msg);
                                msg.RetryCount = msg.RetryCount + 1;
                            }
                            msg.RequestID = relobject.request_id;
                            Log.WriteBizLog(rel);
                        }
                        catch (Exception exx)
                        {
                            Console.WriteLine("发送消息失败：{0}-{1}！", msg.SysNo, exx.Message);
                        }
                    });
                    context.SaveChanges();
                }                
    
            }
            catch (Exception ex)
            {
                Console.WriteLine("程序出错：{0}！", ex.Message);
                Log.WriteException(ex.Message);
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                timer.Start();
            }
        }
    }

    

   
}
