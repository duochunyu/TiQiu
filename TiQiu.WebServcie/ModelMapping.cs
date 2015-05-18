using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using TiQiu.Biz;
using TiQiu.Common.Cryptography;
using TiQiu.Model;
namespace TiQiu.WebServcie
{
    public static class ModelMapping
    {
        public static List<OrderLog> InitOrderLogList(List<DAL.FIELD_ORDER_LOG> list)
        {
            List<OrderLog> rel = new List<OrderLog>();
            list.ForEach(l => {
                rel.Add(new OrderLog() { ID = l.ID, OrderId = l.FIELD_ORDER_ID, FieldId = l.FIELD_ORDER.FIELD_ID, FieldItemId = l.FIELD_ITEM_ID, Message = l.OPERATION, LogDate = l.LOG_DATE });
            });
            return rel;
        }

        public static List<GameScheduled> InitGameScheduledList(List<DAL.GAME_SCHEDULED> sList)
        {
            List<GameScheduled> schList = new List<GameScheduled>();
            sList.ForEach(s => {
                schList.Add(InitGameScheduled(s));
            });
            return schList;
        }

        public static GameScheduled InitGameScheduled(DAL.GAME_SCHEDULED sch)
        {
            return new GameScheduled()
                {
                    ID = sch.ID,
                    Status = sch.STATUS,
                    GameId = sch.GAME_ID,
                    GameName = (sch.GAME_ROUND_GROUP != null && sch.GAME_ROUND_GROUP.GAME_ROUND != null && sch.GAME_ROUND_GROUP.GAME_ROUND.GAME != null) ? sch.GAME_ROUND_GROUP.GAME_ROUND.GAME.NAME : "",
                    GroupId = sch.GROUP_ID,
                    RoundId = sch.ROUND_ID,
                    FieldId = sch.FIELD_ID.GetValueOrDefault(0),
                    FieldItemId = sch.FIELD_ITEM_ID.GetValueOrDefault(0),
                    FieldName = sch.FIELD_ITEM_FULL_NAME,
                    GameDate = sch.GAME_DATE.Date,
                    StartTime = sch.START_TIME,
                    EndTime = sch.END_TIME,
                    TeamAId = sch.TEAM_A_ID,
                    TeamBId = sch.TEAM_B_ID,
                    TeamAName = sch.TEAM_A_NAME,
                    TeamBName = sch.TEAM_B_NAME,
                    TeamAScore = sch.TEAM_A_SCORE.GetValueOrDefault(0),
                    TeamBScore = sch.TEAM_A_SCORE.GetValueOrDefault(0),
                    TeamAGoal = sch.TEAM_A_GOAL.GetValueOrDefault(0),
                    TeamBGoal = sch.TEAM_B_GOAL.GetValueOrDefault(0),
                    TeamALogoUrl = FileManager.GetTeamLogo(sch.TEAM_A_ID),
                    TeamBLogoUrl = FileManager.GetTeamLogo(sch.TEAM_B_ID),
                    FieldOrderId = sch.FIELD_ORDER_ID.GetValueOrDefault(0),
                    EventList = sch.GAME_EVENT == null ? new List<GameEvent>() : InitGameEvent(sch.GAME_EVENT.ToList())
                };
        }

        public static List<GameRoundGroup> InitGameRoundGroup(List<DAL.GAME_ROUND_GROUP> gList)
        {
            List<GameRoundGroup> groupList = new List<GameRoundGroup>();
            gList.ForEach(g => {
                groupList.Add(new GameRoundGroup()
                {
                    ID = g.ID,
                    GameId = g.GAME_ID,
                    Name = g.NAME,
                    RoundId = g.ROUND_ID,
                    ScheduleList = g.GAME_SCHEDULED==null? new List<GameScheduled>() : InitGameScheduledList(g.GAME_SCHEDULED.ToList()),
                    TeamList = g.GAME_TEAM == null ? new List<GameTeam>() : InitGameTeam(g.GAME_TEAM.OrderBy(t=>t.RANK).ToList())
                });
            });
            return groupList;
        }

        public static List<GameTeam> InitGameTeam(List<DAL.GAME_TEAM> tlist)
        {
            List<GameTeam> teamList = new List<GameTeam>();
            tlist.ForEach(t => {
                teamList.Add(new GameTeam()
                {
                    ID = t.ID,
                    GameId = t.GAME_ID,
                    GroupId = t.GROUP_ID,
                    RoundId = t.ROUND_ID,
                    TeamId = t.TEAM_ID,
                    TeamName = t.TEAM_NAME,
                    TeamLogoUrl = FileManager.GetTeamLogo(t.TEAM_ID),
                    Goal = t.GOAL,
                    Conceded = t.CONCEDED,
                    Win = t.WIN,
                    Lose = t.LOSE,
                    Draw = t.DRAW,
                    Rank = t.RANK,
                    Round = t.ROUND,
                    Games = t.GAMES,
                    Score = t.SCORE
                });
            });
            return teamList;
        }

        public static List<GameRound> InitGameRound(List<DAL.GAME_ROUND> rlist)
        {
            List<GameRound> roundList = new List<GameRound>();
            rlist.ForEach(r => roundList.Add(new GameRound()
            {
                ID = r.ID,
                GameId = r.GAME_ID,
                Name = r.NAME,
                Brief = r.BRIEF,
                Start = r.START_DATE.Date,
                End = r.END_DATE.Date,
                Type = r.ROUND_TYPE,
                TypeName = ConstValue.GetGameRoundTypeName(r.ROUND_TYPE),
                GroupList = r.GAME_ROUND_GROUP == null? new List<GameRoundGroup>() : InitGameRoundGroup(r.GAME_ROUND_GROUP.OrderBy(g=>g.SORT).ToList())

            }));
            return roundList;
        }

        public static List<GameEvent> InitGameEvent(List<DAL.GAME_EVENT> elist){
            List<GameEvent> eventList = new List<GameEvent>();
            elist.ForEach(e => eventList.Add(new GameEvent()
            {
                ID = e.ID,
                GameId = e.GAME_ID,
                GroupId = e.GROUP_ID,
                RoundId = e.ROUND_ID,
                TeamId = e.TEAM_ID,
                Time = e.TIMESPAN,
                MemberName = e.MEMBER_NAME,
                MemberId = e.MEMBER_ID.GetValueOrDefault(0),
                PlayerNumber = e.PLAYER_NUMBER,
                RecordAccountId = e.RECORD_ID,
                ScheduledId = e.SCHEDULED_ID,
                RecordDate = e.RECORD_DATE,
                Type = e.TYPE
            }));
            return eventList;
        }

        public static List<File> InitFileList(List<DAL.FILE> flist)
        {
            List<File> files = new List<File>();
            flist.ForEach(f => {
                files.Add(new File() { ID = f.ID, Title = f.TITLE, FileUrl = FileManager.FILE_DOMAIN + f.PATH, Order = f.ORDER,LinkUrl= f.LINK_URL,OptType = f.OperationType.GetValueOrDefault(0),PkId = f.FK_ID });
            });
            return files;
        }

        public static Game InitGame(DAL.GAME g)
        {
            return new Game
                {
                    ID = g.ID,
                    Name = g.NAME,
                    Start = g.START_DATE.Date,
                    End = g.END_DATE.Date,
                    Brief = g.BRIEF,
                    Type = g.GAME_TYPE,
                    TypeName = "",
                    LogoUrl = FileManager.GetGameLogo(g.ID),
                    Images = InitFileList(FileManager.GetFileList(EnumFileType.Game_Slider_Pic, g.ID))
                };         
            
        }

        public static List<Game> InitGames(List<DAL.GAME> rlist)
        {
            List<Game> list = new List<Game>();
            foreach (var f in rlist)
            {
                list.Add(InitGame(f));
            }
            return list;
        }

        public static List<Field> InitFields(List<DAL.FIELD> fieldList)
        {
            List<Field> list = new List<Field>();
            foreach (var f in fieldList)
            {
                list.Add(InitField(f));
            }
            return list;
        }

        public static List<FieldWeekRule> InitFieldWeekRule(List<DAL.FIELD_RULE> rules)
        {
            List<FieldWeekRule> items = new List<FieldWeekRule>();
            rules.ForEach(r => {
                items.Add(new FieldWeekRule()
                {
                    ID = r.ID,
                    FieldId = r.FIELD_ID,
                    FieldItemId = r.FIELD_ITEM_ID,
                    FieldType = r.FIELD_TYPE,
                    DayOfWeek = r.SCHEDULE_TYPE,
                    Type = r.TYPE,
                    Start = r.START_TIME,
                    End = r.END_TIME,
                    Description = r.DESCRIPTION,
                    Price = r.PRICE,
                    Status = r.STATUS
                });
            });
            return items;
        }

        public static Field InitField(DAL.FIELD field)
        {
            var item = new Field();
            item.ID = field.ID;
            item.Name = field.NAME;
            item.Score = field.SCORE;
            item.Status = field.STATUS;
            // item.StatusText = ConstValue.GetStatusText(f.STATUS);
            item.TEL = field.TEL;
            item.Phone = field.PHONE;
            item.Level = field.LEVEL;
            item.Type = field.TYPE;
            
            item.HasBathroom = field.HAS_BATHROOM;
            item.L = field.L;
            item.B = field.B;
            item.Brief = field.BRIEF;
            item.BusinessesID = field.BUSINESSES_ID;
            item.BottomPrice = field.BOTTOM_PRICE;
            item.TopPrice = field.TOP_PRICE;
            item.Adress = field.ADRESS;
            item.AreaCode = field.AREA_CODE;
            item.AreaText = ConstValue.GetAreaText(field.AREA_CODE);
            item.Items = new List<FieldItem>();
            
            foreach (var i in field.FIELD_ITEM)
            {
                var subItem = new FieldItem()
                {
                    ID = i.ID,
                    Name = i.NAME,
                    Brief = i.BRIEF,
                    FieldID = i.FIELD_ID,
                    BusinessesID = i.BUSINESSES_ID,
                    Status = i.STATUS,
                    Level = i.LEVEL,
                    Type = i.TYPE
                };
                item.Items.Add(subItem);
            }

            item.PicPath = FileManager.GetFieldLogo(field.ID);
            item.Images = InitFileList(FileManager.GetFileList(EnumFileType.Field_Slider_Pic, field.ID)); 
            return item;
        }


        public static Account InitAcct(DAL.ACCOUNT acct)
        {
            if (acct == null) throw new ApplicationException("未获取到对应帐户信息！");

            return new Account
            {
                AccountID = acct.ID,
                MemberID = acct.MEMBER_ID,
                Name = acct.NAME,
                MemberName = acct.MEMBER.NAME,
                NickName = acct.MEMBER.NICK_NAME,
                Phone = acct.MEMBER.CELLPHONE,
                //Adress = acct.MEMBER.ADRESS,
                Brief = acct.MEMBER.BRIEF,
                Brithday = acct.MEMBER.BRITHDAY,
                Email = acct.MEMBER.EMAIL,
                //Fav_foot = acct.MEMBER.FAV_FOOT,
                //Fav_Star = acct.MEMBER.FAV_STAR,
                //Fav_Team = acct.MEMBER.FAV_TEAM,
                //Intro = acct.MEMBER.INTRO,
                //Level = acct.MEMBER.LEVLE,
                //PlayAge = acct.MEMBER.PLAY_AGE,
                //Score = acct.MEMBER.SCORE.GetValueOrDefault(0),
                //Work = acct.MEMBER.WORK,
                Feature = acct.MEMBER.FEATURE,               
                Title = acct.MEMBER.TITLE,                
                Position = acct.MEMBER.POSITION,
                Sex = acct.MEMBER.SEX.GetValueOrDefault(0),
                Portrait = FileManager.GetMemberPic(acct.MEMBER_ID),
                Role = acct.ROLE
            };
        }

        public static AccountB InitAcct(DAL.ACCOUNT_B acct)
        {
            if (acct == null) throw new ApplicationException("未获取到对应帐户信息！");
            return new AccountB
            {
                ID = acct.ID,
                Name = acct.NAME,
                NickName = acct.NAME,                
                FieldList = ModelMapping.InitFields(FieldManager.GetFieldList(acct.ID))
            };
        }

        //public static List<OrderView> InitOrderInfo(List<DAL.V_FIELD_ORDER> orders)
        //{
        //    List<OrderView> rList = new List<OrderView>();
        //    foreach (var order in orders)
        //    {
        //        rList.Add(InitOrderView(order));

        //    }
        //    return rList;
        //}

        //public static OrderView InitOrderView(DAL.V_FIELD_ORDER order)
        //{
        //    var o = new OrderView
        //       {
        //           ID = order.ID,
        //           FieldId = order.FIELD_ID,
        //           FieldItemId = order.FIELD_ITEM_ID,
        //           FieldName = order.FIELD_NAME,
        //           FieldItemName = order.FIELD_ITEM_NAME,
        //           FieldType = order.FIELD_ITEM_TYPE,
        //           MemberBId = order.MEMBERB_ID.GetValueOrDefault(0),
        //           MemberBName = order.MEMBERB_NAME,
        //           MemberId = order.MEMBER_ID,
        //           MemberName = order.MEMBER_NAME,
        //           Remark = order.REMARK,
        //           ScheduledId = order.FIELD_SCHEDULED_ID,
        //        //   BusinessId = order.BUSINESSES_ID,
        //           CreateDate = order.CREATE_DATE,
        //           OrderDate = order.ORDER_DATE,
        //           Start = order.START_TIME,
        //           End = order.END_TIME,

        //           Price = order.PRICE.GetValueOrDefault(0),
        //           Payment = order.INCOME.GetValueOrDefault(),
        //      //     Score = order.SCORE.GetValueOrDefault(),
        //           Status = order.STATUS,
        //           StatusText = ConstValue.GetStatusText(order.STATUS),
        //           Type = order.TYPE,
        //           TypeText = ConstValue.GetOrderTypeText(order.TYPE),
        //           NeedReferee = order.NEED_REFEREE,
        //           FieldLogoUrl = FileManager.GetFieldLogo(order.FIELD_ID)                 
                   
        //       };
        //    if(o.Type == (int)EnumOrderType.PK ){
        //        DAL.TEAM_SCORE teamScore = OrderManager.GetTeamScore(o.ID);
        //        o.TeamId = teamScore.TEAM_A_ID;
        //        o.TeamName = teamScore.TEAM_A_NAME;
        //        o.TeamBId = teamScore.TEAM_B_ID.GetValueOrDefault(0);
        //        o.TeamBName = teamScore.TEAM_B_NAME;
                
        //    }
        //    return o;
        //}

        public static List<OrderPKView> InitOrderPKInfo(List<DAL.VIEW_ORDER_PK> orders)
        {
            List<OrderPKView> rList = new List<OrderPKView>();
            foreach (var order in orders)
            {
                rList.Add(new OrderPKView
                {
                    OrderId = order.ORDER_ID,
                    FieldId = order.FIELD_ID,
                    FieldItemId = order.FIELD_ITEM_ID,
                    FieldName = order.FIELD_NAME,
                    FieldItemName = order.FIELD_ITEM_NAME,
                    FieldItemTypeText = ConstValue.GetFieldItemTypeText(order.FIELD_ITEM_TYPE),
                    MemberBId = order.MEMBERB_ID.GetValueOrDefault(0),                    
                    MemberId = order.MEMBER_ID,                    
                    Remark = order.REMARK,
                    ScheduledId = order.FIELD_SCHEDULED_ID,
                    CreateDate = order.CREATE_DATE,
                    OrderDate = order.ORDER_DATE,
                    Start = order.START_TIME,
                    End = order.END_TIME,

                    Price = order.PRICE.GetValueOrDefault(0),
                    Payment = order.INCOME.GetValueOrDefault(),
                    Score = "",
                    Status = order.STATUS,
                    StatusText = ConstValue.GetStatusText(order.STATUS),
                    Type = order.TYPE,
                    TypeText = ConstValue.GetOrderTypeText(order.TYPE),
                    NeedReferee = order.NEED_REFEREE,
                    FieldLogoUrl = FileManager.GetFieldLogo(order.FIELD_ID),
                    TeamId = order.TEAM_A_ID,
                    TeamName = order.TEAM_A_NAME,
                    TeamBId = order.TEAM_B_ID.GetValueOrDefault(),
                    TeamBName = order.TEAM_B_NAME,
                    TeamColor = order.TEAM_A_COLOR,
                    TeamBColor = order.TEAM_B_COLOR,
                    PKPayType = order.PK_PAY_TYPE.GetValueOrDefault(-1),
                    PKPayTypeText = ConstValue.GetPKPayTypeText(order.PK_PAY_TYPE.GetValueOrDefault(-1)),
                    TeamLogoUrl = FileManager.GetTeamLogo(order.TEAM_A_ID),
                    TeamBLogoUrl = FileManager.GetTeamLogo(order.TEAM_B_ID.GetValueOrDefault(0))
                    
                });

            }
            return rList;
        }

        public static List<OrderFreeTeamView> InitOrderFreeTeamInfo(List<DAL.VIEW_ORDER_FREETEAM> orders)
        {
            List<OrderFreeTeamView> rList = new List<OrderFreeTeamView>();
            foreach (var order in orders)
            {
                rList.Add(new OrderFreeTeamView
                {
                    OrderId = order.ORDER_ID,
                    FieldId = order.FIELD_ID,
                    FieldItemId = order.FIELD_ITEM_ID,
                    FieldName = order.FIELD_NAME,
                    FieldItemName = order.FIELD_ITEM_NAME,
                    FieldItemTypeText = ConstValue.GetFieldItemTypeText(order.FIELD_ITEM_TYPE),
                    Remark = order.REMARK,
                    ScheduledId = order.FIELD_SCHEDULED_ID,
                    CreateDate = order.CREATE_DATE,
                    OrderDate = order.ORDER_DATE,
                    Start = order.START_TIME,
                    End = order.END_TIME,
                    PlayerCount = order.PLAYER_COUNT.GetValueOrDefault(0),
                    MinPlayer = order.FREE_TEAM_MIN_PLAYER.GetValueOrDefault(0),
                    FieldItemType = order.FIELD_ITEM_TYPE,
                    ExpireDate = order.EXPIRE_DATE,

                    Status = order.STATUS,
                    StatusText = ConstValue.GetStatusText(order.STATUS),
                    Type = order.TYPE,
                    TypeText = ConstValue.GetOrderTypeText(order.TYPE),
                    NeedReferee = order.NEED_REFEREE,
                    FieldLogoUrl = FileManager.GetFieldLogo(order.FIELD_ID),
                   
                    Price = order.PRICE.GetValueOrDefault(0),
                    PriceUnit = order.PRICE_UNIT.GetValueOrDefault(-1),
                    PriceUnitText = ConstValue.GetFreeTeamPriceUnitText(order.PRICE_UNIT.GetValueOrDefault(-1))
                });

            }
            return rList;
        }

        public static List<Member> InitMemberInfo(List<DAL.MEMBER> mList)
        {
            List<Member> rList = new List<Member>();
            foreach (var m in mList)
            {
                var info = InitMemberInfo(m);
                rList.Add(info);
            }
            return rList;
        }

        public static Member InitMemberInfo(DAL.MEMBER m)
        {
            return new Member
            {
                MemberID = m.ID,
                AccountID = 0,
                Name = m.NAME,
                NickName = m.NICK_NAME,
                Phone = m.CELLPHONE,
                Sex = m.SEX.GetValueOrDefault(0),
                Feature = m.FEATURE,
                Title = m.TITLE,
                Brithday = m.BRITHDAY.GetValueOrDefault(DateTime.Now),
                Email = m.EMAIL,
                Position = m.POSITION,
                Portrait = FileManager.GetMemberPic(m.ID)
            };
        }


        public static List<Team> InitTeamInfo(List<DAL.TEAM> tList)
        {
            List<Team> rList = new List<Team>();
            foreach (var t in tList)
            {
                var info = InitTeamInfo(t);                
                rList.Add(info);
            }
            return rList;
        }

        public static Team InitTeamInfo(DAL.TEAM t)
        {
            var info = new Team
            {
                ID = t.ID,
                Name = t.NAME,
                BuildDate = t.BUILD_DATE,
                Brief = t.BRIEF,
                Declaration = t.DECLARATION,
                Feature = t.FEATURE,
                Win = t.WIN,
                Lose = t.LOSE,
                Draw = t.DRAW,
                Level = t.LEVEL,
                Score = t.SCORE,
                LogoUrl = FileManager.GetTeamLogo(t.ID)
            };
            info.TeamMemberList = new List<TeamMember>();
            foreach (var m in t.TEAM_MEMBER)
            {
                info.TeamMemberList.Add(new TeamMember
                {
                    MemberId = m.MEMBER_ID,
                    MemberName = m.MEMBER.NAME,
                    Phone = m.MEMBER.CELLPHONE,
                    RoleId = m.ROLE_ID,
                    RoleName = ConstValue.GetTeamMemberRoleName(m.ROLE_ID),
                    TeamNumber = m.TEAM_NUMBER,
                    Position = m.POSITION,
                    Portrait = FileManager.GetMemberPic(m.MEMBER_ID)

                });
            }
            return info;
        }

        public static List<TeamScore> InitTeamScoreInfo(List<DAL.TEAM_SCORE> tList)
        {
            List<TeamScore> rList = new List<TeamScore>();
            foreach (var t in tList)
            {
                rList.Add(new TeamScore()
                {
                    OrderId = t.FIELD_ORDER_ID,
                    TeamAId = t.TEAM_A_ID,
                    TeamAName = t.TEAM_A_NAME,
                    TeamAColor = t.TEAM_A_COLOR,
                    TeamBId = t.TEAM_B_ID,
                    TeamBName = t.TEAM_B_NAME,
                    TeamBColor = t.TEAM_B_COLOR,
                    TeamAScore = t.TEAM_A_SCORE,
                    Win = t.RECEIVE_SCORE,
                    Lose = t.LOSE_SCORE,
                    OrderDate = t.ORDER_DATE,
                    SCORE = t.SCORE,
                    EndTime = t.END_TIME,
                    StartTime = t.START_TIME                    
                    
                });
            }
            return rList;
        }

        public static List<GameComment> InitGameComment(List<DAL.GAME_COMMENT> cList)
        {
            List<GameComment> rList = new List<GameComment>();
            foreach (var t in cList)
            {
                rList.Add(new GameComment()
                {
                    ID = t.ID,
                    MsgId = t.MSG_ID,
                    GameId = t.GAME_ID,
                    ScheduledId = t.GAME_SCHEDULED_ID,
                    Comment = t.COMMENT,
                    CreateDate = t.CREATE_DATE,
                    MemberId = t.MEMBER_ID,
                    MemberName = t.MEMBER_NAME
                });
            }
            return rList;
        }
    }
}