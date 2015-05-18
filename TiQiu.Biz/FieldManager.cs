using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TiQiu.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Data;

namespace TiQiu.Biz
{
    public static class FieldManager
    {
        public static FIELD CreateField(string name, string brief, string adress, string area_code,
            int level, decimal top_price, decimal bottom_price, TimeSpan start, TimeSpan end, int half_count, int full_count,
            float locationL, float locationb, bool has_bathroom, int businessId)
        {
            FIELD f = null;

            f = new FIELD()
            {
                BUSINESSES_ID = businessId,
                NAME = name,
                BRIEF = brief,
                ADRESS = adress,
                AREA_CODE = area_code,
                LEVEL = level,
                TOP_PRICE = top_price,
                BOTTOM_PRICE = bottom_price,
                //START_TIME = start,
                //END_TIME = end,
                //HALF_SIZE_FIELD = half_count,
                //FULL_SIZE_FIELD = full_count,
                L = locationL,
                B = locationb,
                HAS_BATHROOM = has_bathroom
            };
            return CreateField(f);
        }

        public static FIELD CreateField(FIELD f)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {
                if (context.BUSINESSES.Count(bu => bu.ID == f.BUSINESSES_ID) == 0) throw new ApplicationException("Not Exists Business.ID =" + f.BUSINESSES_ID);

                context.FIELDs.Add(f);
                context.SaveChanges();
            }
            return f;
        }

        public static FIELD GetField(int fieldId)
        {
            FIELD relObj = null;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                relObj = context.FIELDs.Include("FIELD_ITEM").SingleOrDefault(f => f.ID == fieldId);
                
            }
            return relObj;
        }

        public static FIELD_ITEM CreateField_Item(string name, string brief, int level, int type, int fieldId)
        {
            FIELD_ITEM item = new FIELD_ITEM() { NAME = name, BRIEF = brief, FIELD_ID = fieldId, LEVEL = level, TYPE = type };

            return CreateField_Item(item);
        }

        public static FIELD_ITEM CreateField_Item(FIELD_ITEM item)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {
                FIELD fd = context.FIELDs.FirstOrDefault(fe => fe.ID == item.FIELD_ID);

                if (null == fd) throw new ApplicationException("Not Exists Field.ID = " + item.FIELD_ID);
                item.BUSINESSES_ID = fd.BUSINESSES_ID;
                context.FIELD_ITEM.Add(item);

                context.SaveChanges();
            }
            return item;
        }

        public static void UpdateField(int fieldId, string name, string brief, string adress, string area_code,
            int level, decimal top_price, decimal bottom_price, TimeSpan start, TimeSpan end, int half_count, int full_count,
            float locationL, float locationb, bool has_bathroom)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {

                FIELD f = context.FIELDs.SingleOrDefault(fe => fe.ID == fieldId);

                f.NAME = name;
                f.BRIEF = brief;
                f.ADRESS = adress;
                f.AREA_CODE = area_code;
                f.LEVEL = level;
                f.TOP_PRICE = top_price;
                f.BOTTOM_PRICE = bottom_price;
                f.L = locationL;
                f.B = locationb;
                f.HAS_BATHROOM = has_bathroom;

                context.SaveChanges();
            }

        }

        public static void UpdateField(FIELD fd)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {

                FIELD f = context.FIELDs.SingleOrDefault(fe => fe.ID == fd.ID);

                f.NAME = fd.NAME;
                f.BRIEF = fd.BRIEF;
                f.ADRESS = fd.ADRESS;
                f.AREA_CODE = fd.AREA_CODE;
                f.LEVEL = fd.LEVEL;
                f.TOP_PRICE = fd.TOP_PRICE;
                f.BOTTOM_PRICE = fd.BOTTOM_PRICE;
                f.L = fd.L;
                f.B = fd.B;
                f.HAS_BATHROOM = fd.HAS_BATHROOM;
                f.TEL = fd.TEL;
                f.PHONE = fd.PHONE;
                context.SaveChanges();
            }

        }

        public static void UpdateFieldItem(int fieldItemId, string name, string brief, int level, int type)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {

                FIELD_ITEM f = context.FIELD_ITEM.SingleOrDefault(fe => fe.ID == fieldItemId);

                f.NAME = name;
                f.BRIEF = brief;
                f.LEVEL = level;
                f.TYPE = type;

                context.SaveChanges();
            }
        }

        public static void UpdateFieldItem(FIELD_ITEM fi)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {

                FIELD_ITEM f = context.FIELD_ITEM.SingleOrDefault(fe => fe.ID == fi.ID);

                f.NAME = fi.NAME;
                f.BRIEF = fi.BRIEF;
                f.LEVEL = fi.LEVEL;
                f.TYPE = fi.TYPE;

                context.SaveChanges();
            }
        }

        public static void DelField(int fieldId)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {
                List<FIELD_ITEM> fis = context.FIELD_ITEM.Where(f => f.FIELD_ID == fieldId).ToList();
                fis.ForEach(fi => fi.STATUS = (int)EnumDataStatus.Void);
                FIELD fe = context.FIELDs.SingleOrDefault(f => f.ID == fieldId);
                fe.STATUS = (int)EnumDataStatus.Void;
                context.SaveChanges();
            }
        }

        public static void DelFieldItem(int fieldItemId)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {
                FIELD_ITEM fe = context.FIELD_ITEM.SingleOrDefault(f => f.ID == fieldItemId);
                fe.STATUS = (int)EnumDataStatus.Void;
                context.SaveChanges();
            }
        }

        public static List<FIELD> GetFieldList(Expression<Func<FIELD, bool>> condition, string orderBy, bool ascending, int pageIdx, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<FIELD> rel = new List<FIELD>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = PagingQuery<FIELD>.GetPagingList(context.FIELDs.AsQueryable(), condition, orderBy, ascending, pageIdx, pageSize, out totalCount);
            }
            return rel;
        }

        public static List<FIELD> GetFieldList(string name, string areaCode,List<EnumFieldType> fileType,
            DateTime? startDate,DateTime? endDate,decimal lowPrice,decimal topPrice, 
            string orderBy, bool ascending, int pageIdx, int pageSize,double l,double b, out int totalCount)
        {
            totalCount = 0;
            bool getAllField = true;
            List<FIELD> rel = new List<FIELD>();
            string sql = @"  
                    SELECT F.ID,F.BUSINESSES_ID,F.AREA_CODE,F.L,F.B,
	                    F.NAME,F.BRIEF,F.LEVEL,
	                    F.ADRESS,F.SCORE,F.HAS_BATHROOM,F.TOP_PRICE,F.BOTTOM_PRICE,
	                    F.STATUS,F.TEL,F.PHONE,F.PIC_PATH,F.TYPE
	                FROM DBO.FIELD F
	                {3} JOIN  
	                    (SELECT S.FIELD_ID,COUNT(S.FIELD_ITEM_ID) AS ITEM_COUNT 
		                    FROM dbo.FIELD_SCHEDULED S
                                INNER JOIN dbo.FIELD_ITEM I 
                                ON S.FIELD_ITEM_ID = I.ID 
		                    {0}
			            GROUP BY S.FIELD_ID) SS
	                ON F.ID = SS.FIELD_ID {1}
	                {2}";
            string countSql = @"SELECT COUNT(F.ID) 
                    FROM DBO.FIELD F
	                {3} JOIN  
	                    (SELECT S.FIELD_ID,COUNT(S.FIELD_ITEM_ID) AS ITEM_COUNT 
		                    FROM dbo.FIELD_SCHEDULED S
                                INNER JOIN dbo.FIELD_ITEM I 
                                ON S.FIELD_ITEM_ID = I.ID 
		                    {0}
			            GROUP BY S.FIELD_ID) SS
	                ON F.ID = SS.FIELD_ID {1}
	                {2}";
            string orderSql = string.Empty;
            if (l != 0 && b != 0)
            {
                orderSql = string.Format(" ORDER BY dbo.fnGetDistance({0},{1},F.L,F.B)", l, b);
            }
            List<string> where = new List<string>();
            List<string> schWhere = new List<string>();
            List<SqlParameter> parmeters = new List<SqlParameter>();

            where.Add("F.STATUS = @status");
            SqlParameter para = new SqlParameter("@status", SqlDbType.Int);

            para.Value = EnumDataStatus.Actived;
            parmeters.Add(para);

            if (name != null && name.Length > 0)
            {
                where.Add("F.NAME LIKE @name");
                SqlParameter mypara = new SqlParameter("@name", SqlDbType.VarChar);

                mypara.Value = string.Format("%{0}%", name);
                parmeters.Add(mypara);
            }
            if (areaCode != null && areaCode.Length > 0 && areaCode != "0")
            {
                where.Add("F.AREA_CODE = @area");
                SqlParameter mypara = new SqlParameter("@area", SqlDbType.VarChar);
                mypara.Value = areaCode;
                parmeters.Add(mypara);
            }
            if (fileType.Count > 0)
            {
                int prefix = 0;
                List<string> orSql = new List<string>();
                fileType.ForEach(f =>
                {
                    orSql.Add("I.TYPE = @type" + prefix);
                    SqlParameter mypara = new SqlParameter("@type" + prefix, SqlDbType.Int);
                    mypara.Value = (int)f;
                    parmeters.Add(mypara);
                    prefix++;
                });
                schWhere.Add("(" + string.Join(" OR ", orSql) + ")");
                getAllField = false;

            }
            if (startDate.HasValue && startDate.Value.Year > 2013)
            {
                schWhere.Add("S.SCHEDULED_DATE >= @start");
                SqlParameter mypara = new SqlParameter("@start", SqlDbType.DateTime);
                mypara.Value = startDate.Value.Date;
                parmeters.Add(mypara);
                getAllField = false;
            }
            if (endDate.HasValue && endDate.Value.Year > 2013)
            {
                schWhere.Add("S.SCHEDULED_DATE <= @end");
                SqlParameter mypara = new SqlParameter("@end", SqlDbType.DateTime);
                mypara.Value = endDate.Value.Date;
                parmeters.Add(mypara);
                getAllField = false;
            }
            if (lowPrice > 0)
            {
                schWhere.Add("S.PRICE >= @low");
                SqlParameter mypara = new SqlParameter("@low", SqlDbType.Decimal);
                mypara.Value = lowPrice;
                parmeters.Add(mypara);
                getAllField = false;

            }
            if (topPrice > 0)
            {
                schWhere.Add("S.PRICE <= @top");
                SqlParameter mypara = new SqlParameter("@top", SqlDbType.Decimal);
                mypara.Value = topPrice;
                parmeters.Add(mypara);
                getAllField = false;
            }
            string preJoinstr = getAllField ? " LEFT OUTER " : " INNER ";
            string whereSql = string.Empty;
            if (where.Count > 0)
            {
                whereSql = " WHERE " + string.Join(" AND ", where);

            }
            string schWhereSql = string.Empty;
            if (schWhere.Count > 0)
            {
                schWhereSql = " WHERE " + string.Join(" AND ", schWhere);
            }
            sql = string.Format(sql, schWhereSql, whereSql, orderSql, preJoinstr);
            countSql = string.Format(countSql, schWhereSql, whereSql, " ", preJoinstr);
            SqlParameter[] parmetersArray = parmeters.ToArray();
            SqlParameter[] clonedParameters = new SqlParameter[parmetersArray.Length];
            SqlParameter[] clonedParameters1 = new SqlParameter[parmetersArray.Length];
            for (int i = 0, j = parmetersArray.Length; i < j; i++)
            {
                clonedParameters[i] = (SqlParameter)((ICloneable)parmetersArray[i]).Clone();
                clonedParameters1[i] = (SqlParameter)((ICloneable)parmetersArray[i]).Clone();
            }

            
//            fieldItemSql += whereSql + @" GROUP BY ID,BUSINESSES_ID,AREA_CODE,L,B,NAME,BRIEF,LEVEL,ADRESS,
//        SCORE,HAS_BATHROOM, BOTTOM_PRICE,  TOP_PRICE, STATUS,TEL, PHONE,PIC_PATH,FIELD_ITEM_ID, FIELD_ITEM_STATUS,FIELD_ITEM_NAME,
//        TYPE ";
//            countSql += whereSql;
            using (TIQIUEntities context = new TIQIUEntities())
            {

                totalCount = context.Database.SqlQuery<int>(countSql, parmetersArray).FirstOrDefault();
                rel = context.Database.SqlQuery<FIELD>(sql, clonedParameters)
                    .AsQueryable()
                    .Skip((pageIdx - 1) * pageSize)
                    .Take(pageSize)
                    .ToList<FIELD>();
                List<int> keys = new List<int>();
                rel.ForEach(f => { keys.Add(f.ID); });
                var items = from item in context.FIELD_ITEM
                    where keys.Contains(item.FIELD_ID)
                    select item;
                rel.ForEach(f => {
                    items.Where(i => i.FIELD_ID == f.ID)
                        .ToList()
                        .ForEach(e => {
                            f.FIELD_ITEM.Add(new FIELD_ITEM()
                            {
                                ID = e.ID,
                                NAME = e.NAME,
                                STATUS = e.STATUS,
                                TYPE = e.TYPE,
                                LEVEL = e.LEVEL,
                                BRIEF = e.BRIEF,
                                FIELD_ID = e.FIELD_ID,
                                BUSINESSES_ID = e.BUSINESSES_ID
                            });
                    
                    });
                });
                
/*
                string sql = @"SELECT DISTINCT field.ID,field.BUSINESSES_ID,field.AREA_CODE,field.L,field.B,
	    field.NAME,field.BRIEF,field.LEVEL,
	    field.ADRESS,field.SCORE,field.HAS_BATHROOM,field.TOP_PRICE,field.BOTTOM_PRICE,
	    field.STATUS,field.TEL,field.PHONE,field.PIC_PATH
	FROM v_field_item_scheduled AS field";
                string countSql = " SELECT COUNT(DISTINCT ID)  FROM v_field_item_scheduled AS field ";

                string fieldItemSql = @"SELECT ID,BUSINESSES_ID,AREA_CODE,L,B,NAME,BRIEF,LEVEL,ADRESS,
        SCORE,HAS_BATHROOM, BOTTOM_PRICE,  TOP_PRICE, STATUS,TEL, PHONE,PIC_PATH,FIELD_ITEM_ID, FIELD_ITEM_STATUS,FIELD_ITEM_NAME,
        TYPE, MIN(ISNULL(SCHEDULED_ID,0)) AS SCHEDULED_ID, MIN(ISNULL(SCHEDULED_DATE,'1900-1-1')) AS SCHEDULED_DATE,null  AS START_TIME,null AS END_TIME,0.0 AS PRICE,0 AS SCHEDULED_STATUS
     FROM v_field_item_scheduled AS field ";

                List<string> where = new List<string>();
                List<SqlParameter> parmeters = new List<SqlParameter>();

               
                if (name != null && name.Length > 0){
                    where.Add("field.NAME LIKE @name");                    
                    SqlParameter mypara = new SqlParameter("@name",SqlDbType.VarChar);
                    
                    mypara.Value = string.Format("%{0}%",name);
                    parmeters.Add(mypara);
                }
                if (areaCode != null && areaCode.Length > 0 && areaCode != "0")
                {
                    where.Add("field.AREA_CODE = @area");
                    SqlParameter mypara = new SqlParameter("@area", SqlDbType.VarChar);
                    mypara.Value = areaCode;
                    parmeters.Add(mypara);                   
                }
                if (fileType.Count > 0){
                    int prefix = 0;
                    List<string> orSql = new List<string>();
                    fileType.ForEach(f =>
                    {
                        orSql.Add("field.TYPE = @type" + prefix);
                        SqlParameter mypara = new SqlParameter("@type" + prefix, SqlDbType.Int);
                        mypara.Value = (int)f;
                        parmeters.Add(mypara);                                
                        prefix++;
                    });
                    where.Add( "("+ string.Join(" OR ",orSql)+")");
                    
                }
                if (startDate.HasValue && startDate.Value.Year >2013){
                    where.Add("field.SCHEDULED_DATE >= @start");
                    SqlParameter mypara = new SqlParameter("@start", SqlDbType.DateTime);
                    mypara.Value = startDate.Value.Date;
                    parmeters.Add(mypara);                        
                }
                if (endDate.HasValue && endDate.Value.Year > 2013){
                    where.Add("field.SCHEDULED_DATE <= @end");
                    SqlParameter mypara = new SqlParameter("@end", SqlDbType.DateTime);
                    mypara.Value = endDate.Value.Date;
                    parmeters.Add(mypara);                    
                }
                if (lowPrice > 0){
                    where.Add("field.PRICE >= @low");
                    SqlParameter mypara = new SqlParameter("@low", SqlDbType.Decimal);
                    mypara.Value = lowPrice;
                    parmeters.Add(mypara);          
                    
                }
                if (topPrice > 0){
                    where.Add("field.PRICE <= @top");
                    SqlParameter mypara = new SqlParameter("@top", SqlDbType.Decimal);
                    mypara.Value = topPrice;
                    parmeters.Add(mypara);                            
                }
                
                string whereSql = "";
                if (where.Count > 0)
                {
                    whereSql = " WHERE " + string.Join(" AND ", where);
                   
                }
                sql += whereSql;
                fieldItemSql += whereSql + @" GROUP BY ID,BUSINESSES_ID,AREA_CODE,L,B,NAME,BRIEF,LEVEL,ADRESS,
        SCORE,HAS_BATHROOM, BOTTOM_PRICE,  TOP_PRICE, STATUS,TEL, PHONE,PIC_PATH,FIELD_ITEM_ID, FIELD_ITEM_STATUS,FIELD_ITEM_NAME,
        TYPE ";
                countSql += whereSql;

                SqlParameter[] parmetersArray = parmeters.ToArray();
                SqlParameter[] clonedParameters = new SqlParameter[parmetersArray.Length];
                SqlParameter[] clonedParameters1 = new SqlParameter[parmetersArray.Length];
                for (int i = 0, j = parmetersArray.Length; i < j; i++)
                {
                    clonedParameters[i] = (SqlParameter)((ICloneable)parmetersArray[i]).Clone();
                    clonedParameters1[i] = (SqlParameter)((ICloneable)parmetersArray[i]).Clone();
                }
                
                totalCount = context.Database.SqlQuery<int>(countSql, parmetersArray).FirstOrDefault();

                IEnumerable<FIELD> query = context.Database.SqlQuery<FIELD>(sql, clonedParameters);
                
                rel = query.AsQueryable().OrderBy(orderBy, ascending)
                    .Skip((pageIdx - 1) * pageSize)
                    .Take(pageSize)
                    .ToList<FIELD>();
                IEnumerable<V_FIELD_ITEM_SCHEDULED> fieldItemQuery = context.Database.SqlQuery<V_FIELD_ITEM_SCHEDULED>(fieldItemSql, clonedParameters1);
                List<V_FIELD_ITEM_SCHEDULED> items = fieldItemQuery.ToList();
                foreach (FIELD f in rel)
                {
                    items.Where(i => i.ID == f.ID).OrderBy(i => i.FIELD_ITEM_ID).ToList()
                        .ForEach(i => f.FIELD_ITEM.Add(new FIELD_ITEM()
                        {
                            ID = i.FIELD_ITEM_ID,
                            NAME = i.FIELD_ITEM_NAME,
                            STATUS = i.FIELD_ITEM_STATUS,
                            TYPE = i.TYPE,
                            LEVEL = 0,
                            BRIEF = i.BRIEF,
                            FIELD_ID = i.ID,
                            BUSINESSES_ID = i.BUSINESSES_ID
                        }));
                }
 * */
                //rel.ForEach(f=>fieldItemQuery.Where(i=>i.ID == f.ID)
                //    .ToList()
                //        .ForEach(i=>f.FIELD_ITEM.Add(new field_item(){ 
                //            ID = i.FIELD_ITEM_ID,NAME= i.FIELD_ITEM_NAME,
                //            STATUS = i.FIELD_ITEM_STATUS, TYPE = i.TYPE, 
                //            FIELD_ID= i.ID, BUSINESSES_ID =i.BUSINESSES_ID})));
                     
                //rel = PagingQuery<FIELD>.GetPagingList(query.AsQueryable(), PredicateExtensionses.True<FIELD>(), orderBy, ascending, pageIdx, pageSize, out totalCount);
            }
            return rel;
        }

        public static List<FIELD_ITEM> GetFieldItemList(Expression<Func<FIELD_ITEM, bool>> condition, string orderBy, bool ascending, int pageIdx, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<FIELD_ITEM> rel = new List<FIELD_ITEM>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = PagingQuery<FIELD_ITEM>.GetPagingList(context.FIELD_ITEM.Include("FIELD").AsQueryable(), condition, orderBy, ascending, pageIdx, pageSize, out totalCount);
            }
            return rel;
        }

        public static List<FIELD> GetFieldList(int accountBId)
        {
            List<FIELD> rel = new List<FIELD>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                var ab = context.ACCOUNT_B_BUSINESSES.FirstOrDefault<ACCOUNT_B_BUSINESSES>(b => b.ACCOUNT_B_ID == accountBId);
                if (ab != null)
                {
                    rel = context.FIELDs.Include("FIELD_ITEM").Where<FIELD>(f => f.BUSINESSES_ID == ab.BUSINESSESS_ID).ToList<FIELD>();
                }
            }
            return rel;
        }

        #region Field Rules

        public static FIELD_RULE CreateFieldRule(int fieldId, int fieldItemId, EnumRuleType ruleType, EnumScheduleType scheduleType, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime, decimal price, string ruleName, string description)
        {
            FIELD_RULE fr;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                fr = new FIELD_RULE() { FIELD_ID = fieldId, FIELD_ITEM_ID = fieldItemId, TYPE = (int)ruleType, SCHEDULE_TYPE = (int)scheduleType, START_DATE = startDate.Date, END_DATE = endDate.Date, START_TIME = startTime, END_TIME = endTime, PRICE = price, RULE_NAME = ruleName, DESCRIPTION = description, STATUS = 1 };
                context.FIELD_RULE.Add(fr);
                context.SaveChanges();
            }
            return fr;

        }

        public static void CreateFieldRule(int fieldItemId,List<FIELD_RULE> rules)
        {
            
            using (TIQIUEntities context = new TIQIUEntities())
            {
                context.FIELD_RULE.RemoveRange(context.FIELD_RULE.Where(r => r.FIELD_ITEM_ID == fieldItemId));
                var item = context.FIELD_ITEM.SingleOrDefault(f => f.ID == fieldItemId);
                if(item == null) throw new ApplicationException("场地ID参数错误");
                rules.ForEach(i =>
                {
                    i.FIELD_ID = item.FIELD_ID;
                    i.FIELD_TYPE = item.TYPE;
                });
                context.FIELD_RULE.AddRange(rules);
                context.SaveChanges();
            }
            

        }

        public static List<FIELD_RULE> GetFieldRule(int fieldItemId)
        {
            List<FIELD_RULE> rules = new List<FIELD_RULE>();
            using (TIQIUEntities context = new TIQIUEntities())
            {

                rules = context.FIELD_RULE.Where(r => r.FIELD_ITEM_ID == fieldItemId).ToList();
                
            }
            return rules;

        }

        public static void DuplicateCheck(FIELD_RULE newFieldRule)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {
                List<FIELD_RULE> rs = context.FIELD_RULE.Where(r => r.FIELD_ID == newFieldRule.FIELD_ID
                    && r.FIELD_TYPE == (int)newFieldRule.FIELD_TYPE
                    && r.TYPE == newFieldRule.TYPE
                    && r.SCHEDULE_TYPE == newFieldRule.SCHEDULE_TYPE).ToList();
                switch (newFieldRule.TYPE)
                {
                    case (int)EnumRuleType.Event:


                        break;
                    default:
                        break;
                }
            }

        }

        #endregion

        #region Schedule

        public static void CreateFieldScheduled(int fieldId, DateTime start, DateTime end,EnumScheduleType? dayOfWeek = null)
        {
            using (TIQIUEntities context = new TIQIUEntities())
            {
                start = start.Date;
                end = end.Date;
                List<FIELD_RULE> rs = context.FIELD_RULE.Where(r => r.FIELD_ID == fieldId
                    && r.STATUS == 1 && (!dayOfWeek.HasValue || r.SCHEDULE_TYPE == (int)dayOfWeek.Value)
                    && r.START_DATE <= end && r.END_DATE >= start)
                    .OrderBy(r => r.FIELD_ITEM_ID)
                    .ThenBy(r => r.TYPE)//默认RuleType排名第一为Event.
                    .ToList();
                List<FIELD_SCHEDULED> ss = new List<FIELD_SCHEDULED>();

                SqlParameter startdate = new SqlParameter("@start", SqlDbType.DateTime);
                startdate.Value = start;
                SqlParameter enddate = new SqlParameter("@end", SqlDbType.DateTime);
                enddate.Value = end;
                SqlParameter fid = new SqlParameter("@fieldId", SqlDbType.Int);
                fid.Value = fieldId;
                SqlParameter curDt = new SqlParameter("@curdate", SqlDbType.DateTime);
                curDt.Direction = System.Data.ParameterDirection.InputOutput;
                curDt.Value = new DateTime(2015,1,1);
//                context.Database.ExecuteSqlCommand(@"  select top 1 @curdate=SCHEDULED_DATE from (
//  select convert(varchar(10),SCHEDULED_DATE,120) AS SCHEDULED_DATE,sum(status) as Void
//  from field_scheduled where field_id = @fieldId and  SCHEDULED_DATE >= @start
//  group by convert(varchar(10),SCHEDULED_DATE,120)) k where Void = 0 order by SCHEDULED_DATE ", new SqlParameter[3] { startdate, fid, curDt });

                context.Database.ExecuteSqlCommand(@"  SELECT TOP 1 @curdate=(convert(varchar(10),ORDER_DATE,120)) 
        FROM FIELD_ORDER 
        WHERE FIELD_ID = @fieldId AND  ORDER_DATE >= @start AND [Status] >0 and [Status] < 50
   order by ORDER_DATE DESC ", new SqlParameter[4] { startdate,enddate, fid, curDt });

                DateTime curDate = (DateTime)curDt.Value;
                if (curDate != null && curDate > new DateTime(2015, 1, 1))
                {
                    if (curDate >= start)
                    {
                        start = curDate.AddDays(1);
                        if (start > end) return;
                    }
                }
                context.FIELD_SCHEDULED.RemoveRange(context.FIELD_SCHEDULED.Where(f => f.FIELD_ID == fieldId && f.SCHEDULED_DATE >= start));
                foreach (FIELD_RULE rule in rs)
                {
                    switch (rule.TYPE)
                    {
                        case (int)EnumRuleType.Event:
                            BuildEventSchedule(rule, ss, start, end);
                            break;
                        case (int)EnumRuleType.Void:
                            BuildSpecialSchedule(rule, ss, start, end);
                            break;
                        default:
                            throw new KeyNotFoundException("Type Key is not defined:" + rule.TYPE);
                            
                    }
                }
                context.FIELD_SCHEDULED.AddRange(ss);
                context.SaveChanges();

            }
        }

        /// <summary>
        /// 必须首先执行正常场次规则,简化设计，正常场次规则只需要指定一周所有场次模板。
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="targetList"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void BuildEventSchedule(FIELD_RULE rule, List<FIELD_SCHEDULED> targetList, DateTime start, DateTime end)
        {
            if (rule.SCHEDULE_TYPE > (int)EnumScheduleType.Sunday || rule.SCHEDULE_TYPE < (int)EnumScheduleType.Monday)
                throw new ApplicationException("场次规则类型有误！");
            DateTime curStart = rule.START_DATE >= start ? rule.START_DATE : start;
            DateTime curEnd = rule.END_DATE <= end ? rule.END_DATE : end;
            while (curStart <= curEnd)
            {
                FIELD_SCHEDULED fs = new FIELD_SCHEDULED()
                {
                    FIELD_ID = rule.FIELD_ID,
                    FIELD_ITEM_ID = rule.FIELD_ITEM_ID,
                    SCHEDULED_DATE = curStart,
                    PRICE = rule.PRICE,
                    RULE_TYPE = rule.TYPE,
                    START_TIME = rule.START_TIME,
                    END_TIME = rule.END_TIME,
                    STATUS = (int)EnumFieldStatus.Available,
                    REMARK = rule.RULE_NAME
                };
                switch (rule.SCHEDULE_TYPE)
                {
                    case (int)EnumScheduleType.Monday:
                        if (curStart.DayOfWeek == DayOfWeek.Monday) targetList.Add(fs);
                        break;
                    case (int)EnumScheduleType.Tuesday:
                        if (curStart.DayOfWeek == DayOfWeek.Tuesday) targetList.Add(fs);
                        break;
                    case (int)EnumScheduleType.Wednesday:
                        if (curStart.DayOfWeek == DayOfWeek.Wednesday) targetList.Add(fs);
                        break;
                    case (int)EnumScheduleType.Thusday:
                        if (curStart.DayOfWeek == DayOfWeek.Thursday) targetList.Add(fs);
                        break;
                    case (int)EnumScheduleType.Friday:
                        if (curStart.DayOfWeek == DayOfWeek.Friday) targetList.Add(fs);
                        break;
                    case (int)EnumScheduleType.Saturday:
                        if (curStart.DayOfWeek == DayOfWeek.Saturday) targetList.Add(fs);
                        break;
                    case (int)EnumScheduleType.Sunday:
                        if (curStart.DayOfWeek == DayOfWeek.Sunday) targetList.Add(fs);
                        break;
                }
                curStart = curStart.AddDays(1);
            }

            //    if (rule.SCHEDULE_TYPE == (int)EnumScheduleType.Once)
            //    {
            //        targetList.Add(fs);
            //        break;
            //    }
            //    else if (rule.SCHEDULE_TYPE == (int)EnumScheduleType.Daily)
            //    {
            //        targetList.Add(fs);
            //        curStart = curStart.AddDays(1);
            //    }
            //    else if (rule.SCHEDULE_TYPE == (int)EnumScheduleType.Monday)
            //    {
            //        if (curStart.DayOfWeek == DayOfWeek.Monday)
            //        {
            //            targetList.Add(fs);
            //            curStart = curStart.AddDays(7);
            //        }
            //        else
            //        {
            //            curStart = curStart.AddDays(1);
            //        }

            //    }
            //    else if (rule.SCHEDULE_TYPE == (int)EnumScheduleType.Tuesday)
            //    {
            //        if (curStart.DayOfWeek == DayOfWeek.Tuesday)
            //        {
            //            targetList.Add(fs);
            //            curStart = curStart.AddDays(7);
            //        }
            //        else
            //        {
            //            curStart = curStart.AddDays(1);
            //        }
            //    }
            //    else if (rule.SCHEDULE_TYPE == (int)EnumScheduleType.Wednesday)
            //    {
            //        if (curStart.DayOfWeek == DayOfWeek.Wednesday)
            //        {
            //            targetList.Add(fs);
            //            curStart = curStart.AddDays(7);
            //        }
            //        else
            //        {
            //            curStart = curStart.AddDays(1);
            //        }
            //    }
            //    else if (rule.SCHEDULE_TYPE == (int)EnumScheduleType.Thusday)
            //    {
            //        if (curStart.DayOfWeek == DayOfWeek.Thursday)
            //        {
            //            targetList.Add(fs);
            //            curStart = curStart.AddDays(7);
            //        }
            //        else
            //        {
            //            curStart = curStart.AddDays(1);
            //        }
            //    }
            //    else if (rule.SCHEDULE_TYPE == (int)EnumScheduleType.Friday)
            //    {
            //        if (curStart.DayOfWeek == DayOfWeek.Friday)
            //        {
            //            targetList.Add(fs);
            //            curStart = curStart.AddDays(7);
            //        }
            //        else
            //        {
            //            curStart = curStart.AddDays(1);
            //        }
            //    }
            //    else if (rule.SCHEDULE_TYPE == (int)EnumScheduleType.Saturday)
            //    {
            //        if (curStart.DayOfWeek == DayOfWeek.Saturday)
            //        {
            //            targetList.Add(fs);
            //            curStart = curStart.AddDays(7);
            //        }
            //        else
            //        {
            //            curStart = curStart.AddDays(1);
            //        }
            //    }
            //    else if (rule.SCHEDULE_TYPE == (int)EnumScheduleType.Sunday)
            //    {
            //        if (curStart.DayOfWeek == DayOfWeek.Sunday)
            //        {
            //            targetList.Add(fs);
            //            curStart = curStart.AddDays(7);
            //        }
            //        else
            //        {
            //            curStart = curStart.AddDays(1);
            //        }
            //    }
            //    else if (rule.SCHEDULE_TYPE == (int)EnumScheduleType.Mothly)
            //    {
            //        targetList.Add(fs);
            //        curStart = curStart.AddMonths(1);
            //    }
            //    else if (rule.SCHEDULE_TYPE == (int)EnumScheduleType.Yearly)
            //    {
            //        targetList.Add(fs);
            //        curStart = curStart.AddYears(1);
            //    }
            //    else throw new KeyNotFoundException("场次规则出错，规则ID=" + rule.SCHEDULE_TYPE);
            //}
        }

        /// <summary>
        /// 正常场次生成后，匹配特殊设定规则，生成占用时间段，同时移除存在交集的正常场次。
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="targetList"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void BuildSpecialSchedule(FIELD_RULE rule, List<FIELD_SCHEDULED> targetList, DateTime start, DateTime end)
        {
            if (rule.SCHEDULE_TYPE <= (int)EnumScheduleType.Sunday && rule.SCHEDULE_TYPE >= (int)EnumScheduleType.Monday)
                throw new ApplicationException("场次规则类型有误！");
            DateTime curStart = rule.START_DATE >= start ? rule.START_DATE : start;
            DateTime curEnd = rule.END_DATE <= end ? rule.END_DATE : end;
            while (curStart <= curEnd)
            {
                //List<field_scheduled> events = targetList.FindAll(s => 
                //    s.FIELD_ID == rule.FIELD_ID && s.FIELD_ITEM_ID == s.FIELD_ITEM_ID
                //    && s.RULE_TYPE == (int)EnumRuleType.Event && s.STATUS == (int)EnumFieldStatus.Available && s.SCHEDULED_DATE == curStart
                //    && (
                //        (s.START_TIME <= rule.START_TIME && s.END_TIME >= rule.START_TIME)
                //        || (s.START_TIME <= rule.END_TIME && s.END_TIME >= rule.END_TIME)
                //        || (s.START_TIME <= rule.START_TIME && s.END_TIME >= rule.END_TIME)));
                targetList.RemoveAll(s =>
                    s.FIELD_ID == rule.FIELD_ID && s.FIELD_ITEM_ID == s.FIELD_ITEM_ID
                    && s.RULE_TYPE == (int)EnumRuleType.Event && s.STATUS == (int)EnumFieldStatus.Available && s.SCHEDULED_DATE == curStart
                    && (
                        (s.START_TIME <= rule.START_TIME && s.END_TIME >= rule.START_TIME)
                        || (s.START_TIME <= rule.END_TIME && s.END_TIME >= rule.END_TIME)
                        || (s.START_TIME <= rule.START_TIME && s.END_TIME >= rule.END_TIME)));
                targetList.Add(new FIELD_SCHEDULED()
                {
                    FIELD_ID = rule.FIELD_ID,
                    FIELD_ITEM_ID = rule.FIELD_ITEM_ID,
                    SCHEDULED_DATE = curStart,
                    PRICE = rule.PRICE,
                    RULE_TYPE = rule.TYPE,
                    START_TIME = rule.START_TIME,
                    END_TIME = rule.END_TIME,
                    STATUS = (int)EnumFieldStatus.Void,
                    REMARK = rule.RULE_NAME
                });
                if (rule.SCHEDULE_TYPE == (int)EnumScheduleType.Once) break;
                else if (rule.SCHEDULE_TYPE == (int)EnumScheduleType.Daily) curStart = curStart.AddDays(1);
                else if (rule.SCHEDULE_TYPE == (int)EnumScheduleType.Mothly) curStart = curStart.AddMonths(1);
                else if (rule.SCHEDULE_TYPE == (int)EnumScheduleType.Yearly) curStart = curStart.AddYears(1);
                else throw new KeyNotFoundException("Type Key is not defined:" + rule.SCHEDULE_TYPE);
            }
        }

        public static List<FIELD_SCHEDULED> GetScheduledList(int fieldItemId, DateTime startDay, DateTime endDay)
        {
            List<FIELD_SCHEDULED> rel = new List<FIELD_SCHEDULED>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = context.FIELD_SCHEDULED.Where(s => s.FIELD_ITEM_ID == fieldItemId && s.SCHEDULED_DATE >= startDay.Date && s.SCHEDULED_DATE <= endDay.Date).OrderBy(s => s.SCHEDULED_DATE).ThenBy(s => s.START_TIME).ToList();
            }
            return rel;
        }

        public static FIELD_SCHEDULED GetScheduled(int id)
        {
            FIELD_SCHEDULED obj = null;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                obj = context.FIELD_SCHEDULED.Single(s => s.ID == id);
            }
            return obj;
        }

        #endregion
    }
}
