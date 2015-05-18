using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TiQiu.DAL;
using TiQiu.Model;

namespace TiQiu.Biz
{
    public static class TeamManager
    {
        public static List<TEAM> GetTeamByMemberId(int memberId)
        {
            List<TEAM> rel = new List<TEAM>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                List<TEAM_MEMBER> tm = context.TEAM_MEMBER.Include("TEAM").Include("MEMBER").Where(s=>s.MEMBER_ID == memberId && s.TEAM.STATUS == 1).ToList();
                tm.ForEach(t => rel.Add(t.TEAM));
            }
            return rel;
        }

        public static List<TEAM> GetTeamList(Expression<Func<TEAM, bool>> condition, string orderBy, bool ascending, int pageIdx, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<TEAM> rel = new List<TEAM>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = PagingQuery<TEAM>.GetPagingList(context.TEAMs.Include("TEAM_MEMBER").AsQueryable(), condition, orderBy, ascending, pageIdx, pageSize, out totalCount);
            }
            return rel;
        }

        public static List<TEAM> GetTeamList(string namePrefix, string orderBy, bool ascending, int pageIdx, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<TEAM> rel = new List<TEAM>();
            Expression<Func<TEAM, bool>> ex = PredicateExtensionses.True<TEAM>();
            ex = ex.And(s => s.STATUS == 1);
            ex = ex.And(s => s.TYPE == 0); 
            ex = ex.And(s => s.NAME.Contains(namePrefix)); 
            using (TIQIUEntities context = new TIQIUEntities())
            {
                
                rel = PagingQuery<TEAM>.GetPagingList(context.TEAMs.Include("TEAM_MEMBER").Include("TEAM_MEMBER.MEMBER").AsQueryable(), ex, orderBy, ascending, pageIdx, pageSize, out totalCount);
                List<int> keys = new List<int>();
                
            }
            return rel;
        }

        public static List<TEAM> GetMyTeamList(int memberId)
        {
            List<TEAM> rel = new List<TEAM>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                
                var list = context.TEAM_MEMBER.Include("TEAM").Include("MEMBER").Where(t => t.MEMBER_ID == memberId && t.TEAM.STATUS == 1 && t.TEAM.TYPE == 0).OrderByDescending(t=>t.TEAM.BUILD_DATE).ToList();
                list.ForEach(t =>
                    rel.Add(t.TEAM));
                List<int> keys = new List<int>();
                rel.ForEach(f => { keys.Add(f.ID); });
                var items = from item in context.TEAMs.Include("TEAM_MEMBER").Include("TEAM_MEMBER.MEMBER")
                    where keys.Contains(item.ID)
                    select item;
                rel = items.ToList();
                //rel = context.TEAMs.Include("TEAM_MEMBER").Where(t=>t.TEAM_MEMBER.Any(m=>m.MEMBER_ID == memberId)).ToList();
            }
            
            return rel;

        }

        /// <summary>
        /// 根据球队Id获取球队详情
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public static TEAM GetTeamDetailInfo(int teamId)
        {
            TEAM entity = null;
            using (TIQIUEntities context=new TIQIUEntities())
            {
                entity = context.TEAMs.Include("TEAM_MEMBER").Include("TEAM_MEMBER.MEMBER").SingleOrDefault(a => a.ID == teamId);
            }

            return entity;
        }

        public static TEAM CreateTeam(string name, string brief,string featrue,DateTime buildDate, int memberId, int roleId = (int)EnumTeamMemberRole.Creator)
        {
            TEAM curTeam = new TEAM()
            {
                NAME = name,
                BRIEF = brief,
                FEATURE = featrue,
                BUILD_DATE = buildDate.Date,
                STATUS = 1
            };
            TEAM_MEMBER tm = new TEAM_MEMBER()
            {
                MEMBER_ID = memberId,
                ROLE_ID = roleId,
                STATUS = 1
            };
            
            using (TIQIUEntities context = new TIQIUEntities())
            {
                bool exists = context.TEAMs.Any(t => t.NAME == curTeam.NAME);
                if (exists) throw new ApplicationException("相同名称的球队已经存在，请加入球队或另外选择球队名称创建！");

                
                curTeam.TEAM_MEMBER.Add(tm);
                context.TEAMs.Add(curTeam);
                context.SaveChanges();
            }

            return curTeam;

        }

        public static TEAM CreateTeam(TEAM enity)
        {
            TEAM tm = null;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                tm = new TEAM() { NAME = enity.NAME, BRIEF = enity.BRIEF };
                context.TEAMs.Add(tm);
                context.SaveChanges();
            }
            return tm;
        }

        public static void DelTeam(int memberId,int teamId)
        {
            
            using (TIQIUEntities context = new TIQIUEntities())
            {
                TEAM tm = context.TEAMs.Include("TEAM_MEMBER").SingleOrDefault(t => t.ID == teamId && t.STATUS == 1);
                if(tm == null ) throw new ApplicationException("球队ID不正确或已被删除！");
                if(!tm.TEAM_MEMBER.Any(m=>m.MEMBER_ID == memberId && (m.ROLE_ID == (int)EnumTeamMemberRole.Creator || m.ROLE_ID == (int)EnumTeamMemberRole.Leader)))
                    throw new ApplicationException("当前用户不是球队创建者或队长，无法删除！");
                tm.STATUS = 0;
                context.SaveChanges();
            }
        }

        public static TEAM UpdateTeam(int memberId, int teamId, string name, string brief, string feature, DateTime buildDate)
        {
            TEAM curTeam = null;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                curTeam = context.TEAMs.SingleOrDefault(t => t.ID == teamId);
                if (curTeam == null) throw new ApplicationException("请提供正确的球队ID！");
                if (context.TEAM_MEMBER.Count(m => m.TEAM_ID == teamId && m.MEMBER_ID == memberId 
                    && (m.ROLE_ID == (int)EnumTeamMemberRole.Leader || m.ROLE_ID == (int)EnumTeamMemberRole.Creator) ) != 1){
                    throw new ApplicationException("当前会员不属于此球队或没有修改权限！");
                }
                if (name.Trim().Length > 0)
                {
                    if (!context.TEAMs.Any(t => t.ID != teamId && t.NAME == name.Trim())) curTeam.NAME = name;
                    else throw new ApplicationException("拟修改球队名称已存在，修改失败！");
                }
                if (brief.Trim().Length > 0) curTeam.BRIEF = brief;
                if (feature.Trim().Length > 0) curTeam.FEATURE = feature;
                if (buildDate.Year > 1950) curTeam.BUILD_DATE = buildDate;
                               
                context.SaveChanges();
            }

            return curTeam;

        }

        public static TEAM UpdateTeamInfoDetails(TEAM enity)
        {
            TEAM tm = new TEAM();
            using (TIQIUEntities context = new TIQIUEntities())
            {
              
                tm.NAME = enity.NAME;
                tm.BRIEF = enity.BRIEF;
                tm = context.TEAMs.Single<TEAM>(a => a.ID == enity.ID);
                context.SaveChanges();
            }
            return tm;
        }

        public static void JoinTeam(int memberId, int teamId,int? number,string position,int? roleId)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {
                var team = context.TEAMs.SingleOrDefault(t => t.ID == teamId);
                if (team == null) throw new ApplicationException("请提供正确的球队ID！");
                if (team.TEAM_MEMBER.Count(m =>m.TEAM_ID == teamId && m.MEMBER_ID == memberId) > 0) throw new ApplicationException("当前会员已加入此球队！");
                team.TEAM_MEMBER.Add(new TEAM_MEMBER
                {
                    MEMBER_ID = memberId,
                    POSITION = position,
                    TEAM_NUMBER = number.Value,
                    ROLE_ID = roleId.HasValue ? roleId.Value : (int)EnumTeamMemberRole.Member,
                    STATUS = 1                    
                });
                context.SaveChanges();
            }
        }

        public static void FireTeamMember(int memberId,int teamId, int fireMemberId)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {
                var team = context.TEAMs.SingleOrDefault(t => t.ID == teamId);
                if (team == null) throw new ApplicationException("请提供正确的球队ID！");
                if (!team.TEAM_MEMBER.Any(m => m.TEAM_ID == teamId && m.MEMBER_ID == memberId && 
                    (m.ROLE_ID == (int)EnumTeamMemberRole.Creator || m.ROLE_ID == (int)EnumTeamMemberRole.Leader)))
                    throw new ApplicationException("只有球队创建者或者队长能够进行此操作！");

                var fire = context.TEAM_MEMBER.SingleOrDefault(t => t.TEAM_ID == teamId && t.MEMBER_ID == fireMemberId);
                if (fire == null) throw new ApplicationException("此会员已不属于此球队！");
                if (fire.ROLE_ID == (int)EnumTeamMemberRole.Creator) throw new ApplicationException("球队创建者不能删除！");
                context.TEAM_MEMBER.Remove(fire);
                context.SaveChanges();
            }
        }

        public static List<TEAM> GetteamListByTeamName(Expression<Func<TEAM, bool>> condition, string orderBy, bool ascending, int pageIdx, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<TEAM> rel = new List<TEAM>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = PagingQuery<TEAM>.GetPagingList(context.TEAMs.AsQueryable(), condition, orderBy, ascending, pageIdx, pageSize, out totalCount);

            }
            return rel;
        }

        public static List<TEAM_SCORE> GetTeamScoreList(int teamId, string orderBy, bool ascending,DateTime? start,DateTime? end, int pageIdx, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<TEAM_SCORE> rel = new List<TEAM_SCORE>();

            Expression<Func<TEAM_SCORE, bool>> ex = PredicateExtensionses.True<TEAM_SCORE>();
            ex.And(s => s.TEAM_A_ID == teamId);
            if (start.HasValue && start > new DateTime(2014, 1, 1))
            {
                start = start.Value.Date;
                ex = ex.And(s => s.ORDER_DATE >= start);
            }
            if (end.HasValue && end > new DateTime(2014, 1, 1))
            {
                end = end.Value.AddDays(1).Date;
                ex = ex.And(s => s.ORDER_DATE < end);
            }
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = PagingQuery<TEAM_SCORE>.GetPagingList(context.TEAM_SCORE.AsQueryable(), ex, orderBy, ascending, pageIdx, pageSize, out totalCount);

            }
            return rel;
        }

    }
}
