using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Model
{
    public class GameComment
    {
        public int ID { get; set; }
        public string MsgId { get; set; }
        public int GameId { get; set; }
        public int ScheduledId { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }        
    }
}