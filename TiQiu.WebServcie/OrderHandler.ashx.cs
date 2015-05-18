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
    /// OrderHandler 的摘要说明
    /// </summary>
    public class OrderHandler : IHttpHandler, IRequiresSessionState 
    {
        HttpProcessor apj;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            apj = new HttpProcessor();
            apj.ActionHandlerMaps.Add("OrderNormal", OrderNormal);
            apj.ActionHandlerMaps.Add("OrderBatchByManager", OrderBatchByManager);
            apj.ActionHandlerMaps.Add("OrderPK", OrderPK);
            apj.ActionHandlerMaps.Add("OrderPKByManager", OrderPKByManager);
            apj.ActionHandlerMaps.Add("AcceptPK", AcceptPK);
            apj.ActionHandlerMaps.Add("OrderFreeTeam", OrderFreeTeam);
            apj.ActionHandlerMaps.Add("JoinFreeTeam", JoinFreeTeam);
            apj.ActionHandlerMaps.Add("AcceptOrder", AcceptOrder);
            apj.ActionHandlerMaps.Add("CancelOrderByMember", CancelOrderByMember);
            apj.ActionHandlerMaps.Add("CancelOrderByAccountB", CancelOrderByAccountB);
            apj.ActionHandlerMaps.Add("OrderNormalByManager", OrderNormalByManager);
            apj.ActionHandlerMaps.Add("ConfirmOrder", ConfirmOrder);
            apj.ActionHandlerMaps.Add("CheckInOrder", CheckInOrder);
            apj.ActionHandlerMaps.Add("GetMyOrder", GetMyOrder);
            apj.ActionHandlerMaps.Add("JoinFreeTeamByManager", JoinFreeTeamByManager);
            apj.ActionHandlerMaps.Add("GetPKList", GetPKList);
            apj.ActionHandlerMaps.Add("GetFreeTeamList", GetFreeTeamList);
            apj.ActionHandlerMaps.Add("GetOrderByID", GetOrderByID);
            apj.ActionHandlerMaps.Add("GetOrderRequestList", GetOrderRequestList);

            apj.ActionWithManager.Add("OrderNormalByManager");
            apj.ActionWithManager.Add("OrderFreeTeam");
            apj.ActionWithManager.Add("AcceptOrder");
            apj.ActionWithManager.Add("CheckInOrder");
            apj.ActionWithManager.Add("CancelOrderByAccountB");
            apj.ActionWithManager.Add("JoinFreeTeamByManager");
            apj.ActionWithManager.Add("OrderPKByManager");
            apj.ActionWithManager.Add("OrderBatchByManager");
            apj.ActionWithManager.Add("GetOrderRequestList"); 

            apj.ActionWithMember.Add("OrderNormal");
            apj.ActionWithMember.Add("AcceptPK");
            apj.ActionWithMember.Add("OrderPK");
            apj.ActionWithMember.Add("JoinFreeTeam");
            apj.ActionWithMember.Add("CancelOrderByMember");
            apj.ActionWithMember.Add("ConfirmOrder");
            apj.ActionWithMember.Add("GetMyOrder");
            apj.ActionWithMember.Add("GetOrderByID");
            
            apj.ProcessRequestHandler<ApplicationException>(context, ex => Log.WriteException(ex));
        }

        private void OrderNormalByManager(HttpContext context, ref object userData)
        {
            int scheduledId = Util.GetParamter<int>(context.Request, "scheduledId");
            string phone = Util.GetParamter<string>(context.Request, "phone");
            int accountBId = Util.GetParamter<int>(context.Request, "accountBId");
            bool needReferee = Util.GetParamter<bool>(context.Request, "needReferee", false);
            string name = Util.GetParamter<string>(context.Request, "name",false);
            OrderManager.OrderNormalByManager(scheduledId, accountBId, phone,name, needReferee);
        }

        private void OrderBatchByManager(HttpContext context, ref object userData)
        {
            int scheduledId = Util.GetParamter<int>(context.Request, "scheduledId");
            string phone = Util.GetParamter<string>(context.Request, "phone");
            int accountBId = Util.GetParamter<int>(context.Request, "accountBId");
            DateTime start = Util.GetParamter<DateTime>(context.Request, "start");
            DateTime end = Util.GetParamter<DateTime>(context.Request, "end");
            string remark = Util.GetParamter<string>(context.Request, "remark");
            int teamId = Util.GetParamter<int>(context.Request, "teamId", false);
            string name = Util.GetParamter<string>(context.Request, "name", false);
            OrderManager.OrderBatchByManager(scheduledId, accountBId,start,end, phone, name,teamId,remark);
        }

        private void OrderNormal(HttpContext context, ref object userData)
        {
            
            int scheduledId = Util.GetParamter<int>(context.Request,"scheduledId");
            int memberId = Util.GetParamter<int>(context.Request, "memberId");

            bool needReferee = Util.GetParamter<bool>(context.Request, "needReferee",false);
//            int accoutbId = Util.GetParamter<int>(context.Request, "accountBId", false);
            int memberBId = Util.GetParamter<int>(context.Request, "memberbId", false);
            int teamId = Util.GetParamter<int>(context.Request, "teamId", false);
            int teamBId = Util.GetParamter<int>(context.Request, "teambId", false); ;
            string colthColor = Util.GetParamter<string>(context.Request, "colthcolor",false);
            string colthBColor = Util.GetParamter<string>(context.Request, "colthbcolor", false);


            OrderManager.OrderNormal(scheduledId, memberId, needReferee, memberBId, teamId, teamBId, colthColor, colthBColor);

        }
        
        private void OrderPK(HttpContext context, ref object userData)
        {
            var scheduledId = Util.GetParamter<int>(context.Request, "scheduledId");
            var memberId =Util.GetParamter<int>(context.Request, "memberId");
            var teamId = Util.GetParamter<int>(context.Request, "teamId");

            var needReferee = Util.GetParamter<bool>(context.Request, "needReferee",false);            
            var teamBId = Util.GetParamter<int>(context.Request, "teambId",false);
            var colthColor = Util.GetParamter<string>(context.Request, "colthcolor",false);
            var payType = Util.GetParamter<int>(context.Request, "paytype",false);
            var pay = EnumPKPayType.AA;
            if (payType > 0) pay = (EnumPKPayType)payType;

            OrderManager.OrderPK(scheduledId, needReferee, memberId, teamId, colthColor, pay, teamBId);

        }

        private void OrderPKByManager(HttpContext context, ref object userData)
        {
            var scheduledId = Util.GetParamter<int>(context.Request, "scheduledId");
            var accountId = Util.GetParamter<int>(context.Request, "accountId");
            var teamId = Util.GetParamter<int>(context.Request, "teamId");
            var memberId = Util.GetParamter<int>(context.Request, "memberId");

            var needReferee = Util.GetParamter<bool>(context.Request, "needReferee", false);
            var teamBId = Util.GetParamter<int>(context.Request, "teambId", false);
            var colthColor = Util.GetParamter<string>(context.Request, "colthcolor", false);
            var colthBColor = Util.GetParamter<string>(context.Request, "colthbcolor", false);
            var payType = Util.GetParamter<int>(context.Request, "paytype", false);
            var pay = EnumPKPayType.AA;
            if (payType > 0) pay = (EnumPKPayType)payType;

            OrderManager.OrderPKByManager(scheduledId,accountId,needReferee, memberId, teamId, colthColor, pay, teamBId);

        }

        private void AcceptPK(HttpContext context, ref object userData)
        {
            var orderId = Util.GetParamter<int>(context.Request, "orderid");
            var memberId = Util.GetParamter<int>(context.Request, "memberid");
            var teamBId = Util.GetParamter<int>(context.Request, "teambId");

            var accountBId = Util.GetParamter<int>(context.Request, "accountbid", false);
            
            var colthColor = Util.GetParamter<string>(context.Request, "colthcolor", false);


            OrderManager.AcceptPK(orderId, memberId, teamBId, accountBId, colthColor);

        }
        
        private void OrderFreeTeam(HttpContext context, ref object userData)
        {
            var scheduledId = Util.GetParamter<int>(context.Request, "scheduledId");
            var accountBId = Util.GetParamter<int>(context.Request, "accountbid");

            var price = Util.GetParamter<decimal>(context.Request, "price", false);
            var priceUnit = Util.GetParamter<int>(context.Request, "priceUnit", false);
            var minPlayerCount = Util.GetParamter<int>(context.Request, "minPlayerCount", false);

            
            EnumPriceUnit unit = EnumPriceUnit.PerPlayer;
            if (priceUnit > 0)
            {
                unit = (EnumPriceUnit)priceUnit;
            }

            OrderManager.OrderFreeTeam(scheduledId, accountBId, price, unit, minPlayerCount);

        }
        
        private void JoinFreeTeam(HttpContext context, ref object userData)
        {
            var orderId = Util.GetParamter<int>(context.Request, "orderId");
            var memberId = Util.GetParamter<int>(context.Request, "memberId");
            var playerCount = Util.GetParamter<int>(context.Request, "playerCount");

            OrderManager.JoinFreeTeam(orderId, memberId, playerCount);
        }

        private void JoinFreeTeamByManager(HttpContext context, ref object userData)
        {
            var orderId = Util.GetParamter<int>(context.Request, "orderId");
            var accountBId = Util.GetParamter<int>(context.Request, "accountBid");
            var phone = Util.GetParamter<string>(context.Request, "phone");
            var playerCount = Util.GetParamter<int>(context.Request, "playerCount");
            var name = Util.GetParamter<string>(context.Request, "name", false);

            OrderManager.JoinFreeTeamByManager(orderId,accountBId,phone,name,playerCount);
        }

        private void AcceptOrder(HttpContext context, ref object userData)
        {
            var orderId = Util.GetParamter<int>(context.Request, "orderId");
            var accountBId = Util.GetParamter<int>(context.Request, "accountBId");

            var remark = Util.GetParamter<string>(context.Request, "remark",false);
            var expire = Util.GetParamter<DateTime>(context.Request, "expireDate", false);
            OrderManager.AcceptOrder(orderId, accountBId, remark, expire);
        }

        private void CancelOrderByMember(HttpContext context, ref object userData)
        {
            var orderId = Util.GetParamter<int>(context.Request, "orderId");
            var memberId = Util.GetParamter<int>(context.Request, "memberId");

            var remark = Util.GetParamter<string>(context.Request, "remark",false);
            OrderManager.CancelOrderByMember(orderId, memberId, remark);
        }

        private void CancelOrderByAccountB(HttpContext context, ref object userData)
        {
            var orderId = Util.GetParamter<int>(context.Request, "orderId");
            var accountBId = Util.GetParamter<int>(context.Request, "accountBId");

            var remark = Util.GetParamter<string>(context.Request, "remark",false);
            OrderManager.CancelOrderByManager(orderId, accountBId, remark);
        }
        
        private void ConfirmOrder(HttpContext context, ref object userData)
        {
            var orderId = Util.GetParamter<int>(context.Request, "orderId");
            var memberId = Util.GetParamter<int>(context.Request, "memberId");

            OrderManager.ConfirmOrder(orderId, memberId);
        }

        private void CheckInOrder(HttpContext context, ref object userData)
        {
            var orderId = Util.GetParamter<int>(context.Request, "orderId");
            var accountBId = Util.GetParamter<int>(context.Request, "accountBId");
            var income = Util.GetParamter<decimal>(context.Request, "income");

            var receive = Util.GetParamter<int>(context.Request, "receive", false);
            var lost = Util.GetParamter<int>(context.Request, "lost", false);

           
            OrderManager.CheckInOrder(orderId, accountBId,income,receive,lost);
        }

        private void GetMyOrder(HttpContext context, ref object userData)
        {
            var memberId = Util.GetParamter<int>(context.Request, "memberId");
            var pageIndex = Util.GetParamter<int>(context.Request,"pageIndex");
            var pageSize = Util.GetParamter<int>(context.Request,"pageSize");
            var token = Util.GetParamter<decimal>(context.Request, "token");

            var from = Util.GetParamter<DateTime>(context.Request, "start", false);
            var to = Util.GetParamter<DateTime>(context.Request, "end", false); 

            int total = 0;

            List<MyOrderView> orders = OrderManager.GetMyOrderList(memberId,from,to, pageIndex, pageSize, out total);
            userData = new PageInfo<MyOrderView>() { ItemList = orders, Total = total };         
        }

        private void GetOrderByID(HttpContext context, ref object userData)
        {
            var orderId = Util.GetParamter<int>(context.Request, "orderId");
                       
            userData = OrderManager.GetOrderByID(orderId);
        }

        private void GetPKList(HttpContext context, ref object userData)
        {
            var pageIndex = Util.GetParamter<int>(context.Request, "pageIndex");
            var pageSize = Util.GetParamter<int>(context.Request, "pageSize");
            var start = Util.GetParamter<DateTime>(context.Request, "start",false);
            var end = Util.GetParamter<DateTime>(context.Request, "end",false);
            var areaCode = Util.GetParamter<string>(context.Request, "areaCode", false);
            var fieldType = Util.GetParamter<int>(context.Request, "fieldType", false);
            var pkType = Util.GetParamter<int>(context.Request, "pkStatus", false);
            int total = 0;
            
            List<DAL.VIEW_ORDER_PK> orders = OrderManager.GetOrderPKList(EnumFieldStatus.Booked,areaCode,fieldType,start,end,pkType, pageIndex, pageSize, out total);
            userData = new PageInfo<OrderPKView>() { ItemList = ModelMapping.InitOrderPKInfo(orders), Total = total };   

        }

        private void GetOrderRequestList(HttpContext context, ref object userData)
        {
            var pageIndex = Util.GetParamter<int>(context.Request, "pageIndex");
            var pageSize = Util.GetParamter<int>(context.Request, "pageSize");
            var accountbId = Util.GetParamter<int>(context.Request, "accountbId");
            int total = 0;
            var list = CacheManager.GetFieldIds(apj.CurToken);
            List<OrderView> orders = OrderManager.GetOrderList(list, EnumFieldStatus.Booking, null, null, pageIndex, pageSize, out total);
            //List<DAL.VIEW_ORDER_PK> orders = OrderManager.GetOrderPKList(EnumFieldStatus.Booked,areaCode,fieldType,start,end, pageIndex, pageSize, out total);
            userData = new PageInfo<OrderView>() { ItemList = orders, Total = total };   

        }        

        private void GetFreeTeamList(HttpContext context, ref object userData)
        {
            var pageIndex = Util.GetParamter<int>(context.Request, "pageIndex");
            var pageSize = Util.GetParamter<int>(context.Request, "pageSize");
            var start = Util.GetParamter<DateTime>(context.Request, "start", false);
            var end = Util.GetParamter<DateTime>(context.Request, "end", false);
            var areaCode = Util.GetParamter<string>(context.Request, "areaCode", false);
            var fieldType = Util.GetParamter<int>(context.Request, "fieldType", false);
            int total = 0;
            List<DAL.VIEW_ORDER_FREETEAM> orders = OrderManager.GetOrderFreeTeamList(EnumFieldStatus.Booked,areaCode,fieldType, start, end, pageIndex, pageSize, out total);
            userData = new PageInfo<OrderFreeTeamView>() { ItemList = ModelMapping.InitOrderFreeTeamInfo(orders), Total = total };   

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