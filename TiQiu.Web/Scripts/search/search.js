$(function(){
	$('.search_list_all').delegate('li','hover',function(event) {
	if(event.type==='mouseenter'){
		$(this).css({'background':'#fcf8ef'}).find('.search_info_ac').css('visibility','visible');
	}else{
		$(this).css({'background':'#fff'}).find('.search_info_ac').css('visibility','hidden');
	}
});



//选择房子
$('#house_select').bind('click',function(){
	ZA.showBox('#div_house','.alert_close',['360px','240px']);
});

//修改择偶条件
/*
$('#couple_select').bind('click',function(){
	ZA.showBox('#div_couple','.alert_close',['500px','240px']);
});
*/

/*
var arrHouse = new Array();
$('.house_div').find('input[type=checkbox]').bind('change',function() {
	var _self = $(this);
	var bHouse = _self.attr('checked');
	var sHouse = _self.next('span').html();
	if(bHouse=='checked'){
		
	}else{
		
	}
}
*/


$('.house_div').find('input[type=checkbox]').bind('change',function() {
	var sHouseList = $('#house_list').html();
	//alert(sHouseList);
	var _self = $(this);
    bAc = _self.attr('checked');
	var sHouse = _self.next('span').html();
	if(_self.get(0).checked==true){
		if(sHouseList=='不限'){sHouseList=''}
		else if(sHouseList.substr(-1)!=','){sHouseList = sHouseList + ','}
		
		sHouseList = sHouseList + sHouse + ',' ;
		//if(sHouseList.substr(-1)==','){ sHouseList=sHouseList.substring(0,sHouseList.length-1)}
		$('#house_list').html(sHouseList.substring(0,sHouseList.length-1));
		
		return sHouseList;
	} else
	{
		if(sHouseList.substr(-1)!=','){sHouseList = sHouseList + ','}
		
		var flag = sHouseList.search(sHouse);

		if(flag!=-1){
			
			sHouseList = sHouseList.replace((sHouse + ','),'');
			if(sHouseList==''){sHouseList='不限'}
			var liM = sHouseList.slice(-1)==','?sHouseList.substring(0,sHouseList.length-1):sHouseList ;
			$('#house_list').html(liM); 
		}
		
		
		
	}
	
});

//表单焦点
$('.search_id input').bind('focus',function(){
	var _self = $(this);
	if(_self.val()=='ID/昵称'){
		_self[0].value = '';
	}
});

$('.search_id input').bind('blur',function() {
	var _self = $(this);
	if(_self.val()=='') {
		_self[0].value = 'ID/昵称';
	}
});

})

//搜索页面左右两列等高
function fnHeight() {
	var lHeight = $('.w710').eq(0).outerHeight();
	var rHeight = $('.search_aside').eq(0).outerHeight();
	var mHeight = lHeight>=rHeight?lHeight:rHeight;
	$('.search_body').height(mHeight);
	$('.search_aside').height(mHeight-1);
}

//推荐面页
$('.putup_tab li').bind('click',function() {
	$('.search_body').css('height','auto');
	setTimeout(fnHeight,100);
});

//默认页面
$(function(){
	fnHeight();
});

function callbackHeight(){
	$('.search_body').removeAttr('style');
	fnHeight()
}
