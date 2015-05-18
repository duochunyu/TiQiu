using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Model
{
    public class Account
    {
        //public int AccountID { get; set; }
        //public string Name { get; set; }
        //public string Phone { get; set; }
        //public int MemberID { get; set; }
        public string Email { get; set; }
        //public string Adress { get; set; }
        //public int Level { get; set; }
        //public int Score { get; set; }
        //public string NickName { get; set; }
        //public string Position { get; set; }
        //public string Fav_foot { get; set; }
        //public string Fav_Team { get; set; }
        //public string Fav_Star { get; set; }
        public string Brief { get; set; }
        //public string Work { get; set; }
        //public string Intro { get; set; }
        public DateTime? Brithday { get; set; }
        
        //public DateTime? PlayAge { get; set; }
        //public string Title { get; set; }
        //public string Feature { get; set; }
        //public string Token { get; set; }
        

        public int MemberID { get; set; }
        public int AccountID { get; set; }
        public string Name { get; set; }
        public string MemberName { get; set; }
        public string NickName { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public string Portrait { get; set; }
        public string Title { get; set; }
        public string Feature { get; set; }
        public string Token { get; set; }
        public int Sex { get; set; }
        public string Role { get; set; }
    
    }

    public class AccountB
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }
        public int BusinessesID {get;set;}
        public List<Field> FieldList { get; set; }
        

    }
}