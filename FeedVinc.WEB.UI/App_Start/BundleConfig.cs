using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace FeedVinc.WEB.UI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region SiteStyleBundles
            bundles.Add(new StyleBundle("~/bundles/style").Include(
            "~/Content/Site/assets/css/bootstrap.css",
            "~/Content/Site/assets/css/remodal.css",
            "~/Content/Site/assets/css/remodal-default-theme.css",
            "~/Content/Site/assets/css/jquery.dropdown.css",
            "~/Content/Site/assets/css/jquery.mCustomScrollbar.css",
            "~/Content/Site/assets/css/style.css",
            "~/Content/Site/assets/css/media-queries.css"
                    ).Include("~/Content/Site/assets/css/font-awesome.css", new CssRewriteUrlTransform()));
            #endregion

            #region SiteScriptBundles
            bundles.Add(new ScriptBundle("~/bundles/script").Include(
                "~/Content/Site/assets/js/jquery-1.11.2.min.js",
                "~/Content/Site/assets/js/modernizr-custom.js",
                "~/Content/Site/assets/js/remodal.js",
                "~/Content/Site/assets/js/bootstrap.min.js",
                "~/Content/Site/assets/js/jquery.dropdown.js",
                "~/Content/Site/assets/js/jquery.mCustomScrollbar.min.js",
                "~/Content/Site/assets/js/jquery.nicefileinput.js",
                "~/Content/Site/assets/js/notifyMe.js",
                "~/Content/Site/assets/js/jquery.raty-fa.js",
                "~/Content/Site/assets/js/scripts.js"
                ));
            #endregion

            BundleTable.EnableOptimizations = true;

        }
    }
}