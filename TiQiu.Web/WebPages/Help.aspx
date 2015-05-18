<%@ Page Language="C#" MasterPageFile="../MasterPages/Content.master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="TiQiu.Web.WebPages.Help" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" runat="server">
    <link href="../Scripts/JQueryUI/css/smoothness/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" />

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            height: 60px;
        }

        .box1 {
            width: 275px;
            margin-left: 15px;
            float: left;
        }

        .title {
            color: #6c9900;
            font-size: 14px;
            padding-bottom: 10px;
        }

        .list li {
            padding: 10px 0px;
            height: 82px;
        }

            .list li img {
                float: left;
            }

            .list li p {
                margin-left: 145px;
                padding: 5px 0px;
            }

        .box2 {
            border: 1px solid rgb(217, 215, 215);
            margin-left: 25px;
            padding: 15px;
            width: 520px;
            float: left;
            display: inline;
        }

            .box2 img {
                float: left;
            }

            .box2 h2 {
                font-size: 14px;
                margin-bottom: 10px;
            }

            .box2 h2, .box2 p {
                margin-left: 252px;
            }

        .list2 li {
            height: 30px;
            line-height: 30px;
            overflow: hidden;
        }

            .list2 li span {
                display: inline-block;
                margin: 0px 10px;
            }

        .create-btn {
            background: url("../Images/create-btn.png") no-repeat scroll 0 0 transparent;
            color: #FFFFFF;
            display: inline-block;
            height: 25px;
            line-height: 25px;
            text-align: center;
            width: 87px;
        }

        .help .help_menu {
            width: 200px;
            background-color: rgb(47, 108, 189);
        }

            .help .help_menu dt {
                height: 55px;
            }

                .help .help_menu dt a {
                    padding: 0px 40px 0px 30px;
                    height: 54px;
                    color: rgb(255, 255, 255);
                    line-height: 54px;
                    overflow: hidden;
                    font-size: 14px;
                    font-weight: bold;
                    border-bottom-color: rgb(109, 152, 208);
                    border-bottom-width: 1px;
                    border-bottom-style: solid;
                    display: block;
                }

                    .help .help_menu dt a:hover {
                        background-position: 165px -71px;
                        background-color: rgb(88, 137, 201);
                    }

            .help .help_menu .expand a {
                background: url("../Nest/img/help_arrow.gif") no-repeat 165px 23px;
            }

            .help .help_menu .now a {
                background-position: 165px -25px;
                color: rgb(255, 204, 1);
                border-bottom-color: rgb(32, 75, 131);
                background-color: rgb(32, 75, 131);
            }

                .help .help_menu .now a:hover {
                    background-position: 165px -25px;
                    color: rgb(255, 204, 1);
                    border-bottom-color: rgb(32, 75, 131);
                    background-color: rgb(32, 75, 131);
                }

            .help .help_menu dd {
                padding-bottom: 20px;
                display: none;
                background-color: rgb(32, 75, 131);
            }

            .help .help_menu li {
                height: 30px;
                line-height: 30px;
            }

                .help .help_menu li a {
                    height: 30px;
                    line-height: 30px;
                }

                .help .help_menu li a {
                    color: rgb(255, 255, 255);
                    padding-left: 30px;
                    display: block;
                }

                    .help .help_menu li a:hover {
                        color: rgb(255, 204, 1);
                    }

                .help .help_menu li .now {
                    color: rgb(255, 204, 1);
                }

        .help .help_content {
            width: 780px;
            float: right;
        }

        .fl {
            float: left;
        }
    </style>
    <div class="help">
        <div class="fl help_menu">
            <dl>
                <dt class="expand"><a href="###">关于踢球去 </a></dt>


                <dt class="expand"><a href="###">常见问题</a></dt>


                <dt class="expand"><a href="###">联系我们</a></dt>
                <dd style="display: none;">
                    <ul>

                        <li><a href="">企业简介</a></li>

                        <li><a href="">发展历程</a></li>

                        <li><a href="">历史荣誉</a></li>

                    </ul>
                </dd>

                <dt class="expand  now"><a href="###">联系我们</a></dt>
                <dd style="display: block;">
                    <ul>

                        <li><a href=">联系我们</a></li>

                    </ul>
                </dd>

                <dt><a href="" target="_blank">投资者关系</a></dt>
                <dt><a href="">网站地图</a></dt>



            </dl>

        </div>
        <div class="help_content">
            <div style="float: left; margin-left: 25px; display: inline; padding: 10px;">
                <h1 style="font-size: 25px;">关于踢球去
                </h1>
                <p>
                    踢球去（ www.tiqiu365.com）是由成都踢球去信息技术有限公司推出的足球预定、约战平台。
                </p>
                <p>
                    踢球去整合了成都市近200块足球场和近1000多支球队供广大足球爱好者选择预定和约战，
                </p>
                <p>
                    随着成都足球事业的发展和足球爱好者群体的增加，踢球去致力于为广大足球爱好者提供快捷的线上预定、约战等服务，
                </p>
                <p>
                    是国内首家在足球领域完成线上预定和约战的专业性网站，踢球去全力维护真实、安全、诚信的服务环境，

                </p>
                <p>
                    并建立严格的手机认证机制和会员投诉机制，通过客服人工审核、技术屏蔽、会员投诉等方式屏蔽不良会员，
                </p>
                <p>
                    保证征友会员的质量、预定、约战等过程的安全。踢球去创始团队是一群爱好足球，经常投入到足球运动中的年轻人，
                </p>
                <p>
                    希望和广大足球爱好者一起享受足球带给大家的快乐。
                </p>
                <p>
                    踢球去的目标是通过不断探索和创新，为中国足球事业和广大爱好足球的人们做出自己的贡献。
                </p>


            </div>
            <div style="float: left; margin-left: 25px; display: inline; padding: 10px; margin-top: 10px;">
                <h1 style="font-size: 25px;">常见问题</h1>
                <p>
                    账户无法注册成功的几种情况:
                </p>

                <p>
                    如您未能成功注册会员，请查看下面所列原因及解决方法：
                </p>
                <p>
                    1、您输入的用户名已存在。
                </p>
                <p>
                    2、我们保留了用户名，以阻止垃圾邮件或滥用行为。
                </p>
                <p>
                    3、已经用过的电话号码，一个电话号码只能对应一个踢球去会员帐号。
                </p>
                <p>
                    4、请注意输入法切换在半角状态，内容输入完毕后不要留空格。
                </p>
                <p>
                    5、如提示输入的会员名不能用，请点击这里了解解决办法（会员名长度请控制在5-25 个字符，一个汉字为2个字符）。
                </p>
                <p>
                    6、如提示验证码输入错误，请重新验证。
                </p>
                <p>
                    7、如提示填表项数据异常的，请重启电脑后重新注册。
                </p>

            </div>

            <div style="float: left; margin-left: -650px; display: inline; padding: 10px; margin-top: 240px;">
                <h1 style="font-size: 25px;">联系我们</h1>
                <p>
                    联系地址：四川省成都市XXXX
                </p>
                <p>
                    邮政编码：610041
                </p>
                <p>
                    服务热线：xxxxx
                </p>
                <p>
                    电子邮件：xxxxx
                </p>
                <p>
                    业务联系
                </p>
                <p>
                    QQ：xxxxx  电话：xxxxxx

                </p>
            </div>
        </div>
    </div>
</asp:Content>
