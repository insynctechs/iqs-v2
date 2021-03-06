﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using IQSDirectory.Helpers;
using System.Data;



namespace IQSDirectory
{
    public partial class CategoryPage2 : System.Web.UI.Page
    {
        WebApiHelper wHelper = new WebApiHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeVars();
                CheckCategory();
            }
        }

        private void InitializeVars()
        {
            TierAdvertisements = new List<DataRow>();
            ProfileLinks = new List<DataRow>();
            ClientRatings = new List<DataRow>();
            Articles = new List<DataRow>();
        }


        private void CheckCategory()
        {
            try
            {
                string url = HttpContext.Current.Request.Url.AbsolutePath;
                if (url.IndexOf("/", url.Length - 1) > -1)
                {
                    RootPath = "../../";
                    url = url.Remove(url.Length - 1);
                }
                else
                {
                    url = url + '/';
                    Response.StatusCode = 301;
                    Response.Redirect(url);
                    Response.End();
                    RootPath = "../";
                }

                CategoryName = url.Split('/').Reverse().Skip(1).Take(1).First();
                ShareURL = HttpContext.Current.Request.Url.AbsoluteUri;
                DirectoryURL = HttpContext.Current.Request.Url.Authority;
                var urlGetId = string.Format("api/CategoryPages/GetCategoryIdByName?DisplayName=" + CategoryName);
                DataTable dt = wHelper.GetDataTableFromWebApi(urlGetId);
                DisplayCategory(dt.Rows[0]["Category_SK"].ToString());
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }
        }

        private void DisplayCategory(string CategoryID)
        {
            try
            {
                var url = string.Format("api/CategoryPages/GetCategoryPage2Details?CategorySK=" + CategoryID + "&WebsiteType=Directory&WebURL=" + Utils.WebURL);
                DataSet ds = wHelper.GetDataSetFromWebApi(url);
                if (ds != null)
                {
                    //Response.Write(ds.Tables[0].Rows.Count + "<br/>" + ds.Tables[1].Rows.Count+"<br/>"+ ds.Tables[2].Rows.Count + "<br/>" + ds.Tables[3].Rows.Count + "<br/>" + ds.Tables[4].Rows.Count);
                    GenerateHeader(ds.Tables[0]);
                    GenerateProfile(ds.Tables[1]);
                    GenerateAdvertisements(ds.Tables[2]);
                    GenerateMetaTagsAndScripts(ds.Tables[3], ds.Tables[4]);


                }
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }
        }

        private void GenerateHeader(DataTable dt)
        {
            ApiPath = wHelper.ApiUrl;
            //RootPath = HttpContext.Current.Request.ApplicationPath.ToString();
            try
            {
                CategorySK = dt.Rows[0]["CATEGORY_SK"].ToString();
                H1Text = dt.Rows[0]["H1DISPLAY_NAME"].ToString();
                DisplayName = dt.Rows[0]["DISPLAY_NAME"].ToString();
                ItemDesc = new HtmlString(dt.Rows[0]["DESCRIPTION"].ToString());
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }
        }

        private void GenerateProfile(DataTable dt)
        {
            ProfileLinks = dt.AsEnumerable().ToList();
        }

        private void GenerateAdvertisements(DataTable dt)
        {
            dt.Columns.Add("FORMATED_NAME");
            dt.Columns.Add("FORMATED_URL");
            dt.Columns.Add("PROFILE_URL");
            dt.Columns.Add("NUM_PHONE");
            try
            {
                dt.AsEnumerable().ToList().ForEach(r =>
                {
                    r["FORMATED_NAME"] = Utils.FormatCompanyWebsiteLink(r["CLIENT_NAME"].ToString());
                    r["FORMATED_URL"] = Utils.ReplaceContent(r["COMPANY_URL"].ToString(), 0);
                    string[] phoneList = r["PHONE"].ToString().Split(',').Where(x => x != null && x.Trim().Length > 0).Select(x => x.Trim()).ToArray();
                    foreach (string phone in phoneList)
                    {
                        r["NUM_PHONE"] += "<a itemprop='telephone' href='tel:+1-" + phone + "' >" + phone + "</a>";
                    }
                    r["PROFILE_URL"] = Utils.ReplaceContent(ProfileLinks.Where(
                        p => p["CLIENT_SK"].ToString() == r["CLIENT_SK"].ToString()
                        && p["ENTITY_ATTRIBUTE_ID"].ToString() == "E-MAIL")
                        .Select(p => p["DESCRIPTION"].ToString()).FirstOrDefault(), 0);
                });

                TierAdvertisements = dt.AsEnumerable().ToList();
                GetClientSkForRating();
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }
        }

        private void GetClientSkForRating()
        {
            string ClientSKForRating = "";
            try
            {
                if (TierAdvertisements.Count > 0)
                {
                    ClientSKForRating += string.Join(",", TierAdvertisements.Select(ad => ad["CLIENT_SK"].ToString()));
                }
                if (ClientSKForRating != "")
                {
                    var url = string.Format("api/Reviews/GetCompanyRatingByArray?ClientSkArray=" + ClientSKForRating);
                    DataTable dt = wHelper.GetDataTableFromWebApi(url);
                    if (dt.Rows.Count > 0)
                    {
                        ClientRatings = dt.Select("SHOW_REVIEWS='Y'").ToList();
                    }
                    else
                    {
                        ClientRatings = dt.AsEnumerable().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }
        }

        private void GenerateMetaTagsAndScripts(DataTable dtMeta, DataTable dtScripts)
        {

            DataRow[] dr;

            this.Master.PageIndex = new HtmlString("<meta name='robots' content='" + Utils.MetaRobots + "'>");
            try
            {
                if (dtMeta.Rows.Count > 0)
                {
                    dr = dtMeta.Select("META_TAG_ID = 'TITLE'");

                    if (dr.Length > 0)
                    {
                        CategoryTitle = dr[0]["DESCRIPTION"].ToString();
                        CategoryTitle = CategoryTitle.Replace("–", "-");
                        CategoryTitle = CategoryTitle.Replace("&", "&amp;");
                        this.Master.PageTitle = CategoryTitle;
                    }
                    else
                        CategoryTitle = "IQS Product Categories";

                    dr = dtMeta.Select("META_TAG_ID = 'DESCRIPTION'");

                    if (dr.Length > 0)
                    {
                        MetaDesc = dr[0]["DESCRIPTION"].ToString();
                        this.Master.PageDescription = MetaDesc;


                    }

                    dr = dtMeta.Select("META_TAG_ID='KEYWORD'");
                    if (dr.Length > 0)
                        this.Master.PageKeywords = dr[0]["DESCRIPTION"].ToString();


                    dr = dtMeta.Select("META_TAG_ID='TRACKING SCRIPT'");
                    if (dr.Length > 0)
                        this.Master.HitsLinkScript = new HtmlString(dr[0]["DESCRIPTION"].ToString());
                    dr = dtMeta.Select("META_TAG_ID='VERIF_CODE'");
                    if (dr.Length > 0)
                        this.Master.PageIndex = new HtmlString(dr[0]["DESCRIPTION"].ToString());
                }

                

                if (dtScripts.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dtScripts.Rows)
                    {
                        if (dr1["HEAD_SCRIPT"].ToString() != "")
                        {
                            this.Master.HeadScript = new HtmlString(dr1["HEAD_SCRIPT"].ToString());
                        }
                        if (dr1["BODY_START_SCRIPT"].ToString() != "")
                        {
                            this.Master.BodyOpenScript = new HtmlString(dr1["BODY_START_SCRIPT"].ToString());
                        }
                        if (dr1["BODY_BFR_CLOSE_SCRIPT"].ToString() != "")
                        {
                            this.Master.BodyCloseScript = new HtmlString(dr1["BODY_BFR_CLOSE_SCRIPT"].ToString());
                        }
                        if (dr1["BODY_AFT_CLOSE_SCRIPT"].ToString() != "")
                        {
                            this.Master.BodyAfterCloseScript = new HtmlString(dr1["BODY_AFT_CLOSE_SCRIPT"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }
            finally
            {
                this.Master.BindMeta();
            }
        }

        public string RootPath { get; set; }
        public string CategoryName { get; set; }
        public string CategorySK { get; set; }
        public string CategoryTitle { get; set; }
        public string MetaDesc { get; set; }
        public string H1Text { get; set; }
        public string DisplayName { get; set; }
        public string ShareURL { get; set; }
        public string DirectoryURL { get; set; }
        public IHtmlString ItemDesc { get; set; }
        public List<DataRow> TierAdvertisements { get; set; }
        public List<DataRow> ProfileLinks { get; set; }
        public List<DataRow> Articles { get; set; }
        public IHtmlString IndustryInformation { get; set; }
        public List<DataRow> ClientRatings { get; set; }
        public string ApiPath { get; set; }
    }
}