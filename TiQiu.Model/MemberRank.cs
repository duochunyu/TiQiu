using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Model
{
    public class MemberRank
    {
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public string MemberNickName { get; set; }
        public int Goal { get; set; }
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public int GameID { get; set; }
        public string Portrait { get; set; }
    }
}