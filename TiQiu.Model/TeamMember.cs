using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Model
{
    public class TeamMember
    {
        public int RoleId { get; set; }
        public int MemberId { get; set; }
        public string RoleName { get; set; }
        public string MemberName { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public int? TeamNumber { get; set; }
        public string Portrait { get; set; }
    }
}