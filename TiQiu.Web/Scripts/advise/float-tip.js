
//左右侧广告栏
$(function (){
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
                });
                $('.gobal_floater_box').hover(function(){
                    	$(this).find('.floater_hover_layer').show();
                    },function(){
                    	$(this).find('.floater_hover_layer').hide();
                 })
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