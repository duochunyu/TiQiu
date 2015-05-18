using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Model
{

    public class MyOrderView
    {
        public int ID { get; set; }
        
        /// <summary>
        /// 类型 Normal = 0,PK=1,FreeTeam = 2
        /// </summary>
        public int Type { get; set; }
        public int Status { get; set; }
        public int MemeberId { get; set; }

        public string FieldName { get; set; }
        public string FieldLogoUrl { get; set; }
        public string FieldItemName { get; set; }
        public int FieldType { get; set; }


        /// <summary>
        /// 下单日期时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 比赛日期（仅日期有效）
        /// </summary>
        public DateTime OrderDate { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public decimal Price { get; set; }
        public decimal? Payment { get; set; }
    }

    public class OrderView
    {
        public OrderView()
        {
            FreeTeamJoinLog = new List<FreeTeamLog>();
        }
        public int ID { get; set; }
        public int ScheduledId { get; set; }
        /// <summary>
        /// 类型 Normal = 0,PK=1,FreeTeam = 2
        /// </summary>
        public int Type { get; set; }
        public int Status { get; set; }
       
        public int FieldId { get; set; }        
        public string FieldName { get; set; }
        public string FieldLogoUrl { get; set; }
        public int FieldItemId { get; set; }
        public string FieldItemName { get; set; }
        public int FieldType { get; set; }       
        /// <summary>
        /// 下单日期时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 比赛日期（仅日期有效）
        /// </summary>
        public DateTime OrderDate { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public bool? NeedReferee { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// PK付款方式   AA = 1, 约战方付费 = 2, 输家付款 = 3
        /// </summary>
        public int? PKPayType { get; set; }
        /// <summary>
        /// 单飞付款单位（PerPlayer每人 = 0,AA= 1）
        /// </summary>
        public int? FreeTeamPriceUnit { get; set; }
        /// <summary>
        /// 实付
        /// </summary>
        public decimal? Payment { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创单人ID
        /// </summary>
        public int? MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberPhone { get; set; }
        /// <summary>
        /// 创单人球队
        /// </summary>
        public int? TeamId { get; set; }
        public string TeamName { get; set; }

        /// <summary>
        /// PK应战人ID
        /// </summary>
        public int? MemberBId { get; set; }
        public string MemberBName { get; set; }
        public string PhoneB { get; set; }
        /// <summary>
        /// PK应战人球队
        /// </summary>
        public int? TeamBId { get; set; }
        public string TeamBName { get; set; }       
       
        /// <summary>
        /// 单飞最小要求人数
        /// </summary>
        public int? FreeTeamMinPlayer { get; set; }
        /// <summary>
        /// 单飞当前加入人数
        /// </summary>
        public int? FreeTeamPlayer { get; set; }
        /// <summary>
        /// 单飞加入记录
        /// </summary>
        public List<FreeTeamLog> FreeTeamJoinLog { get; set; }        
    }

    public class FreeTeamLog
    {
        public int OrderId { get; set; }
        public int MemberId{get;set;}
        public string MemberName{get;set;}
        /// <summary>
        /// 加入人数（包括加入人）
        /// </summary>
        public int JoinPlayerCount {get;set;}
        public DateTime JoinDate {get;set;}       
    }

    
}