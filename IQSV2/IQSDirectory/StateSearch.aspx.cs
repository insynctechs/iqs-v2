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
    public partial class StateSearch : System.Web.UI.Page
    {
        WebApiHelper wHelper = new WebApiHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            WebApiHelper wHelper = new WebApiHelper();
            if (!IsPostBack)
            {
                DisplayResults();
            }
        }

        private void DisplayResults()
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            if (url.IndexOf("/", url.Length - 1) > -1)
            {
                url = url.Remove(url.Length - 1);
                Response.Redirect(url);
            }
            string category = "", state = "";
            string[] cval = url.Split('/');
            if(url.Contains("canada"))
            {
                category = cval[cval.Length - 3];
                state = cval[cval.Length - 1];
                RootPath = "../../";
            }
            else
            {
                category = cval[cval.Length - 2];
                state = cval[cval.Length - 1];
                RootPath = "../";
            }
            var urlGet = string.Format("api/StateSearch/GetStateSearchPageDetails?Category=" + category + "&State=" + state);
            DataSet ds = wHelper.GetDataSetFromWebApi(urlGet);
            if(ds != null)
            {
                GenerateHeader(ds.Tables[0]);
                GenerateRelatedCategories(ds.Tables[1]);
                GenerateAdvertisements(ds.Tables[2]);
                GenerateOtherAdvertisements(ds.Tables[4]);
            }
            ShareURL = HttpContext.Current.Request.Url.AbsoluteUri;
            DirectoryURL = HttpContext.Current.Request.Url.Authority;
        }

        private void GenerateHeader(DataTable dt)
        {
            ApiPath = wHelper.ApiUrl;
            CategorySK = dt.Rows[0]["CATEGORY_SK"].ToString();
            CategoryName = dt.Rows[0]["NAME"].ToString();
            H1Text = dt.Rows[0]["FACET_DISPLAY_NAME"].ToString();
            DisplayName = dt.Rows[0]["DISPLAY_NAME"].ToString();
            ItemDesc = new HtmlString(dt.Rows[0]["DESCRIPTION"].ToString());
        }

        private void GenerateRelatedCategories(DataTable dt)
        {
            RelatedCategories = dt.AsEnumerable().ToList();
        }

        private void GenerateAdvertisements(DataTable dt)
        {
            string stateCode = dt.Rows[0]["CURSTATECODE"].ToString();
            CurrentState = dt.Rows[0]["STATECODE"].ToString();
            dt.Columns.Add("FORMATED_NAME");
            dt.Columns.Add("FORMATED_URL");
            dt.Columns.Add("PROFILE_URL");
            dt.AsEnumerable().ToList().ForEach(r => {
                r["FORMATED_NAME"] = Utils.FormatCompanyWebsiteLink(r["CLIENT_NAME"].ToString());
                r["FORMATED_URL"] = Utils.ReplaceContent(r["COMPANY_URL"].ToString(), 0);
                r["PROFILE_URL"] = Utils.ReplaceContent(r["COPRA_PATH"].ToString(), 0);
                r["ADDESCRIPTION"] = r["ADDESCRIPTION"].ToString().Replace("[keyword]", H1Text)
                    .Replace("[state], [country]", "[state]")
                    .Replace("[state],[country]", "[state]")
                    .Replace("[city],", "")
                    .Replace("[state]", CurrentState);
            });

            StateAdvertisements = dt.Select("theSTATE = '" + stateCode + "'").AsEnumerable().ToList();
            NeighAdvertisements = dt.Select("theSTATE <> '" + stateCode + "'", "theState").AsEnumerable().ToList();
        }

        private void GenerateOtherAdvertisements(DataTable dt)
        {
            OtherAdvertisements = dt.AsEnumerable().ToList();
        }

        public string RootPath { get; set; }
        public string CategoryName { get; set; }
        public string CategorySK { get; set; }
        public string H1Text { get; set; }
        public string DisplayName { get; set; }
        public string CategoryTitle { get; set; }
        public string MetaDesc { get; set; }
        public string ShareURL { get; set; }
        public string CurrentState { get; set; }
        public string DirectoryURL { get; set; }
        public IHtmlString ItemDesc { get; set; }
        public List<DataRow> RelatedCategories { get; set; }
        public List<DataRow> StateAdvertisements { get; set; }
        public List<DataRow> NeighAdvertisements { get; set; }
        public List<DataRow> OtherAdvertisements { get; set; }
        public string ApiPath { get; set; }
    }
}