$(function() {
    var hideValue = function() {
        if ($('#uu_password').val() != '') {
            $('#password_desc2').hide();
        }
    };
	//浏览器自动填充账号和密码
	if($("#uu_email").val() != "用户名" && $("#uu_password").val() != ""){$("#password_desc2").hide();}
	
	//登录按钮交互
    $('#header_login_btn').bind('click', function() {
        $('#head_login_form').removeClass('hide');
    });
    $('#head_login_form .close_this').bind('click', function() {
        $(this).parent().addClass('hide');
    });
	// 登陆输入框交互
	$("#uu_email").focus(function(){
		if("用户名" == $(this).val())$(this).attr("value","");
	 checkval=setInterval(hideValue,50);
	}).blur(function(){
		if("" == $(this).val())$(this).attr("value","用户名");
		clearInterval(checkval);
	});
	$("#uu_password").focus(function(){
		$("#password_desc2").hide();
	}).blur(function(){
		if("" == $(this).val())$("#password_desc2").show();
	});
	$('#password_desc2').click(function(){
		$('#uu_password').focus();
	});


    //浏览器自动填充账号和密码
	if ($("#reg_email").val() != "注册邮箱" && $("#uu_password").val() != "") { $("#password_desc2").hide(); }


    // 注册输入框交互
    $("#reg_user").focus(function () {
	    if ("昵称" == $(this).val()) $(this).attr("value", "");
	    checkval = setInterval(hideValue, 50);
	}).blur(function () {
	    if ("" == $(this).val()) $(this).attr("value", "昵称");
	    clearInterval(checkval);
	});

    // 注册输入框交互
    $("#reg_email").focus(function () {
        if ("注册邮箱" == $(this).val()) $(this).attr("value", "");
	    checkval = setInterval(hideValue, 50);
	}).blur(function () {
	    if ("" == $(this).val()) $(this).attr("value", "注册邮箱");
	    clearInterval(checkval);
	});


	//浏览器自动填充密码
	hideValue();
	setTimeout(hideValue,600); 
	
	//进入页面，检查是否有从服务器过来的错误信息，有就显示
	var msg = $("#login_error_msg_form").attr("msg");
	if(msg!=null && msg!='')
	{		
		var result='';		
		
		var json = jQuery.parseJSON(msg);
		
		$.each(json, function(key, val){
			 result+=val+",";
		});
		
		//密码登陆失败动画
		$('#uu_password,#password_desc2,#uu_email').removeClass('animation_shake');
		window.setTimeout(function(){
			if(result.indexOf('密码') != -1){
				$('#uu_password,#password_desc2').addClass('animation_shake');
			}else{
				$('#uu_email').addClass('animation_shake');
			}
		},0.5*1000);

		if(result!='')
		{
			$("#login_error_msg").empty().append(result.substring(0,result.length-1)).show();
		}
		
	}
});




function loginIndexSubmit(email,password,loginPreUrl)
{
	

	
$.ajax({  
    async:false,  //异步或是同步，默认为异步  
    url:"/login/user-login.do",  
    data:{ user:email,password:password,rememberMe:$("#rememberMe").is(':checked'),type:'dialog'},  
    type:"post",  
    dataType:"text",  
    success:function(data){  
        if(data != null && data!="")
        {
        	var obj = jQuery.parseJSON(data); 

        	if(obj.userLocked=='yes'&& obj.lockedUrl !=null){
        		window.location =obj.lockedUrl;
        	}

        	//验证失败
        	if(obj.result==null)
        	{
        		var result='';
        		var json = jQuery.parseJSON(data);
        		
        		$.each(json, function(key, val){
        			 result+=val+",";
        		});
				
				//密码登陆失败动画
				$('#uu_password,#uu_email').removeClass('animation_shake');
				window.setTimeout(function(){
					if(result.indexOf('密码') != -1){
						$('#uu_password').addClass('animation_shake');
					}else{
						$('#uu_email').addClass('animation_shake');
					}
				},0.5*1000);

        		$("#login_error_msg_index").remove();
				loginAddErrorTipOfIndexPage('<span id="login_error_msg_index" class="error_tips color1">'+result.substring(0, result.length-1)+'</span>');return;
        	}
			//新版用户 回到新版
        	if(obj.newversion==true){
				window.location.href="";
				return false;
			}
        	// 抽奖活动特殊处理
        	if (window.location.href.indexOf("/activity/ticket/") >= 0 && obj.result != null) {
        		window.location.reload();
        		return true;
        	}
        	
        	//通过验证，资料不全
        	if(obj.result=='no' && obj.url!=null)
        	{
				if(loginPreUrl=='index_login'){
					loginPreUrl='/personal/myindex.do';
				}
        		window.location=obj.url+"?loginPreUrl="+loginPreUrl;
        	}
        	//通过验证，资料全
        	if(obj.result=='yes' && obj.url==null)
        	{
        		if(loginPreUrl!=null&&loginPreUrl=="index_login"){
        			window.location.href="/personal/myindex.do";
        		}else{
        			window.location.reload();
        		}
        	}
        }
    }  
});  

}



function loginDialogSubmit(email,password,loginPreUrl)
{
	

	
$.ajax({  
    async:false,  //异步或是同步，默认为异步  
    url:"/login/user-login.do",  
    data:{ user:email,password:password,rememberMe:$("#rememberMe").is(':checked'),type:'dialog'},  
    type:"post",  
    dataType:"text",  
    success:function(data){  
        if(data != null && data!="")
        {
        	var obj = jQuery.parseJSON(data); 

        	if(obj.userLocked=='yes'&& obj.lockedUrl !=null){
        		window.location =obj.lockedUrl;
        	}
        	
        	//验证失败
        	if(obj.result==null)
        	{
        		var result='';
        		var json = jQuery.parseJSON(data);
        		
        		$.each(json, function(key, val){
        			 result+=val+",";
        		});
        		
				//密码登陆失败动画
				$('#uu_password, #uu_email').removeClass('animation_shake');
				window.setTimeout(function(){
					if(result.indexOf('密码') != -1){
						$('#uu_password').addClass('animation_shake');
					}else{
						$('#uu_email').addClass('animation_shake');
					}
				},0.5*1000);

        		loginAddErrorTipOfDialog(result.substring(0, result.length-1));
        	}
			//新版用户 回到新版
        	if(obj.newversion==true){
				window.location.href="";
				return false;
			}
        	// 抽奖活动特殊处理
        	if (window.location.href.indexOf("/activity/ticket/") >= 0 && obj.result != null) {
        		window.location.reload();
        		return true;
        	}
        	
        	//通过验证，资料不全
        	if(obj.result=='no' && obj.url!=null)
        	{
				if(loginPreUrl=='index_login'){
					loginPreUrl='/personal/myindex.do';
				}
        		window.location=obj.url+"?loginPreUrl="+loginPreUrl;
        	}
        	//通过验证，资料全
        	if(obj.result=='yes' && obj.url==null)
        	{
        		if(loginPreUrl!=null&&loginPreUrl=="index_login"){
        			window.location.href="/personal/myindex.do";
        		}else{
        			window.location.reload();
        		}
        	}
        }
    }  
});  

}

function setCookie(c_name,value,expiredays)
{
	var exdate=new Date();
	exdate.setDate(exdate.getDate()+expiredays);
	document.cookie=c_name+ "=" +escape(value)+((expiredays==null) ? "" : ";expires="+exdate.toGMTString());
}

function getCookie(c_name)
{
if (document.cookie.length>0)
  {
  c_start=document.cookie.indexOf(c_name + "=");
  if (c_start!=-1)
    { 
    c_start=c_start + c_name.length+1 ;
    c_end=document.cookie.indexOf(";",c_start);
    if (c_end==-1) c_end=document.cookie.length;
    return unescape(document.cookie.substring(c_start,c_end));
    } 
  }
return "";
}


function loginAddErrorTipOfDialog(msg)
{
	$("#login_error_msg_dialog").empty().append(msg);
}


function loginAddErrorTipOfIndexPage(msg)
{
	$("#index_login").append(msg);
}
function loginAddErrorTipOfPage(msg)
{
	$("#login_error_msg").empty().append(msg).show();
}

//换一个验证码
function changeValidateCode(obj) {
	//获取当前的时间作为参数，无具体意义
	var timenow = new Date().getTime();
	//每次请求需要一个不同的参数，否则可能会返回同样的验证码
	//这和浏览器的缓存机制有关系，也可以把页面设置为不缓存，这样就不用这个参数了。
	document.getElementById("img_validateCode").src = "/login/random.do?d="
			+ timenow;
}

//检验邮箱有效性
function validateEmail(email)
{
	if (email.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) == -1) {
		return false;
	}else
		{
		return true;
		}
}

//移除选项后面的提示
function removeTip(my)
{
	my.find(".tip_msg").remove();
	my.find("#tip_msg_tip_msg3").remove();
}
//添加选项后面的"失败"提示
function addErrorTip(my,error)
{
	removeTip(my);
	var error1='<div id="tip_msg_tip_msg3" class="tip_msg tip_msg3">';
	var error2='<span class="angle1"></span><span class="angle2"></span><span class="radius1"></span><span class="radius2"></span><span class="radius3"></span><span class="radius4"></span></div>';
	my.append(error1+error+error2);
}

function mobileErroTip(mobile){
	if(mobile==null || $.trim(mobile)==""){
		topErrorTip("#right_form_msg","请输入手机号");
		return false;
	}else if(!phone_check(mobile) || mobile.length!=11){
		topErrorTip("#right_form_msg","手机号格式不正确");
		return false;
	}
	return true;
}
//添加表单顶部的"失败"提示
function topErrorTip(id,error)
{
	$(id).html(error);
}
//验证手机号
function phone_check(val){
	if(!(/^1[3|5|8][0-9]\d{4,8}$/.test(val))){
		return false;
	}
	return true;
}

//检查 注册资料-密码 部分
function checkPassword(password) {
    var val = password;
	var msg='';
	if(val==null || val=='')
		{
		msg='请填写密码';
		}
	else if(val.length<6)
		{
		msg='密码太短不安全，密码长度应在6-20位';
		}
	else if(val.length>20)
		{
		msg='密码太长不好记，密码长度应在6-20位';
		}
	else if(!validatePassword(val))
		{
		msg='密码不能填写特殊字符';
		}
	if(msg!='') {
	    addErrorTip($("#password").parent(), msg);
		return false;
	}
		
}

//验证密码有效性
function validatePassword(str)
{
	var reg = /^[a-zA-Z0-9]{6,20}$/g;  
	return reg.test(str);
}

function addErrorTip(my,error)
{
	removeTip(my);
	var error1='<div id="tip_msg_tip_msg3" class="tip_msg tip_msg3">';
	var error2='<span class="angle1"></span><span class="angle2"></span><span class="radius1"></span><span class="radius2"></span><span class="radius3"></span><span class="radius4"></span></div>';
	my.append(error1+error+error2);
}

function removeTip(my)
{
	$("#mysex_0").parent().find(".tip_msg").remove();
	my.find(".tip_msg").remove();
	my.find("#tip_msg_tip_msg3").remove();
	my.find("#success_img").remove();
}



function addCookieNew(objName, objValue, objHours) {//添加cookie 
    var str = objName + "=" + escape(objValue);
    if (objHours > 0) { //为时不设定过期时间，浏览器关闭时cookie自动消失 
        var date = new Date();
        var ms = objHours * 3600 * 1000;
        date.setTime(date.getTime() + ms);
        str += "; expires=" + date.toGMTString();
    }
    document.cookie = str + ";path=/";
}

function getCookie(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=");
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1;
            c_end = document.cookie.indexOf(";", c_start);
            if (c_end == -1) c_end = document.cookie.length;
            return unescape(document.cookie.substring(c_start, c_end));
        }
    }
    return "";
}