using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TiQiu.DAL;
using System.Configuration;

using System.Data;
using System.Data.SqlClient;
using TiQiu.Common.Util;
using TiQiu.Model;

namespace TiQiu.Biz
{
    public static class OrderManager
    {

        public static List<FIELD_ORDER_LOG> GetOrderLog(DateTime stamp, List<int> list)
        {
            List<FIELD_ORDER_LOG> logs = new List<FIELD_ORDER_LOG>();
            DateTime now = DateTime.Now.AddDays(-3).Date;
            int[] status = new int[] { (int)EnumFieldStatus.Booking, (int)EnumFieldStatus.Canceled, (int)EnumFieldStatus.CustomerConfirmed };
            using (TIQIUEntities context = new TIQIUEntities())
            {
                logs = context.FIELD_ORDER_LOG.Include("FIELD_ORDER")
                    .Where(l =>

                        l.LOG_DATE >= now && list.Contains(l.FIELD_ORDER.FIELD_ID) && l.MEMBER_ID.Value > 0 && !l.ACCOUNT_B_ID.HasValue
                        &&  status.Contains(l.FIELD_ORDER.STATUS )
                        ).ToList();
            }
            return logs;
        }

        public static void CheckMessage(int msgId)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {
                var log = context.FIELD_ORDER_LOG.SingleOrDefault(f => f.ID == msgId);
                if (log == null) throw new ApplicationException("请提供正确的MessageID！");
                //log.MESSAGE_STATUS = 1;
                context.SaveChanges();
            }
        }

        public static OrderView GetOrderByID(int orderId)
        {
            OrderView rel;
            List<SqlParameter> paras = new List<SqlParameter>();
            var id = new SqlParameter("@orderId", SqlDbType.Int);
            id.Value = orderId;
            paras.Add(id);
            string sql = @"SELECT O.[ID]
      ,O.[FIELD_SCHEDULED_ID] AS ScheduledId
      ,O.[FIELD_ID] AS FieldId
      ,O.[FIELD_ITEM_ID] AS FieldItemId
      ,O.[ORDER_DATE] AS OrderDate
      ,O.[NEED_REFEREE] AS NeedReferee
      ,O.[STATUS] AS [Status]
      ,O.[START_TIME] AS Start
      ,O.[END_TIME] AS [End]
      ,O.[MEMBER_ID] AS MemberId
      ,O.[CREATE_DATE] AS CreateDate
      ,O.[TYPE] AS [Type]
      ,O.[PK_PAY_TYPE] AS PKPayType
      ,O.[INCOME] AS Payment
      ,O.[REMARK] AS Remark
      ,O.[MEMBERB_ID] AS MemberBId
      ,O.[PRICE] AS Price
      ,O.[PRICE_UNIT] AS FreeTeamPriceUnit
      ,O.[FREE_TEAM_MIN_PLAYER] AS FreeTeamMinPlayer
	  ,f.NAME AS FieldName
	  ,i.NAME AS FieldItemName
	  ,i.[TYPE] AS FieldType
	  ,m.Name AS MemberName
      ,m.CELLPHONE AS MemberPhone
      ,m1.Name AS MemberBName
      ,m1.CELLPHONE AS PhoneB
	  ,e.[PATH] AS FieldLogoUrl
	  
  FROM [dbo].[FIELD_ORDER] O 
        INNER JOIN  dbo.FIELD AS f ON f.ID = o.FIELD_ID 
        INNER JOIN dbo.FIELD_ITEM AS i ON i.ID = o.FIELD_ITEM_ID 
        LEFT OUTER JOIN dbo.MEMBER AS m ON m.ID = o.MEMBER_ID 
		LEFT OUTER JOIN dbo.MEMBER AS m1 ON m.ID = o.MEMBERB_ID
		LEFT OUTER JOIN dbo.[FILE] e ON e.TYPE = 6 AND e.FK_ID = o.ID
WHERE O.ID = @orderId;";
            
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = context.Database.SqlQuery<OrderView>(sql, paras.ToArray<SqlParameter>()).Single();
                var teamScore = context.TEAM_SCORE.SingleOrDefault(t => t.FIELD_ORDER_ID == orderId);
                if (teamScore != null)
                {
                    rel.TeamId = teamScore.TEAM_A_ID;
                    rel.TeamName = teamScore.TEAM_A_NAME;
                    rel.TeamBId = teamScore.TEAM_B_ID;
                    rel.TeamBName = teamScore.TEAM_B_NAME;
                }
                if (rel.Type == (int)EnumOrderType.FreeTeam)
                {
                    List<SqlParameter> teamParas = new List<SqlParameter>();
                    var oid = new SqlParameter("@orderId", SqlDbType.Int);
                    oid.Value = orderId;
                    teamParas.Add(oid);
                    var totalPlyaer = new SqlParameter("@totalPalyer", SqlDbType.Int);
                    totalPlyaer.Direction = ParameterDirection.Output;
                    totalPlyaer.Value = 0;
                    teamParas.Add(totalPlyaer);
                    string freeTeamLogSql = @"SELECT 
              L.[MEMBER_ID] AS MemberId
              ,L.[CHECKIN_DATE] AS JoinDate
              ,L.[PLAYER_COUNT] AS JoinPlayerCount
              ,M.NAME AS MemberName
          FROM [dbo].[FREE_TEAM_LOG] L
	        INNER JOIN dbo.MEMBER M
		        ON M.ID = L.MEMBER_ID 
            WHERE FIELD_ORDER_ID = @orderId;
                SELECT @totalPalyer = SUM(PLAYER_COUNT) FROM [dbo].[FREE_TEAM_LOG]  WHERE FIELD_ORDER_ID = @orderId;";
                    rel.FreeTeamJoinLog = context.Database.SqlQuery<FreeTeamLog>(freeTeamLogSql, teamParas.ToArray<SqlParameter>()).ToList();
                    if (rel.FreeTeamJoinLog.Count == 0)
                    {
                        rel.FreeTeamPlayer = 0;
                    }else
                    {
                        rel.FreeTeamPlayer = (int)totalPlyaer.Value;
                    }
                }
                
            }
            return rel;
        }
        public static List<FIELD_ORDER> GetOrderList(int fieldItemId, DateTime startDay, DateTime endDay)
        {
            List<FIELD_ORDER> rel = new List<FIELD_ORDER>();
            endDay = endDay.AddDays(1);
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = context.FIELD_ORDER.Include("TEAM_SCORE").Where(f => f.FIELD_ITEM_ID == fieldItemId && f.ORDER_DATE >= startDay.Date && f.ORDER_DATE <= endDay.Date).OrderBy(f=>f.START_TIME).ToList();
            }
            return rel;
        }
        public static List<FIELD_ORDER> GetOrderList(Expression<Func<FIELD_ORDER, bool>> condition, string orderBy, bool ascending, int pageIdx, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<FIELD_ORDER> rel = new List<FIELD_ORDER>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = PagingQuery<FIELD_ORDER>.GetPagingList(context.FIELD_ORDER.Include("TEAM_SCORE").AsQueryable(), condition, orderBy, ascending, pageIdx, pageSize, out totalCount);
            }
            return rel;
        }

        public static List<FIELD_ORDER> GetOrderList(int businessesId,  int pageIdx, int pageSize, out int totalCount,string orderBy = "CREATE_DATE", bool ascending = true)
        {
            totalCount = 0;
            List<FIELD_ORDER> rel = new List<FIELD_ORDER>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                List<FIELD> fs = context.FIELDs.Where(f => f.BUSINESSES_ID == businessesId).ToList();
                Expression<Func<FIELD_ORDER, bool>> condition = PredicateExtensionses.True<FIELD_ORDER>();
                fs.ForEach(f => condition = condition.Or(o => o.FIELD_ID == f.ID));
                
                rel = PagingQuery<FIELD_ORDER>.GetPagingList(context.FIELD_ORDER.AsQueryable(), condition, orderBy, ascending, pageIdx, pageSize, out totalCount);
            }
            return rel;
        }

        public static List<V_FIELD_ORDER> GetOrderViewList(Expression<Func<V_FIELD_ORDER, bool>> condition, string orderBy, bool ascending, int pageIdx, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<V_FIELD_ORDER> rel = new List<V_FIELD_ORDER>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = PagingQuery<V_FIELD_ORDER>.GetPagingList(context.V_FIELD_ORDER.AsQueryable(), condition, orderBy, ascending, pageIdx, pageSize, out totalCount);
            }
            return rel;
        }

        public static List<FieldScheduledInfo> GetFieldScheduledInfoList(int fieldItemId, DateTime from, DateTime to, int accountId)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {
                var ab = context.ACCOUNT_B_BUSINESSES.FirstOrDefault<ACCOUNT_B_BUSINESSES>(b => b.ACCOUNT_B_ID == accountId);
                if (ab == null) throw new ApplicationException("当前用户不属于任何商家！");
                var item = context.FIELD_ITEM.FirstOrDefault<FIELD_ITEM>(f => f.ID == fieldItemId);
                if (item == null) throw new ApplicationException("场地ID不存在!");
                if (item.BUSINESSES_ID != ab.BUSINESSESS_ID) throw new ApplicationException("当前用户没有权限管理此场地！");

            }
            return GetFieldScheduledInfoList(fieldItemId, from, to);
        }

        public static List<FieldScheduledInfo> GetFieldScheduledInfoList(int fieldItemId, DateTime from, DateTime to)
        {
            List<FieldScheduledInfo> list = new List<FieldScheduledInfo>();
            List<SqlParameter> paras = new List<SqlParameter>();
            var id = new SqlParameter("@fieldItemId", SqlDbType.Int);
            id.Value = fieldItemId;
            paras.Add(id);
            var fromDate = new SqlParameter("@from", SqlDbType.DateTime);
            fromDate.Value = from.Date;
            paras.Add(fromDate);
            var toDate = new SqlParameter("@to", SqlDbType.DateTime);
            
            toDate.Value = to.AddDays(1).Date;
            paras.Add(toDate);
            using (TIQIUEntities context = new TIQIUEntities())
            {
#region sql
                string sql = @"
select s.id as ScheduledID
    ,s.Field_ID as FieldID
	,s.Field_Item_ID as FileItemID
    ,s.Status
	,s.Scheduled_Date as ScheduledDate
	,s.Start_Time as StartTime
	,s.End_Time as EndTime
	,s.Price
	,s.Remark as ScheduledRemark
	,o.OrderID
	,o.NeedReferee
	,o.OrderStatus
	,o.MemberID
	,o.OrderCreateDate
	,o.OrderType
	,o.PkPayType
	,o.MemberBID
	,o.SoloMinPlayer
	,o.OrderRemark
	,o.OrderPrice
    ,o.Income
	,o.PriceUnit
	,o.MemberName
    ,o.MemberPhone
	,o.MemberBName
    ,o.MemberBPhone
    ,o.TeamID
    ,o.TeamBID
	,o.TeamName
	,o.TeamBName
	,o.TeamColor
	,o.TeamBColor
from field_scheduled s 
left join 
	(select 
		f.field_scheduled_id
		,f.id as OrderId
		,f.Need_Referee as NeedReferee
		,f.Status as OrderStatus
		,f.Member_ID as MemberId
		,f.create_date as OrderCreateDate
		,f.Type AS OrderType
		,f.PK_Pay_Type as PkPayType
		,f.MemberB_ID as MemberBId
		,f.Free_Team_Min_Player as SoloMinPlayer
		,f.Remark AS OrderRemark
        ,f.INCOME AS InCome
		,f.price as OrderPrice
		,f.Price_Unit as PriceUnit
		,m.Name as MemberName
        ,m.CELLPHONE AS MemberPhone
        ,m1.Name as MemberBName
        ,m1.CELLPHONE AS MemberBPhone
		,c.team_a_id as TeamID,c.team_b_id as TeamBID,c.team_a_name as TeamName,c.team_b_name as TeamBName
		,c.team_a_color as TeamColor,c.team_b_name as TeamBColor
		from field_order f
		left outer join member m
			on m.id = f.member_id
		left outer join member m1
			on m1.id = f.memberb_id
		left outer join team_score c
			on c.field_order_id = f.id
	where f.field_item_id = @fieldItemId and  f.order_date >= @from and f.order_date < @to and f.status > -1 and f.status < 60
		) o
	on o.field_scheduled_id = s.id
where s.field_item_id = @fieldItemId and  s.scheduled_date >= @from and s.scheduled_date <@to
order by s.scheduled_date,s.start_time
;";
                string ftlSql = @"
            select f.id as OrderID, l.Member_ID as MemberID,m.Name as MemberName,m.CELLPHONE AS MemberPhone
	,l.CheckIn_Date as JoinDate,l.Player_count as CountMember 
	from field_order f 
	inner join free_team_log l
		on l.field_order_id = f.id
	inner join member m
		on m.id = l.member_id
where  f.field_item_id = @fieldItemId and  f.order_date >= @from and f.order_date < @to and f.status > -1 and f.status < 60
order by l.CheckIn_Date desc;
";
#endregion
                SqlParameter[] clonedParameters = new SqlParameter[paras.Count];
                for (int i = 0, j = paras.Count; i < j; i++)
                {
                    clonedParameters[i] = (SqlParameter)((ICloneable)paras[i]).Clone();
                }
                list = context.Database.SqlQuery<FieldScheduledInfo>(sql, paras.ToArray<SqlParameter>()).ToList();
                
                List<SoloLog> ftl = context.Database.SqlQuery<SoloLog>(ftlSql, clonedParameters).ToList();

                ftl.ForEach(f => {
                    var order = list.SingleOrDefault(o => o.OrderID.GetValueOrDefault(0) == f.OrderID); 
                    
                    if(order != null){
                        if (order.SoloLogList == null) order.SoloLogList = new List<SoloLog>();
                        order.SoloLogList.Add(f);
                        order.SoloPalyerCount += f.CountMember; 
                    }
                });
                

            }
            return list;
        }

        public static List<MyOrderView> GetMyOrderList(int memberId, DateTime? start, DateTime? end, int pageIdx, int pageSize, out int totalCount)
        {
            if (pageIdx <= 0) throw new ApplicationException("分页索引从1开始！");
            List<MyOrderView> rList = new List<MyOrderView>();
            List<SqlParameter> paras = new List<SqlParameter>();
            var id = new SqlParameter("@memberId", SqlDbType.Int);
            var fromDate = new SqlParameter("@from", SqlDbType.DateTime);
            var toDate = new SqlParameter("@to", SqlDbType.DateTime);
            var offset = new SqlParameter("@offset", SqlDbType.Int);
            var pSize = new SqlParameter("@pageSize", SqlDbType.Int);
            var total = new SqlParameter("@total", SqlDbType.Int);
            total.Value = totalCount = 0;
            total.Direction = ParameterDirection.Output;
            id.Value = memberId;
            paras.Add(id);
            if (start.HasValue && start > new DateTime(2014, 1, 1))
            {
                fromDate.Value = start.Value.Date;
            }
            else
            {
                fromDate.Value = new DateTime(2014, 1, 1);
            }
            if (end.HasValue && end > new DateTime(2014, 1, 1))
            {
                toDate.Value = end.Value.AddDays(1).Date;
            }
            else
            {
                toDate.Value = DateTime.Now.AddDays(1).Date;
            }
            paras.Add(fromDate);
            paras.Add(toDate);
            offset.Value = (pageIdx - 1) * pageSize;
            pSize.Value = pageSize;
            paras.Add(offset);
            paras.Add(pSize);
            paras.Add(total);
            
            string sql = @"SELECT  o.[ID]
      ,o.[ORDER_DATE] AS OrderDate
      ,o.[STATUS] AS [Status]
      ,o.[START_TIME] AS Start
      ,o.[END_TIME] AS [End]
      ,o.[CREATE_DATE] AS CreateDate
      ,o.[TYPE] AS [Type]
      ,o.[INCOME] AS Payment
      ,o.[PRICE] AS Price
	  ,o.MEMBER_ID AS MemberId
	  ,f.NAME AS FieldName
	  ,i.NAME AS FieldItemName
	  ,i.[TYPE] AS FieldType
	  ,e.[PATH] AS FieldLogoUrl
 FROM (
	SELECT ID FROM (
		SELECT ID,ORDER_DATE,START_TIME FROM DBO.FIELD_ORDER WHERE TYPE = 0 AND ORDER_DATE >= @from AND ORDER_DATE <= @to AND  STATUS > 0 AND STATUS < 50 AND  MEMBER_ID = @memberId 
		UNION ALL
		SELECT ID,ORDER_DATE,START_TIME FROM DBO.FIELD_ORDER WHERE TYPE = 1 AND ORDER_DATE >= @from AND ORDER_DATE <= @to AND  STATUS > 0 AND STATUS < 50 AND (MEMBER_ID = @memberId OR MEMBERB_ID = @memberId	)
		UNION ALL 
		SELECT O.ID,O.ORDER_DATE,O.START_TIME FROM DBO.FIELD_ORDER O 
				INNER JOIN DBO.FREE_TEAM_LOG L 
				ON O.ID = L.FIELD_ORDER_ID
		   WHERE L.MEMBER_ID = @memberId AND O.TYPE = 2 AND O.ORDER_DATE >= @from AND O.ORDER_DATE <= @to AND O.STATUS > 0 AND O.STATUS < 50 
		   ) F ORDER BY F.ORDER_DATE DESC,START_TIME DESC OFFSET @offset ROW FETCH NEXT @pageSize ROWS ONLY) Pager
		   INNER JOIN dbo.FIELD_ORDER o ON o.ID = Pager.ID
		   INNER JOIN dbo.FIELD AS f ON f.ID = o.FIELD_ID 
		   INNER JOIN dbo.FIELD_ITEM AS i ON i.ID = o.FIELD_ITEM_ID 
		   LEFT OUTER JOIN dbo.[FILE] e ON e.TYPE = 6 AND e.FK_ID = o.ID; 
    SELECT @total = COUNT(0) FROM (
		SELECT ID,ORDER_DATE,START_TIME FROM DBO.FIELD_ORDER WHERE TYPE = 0 AND ORDER_DATE >= @from AND ORDER_DATE <= @to AND  STATUS > 0 AND STATUS < 50 AND  MEMBER_ID = @memberId 
		UNION ALL
		SELECT ID,ORDER_DATE,START_TIME FROM DBO.FIELD_ORDER WHERE TYPE = 1 AND ORDER_DATE >= @from AND ORDER_DATE <= @to AND  STATUS > 0 AND STATUS < 50 AND (MEMBER_ID = @memberId OR MEMBERB_ID = @memberId	)
		UNION ALL 
		SELECT O.ID,O.ORDER_DATE,O.START_TIME FROM DBO.FIELD_ORDER O 
				INNER JOIN DBO.FREE_TEAM_LOG L 
				ON O.ID = L.FIELD_ORDER_ID
		   WHERE L.MEMBER_ID = @memberId AND O.TYPE = 2 AND O.ORDER_DATE >= @from AND O.ORDER_DATE <= @to AND O.STATUS > 0 AND O.STATUS < 50 
		   ) F;";
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rList = context.Database.SqlQuery<MyOrderView>(sql, paras.ToArray<SqlParameter>()).ToList();
                totalCount = (int)total.Value;
            }

            return rList;
        }

        public static List<OrderView> GetOrderList(List<int> fieldIds,EnumFieldStatus orderStatus, DateTime? start, DateTime? end, int pageIdx, int pageSize, out int totalCount)
        {
            if (pageIdx <= 0) throw new ApplicationException("分页索引从1开始！");
            string fieldIdString = string.Join(",",fieldIds);
            List<OrderView> rList = new List<OrderView>();
            List<SqlParameter> paras = new List<SqlParameter>();
            var status = new SqlParameter("@status", SqlDbType.Int);
            var fromDate = new SqlParameter("@from", SqlDbType.DateTime);
            var toDate = new SqlParameter("@to", SqlDbType.DateTime);
            var offset = new SqlParameter("@offset", SqlDbType.Int);
            var pSize = new SqlParameter("@pageSize", SqlDbType.Int);
            var total = new SqlParameter("@total", SqlDbType.Int);
            total.Value = totalCount = 0;
            total.Direction = ParameterDirection.Output;
            status.Value = orderStatus;
            paras.Add(status);
            if (start.HasValue && start > new DateTime(2014, 1, 1))
            {
                fromDate.Value = start.Value.Date;
            }
            else
            {
                fromDate.Value = DateTime.Now.Date;
            }
            if (end.HasValue && end > new DateTime(2014, 1, 1))
            {
                toDate.Value = end.Value.AddDays(1).Date;
            }
            else
            {
                toDate.Value = new DateTime(2024, 1, 1);
            }
            paras.Add(fromDate);
            paras.Add(toDate);
            offset.Value = (pageIdx - 1) * pageSize;
            pSize.Value = pageSize;
            paras.Add(offset);
            paras.Add(pSize);
            paras.Add(total);

            string sql = @"SELECT O.[ID]
              ,O.[FIELD_SCHEDULED_ID] AS ScheduledId
              ,O.[FIELD_ID] AS FieldId
              ,O.[FIELD_ITEM_ID] AS FieldItemId
              ,O.[ORDER_DATE] AS OrderDate
              ,O.[NEED_REFEREE] AS NeedReferee
              ,O.[STATUS] AS [Status]
              ,O.[START_TIME] AS Start
              ,O.[END_TIME] AS [End]
              ,O.[MEMBER_ID] AS MemberId
              ,O.[CREATE_DATE] AS CreateDate
              ,O.[TYPE] AS [Type]
              ,O.[PK_PAY_TYPE] AS PKPayType
              ,O.[INCOME] AS Payment
              ,O.[REMARK] AS Remark
              ,O.[MEMBERB_ID] AS MemberBId
              ,O.[PRICE] AS Price
              ,O.[PRICE_UNIT] AS FreeTeamPriceUnit
              ,O.[FREE_TEAM_MIN_PLAYER] AS FreeTeamMinPlayer
	          ,f.NAME AS FieldName
	          ,i.NAME AS FieldItemName
	          ,i.[TYPE] AS FieldType
	          ,m.Name AS MemberName
              ,m.CELLPHONE AS MemberPhone
              ,m1.Name AS MemberBName
              ,m1.CELLPHONE AS PhoneB
	          ,e.[PATH] AS FieldLogoUrl
	  
          FROM [dbo].[FIELD_ORDER] O 
                INNER JOIN  dbo.FIELD AS f ON f.ID = o.FIELD_ID 
                INNER JOIN dbo.FIELD_ITEM AS i ON i.ID = o.FIELD_ITEM_ID 
                LEFT OUTER JOIN dbo.MEMBER AS m ON m.ID = o.MEMBER_ID 
		        LEFT OUTER JOIN dbo.MEMBER AS m1 ON m.ID = o.MEMBERB_ID
		        LEFT OUTER JOIN dbo.[FILE] e ON e.TYPE = 6 AND e.FK_ID = o.ID
        WHERE O.FIELD_ID in ({0}) AND O.ORDER_DATE >= @from AND O.ORDER_DATE <= @to AND O.STATUS =@status
            ORDER BY O.CREATE_DATE DESC,START_TIME DESC OFFSET @offset ROW FETCH NEXT @pageSize ROWS ONLY;
        SELECT @total = Count(0) 
            FROM [dbo].[FIELD_ORDER] O 
            INNER JOIN  dbo.FIELD AS f ON f.ID = o.FIELD_ID 
            INNER JOIN dbo.FIELD_ITEM AS i ON i.ID = o.FIELD_ITEM_ID 
        WHERE O.FIELD_ID in ({0}) AND O.ORDER_DATE >= @from AND O.ORDER_DATE <= @to AND O.STATUS =@status;
";
            sql = string.Format(sql, fieldIdString);
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rList = context.Database.SqlQuery<OrderView>(sql, paras.ToArray<SqlParameter>()).ToList();
                totalCount = (int)total.Value;
                List<int> orderIds = new List<int>();
                rList.ForEach(o => {
                    if (o.Type == (int)EnumOrderType.FreeTeam)
                    {
                        orderIds.Add(o.ID);                        
                    }
                });

                if (orderIds.Count > 0)
                {
                    string freeTeamLogSql = string.Format(@"
                    SELECT 
                       L.FIELD_ORDER_ID AS OrderId 
                      ,L.[MEMBER_ID] AS MemberId
                      ,L.[CHECKIN_DATE] AS JoinDate
                      ,L.[PLAYER_COUNT] AS JoinPlayerCount
                      ,M.NAME AS MemberName
                  FROM [dbo].[FREE_TEAM_LOG] L
	                INNER JOIN dbo.MEMBER M
		                ON M.ID = L.MEMBER_ID 
                    WHERE FIELD_ORDER_ID in ({0});",string.Join(",",orderIds));
                    List<FreeTeamLog> freeTeamJoinLog = context.Database.SqlQuery<FreeTeamLog>(freeTeamLogSql, null).ToList();
                    orderIds.ForEach(o => {
                        var item = rList.First(r => r.ID == o);
                        item.FreeTeamJoinLog = freeTeamJoinLog.Where(f => f.OrderId == o).ToList();
                        item.FreeTeamPlayer = 0;
                        item.FreeTeamJoinLog.ForEach(i => { item.FreeTeamPlayer += i.JoinPlayerCount; });
                    
                    });
                    
                }
            }

            return rList;
        }


        public static List<V_FIELD_ORDER> GetOrderHistoryList(int memberId,DateTime? start,DateTime? end, int pageIdx, int pageSize, out int totalCount)
        {
            totalCount = 0;
            Expression<Func<V_FIELD_ORDER, bool>> ex = PredicateExtensionses.True<V_FIELD_ORDER>();
            ex = ex.And(s => s.MEMBER_ID == memberId );
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
            List<V_FIELD_ORDER> rel = new List<V_FIELD_ORDER>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = PagingQuery<V_FIELD_ORDER>.GetPagingList(context.V_FIELD_ORDER.AsQueryable(), ex, "ORDER_DATE", false, pageIdx, pageSize, out totalCount);
            }
            return rel;
        }

        public static List<VIEW_ORDER_PK> GetOrderPKList(EnumFieldStatus status,string areaCode,int fieldType,DateTime? start,DateTime? end,int pkType, int pageIdx, int pageSize, out int totalCount)
        {
            totalCount = 0;
            Expression<Func<VIEW_ORDER_PK, bool>> ex = PredicateExtensionses.True<VIEW_ORDER_PK>();
            ex = ex.And(s => s.STATUS == (int)status);
            if (pkType == 1)
            {
                ex = ex.And(s => s.TEAM_B_ID == null || s.TEAM_B_ID.Value == 0);
            }else if (pkType == 2)
            {
                ex = ex.And(s => s.TEAM_B_ID.HasValue && s.TEAM_B_ID.Value > 0);
            }
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
            if (areaCode != null && areaCode != "0")
            {
                ex = ex.And(s => s.AREA_CODE == areaCode);
            }
            if (fieldType > 0)
            {
                ex = ex.And(s => s.FIELD_ITEM_TYPE == fieldType);
            }
            List<VIEW_ORDER_PK> rel = new List<VIEW_ORDER_PK>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = PagingQuery<VIEW_ORDER_PK>.GetPagingList(context.VIEW_ORDER_PK.AsQueryable(), ex, "ORDER_DATE", false, pageIdx, pageSize, out totalCount);
            }
            return rel;
        }

        public static List<VIEW_ORDER_FREETEAM> GetOrderFreeTeamList(EnumFieldStatus status, string areaCode, int fieldType, DateTime? start, DateTime? end, int pageIdx, int pageSize, out int totalCount)
        {
            totalCount = 0;
            Expression<Func<VIEW_ORDER_FREETEAM, bool>> ex = PredicateExtensionses.True<VIEW_ORDER_FREETEAM>();
            ex = ex.And(s => s.STATUS == (int)status);
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
            if (areaCode != null  && areaCode != "0")
            {
                ex = ex.And(s => s.AREA_CODE == areaCode);
            }
            if ( fieldType > 0)
            {
                ex = ex.And(s => s.FIELD_ITEM_TYPE == fieldType);
            }
            List<VIEW_ORDER_FREETEAM> rel = new List<VIEW_ORDER_FREETEAM>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = PagingQuery<VIEW_ORDER_FREETEAM>.GetPagingList(context.VIEW_ORDER_FREETEAM.AsQueryable(), ex, "ORDER_DATE", false, pageIdx, pageSize, out totalCount);
            }
            return rel;
        }

        //public static List<field_order> GetOrderList(int fieldItemId, DateTime startDay, DateTime endDay)
        //{
        //    List<field_order> rel = new List<field_order>();
        //    Expression<Func<field_order, bool>> ex = PredicateExtensionses.True<field_order>();
        //    ex.And(s => s.FIELD_ITEM_ID == fieldItemId && s.ORDER_DATE >= startDay.Date && s.ORDER_DATE < endDay.AddDays(1).Date);
        //    using (TIQIUEntities context = new TIQIUEntities())
        //    {
        //        rel = context.FIELD_ORDER.Where(ex).OrderBy(s => s.ORDER_DATE).ThenBy(s => s.START_TIME).ToList();
        //    }
        //    return rel;
        //}

        public static TEAM_SCORE GetTeamScore(int orderId)
        {
            TEAM_SCORE tm = null;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                tm = context.TEAM_SCORE.Single(t => t.FIELD_ORDER_ID == orderId);
            }
            return tm;
        }


        public static void OrderNormalByManager(int scheduledId, int accountBId, string phone,string name,bool needReferee)
        {

            if (!Tools.IsMobile(phone)) throw new ApplicationException("请检查电话号码！");
            List<MEMBER> mList = MemberManager.GetMemberByPhone(phone);
            MEMBER curMember = null;
            if (mList.Count == 1)
            {
                curMember = mList[0];
            }
            else
            {
                name = name.Length == 0 ?  phone : name;
                curMember = MemberManager.CreateMember(name, phone);

            }
            OrderNormal(scheduledId, curMember.ID, needReferee, 0, accountBId);
        }

        public static void OrderBatchByManager(int scheduledId, int accountBId,DateTime start,DateTime end, string phone, string name,int? teamId,string remark)
        {

            if (!Tools.IsMobile(phone)) throw new ApplicationException("请检查电话号码！");
            List<MEMBER> mList = MemberManager.GetMemberByPhone(phone);
            MEMBER curMember = null;
            if (mList.Count == 1)
            {
                curMember = mList[0];
            }
            else
            {
                name = name.Length == 0 ? phone : name;
                curMember = MemberManager.CreateMember(name, phone);

            }
            using (TIQIUEntities context = new TIQIUEntities())
            {
                var scheduled = context.FIELD_SCHEDULED.SingleOrDefault(s => s.ID == scheduledId);
                if (scheduled == null) throw new ApplicationException("请提供正确的场次ID");
                int dayOfWeek = (int)scheduled.SCHEDULED_DATE.DayOfWeek;
                if (dayOfWeek == 0) dayOfWeek = 7;
                FIELD_ORDER_BATCH fb = new FIELD_ORDER_BATCH
                {
                    MEMBER_ID = curMember.ID,
                    ACCOUNT_B_ID = accountBId,
                    SCHEDULED_ID = scheduledId,
                    START_DATE = start,
                    END_DATE = end,
                    START_TIME = scheduled.START_TIME,
                    END_TIME = scheduled.END_TIME,
                    PRICE = scheduled.PRICE,
                    REMARK = remark,
                    FIELD_ID = scheduled.FIELD_ID,
                    FIELD_ITEM_ID = scheduled.FIELD_ITEM_ID,
                    CREATE_DATE = DateTime.Now,
                    DAY_OF_WEEK = dayOfWeek,
                    STATUS = 1
                };
                if (teamId.HasValue) fb.TEMA_ID = teamId.Value;
                List<FIELD_ORDER> orderList = new List<FIELD_ORDER>();
                List<FIELD_ORDER_LOG> logList = new List<FIELD_ORDER_LOG>();
                var schList = context.FIELD_SCHEDULED.Where(s => s.FIELD_ITEM_ID == fb.FIELD_ITEM_ID
                    && s.SCHEDULED_DATE >= fb.START_DATE.Date && s.SCHEDULED_DATE < fb.END_DATE.AddDays(1).Date
                    && s.START_TIME == fb.START_TIME && s.END_TIME == fb.END_TIME).ToList();
                schList.ForEach(item => {
                    if (item.SCHEDULED_DATE.DayOfWeek == scheduled.SCHEDULED_DATE.DayOfWeek)
                    {
                        if (item.STATUS != (int)EnumFieldStatus.Available)
                        {
                            throw new ApplicationException(string.Format("批量预订失败，{0}的对应场次{1}无法预订，请先调整对应场次状态！",item.SCHEDULED_DATE.ToString("yyyy-MM-dd"),item.START_TIME.ToString("hh:mm")+"-"+item.END_TIME.ToString("hh:mm")));
                        }
                        var order = BuildFieldOrderBase(fb.CREATE_DATE, item.ID, curMember.ID, 0, EnumOrderType.Normal, EnumFieldStatus.Booked, false,remark);
                        FIELD_ORDER_LOG fol = new FIELD_ORDER_LOG()
                        {
                            LOG_DATE = fb.CREATE_DATE,
                            MEMBER_ID = curMember.ID,
                            ACCOUNT_B_ID = accountBId,
                            FIELD_ITEM_ID = fb.FIELD_ITEM_ID,
                            OPERATION = "管理员代客户发起批量预订申请"
                        };
                        order.FIELD_ORDER_LOG.Add(fol);
                        orderList.Add(order);
                    }
                });
                if (orderList.Count == 0) throw new ApplicationException("批量预订失败，没有任何符合条件的场次预订完成！");

                context.FIELD_ORDER_BATCH.Add(fb);
                context.FIELD_ORDER.AddRange(orderList);
                context.SaveChanges();
            }
            
            //OrderNormal(scheduledId, curMember.ID, false, 0, accountBId);
        }
        /// <summary>
        /// 普通预订
        /// </summary>
        /// <param name="scheduledId"></param>
        /// <param name="memberId"></param>
        /// <param name="needReferee"></param>
        /// <param name="memberBId"></param>
        public static void OrderNormal(int scheduledId, int memberId, bool needReferee, int memberBId =0,int accountBId=0)
        {
            OrderNormal(scheduledId, memberId, needReferee, memberBId, 0, 0, "", "",accountBId);
        }
        /// <summary>
        /// 普通预订(包含完整对战信息)
        /// </summary>
        /// <param name="scheduledId"></param>
        /// <param name="memberId">预订人</param>       
        /// <param name="needReferee">是否需要裁判</param>
        ///  /// <param name="memberBId">对方球队联络人（默认为0）</param>
        public static void OrderNormal(int scheduledId, int memberId, bool needReferee, int memberBId,int teamId,int teamBId,string colthColor,string colthBColor,int accountBId = 0)
        {
            DateTime dtNow = DateTime.Now;

            FIELD_ORDER fo =BuildFieldOrderBase(dtNow,scheduledId,memberId,memberBId,EnumOrderType.Normal,EnumFieldStatus.Booking,needReferee,string.Empty);
            if (teamId > 0)
            {
                fo.TEAM_SCORE = new TEAM_SCORE() { TEAM_A_ID = teamId, TEAM_A_COLOR = colthColor };
                if (teamBId > 0)
                {
                    fo.TEAM_SCORE.TEAM_B_ID = teamBId;
                    fo.TEAM_SCORE.TEAM_B_COLOR = colthBColor;
                }
            }

            FIELD_ORDER_LOG fol = new FIELD_ORDER_LOG()
            {
                LOG_DATE = dtNow,
                MEMBER_ID = memberId,
                OPERATION = "客户发起预订申请"
            };
            if (accountBId > 0)
            {
                fol.ACCOUNT_B_ID = accountBId;
                fol.OPERATION = "管理员代为发起预订申请";
                fo.STATUS = (int)EnumFieldStatus.Booked;
            }
            fo.FIELD_ORDER_LOG.Add(fol);
            using (TIQIUEntities context = new TIQIUEntities())
            {
                if (fo.TEAM_SCORE != null)
                {
                    TEAM_SCORE ts = fo.TEAM_SCORE;
                    TEAM teamA = context.TEAMs.FirstOrDefault(s => s.ID == ts .TEAM_A_ID);
                    if (teamA != null) ts.TEAM_A_NAME = teamA.NAME;
                    TEAM teamB = context.TEAMs.FirstOrDefault(s => s.ID == ts.TEAM_B_ID);
                    if (teamB != null) ts.TEAM_A_NAME = teamB.NAME;
                }
                FIELD_SCHEDULED fs = context.FIELD_SCHEDULED.SingleOrDefault(s => s.ID == scheduledId);              
               
                //检查当前场地状态
                if (fs.STATUS != (int)EnumFieldStatus.Available)
                {
                    
                    throw new ApplicationException("预订申请失败，此场地已被预订或无法预订！");
                    //field_order efo = context.FIELD_ORDER.Where(s => s.FIELD_SCHEDULED_ID == scheduledId && s.STATUS > (int)EnumFieldStatus.Booking).OrderByDescending(s => s.CREATE_DATE).FirstOrDefault();
                    //throw new ApplicationException(string.Format("预订申请失败，此场次已于{0}被预订！",efo.CREATE_DATE.ToString("yyyy-MM-DD hh:mm")));
                }
                fs.STATUS = fo.STATUS;

                fo.FIELD_ID = fs.FIELD_ID;
                fo.FIELD_ITEM_ID = fs.FIELD_ITEM_ID;
                fo.FIELD_SCHEDULED_ID = fs.ID;
                fo.ORDER_DATE = fs.SCHEDULED_DATE;
                fo.START_TIME = fs.START_TIME;
                fo.END_TIME = fs.END_TIME;
                fo.PRICE = fs.PRICE;
                fol.FIELD_ITEM_ID = fo.FIELD_ITEM_ID;
                //订单过期时间目前默认为提前一天，小于一天则设置为当前时间+5min.
                fo.EXPIRE_DATE = dtNow > fs.SCHEDULED_DATE.AddDays(-1) ? dtNow.AddMinutes(5) : fs.SCHEDULED_DATE.AddDays(1).Date.AddHours(5);


                //context.FIELD_SCHEDULED.Attach(fs);
                context.FIELD_ORDER.Add(fo);
                
                context.SaveChanges();
            }
                   
        }

       

        /// <summary>
        /// PK约战
        /// </summary>
        /// <param name="scheduledId"></param>
        /// <param name="needReferee">是否需要裁判</param>
        /// <param name="memberId"></param>
        /// <param name="teamId"></param>
        /// <param name="colthColor">队服颜色</param>
        /// <param name="pkPayType">PK付款方式</param>
        /// <param name="teamBId">约战对象</param>
        public static void OrderPK(int scheduledId, bool needReferee, int memberId, int teamId, string colthColor, EnumPKPayType pkPayType,int teamBId)
        {
            DateTime dtNow = DateTime.Now;
            FIELD_ORDER fo = BuildFieldOrderBase(dtNow, scheduledId, memberId, 0, EnumOrderType.PK, EnumFieldStatus.Booking, needReferee,string.Empty);
            fo.PK_PAY_TYPE = (int)pkPayType;
            FIELD_ORDER_LOG fol = new FIELD_ORDER_LOG()
            {
                LOG_DATE = dtNow,
                MEMBER_ID = memberId,
                OPERATION = "客户发起PK预订申请"
            };
            fo.FIELD_ORDER_LOG.Add(fol);
            
            using (TIQIUEntities context = new TIQIUEntities())
            {
                TEAM t = context.TEAMs.FirstOrDefault(s => s.ID == teamId);
                if (t == null) throw new ApplicationException("请选择约战球队！");
                TEAM_SCORE ts = new TEAM_SCORE()
                {
                    
                    TEAM_A_ID = teamId,
                    TEAM_A_COLOR = colthColor
                };
                fo.TEAM_SCORE = ts;
                fo.TEAM_SCORE.TEAM_A_NAME = t.NAME;

                FIELD_SCHEDULED fs = context.FIELD_SCHEDULED.SingleOrDefault(s => s.ID == scheduledId);
                 //检查当前场地状态
                if (fs.STATUS != (int)EnumFieldStatus.Available)
                {
                    throw new ApplicationException("预订申请失败，此场地已被预订或无法预订！");
                    //field_order efo = context.FIELD_ORDER.Where(s => s.FIELD_SCHEDULED_ID == scheduledId && s.STATUS > (int)EnumFieldStatus.Booking).OrderByDescending(s => s.CREATE_DATE).FirstOrDefault();
                    //if (efo.STATUS == (int)EnumFieldStatus.Void || efo.STATUS == (int)EnumFieldStatus.Expired)
                    //    throw new ApplicationException("预订申请失败，此场地已过期或无法预订！");
                    //else
                    //    throw new ApplicationException(string.Format("预订申请失败，此场次已于{0}被预订！", efo.CREATE_DATE.ToString("yyyy-MM-DD hh:mm")));
                }

                fo.TEAM_SCORE.ORDER_DATE = fs.SCHEDULED_DATE;
                fo.TEAM_SCORE.START_TIME = fs.START_TIME;
                fo.TEAM_SCORE.END_TIME = fs.END_TIME;
                

                fs.STATUS = fo.STATUS;

                fo.FIELD_ID = fs.FIELD_ID;
                fo.FIELD_ITEM_ID = fs.FIELD_ITEM_ID;
                fo.FIELD_SCHEDULED_ID = fs.ID;
                fo.ORDER_DATE = fs.SCHEDULED_DATE;
                fo.START_TIME = fs.START_TIME;
                fo.END_TIME = fs.END_TIME;
                fo.PRICE = fs.PRICE;
                //订单过期时间目前默认为提前一天，小于一天则设置为当前时间+5min.
                fo.EXPIRE_DATE = dtNow > fs.SCHEDULED_DATE.AddDays(-1) ? dtNow.AddMinutes(5) : fs.SCHEDULED_DATE.AddDays(1).Date.AddHours(5);
                fol.FIELD_ITEM_ID = fo.FIELD_ITEM_ID;
               // context.FIELD_SCHEDULED.Attach(fs);
                context.FIELD_ORDER.Add(fo);
                context.SaveChanges();
            }

        }

        public static void OrderPKByManager(int scheduledId,int accountId, bool needReferee, int memberId, int teamId, string colthColor, EnumPKPayType pkPayType, int teamBId)
        {
            DateTime dtNow = DateTime.Now;
            FIELD_ORDER fo = BuildFieldOrderBase(dtNow, scheduledId, memberId, 0, EnumOrderType.PK, EnumFieldStatus.Booked, needReferee, string.Empty);
            fo.PK_PAY_TYPE = (int)pkPayType;
            FIELD_ORDER_LOG fol = new FIELD_ORDER_LOG()
            {
                LOG_DATE = dtNow,
                MEMBER_ID = memberId,
                ACCOUNT_B_ID = accountId,
                OPERATION = "管理员代客户发起PK预订申请"
            };
            fo.FIELD_ORDER_LOG.Add(fol);
            if (teamId > 0)
            {
                TEAM_SCORE ts = new TEAM_SCORE()
                {
                    TEAM_A_ID = teamId,
                    TEAM_A_COLOR = colthColor
                };
                // if (teamBId > 0) ts.TEAM_B_ID = teamBId;
                fo.TEAM_SCORE = ts;
            }

            using (TIQIUEntities context = new TIQIUEntities())
            {
                TEAM t = context.TEAMs.FirstOrDefault(s => s.ID == teamId);
                if (t != null) fo.TEAM_SCORE.TEAM_A_NAME = t.NAME;

                FIELD_SCHEDULED fs = context.FIELD_SCHEDULED.SingleOrDefault(s => s.ID == scheduledId);
                FIELD_ITEM fi = context.FIELD_ITEM.Include("FIELD").Single(i => i.ID == fo.FIELD_ITEM_ID);
                //检查当前场地状态
                if (fs.STATUS != (int)EnumFieldStatus.Available)
                {
                    throw new ApplicationException("预订申请失败，此场地已被预订或无法预订！");
                    //field_order efo = context.FIELD_ORDER.Where(s => s.FIELD_SCHEDULED_ID == scheduledId && s.STATUS > (int)EnumFieldStatus.Booking).OrderByDescending(s => s.CREATE_DATE).FirstOrDefault();
                    //if (efo.STATUS == (int)EnumFieldStatus.Void || efo.STATUS == (int)EnumFieldStatus.Expired)
                    //    throw new ApplicationException("预订申请失败，此场地已过期或无法预订！");
                    //else
                    //    throw new ApplicationException(string.Format("预订申请失败，此场次已于{0}被预订！", efo.CREATE_DATE.ToString("yyyy-MM-DD hh:mm")));
                }
                fs.STATUS = fo.STATUS;

                fo.FIELD_ID = fs.FIELD_ID;
                fo.FIELD_ITEM_ID = fs.FIELD_ITEM_ID;
                fo.FIELD_SCHEDULED_ID = fs.ID;
                fo.ORDER_DATE = fs.SCHEDULED_DATE;
                fo.START_TIME = fs.START_TIME;
                fo.END_TIME = fs.END_TIME;
                fo.PRICE = fs.PRICE;
                //订单过期时间目前默认为提前一天，小于一天则设置为当前时间+5min.
                fo.EXPIRE_DATE = dtNow > fs.SCHEDULED_DATE.AddDays(-1) ? dtNow.AddMinutes(5) : fs.SCHEDULED_DATE.AddDays(1).Date.AddHours(5);
                fol.FIELD_ITEM_ID = fo.FIELD_ITEM_ID;
                // context.FIELD_SCHEDULED.Attach(fs);
                context.FIELD_ORDER.Add(fo);
                context.SaveChanges();
                
                string tag = string.Format("{0}{1} {2} {3}", fi.FIELD.NAME, fi.NAME, fo.ORDER_DATE.ToString("yyyy-MM-dd"), fo.START_TIME.ToString(@"hh\:mm"));                
                Messager.SendSMS(fo.MEMBER_ID, string.Format(ConfigurationManager.AppSettings["PK_SMS"], tag));
                Messager.PushOrderMessage(string.Format("已有对手应战，请准时到场比赛：{0} ", tag),
                    fo.MEMBER_ID, fo.ID);
                Messager.PushOrderMessage(string.Format("应战成功，请准时到场比赛：{0} ", tag),
                    memberId, fo.ID);   
            }

        }

        /// <summary>
        /// 应战
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="memberId"></param>
        /// <param name="teamBId"></param>
        /// <param name="accountBId"></param>
        public static void AcceptPK(int orderId, int memberId, int teamBId,int accountBId = 0,string colthBColor = "")
        {
            FIELD_ORDER_LOG fol = new FIELD_ORDER_LOG()
            {
                FIELD_ORDER_ID = orderId,
                LOG_DATE = DateTime.Now,
                MEMBER_ID = memberId,
                ACCOUNT_B_ID = accountBId                
            };
            using (TIQIUEntities context = new TIQIUEntities())
            {
                TEAM teamB = context.TEAMs.FirstOrDefault(s => s.ID ==teamBId);
                if (teamB == null) throw new ApplicationException("应战球队数据有误！");
               
                FIELD_ORDER fo = context.FIELD_ORDER.Include("TEAM_SCORE").Single(s => s.ID == orderId);
                FIELD_ITEM fi = context.FIELD_ITEM.Include("FIELD").Single(i => i.ID == fo.FIELD_ITEM_ID);
                TEAM_SCORE ts = fo.TEAM_SCORE;
                if (ts.TEAM_B_ID > 0 && ts.TEAM_B_NAME.Length > 0) throw new ApplicationException(string.Format("已有球队({0})应战!",ts.TEAM_B_NAME));
                fo.MEMBERB_ID = memberId;
                ts.TEAM_B_ID = teamBId;
                ts.TEAM_B_NAME = teamB.NAME;
                ts.TEAM_B_COLOR = colthBColor;
                fol.OPERATION = string.Format("球队（{0}）应战！", teamB.NAME);
                fol.FIELD_ITEM_ID = fo.FIELD_ITEM_ID;
                //context.FIELD_ORDER.Attach(fo);
                context.FIELD_ORDER_LOG.Add(fol);
                context.SaveChanges();
                string tag = string.Format("{0}{1} {2} {3}", fi.FIELD.NAME, fi.NAME, fo.ORDER_DATE.ToString("yyyy-MM-dd"), fo.START_TIME.ToString(@"hh\:mm"));                
                Messager.SendSMS(fo.MEMBER_ID, string.Format(ConfigurationManager.AppSettings["PK_SMS"], tag));
                Messager.PushOrderMessage(string.Format("已有对手应战，请准时到场比赛：{0}",tag),
                    fo.MEMBER_ID, fo.ID);
                Messager.PushOrderMessage(string.Format("应战成功，请准时到场比赛：{0} ",tag),
                    memberId, fo.ID);   
            }

        }
        /// <summary>
        /// 商家发布散打信息
        /// </summary>
        /// <param name="scheduledId"></param>
        /// <param name="accountBId"></param>
        /// <param name="price"></param>
        /// <param name="priceUnit"></param>
        public static void OrderFreeTeam(int scheduledId, int accountBId, decimal price, EnumPriceUnit priceUnit,int minPlayerCount)
        {
            DateTime dtNow = DateTime.Now;

            FIELD_ORDER fo = BuildFieldOrderBase(dtNow, scheduledId, 0, 0, EnumOrderType.FreeTeam, EnumFieldStatus.Booked, false, string.Empty);
            fo.PRICE = price;
            fo.PRICE_UNIT = (int)priceUnit;
            FIELD_ORDER_LOG fol = new FIELD_ORDER_LOG()
            {
                LOG_DATE = dtNow,
                ACCOUNT_B_ID = accountBId,
                OPERATION = "商家发布散打场次"
            };
            fo.FIELD_ORDER_LOG.Add(fol);
            using (TIQIUEntities context = new TIQIUEntities())
            {
                FIELD_SCHEDULED fs = context.FIELD_SCHEDULED.SingleOrDefault(s => s.ID == scheduledId);

                //检查当前场地状态
                if (fs.STATUS != (int)EnumFieldStatus.Available)
                {
                    throw new ApplicationException("预订申请失败，此场地已被预订或无法预订！");
                    //field_order efo = context.FIELD_ORDER.Where(s => s.FIELD_SCHEDULED_ID == scheduledId && s.STATUS > (int)EnumFieldStatus.Booking).OrderByDescending(s => s.CREATE_DATE).FirstOrDefault();
                    //throw new ApplicationException(string.Format("预订失败，此场次已于{0}被预订申请，请先处理预订申请！", efo.CREATE_DATE.ToString("yyyy-MM-DD hh:mm")));
                }
                fo.FREE_TEAM_MIN_PLAYER = minPlayerCount;
                fo.FIELD_ID = fs.FIELD_ID;
                fo.FIELD_ITEM_ID = fs.FIELD_ITEM_ID;
                fo.FIELD_SCHEDULED_ID = fs.ID;
                fo.ORDER_DATE = fs.SCHEDULED_DATE;
                fo.START_TIME = fs.START_TIME;
                fo.END_TIME = fs.END_TIME;
                //订单过期时间目前默认为提前一天，小于一天则设置为当前时间+5min.
                fo.EXPIRE_DATE = dtNow > fs.SCHEDULED_DATE.AddDays(-1) ? dtNow.AddMinutes(5) : fs.SCHEDULED_DATE.AddDays(1).Date.AddHours(5);
                fol.FIELD_ITEM_ID = fo.FIELD_ITEM_ID;
                
                fs.STATUS = fo.STATUS;

             //   context.FIELD_SCHEDULED.Attach(fs);
                context.FIELD_ORDER.Add(fo);

                context.SaveChanges();
            };
        }

        public static void JoinFreeTeamByManager(int orderId,int accountBId, string phone, string name ,int playerCount = 1)
        {
            if (!Tools.IsMobile(phone)) throw new ApplicationException("请检查电话号码！");
            List<MEMBER> mList = MemberManager.GetMemberByPhone(phone);
            
            MEMBER curMember = null;
            if (mList.Count == 1)
            {
                curMember = mList[0];
            }
            else
            {
                if(name.Length == 0)name = phone;
                curMember = MemberManager.CreateMember(name, phone);
            }
            JoinFreeTeam(orderId, curMember.ID, playerCount, accountBId);
            

        }
        /// <summary>
        /// 加入散打
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="memberId"></param>
        /// <param name="playerCount"></param>
        public static void JoinFreeTeam(int orderId, int memberId, int playerCount = 1,int accountBid = 0)
        {
            int curCount = playerCount;
            FIELD_ORDER_LOG fol = new FIELD_ORDER_LOG()
            {
                FIELD_ORDER_ID = orderId,
                LOG_DATE = DateTime.Now,
                MEMBER_ID = memberId,
                ACCOUNT_B_ID = accountBid,
                OPERATION = accountBid > 0 ? string.Format("管理员帮助加入{0}人",playerCount) : string.Format("会员加入散打{0}人",playerCount)
            };
            FREE_TEAM_LOG ftl = new FREE_TEAM_LOG() { FIELD_ORDER_ID = orderId, CHECKIN_DATE = DateTime.Now, MEMBER_ID = memberId, PLAYER_COUNT = playerCount };
            using (TIQIUEntities context = new TIQIUEntities())
            {              
                List<FREE_TEAM_LOG> ftls = context.FREE_TEAM_LOG.Where(s => s.FIELD_ORDER_ID == orderId).ToList();
                FIELD_ORDER fo = context.FIELD_ORDER.Single(s => s.ID == orderId);
                FIELD_ITEM fi = context.FIELD_ITEM.Include("FIELD").Single(i => i.ID == fo.FIELD_ITEM_ID);
                ftls.ForEach(f => curCount += f.PLAYER_COUNT);
                fol.FIELD_ITEM_ID = fo.FIELD_ITEM_ID;
                context.FREE_TEAM_LOG.Add(ftl);
                context.FIELD_ORDER_LOG.Add(fol);
                
                if (curCount >= fo.FREE_TEAM_MIN_PLAYER)
                {
                    FIELD_SCHEDULED fs = context.FIELD_SCHEDULED.SingleOrDefault(s => s.ID == fo.FIELD_SCHEDULED_ID);
                    fs.STATUS = fo.STATUS = (int)EnumFieldStatus.Booked;

                }
                context.SaveChanges();
                string tag = string.Format("{0}{1} {2} {3}", fi.FIELD.NAME, fi.NAME, fo.ORDER_DATE.ToString("yyyy-MM-dd"), fo.START_TIME.ToString(@"hh\:mm"));
                Messager.SendSMS(memberId, string.Format(ConfigurationManager.AppSettings["SOLO_SMS"], tag));
                Messager.PushOrderMessage(string.Format("您已加入单飞场，请准时到场比赛：{0} ", tag),
                    memberId, fo.ID);        
            }
           
        }
        /// <summary>
        /// 商家确认预订
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="accountBId"></param>
        /// <param name="remark"></param>
        public static void AcceptOrder(int orderId, int accountBId, string remark)
        {
            AcceptOrder(orderId, accountBId, remark, DateTime.MinValue);

        }

        /// <summary>
        /// 商家确认预订，可手动指定过期时间
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="accountBId"></param>
        /// <param name="remark"></param>
        /// <param name="expireDate"></param>
        public static void AcceptOrder(int orderId, int accountBId, string remark, DateTime expireDate)
        {
            FIELD_ORDER_LOG fol = new FIELD_ORDER_LOG()
            {
                FIELD_ORDER_ID = orderId,
                LOG_DATE = DateTime.Now,
                ACCOUNT_B_ID = accountBId,
                OPERATION = "商家接受预订"
            };
            int memberId = 0;
            string msgContent = string.Empty;
            using (TIQIUEntities context = new TIQIUEntities())
            {                
                FIELD_ORDER fo = context.FIELD_ORDER.Single(s => s.ID == orderId);
                
                FIELD_SCHEDULED fs = context.FIELD_SCHEDULED.Single(s => s.ID == fo.FIELD_SCHEDULED_ID);

                FIELD_ITEM fi = context.FIELD_ITEM.Include("FIELD").Single(i => i.ID == fo.FIELD_ITEM_ID);
                
                if (fo.STATUS > (int)EnumFieldStatus.Booking) throw new ApplicationException("接受预订失败，此预订已处理！");
                
                fs.STATUS = fo.STATUS = (int)EnumFieldStatus.Booked;
                if(expireDate != DateTime.MinValue) fo.EXPIRE_DATE = expireDate;
                fo.REMARK = remark;
                fol.FIELD_ITEM_ID = fo.FIELD_ITEM_ID;
                context.FIELD_ORDER_LOG.Add(fol);
                context.SaveChanges();
                string tag = string.Format("{0}{1} {2} {3}", fi.FIELD.NAME, fi.NAME, fo.ORDER_DATE.ToString("yyyy-MM-dd"), fo.START_TIME.ToString(@"hh\:mm"));
                Messager.PushOrderMessage(string.Format("您的场地预订已成功，请准时到场比赛：{0}",tag),
                    fo.MEMBER_ID, fo.ID);               
                Messager.SendSMS(fo.MEMBER_ID,string.Format(ConfigurationManager.AppSettings["CONFIRM_SMS"],tag));
                if (fs.SCHEDULED_DATE.Date > DateTime.Now.Date)
                {
                    Messager.SendSMS(fo.MEMBER_ID, string.Format(ConfigurationManager.AppSettings["RECONFIRM_SMS"], tag), fs.SCHEDULED_DATE.Date.AddHours(8));
                }

            }

        }

        /// <summary>
        /// 客户取消预订
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="memberId"></param>
        /// <param name="remark"></param>
        public static void CancelOrderByMember(int orderId, int memberId, string remark)
        {
            FIELD_ORDER_LOG fol = new FIELD_ORDER_LOG()
            {
                FIELD_ORDER_ID = orderId,
                LOG_DATE = DateTime.Now,
                MEMBER_ID = memberId,
                OPERATION = "客户取消预订！"
            };
            using (TIQIUEntities context = new TIQIUEntities())
            {
                FIELD_ORDER fo = context.FIELD_ORDER.Single(s => s.ID == orderId);
                FIELD_SCHEDULED fs = context.FIELD_SCHEDULED.Single(s => s.ID == fo.FIELD_SCHEDULED_ID);
                FIELD_ITEM fi = context.FIELD_ITEM.Include("FIELD").Single(i => i.ID == fo.FIELD_ITEM_ID);
                if (fo.STATUS == (int)EnumFieldStatus.Canceled) throw new ApplicationException("此预订已取消！");
                fo.STATUS = (int)EnumFieldStatus.Canceled;
                fs.STATUS = (int)EnumFieldStatus.Available;
                fol.FIELD_ITEM_ID = fo.FIELD_ITEM_ID;
                fo.REMARK = remark;
                //context.FIELD_SCHEDULED.Attach(fs);
                //context.FIELD_ORDER.Attach(fo);
                context.FIELD_ORDER_LOG.Add(fol);
                context.SaveChanges();

                Messager.PushOrderMessage(string.Format("您已取消场地预订,原因[{5}]：{0}{1} {2} {3}-{4} ", fi.FIELD.NAME, fi.NAME, fo.ORDER_DATE.ToString("yyyy-MM-dd"), fo.START_TIME.ToString(@"hh\:mm"), fo.END_TIME.ToString(@"hh\:mm"), remark),
                    fo.MEMBER_ID, fo.ID);
                Messager.CancelSMS(fo.ID,DateTime.Now);
            }
        }

        /// <summary>
        /// 商家取消预订
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="accountBId"></param>
        /// <param name="remark"></param>
        public static void CancelOrderByManager(int orderId, int accountBId, string remark)
        {
            FIELD_ORDER_LOG fol = new FIELD_ORDER_LOG()
            {
                FIELD_ORDER_ID = orderId,
                LOG_DATE = DateTime.Now,
                ACCOUNT_B_ID = accountBId,
                OPERATION = "商家取消预订！"
            };
            using (TIQIUEntities context = new TIQIUEntities())
            {
                FIELD_ORDER fo = context.FIELD_ORDER.Single(s => s.ID == orderId);
                FIELD_SCHEDULED fs = context.FIELD_SCHEDULED.Single(s => s.ID == fo.FIELD_SCHEDULED_ID);
                FIELD_ITEM fi = context.FIELD_ITEM.Include("FIELD").Single(i => i.ID == fo.FIELD_ITEM_ID);
                if (fo.STATUS == (int)EnumFieldStatus.Canceled) throw new ApplicationException("此预订已取消！");
                fo.STATUS = (int)EnumFieldStatus.Canceled;
                fs.STATUS = (int)EnumFieldStatus.Available;

                fo.REMARK = remark;
                //context.FIELD_SCHEDULED.Attach(fs);
                //context.FIELD_ORDER.Attach(fo);
                fol.FIELD_ITEM_ID = fo.FIELD_ITEM_ID;
                context.FIELD_ORDER_LOG.Add(fol);

                string tag = string.Format("{0}{1} {2} {3}", fi.FIELD.NAME, fi.NAME, fo.ORDER_DATE.ToString("yyyy-MM-dd"), fo.START_TIME.ToString(@"hh\:mm"));                
                if (fo.TYPE == (int) EnumOrderType.FreeTeam)
                {
                    var fsolo = context.FREE_TEAM_LOG.Where(o => o.FIELD_ORDER_ID == orderId).Select(x=>x.MEMBER_ID).ToList<int>();
                    Messager.PushOrderMessage(string.Format("非常抱歉，您有场地预订已被取消,原因[{1}]：{0}", tag, remark),
                            fsolo.ToArray(), orderId);
                    Messager.SendSMS(fsolo.ToArray(), string.Format(ConfigurationManager.AppSettings["CANCEL_SMS"], tag));
                }
                else
                {
                    Messager.PushOrderMessage(string.Format("非常抱歉，您有场地预订已被取消,原因[{1}]：{0}", tag, remark),
                    fo.MEMBER_ID, orderId);
                    Messager.SendSMS(fo.MEMBER_ID, string.Format(ConfigurationManager.AppSettings["CANCEL_SMS"], tag));
                    if (fo.TYPE == (int) EnumOrderType.PK && fo.MEMBERB_ID > 0)
                    {
                        Messager.PushOrderMessage(string.Format("非常抱歉，您有场地预订已被取消,原因[{1}]：{0}", tag, remark),
                            fo.MEMBERB_ID.GetValueOrDefault(), orderId);
                        Messager.SendSMS(fo.MEMBERB_ID.GetValueOrDefault(), string.Format(ConfigurationManager.AppSettings["CANCEL_SMS"], tag));
                    }
                    
                }
                Messager.CancelSMS(fo.ID,DateTime.Now);

                context.SaveChanges();
            }
        }

        /// <summary>
        /// 会员再次确认订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="memberId"></param>
        public static void ConfirmOrder(int orderId, int memberId, string opr = "客户确认预订！")
        {
            FIELD_ORDER_LOG fol = new FIELD_ORDER_LOG()
            {
                FIELD_ORDER_ID = orderId,
                LOG_DATE = DateTime.Now,
                MEMBER_ID = memberId,
                OPERATION = opr
            };
            using (TIQIUEntities context = new TIQIUEntities())
            {
                FIELD_ORDER fo = context.FIELD_ORDER.Single(s => s.ID == orderId);
                FIELD_SCHEDULED fs = context.FIELD_SCHEDULED.Single(s => s.ID == fo.FIELD_SCHEDULED_ID);
                if (fo.STATUS >= (int)EnumFieldStatus.CustomerConfirmed) throw new ApplicationException("此预订已确认！");
                fs.STATUS = fo.STATUS = (int)EnumFieldStatus.CustomerConfirmed;
                fol.FIELD_ITEM_ID = fo.FIELD_ITEM_ID;
                //context.FIELD_SCHEDULED.Attach(fs);
                //context.FIELD_ORDER.Attach(fo);
                context.FIELD_ORDER_LOG.Add(fol);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 商家确认到场消费
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="accountBId"></param>
        /// <param name="income"></param>
        /// <param name="receive"></param>
        /// <param name="lost"></param>
        public static void CheckInOrder(int orderId, int accountBId, decimal income, int receive =0, int lost = 0)
        {
            FIELD_ORDER_LOG fol = new FIELD_ORDER_LOG()
            {
                FIELD_ORDER_ID = orderId,
                LOG_DATE = DateTime.Now,
                ACCOUNT_B_ID = accountBId,
                OPERATION = "比赛结束！"
            };
            using (TIQIUEntities context = new TIQIUEntities())
            {
                FIELD_ORDER fo = context.FIELD_ORDER.Include("TEAM_SCORE").Single(s => s.ID == orderId);
                FIELD_SCHEDULED fs = context.FIELD_SCHEDULED.Single(s => s.ID == fo.FIELD_SCHEDULED_ID);
                if (fo.STATUS >= (int)EnumFieldStatus.Ending) throw new ApplicationException("此预订已结束！");
                fs.STATUS = fo.STATUS = (int)EnumFieldStatus.Ending;
                fo.INCOME = income;
                fol.FIELD_ITEM_ID = fo.FIELD_ITEM_ID;
                if (fo.TEAM_SCORE != null)
                {
                    TEAM_SCORE ts = fo.TEAM_SCORE;
                    ts.RECEIVE_SCORE = receive;
                    ts.LOSE_SCORE = lost;
                }

                //context.FIELD_SCHEDULED.Attach(fs);
                //context.FIELD_ORDER.Attach(fo);
                context.FIELD_ORDER_LOG.Add(fol);
                context.SaveChanges();
            }
        }

        private static FIELD_ORDER BuildFieldOrderBase(DateTime dtNow, int scheduledId, int memberId, int memberBId, EnumOrderType orderType, EnumFieldStatus status, bool needReferee,string remark)
        {
            return new FIELD_ORDER()
            {
                CREATE_DATE = dtNow,
                FIELD_SCHEDULED_ID = scheduledId,
                MEMBER_ID = memberId,
                MEMBERB_ID = memberBId,
                TYPE = (int)orderType,
                STATUS = (int)status,
                NEED_REFEREE = needReferee,
                REMARK = remark
            };
        }
    }
}
