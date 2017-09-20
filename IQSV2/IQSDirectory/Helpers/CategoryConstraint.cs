using IQSDirectory.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace IQSDirectory
{
    public class CategoryConstraint: IRouteConstraint
    {
        WebApiHelper wHelper = new WebApiHelper();

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return GetCategories().Any(x => x["NAME"].ToString().ToLower() == values[parameterName].ToString().ToLower());
        }

        private List<DataRow> GetCategories()
        {
            var url = string.Format("api/CategoryPages/GetCategoryList");
            DataSet dt = wHelper.GetDataSetFromWebApi(url);
            List<DataRow> categories = dt.Tables[0].AsEnumerable().ToList();
            return categories;
        }
    }
}