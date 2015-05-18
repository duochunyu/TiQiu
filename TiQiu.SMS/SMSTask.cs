using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TiQiu.Common.Util;
using System.Web.Services;
using TiQiu.DAL;
using TiQiu.Biz;
namespace TiQiu.SMS
{
    public class SMSTask
    {
        
        private static string _sn = ConfigurationManager.AppSettings["SMSSN"];
        private static string _key = ConfigurationManager.AppSettings["SMSKEY"];
        private static string _pwd = ConfigurationManager.AppSettings["SMSPWD"];
        private static string _eName = ConfigurationManager.AppSettings["ENAME"];
        private static string _linkMan = ConfigurationManager.AppSettings["LINKMAN"];
        private static string _phoneNum = ConfigurationManager.AppSettings["PHONENUM"];
        private static string _eMail = ConfigurationManager.AppSettings["EMAIL"];
        private static string _mobile = ConfigurationManager.AppSettings["MOBILE"];
        private static string _fax = ConfigurationManager.AppSettings["FAX"];
        private static string _address = ConfigurationManager.AppSettings["ADDRESS"];
        private static string _postcode = ConfigurationManager.AppSettings["POSTCODE"];
       


        private Dictionary<int, string> Message = new Dictionary<int, string>();

        private WSService.SDKService serviceClient;
       
        public SMSTask()
        {
            InitMessage();            
            Init();
            //Register();
            //RegistDetail();
        }

        private void InitMessage()
        {
            Message.Clear();

            Message.Add(0, "成功");
            Message.Add(-1, "系统异常");
            Message.Add(-2, "客户端异常");
            Message.Add(-101, "命令不被支持");
            Message.Add(-102, "RegistryTransInfo删除信息失败");
            Message.Add(-103, "RegistryInfo更新信息失败");
            Message.Add(-104, "请求超过限制");
            Message.Add(-110, "号码注册激活失败");
            Message.Add(-111, "企业注册失败");
            Message.Add(-113, "充值失败");
            Message.Add(-117, "发送短信失败");
            Message.Add(-118, "接收MO失败");
            Message.Add(-119, "接收Report失败");
            Message.Add(-120, "修改密码失败");
            Message.Add(-122, "号码注销激活失败");
            Message.Add(-123, "查询单价失败");
            Message.Add(-124, "查询余额失败");
            Message.Add(-125, "设置MO转发失败");
            Message.Add(-126, "路由信息失败");
            Message.Add(-127, "计费失败0余额");
            Message.Add(-128, "计费失败余额不足");
            Message.Add(-190, "数据操作失败");
            Message.Add(-1100, "序列号错误,序列号不存在内存中,或尝试攻击的用户");
            Message.Add(-1102, "序列号密码错误");
            Message.Add(-1103, "序列号Key错误");
            Message.Add(-1104, "路由失败，请联系系统管理员");
            Message.Add(-1105, "注册号状态异常, 未用 1");
            Message.Add(-1107, "注册号状态异常, 停用 3");
            Message.Add(-1108, "注册号状态异常, 停止 5");
            Message.Add(-1131, "充值卡无效");
            Message.Add(-1132, "充值密码无效");
            Message.Add(-1133, "充值卡绑定异常");
            Message.Add(-1134, "充值状态无效");
            Message.Add(-1135, "充值金额无效");
            Message.Add(-1901, "数据库插入操作失败");
            Message.Add(-1902, "数据库更新操作失败");
            Message.Add(-1903, "数据库删除操作失败");
            Message.Add(-9000, "数据格式错误,数据超出数据库允许范围");
            Message.Add(-9001, "序列号格式错误");
            Message.Add(-9002, "密码格式错误");
            Message.Add(-9003, "客户端Key格式错误");
            Message.Add(-9004, "设置转发格式错误");
            Message.Add(-9005, "公司地址格式错误");
            Message.Add(-9006, "企业中文名格式错误");
            Message.Add(-9007, "企业中文名简称格式错误");
            Message.Add(-9008, "邮件地址格式错误");
            Message.Add(-9009, "企业英文名格式错误");
            Message.Add(-9010, "企业英文名简称格式错误");
            Message.Add(-9011, "传真格式错误");
            Message.Add(-9012, "联系人格式错误");
            Message.Add(-9013, "联系电话");
            Message.Add(-9014, "邮编格式错误");
            Message.Add(-9015, "新密码格式错误");
            Message.Add(-9016, "发送短信包大小超出范围");
            Message.Add(-9017, "发送短信内容格式错误");
            Message.Add(-9018, "发送短信扩展号格式错误");
            Message.Add(-9019, "发送短信优先级格式错误");
            Message.Add(-9020, "发送短信手机号格式错误");
            Message.Add(-9021, "发送短信定时时间格式错误");
            Message.Add(-9022, "发送短信唯一序列值错误");
            Message.Add(-9023, "充值卡号格式错误");
            Message.Add(-9024, "充值密码格式错误");
            Message.Add(-9025, "客户端请求sdk5超时");


        }

        public void Init()
        {
            serviceClient = new WSService.SDKService();          
            
        }

        public void Register()
        {
            int result = 99999;
            //public int registEx(String softwareSerialNo, String key, String serialpass)
            try{
                result = serviceClient.registEx(_sn, _pwd,_key);
            }catch(Exception ex){
                Log.WriteException(string.Format("调用注册服务异常：{0}-{1}", ex.Message,ex.StackTrace));
            }

            if (result == 0)
            {
                
                Log.WriteException(string.Format("注册服务成功：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            }
            else
            {
                WriteLog(result, "Register Result:");
            }
        }


        public void GetReport()
        {
            try
            {
                //public List<StatusReport> getReport(String softwareSerialNo, String key)
                WSService.statusReport[] sr = serviceClient.getReport(_sn, _key);
                if (sr == null || sr.Length == 0) return;
                List<long> keys = new List<long>();
                sr.ToList().ForEach(s=>{keys.Add(s.seqID);});

                using (TIQIUEntities context = new TIQIUEntities())
                {
                    var items = (from sms in context.SMS 
                          where (keys.Contains(sms.ID)) 
                          select sms).ToList();
                    sr.ToList().ForEach(s =>
                    {
                        var it = items.SingleOrDefault(i => i.ID == s.seqID);
                        if (it == null)
                        {
                            Log.WriteException(string.Format("发送报告OrderID-{0}没有对应记录，发送状态{1},电话{2}", s.seqID, s.reportStatus, s.mobile));

                        }
                        else
                        {
                            it.STATUS = s.reportStatus == 0 ? 1 : 0;
                            it.SMS_NUMBER = s.serviceCodeAdd;
                            it.ERROR_CODE = s.errorCode;
                            it.SEND_TIME = DateTime.ParseExact(s.receiveDate, "yyyyMMddHHmmss", null);
                        }
                    });
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }    

        //public List<MO> getMO(String softwareSerialNo, String key) throws Exception
        public void getMO()
        {
            
            try
            {
                WSService.mo[] mos = serviceClient.getMO(_sn, _key);
                if (mos == null || mos.Length == 0) return;
                List<DAL.SMS_RECEIVE> res = new List<SMS_RECEIVE>();
                for (int i = 0; i < mos.Length; i++)
                {
                    var item = mos[i];
                    var entity = new DAL.SMS_RECEIVE()
                    {
                        PHONE = item.mobileNumber,
                        RECEIVE_DATE = DateTime.ParseExact(item.sentTime,"yyyyMMddHHmmsss",null),
                        MESSAGE = item.smsContent.Trim(),
                        EXT_INFO = item.addSerial,
                        SMS_NUMBER = item.channelnumber                      

                    };
                    res.Add(entity);

                    if (entity.EXT_INFO.Length > 0 && entity.MESSAGE.Length > 0)
                    {
                        var key = item.addSerial.Split(new char[]{'-'}, StringSplitOptions.RemoveEmptyEntries).ToList();
                        int memberId,orderId;
                        if (key.Count == 2 && int.TryParse(key[0], out memberId) && int.TryParse(key[1], out orderId))
                        {
                            try
                            {
                                if (entity.MESSAGE == "9")
                                {
                                    OrderManager.ConfirmOrder(orderId, memberId, "客户回复短信确认！");
                                }
                                else if (entity.MESSAGE == "0")
                                {
                                    OrderManager.CancelOrderByMember(orderId, memberId, "客户回复短信取消预订！");
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.WriteException(string.Format("客户回复确认出错：{2}-orderId:{0}-memberId:{1}",orderId,memberId,ex.Message));
                            }
                        }

                    }
                }

                using (TIQIUEntities context = new TIQIUEntities())
                {
                    context.SMS_RECEIVE.AddRange(res);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(string.Format("获取回复异常：{0}-{1}", ex.Message, ex.StackTrace));
            }
        }

        public void SendConfirmSMS()
        {
            int result = 99999;
            try
            {
                result = serviceClient.sendSMS(_sn, _pwd, DateTime.Now.ToString("yyyyMMddHHmmss"), new string[] { "13348967321"}, "【51足球】您本次身份校验码是XXX，请尽快使用，该校验码将在您使用后失效。", "", "GBK", 1, (long)0);
                result += serviceClient.sendSMS(_sn, _pwd, DateTime.Now.ToString("yyyyMMddHHmmss"), new string[] { "13880873803", }, "【51足球】您本次身份校验码是XXX，请尽快使用，该校验码将在您使用后失效。", "", "GBK", 1, (long)0);
                result += serviceClient.sendSMS(_sn, _pwd, DateTime.Now.ToString("yyyyMMddHHmmss"), new string[] { "18981756911", }, "【51足球】您本次身份校验码是XXX，请尽快使用，该校验码将在您使用后失效。", "", "GBK", 1, (long)0);
                result += serviceClient.sendSMS(_sn, _pwd, DateTime.Now.ToString("yyyyMMddHHmmss"), new string[] { "13408568554", }, "【51足球】您本次身份校验码是XXX，请尽快使用，该校验码将在您使用后失效。", "", "GBK", 1, (long)0);
                result += serviceClient.sendSMS(_sn, _pwd, DateTime.Now.ToString("yyyyMMddHHmmss"), new string[] { "13881929492", }, "【51足球】您本次身份校验码是XXX，请尽快使用，该校验码将在您使用后失效。", "", "GBK", 1, (long)0);
                
            }
            catch (Exception ex)
            {
                Log.WriteException(string.Format("调用信息发送异常：{0}-{1}", ex.Message, ex.StackTrace));

            }
            if (result != 0)
            {
               
                WriteLog(result, "信息发送失败:" + result);
            }
            
        }

        private void WriteLog(int key, string prefix)
        {
            if (key == 0)
            {
                return;
            }

            if (Message.ContainsKey(key))
            {
                Log.WriteException(string.Format("{1}{0}", Message[key], prefix));
            }
            else
            {
                Log.WriteException(string.Format("{1}{0}", key, prefix));
            }
        }
    }
}
