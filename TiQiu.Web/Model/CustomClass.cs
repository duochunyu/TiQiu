using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Web
{
    public class CustomField
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public List<CustomFieldItem> FieldItemList { get; set; }
        public CustomField()
        {
            FieldItemList = new List<CustomFieldItem>();
        }
    }

    public class CustomFieldItem
    {
        public int ID { get; set; }
        public int FieldID { get; set; }
        public string BRIEF { get; set; }
    }

    public class CustomScheduled
    {
        public int ID { get; set; }
        public int FieldItemID { get; set; }
        public int FieldID { get; set; }
        public string FieldItemBrief { get; set; }
        public string FieldName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public decimal Price { get; set; }
    }

    public class CustomOrder
    {
        public int ID { get; set; }
        public int FieldItemID { get; set; }
        public int FieldID { get; set; }
        public int ScheduledID { get; set; }
        public int MemberID { get; set; }
        public bool NeedReferee { get; set; }
        public int OrderStatus { get; set; }
        public int OrderType { get; set; }
        public string PaymentType { get; set; }
        public CustomTeamScore TeamScore { get; set; }
    }

    public class CustomTeamScore
    {
        public int OrderID { get; set; }
        public int TeamAID { get; set; }
        public string TeamAName { get; set; }
        public string TeamAContactName { get; set; }
        public string TeamAPhone { get; set; }
        public string TeamAColor { get; set; }
        public int TeamBID { get; set; }
        public string TeamBName { get; set; }
        public string TeamBContactName { get; set; }
        public string TeamBPhone { get; set; }
        public string TeamBColor { get; set; }
    }
}