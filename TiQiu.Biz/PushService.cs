using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiQiu.DAL;

namespace TiQiu.Biz
{
    public class PushService
    {
        public void PushMessage(int userID, string message)
        {
            PushMessage(userID, DeviceType.All, message);
        }
        public void PushMessage(int userID, DeviceType deviceType, string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ApplicationException("消息为空，不能推送!");
            if (userID <= 0)
                throw new ApplicationException("未指定用户，不能推送!");

            using (TIQIUEntities context = new TIQIUEntities())
            {
                PushMessages pushMessage = new PushMessages();
                pushMessage.UserID = userID.ToString();
                pushMessage.PushType = ((int)PushType.Person).ToString();
                pushMessage.Tag = string.Empty;
                pushMessage.Message = message;
                pushMessage.Status = 0;
                if (deviceType == DeviceType.All)
                {
                    pushMessage.DeviceType = (int)DeviceType.Andriod;
                    context.PushMessages.Add(pushMessage);
                    pushMessage.DeviceType = (int)DeviceType.IOS;
                    context.PushMessages.Add(pushMessage);
                }
                else
                {
                    pushMessage.DeviceType = (int)deviceType;
                    context.PushMessages.Add(pushMessage);
                }
                context.SaveChanges();
            }
        }
        public void Broadcast(string message)
        {
            Broadcast(DeviceType.All, message);
        }
        public void Broadcast(DeviceType deviceType, string message)
        {
            Broadcast(DeviceType.All, message, string.Empty);
        }
        public void Broadcast(DeviceType deviceType, string message, string tag)
        {
            if (string.IsNullOrEmpty(message))
                throw new ApplicationException("消息为空，不能推送!");

            using (TIQIUEntities context = new TIQIUEntities())
            {
                PushMessages pushMessage = new PushMessages();
                pushMessage.UserID = string.Empty;
                if (string.IsNullOrEmpty(tag))
                {
                    pushMessage.PushType = ((int)PushType.All).ToString();
                }
                else
                {
                    pushMessage.PushType = ((int)PushType.Group).ToString();
                }
                pushMessage.Tag = tag;
                pushMessage.Message = message;
                pushMessage.Status = 0;
                if (deviceType == DeviceType.All)
                {
                    pushMessage.DeviceType = (int)DeviceType.Andriod;
                    context.PushMessages.Add(pushMessage);
                    pushMessage.DeviceType = (int)DeviceType.IOS;
                    context.PushMessages.Add(pushMessage);
                }
                else
                {
                    pushMessage.DeviceType = (int)deviceType;
                    context.PushMessages.Add(pushMessage);
                }
                context.SaveChanges();
            }
        }
    }
    public enum PushType
    {
        /// <summary>
        /// 单个人
        /// </summary>
        Person = 1,
        /// <summary>
        /// 一部分人
        /// </summary>
        Group = 2,
        /// <summary>
        /// 所有人
        /// </summary>
        All = 3
    }
    public enum DeviceType
    {
        Andriod = 3,
        IOS = 4,
        All = 10
    }
}