using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Model
{
    public class OrderFreeTeamView
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
        public DateTime ExpireDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int Type { get; set; }
        public string TypeText { get; set; }
        public string Remark { get; set; }
        public decimal Price { get; set; }
        public int PriceUnit { get; set; }
        public string PriceUnitText { get; set; }
        public int MinPlayer { get; set; }
        public int PlayerCount { get; set; }
        public string FieldName { get; set; }
        public string FieldItemName { get; set; }
        public int FieldItemType { get; set; }
        public string FieldItemTypeText { get; set; }
        public string FieldLogoUrl { get; set; }

    }
}