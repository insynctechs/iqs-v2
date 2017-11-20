﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IQSDirectory.Helpers;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Text;

namespace IQSDirectory
{
    public partial class DirectorySearch : System.Web.UI.Page
    {
        WebApiHelper wHelper = new WebApiHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            ApiPath = wHelper.ApiUrl;
            if (ValidatePage() == false)
            {
                Response.Redirect(RootPath);
            }

            CheckStateSearch();

            if (!IsPostBack)
            {
                int start = 1;
                CategorySK = "0";
                if (CurPage != null)
                {
                    if (int.TryParse(CurPage, out start))
                        StartPage = Convert.ToInt32(CurPage);
                    else
                        StartPage = 1;
                }
                try
                {
                    CurQuery = Uri.UnescapeDataString(CurQuery);
                }
                catch
                {
                }
                ProductList = new List<DataRow>();
                OtherList = new List<DataRow>();
                CompanyList = new List<DataRow>();
                DisplayResults();
            }
        }

        private bool ValidatePage()
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            if (url.IndexOf("/", url.Length - 1) > -1)
            {
                url = url.Remove(url.Length - 1);
                Response.Redirect(url);
            }
            if (url.IndexOf("search/") != -1)
            {
                string srh = url.Substring(url.IndexOf("search/")).Replace("search/", "");
                object[] queryVals = srh.Split('/');
                if (queryVals.Length >= 2)
                {
                    CurQuery = queryVals[0].ToString();
                    CurPage = queryVals[1].ToString();
                    CurState = "";
                    RootPath = "../../";
                    Response.Write(CurQuery);
                    if (queryVals.Length > 2)
                    {
                        CurState = queryVals[2].ToString();
                        RootPath = "../../../";
                    }
                    return true;
                }
                else if (queryVals.Length == 1)
                {
                    CurQuery = queryVals[0].ToString();
                    CurPage = "1";
                    CurState = "";
                    RootPath = "../";
                    return true;
                }
                else
                {
                    RootPath = "../";
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        private void DisplayResults()
        {
            Int16 RecCount = 10;
            TextInfo tInfo = new CultureInfo("en-US", false).TextInfo;
            this.Title = "IQS Directory - " + tInfo.ToTitleCase(CurQuery).ToString() + " Results";
             string citycode = "";
            try
            {
                citycode = getIPState("https://freegeoip.net/json/");
            }
            catch (Exception)
            {
            }
            var url = string.Format("api/StateSearch/GetSearchResultsDetails?SrhStr=" + CurQuery + "&Start=" + StartPage + "&Count=" + RecCount + "&State=" + CurState);
            DataTable dt = wHelper.GetDataTableFromWebApi(url);
            if (dt.Rows.Count > 0)
            {
                DataRow[] drCatChek = (DataRow[])dt.Select("DISPLAY_NAME='" + CurQuery.Replace("'", "''") + "'");
                if (drCatChek.Length == 1)
                {
                    url = RootPath + dt.Rows[0]["URL"].ToString().Replace("\\", "/");
                    Response.Redirect(url);
                }

                drCatChek = (DataRow[])dt.Select("CTYPE='CATEGORY'");
                if (drCatChek.Length == 1)
                {
                    url = RootPath + dt.Rows[0]["URL"].ToString().Replace("\\", "/");
                    Response.Redirect(url);
                }

                if (dt.Rows.Count == 1)
                {
                    if (dt.Rows[0]["CTYPE"].ToString() == "COMPANY")
                    {
                        url = RootPath + dt.Rows[0]["URL"].ToString().Replace("\\", "/");
                        //Response.Write(url);
                        Response.Redirect(url);
                    }
                }

                dt.Columns.Add("FORMATED_TITLE");
                dt.AsEnumerable().ToList().ForEach(dr =>
                {
                    dr["URL"] = wHelper.WebUrl + dr["URL"].ToString().Replace("\\", "/");
                    dr["MDESC"] = dr["MDESC"].ToString().IndexOf(". ") > 0 ? dr["MDESC"].ToString().Substring(0, dr["MDESC"].ToString().IndexOf(". ") + 1).ToString() : dr["MDESC"].ToString();
                    dr["FORMATED_TITLE"] = Utils.FormatCompanyWebsiteLink(dr["TITLE"].ToString());
                });

                ProductList = dt.Select("NORDER = 1").AsEnumerable().ToList();

                DataRow[] drComp = dt.Select("NORDER = 2");
                foreach (var cList in drComp)
                {
                    string[] csite = cList["WEBSITE"].ToString().Split(',');
                    if (csite.Length > 0)
                    {
                        if (csite[0].Trim().StartsWith("http://"))
                            cList["WEBSITE"] = csite[0].Trim();
                        else
                            cList["WEBSITE"] = "http://" + csite[0].Trim();
                    }
                    if (cList["MDESC"].ToString().Length > 300)
                    {
                        cList["MDESC"] = cList["MDESC"].ToString().Substring(0, 300);
                    }
                    cList["MDESC"] = cList["MDESC"].ToString().Substring(0, cList["MDESC"].ToString().LastIndexOf(' '));
                }
                CompanyList = drComp.AsEnumerable().ToList();

                OtherList = dt.Select("NORDER <> 1 AND NORDER <> 2").AsEnumerable().ToList();
                TotalCount = Convert.ToInt32(dt.Rows[0]["TCOUNT"].ToString());

                PageCount = Math.Ceiling(Convert.ToDouble(TotalCount) / RecCount);
               
            }
            else
            {
                TotalCount = 0;
                PageCount = 0;
            }

            PgSrhUrl = RootPath + "search/" + CurQuery + "/";
            PageTitle = "Search Found <strong> " + TotalCount.ToString() + " </strong> Result(s) matching the word - <span> " + tInfo.ToTitleCase(CurQuery).ToString() + " </span>";

            if (StartPage != 1)
            {
                PgPreURl = PgSrhUrl + (StartPage - 1).ToString();
            }

            if (StartPage != PageCount)
            {
                PgNxtURl = PgSrhUrl + (StartPage + 1).ToString();
            }
        }

        private void CheckStateSearch()
        {
            if (CurState == "")
                return;
            var url = string.Format("api/StateSearch/GetStateSearchResults?Category=" + CurQuery + "&State=" + CurState);
            DataTable dt = wHelper.GetDataTableFromWebApi(url);
            if (dt != null)
            {
                if ((dt.Rows[0][0].ToString() == "") || (dt.Rows[0][1].ToString() == ""))
                {
                    return;
                }
                else
                {
                    string navUrl = RootPath + dt.Rows[0][0].ToString() + "/" + dt.Rows[0][1].ToString().Replace(" ", "-");
                    Response.Redirect(navUrl);
                }
            }
            else
            {
                return;
            }
        }

        private string getIPState(string address)
        {
            WebRequest request = (HttpWebRequest)WebRequest.Create(address);
            request.Timeout = 500;
            string s = "";
            try
            {
                using (WebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response != null)
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                            string json = reader.ReadToEnd().ToString();
                            JavaScriptSerializer jss = new JavaScriptSerializer();
                            Dictionary<string, string> obj = jss.Deserialize<Dictionary<string, string>>(json);
                            s = obj["region_code"].ToString().Trim(); //"Carnegie, IN";
                            return s;
                        }
                    }
                    else
                    {
                        return s;
                    }
                }
            }
            catch (WebException ex)
            {
                s = "";
                return s;
                
            }


        }

        public string RootPath { get; set; }
        public string CurQuery { get; set; }
        public string CurPage { get; set; }
        public string CurState { get; set; }
        public int StartPage { get; set; }
        public string PageTitle { get; set; }
        public string PgPreURl { get; set; }
        public string PgNxtURl { get; set; }
        public string PgSrhUrl { get; set; }
        public double PageCount { get; set; }
        public int TotalCount { get; set; }
        public List<DataRow> ProductList { get; set; }
        public List<DataRow> CompanyList { get; set; }
        public List<DataRow> OtherList { get; set; }
        public string CategorySK { get; set; }
        public string ApiPath { get; set; }
    }
}