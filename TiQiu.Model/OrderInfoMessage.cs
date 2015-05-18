using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Model
{
    public class OrderInfoMessage
    {
        public int OrderId { get; set; }
        public int ScheduledId { get; set; }
        public int FieldId { get; set; }
        public int FieldItemId { get; set; }
        public string FieldName { get; set; }
        public string FieldItemName { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public int Status { get; set; }
        public int OrderType { get; set; }
        public string OrderTypeText { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
       
    }
}