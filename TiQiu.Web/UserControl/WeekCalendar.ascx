<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WeekCalendar.ascx.cs" Inherits="TiQiu.Web.UserControl.WeekCalendar" %>

<div><button onclick="setStartEnd">Test</button></div>
<div id="<%= this.ClientCalendarDivID %>"></div>
<script>
    var year = new Date().getFullYear();
    var month = new Date().getMonth();
    var day = new Date().getDate();

    function setStartEnd() {
        $('#<%=this.ClientCalendarDivID %>').weekCalendar('option', 'businessHours').end = 23;
        $('#<%=this.ClientCalendarDivID %>').weekCalendar('refresh');
    }

    $(document).ready(function () {

        $('#<%=this.ClientCalendarDivID %>').weekCalendar({
            use24Hour : true,
            buttonText: {today : "今天", lastWeek : "上周", nextWeek : "下周"},
            shortMonths: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
            longMonths: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
            shortDays: ['一', '二', '三', '四', '五', '六', '日'],
            longDays: ['周一', '周二', '周三', '周四', '周五', '周六', '周日'],
            timeslotsPerHour: 2,
            timeslotHeight: 15,
            defaultEventLength: 3,
            dateFormat:"Y-m-d",
            height: function ($<%=this.ClientCalendarDivID %>) {
               return 600;
            },
            businessHours: { start: 9, end: 22, limitDisplay: true },

            eventRender: function (calEvent, $event) {
                if (calEvent.end.getTime() < new Date().getTime()) {
                    $event.css("backgroundColor", "#aaa");
                    $event.find(".time").css({ "backgroundColor": "#999", "border": "1px solid #888" });
                }
            },
            readonly:true,
            eventDrop: function (calEvent, $event) {
                displayMessage("<strong>Moved Event</strong><br/>Start: " + calEvent.start + "<br/>End: " + calEvent.end);
            },
            eventResize: function (calEvent, $event) {
                displayMessage("<strong>Resized Event</strong><br/>Start: " + calEvent.start + "<br/>End: " + calEvent.end);
            },
            eventClick: function (calEvent, $event) {
                alert("xx");
            },
            eventMouseover: function (calEvent, $event) {
                displayMessage("<strong>Mouseover Event</strong><br/>Start: " + calEvent.start + "<br/>End: " + calEvent.end);
            },
            eventMouseout: function (calEvent, $event) {
                displayMessage("<strong>Mouseout Event</strong><br/>Start: " + calEvent.start + "<br/>End: " + calEvent.end);
            },
            noEvents: function () {
                displayMessage("There are no events for this week");
            }
            //data: "events.json"
        });

        function displayMessage(message) {
            $("#message").html(message).fadeIn();
        }

        $("<div id=\"message\" class=\"ui-corner-all\"></div>").prependTo($("body"));

    });


</script>