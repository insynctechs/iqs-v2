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
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);

            routes.MapPageRoute("CategoryPage1", "{category}", "~/CategoryPage1.aspx");

        }

        private static void GetCategories()
        {
            client.BaseAddress = new Uri("");
        }
    }
}
