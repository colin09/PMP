﻿using System.Web;
using System.Web.Optimization;

namespace com.pmp.web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/js/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/js/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/js/bootstrap.js"
                      //,"~/Scripts/respond.js"
                      ));
            
            bundles.Add(new ScriptBundle("~/bundles/jq-bs-ng").Include(
                     "~/Scripts/js/jquery-1.10.2.min.js",
                     //"~/Scripts/jquery.cxscroll.min.js",
                    // "~/Scripts/jquery.lazyload.min.js",
                     //"~/Scripts/js/jquery.cxscroll.min.js",
                     "~/Scripts/js/jquery.lazyload.min.js",

                     "~/Scripts/js/angular.min.js",
                     "~/Scripts/js/angular-sanitize.min.js",
                     "~/Scripts/js/bootstrap.min.js",
                     "~/Scripts/js/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/main.css"));


        }
    }
}
