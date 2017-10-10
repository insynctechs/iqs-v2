using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace IQSDirectory
{
    public class BundleConfig
    {
        // For more information on Bundling, visit https://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                            "~/Scripts/WebForms/WebForms.js",
                            "~/Scripts/WebForms/WebUIValidation.js",
                            "~/Scripts/WebForms/MenuStandards.js",
                            "~/Scripts/WebForms/Focus.js",
                            "~/Scripts/WebForms/GridView.js",
                            "~/Scripts/WebForms/DetailsView.js",
                            "~/Scripts/WebForms/TreeView.js",
                            "~/Scripts/WebForms/WebParts.js"));

            // Order is very important for these files to work, they have explicit dependencies
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/bundles/SiteMasterStyles").Include(
                "~/Content/master_styles.css",
                "~/Content/searchbox_styles.css",
                "~/Content/jquery-ui.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/SiteMasterScripts").Include(
                "~/Scripts/jquery-1.7.2.min.js",
                "~/Scripts/jquery-ui.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/ScriptCategoryPage1").Include(
                "~/Content/dynamic_styles.css",
                "~/Content/stylerprint.css",
                "~/Content/jquery-ui.css",
                "~/Content/jquery.fancybox-1.3.4.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/StyleCategoryPage1").Include(
                "~/Scripts/jquery-1.7.2.min.js",
                "~/Scripts/jquery.rating.pack.js",
                "~/Scripts/jquery-ui.js",
                "~/Scripts/jquery.fancybox-1.3.4.js",
                "~/Scripts/fb.js",
                "~/Scripts/category_page1.js",
                "~/Scripts/move_top.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/StyleStateSearch").Include(
                "~/Content/category_styles.css",
                "~/Content/stylerprint.css",
                "~/Content/jquery.fancybox-1.3.4.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/ScriptStateSearch").Include(
                "~/Scripts/jquery.rating.pack.js",
                "~/Scripts/jquery.fancybox-1.3.4.js",
                "~/Scripts/fb.js",
                "~/Scripts/category_page2.js",
                "~/Scripts/move_top.js"
                ));

            ScriptManager.ScriptResourceMapping.AddDefinition(
                "respond",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/respond.min.js",
                    DebugPath = "~/Scripts/respond.js",
                });
        }
    }
}