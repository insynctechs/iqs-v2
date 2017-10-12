using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IQSDirectory.Helpers;
using System.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Text;

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
                SetRegionalValues(ds.Tables[5]);
                GenerateHeader(ds.Tables[0]);
                
                GenerateMetaAndScripts(ds.Tables[7], ds.Tables[8], ds.Tables[6], ds.Tables[10]);
                GenerateRelatedCategories(ds.Tables[1]);
                GenerateAdvertisements(ds.Tables[2]);
                GenerateOtherAdvertisements(ds.Tables[4]);
            }
            ShareURL = HttpContext.Current.Request.Url.AbsoluteUri;
            DirectoryURL = HttpContext.Current.Request.Url.Authority;
        }

        private void SetRegionalValues(DataTable dt)
        {
            StateName = dt.Rows[0]["STATE_NAME"].ToString();
            StateCode = dt.Rows[0]["STATE_CODE"].ToString();
            StateSK = dt.Rows[0]["STATE_SK"].ToString();
            CountryCode = dt.Rows[0]["COUNTRY_SHORT"].ToString();
            CountryName = dt.Rows[0]["COUNTRY_LONG"].ToString();
        }

        



        private void GenerateHeader(DataTable dt)
        {
            ApiPath = wHelper.ApiUrl;
            CategorySK = dt.Rows[0]["CATEGORY_SK"].ToString();
            CategoryName = dt.Rows[0]["NAME"].ToString();
            H1Text = dt.Rows[0]["FACET_DISPLAY_NAME"].ToString();
            DisplayName = dt.Rows[0]["DISPLAY_NAME"].ToString();
            string description = dt.Rows[0]["DESCRIPTION"].ToString();
            description = description.Replace("[keyword]", H1Text);
            if (description.IndexOf("[city],") > 0)
                description = description.Replace("[city],", "");
            
            if ((description.IndexOf("[state], [country]") > 0 || description.IndexOf("[state],[country]") > 0) )
            {
                description = description.Replace("[state], [country]", "[state]");
                description = description.Replace("[state],[country]", "[state]");
            }
            else
                description = description.Replace("[country]", CountryName);
            description = description.Replace("[state]", StateName);
            ItemDesc = new HtmlString(description);
        }

        private void GenerateMetaAndScripts(DataTable dtMeta, DataTable dtScripts, DataTable dtAnalytics, DataTable dtIndexes)
        {
            
            DataRow[] dr = dtMeta.Select("META_TAG_ID='TITLE'");
            if (dr.Length > 0)
            {
                this.Master.PageTitle = dr[0]["DESCRIPTION"].ToString().Replace("[state]", StateName).Replace("[keyword]", H1Text);
                
            }
            dr = dtMeta.Select("META_TAG_ID='DESCRIPTION'");
            if (dr.Length > 0)
            {
                this.Master.PageDescription = dr[0]["DESCRIPTION"].ToString().Replace("[state]", StateName).Replace("[keyword]", H1Text);
                
            }
            dr = dtMeta.Select("META_TAG_ID='KEYWORD'");
            if (dr.Length > 0)
                this.Master.PageKeywords = dr[0]["DESCRIPTION"].ToString().Replace("[state]", StateName).Replace("[keyword]",H1Text);
            dr = dtMeta.Select("META_TAG_ID='TRACKING SCRIPT'");
            if (dr.Length > 0)
                this.Master.HitsLinkScript = new HtmlString(dr[0]["DESCRIPTION"].ToString());
            
            DataRow[] drIndex = dtIndexes.Select("state_sk=" + StateSK);
            bool isIndexed = false;            
            if (drIndex.Length > 0)
            {
                isIndexed = true;
                this.Master.PageIndex =  new HtmlString("<meta name='robots' content='index,follow'>");
            }          

            if (isIndexed == false)
            {
                dr = dtMeta.Select("META_TAG_ID='VERIF_CODE'");
                if (dr.Length > 0)
                    this.Master.PageIndex =  new HtmlString(dr[0]["DESCRIPTION"].ToString());
                else
                    this.Master.PageIndex =  new HtmlString("<meta name='robots' content='noindex,follow'>");
            }

            this.Master.BindMeta();
            foreach (DataRow dr1 in dtScripts.Rows)
            {
                if (dr1["HEAD_SCRIPT"].ToString() != "")
                {
                    this.Master.HeadScript =  new HtmlString( dr1["HEAD_SCRIPT"].ToString() );
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
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public string StateSK { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
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