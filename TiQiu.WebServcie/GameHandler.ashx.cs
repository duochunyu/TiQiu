using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using TiQiu.Biz;
using TiQiu.Model;
namespace TiQiu.WebServcie
{
    /// <summary>
    /// GameHandler 的摘要说明
    /// </summary>
    public class GameHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            HttpProcessor apj = new HttpProcessor();
            apj.ActionHandlerMaps.Add("GetGameInfo", GetGameInfo);
            apj.ActionHandlerMaps.Add("GetGameList", GetGameList);
            apj.ActionHandlerMaps.Add("GetGameRoundList", GetGameRoundList);
            apj.ActionHandlerMaps.Add("GetGameRoundGroupList", GetGameRoundGroupList);
            apj.ActionHandlerMaps.Add("GetGameScheduledList", GetGameScheduledList);
            apj.ActionHandlerMaps.Add("GetGameTeamList", GetGameTeamList);
            apj.ActionHandlerMaps.Add("GetGameEventList", GetGameEventList);
            apj.ActionHandlerMaps.Add("GetGameListForDropdown", GetGameListForDropdown);
            apj.ActionHandlerMaps.Add("GetGameToptenz", GetGameToptenz);            
                      
            apj.ActionHandlerMaps.Add("AddGameEvent", AddGameEvent);
            apj.ActionHandlerMaps.Add("DelGameEvent", DelGameEvent);
            apj.ActionHandlerMaps.Add("GetGameLiveList", GetGameLiveList);
            apj.ActionHandlerMaps.Add("GetGameLive", GetGameLive);
            apj.ActionHandlerMaps.Add("AddGameComment", AddGameComment);
            apj.ActionHandlerMaps.Add("DelGameComment", DelGameComment);
            apj.ActionHandlerMaps.Add("GetGameCommentList", GetGameCommentList);
            apj.ActionHandlerMaps.Add("GetNewGameCommentList", GetNewGameCommentList);  
            apj.ActionHandlerMaps.Add("GameOver", GameOver);

            apj.ActionWithMember.Add("GameOver");
            apj.ActionWithMember.Add("AddGameEvent");
            apj.ActionWithMember.Add("DelGameEvent");
            apj.ActionWithMember.Add("AddGameComment");
            apj.ActionWithMember.Add("DelGameComment");

            apj.ProcessRequestHandler<ApplicationException>(context, ex => Log.WriteException(ex));
        }

        private void GetGameToptenz(HttpContext context, ref object userData)
        {
            var pageIdx = Util.GetParamter<int>(context.Request, "pageindex");
            var pageSize = Util.GetParamter<int>(context.Request, "pagesize");
            var id = Util.GetParamter<int>(context.Request, "gameId");
            int totalCount = 0;
            var list = GameManager.GetGameToptenz(id,pageIdx, pageSize, out totalCount);
            userData = new PageInfo<MemberRank>() { ItemList = list, Total = totalCount };

        }
        private void GetGameList(HttpContext context, ref object userData)
        {
            var pageIdx = Util.GetParamter<int>(context.Request, "pageindex");
            var pageSize = Util.GetParamter<int>(context.Request, "pagesize");
            
            int totalCount = 0;
            var list = GameManager.GetGameList(pageIdx, pageSize, out totalCount);
            userData = new PageInfo<Game>() { ItemList = ModelMapping.InitGames(list), Total = totalCount };

        }

        private void GetGameListForDropdown(HttpContext context, ref object userData)
        {

            userData = GameManager.GetGameList();          

        }



        private void GetGameInfo(HttpContext context, ref object userData)
        {
            var id = Util.GetParamter<int>(context.Request, "id");
            userData = ModelMapping.InitGame(GameManager.GetGameInfo(id));
        }

        private void GetGameRoundList(HttpContext context, ref object userData)
        {
            var id = Util.GetParamter<int>(context.Request, "gameId");
            userData = ModelMapping.InitGameRound(GameManager.GetGameRoundList(id));
        }

        private void GetGameEventList(HttpContext context, ref object userData)
        {
            var id = Util.GetParamter<int>(context.Request, "schId");
            userData = ModelMapping.InitGameEvent(GameManager.GetGameEventList(id));
        }


        private void GetGameLive(HttpContext context, ref object userData)
        {
            var id = Util.GetParamter<int>(context.Request, "shid");
            userData = ModelMapping.InitGameScheduled(GameManager.GetGameLive(id)); 
        }
        
        private void GetGameLiveList(HttpContext context, ref object userData)
        {
            var from = Util.GetParamter<DateTime>(context.Request, "from");
            var to = Util.GetParamter<DateTime>(context.Request, "to");
            var gameId = Util.GetParamter<int>(context.Request, "gameid");
            var status = Util.GetParamter<int>(context.Request, "status");
            var pageIdx = Util.GetParamter<int>(context.Request, "pageindex");
            var pageSize = Util.GetParamter<int>(context.Request, "pagesize");

            int totalCount = 0;
            var list = GameManager.GetGameLiveList(from, to, gameId,status, pageIdx, pageSize, out totalCount);
            userData = new PageInfo<GameScheduled>() { ItemList = ModelMapping.InitGameScheduledList(list), Total = totalCount }; 
        }

        private void GetGameRoundGroupList(HttpContext context, ref object userData)
        {
            var id = Util.GetParamter<int>(context.Request, "roundId");
            userData = ModelMapping.InitGameRoundGroup(GameManager.GetGameRoundGroupList(id));
        }

        private void GetGameScheduledList(HttpContext context, ref object userData)
        {
            var groupid = Util.GetParamter<int>(context.Request, "groupId");
            userData = ModelMapping.InitGameScheduledList(GameManager.GetGameScheduledList(groupid));
        }

        private void GetGameTeamList(HttpContext context, ref object userData)
        {
            
            var groupid = Util.GetParamter<int>(context.Request, "groupId");
            userData = ModelMapping.InitGameTeam(GameManager.GetGameTeamList(groupid));
        }

        private void AddGameEvent(HttpContext context, ref object userData)
        {
            var schId = Util.GetParamter<int>(context.Request, "schId");
            var teamId = Util.GetParamter<int>(context.Request, "teamId");
           // var playerNum = Util.GetParamter<int>(context.Request, "playerNum");
            var type = Util.GetParamter<int>(context.Request, "type");
            var recordId = Util.GetParamter<int>(context.Request, "recordId");
            var memberId = Util.GetParamter<int>(context.Request, "memberId");
            var min = Util.GetParamter<int>(context.Request, "time");   
            //typeof(EnumGameEventType).GetEnumValues()
            GameManager.AddGameEvent(schId,teamId,memberId,type,recordId,min);
        }

        private void DelGameEvent(HttpContext context, ref object userData)
        {
            var id = Util.GetParamter<int>(context.Request, "eventId");
            GameManager.DelGameEvent(id);
        }

        private void GameOver(HttpContext context, ref object userData)
        {
            var schid = Util.GetParamter<int>(context.Request, "schid");
            GameManager.GameOver(schid);
        }

        private void AddGameComment(HttpContext context, ref object userData)
        {
            var schId = Util.GetParamter<int>(context.Request, "schId");
            var msgId = Util.GetParamter<string>(context.Request, "msgId");
            var memberId = Util.GetParamter<int>(context.Request, "memberid");
            var comment = Util.GetParamter<string>(context.Request, "comment");
          
            //typeof(EnumGameEventType).GetEnumValues()
            GameManager.AddGameComment(schId, memberId, comment, msgId);
        }

        private void DelGameComment(HttpContext context, ref object userData)
        {
            var id = Util.GetParamter<int>(context.Request, "id");
            var memberId = Util.GetParamter<int>(context.Request, "memberid");
            GameManager.DelGameComment(id, memberId);
        }

        private void GetGameCommentList(HttpContext context, ref object userData)
        {
            var schid = Util.GetParamter<int>(context.Request, "schid");
            var pageIdx = Util.GetParamter<int>(context.Request, "pageindex");
            var pageSize = Util.GetParamter<int>(context.Request, "pagesize");
            int totalCount = 0;
            var list = GameManager.GetGameCommentList(schid, pageIdx, pageSize, out totalCount);
            userData = new PageInfo<GameComment>() { ItemList = ModelMapping.InitGameComment(list), Total = totalCount }; 
        }

        private void GetNewGameCommentList(HttpContext context, ref object userData)
        {
            var id = Util.GetParamter<int>(context.Request, "schid");
            var lastid = Util.GetParamter<int>(context.Request, "lastid");
            userData = ModelMapping.InitGameComment(GameManager.GetGameCommentList(id, lastid));
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}