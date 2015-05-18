using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiQiu.DAL;

namespace TiQiu.Biz
{
    public static class ConstValue
    {
        public static List<AREA> GetAreaList(bool needAll)
        {
            List<AREA> areas = new List<AREA>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                areas = context.AREAs.ToList();

            }
            if (needAll) areas.Insert(0, new AREA() { CODE = "0", AREA_NAME = "全部" });
            return areas;
        }

        public static string GetAreaText(string areaCode)
        {
            string str = string.Empty;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                var area = context.AREAs.Single<AREA>(a => a.CODE == areaCode);
                if(area != null) str = area.AREA_NAME;
            }
            return str;
        }

        public static List<KeyValuePair<string, string>> GetFieldType(bool needAll = false)
        {
            List<KeyValuePair<string, string>> types = new List<KeyValuePair<string, string>>();
            foreach (var v in typeof(EnumFieldType).GetEnumValues())
            {
                int t = (int)v;
                types.Add(new KeyValuePair<string, string>(t.ToString() + "人制", t.ToString()));
            }
            if (needAll) types.Insert(0, new KeyValuePair<string, string>("全部", "0"));
            return types;
        }

        public static List<KeyValuePair<int, string>> GetTeamMemberRoleList()
        {
            List<KeyValuePair<int, string>> roles = new List<KeyValuePair<int, string>>();
            foreach (var v in typeof(EnumTeamMemberRole).GetEnumValues())
            {
                
                int t = (int)v;
               if(t != (int)EnumTeamMemberRole.Creator)roles.Add(new KeyValuePair<int, string>(t,GetTeamMemberRoleName(t)));
            }
            
            return roles;
        }

        public static string GetTeamMemberRoleName(int roleId)
        {
            string str = "未知角色";
            switch (roleId)
            {
                case (int)EnumTeamMemberRole.Member:
                    str = "队员";
                    break;
                case (int)EnumTeamMemberRole.Leader:
                    str = "队长";
                    break;
                case (int)EnumTeamMemberRole.Contact:
                    str = "联系人";
                    break;
                case (int)EnumTeamMemberRole.Creator:
                    str = "创建者";
                    break;
                default:
                    str = "未知角色";
                    break;
            }
            return str;
        }

        public static string GetGameRoundTypeName(int roundType)
        {
            string str = "未定赛制";
            switch (roundType)
            {
                case (int)EnumGameRoundType.Knockout:
                    str = "淘汰赛";
                    break;
                case (int)EnumGameRoundType.RoundRobin:
                    str = "循环赛";
                    break;             
                default:
                    str = "未定赛制";
                    break;
            }
            return str;
        }

        public static string GetFieldItemTypeText(int type)
        {
            string str = "未知类型";
            foreach (var v in typeof(EnumFieldType).GetEnumValues())
            {
                int t = (int)v;
                if (t == type)
                {
                    str = t.ToString() + "人制";
                    break;
                }                
            }
            return str;

        }

        public static string GetOrderTypeText(int typeId)
        {
            
            string text = string.Empty;
            switch (typeId)
            {
                case (int)EnumOrderType.PK:
                    text = "PK";
                    break;
                case (int)EnumOrderType.Normal:
                    text = "普通预订";
                    break;
                case (int)EnumOrderType.FreeTeam:
                    text = "单飞";
                    break;
                default:
                    text = "未知类型";
                    break;
            }
            return text;

        }

        public static string GetPKPayTypeText(int typeId)
        {
            string text = string.Empty;
            switch (typeId)
            {
                case (int)EnumPKPayType.AA:
                    text = "AA制";
                    break;
                case (int)EnumPKPayType.Bet:
                    text = "输家付钱";
                    break;
                case (int)EnumPKPayType.TeamA:
                    text = "约战方付钱";
                    break;
                default:
                    text = "待定";
                    break;
            }
            return text;

        }

        public static string GetFreeTeamPriceUnitText(int unitId)
        {
            string text = string.Empty;
            switch (unitId)
            {
                case (int)EnumPriceUnit.AA:
                    text = "AA制";
                    break;
                case (int)EnumPriceUnit.PerPlayer:
                    text = "每人固定";
                    break;
                default:
                    text = "待定";
                    break;
            }
            return text;
        }

        public static string GetStatusText(int statusId)
        {
            string text = string.Empty;
            switch (statusId)
            {
                case (int)EnumFieldStatus.Available :
                    text = "可预订";
                    break;
                case (int)EnumFieldStatus.Booked:
                    text = "已预订";
                    break;
                case (int)EnumFieldStatus.Booking:
                    text = "预订申请中";
                    break;
                case (int)EnumFieldStatus.Canceled:
                    text = "已取消";
                    break;
                case (int)EnumFieldStatus.CheckIn:
                    text = "已签到";
                    break;
                case (int)EnumFieldStatus.CustomerConfirmed:
                    text = "用户已确认";
                    break;
                case (int)EnumFieldStatus.Ending:
                    text = "已结束";
                    break;
                case (int)EnumFieldStatus.Expired:
                    text = "已过期";
                    break;
                case (int)EnumFieldStatus.Void:
                    text = "不可用";
                    break;
                default:
                    text = "未知状态";
                    break;
            }
            return text;
        }
    }
}
