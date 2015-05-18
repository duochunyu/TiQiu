
function InitFunc() {

    //表格样式
    $(".ui_table tbody tr").mouseover(function() {
    $(this).children().css("background-color", "#ADDFFF");
    });
    $(".spceilTd").addClass("wrapTd");
    $(".ui_table tbody tr").mouseout(function() {
    $(this).children().css("background-color", "#FFFFFF");
    });
    $(".CloseDialog").click(function() {
        CloseDialog();
    });
    $(".CallBackDialog").click(function() {
        CallBackDialog();
    });

    //关闭Dialog
    $(".CloseDialog").click(function() {
        CloseDialog();
    });
    //执行Dialog回调
    $(".CallBackDialog").click(function() {
        CallBackDialog();
    });

    //双击清空
    $(".clearInput").dblclick(function() {
        $(this).val("");
    });
}

$(function() {
    InitFunc();
});

//弹出确认框
function ConfirmMessage(contorlID, confirmMessage) {
    var allowConfirmPose = parent.window.ConfirmMessage(contorlID, confirmMessage);
    return allowConfirmPose;
}

//弹出提示信息 msgType:correct/info/warn/error
function ShowMessage(msgType, msgContent) {    
    parent.window.ShowMessage(msgType, msgContent);  
}

//全选方法
function SelectAll(e) {
    if ($(e).attr("checked") == true) {
        $(".CheckItem input").attr("checked", true);
    }
    else {
        $(".CheckItem input").attr("checked", false);
    }
} 

//打开新Tab页
function OpenTabWindow(url, rel, title) {
    parent.window.OpenTabWindow(url, rel, title);
}

//弹出Dialog
function ShowDialog(url, title, width, height, callback) {
    parent.window.ShowDialog(url, title, width, height, callback);
}

//关闭Dialg
function CloseDialog() {
    parent.window.CloseDialog();
}

//执行Dialog回调函数
function CallBackDialog() {
    parent.window.CallBack();
}

//执行Dialog回调函数
function CallBackDialogWithArgs(args) {
    parent.window.CallBackWithArgs(args);
}

//关闭Dialog并实行回调函数
function CloseCallBackDialog() {
    parent.window.CloseCallBackDialog();
}

//关闭Dialog并实行回调函数
function CloseCallBackDialogWithArgs(args) {
    parent.window.CloseCallBackDialogWithArgs(args);
}

//弹出输入Dialog
function ShowInputDialog(url, title, width, height, callback) {
    parent.window.ShowInputDialog(url, title, width, height, callback);
}

//function glabalEnter() {
//    var $search; //遍历到的查询按钮
//    $("table:eq(0)").find("tr").each(function() {
//        $(this).find("td").each(function() {
//            $(this).find("a").each(function() {
//                if ($(this).text().trim() == "查询") {
//                    $search = $(this);
//                }
//            });
//        });
//    });
//    if ($search == null) return;
//    var $inp = $("table:eq(0)").children();
//    $inp.bind('keydown', function(e) {
//        var key = e.which;
//        if (key == 13) {
//            e.preventDefault();
//            $search.focus();
//            document.location = $search.attr('href');
//        }
//    });
//}

//此方案解决一个页面多个查询按钮情况
function glabalEnter() {
    var i = $('table').size(); //获得table标签的数目
    for (var index = 0; index < i; index++) {
        $("table:eq(" + index + ")").find("a").each(function() {
            if ($(this).text().trim() == "查询") {
                var $search = $(this); //遍历到的查询按钮
                if ($search == null) return;
                var $inp = $(this).parents("table:first").children();
                $inp.bind('keydown', function(e) {
                    var key = e.which;
                    if (key == 13) {
                        e.preventDefault();
                        $search.focus();
                        document.location = $search.attr('href');
                        //alert($search.attr('id') + "   " + $(this).parents("table:first").html());
                    }
                });
            }
        });
    }
}

String.prototype.Trim = function() {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}