
jQuery(function () {
    InitPublicHeader();
});
InitPublicHeader = function () {
    var dataHeader = "";
    var date = new Date().getTime();
    var timestart = "2016-04-25";
    var time1 = (timestart + ' 08:00:00').toString();
    var beginDate = new Date(Date.parse(time1.replace(/-/g, "/"))).getTime();
    var timeend = "2016-04-27";
    var time2 = (timeend + ' 06:00:00').toString();
    var endDate = new Date(Date.parse(time2.replace(/-/g, "/"))).getTime();
    if (date > beginDate && date < endDate) {
        dataHeader += '<div style="color:#f00;width: 1200px;margin: 0 auto;text-align: center;">4月27日(周三)凌晨 00:00 - 06:00点，进行系统升级，届时将暂停所有预订服务，给您造成不便，敬请谅解。</div>'
    }
    dataHeader += '<div class="top_bg">'
    dataHeader += '    <div class="top">'
    dataHeader += '        <a href="http://www.jsj.com.cn"><span class="logo"></span></a>'
    dataHeader += '        <div class="top_center">'
    //dataHeader += '            <a class="hide" href="http://www.jsj.com.cn/mobile/download.htm" target="_blank" rel="nofollow"><span class="phoneapp"></span><b>手机APP</b><u></u></a>'
    dataHeader += '            <a class="hide" href="http://weibo.com/jinseshiji" target="_blank" rel="nofollow"><span class="weibo"></span><b>关注公众号</b><u></u></a>'
    dataHeader += '            <b>客服电话</b><dfn>110-110-110</dfn>'
    dataHeader += '        </div>'

    dataHeader += '    </div>'
    dataHeader += '</div>'
    dataHeader += '<div class="nav_width">'
    dataHeader += '    <div class="nav">'
    dataHeader += '        <div class="menu">'
    dataHeader += '            <ul>'
    dataHeader += '                <li class="index nav_current" isselected="1"><a href="http://www.jsj.com.cn"><u>首页</u></a></li>'
    dataHeader += '                <li class="nav_parent">'
    dataHeader += '                    <a href="javascript:;"><u>账号设置</u><i></i></a>'
    dataHeader += '                    <div class="nav_on" style="display: none;">'
    dataHeader += '                        <div class="center">'
    dataHeader += '                            <dl id="flight">'
    //dataHeader += '                                <dt><a href="http://zy1.jsjit.cn:8890/cflight/cflightindex">国内机票</a></dt>'
    //dataHeader += '                                <dt><a href="http://iflight.jsj.com.cn">国际机票</a></dt>'
    //dataHeader += '                                <dt><a href="http://www.jsj.com.cn/SMS_Guarder/">机票卫士</a></dt>'
    dataHeader += '                            </dl>'
    dataHeader += '                        </div>'
    dataHeader += '                    </div>'
    dataHeader += '                </li>'
    dataHeader += '                <li class="nav_parent">'
    dataHeader += '                    <a href="javascript:;"><u>我的消息</u><i></i></a>'
    dataHeader += '                    <div class="nav_on" style="display: none;">'
    dataHeader += '                        <div class="center">'
    dataHeader += '                            <dl id="hotel">'
    //dataHeader += '                                <dt><a href="http://hotel.jsj.com.cn/">国内酒店</a></dt>'
    //dataHeader += '                                <dt><a href="http://jinseshiji.wisetravel.cn/">国际酒店</a></dt>'
    dataHeader += '                            </dl>'
    dataHeader += '                        </div>'
    dataHeader += '                    </div>'
    dataHeader += '                </li>'
    //dataHeader += '                <li><a id="viphallUrl" href=""><u>金色逸站</u></a></li>'
    //dataHeader += '                <li><a id="trainTicketUrl" href="http://train.jsj.com.cn/"><u>火车票</u></a></li>'
    //dataHeader += '                <li><a href="http://www.jsj.com.cn/Lvyou/index.htm" class="nav_w"><u>旅游</u></a></li>'
    //dataHeader += '                <li class="card"><a href="http://www.jsj.com.cn/Card/index.html" class="nav_w"><u>金卡</u><em></em></a></li>'
    //dataHeader += '                <li><a href="http://www.jsj.com.cn/MemberServices/MemberIntegral/MemberStore.aspx" class="nav_w"><u>商城</u></a></li>'
    dataHeader += '            </ul>'
    dataHeader += '        </div>'
    dataHeader += '        <div class="login" id="Login">'
    dataHeader += '            <span class="user"></span>'
    dataHeader += '            <div class="login_lf">'
    dataHeader += '                <p><a id="loginUrl">登录&nbsp;&nbsp;|&nbsp;&nbsp;</a><a id="registeUrl">注册</a></p>'
    dataHeader += '            </div>'
    dataHeader += '            <div class="denglu" style="display:none;"><button>登录</button></div>'
    dataHeader += '        </div>'
    dataHeader += '        <div class="login" name="login" id="Logined" style="display:none">'
    dataHeader += '            <span class="user"></span>'
    dataHeader += '            <div class="login_lf">'
    dataHeader += '                <p><span></span></p>'
    dataHeader += '                <p><a href="javascript:;" class="fontsize16">我是雇主</a><b class="me"></b></p>'
    dataHeader += '            </div>'
    dataHeader += '            <div class="login_rg"><a id="loginOutUrl">退出</a></div>'
    dataHeader += '            <div class="login_list" style="display: none;">'
    dataHeader += '                <ul>'
    //dataHeader += '                    <li><a id="chinaFlightOrder" href="@ViewBag.BaseMyInfoUrl/CFlight/CFlightOrderList">国内机票订单</a></li>'
    //dataHeader += '                    <li><a id="iFlightOrder" href="@ViewBag.BaseMyInfoUrl/IFlight/IFlightOrderList">国际机票订单</a></li>'
    //dataHeader += '                    <li><a id="trainTicketOrder" href="@ViewBag.BaseMyInfoUrl/TrainTicket/TrainTicketOrder">火车票订单</a></li>'
    //dataHeader += '                    <li><a id="hotelOrder" href="http://zy3.jsjit.cn:8892/Hotel/HotelOrderList">国内酒店订单</a></li>'
    dataHeader += '                    <li>'
    dataHeader += '                        <ul class="border_top">'
    //dataHeader += '                            <li><a id="myInfo" href="@ViewBag.OnlineMemberUrl/MemberServices/MemberEdit.aspx">个人资料</a></li>'
    //dataHeader += '                            <li><a id="myAddress" href="@ViewBag.OnlineMemberUrl/MemberServices/CommonAddress.aspx">常用地址</a></li>'
    //dataHeader += '                            <li><a id="myFrequentTraveler" href="@ViewBag.OnlineMemberUrl/MemberServices/FrequentTraveler.aspx">常用旅客信息</a></li>'
    dataHeader += '                        </ul>'
    dataHeader += '                    </li>'
    dataHeader += '                </ul>'
    dataHeader += '            </div>'
    dataHeader += '        </div>'
    dataHeader += '    </div>'
    dataHeader += '</div>'





    $("body").prepend(dataHeader);
    //用户已经登录
    //if (!JSJ.String.IsEmpty($.cookie("EmployeeInfo"))) {
    //    $("#Login").hide();
    //    $("#Logined").show();
    //    var employeeInfo = eval('(' + $.cookie("EmployeeInfo") + ')');
    //    $("#Logined .login_lf P span").html(employeeInfo.REALNAME);
    //}
    //登录地址
    //$("#loginUrl,#myjsj").attr("href", JSJ.UrlMember + "/index?callback=" + encodeURIComponent(location.href));
    ////未登录
    //$('.denglu').click(function () {
    //    window.location.href = JSJ.UrlMember + "/index?callback=" + encodeURIComponent(location.href);
    //});
    ////注册地址
    //$("#registeUrl").attr("href", JSJ.UrlMember + "/register");
    ////推出登录地址
    //$("#loginOutUrl").attr("href", JSJ.UrlMember + "/loginout?callback=" + encodeURIComponent("http://" + location.host))
    ////火车票站点地址
    //$("#trainTicketUrl").attr("href", JSJ.TrainTicket_Url);
    ////贵宾厅站点地址
    //$("#viphallUrl").attr("href", JSJ.VipHall_Url);
    ////国内机票订单
    ////$("#chinaFlightOrder").attr("href", JSJ.OnlineMember_Url + "/MemberServices/MemberTicketOrder.aspx");
    //$("#chinaFlightOrder").attr("href", JSJ.MyInfo_Url + "/CFlight/CFlightOrderList");
    ////国际机票订单
    //$("#iFlightOrder").attr("href", JSJ.MyInfo_Url + "/IFlight/IFlightOrderList");
    ////火车票订单
    //$("#trainTicketOrder").attr("href", JSJ.MyInfo_Url + "/TrainTicket/TrainTicketOrder");
    ////个人信息
    //$("#myInfo").attr("href", JSJ.OnlineMember_Url + "/MemberServices/MemberEdit.aspx");
    ////常用地址
    //$("#myAddress").attr("href", JSJ.OnlineMember_Url + "/MemberServices/CommonAddress.aspx");
    ////常旅客
    //$("#myFrequentTraveler").attr("href", JSJ.OnlineMember_Url + "/MemberServices/FrequentTraveler.aspx");
    ////酒店订单
    //$("#hotelOrder").attr("href", JSJ.MyInfo_Url + "/Hotel/HotelOrderList");
    ///头部特效

    //头部右侧图片滚动
    var index = 0;
    var itStop = false;
    var it = setInterval(function () {
        if (itStop) {
            return;
        }
        if (index == $('.top_rightturn a').length - 1) {
            index = -1;
        }
        jQuery('.top_right:eq(0)').animate({ 'scrollLeft': 202 * (++index) }, 500);
    }, 3000);
    jQuery('.top_right:eq(0)').hover(function () {
        itStop = true;
    }, function () {
        itStop = false;
    });


    //头部右侧登录
    jQuery(".login[name='login']").mouseover(function () {
        jQuery(this).find("b.me").addClass("mehover");
        jQuery(".login_list").show();
    }).mouseout(function () {
        jQuery(this).find("b.me").removeClass("mehover");
        jQuery(".login_list").hide();
    });


    //头部菜单
    var w = jQuery(window).width();
    jQuery(".menu li").mouseover(function () {
        jQuery(".menu li[IsSelected!=1]").removeClass("nav_current");
        jQuery(this).addClass("nav_current");
        jQuery(".menu li").find("div.nav_on").hide();

        if (jQuery(this).find("div.nav_on").length > 0) {
            jQuery(this).find(".nav_on").css("width", w).show();
        } else {
            if (jQuery(".menu li[IsSelected=1]").find("div.nav_on").length > 0) {
                jQuery(".menu li[IsSelected=1]").find("div.nav_on").css("width", w).show();
            }
        }
    }).mouseleave(function () {
        if (jQuery(this).attr("IsSelected") != "1") {
            jQuery(this).removeClass("nav_current");
            jQuery(this).find("div.nav_on").hide();
        }
        jQuery(".menu li[IsSelected=1]").find("div.nav_on").css("width", w).show();

    });
    //登录隐藏
    jQuery("#Login").mouseover(function () {
        $(".denglu").show();
    }).mouseleave(function () {
        $(".denglu").hide();

    });
    //默认选中当前项目的菜单
    jQuery(".menu ul li.nav_current").removeAttr("isselected");
    jQuery(".menu ul li.nav_current").removeClass("nav_current");
    jQuery(".menu ul li a[href$='" + document.domain + "']:first").parent().attr("isselected", "1");
    jQuery(".menu ul li a[href$='" + document.domain + "']:first").parent().attr("class", "nav_current");
    //if (jQuery(".menu ul li:first").hasClass("nav_current") && jQuery(".menu ul li:first").attr("isselected") == 1) {        
    //    jQuery(".English").show();
    //}
    //new year 主题
    var myDate = new Date();
    //var tody = JSJ.Date.GetTimeByString(myDate.getFullYear() + '-' + (myDate.getMonth() + 1) + "-" + myDate.getDate());
    //var startTime = JSJ.Date.GetTimeByString("2016-2-5");
    //var endTime = JSJ.Date.GetTimeByString("2016-2-23");
    //if (startTime <= tody && tody <= endTime) {
    //    jQuery(".top_bg .top").addClass("newYear");
    //} else {
    //    jQuery(".top_bg .top").removeClass("newYear");
    //}
};
