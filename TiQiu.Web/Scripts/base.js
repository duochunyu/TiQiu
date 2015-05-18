(function (win, UI, undefined) {
    if (win[UI] === undefined) {
        win[UI] = {};
    } else {
        return;
    }
    var mix = function (r, s, ov) {
        if (!s || !r) return r;
        if (ov === undefined) ov = true;
        for (var p in s) {
            if (ov || !(p in r)) {
                r[p] = s[p];
            }
        }
        return r;
    };
    UI = win[UI];
    mix(UI, {
        laterEvent: function (fn, times, me) {
            if (me.sleepid) {
                clearTimeout(me.sleepid);
            }
            me.sleepid = setTimeout(fn, times);
        },
        popWinA: function (s, op) {
            op = $.extend({
                opener: ".opener",
                openerC: ".openerC",
                addClass: "now"
            }, op || {});

            $(s).each(function () {
                var $this = $(this),
					objOpner = $(this).find(op.opener),
					objC = $(this).find(op.openerC);

                $(this).hover(function () {
                    $this.addClass(op.addClass);
                    objC.show();
                }, function () {
                    $this.removeClass(op.removeClass || op.addClass);
                    objC.hide();
                });
            });
        },
        popWinB: function (s, hover, clickHide, delayTime) {
            var objOpner = $("." + s + " .opener");
            var objShuter = $("." + s + " .shuter");
            if (hover == false) {
                if (objOpner.length) {
                    objOpner.click(function () {
                        if ($(this).parents(".over").length) {
                            $(this).parents("." + s).removeClass("over");
                        }
                        else {
                            $("." + s).removeClass("over");
                            $(this).parents("." + s).addClass("over");
                        }
                    });
                }
                if (objShuter.length) {
                    objShuter.click(function () {
                        $(this).parents("." + s).removeClass("over");
                    });
                }
                if (clickHide == true) {
                    $("html, body").bind("click", function (e) {
                        if ($(e.target).parents("." + s).length == 0) {
                            objOpner.parents("." + s).removeClass("over");
                        }
                    });
                }
                if (delayTime) {
                    objOpner.parents("." + s).hover(
					function () {
					    objOpner.parents("." + s)[0].sleepid && clearTimeout(objOpner.parents("." + s)[0].sleepid);
					}, function () {
					    UI.laterEvent(function () {
					        objOpner.parents("." + s).removeClass("over");
					    }, delayTime, objOpner.parents("." + s)[0]);
					});
                }
            }
            if (hover == true) {
                if ($.browser.msie) {
                    objOpner.parents("." + s).hover(
					function () {
					    $(this).addClass("over");
					}, function () {
					    $(this).removeClass("over");
					});
                } else {
                    objOpner.parents("." + s).mouseover(
					function () {
					    $(this).addClass("over");
					}).mouseout(function () {
					    $(this).removeClass("over");
					});
                }
            }
        },
        popWinC: function (s, op) {
            op = $.extend({
                opener: ".opener",
                openerC: ".openerC",
                addClass: "now",
                funcIn: function () { },
                funcOut: function () { }
            }, op || {});

            $(s).each(function () {
                var $this = $(this),
					objOpner = $(this).find(op.opener),
					objC = $(this).find(op.openerC);

                $(this).hover(function () {
                    $this.addClass(op.addClass);
                    op.funcIn.call(this, objC, op);
                }, function () {
                    $this.removeClass(op.removeClass || op.addClass);
                    op.funcOut.call(this, objC, op);
                });
            });
        },
        childUntil: function (expr, obj) {
            if (obj.length == 0) return null;
            var child = obj.children(expr);
            if (child.length == 0) {
                return arguments.callee(expr, obj.children());
            } else {
                return child;
            }
        } //,
        //        defaultText: function(op) {
        //            op = $.extend({
        //                hasDefaultText: ".hasDefaultText",
        //                removeClass: "hasDefaultText",
        //                addClass: "hasDefaultText"
        //            }, op || {});
        //            var obj = $(op.hasDefaultText);
        //            var tmpText = new Array();
        //            var objIndex = 0;
        //            for (i = 1; i <= obj.length; i++) {
        //                tmpText[i - 1] = obj.eq(i - 1).attr("tip") ? obj.eq(i - 1).attr("tip") : obj.eq(i - 1).val();
        //                obj.eq(i - 1).attr("tip", tmpText[i - 1]);
        //            }
        //            obj.focus(function() {
        //                objIndex = obj.index($(this));
        //                if ($(this).val() == $(this).attr("tip")) {
        //                    $(this).val("");
        //                }
        //                $(this).removeClass(op.removeClass);
        //            });
        //            obj.blur(function() {
        //                objIndex = obj.index($(this));
        //                if ($(this).val() == "") {
        //                    $(this).val($(this).attr("tip"));
        //                    $(this).addClass(op.addClass);
        //                }
        //            });
        //        }
    });
})(window, "UI");

$(function () {
    /* Default Input Place Holder */
    //UI.defaultText();

    /* Global Tab Function */
    $(".tab").each(function () {
        var $this = $(this),
		tabc = UI.childUntil(".tabc", $this.parent());
        if (tabc == null || tabc == "undefined")
            return false;
        $this.children("a:not([rel*='link'])").add($this.find(".tabitem")).each(function (n) {
            $(this).attr("rel", n);
        }).mouseenter(function () {
            if ($this.attr("trigger") == "click") { return false; }
            $(this).addClass("now").siblings().removeClass("now");
            $(this).siblings("b[class*=hide]").removeClass("hide");
            $(this).prev("b").addClass("hide").andSelf().next("b").addClass("hide");
            tabc.hide().eq(parseInt($(this).attr("rel"))).show();
            if ($(this).attr("command")) {
                eval($(this).attr('command') + "(this)");
            }
        }).click(function () {
            if ($this.attr("trigger") != "click") { return false; }
            $(this).addClass("now").siblings().removeClass("now");
            tabc.hide().eq(parseInt($(this).attr("rel"))).show();
            //if($(this).prev().is("input")){$(this).prev().attr("checked","checked")}
            if ($(this).attr("command")) {
                eval($(this).attr('command') + "(this)");
            }
            //return false;
            //$(".togglerIconHolder .toggler").click();
            var toggler = $(".togglerIconHolder .toggler");
            if (toggler) {
                var cont = toggler.parent().parent().next();

                if (toggler.attr("data-animated") == "false") {
                    return false;
                }
                toggler.attr("data-animated", "false");
                if (!cont.hasClass("togglerContent")) {
                    cont = toggler.parents(".togglerIconHolder").next(".togglerContent");
                }
                toggler.attr({ "data-state": "collapse", "title": "收起" }).text("收起").removeClass("toggler_expand");
                cont.slideDown("fast", function () {
                    toggler.attr("data-animated", "true");
                });
            }
        });
    });

    /* Init Global pop layer function */
    UI.popWinC(".weather,.loginbar_bak", {
        addClass: "over",
        funcIn: function (o) {
            this.sleepid && clearTimeout(this.sleepid);
            //$(".searchpop").removeClass("over");
        },
        funcOut: function (o, op) {
            var self = this;
            $(self).addClass(op.addClass);
            UI.laterEvent(function () {
                $(self).removeClass(op.addClass);
            }, 200, self);
        }
    });

    //Append iframe for Submenu in IE6 to Mask Select
    if (! -[1, ] && !window.XMLHttpRequest) {
        $(".menu .expand").each(function () {
            var oc = $(this).find(".openerC");
            oc.append('<iframe style="position:absolute;left:-2px;top:-2px;z-index:-1; background-color:transparent;filter:alpha(opacity=0);" width="' + (oc.width() + 4) + '" height="' + (oc.height() + 4) + '"></iframe>');
        });
    }

    UI.popWinC(".menu .expand", {
        addClass: "over",
        funcIn: function (o) {
            UI.laterEvent(function () {
                /* if IE6\7\8 */
                if ($.browser.msie && $.browser.version >= 6 && $.browser.version < 9) {
                    o.stop().css({ top: 0 }).slideDown().animate({ top: 47 }, { queue: false, duration: 200 });
                    o.width(o.find(".wrap").width());
                } else {
                    o.stop().css({ top: 0, opacity: 1 }).fadeIn().animate({ top: 47 }, { queue: false, duration: 200 });
                }
                //$(".searchpop").removeClass("over");
            }, 80, this);
        },
        funcOut: function (o) {
            o.css({ display: "none", top: 0 });
            this.sleepid && clearTimeout(this.sleepid);
        }
    });

    UI.popWinC(".utility .func_minicart", {
        addClass: "over"
    });

    //IE6 Fix Linear @ Submenu
    ! -[1, ] && !window.XMLHttpRequest && (function () {
        var linear = $(".expand").find(".linear").find("img");
        if (linear.length == 0) return;
        linear.each(function () {
            var pngbg = $(this).css("backgroundImage").split('"')[1];
            this.style.filter = 'progid:DXImageTransform.Microsoft.AlphaImageLoader(src="' + pngbg + '" ,sizingMethod="scale")';
            this.style.backgroundImage = "none";

            this.src = $.newegg.buildWWW("WebResources/Default/Nest/img/blank.gif");
        });

    })();

    //IE6 Fix Weather Icon @ Topbar
    ! -[1, ] && !window.XMLHttpRequest && (function () {
        var wi = $(".curdate .icon_weather").find("img");
        if (wi.length == 0) return;
        wi.each(function () {
            this.style.filter = 'progid:DXImageTransform.Microsoft.AlphaImageLoader(src="' + this.getAttribute("src") + '" ,sizingMethod="scale")';

            this.src = $.newegg.buildWWW("WebResources/Default/Nest/img/blank.gif");
        });
        $(".weather_nearby").css("visibility", "hidden").parent().addClass("over").find("li").each(function () {
            $(this).width($(this).width() + 2);
        });
        $(".curdate").find(".weather").removeClass("over").find(".weather_nearby").css("visibility", "visible");
    })();


    /* Menu Indicator */
    var mi = $(".menu_indicator");
    var mc = $(".menu").children(".current");
    $(".menu").children("li").hover(
		function () {
		    var l = $(this).offset().left;
		    mi.stop().animate({
		        left: l - (mi.width() - $(this).width()) / 2
		    })
		},
		null
	);
    $(".menu").hover(
		null,
		function () {

		    (mc.length != 0) && mi.stop().animate({
		        left: mc.offset().left - (mi.width() - mc.width()) / 2
		    });
		    (mc.length == 0) && mi.stop().animate({
		        left: -1910
		    });
		}
	);
    if (mc.length != 0) {
        mi.stop().animate({
            left: mc.offset().left - (mi.width() - mc.width()) / 2
        }, 1000);
    }
    $(window).resize(function () {
        (mc.length != 0) && mi.stop().animate({
            left: mc.offset().left - (mi.width() - mc.width()) / 2
        })
    })

    /* Minicart Product line Hover Highlight */
    $(".utility .prolist li").hover(
		function () {
		    $this = $(this);
		    $this.addClass("cur");
		},
		function () {
		    $this = $(this);
		    $this.removeClass("cur");
		}
	);

    $(".utility .prolist li .btn_del").hover(
		function () {
		    $(this).parent().parent().addClass("hover");
		},
		function () {
		    $(this).parent().parent().removeClass("hover");
		}
	);

    /*  Minicart XSlider */
    $(".utility .cart .listwrap").each(function (k, v) {
        var $this = $(this);
        var li = $this.find(".prolist").children("li");
        if (li.length > 3) {
            $this.find(".slidewrap").height(213);
            $this.find(".abtn").css("display", "block");
            UI.Xslider($this, {
                dir: "V",
                viewedSize: 216,
                unitLen: 72,
                unitDisplayed: 3,
                numtoMove: 3,
                speed: 600
            });
        }
    });

    //back to the top;
    var back_to_top = $("a.back_to_top");
    back_to_top.hide();
    $(window).scroll(function () {
        if ($(window).scrollTop() > 100) {
            back_to_top.fadeIn(1500);
        }
        else {
            back_to_top.fadeOut(1000);
        }
    });

    back_to_top.click(function () {
        $($.browser.safari || document.compatMode == 'BackCompat' ? document.body : document.documentElement).animate({
            scrollTop: 0
        }, 200);
        return false;
    });

    /* Hotel Index SliderList */
    $(".sliderlist").find("h3").bind("click", function () {
        if ($(this).hasClass("expand")) return false;
        $(this).parent().find(".expand").removeClass("expand").next().slideUp();
        $(this).addClass("expand").next().slideDown();
    });

    /* Booksearch Half-auto Focus the Input */
    /*$(".bookSearch").find(".item").bind("click",function(e){
    var o = $(this).find(".intxt[type=text]").not("[onfocus]");
    if(o.length!=0){
    o.focus();	
    }
		
	});*/

    //Floating Online CS for IE6
    if (! -[1, ] && !window.XMLHttpRequest) {
        (function () {
            var onlineCS = $(".onlineCS"), sctop;
            function floating() {
                scleft = document.documentElement.scrollLeft ? document.documentElement.scrollLeft : document.body.scrollLeft;
                sctop = document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop;
                sctop = sctop + $(window).height() / 2;

                onlineCS.css({
                    width: $(window).width(),
                    top: sctop,
                    left: scleft
                });
            }
            floating();
            $(window).bind("scroll resize", function () {
                setTimeout(floating, 0);
            });
            try { DD_belatedPNG.fix(".onlineCS .ie6png2"); }
            catch (e) { }
        })();
    }

    //Popup Layer for Friend Link @ Footer
    var linkfriend = $("#link_friend");
    linkfriend.bind("click", function () {
        $("#link_friend_popup").show();
        $("#footer").css("zIndex", 91);
    });
    $("#link_friend_popup").find(".close").bind("click", function () {
        $("#link_friend_popup").hide();
        $("#footer").css("zIndex", 1);
    });
});
//Function Hide MydatePicker97 layer & Input Suggestion Layer
function hideEleForTabc(obj) {
    if ($(obj).attr("hovered") == 1) {
        $(obj).siblings().attr("hovered", 0);
    }
    else {
        $(obj).attr("hovered", 1).siblings().attr("hovered", 0);
        $dp.hide();
    }
    $(".address_hot_htldom").hide();
    $(".c_address_select").hide();
}

//Showmsg Function To Simulate Alert()
function Showmsg(msgHtml, mask) {
    var fire = function (m, k) {
        $("#Validform_msg").remove();
        //Show Msg Demo, Requie ui.Validform_v5.1_min.js 
        $.Showmsg(m);

        //Mask Layer, Requie ui.popWin.js & ele_popWin.css
        if (mask !== false && (typeof PopWin == "function")) {
            //Fake Pop Msg, Just to use Mask layer
            if ($("body").find("#blankShowmsg").length == 0) {
                $("body").append('<div class="blankShowmsg popWin" id="blankShowmsg"><div class="popWin_wrap"></div><a class="close" href="javascript:void(0)"></a></div>');
            }
            //pop_overlay
            var pop_overlay = PopWin("#blankShowmsg");
            pop_overlay.fn.popIn();
            //Hide Overlay if close Pop Msg
            $("#Validform_msg").find(".Validform_close").bind("click", function () {
                pop_overlay.fn.popOut();
                if (k) {
                    k();
                }
            });
        }

    }

    if (UI["Validform"]) {
        fire(msgHtml, mask);
    }
    else {
        //console.log("JQuery Library of Validform requied!");
        loadScript($Url.BuildUrl("WebResources/default/Scripts/js/ui.Validform_v5.1_min.js", 'WWW'), function () {
            fire(msgHtml, mask);
        });
    }
}

//Showconfirm Function To Simulate Confirm()
function ShowConfirm(msgHtml, callback) {
    var fire = function (m, c) {
        $("#Validform_msg").remove();
        var btnsHtml = '<div class="btns mt20" style=\"text-align: center; display:block; background:none;  padding:0px;\"><a class="btn btn_yellow mr10" href="javascript:void(0);"><span>确定</span></a><a class="btn btn_yellow" href="javascript:void(0);"><span>取消</span></a></div>';
        //Show Msg Demo, Requie ui.Validform_v5.1_min.js 
        $.Showmsg(m + btnsHtml);

        var msgBox = $("#Validform_msg");
        msgBox.find(".btns").find(".btn").click(function () {
            msgBox.find(".Validform_close").click();
            if ($(this).index() === 0) {
                c.confirmCallBack.call();
            }
            else {
                c.cancelCallBack.call();
            }
        });

        //Mask Layer, Requie ui.popWin.js & ele_popWin.css
        if (typeof PopWin == "function") {
            //Fake Pop Msg, Just to use Mask layer
            if ($("body").find("#blankShowmsg").length == 0) {
                $("body").append('<div class="blankShowmsg popWin" id="blankShowmsg"><div class="popWin_wrap"></div><a class="close" href="javascript:void(0)"></a></div>');
            }
            //pop_overlay
            var pop_overlay = PopWin("#blankShowmsg");
            pop_overlay.fn.popIn();
            //Hide Overlay if close Pop Msg
            $("#Validform_msg").find(".Validform_close").bind("click", function () {
                pop_overlay.fn.popOut();
            });
        }
    }

    if (UI["Validform"]) {
        fire(msgHtml, callback);
    }
    else {
        //console.log("JQuery Library of Validform requied!");
        loadScript($Url.BuildUrl("WebResources/default/Scripts/js/ui.Validform_v5.1_min.js", 'WWW'), function () {
            fire(msgHtml, callback);
        });
    }
}

//Load Script Function
function loadScript(url, callback) {
    var script = document.createElement("script");
    script.type = "text/javascript";
    if (script.readyState) {  //IE
        script.onreadystatechange = function () {
            if (script.readyState == "loaded" ||
                    script.readyState == "complete") {
                script.onreadystatechange = null;
                callback();
            }
        };
    } else {  //Others
        script.onload = function () {
            callback();
        };
    }
    script.src = url;
    document.getElementsByTagName("head")[0].appendChild(script);
}

/***
* Customized Select Menu
* Version: 0.0.1
* Last Update: 2013.05.30
* Author: Sean Huang
* Modified By: Teller Shen, Dan Yang
* JQuery Requires: 1.6.2+
* JS Method Requires: UI.laterEvent()
* CSS Style Requires: .select
* Call Method: bindSelect() or bindSelect("#id .select") or bindSelect($("#id .select"))
***/
var bindSelect = function (_obj) {
    var obj = (arguments.length != 0) ? $(_obj) : $(".select");
    obj.mouseenter(function () {
        if (this.sleepid) {
            clearTimeout(this.sleepid);
        }
    }).mouseleave(function () {
        var me = $(this);
        UI.laterEvent(function () {
            me.css({ zIndex: 0 }).removeClass("select_expand").find("dd").hide();
            me.parents("li").css({ zIndex: 0 });
        }, 200, this);
    }).delegate("dd a", "click", function () {
        var curselect = $(this).parents(".select");
        if (this.selected == "selected") {
            curselect.parents("li").css({ zIndex: 0 });
            // $(this).parents("dd").hide();
            $(this).parent().parent().parent("dd").hide();
            return;
        }
        var text = $(this).html();
        $(this).parents("dd").prev("dt").find("a").html(text);
        $(this).parents("dd").find("a").each(function () {
            this.selected = "";
        });
        this.selected = "selected";
        $(this).parent().addClass("selected").siblings("li").removeClass("selected");
        curselect.parents("li").css({ zIndex: 0 });
        curselect.siblings("input[name='" + curselect.attr("name") + "']").val($(this).attr("value"));
        curselect.removeClass("select_expand").trigger("change");
        // $(this).parents("dd").hide();
        $(this).parent().parent().parent("dd").hide();
    }).end().find("dt").click(function () {
        var curselect = $(this).parent(".select");
        if (curselect.is(".disabled")) { return false; }
        curselect.css({ zIndex: 1 }).find("dd").toggle();
        curselect.toggleClass("select_expand");
        curselect.parents("li").css({ zIndex: 1 });
        //Start: Options Width & Height Fix
        var ul_w = curselect.find("ul").width();
        var ul_h = curselect.find("ul").height();
        var dt_w = curselect.width();
        if (ul_h > 300) {
            ul_w += 15;
            ul_h = 300;
        }
        if (ul_w < dt_w) {
            curselect.find("ul").css({ width: dt_w });
            ul_w = dt_w;
        }
        curselect.find("dd").css({ width: ul_w, heigth: ul_h });
        //End: Options Width & Height Fix
    });
    obj.each(function () {
        var val = $(this).find("dt a").html();
        $(this).find("dd a").each(function () {
            if ($(this).html() == val) {
                this.selected = "selected";
                var curselect = $(this).parents(".select");
                $(this).parent().addClass("selected");
                curselect.siblings("input[name='" + curselect.attr("name") + "']").val($(this).attr("value"));
            }
        });
    });
}


//
function getBrowser(_b) {
    _b = _b.toString().toLowerCase();
    return (navigator.userAgent.toLowerCase().indexOf(_b) != -1) ? true : false;
}
/*
Global Method to fix Mask Overlay Height & Width
*/
function fixMaskLayer(_o) {
    if (getBrowser("msie 6")) {
        var o = $(_o);
        var relWidth;
        var relHeight;
        var wHeight;
        if (document.documentElement && document.documentElement.clientHeight) {
            var doc = document.documentElement;
            relWidth = (doc.clientWidth > doc.scrollWidth) ? doc.clientWidth - 1 : doc.scrollWidth;
            relHeight = (doc.clientHeight > doc.scrollHeight) ? doc.clientHeight : doc.scrollHeight;
            wHeight = doc.clientHeight;
        }
        else {
            var doc = document.body;
            relWidth = (window.innerWidth > doc.scrollWidth) ? window.innerWidth : doc.scrollWidth;
            relHeight = (window.innerHeight > doc.scrollHeight) ? window.innerHeight : doc.scrollHeight;
            wHeight = relHeight;
        }
        o.css({ width: relWidth, height: wHeight });
    }
}
//Mousewheel Plugin
(function ($) {
    $.event.special.mousewheel = {
        setup: function () {
            var handler = $.event.special.mousewheel.handler;
            // Fix pageX, pageY, clientX and clientY for mozilla
            if ($.browser.mozilla)
                $(this).bind('mousemove.mousewheel', function (event) {
                    $.data(this, 'mwcursorposdata', {
                        pageX: event.pageX,
                        pageY: event.pageY,
                        clientX: event.clientX,
                        clientY: event.clientY
                    });
                });
            if (this.addEventListener)
                this.addEventListener(($.browser.mozilla ? 'DOMMouseScroll' : 'mousewheel'), handler, false);
            else
                this.onmousewheel = handler;
        },
        teardown: function () {
            var handler = $.event.special.mousewheel.handler;
            $(this).unbind('mousemove.mousewheel');
            if (this.removeEventListener)
                this.removeEventListener(($.browser.mozilla ? 'DOMMouseScroll' : 'mousewheel'), handler, false);
            else
                this.onmousewheel = function () { };
            $.removeData(this, 'mwcursorposdata');
        },
        handler: function (event) {
            var args = Array.prototype.slice.call(arguments, 1);
            event = $.event.fix(event || window.event);
            // Get correct pageX, pageY, clientX and clientY for mozilla
            $.extend(event, $.data(this, 'mwcursorposdata') || {});
            var delta = 0, returnValue = true;
            if (event.wheelDelta) delta = event.wheelDelta / 120;
            if (event.detail) delta = -event.detail / 3;
            //if ( $.browser.opera ) delta = -event.wheelDelta;
            event.data = event.data || {};
            event.type = "mousewheel";
            // Add delta to the front of the arguments
            args.unshift(delta);
            // Add event to the front of the arguments
            args.unshift(event);
            return $.event.handle.apply(this, args);
        }
    };
    $.fn.extend({
        mousewheel: function (fn) {
            return fn ? this.bind("mousewheel", fn) : this.trigger("mousewheel");
        },
        unmousewheel: function (fn) {
            return this.unbind("mousewheel", fn);
        }
    });
})(jQuery);
/*
Global Popup Layer
Demo:
popWin("#popID",{...});
popWin(myPopWin,{...});
var pop = popWin("#popID",
{
action: "in" | "out", //可选项，用于初始化时直接弹入|弹出，此参考与obj.fn.popIn()|obj.fn.popOut()方法不可同时使用
animate : true | false, //可选项
speed: 200, //弹入|弹走的速度，可选项
overlay: String | object //可选项
olSpeed : 200, //mask层的fadeIn | fadeOut速度，可选项
beforeStart: funciton(), //在弹入窗口前执行的方法
callOnce: funciton(), //仅需在第一次弹入窗口动作完成后执行的方法
callback: funciton(), //在弹入窗口动作完成后执行的方法
afterPopOut : function, //在弹出窗口动作完成后执行的方法
}
pop.fn.popIn(); //调用内部方法，无缓动，直接弹入
pop.fn.popOut(); //调用内部方法，无缓动，直接弹出
pop.fn.popIn(true); //调用内部方法，有缓动，动画弹入
pop.fn.popOut(true); //调用内部方法，有缓动，动画弹出
*/
function PopWin(_o, _settings) {
    var self = this;
    if (!(self instanceof PopWin)) {
        return new PopWin(_o, _settings);
    }
    var o = $(_o);
    var ol = null;
    var settings = {};
    var _default = {
        x: ($(window).width() - o.outerWidth()) / 2,
        y: ($(window).height() - o.outerHeight()) / 2,
        action: "in",
        animate: false,
        speed: 200,
        overlay: "#overlay",
        olSpeed: 200,
        olOpacity: 0.7,
        queue: false,
        beforeStart: function () { },
        callOnce: function () { },
        callback: function () { },
        afterPopOut: function () { }
    }
    _default.y = (document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop) + ((_default.y > 0) ? _default.y : 0);
    if (_settings) {
        for (var v in _settings) {
            _default[v] = _settings[v];//Overwite defaults settings if customed parameters parsed in
        }
    }
    settings = _default;
    ol = $(settings.overlay);//Overlay DOM Obj
    //fix functions
    var fixFunc = {
        preventScroll: function () {
            return false;
        },
        preventResize: function () {
            return false;
        }
    };
    //funciton init
    self.initDOM = function () {
        //overlay style
        if (ol.length == 0) {
            ol = $('<div id="overlay" style="position:fixed;top:0;left:0;right:0;bottom:0;"></div>');
            if (getBrowser("msie 6")) {
                ol = $('<div id="overlay" style="position:absolute;left:0;width:0;height:0;"></div>');
            }
            $("body").append(ol);
            var olBgColor = ol.css("backgroundColor"),
            olBgImg = ol.css("backgroundImage"),
            olBgRpt = ol.css("backgroundRepeat"),
            olBgPst = ol.css("backgroundPosition");
            olBgColor = (olBgColor) ? olBgColor : "#000";
            olBgImg = (olBgImg) ? olBgImg : "url(base64:*.gif)";
            olBgRpt = (olBgRpt) ? olBgRpt : "repeat";
            olBgPst = (olBgPst) ? olBgPst : "left top";
            ol.css({
                backgroundColor: olBgColor,
                backgroundImage: olBgImg,
                backgroundRepeat: olBgRpt,
                backgroundPosition: olBgPst
            });
        }
        else {
            if (getBrowser("msie 6")) {
                ol.css("left", 0);
            } else {
                ol.css({
                    top: 0,
                    left: 0,
                    right: 0,
                    bottom: 0
                });
            }
        }
        //Pop Window style
        if (o.length != 0) {
            o.css({
                left: "50%",
                top: 0 - o.outerHeight(),
                marginLeft: 0 - o.outerWidth() / 2
            });
            if (o.css("zIndex") == "auto" || o.css("zIndex") == 0) {
                o.css("zIndex", 1000);
            }
            ol.css("zIndex", parseInt(o.css("zIndex")) - 1);
        }
    };//init end
    self.initDOM();
    settings.callOnce.call(o, o, self);
    //function obj
    self.fn = {
        popIn:
        function (_a, _fn, _data) {
            if (o.attr("moving") == "1") return false;
            if (o.attr("showed") == "1") return false;
            settings.animate = (_a != undefined) ? _a : settings.animate;
            self.fn.specialFix("beforeIn");
            //Call Customized BeforeStart Function
            if (settings.beforeStart) {
                settings.beforeStart.call(o, o, _data);
            }
            if (settings.animate == true) {
                self.fn.domMouseWheel(false);
                o.attr("moving", "1");
                fixMaskLayer(ol);
                ol.css({ opacity: 0, display: "block" }).animate({ opacity: settings.olOpacity, duration: settings.olSpeed },
                function () {
                    o.css("top", self.fn.getEndPos().inY).show().animate({
                        top: self.fn.getEndPos().y
                    }, {
                        duration: settings.speed,
                        queue: settings.queue,
                        complete: function () {
                            //Call Customized Callback Function
                            settings.callback.call(o, o);
                            (typeof _fn == "function") ? _fn.call(o, o) : '';
                            o.attr("showed", "1");
                            o.attr("moving", "0");
                            self.fn.addEvent();
                            self.fn.domMouseWheel(true);
                        }
                    });
                }
                );
            }
            else {
                ol.css({ opacity: settings.olOpacity, display: "block" });
                fixMaskLayer(ol);
                o.show().css({
                    top: self.fn.getEndPos().y
                });
                //Call Customized BeforeStart Function
                settings.callback.call(o, o);
                (typeof _fn == "function") ? _fn.call(o, o) : '';
                o.attr("showed", "1");
                o.attr("moving", "0");
                self.fn.addEvent();
            }
        },//popIn End
        popOut:
        function (_a, _fn) {
            if (o.attr("moving") == "1") return false;
            if (o.attr("showed") != "1") return false;
            settings.animate = (_a != undefined) ? _a : settings.animate;
            if (settings.animate == true) {
                self.fn.domMouseWheel(false);
                o.attr("moving", "1").show().animate({
                    top: self.fn.getEndPos().outY
                }, {
                    duration: settings.speed,
                    queue: settings.queue,
                    complete: function () {
                        //Call Customized Callback Function
                        o.hide().attr("showed", "0");
                        o.attr("moving", "0");
                        //ol.fadeOut(settings.olSpeed,function(){ol.css("width","100%")});
                        ol.fadeOut(settings.olSpeed);
                        settings.afterPopOut.call(o, o);
                        (typeof _fn == "function") ? _fn.call(o, o) : '';
                        self.fn.specialFix("afterOut");
                        self.fn.domMouseWheel(true);
                    }
                });
            }
            else {
                o.show().css({
                    top: 0 - self.fn.getEndPos().y,
                    display: "none"
                });
                //Call Customized BeforeStart Function
                o.attr("showed", "0");
                o.attr("moving", "0");
                //ol.css("width","100%").hide();
                ol.hide();
                settings.afterPopOut.call(o, o);
                (typeof _fn == "function") ? _fn.call(o, o) : '';
                self.fn.specialFix("afterOut");
            }
        },//popOut end
        getEndPos:
        function () {
            var st = (document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop);
            //var sl = (document.documentElement.scrollLeft ? document.documentElement.scrollLeft : document.body.scrollLeft);
            var win = $(window);
            var _y = (win.height() - o.outerHeight()) / 2;
            //var _x = ( win.width() - o.outerWidth() )/2;
            return {
                //x : sl + ((_x > 0 ) ? _x : 0),
                y: st + ((_y > 0) ? _y : 0),
                //curTop : st,
                inY: 0 - o.outerHeight() + st,
                outY: 0 - o.outerHeight() + st
            }
        },//getEndPos end
        addEvent://events which runs after popWindow finishing Entering
        function () {
            //When Scroll & Resize Window, Reposition the PopWindow
            $(window).bind("resize scroll", function (e) {
                (e.type == "resize") && fixMaskLayer(ol);
                self.fn.rePosition();
            });
            //More Events could happen here....
            //.....
        },//addEvent End
        rePosition:
        function (_a) {
            //if(o.attr("moving") == "1" ) return false;
            if (o.attr("showed") != "1") return false;
            settings.animate = (_a != undefined) ? _a : settings.animate;
            if (settings.animate == true) {
                o.attr("moving", "1");
                o.show().animate({
                    top: self.fn.getEndPos().y
                }, {
                    duration: 200,
                    queue: settings.queue,
                    complete: function () {
                        o.attr("moving", "0");
                    }
                });
            }
            else {
                o.show().css({
                    top: self.fn.getEndPos().y
                });
                o.attr("moving", "0");
            }
        },//rePostion end
        domMouseWheel:
        function (_f) {
            if (_f == false) {
                $(window).bind("resize", fixFunc.preventResize);
                $(document).bind("mousewheel", fixFunc.preventScroll);
            }
            else {
                $(window).unbind("resize", fixFunc.preventResize);
                $(document).unbind("mousewheel", fixFunc.preventScroll);
            }
        },
        specialFix://Special Fix Events which runs Before popIn & After popOut
        function (_f) {
            if (getBrowser("msie 6")) {
                var v = (_f == "beforeIn") ? "hidden" : "visible";
                for (var i = 0, selects = document.getElementsByTagName("select") ; i < selects.length; i++) {
                    selects[i].style.visibility = v;
                }
            }
            //More Special Fix could happens here....
            //.....
        }// specialFix end
    }//fn end
    //Bind click Event to Close Icon
    o.find(".close").bind("click", function () {
        self.fn.popOut(settings.animate);
    });
    if (_settings) {
        //初始化对象时，如果参数中设置了action为"in"|"out"，则执行相应的弹入|弹出的动作
        (_settings.action == "in") ? self.fn.popIn(_settings.animate) : "";
        (_settings.action == "out") ? self.fn.popOut(_settings.animate) : "";
    }
    return self;
}
