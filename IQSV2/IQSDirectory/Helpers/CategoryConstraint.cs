using IQSDirectory.Helpers;
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
            HttpContext.Current.Response.RedirectPermanent(Utils.WebURL);
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
            HttpContext.Current.Response.RedirectPermanent(Utils.WebURL);
            HttpContext.Current.Response.End();
            return false;
        }
    }

    public class StateSearchConstraint : IRouteConstraint
    {
        WebApiHelper wHelper = new WebApiHelper();

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return GetCategoryIdByName(values[parameterName.Split(',')[0]].ToString().ToLower(), values[parameterName.Split(',')[1]].ToString().ToLower(), parameterName.Split(',').Length);
        }

        private bool GetCategoryIdByName(string Category, string State, int paramcount)
        {
            var url = string.Format("api/CategoryPages/GetCategoryStateValidate?Category=" + Category + "&State=" + State);
            DataTable dt = wHelper.GetDataTableFromWebApi(url);
            bool val = Convert.ToBoolean(dt.Rows[0][0]);
            if (val == true)
            {
                if(paramcount >= 3)
                {
                    HttpContext.Current.Response.StatusCode = 301;
                    HttpContext.Current.Response.RedirectPermanent(Utils.WebURL + Category + "/" + State);
                    HttpContext.Current.Response.End();
                    return false;
                }
                return true;
            }
                
            else
            {
                url = string.Format("api/CategoryPages/GetCategoryIdByName?DisplayName=" + State);
                dt = wHelper.GetDataTableFromWebApi(url);
                if (dt.Rows.Count > 0)
                {
                    HttpContext.Current.Response.StatusCode = 301;
                    HttpContext.Current.Response.RedirectPermanent(Utils.WebURL + State + "/");
                    HttpContext.Current.Response.End();
                    return false;                    
                }
                else
                {
                    url = string.Format("api/CategoryPages/GetCategoryIdByName?DisplayName=" + Category);
                    dt = wHelper.GetDataTableFromWebApi(url);
                    if (dt.Rows.Count > 0)
                    {
                        HttpContext.Current.Response.StatusCode = 301;
                        HttpContext.Current.Response.RedirectPermanent(Utils.WebURL + Category + "/");
                        HttpContext.Current.Response.End();
                        return false;
                    }
                }
                return false;
            }
            
        }
    }

    public class StateSearchRedirectConstraint : IRouteConstraint
    {
        WebApiHelper wHelper = new WebApiHelper();

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string[] pnames = parameterName.Split(',');
            if (Array.IndexOf(pnames, "category1")>-1)
            {
                if (Array.IndexOf(pnames, "category2") > -1)
                {
                    return GetCategoryIdByName(values[parameterName.Split(',')[0]].ToString().ToLower(), values[parameterName.Split(',')[1]].ToString().ToLower(),"canada", values[parameterName.Split(',')[2]].ToString().ToLower(), values[parameterName.Split(',')[3]].ToString().ToLower());
                }
                else
                {
                    return GetCategoryIdByName(values[parameterName.Split(',')[0]].ToString().ToLower(), values[parameterName.Split(',')[1]].ToString().ToLower(), "canada", values[parameterName.Split(',')[2]].ToString().ToLower());

                }
            }
            else
                return GetCategoryIdByName(values[parameterName.Split(',')[0]].ToString().ToLower(), values[parameterName.Split(',')[1]].ToString().ToLower(),"canada");
        }

        private bool GetCategoryIdByName(string Category, string State, string Country, string Category1=null, string Category2=null)
        {
            var url = string.Format("api/StateSearch/StateSearchURLValidate?category=" + Category + "&state=" + State + "&country="+Country + "&category1="+ Category1 + "&category2="+Category2);
            DataTable dt = wHelper.GetDataTableFromWebApi(url);
            string redirect = dt.Rows[0][0].ToString();
            HttpContext.Current.Response.StatusCode = 301;
            HttpContext.Current.Response.RedirectPermanent(Utils.WebURL + redirect);
            HttpContext.Current.Response.End();
            return false;
            
                
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
            HttpContext.Current.Response.RedirectPermanent(Utils.WebURL);
            HttpContext.Current.Response.End();
            return false;
        }
    }
}