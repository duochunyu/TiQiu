using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiQiu.PushJob
{
    class IOSNotification
    {
        public string title { get; set; } //通知标题，可以为空；如果为空则设为appid对应的应用名;
        public string description { get; set; } //通知文本内容，不能为空;
        public string aps { get; set; }

        public IOSNotification()
        {
        }
    }
}
