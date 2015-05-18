using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiQiu.DAL;
using TiQiu.Common.Util;

namespace TiQiu.Biz
{
    public static class MemberManager
    {
        /// <summary>
        /// 创建会员,必须要有手机号，默认创建或带入关联账号
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static MEMBER CreateMember(string name, string phone)
        {
            
            //string defaultPwd = Common.Cryptography.CryptoManager.GetCrypto(Common.Cryptography.CryptoAlgorithm.MD5).Encrypt(phone);
            MEMBER m;
            using (TIQIUEntities tes = new TIQIUEntities())
            {
                if (!Tools.IsMobile(phone)) throw new ApplicationException("Error Cell Phone!");
                if (CheckMemberByPhone(phone)) throw new ApplicationException("Exists same Cell Phone!");
                m = new MEMBER() { NAME = name, CELLPHONE = phone};
                
                tes.MEMBER.Add(m);
                tes.SaveChanges();
            }
            return m;
        }

        public static void UpdateTeamMemberInfo(int memberId, int teamId, int number, string position) {
            if(number <0) throw new ApplicationException("球衣号码不可为负数！");
            using (TIQIUEntities context = new TIQIUEntities())
            {
                TEAM_MEMBER tm = context.TEAM_MEMBER.SingleOrDefault(t => t.TEAM_ID == teamId && t.MEMBER_ID == memberId);
                if (tm == null) throw new ApplicationException("会员ID错误或不属于当前球队！");
                tm.TEAM_NUMBER = number;
                tm.POSITION = position;

                context.SaveChanges();
            }
        }

        public static void UpdateMember(int memberId, string name, string nickName, string position, string phone, string feature, string eMail,string title, int sex, DateTime brithday)
        {
            if (phone==null || !Tools.IsMobile(phone)) throw new ApplicationException("手机号错误!");
            using (TIQIUEntities context = new TIQIUEntities())
            {
                MEMBER mb = context.MEMBER.SingleOrDefault(b => b.ID == memberId);
                if (mb == null) throw new ApplicationException("未能获取正确的会员信息！");

                mb.FEATURE = feature;
                mb.CELLPHONE = phone;
                mb.EMAIL = eMail;
                mb.NAME = name;
                mb.NICK_NAME = nickName;
                mb.POSITION = position;
                mb.SEX = sex;
                mb.TITLE = title;
                mb.BRITHDAY = brithday.Year > 1930 ?  brithday.Date : DateTime.Now.Date;
                context.SaveChanges();

            }
        }

        public static void UpdateMember(MEMBER m)
        {
            if (!Tools.IsMobile(m.CELLPHONE)) throw new ApplicationException("Error Cell Phone!");
            using (TIQIUEntities context = new TIQIUEntities())
            {
                MEMBER mb = context.MEMBER.Single(b => b.ID == m.ID);
                mb.ADRESS = m.ADRESS;
                mb.AREA_CODE = m.AREA_CODE;
                mb.BRIEF = m.BRIEF;
                mb.CELLPHONE = m.CELLPHONE;
                mb.EMAIL = m.EMAIL;
                mb.FAV_FOOT = m.FAV_FOOT;
                mb.FAV_STAR = m.FAV_STAR;
                mb.FAV_TEAM = m.FAV_TEAM;
                mb.IS_TEAMLEADER = m.IS_TEAMLEADER;
                mb.LEVLE = m.LEVLE;
                mb.NAME = m.NAME;
                mb.NICK_NAME = m.NICK_NAME;
                mb.POSITION = m.POSITION;
                mb.SCORE = m.SCORE;

                context.SaveChanges();

            }
        }

        public static void AddFeedback(int memberId, string memberName, string phone, string eMail, string content)
        {
            FEEDBACK fb = new FEEDBACK() { MEMBER_ID = memberId, CONTENT = content, EMAIL = eMail, MEMBER_NAME = memberName };
            using (TIQIUEntities tes = new TIQIUEntities())
            {
                tes.FEEDBACK.Add(fb);
            }

        }

        //public static void BindAccount(MEMBER mb,int accountId)
        //{
        //    using (TIQIUEntities tes = new TIQIUEntities())
        //    {
        //        tes.MEMBERs.Attach(mb);
        //        mb.ACCOUNT_ID = accountId;
                
        //        tes.SaveChanges();
        //    }
        //}

        public static bool CheckMemberByPhone(string phone)
        {
            bool exists = false;
            using (TIQIUEntities tes = new TIQIUEntities())
            {
                exists = tes.MEMBER.Count<MEMBER>(a => a.CELLPHONE == phone) > 0;
            }
            return exists;
        }

        public static List<MEMBER> GetMemberByPhone(string phone)
        {
            List<MEMBER> m = new List<MEMBER>();
            using (TIQIUEntities tes = new TIQIUEntities())
            {
                m = tes.MEMBER.Where<MEMBER>(me => me.CELLPHONE.StartsWith(phone)).ToList();
            }
            return m;
        }

        public static MEMBER GetMemberById(int id)
        {
            MEMBER m = null;
            using (TIQIUEntities context = new TIQIUEntities())
            {
                m = context.MEMBER.SingleOrDefault(me => me.ID == id);
            }
            return m;
        }

        public static List<MEMBER> GetMemberByIds(int[] ids)
        {
            var list = new List<MEMBER>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                list = context.MEMBER.Where(m => ids.Contains(m.ID)).ToList();
            }
            return list;
        }

        /// <summary>
        /// 根据球队TeamId获取球队队员列表
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public static List<MEMBER> GetMemberList(System.Linq.Expressions.Expression<Func<MEMBER, bool>> condition, int pageIdx, int pageSize, out int totalCount)
        {
            totalCount = 0;
            List<MEMBER> rel = new List<MEMBER>();
            using (TIQIUEntities context = new TIQIUEntities())
            {
                rel = PagingQuery<MEMBER>.GetPagingList(context.MEMBER.AsQueryable(), condition, "NAME", true, pageIdx, pageSize, out totalCount);
            }
            return rel;
        }
    }
}
