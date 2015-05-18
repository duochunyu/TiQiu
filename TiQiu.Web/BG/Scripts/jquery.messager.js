(function() {
    var ua = navigator.userAgent.toLowerCase();
    var is = (ua.match(/\b(chrome|opera|safari|msie|firefox)\b/) || ['', 'mozilla'])[1];
    var r = '(?:' + is + '|version)[\\/: ]([\\d.]+)';
    var v = (ua.match(new RegExp(r)) || [])[1];
    jQuery.browser.is = is;
    jQuery.browser.ver = v;
    jQuery.browser[is] = true;
})();
(function(jQuery) {
    this.version = '@1.3';
    this.layer = { 'width': 200, 'height': 100 };
    this.title = '信息提示';
    this.time = 4000;
    this.anims = { 'type': 'slide', 'speed': 600 };
    this.timer1 = null;
    this.display = 'none';

    this.inits = function(title, text) {
        if ($("#message").is("div")) { return; }
        $("#tipsBar").prepend('<div id="message" style="z-index: 100; width: ' + this.layer.width + 'px; height: ' + this.layer.height + 'px; position: absolute;display: none; bottom: 0; right: 0; overflow: hidden; margin-right: 2px; background: white;margin-bottom:2px;"><div style="border: #dddddc 1px solid; width: ' + (this.layer.width - 2) + 'px; height: 25px; overflow: hidden;background: url(Images/GroupBox/GroupTitleBg.gif) repeat-x; font-weight: bold;font-size: 12px;"><span id="message_close" title="关闭" style="float: right; padding: 5px 0 5px 0; width: 16px;line-height: auto; color: red;font-size:16px; text-align: center; cursor: pointer; overflow: hidden;">×</span><div style="line-height: 25px; text-align: left; overflow: hidden; padding-left: 8px;color: gray;">' + title + '</div><div style="clear: both;"></div></div><div style="width: ' + (this.layer.width - 2) + 'px; height: auto; font-size: 12px; border: #dddddc 1px solid;border-top: none; border-bottom: none;"><div id="message_content" style="font-size: 12px; margin: 8px; width: 100%; height: ' + (this.layer.height - 47) + 'px;color: #1f336b; text-align: left; overflow: hidden;">' + text + '</div></div><div style="background: url(Images/GroupBox/GroupBoxBg.gif) repeat-x 50% bottom;height: 4px; width: ' + (this.layer.width - 2) + 'px;"><div style="display: inline; float: left; height: 100%; width: 4px; font-size: 0px;background: url(Images/GroupBox/GroupBoxLeftBg.gif) no-repeat;"></div><div style="display: inline; float: right; height: 100%; width: 4px; font-size: 0px;background: url(Images/GroupBox/GroupBoxRightBg.gif) no-repeat;"></div></div></div>');

        $("#message_close").click(function() {
            setTimeout('this.close()', 1);
        });
    };
    this.show = function(title, text, time) {
        if ($("#message").is("div")) { return; }
        if (title == 0 || !title) title = this.title;
        this.inits(title, text);
        this.display = 'block';
        if (time >= 0) this.time = time;
        switch (this.anims.type) {
            case 'slide': $("#message").slideDown(this.anims.speed); break;
            case 'fade': $("#message").fadeIn(this.anims.speed); break;
            case 'show': $("#message").show(this.anims.speed); break;
            default: $("#message").slideDown(this.anims.speed); break;
        }
        if ($.browser.is == 'chrome') {
            setTimeout(function() {
                $("#message").remove();
                this.inits(title, text);
                $("#message").css("display", "block");
            }, this.anims.speed - (this.anims.speed / 5));
        }
        this.rmmessage(this.time);
    };
    this.lays = function(width, height) {
        if ($("#message").is("div")) {
            return;
        }
        if (width != 0 && width) this.layer.width = width;
        if (height != 0 && height) this.layer.height = height;
    };
    this.anim = function(type, speed) {
        if ($("#message").is("div")) {
            return;
        }
        if (type != 0 && type) this.anims.type = type;
        if (speed != 0 && speed) {
            switch (speed) {
                case 'slow':
                    ;
                    break;
                case 'fast':
                    this.anims.speed = 200;
                    break;
                case 'normal':
                    this.anims.speed = 400;
                    break;
                default:
                    this.anims.speed = speed;
            }
        }
    };
    this.rmmessage = function(time) {
        if (time > 0) {
            timer1 = setTimeout('this.close()', time);
        }
    };
    this.changeText = function(text) {
        if ($("#message_content").is("div")) {
            $("#message_content").html(text);
        }
    };
    this.close = function() {
        switch (this.anims.type) {
            case 'slide':
                $("#message").slideUp(this.anims.speed);
                break;
            case 'fade':
                $("#message").fadeOut(this.anims.speed);
                break;
            case 'show':
                $("#message").hide(this.anims.speed);
                break;
            default:
                $("#message").slideUp(this.anims.speed);
                break;
        }
        ;
        setTimeout('$("#message").remove();', this.anims.speed);
        this.original();
    };
    this.original = function() {
        this.layer = { 'width': 200, 'height': 100 };
        this.title = '信息提示';
        this.time = 4000;
        this.anims = { 'type': 'slide', 'speed': 600 };
        this.display = 'none';
    };
    jQuery.messager = this;
    return jQuery;
})(jQuery);