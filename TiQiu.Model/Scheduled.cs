using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Model
{
    public class Scheduled
    {
        public int ID { get; set; }
        public int FieldID { get; set; }
        public int FieldItemID { get; set; }
        public System.DateTime CurDate { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
        public string Remark { get; set; }
        public int RuleType { get; set; }
    }

    //public class Order
    //{
    //    public int ID { get; set; }
    //    public int ScheduledID { get; set; }
    //    public int FieldID { get; set; }
    //    public int FieldItemID { get; set; }
    //    public System.DateTime OrderDate { get; set; }
    //    public bool NeedReferee { get; set; }
    //    public int Status { get; set; }
    //    public System.TimeSpan StartTime { get; set; }
    //    public System.TimeSpan EndTime { get; set; }
    //    public int MemberID { get; set; }
    //    public System.DateTime ExpireDate { get; set; }
    //    public System.DateTime CreateDate { get; set; }
    //    public int Type { get; set; }
    //    public Nullable<int> PkPayType { get; set; }
    //    public Nullable<decimal> InCome { get; set; }
    //    public Nullable<int> Score { get; set; }
    //    public string Remark { get; set; }
    //    public Nullable<int> MemberBID { get; set; }
    //    public Nullable<decimal> Price { get; set; }
    //    public Nullable<int> PriceUnit { get; set; }
    //    public Nullable<int> FreeTeamMinPlayer { get; set; }

    //    public string FIELD_NAME { get; set; }
    //    public int FIELD_ITEM_TYPE { get; set; }
    //    public string FIELD_ITEM_NAME { get; set; }
    //    public string MEMBER_NAME { get; set; }
    //    public int BUSINESSES_ID { get; set; }

    //}

    //public class OrderLog
    //{
    //    public int ID { get; set; }
    //    public int OrderID { get; set; }
    //    public int FieldItemID { get; set; }
    //    public string Operation { get; set; }
    //    public Nullable<int> MemberID { get; set; }
    //    public Nullable<int> AccountBID { get; set; }
    //    public System.DateTime LogDate { get; set; }

    //}
}