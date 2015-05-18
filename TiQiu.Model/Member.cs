using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Model
{
    public class Member
    {
        public int MemberID { get; set; }
        public int AccountID { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public string Portrait { get; set; }
        public string Title { get; set; }
        public string Feature { get; set; }        
        public int Sex { get; set; }
        public DateTime Brithday { get; set; }
        public string Email { get; set; }   
   
    }
}