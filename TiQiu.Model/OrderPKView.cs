using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Model
{
    public class OrderPKView
    {
        public int OrderId { get; set; }
        public int ScheduledId { get; set; }
        public int FieldId { get; set; }
        public int FieldItemId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool NeedReferee { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public int MemberId { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int Type { get; set; }
        public string TypeText { get; set; }
        public decimal Price { get; set; }
        public decimal Payment { get; set; }
        public string Remark { get; set; }
        public int MemberBId { get; set; }
        public int TeamId { get; set; }
        public int TeamBId { get; set; }
        public string TeamLogoUrl { get; set; }
        public string TeamName { get; set; }
        public string TeamBName { get; set; }
        public string TeamBLogoUrl { get; set; }
        public string Score { get; set; }
        public int ReceiveScore { get; set; }
        public int LoseScore { get; set; }
        public string TeamColor { get; set; }
        public string TeamBColor { get; set; }
        public string FieldName { get; set; }
        public string FieldItemName { get; set; }
        public string FieldItemTypeText { get; set; }
        public string FieldLogoUrl { get; set; }
        public int PKPayType { get; set; }
        public string PKPayTypeText { get; set; }
    }
}