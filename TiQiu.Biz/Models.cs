using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TiQiu.Biz
{
    public class FieldScheduledInfo
    {
        #region scheduled info

        public int ScheduledID { get; set; }
        public int FieldID { get; set; }
        public int FieldItemID {get;set;}
        public int Status { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime ScheduledDate { get; set; }
        public decimal Price { get; set; }
        public string ScheduledRemark {get;set;}

        #endregion

        #region Order Info

        public int? OrderID { get; set; }
        public DateTime? OrderCreateDate{get;set;}
        public bool? NeedReferee { get; set; }
        public int? OrderType { get; set; }
        public int? OrderStatus { get; set; }
        public int? MemberID { get; set; }
        public string MemberName { get; set; }
        public string MemberPhone { get; set; }
        public int? MemberBID { get;set;}
        public string MemberBName {get;set;}
        public string MemberBPhone { get; set; }
        public int? PkPayType {get;set;}
        public int? SoloMinPlayer {get;set;}
        public decimal? OrderPrice {get;set;}
        public decimal? Income { get; set; }
        public int? PriceUnit {get;set;}
        public string OrderRemark {get;set;}

        #endregion      

        #region Team Score

        public int? TeamID { get; set; }
        public string TeamName { get; set; }
        public string TeamColor { get; set; }
        public int? TeamBID { get; set; }
        public string TeamBName { get; set; }
        public string TeamBColor { get; set; }

        #endregion

        /// <summary>
        /// 单飞加入记录
        /// </summary>
        public List<SoloLog> SoloLogList { get; set; }

        #region extends.

        public bool IsMyOrder { get; set; }
        public int SoloPalyerCount { get; set; }
        public string HTMLAttributeString { get; set; }
        public string HTMLDisplayString { get; set; }
        
        #endregion
    }
    public class SoloLog
    {
        public int OrderID { get; set; }
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public string MemberPhone { get; set; }
        public DateTime JoinDate { get; set; }
        public int CountMember { get; set; }
    }

}
