(function(){
var pl_get_cookie = function(c_name)
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
var savePageLog = function(userId,pagelogType){
	var url=""+pagelogType+"&userId="+userId+"&r="+Math.random();
	
	var image = new Image();
	image.src= url;
}
var pl_get_userid = function(){
	var userId=$("meta[name='currentUserId']").attr("content");
	
	if(userId=="" || userId == undefined || userId=="undefined" || userId == null || userId == "null")
	{
		userId=pl_get_cookie("userId");
	}
	return userId
}
$(function(){
  $('#top_entrance_container').delegate('div,a,img','click',function(){
      var userId = pl_get_userid();
	  savePageLog(userId,163);
  })
})
})()
