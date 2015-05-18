using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TiQiu.DAL;
using TiQiu.Model;


namespace TiQiu.Biz
{
    public static class GameManager
    {

        public static List<KeyValuePair<int, string>> GetGameList()
        {
            List<KeyValuePair<int, string>> rList = new List<KeyValuePair<int, string>>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                var start = DateTime.Now.AddMonths(-12).Date;
                var rel = context.GAME.Where(g => g.START_DATE >= start).OrderByDescending(o => o.START_DATE).ToList<GAME>();
                rel.ForEach(i => {
                    rList.Add(new KeyValuePair<int, string>(i.ID, i.NAME));
                });
            }
            
            return rList;
        }

        public static List<GAME> GetGameList(int pageIdx, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<GAME> rel = new List<GAME>();
            Expression<Func<GAME, bool>> condition = PredicateExtensionses.True<GAME>();
            condition = condition.And(g => g.STATUS == (int)EnumDataStatus.Normal);
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = PagingQuery<GAME>.GetPagingList(context.GAME.AsQueryable(), condition, "START_DATE", false, pageIdx, pageSize, out totalCount);
            }
            return rel;
        }

        public static List<MemberRank> GetGameToptenz(int gameId,int pageIdx, int pageSize, out int totalCount)
        {
            if (pageIdx <= 0) throw new ApplicationException("分页索引从1开始！");
            List<MemberRank> rList = new List<MemberRank>();
            List<SqlParameter> paras = new List<SqlParameter>();
            var id = new SqlParameter("@gameId", SqlDbType.Int);
            var offset = new SqlParameter("@offset", SqlDbType.Int);
            var pSize = new SqlParameter("@pageSize", SqlDbType.Int);
            var total = new SqlParameter("@total", SqlDbType.Int);
            total.Value = totalCount = 0;
            total.Direction = ParameterDirection.Output;
            id.Value = gameId;
            paras.Add(id);
            offset.Value = (pageIdx - 1) * pageSize;
            pSize.Value = pageSize;
            paras.Add(offset);
            paras.Add(pSize);
            paras.Add(total);
            string sql = @"select pager.GAME_ID as GameID, pager.TEAM_ID as TeamID, pager.MEMBER_ID as MemberID,pager.GOAL,
	m.Name as MemberName,m.NICK_NAME as MemberNickName,
	t.NAME as TeamName,f.Link_URL as Portrait
 FROM (
    Select p.GAME_ID, p.TEAM_ID, p.MEMBER_ID,p.GOAL FROM (
        SELECT   GAME_ID, TEAM_ID, MEMBER_ID, COUNT(TYPE) AS GOAL
        FROM      dbo.GAME_EVENT
        WHERE   (TYPE = 0) AND GAME_ID = @gameId 
        GROUP BY GAME_ID, TEAM_ID, MEMBER_ID ) p ORDER BY p.GOAL DESC
         OFFSET @offset ROW FETCH NEXT @pageSize ROWS ONLY )pager 
inner join MEMBER m on m.ID = pager.MEMBER_ID
inner join TEAM t on t.id = pager.TEAM_ID
left outer join [FILE] f on f.fk_id = pager.member_ID and f.type = 3 ;
SELECT @total = count(0) from (
SELECT  MEMBER_ID
FROM      dbo.GAME_EVENT
WHERE   (TYPE = 0) and GAME_ID = @gameId
GROUP BY TEAM_ID, MEMBER_ID) a;

";
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rList = context.Database.SqlQuery<MemberRank>(sql, paras.ToArray<SqlParameter>()).ToList();
                totalCount = (int)total.Value;
            }

            return rList;
        }

        public static GAME GetGameInfo(int gameId)
        {
            GAME game; 
            using (TIQIUEntities context = new TIQIUEntities())
            {
                game = context.GAME.SingleOrDefault(g => g.ID == gameId);
            }
            if (game == null) throw new ApplicationException("未能加载对应的赛事信息！");
            return game;
        }

        public static List<GAME_ROUND> GetGameRoundList(int gameId)
        {
            List<GAME_ROUND> gameRoundList = new List<GAME_ROUND>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                gameRoundList = context.GAME_ROUND.Include("GAME_ROUND_GROUP").Where(r => r.GAME_ID == gameId).OrderBy(r => r.START_DATE).ToList();
            }
            return gameRoundList;
        }

        public static List<GAME_EVENT> GetGameEventList(int scheduledId)
        {
            List<GAME_EVENT> eventList = new List<GAME_EVENT>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                eventList = context.GAME_EVENT.Where(r => r.SCHEDULED_ID == scheduledId).OrderBy(r=>r.TIMESPAN).ToList();
            }
            return eventList;
        }

        public static List<GAME_EVENT> GetGameEventList(int scheduledId,int lastId)
        {
            List<GAME_EVENT> eventList = new List<GAME_EVENT>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                eventList = context.GAME_EVENT.Where(r => r.SCHEDULED_ID == scheduledId && r.ID > lastId).OrderBy(r => r.TIMESPAN).ToList();
            }
            return eventList;
        }

        public static List<GAME_ROUND_GROUP> GetGameRoundGroupList(int roundId)
        {
            List<GAME_ROUND_GROUP> groupList = new List<GAME_ROUND_GROUP>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                groupList = context.GAME_ROUND_GROUP.Include("GAME_SCHEDULED").Include("GAME_TEAM").Where(r => r.ROUND_ID == roundId).OrderBy(r => r.SORT).ToList();
            }
            return groupList;
        }

        public static List<GAME_SCHEDULED> GetGameScheduledList(int groupId)
        {
            List<GAME_SCHEDULED> groupList = new List<GAME_SCHEDULED>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                groupList = context.GAME_SCHEDULED.Include("GAME_ROUND_GROUP.GAME_ROUND.GAME").Where(r => r.GROUP_ID == groupId).OrderBy(r => r.GAME_DATE).ThenBy(r=>r.START_TIME).ToList();
            }
            return groupList;
        }

        public static List<GAME_TEAM> GetGameTeamList(int groupId)
        {
            List<GAME_TEAM> groupList = new List<GAME_TEAM>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                groupList = context.GAME_TEAM.Where(r => r.GROUP_ID == groupId).OrderByDescending(r => r.RANK).OrderBy(r => r.SCORE).ToList();
            }
            return groupList;
        }

        public static void AddGameEvent(int schId,int teamId,int memberId,int type,int recordId,int min)
        {
            GAME_EVENT ev = new GAME_EVENT()
            {
                SCHEDULED_ID = schId,
                TEAM_ID = teamId,
                PLAYER_NUMBER = 0,
                MEMBER_ID = memberId,
                RECORD_ID = recordId,
                RECORD_DATE = DateTime.Now,
                TIMESPAN = new TimeSpan(0,min,0),
                TYPE = type
            };
            using (TIQIUEntities context = new TIQIUEntities())
            {
                var sch = context.GAME_SCHEDULED.SingleOrDefault(s => s.ID == schId);
                if (sch == null) throw new ApplicationException("未能获取对应比赛场次信息，录入失败！");
                ev.GAME_ID = sch.GAME_ID;
                ev.GROUP_ID = sch.GROUP_ID;
                ev.ROUND_ID = sch.ROUND_ID;
                if (sch.TEAM_A_ID != ev.TEAM_ID && sch.TEAM_B_ID != ev.TEAM_ID) throw new ApplicationException("比赛球队主键错误，录入失败！");
                var team = context.TEAM_MEMBER.Include("MEMBER").SingleOrDefault(t => t.TEAM_ID == teamId && t.MEMBER_ID == memberId);
                if (team == null) throw new ApplicationException("未能获取对应球队信息，录入失败！");
                ev.MEMBER_NAME = team.MEMBER.NAME;
                if (type == (int)EnumGameEventType.Goal)
                {
                    if (sch.TEAM_A_ID == ev.TEAM_ID)
                    {
                        sch.TEAM_A_GOAL = sch.TEAM_A_GOAL.GetValueOrDefault(0) + 1;
                    }
                    else
                    {
                        sch.TEAM_B_GOAL = sch.TEAM_B_GOAL.GetValueOrDefault(0) + 1;
                    }
                }
                //sch.STATUS = (int)EnumGameScheduledStatus.Live;
                context.GAME_EVENT.Add(ev);
                context.SaveChanges();
            }
        }

        public static void DelGameEvent(int evenId)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {
                var ev = context.GAME_EVENT.SingleOrDefault(s => s.ID == evenId);
                if (ev == null) throw new ApplicationException("未能获取对应比赛信息！");
                if (ev.TYPE == (int)EnumGameEventType.Goal)
                {
                    var sch = context.GAME_SCHEDULED.SingleOrDefault(s => s.ID == ev.SCHEDULED_ID);
                    if (sch.TEAM_A_ID == ev.TEAM_ID)
                    {
                        sch.TEAM_A_GOAL = sch.TEAM_A_GOAL.GetValueOrDefault(0) - 1;
                    }
                    else
                    {
                        sch.TEAM_B_GOAL = sch.TEAM_B_GOAL.GetValueOrDefault(0) - 1;
                    }
                }
                context.GAME_EVENT.Remove(ev);               
                context.SaveChanges();
            }
            
        }

        public static void GameOver(int schId)
        {
            //using (TIQIUEntities context = new TIQIUEntities())
            //{
            //    var sch = context.GAME_SCHEDULED.SingleOrDefault(s => s.ID == schId);
            //    if (sch == null) throw new ApplicationException("未能获取对应比赛场次信息，确认失败！");
            //    sch.STATUS = (int)EnumGameScheduledStatus.Over;                             
            //    context.SaveChanges();
            //}
        }

        public static GAME_SCHEDULED GetGameLive(int schId)
        {
            GAME_SCHEDULED rel ;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = context.GAME_SCHEDULED.Include("GAME_EVENT").Include("GAME_ROUND_GROUP.GAME_ROUND.GAME").Single(s => s.ID == schId);
            }
            return rel;
        }

        public static List<GAME_SCHEDULED> GetGameLiveList(DateTime from, DateTime to,int gameId,int status, int pageIdx, int pageSize, out int totalCount)
        {
            if (from < DateTime.Parse("2014-01-01")) from = DateTime.Parse("2014-01-01");
            if (to < DateTime.Parse("2014-01-01") || to < from) to = DateTime.Now.Date;
            to = to.AddDays(1).Date;
            totalCount = 0;
            List<GAME_SCHEDULED> rel = new List<GAME_SCHEDULED>();
            Expression<Func<GAME_SCHEDULED, bool>> condition = PredicateExtensionses.True<GAME_SCHEDULED>();
            condition = condition.And(r => r.GAME_DATE >= from );
            condition = condition.And(r => r.GAME_DATE < to );
            if(gameId>0) condition = condition.And(r => r.GAME_ID == gameId);
            if (status > 0) condition = condition.And(r => r.STATUS == status);
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = PagingQuery<GAME_SCHEDULED>.GetPagingList(context.GAME_SCHEDULED.Include("GAME_EVENT").Include("GAME_ROUND_GROUP.GAME_ROUND.GAME").AsQueryable(), condition, "GAME_DATE", false, pageIdx, pageSize, out totalCount);
            }
            return rel;
        }

        public static void AddGameComment(int schId, int memberId, string comment,string msgId)
        {
            GAME_COMMENT ev = new GAME_COMMENT()
            {
                GAME_SCHEDULED_ID = schId,
                MEMBER_ID = memberId,
                CREATE_DATE = DateTime.Now,
                MSG_ID = msgId,
                COMMENT = comment
            };
            using (TIQIUEntities context = new TIQIUEntities())
            {
                var sch = context.GAME_SCHEDULED.SingleOrDefault(s => s.ID == schId);
                if (sch == null) throw new ApplicationException("未能获取对应比赛场次信息，录入失败！");
                var member = context.MEMBER.SingleOrDefault(m => m.ID == memberId);
                if (member == null) throw new ApplicationException("未能获取评论人会员信息！");
                ev.GAME_ID = sch.GAME_ID;
                ev.MEMBER_NAME = member.NICK_NAME;
                context.GAME_COMMENT.Add(ev);
                context.SaveChanges();
            }
        }

        public static void DelGameComment(int id, int memberId)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {
                var co = context.GAME_COMMENT.SingleOrDefault(s => s.ID == id && s.MEMBER_ID == memberId);
                if (co == null) throw new ApplicationException("对应评论不存在或操作人不是评论人！");
                context.GAME_COMMENT.Remove(co);
                context.SaveChanges();
            }
        }

        public static List<GAME_COMMENT> GetGameCommentList(int schId, int pageIdx, int pageSize, out int totalCount)
        {
            
            totalCount = 0;
            List<GAME_COMMENT> rel = new List<GAME_COMMENT>();
            Expression<Func<GAME_COMMENT, bool>> condition = PredicateExtensionses.True<GAME_COMMENT>();
            condition = condition.And(r => r.GAME_SCHEDULED_ID == schId);
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = PagingQuery<GAME_COMMENT>.GetPagingList(context.GAME_COMMENT.AsQueryable(), condition, "CREATE_DATE", false, pageIdx, pageSize, out totalCount);
            }
            return rel;
        }

        public static List<GAME_COMMENT> GetGameCommentList(int schId, int lastId)
        {

            
            List<GAME_COMMENT> rel = new List<GAME_COMMENT>();
            
            
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = context.GAME_COMMENT.Where(c => c.GAME_SCHEDULED_ID == schId && c.ID > lastId).OrderByDescending(r => r.CREATE_DATE).ToList();
            }
            return rel;
        }
    }
}
