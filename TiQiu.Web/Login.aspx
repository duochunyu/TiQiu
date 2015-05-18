<%@ Page AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TiQiu.Web.Login" Language="C#" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html class="webkit" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>会员登录_踢球去（www.tiqiu365.com）去踢球找踢球去 </title>
    <meta name="keywords" content="踢球去登陆,踢球去登陆" />
    <meta name="description" content="登录踢球去，第一时间掌握最新球场赛事信息，让精彩瞬间不再擦肩而过。" />
    <link href="Styles/login/login.css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/public/html5.js"></script>
    <script type="text/javascript" src="Scripts/public/lab/LAB.js"></script>
    <meta name="currentUserId" content="" />
    <script>
        $LAB
.script('Scripts/public/jQuery/jquery-1.6.4.min.js')
.script('Scripts/search/search.js')
.script('Scripts/search/search_bar.js')
.script('Scripts/login/login.js');
    </script>
</head>
<body class="footer_bg">
    <form id="loginForm" runat="server">
        <header class="login_header">
            <div class="logo_login">
                <a href="/">
                    <h2>注册踢球去</h2>
                </a>
            </div>
        </header>
        <div class="wrap">
            <div class="loginbox_bg">
                <div class="loginbox_bg2 ">
                    <div class="w960">
                        <div class="loginbox">
                            <h1 class="title color1">踢球去，时刻掌握最新球场信息</h1>
                            <div class="top">
                                <h2 id="index_login" class="title1 color2">会员登录
                                
                                </h2>


                                <div id="form_login" class="form">
                                    <asp:Panel ID="pMsg" runat="server" Visible="false" CssClass="error_tips color1">
                                        <asp:Literal ID="litErrorMsg" Text="11111111" runat="server"></asp:Literal>
                                    </asp:Panel>

                                    <input type="hidden" name="type" value="page" />
                                    <input type="hidden" name="forward" />

                                    <div class="formtip">
                                        <p class="color5" id="login_error_msg_form" runat="server"></p>
                                    </div>

                                    <div>
                                        <input type="text" class="ipt ipt_name" id="uu_email" name="user" runat="server" />
                                    </div>


                                    <div class="pwd_box mt15">
                                        <input type="password" class="ipt ipt_pwd" id="uu_password" runat="server" name="password" /><span id="password_desc2" class="pwd_desc" runat="server">密码</span>
                                    </div>



                                    <div class="mt10">
                                        <input class="chk_auto" type="radio" id="radioNormalLogin" name="rememberMe" checked="True" runat="server" value="普通登录" />
                                        <label for="radioNormalLogin">普通登录</label>
                                        <input class="chk_auto" type="radio" id="radioMerchantLogin" name="rememberMe" runat="server" value="商家登录" />
                                        <label for="radioMerchantLogin">商家登录</label>
                                    </div>
                                    <div class="mt10">
                                        <a class="btn_login btn_a1">
                                            <asp:Button ID="ibtnLogin" runat="server" Height="34" Text="快速登录" OnClick="ibtnLogin_Click"></asp:Button>
                                        </a>
                                    </div>


                                </div>

                            </div>

                            <div class="bottom">
                                <div class="bdr">
                                    <p class="b_t color4">还没有踢球去账号？</p>
                                    <p class="b_b">
                                        <a id="A1" href="WebPages/Register.aspx" class="btn_reg btn_a2" runat="server"><span>快速注册新账号</span></a>
                                    </p>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
            <footer class="pub_footer">

                <div class="footer_wrap">

                    <div class="footer_box w960 clearfix">
                        
                    <div id="footer" class="align-center">
                    <div class="footer-help"><a href="../WebPages/Help.aspx">帮助中心</a></div>
                    <div class="copyright">Copyrights © tiqiu365.com ALl rights reserved <a href="http://www.miitbeian.gov.cn/">蜀ICP备13018625</a></div>
                        </div>                            
                    
                </div>
            </footer>

        </div>
    </form>
</body>

</html>
