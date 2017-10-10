using IQSDirectory.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace IQSDirectory
{
    public class CategoryPage1Constraint: IRouteConstraint
    {
        WebApiHelper wHelper = new WebApiHelper();

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return GetCategoryIdByName(values[parameterName].ToString().ToLower());
            //return GetCategories().Any(x => x["NAME"].ToString().ToLower() == values[parameterName].ToString().ToLower());
        }

        private bool GetCategoryIdByName(string DisplayName)
        {
            var url = string.Format("api/CategoryPages/GetCategoryIdByName?DisplayName=" + DisplayName);
            DataTable dt = wHelper.GetDataTableFromWebApi(url);
            if(dt.Rows.Count >0)
            {
                return true;
            }
            return false;
        }
    }

    public class CategoryPage2Constraint : IRouteConstraint
    {
        WebApiHelper wHelper = new WebApiHelper();

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if(values[parameterName.Split(',')[0]].ToString() != values[parameterName.Split(',')[1]].ToString())
            {
                return false;
            }
            return GetCategoryIdByName(values[parameterName.Split(',')[0]].ToString().ToLower());
        }

        private bool GetCategoryIdByName(string DisplayName)
        {
            var url = string.Format("api/CategoryPages/GetCategoryIdByName?DisplayName=" + DisplayName);
            DataTable dt = wHelper.GetDataTableFromWebApi(url);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
    }

    public class StateSearchConstraint : IRouteConstraint
    {
        WebApiHelper wHelper = new WebApiHelper();

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return GetCategoryIdByName(values[parameterName.Split(',')[0]].ToString().ToLower(), values[parameterName.Split(',')[1]].ToString().ToLower());
        }

        private bool GetCategoryIdByName(string Category, string State)
        {
            var url = string.Format("api/CategoryPages/GetCategoryStateValidate?Category=" + Category + "&State=" + State);
            DataTable dt = wHelper.GetDataTableFromWebApi(url);
            return Convert.ToBoolean(dt.Rows[0][0]);
        }
    }
}