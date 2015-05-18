using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Web.Model
{
    public class MemberInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public List<TeamInfo> TeamList { get; set; }
    }

    public class TeamInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}