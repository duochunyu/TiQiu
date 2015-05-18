/**
 * 搜索表单
 */
$(function(){
	// 性别
	if (document.getElementById("search_bar_gender") != null) {
		html = "";
		html += syscode.select('', 'gender', 'params.gender', _YL.search_bar.gender, '2', syscode.sex, "");
		$(html).insertAfter($("#search_bar_gender"));
	}
	
	// 年龄
	if (document.getElementById("search_bar_age") != null) {
		html = "";
		html += syscode.select('', 'ageMin', 'params.ageMin', _YL.search_bar.age[0], '18', syscode.age, syscode.qingxuanze);
		html += "<span>~</span>";
		html += syscode.select('', 'ageMax', 'params.ageMax', _YL.search_bar.age[1], '23', syscode.age, syscode.qingxuanze);
		$(html).insertAfter($("#search_bar_age"));
	}
	
	// 地区
	if(document.getElementById("search_bar_workCity") != null) {
		html = "";
		html += syscode.provinceSelect('', 'province', 'params.workProvince', 'city', _YL.search_bar.workCity, '', syscode.qingxuanze, true);
		html += "<span>&nbsp;&nbsp;</span>";
		html += syscode.citySelect('', 'city', 'params.workCity', _YL.search_bar.workCity, '-1', syscode.qingxuanze, true);
		$(html).insertAfter($("#search_bar_workCity"));
	}
});

//左右侧广告栏
$(function () {
	
	//登录按钮交互
	$('#header_login_btn').bind('click',function(){
		$('#head_login_form').removeClass('hide');
	})
	$('#head_login_form .close_this').bind('click',function(){
		$(this).parent().addClass('hide');
	})
	
	// 两侧游戏广告 
	  if (document.getElementById('enter') != null) {
  		var url=window.top.location.href;
		url=encodeURIComponent(url.split('?')[0]);
		$('#enter').find('#advise_link').attr('href',$('#enter').find('#advise_link').attr('href')+url);
		
        if (window.ActiveXObject) {
            var ua = navigator.userAgent.toLowerCase();
            var ie = ua.match(/msie ([\d.]+)/)[1];
            if (ie < 7) {
				$('#enter').css('position','absolute');
                $(window).bind('scroll', function () {
                    $('#enter').css('top', ($(window).scrollTop() + 100) + 'px');
                    $('.gobal_floater_box').hover(function(){
                    	$(this).find('.floater_hover_layer').show();
                    },function(){
                    	$(this).find('.floater_hover_layer').hide();
                    })
                });
            }
        }
    }

    //广告关闭方法
    $('#enter .ad_close').click(function (e) {
        addCookieNew("ad_left_game_close", 1, 0);
        $('#enter .ad_dxz').hide();
        return false;
    })

});

function addCookieNew(objName,objValue,objHours){//添加cookie 
	var str = objName + "=" + escape(objValue);
	if(objHours > 0){ //为时不设定过期时间，浏览器关闭时cookie自动消失 
		var date = new Date(); 
		var ms = objHours*3600*1000; 
		date.setTime(date.getTime() + ms); 
		str += "; expires=" + date.toGMTString();
	} 
	document.cookie = str + ";path=/";
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