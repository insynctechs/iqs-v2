using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;
using System.Net.Http;

namespace IQSDirectory
{
    public static class RouteConfig
    {
        static HttpClient client = new HttpClient();
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            //settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);

            routes.MapPageRoute("CategoryPage1", 
                "{category}", 
                "~/CategoryPage1.aspx", 
                true,
                null,
                new RouteValueDictionary { { "category", new CategoryPage1Constraint() } }
                );

            routes.MapPageRoute("CategoryPage2",
                "{category1}/{category2}-2",
                "~/CategoryPage2.aspx",
                true,
                null,
                new RouteValueDictionary { { "category1,category2", new CategoryPage2Constraint() } }
                );

            routes.MapPageRoute("StateSearch",
                "{category}/{state}",
                "~/StateSearch.aspx",
                true,
                null,
                new RouteValueDictionary { { "category,state", new StateSearchConstraint() } }
                );

            routes.MapPageRoute("StateSearchCan",
                "{category}/canada/{state}",
                "~/StateSearch.aspx",
                true,
                null,
                new RouteValueDictionary { { "category,state", new StateSearchConstraint() } }
                );

            routes.MapPageRoute("CompanyProfile",
                "profile/{copro}",
                "~/CompanyProfile.aspx",
                true,
                null,
                new RouteValueDictionary { { "copro", new CompanyProfileConstraint() } }
                );

            routes.MapPageRoute("SearchWithState",
                "search/{query}/{page}/{state}",
                "~/DirectorySearch.aspx",
                true,
                null,
                null
                );

            routes.MapPageRoute("SearchWithoutState",
                "search/{query}/{page}",
                "~/DirectorySearch.aspx",
                true,
                null,
                null
                );
        }
    }
}
