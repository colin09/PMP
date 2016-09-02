/// <reference path="../js/jsj.js" />
jQuery(function () {
    InitPublicFooter();
});
InitPublicFooter = function () {
    var footerData = "";
    footerData += '<div class="ft" data-level="4">'
    footerData += '    <div class="foot">'
    footerData += '        <div class="footer_con">'
    //footerData+='            <div class="contant">'
    //footerData+='                <ul>'
    //footerData+='                    <li><a href="http://www.jsj.com.cn/BottomLink/about_intro.aspx">关于我们</a><u>|</u></li>'
    //footerData+='                    <li><a href="http://www.jsj.com.cn/sitemap.aspx">网站地图</a><u>|</u></li>'
    //footerData+='                    <li><a href="http://www.jsj.com.cn/BottomLink/dfdl.aspx">代理中心</a><u>|</u></li>'
    //footerData+='                    <li><a href="http://www.jsj.com.cn/BottomLink/job.aspx">招聘信息</a><u>|</u></li>'
    //footerData+='                    <li><a href="http://www.jsj.com.cn/Card/index.html">VIP服务</a><u>|</u></li>'
    //footerData+='                    <li><a href="http://www.jsj.com.cn/BottomLink/about_department.aspx">联系我们</a><u>|</u></li>'
    //footerData+='                    <li><a href="http://www.jsj.com.cn/BottomLink/flink.aspx">友情链接</a><u>|</u></li>'
    //footerData+='                </ul>'
    //footerData+='            </div>'
    footerData += '            <div class="contant_t">'
    footerData += '                <p>Copyright 2016-2017</p>'
    footerData += '            </div>'
    footerData += '        </div>'
    footerData += '    </div>'
    footerData += '</div>'

    $("body").append(footerData);

};
