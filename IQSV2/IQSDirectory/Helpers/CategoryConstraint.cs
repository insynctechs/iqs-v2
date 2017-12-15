﻿using IQSDirectory.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
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
            HttpContext.Current.Response.StatusCode = 301;
            HttpContext.Current.Response.Redirect(Utils.WebURL);
            HttpContext.Current.Response.End();
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
            HttpContext.Current.Response.StatusCode = 301;
            HttpContext.Current.Response.Redirect(Utils.WebURL);
            HttpContext.Current.Response.End();
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

    public class CompanyProfileConstraint : IRouteConstraint
    {
        WebApiHelper wHelper = new WebApiHelper();

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return GetCompanyProfileById(values[parameterName].ToString().ToLower());
        }

        private bool GetCompanyProfileById(string copro)
        {
            if (copro.LastIndexOf('-') > 0)
            {
                string Client_SK = copro.Substring(copro.LastIndexOf('-')).Trim('-');
                var url = string.Format("api/Clients/GetCompanyProfileById?Client_SK=" + Client_SK);
                DataTable dt = wHelper.GetDataTableFromWebApi(url);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["COPRO_URL"].ToString().ToLower().Replace("profile/","").Replace("/","")  == copro.ToLower())
                        {
                            return true;
                        }
                    }
                }
            }
            HttpContext.Current.Response.StatusCode = 301;
            HttpContext.Current.Response.Redirect(Utils.WebURL);
            HttpContext.Current.Response.End();
            return false;
        }
    }
}