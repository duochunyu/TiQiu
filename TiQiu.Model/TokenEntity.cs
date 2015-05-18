using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Model
{
    public class TokenEntity
    {
        public bool IsAccount { get; set; }
        public int AccountId { get; set; }
        public string AcccountName { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string NickName { get; set; }
        public int AccountBId { get; set; }
        public string AccountBName { get; set; }
        public string PortraitUrl { get; set; }
        public string Phone { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<int> Fields { get; set; }

    }
}