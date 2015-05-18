using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiQiu.Biz
{
    public enum EnumPushType
    {
        SingleMember = 1,
        TagGroup = 2,
        All = 3
    }

    public enum EnumMessageType
    {
        Info = 0,

        WebLink = 1,

        OrderInfo = 2,

        GameInfo = 3,

        GameLive = 4,

        FieldInfo = 5,
 
        TeamInfo = 6
    }

    public enum EnumDataStatus
    {
        Actived = 1,

        Normal = 0,

        Void = -1
    }

    /// <summary>
    /// 场地规则类型
    /// </summary>
    public enum EnumRuleType
    {
        /// <summary>
        /// 正常场次
        /// </summary>
        Event = 0,
        /// <summary>
        /// 不可用
        /// </summary>
        Void = -1
    }

    /// <summary>
    /// 规则频率类型
    /// </summary>
    public enum EnumScheduleType
    {
      

        Daily = 0,
        /// <summary>
        /// 每周一
        /// </summary>
        Monday = 1,

        Tuesday =2,

        Wednesday = 3,

        Thusday = 4,

        Friday = 5,

        Saturday = 6,

        Sunday = 7,
        /// <summary>
        /// 按月重复
        /// </summary>
        Mothly = 8,
        /// <summary>
        /// 按年重复
        /// </summary>
        Yearly = 9,
      /// <summary>
        /// 只一次
        /// </summary>
        Once = 10
        
    }

    public enum EnumFieldType
    {
        /// <summary>
        /// 五人制
        /// </summary>
        Five = 5,
        Seven = 7,
        Nine = 9,
        Eleven = 11
    }

    public enum EnumFileType
    {
        Field_Slider_Pic  = 1,

        Field_Item_Pic = 2,

        Member_Portrait = 3,

        Team_Pic = 4,

        Slider_Pic = 5,

        Field_Logo = 6,

        Game_Logo = 7,

        Game_Slider_Pic = 8,

        Game_Pic = 9


    }

    public enum EnumPKPayType
    {
        /// <summary>
        /// AA制
        /// </summary>
        AA = 1,
        
        /// <summary>
        /// 约战方付费
        /// </summary>
        TeamA = 2,

        /// <summary>
        /// 输家付款
        /// </summary>
        Bet = 3
    }
    
    public enum EnumOrderType
    {
        /// <summary>
        /// 普通预订
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 约战
        /// </summary>
        PK = 1,
        /// <summary>
        /// 散打、单飞、自由组合
        /// </summary>
        FreeTeam = 2
    }

    public enum EnumPriceUnit
    {
        PerPlayer = 0,

        AA = 1
    }

    public enum EnumFieldStatus
    {
        /// <summary>
        /// 可用
        /// </summary>
        Available = 0,

        /// <summary>
        /// 预订申请中
        /// </summary>
        Booking = 1,

        /// <summary>
        /// 已预订
        /// </summary>
        Booked = 2,

        /// <summary>
        /// 用户已确认
        /// </summary>
        CustomerConfirmed = 3,

        /// <summary>
        /// 用户已到达
        /// </summary>
        CheckIn = 10,

        /// <summary>
        /// 比赛结束
        /// </summary>
        Ending = 20,

        /// <summary>
        /// 预订过期
        /// </summary>
        Expired = 50,

        /// <summary>
        /// 已取消
        /// </summary>
        Canceled = 60,

        /// <summary>
        /// 不可用
        /// </summary>
        Void = -1
    }

    public enum EnumTeamMemberRole
    {
        /// <summary>
        /// 队员
        /// </summary>
        Member = 0,

        /// <summary>
        /// 队长
        /// </summary>
        Leader = 1,

        /// <summary>
        /// 联络人
        /// </summary>
        Contact = 2,

        /// <summary>
        /// 创建者
        /// </summary>
        Creator = 3
       
    }

    public enum EnumGameRoundType
    {
        /// <summary>
        /// 淘汰赛
        /// </summary>
        Knockout = 0,
 
        /// <summary>
        /// 循环赛
        /// </summary>
        RoundRobin =1
    }

    public enum EnumGameEventType
    {
        /// <summary>
        /// 进球
        /// </summary>
        Goal = 0,
        /// <summary>
        /// 黄牌
        /// </summary>
        Yellow =1,
        /// <summary>
        /// 红牌
        /// </summary>
        Red = 2,
        /// <summary>
        /// 换上
        /// </summary>
        Join = 3,
        /// <summary>
        /// 换下
        /// </summary>
        Quit = 4,
        /// <summary>
        /// 点球
        /// </summary>
        Penalty = 5
    }

    public enum EnumGameScheduledStatus
    {
        /// <summary>
        /// 计划比赛
        /// </summary>
        Normal =0,

        /// <summary>
        /// 直播中
        /// </summary>
        Live = 1,

        /// <summary>
        /// 直播结束
        /// </summary>
        Over = 2
    }

    public enum EnumOS
    {
        Andriod = 1,

        IOS = 2
    }
}
