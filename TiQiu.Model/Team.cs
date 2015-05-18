using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Model
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string LinkPhone { get; set; }
        public int Level { get; set; }
        public int Score { get; set; }
        public int Win { get; set; }
        public int Lose { get; set; }
        public int Draw { get; set; }
        public string Brief { get; set; }
        public DateTime BuildDate { get; set; }
        public string Declaration { get; set; }
        public string Feature { get; set; }
        public List<TeamMember> TeamMemberList { get; set; }
    }
}