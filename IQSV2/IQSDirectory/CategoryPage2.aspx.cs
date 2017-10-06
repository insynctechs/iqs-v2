using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using IQSDirectory.Helpers;
using System.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;


namespace IQSDirectory
{
    public partial class CategoryPage2 : System.Web.UI.Page
    {
        WebApiHelper wHelper = new WebApiHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckCategory();
            }
        }

        private void CheckCategory()
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            if (url.IndexOf("/", url.Length - 1) > -1)
            {
                url = url.Remove(url.Length - 1);
                Response.Redirect(url);
            }
            CategoryName = url.Split('/').Reverse().Skip(1).Take(1).First();
            ShareURL = HttpContext.Current.Request.Url.AbsoluteUri;
            DirectoryURL = HttpContext.Current.Request.Url.Authority;
            var urlGetId = string.Format("api/CategoryPages/GetCategoryIdByName?DisplayName=" + CategoryName);
            DataTable dt = wHelper.GetDataTableFromWebApi(urlGetId);
            DisplayCategory(dt.Rows[0]["Category_SK"].ToString());
        }

        private void DisplayCategory(string CategoryID)
        {
            var url = string.Format("api/CategoryPages/GetCategoryPage2Details?CategorySK=" + CategoryID + "&WebsiteType=Directory");
            DataSet ds = wHelper.GetDataSetFromWebApi(url);
            if (ds != null)
            {
                GenerateHeader(ds.Tables[0]);
                GenerateProfile(ds.Tables[2]);
                GenerateAdvertisements(ds.Tables[3]);
                //GenerateIndustryInformation(ds.Tables[6]);
                //GenerateMetaTags(ds.Tables[5]);
                //DisplayArticles();
            }
        }

        private void GenerateHeader(DataTable dt)
        {
            ApiPath = wHelper.ApiUrl;
            //RootPath = HttpContext.Current.Request.ApplicationPath.ToString();
            CategorySK = dt.Rows[0]["CATEGORY_SK"].ToString();
            H1Text = dt.Rows[0]["H1DISPLAY_NAME"].ToString();
            DisplayName = dt.Rows[0]["DISPLAY_NAME"].ToString();
            ItemDesc = new HtmlString(dt.Rows[0]["DESCRIPTION"].ToString());
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
            dt.AsEnumerable().ToList().ForEach(r => {
                r["FORMATED_NAME"] = Utils.FormatCompanyWebsiteLink(r["CLIENT_NAME"].ToString());
                r["FORMATED_URL"] = Utils.ReplaceContent(r["COMPANY_URL"].ToString(), 0);
                r["PROFILE_URL"] = Utils.ReplaceContent(ProfileLinks.Where(
                    p => p["CLIENT_SK"].ToString() == r["CLIENT_SK"].ToString()
                    && p["ENTITY_ATTRIBUTE_ID"].ToString() == "E-MAIL")
                    .Select(p => p["DESCRIPTION"].ToString()).FirstOrDefault(), 0);
            });

            TierAdvertisements = dt.AsEnumerable().ToList();
        }

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