// JavaScript Document
/*
dabai.chen
aip文档说明，不知道用的麻烦去看一下。 http://10.10.11.133:800/pages/viewpage.action?pageId=852043
*/
(function(){
	if(window.ZA == null ){
        window['ZA'] = {};      
	}	
//弹出框 方法  boxId 需要弹出的对象名字
function showBox(boxId,clsObj,size,callBack){
//获取弹出对象
var alertBox = $(boxId);

if(alertBox.html()==null){
		alert('警告：html结构不存在showBox第一个参数【'+boxId+'】所指定的页面结构。请检查结构代码和逻辑代码。');
		return false;
	}
var alertBoxPos = alertBox.find('.alert_box_pos');
var alertBg = alertBox.find('.alert_bg');
//ie6垂直居中方法
if(!-[1,]&&!window.XMLHttpRequest){
	//隐藏所有除开弹出层以外的下拉框元素
	$('select').css('visibility','hidden');
	alertBox.find('select').css('visibility','');
	//获取body标签对象
	var box = $('body');
	//获取弹出框标签对象（不包含遮罩）
	//获取body页面高度
		var boxH = box.height();
	//获取可见区域高度
		var documentH = document.documentElement.clientHeight;	
	//判断页面高度是否小于一屏 设置给遮罩层
	if(boxH > documentH ){
			alertBg.height(boxH);
		}	
		else{
			alertBg.height(documentH);
		}
	//获取居中Y坐标 (可见区域的2分之一)
	var boxTop = documentH/2;
	//设置弹出层显示的位置
	alertBoxPos.css('top',boxTop+document.documentElement.scrollTop+'px');
}
		alertBox.show(saveLog(alertBox)).removeClass('hide_alert').addClass('show_alert');
		//定义尺寸
		resetSize(boxId,size);
		if(clsObj == null){
			alertBox.find('.alert_close').hide();
		}else{
			closeBox(alertBox,clsObj,callBack);
		};
		if(callBack){
							callBack();
						}
}


//记录弹层展示次数，根据弹层是否有pagelogType属性决定是否记录日志
function saveLog(target)
{
	if(target.attr("pagelogType") == "" || target.attr("pagelogType") == "undefined" || target.attr("pagelogType") == undefined)
		{
		return;
		}
	
	var userId=$("meta[name='currentUserId']").attr("content");
	
	if(userId=="" || userId == undefined || userId=="undefined" || userId == null || userId == "null")
	{
		userId=getCookie("userId");
	}

	var pagelogType=target.attr("pagelogType");
	url="http://cdnlog.yuanlai.com/pagelog.do?pagelogType="+pagelogType+"&userId="+userId;
	
	var image = new Image();
	image.src= url;
	
}

//获取cookies
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


//尺寸设置
	function resetSize(boxId,size){
		var alertBox = $(boxId);
		var alertBoxPos = alertBox.find('.alert_box_pos');
		if(size){
			alertBoxPos.width(size[0]).height(size[1]);
		}else{
			//页面里面用出html5 <header>标签， js里面插入html5标签不会被渲染。采用 <div class="header">  在计算的时候避免某些情况取不到 header 和 footer的值。所以2个都计算进去
			var adHeight = alertBox.find('.alert_mail_banner').outerHeight(true) || 0;
			var boxHeight = alertBox.find('header').outerHeight(true)+alertBox.find('.header').outerHeight(true)+alertBox.find('.alert_main').outerHeight(true)+adHeight;
			if(alertBox.find('footer')){		
				alertBoxPos.height(boxHeight+alertBox.find('footer').outerHeight(true)+alertBox.find('.footer').outerHeight(true))
			}else{
				alertBoxPos.height(boxHeight);
			}
			if(document.all){
				var boxWidth = alertBox.find('.alert_main').outerWidth(true);
					alertBox.find('header').width(boxWidth);
					alertBoxPos.css('width',boxWidth);
				}
		}
	}
	
	var isModernBroswer = $('html.modern').length>0;
	var hideAnimate = function(alertBox){
		alertBox.addClass('hide_alert');
		alertBox.find('.alert_box').bind('oanimationend animationend webkitAnimationEnd',function(){
			alertBox.removeClass('show_alert');
			$(this).unbind();
		})
	}

//关闭弹出层
function closeBox(alertBox,obj,callBack){
			if (obj){
				alertBox.find(obj).bind('click', function() {
					//判断关闭回调函数存不存在
					if(callBack){
							callBack();
						}
						
					if(isModernBroswer){
						hideAnimate(alertBox);
					}else{
						alertBox.hide();
					}
					alertBox.find(obj).unbind();
					if (! -[1, ] && !window.XMLHttpRequest) {
                        if(!$('.alert').is(":visible")){
					        $('select').css('visibility','');
                        }
					}
				})
			}else{
				if(isModernBroswer){
					hideAnimate($(alertBox));
				}else{
					$(alertBox).hide();
				}
				if(!-[1,]&&!window.XMLHttpRequest){
						$('select').css('visibility','');
					}
			}
	};

//警告窗口
function alertZa(text,callback,url){
		if($('#alert').html()){
				$('#alert .alert_main').html(text);
				if(url){
					$('#alert .close_zaalert').attr('href',url);
				}
				showBox('#alert','.close_zaalert','',callback);

			}else{
				if(url){
					var curl = url;
				}else{
					var curl = "###"
					
				}
				$('body').append('<div class="alert" id="alert"><div class="alert_bg" ></div><div class="alert_box_pos" ><div class="alert_box"><div class="header"><span class="alert_close close_zaalert">×</span><h1>温馨提示</h1></div><div class="alert_main">'+text+'</div><div  class="footer"><a href="'+ curl +'" class="btn_b1 close_zaalert"><span>确定</span></a></div></div></div></div>');
				showBox('#alert','.close_zaalert','',callback);
			}
	}

//确定窗口
function confirmZa(text,callback){
		if($('#confirm').html()){
				$('#confirm .alert_main').html(text);
				showBox('#confirm','.alert_close,.btn_b1');
			}else{
				$('body').append('<div class="alert" id="confirm"><div class="alert_bg" ></div><div class="alert_box_pos" ><div class="alert_box"><div class="header"><span class="alert_close">×</span><h1>温馨提示</h1></div><div class="alert_main">'+text+'</div><div  class="footer"><a href="###" class="btn_b1" id="confirmTrue"><span>确定</span></a> <a href="#" class="btn_b1" id="confirmFalse"><span>取消</span></a></div></div></div></div>');
				showBox('#confirm','.alert_close,.btn_b1');
			}
			
			$('#confirmTrue').click(function(){
					callback(true);					
					$('#confirmTrue,#confirmFalse').unbind();
				});
			$('#confirmFalse').click(function(){
					callback(false);
					$('#confirmFalse,#confirmFalse').unbind();
				});
		
	}

	window['ZA']['showBox'] = showBox;
	window['ZA']['closeBox'] = closeBox;
	window['ZA']['alert'] = alertZa;
	window['ZA']['confirm'] = confirmZa;
	window['ZA']['resetShowBoxSize'] = resetSize;
})()
