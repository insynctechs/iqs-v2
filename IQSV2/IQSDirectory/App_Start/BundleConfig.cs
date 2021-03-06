﻿using System;
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
                "~/Content/master_styles.css"
                
                ));

            bundles.Add(new StyleBundle("~/bundles/MainStyles").Include(                
                "~/Styles/materialize.css",
                "~/Styles/main_style.css",
                "~/Styles/jquery-ui.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/SiteMasterScripts").Include(
                "~/Scripts/jquery-1.7.2.min.js",                
                "~/Scripts/jquery-ui.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/MainScripts").Include(
               "~/Scripts/jquery-2.1.1.min.js",
               "~/Scripts/materialize.js",
               "~/Scripts/init.js",
               "~/Scripts/jquery-ui-notab.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/MainScripts2").Include(
               "~/Scripts/jquery-1.7.2.min.js",
               "~/Scripts/jquery-ui.js"
               ));

            bundles.Add(new StyleBundle("~/bundles/StyleCategoryPage1").Include(
                "~/Content/category_styles.css",
                "~/Content/stylerprint.css"                
                ));

            bundles.Add(new ScriptBundle("~/bundles/CategoryScripts").Include(
                /*"~/Scripts/jquery.rating.pack.js",
                "~/Scripts/jquery.fancybox-1.3.4.js", */               
                "~/Scripts/category_page1.js"               
                ));

            bundles.Add(new ScriptBundle("~/bundles/ScriptCategoryPage1").Include(                
                "~/Scripts/jquery.rating.pack.js",               
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

            bundles.Add(new ScriptBundle("~/bundles/StateSearchScripts").Include(
               "~/Scripts/jquery.rating.pack.js",
               "~/Scripts/jquery.fancybox-1.3.4.js",
               "~/Scripts/category_page2.js"               
               ));

            bundles.Add(new StyleBundle("~/bundles/StyleCopro").Include(
                "~/Content/form_styles.css",
                "~/Content/copro_styles_min.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/ScriptCopro").Include(
                "~/Scripts/jquery.rating.pack.js",
                "~/Scripts/jquery.fancybox-1.3.4.js",
                "~/Scripts/jquery.cookie.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/company_profile.js",
                "~/Scripts/move_top.js",
                "~/Scripts/fb.js"
                ));

            BundleTable.EnableOptimizations = true;

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