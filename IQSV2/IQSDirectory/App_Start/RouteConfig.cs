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

            routes.MapPageRoute("StateSearchCity",
                "{category}/{state}/{city}",
                "~/StateSearch.aspx",
                true,
                null,
                new RouteValueDictionary { { "category,state,city", new StateSearchConstraint() } }
                );

            
            routes.MapPageRoute("StateSearchRedirect",
               "{category}/canada/{state}/{city}",
               "~/StateSearch.aspx",
               true,
               null,
               new RouteValueDictionary { { "category,state", new StateSearchRedirectConstraint() } }
               );
            routes.MapPageRoute("StateSearchRedirect1",
               "{category}/{category1}/canada/{state}",
               "~/StateSearch.aspx",
               true,
               null,
               new RouteValueDictionary { { "category,state,category1", new StateSearchRedirectConstraint() } }
               );
            routes.MapPageRoute("StateSearchRedirect2",
              "{category}/{category1}/canada/{state}/{city}",
              "~/StateSearch.aspx",
              true,
              null,
              new RouteValueDictionary { { "category,state,category1,city", new StateSearchRedirectConstraint() } }
              );
            routes.MapPageRoute("StateSearchRedirect3",
              "{category}/{category1}/{category2}/canada/{state}",
              "~/StateSearch.aspx",
              true,
              null,
              new RouteValueDictionary { { "category,state,category1,category2", new StateSearchRedirectConstraint() } }
              );
            routes.MapPageRoute("StateSearchRedirect4",
              "{category}/{category1}/{category2}/canada/{state}/{city}",
              "~/StateSearch.aspx",
              true,
              null,
              new RouteValueDictionary { { "category,state,category1,category2,city", new StateSearchRedirectConstraint() } }
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
            routes.MapPageRoute("ListCompanies",
                "listcompanies/{letter}/{page}",
                "~/ListCompanies.aspx",
                true,
                null,
                null
                );

            /*routes.MapPageRoute("RedirectionChecking",
                "{*url}",
                "~/Redirection.aspx",
                true,
                null,
                null
                );*/

        }
    }
}
