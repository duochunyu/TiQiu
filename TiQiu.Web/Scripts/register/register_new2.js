/*
 * 注册流程使用的js文件
 */

$(document).ready(function () {
   
    //清空刷新后文本框的值

    //禁止按回车;提交
    $("#register_form").keydown(function (event) {
        if (event.keyCode == 13) { return false; }
    });




    //注册，获取焦点提示---begin

    $("#uu_email").focus(function () {
        addTip($('#div_email'), '请填写手机号码');
    }).blur(function () {
        removeTip($('#div_email'));
    });

    $("#uu_password").focus(function () {
        addTip($(this).parent(), '6-20个字符,数字和字母均可');
    }).blur(function () {
        removeTip($(this).parent());
    });

    $("#uu_nickname").focus(function () {
        addTip($(this).parent(), '4-16个字符,汉字为两个字符，支持中英文和数字');
    }).blur(function () {
        removeTip($(this).parent());
    });
    
    $("#uu_confirmpassword").focus(function () {
        addTip($(this).parent(), '请再次输入密码');
    }).blur(function () {
        removeTip($(this).parent());
    });


    $("#uu_password").blur(function () {
        checkPassword();
    });

    $("uu_confirmpassword").blur(function() {
        checkconfirmpassword();
    });

    $("#uu_email").blur(function () {
        checkTelPhone();
    });


    $("#uu_nickname").blur(function () {
        checkNickname();
    });
    //注册，失去焦点数据检查---end

    $("#button_register_submit").bind("click", function () {    
        var result = register_form_onsubmit();
        var tip1 = '<p class="twarn" id="chang_email_tip">';
        var tip2 = '</p>';
        if (result) {
            $.ajax({
                //async: false,  //异步或是同步，默认为异步  
                url: "../../AjaxHandler/CreateNewAccount.ashx",
                dataType: "json",
                data: {
                    name: $("#uu_nickname").val(), telphone: $("#uu_email").val(), password: $("#uu_password").val(),
                    d: Math.random()
                },
              
                success: function (data) {
                    if (data == '创建成功') {
                        window.location.href = "../Login.aspx";
                        $("#div_change_email").find("#chang_email_tip").remove();
                        $("#div_change_email").append(tip1 + data + tip2);
                    } else {
                        alert(data);
                        return;
                    }
                }
            });
        }
        return false;
    });



});

function register_form_onsubmit() {
    $("#a_register_submit").removeClass().addClass("btn_a3");
    $("#button_register_submit").attr("disabled", "disabled");
   // $("#button_register_submit").empty().append("正在注册");

    var result = true;

    checkTelPhone() == false ? result = false : "";
    checkPassword() == false ? result = false : "";
    checkNickname() == false ? result = false : "";


    if (result == false) {
        $("#a_register_submit").removeClass().addClass("btn_a1");
        $("#button_register_submit").removeAttr("disabled");
        $("#button_register_submit").empty().append("免费注册");
    }
    return result;
}



//验证昵称有效性
function validateNickname(str) {
    var reg = /^[a-zA-Z0-9\u4E00-\u9FA5]{4,16}$/g;
    return reg.test(str);
}
//验证密码有效性
function validatePassword(str) {
    var reg = /^[a-zA-Z0-9]{6,20}$/g;
    return reg.test(str);
}
//检验手机号码有效性
function validateEmail(telPhone) {
    if (telPhone.search(/^([0-9]{11})?$/) == -1) {
        return false;
    } else {
        return true;
    }
}

//检查 注册资料-手机号码 部分
function checkTelPhone() {
    var val = $.trim($("#uu_email").val());
    var msg = '';
    if (val == null || val == '' || val == '手机号码') {
        msg = '请填写手机号码（手机号码将作为预定联系方式）';
    }
    else if (!validateEmail(val)) {
        msg = '手机号码格式不对，请重填';
    }
    //else {

    //    $.ajax({
    //        async: false,  //异步或是同步，默认为异步  
    //        url: "/register/exist-email.do",
    //        data: { email: val, d: Math.random() },
    //        type: "post",
    //        dataType: "text",
    //        success: function (data) {
    //            if (data == 'no')
    //            { msg = '该手机号码已被注册，请重填'; }
    //            else if (data == 'yes') { }
    //        }
    //    });

    //}


    if (msg != '') {
        addErrorTip($("#div_email"), msg);
        return false;
    }
    else {
        addSuccessTip($("#div_email"));
        return true;
    }
}

//检查 注册资料-密码 部分
function checkPassword() {
    var val = $("#uu_password").val();
    var msg = '';
    if (val == null || val == '') {
        msg = '请填写密码';
    }
    else if (val.length < 6) {
        msg = '密码太短不安全';
    }
    else if (val.length > 20) {
        msg = '密码太长不好记';
    }
    else if (!validatePassword(val)) {
        msg = '密码不能填写特殊字符';
    }
    if (msg != '') {
        addErrorTip($("#uu_password").parent(), msg);
        return false;
    }
    else {
        addSuccessTip($("#uu_password").parent());
        return true;
    }
}

//检查两次密码是否相同

function checkconfirmpassword() {
    var password = $("#uu_password").val();
    var confirmpassword = $("#uu_confirmpassword").val();
    var msg = '';
    if (confirmpassword == null || confirmpassword=='') {
        msg = '请输入确认密码';
    }
   else if (confirmpassword.isEqual(password)) {
        msg = '两次输入密码不一致，请重新输入';
    }
    if (msg != '') {
        addErrorTip($("#uu_confirmpassword").parent(), msg);
        return false;
    } else {

        addSuccessTip($("#uu_confirmpassword").parent());
        return true;
    }
}


//检查 注册资料-昵称 部分
function checkNickname() {
    var val = $.trim($("#uu_nickname").val());
    var msg = '';
    if (val == null || val == '') {
        msg = '请填写昵称';
    }
    else if (val.replace(/[^\x00-\xff]/g, "**").length < 4) {
        msg = '昵称太短，应为4-16个字符，一个汉字为两个字符';
    }
    else if (val.replace(/[^\x00-\xff]/g, "**").length > 16) {
        msg = '昵称太长，应为4-16个字符，一个汉字为两个字符';
    }

    var specialCharacter = "";
    //下面的正则过滤，有缺陷，还没完善，时间关系，加上这段
    if (msg == '') {
        var notAllowChar = ['~', '!', '`', '@', '#', '$', '%', '^', '&', '*', ')', '(', '-', '+', '\'', '"', ',', '.', '\\', '<', '>', '/'];
        for (var i = 0; i < notAllowChar.length; i++) {
            if (val.indexOf(notAllowChar[i]) >= 0) {
                specialCharacter += notAllowChar[i];
            }
        }
        if (specialCharacter != '') msg = '昵称只支持中英文和数字，不能出现字符' + specialCharacter;
    }

    if (msg == '') {
        var array = val.match(/[^a-z^A-Z^0-9^\u4E00-\u9FA5]/ig);
        if (array != null) {
            //特殊字符	
            for (i = 0; i < array.length; i++) {
                var index = specialCharacter.search(escape(array[i]));
                if (index < 0) {
                    specialCharacter += array[i];
                }
            }
            msg = '昵称只支持中英文和数字，不能出现字符' + specialCharacter;
        }
    }


    if (msg != '') {
        addErrorTip($("#uu_nickname").parent(), msg);
        return false;
    }
    else {
        addSuccessTip($("#uu_nickname").parent());
        return true;
    }
}
//获取URL参数
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
//移除注册资料选项后面的提示，包括成功和失败
function removeTip(my) {
   
    my.find(".tip_msg").remove();
    my.find(".tip_msg3").remove();
    my.find("#success_img").remove();
}
//添加注册资料选项后面的"失败"提示
function addErrorTip(my, error) {
    removeTip(my);
    var error1 = '<div class="tip_msg tip_msg3">';
    var error2 = '<span class="angle1"></span><span class="angle2"></span><span class="radius1"></span><span class="radius2"></span><span class="radius3"></span><span class="radius4"></span></div>';
    my.append(error1 + error + error2);
}
//添加注册资料选项后面的"成功"提示
function addSuccessTip(my) {
    removeTip(my);
    var applicationScope = $("#applicationScope").attr("val");
    var success = '<img src="http://localhost:29751/Images/register/b_09.gif" class="correct" id="success_img">';
    my.append(success);
}

//添加注册资料选项后面的提示
function addTip(my, tip) {
    removeTip(my);
    var tip1 = '<div class="tip_msg">';
    var tip2 = '<span class="angle1"></span><span class="angle2"></span><span class="radius1"></span><span class="radius2"></span><span class="radius3"></span><span class="radius4"></span></div>';
    my.append(tip1 + tip + tip2);
}


