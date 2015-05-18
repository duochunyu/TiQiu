// 创建html5元素
(function(){
	var l=arguments.length;
		for(var i=0; i<l; i++){
			document.createElement(arguments[i]);
		}
})("address","article","aside","audio","canvas","details","figcaption","figure","header","footer","hgroup","menu","nav","section","summary","time","video");
