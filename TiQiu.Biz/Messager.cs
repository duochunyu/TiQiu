using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiQiu.DAL;
using TiQiu.Common.Util;
namespace TiQiu.Biz
{
    public static class Messager
    {
        private static SMSManager smsManager = SMSManager.GetInstace();

        private static SMS AddMessge(DAL.SMS entity)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {
                context.SMS.Add(entity);
                context.SaveChanges();
            }
            return entity;
        }

        private static List<DAL.SMS> AddMessge(List<DAL.SMS> entities)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {
                context.SMS.AddRange(entities);
                context.SaveChanges();
            }
            return entities;
        }

        public static void SendSMS(int memberId, string content)
        {
            SendSMS(memberId,content, DateTime.Now);
        }

        public static void SendSMS(int[] memberId, string content)
        {
            List<MEMBER> mList = MemberManager.GetMemberByIds(memberId);
            List<string> phoneList = new List<string>();
            mList.ForEach(m => { if(m.CELLPHONE.Length == 11)phoneList.Add(m.CELLPHONE);});
            SendSMS(phoneList.ToArray(), content, DateTime.Now);

        }

        public static void SendSMS(int memberId, string content, DateTime scheduleTime, int priority = 1, int orderId = 0)
        {
            MEMBER m = MemberManager.GetMemberById(memberId);
            if (m.CELLPHONE.Length == 11) SendSMS(new string[]{ m.CELLPHONE}, content, scheduleTime, memberId, priority, orderId);
            else throw new ApplicationException("会员没有注册手机号，未发送提示信息！");
        }

        public static void CancelSMS(int orderId,DateTime planTime)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {
                var sms = context.SMS.Where(s => s.ORDER_ID == orderId &&  s.STATUS == 0 && s.PLAN_SEND_TIME > planTime).ToList();
                sms.ForEach(s => s.STATUS = -1);
                context.SaveChanges();               
            }

        }

        public static void SendSMS(string[] phone, string content, DateTime scheduledTime, int memberId = 0,
            int priority = 1, int orderId = 0)
        {
           
                var smsList = new List<DAL.SMS>();
            for (int i = 0; i < phone.Length; i++)
            {
                var cellPhone = phone[i];

                try
                {
                    smsList.Add(new DAL.SMS
                    {
                        CTEATE_TIME = DateTime.Now,
                        ORDER_ID = orderId,
                        MESSAGE = content,
                        PHONE = cellPhone,
                        PLAN_SEND_TIME = scheduledTime,
                        PRIORITY = priority,
                        STATUS = 0,
                        EXT_INFO = orderId > 0 ? string.Format("{0}-{1}", memberId, orderId) : "",
                        MEMBER_ID = memberId,
                        SMS_SEQID = orderId
                    });
                }
                catch (Exception ex)
                {
                    Log.WriteException(string.Format("消息无法发送:{0}-{1}-{2}", cellPhone, orderId, ex.Message));
                }

            }
            try
            {
                AddMessge(smsList);
            }
            catch (Exception x)
            {
                Log.WriteException(string.Format("消息添加失败:{0}-{1}", orderId, x.Message));
            }

        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="memberId"></param>
        /// <param name="messageType"></param>
        /// <param name="planSendDate"></param>
        /// <param name="pkId"></param>
        public static void PushMessage2Member(string message, int[] memberIds, EnumMessageType messageType, int pkId, DateTime planSendDate)
        {
            try
            {
                using (TIQIUEntities context = new TIQIUEntities())
                {

                    List<BAIDU_USER_MEMBER> dList = context.BAIDU_USER_MEMBER.Where(b =>memberIds.Contains(b.MEMBER_ID)).ToList();
                    dList.ForEach(d =>
                    {
                        PushMessages msg = new PushMessages()
                        {
                            MemberId = d.MEMBER_ID,
                            PushType = ((int)EnumPushType.SingleMember).ToString(),
                            Status = 0,
                            Tag = "",
                            DeviceType = d.DEVICE_TYPE,
                            Message = message,
                            UserID = d.USER_ID,
                            ChannelID = d.CHANNEL_ID,
                            PkId = pkId,
                            MessageType = (int)messageType,
                            PlanSendDate = planSendDate
                        };
                        context.PushMessages.Add(msg);
                    });
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                Log.WriteException(string.Format("PushMessage2Member error,memberId:{0},messageType:{1},pkId{2},message{3},ex:{4}", string.Join("-",memberIds), messageType.ToString(), pkId, message, ex.Message));
            }
        }

        public static void PushMessage2All(string message,EnumMessageType messageType, int pkId, DateTime planSendDate)
        {
            try
            {
                using (TIQIUEntities context = new TIQIUEntities())
                {                   
                    PushMessages msg = new PushMessages()
                    {                            
                        PushType = ((int)EnumPushType.All).ToString(),
                        Status = 0,
                        Tag = "",
                        DeviceType = 0,
                        Message = message,
                        PkId = pkId,
                        MessageType = (int)messageType,
                        PlanSendDate = planSendDate
                    };
                    context.PushMessages.Add(msg);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(string.Format("PushMessage2All error,messageType:{0},pkId{1},message{2},ex:{3}", messageType.ToString(), pkId, message, ex.Message));
            }
        }

        public static void PushOrderMessage(string message, int memberId, int orderId)
        {
            PushMessage2Member(message,new int[]{ memberId}, EnumMessageType.OrderInfo, orderId, DateTime.Now);
        }
        public static void PushOrderMessage(string message, int[] memberIds, int orderId)
        {
            PushMessage2Member(message, memberIds, EnumMessageType.OrderInfo, orderId, DateTime.Now);       
        }
    }
}
