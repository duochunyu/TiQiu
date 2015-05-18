using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Model
{
    public class Game
    {
        public int ID {get;set;}
        public string Name {get;set;}
        public DateTime Start{get;set;}
        public DateTime End{get;set;}
        public int Type { get; set; }
        public string TypeName { get; set; }
        public string Brief { get; set; }
        public string LogoUrl { get; set; }
        public List<GameRound> RoundList { get; set; }
        public List<File> Images { get; set; }
    }

    public class GameRound
    {
        public int ID { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
        public string Brief { get; set; }
        public List<GameRoundGroup> GroupList { get; set; }
    }

    public class GameRoundGroup
    {
        public int ID { get; set; }
        public int GameId { get; set; }
        public int RoundId { get; set; }
        public string Name { get; set; }
        public List<GameScheduled> ScheduleList { get; set; }
        public List<GameTeam> TeamList { get; set; }
    }

    public class GameScheduled
    {
         public int ID { get; set; }
         public int Status { get; set; }
        public int GameId { get; set; }
        public string GameName { get; set; }
        public int RoundId { get; set; }
        public int GroupId {get;set;}
        public int TeamAId {get;set;}
        public int TeamBId {get;set;}
        public string TeamAName { get; set; }
        public string TeamBName { get; set; }
        public DateTime GameDate{get;set;}
        public TimeSpan StartTime{get;set;}
        public TimeSpan EndTime{get;set;}
        public int FieldId {get;set;}
        public string FieldName { get; set; }
        public int FieldItemId {get;set;}
        public int FieldOrderId {get;set;}
        public int TeamAGoal {get;set;}
        public int TeamBGoal {get;set;}
        public int TeamAScore {get;set;}
        public int TeamBScore {get;set;}
        public string TeamALogoUrl { get; set; }
        public string TeamBLogoUrl { get; set; }
        public List<GameEvent> EventList { get; set; }
    }

    public class GameTeam
    {
        public int ID { get; set; }
        public int GameId { get; set; }
        public int RoundId { get; set; }
        public int GroupId {get;set;}
        public int TeamId {get;set;}
        public string TeamName{get;set;}
        public string TeamLogoUrl { get; set; }
        public int Score{get;set;}
        public int Rank {get;set;}
        public int Goal{get;set;}        
        public int Conceded {get;set;}
        public int Win {get;set;}
        public int Lose {get;set;}
        public int Draw {get;set;}
        /// <summary>
        /// 第几轮
        /// </summary>
        public int Round{get;set;}
        /// <summary>
        /// 已完成比赛场次
        /// </summary>
        public int Games {get;set;}

    }

    public class GameEvent {
        public int ID { get; set; }
        public int GameId { get; set; }
        public int RoundId { get; set; }
        public int GroupId { get; set; }
        public int ScheduledId { get; set; }
        public int TeamId { get; set; }
        public int MemberId { get; set; }
        public int Type { get; set; }
        public int PlayerNumber {get;set;}
        public TimeSpan Time {get;set;}
        public int RecordAccountId {get;set;}
        public DateTime RecordDate {get;set;}
        public string MemberName {get;set;}
    }

}