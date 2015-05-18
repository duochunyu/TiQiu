var _drag = false
var _getElement = new Function('obj', 'return document.getElementById(obj);')
var _oevent = new Function('e', 'if (!e) e = window.event;return e')
var _callback = null;
var _height = null;


//执行回调函数
function CallBack() {
    if (_callback != null) {
        _callback();
    }
}

//执行回调函数
function CallBackWithArgs(args) {
    if (_callback != null) {
        _callback(args);
    }
}

//关闭弹出层
function CloseDialog() {
    $("#floatBoxBg").animate({ opacity: "0" }, "normal");
    $("#floatBox").animate({ top: ($(document).scrollTop() - (_height == "auto" ? 300 : parseInt(_height))) + "px" }, "normal");
    $("#floatBox").remove();
    $("#floatBoxBg").remove();
}
function CloseCallBackDialog() {
   
    var tempCallBack = this._callback;
    
    this.CloseDialog();
    if (tempCallBack != null) {
        
        tempCallBack();
    }
}
function CloseCallBackDialogWithArgs(args) {    ;
    var tempCallBack = this._callback;
    this.CloseDialog();
    if (tempCallBack != null) {
        tempCallBack(args);
    }
}

//弹出层
function dialog(title, content, width, height, callback, isTransparent) {

    _callback = callback;
    _height = height;
    var temp_float = new String;

    if (height == "auto") {
        tempTop = "40%";
    }
    else {
        tempTop = ($(document).height() - parseInt(height)) / 2 + "px";
    }

    temp_float = '<div id="floatBoxBg" style="height:"></div>';
    temp_float += '<div id="floatBox" class="floatBox" style="width:' + width + 'px;left:' + ($(document).width() - parseInt(width)) / 2 + 'px;top:' + tempTop + '">';
    //temp_float += '<div id="titleBar" class="title"><h4></h4><img title="关闭" src="Images/Dialog/close_2.jpg"></div>';
    temp_float += '<div id="titleBar" class="title"><h4></h4></div>';
    temp_float += '<div class="content"></div>';
    temp_float += '</div>';

    $("body").append(temp_float);

    if (isTransparent == "Y") {
        $("#floatBoxBg").addClass("isTransparent");
    }
    if (isTransparent == "N") {
        $("#floatBoxBg").addClass("isNotTransparent");
    }

    //关闭
    $("#floatBox .title img").click(function() {
        CloseDialog();
    });

    //鼠标移向关闭按钮
    $("#floatBox .title img").mouseover(function() {
        $(this).attr("src", "Images/Dialog/close_1.jpg");
    });

    //鼠标移开关闭按钮
    $("#floatBox .title img").mouseout(function() {
        $(this).attr("src", "Images/Dialog/close_2.jpg");
    });

    //移动
    $("#titleBar").mouseover(function() {
        _moveDialog("floatBox", "titleBar");
    });

    $("#floatBox .title h4").html(title);
    contentType = content.substring(0, content.indexOf(":"));
    content = content.substring(content.indexOf(":") + 1, content.length);

    switch (contentType) {
        case "url":
            var content_array = content.split("?");
            $("#floatBox .content").ajaxStart(function() {
                $(this).html("loading...");
            });
            $.ajax({
                type: content_array[0],
                url: content_array[1],
                data: content_array[2],
                error: function() {
                    $("#floatBox .content").html("error...");
                },
                success: function(html) {
                    $("#floatBox .content").html(html);
                }
            });
            break;
        case "text":
            $("#floatBox .content").html(content);
            break;
        case "id":
            $("#floatBox .content").html($("#" + content + "").html());
            break;
        case "iframe":
            $("#floatBox .content").html("<iframe src=\"" + content + "\" width=\"100%\" height=\"" + (parseInt(height) - 27) + "px" + "\" scrolling=\"auto\" frameborder=\"0\" marginheight=\"0\" marginwidth=\"0\"></iframe>");
            break;
    }
}

//移动弹出窗口
function _moveDialog(obj, objhandle) {
    var x, y;
    _getElement(objhandle).onmousedown = function(e) {
        _drag = true;
        with (_getElement(obj)) {
            
            //style.position = "absolute";
            
            var temp1 = offsetLeft;
            var temp2 = offsetTop;
            x = _oevent(e).clientX; y = _oevent(e).clientY;
            document.onmousemove = function(e) {
                if (!_drag) return false;
                with (this) {
                    style.left = temp1 + _oevent(e).clientX - x + "px";
                    style.top = temp2 + _oevent(e).clientY - y + "px";
                }
            }
        }
        document.onmouseup = new Function("_drag=false");
    }
}