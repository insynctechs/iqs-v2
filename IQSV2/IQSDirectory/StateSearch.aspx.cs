using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using IQSDirectory.Helpers;
using System.Data;

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
                InitializeVars();
                DisplayResults();
            }
        }

        private void InitializeVars()
        {
            StateAdvertisements = new List<DataRow>();
            RelatedCategories = new List<DataRow>();
            NeighAdvertisements = new List<DataRow>();
            ClientRatings = new List<DataRow>();
            OtherAdvertisements = new List<DataRow>();

        }
        private void DisplayResults()
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            string category = "", state = "";
            try
            {
                if (url.IndexOf("/", url.Length - 1) > -1)
                {
                    url = url.Remove(url.Length - 1);

                }
                else
                {
                    url = url + '/';
                    Response.StatusCode = 301;
                    Response.Redirect(url);
                    Response.End();

                }
                string[] cval = url.Split('/');
                if (url.Contains("canada"))
                {
                    category = cval[cval.Length - 3];
                    state = cval[cval.Length - 1];
                    RootPath = "../../../";
                }
                else
                {
                    category = cval[cval.Length - 2];
                    state = cval[cval.Length - 1];
                    RootPath = "../../";
                }
                var urlGet = string.Format("api/StateSearch/GetStateSearchPageDetails?Category=" + category + "&State=" + state);
                DataSet ds = wHelper.GetDataSetFromWebApi(urlGet);
                if (ds != null)
                {
                    SetRegionalValues(ds.Tables[5]);
                    //Response.Write("<!-- URL=" + url + " --- Countrycode=" + CountryCode);
                    if (url.Contains("canada") && CountryCode.Trim() == "USA") {
                        url = url.Replace("canada/", "");
                        //Response.Write("<!-- URL=" + url );
                        Response.StatusCode = 301;
                        Response.Redirect(url);
                        Response.End();

                    }
                    else if (!url.Contains("canada") && CountryCode.Trim() == "CAN")
                    {
                        url = RootPath + category + "/canada/" + state + "/";
                        //Response.Write("<!-- URL=" + url);
                        Response.StatusCode = 301;
                        Response.Redirect(url);
                        Response.End();

                    }
                    GenerateHeader(ds.Tables[0]);
                    GenerateMetaAndScripts(ds.Tables[7], ds.Tables[8], ds.Tables[6], ds.Tables[10]);
                    GenerateRelatedCategories(ds.Tables[1]);
                    GenerateAdvertisements(ds.Tables[2]);
                    GenerateOtherAdvertisements(ds.Tables[4]);
                    GenerateCityList(ds.Tables[11]);
                    GenerateCountyList(ds.Tables[12]);
                }

            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }
            ShareURL = HttpContext.Current.Request.Url.AbsoluteUri;
            DirectoryURL = HttpContext.Current.Request.Url.Authority;
        }

        private void SetRegionalValues(DataTable dt)
        {
            try
            {
                StateName = dt.Rows[0]["STATE_NAME"].ToString();
                StateCode = dt.Rows[0]["STATE_CODE"].ToString();
                StateSK = dt.Rows[0]["STATE_SK"].ToString();
                CountryCode = dt.Rows[0]["COUNTRY_SHORT"].ToString();
                CountryName = dt.Rows[0]["COUNTRY_LONG"].ToString();
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }
        }

        



        private void GenerateHeader(DataTable dt)
        {
            ApiPath = wHelper.ApiUrl;
            try
            {
                CategorySK = dt.Rows[0]["CATEGORY_SK"].ToString();
                CategoryName = dt.Rows[0]["NAME"].ToString();
                H1Text = dt.Rows[0]["FACET_DISPLAY_NAME"].ToString();
                DisplayName = dt.Rows[0]["DISPLAY_NAME"].ToString();
                string description = dt.Rows[0]["DESCRIPTION"].ToString();
                description = description.Replace("[keyword]", H1Text);
                if (description.IndexOf("[city],") > 0)
                    description = description.Replace("[city],", "");

                if ((description.IndexOf("[state], [country]") > 0 || description.IndexOf("[state],[country]") > 0))
                {
                    description = description.Replace("[state], [country]", "[state]");
                    description = description.Replace("[state],[country]", "[state]");
                }
                else
                    description = description.Replace("[country]", CountryName);
                description = description.Replace("[state]", StateName);
                ItemDesc = new HtmlString(description);
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }
        }

        private void GenerateMetaAndScripts(DataTable dtMeta, DataTable dtScripts, DataTable dtAnalytics, DataTable dtIndexes)
        {
            bool isIndexed = false;
            DataRow[] dr;

            this.Master.PageIndex = new HtmlString("<meta name='robots' content='" + Utils.MetaRobots + "'>");
            try
            {
                if (dtMeta.Rows.Count > 0)
                {
                    dr = dtMeta.Select("META_TAG_ID='TITLE'");
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
                        this.Master.PageKeywords = dr[0]["DESCRIPTION"].ToString().Replace("[state]", StateName).Replace("[keyword]", H1Text);
                    dr = dtMeta.Select("META_TAG_ID='TRACKING SCRIPT'");
                    if (dr.Length > 0)
                        this.Master.HitsLinkScript = new HtmlString(dr[0]["DESCRIPTION"].ToString());


                    if (dtIndexes.Rows.Count > 0)
                    {
                        DataRow[] drIndex = dtIndexes.Select("state_sk=" + StateSK);

                        if (drIndex.Length > 0)
                        {
                            isIndexed = true;
                            this.Master.PageIndex = new HtmlString("<meta name='robots' content='" + Utils.MetaRobots + "'>");
                        }
                    }
                    if (isIndexed == false)
                    {
                        dr = dtMeta.Select("META_TAG_ID='VERIF_CODE'");
                        if (dr.Length > 0)
                            this.Master.PageIndex = new HtmlString(dr[0]["DESCRIPTION"].ToString());
                        else
                            this.Master.PageIndex = new HtmlString("<meta name='robots' content='" + Utils.MetaRobots + "'>");
                    }
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
        private void GenerateRelatedCategories(DataTable dt)
        {
            RelatedCategories = dt.AsEnumerable().ToList();
        }

        private void GenerateAdvertisements(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    string stateCode = dt.Rows[0]["CURSTATECODE"].ToString();
                    CurrentState = dt.Rows[0]["STATECODE"].ToString();
                    dt.Columns.Add("FORMATED_NAME");
                    dt.Columns.Add("FORMATED_URL");
                    dt.Columns.Add("PROFILE_URL");
                    dt.Columns.Add("NUM_PHONE");
                    dt.AsEnumerable().ToList().ForEach(r =>
                    {
                        r["FORMATED_NAME"] = Utils.FormatCompanyWebsiteLink(r["CLIENT_NAME"].ToString());
                        r["FORMATED_URL"] = Utils.ReplaceContent(r["COMPANY_URL"].ToString(), 0);
                        r["PROFILE_URL"] = Utils.ReplaceContent(r["COPRA_PATH"].ToString(), 0);
                        string[] phoneList = r["PHONE"].ToString().Split(',').Where(x => x != null && x.Trim().Length > 0).Select(x => x.Trim()).ToArray();
                        foreach (string phone in phoneList)
                        {
                            r["NUM_PHONE"] += "<a itemprop='telephone' href='tel:+1-" + phone + "' >" + phone + "</a>";
                        }
                        r["ADDESCRIPTION"] = r["ADDESCRIPTION"].ToString().Replace("[keyword]", H1Text)
                            .Replace("[state], [country]", "[state]")
                            .Replace("[state],[country]", "[state]")
                            .Replace("[city],", "")
                            .Replace("[state]", r["theSTATENAME"].ToString());
                    });
                    if (dt.Rows.Count > 0)
                    {
                        StateAdvertisements = dt.Select("theSTATE = '" + stateCode + "'").AsEnumerable().ToList();
                        NeighAdvertisements = dt.Select("theSTATE <> '" + stateCode + "'", "theState").AsEnumerable().ToList();
                        GetClientSkForRating();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }

        }

        private void GenerateOtherAdvertisements(DataTable dt)
        {
            OtherAdvertisements = dt.AsEnumerable().ToList();
        }

        private void GenerateCityList(DataTable dt)
        {
            try
            {
                CityList = string.Join(", ", dt.AsEnumerable()
                                     .Select(x => x["CITY"].ToString())
                                     .ToArray());
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }
        }

        private void GenerateCountyList(DataTable dt)
        {
            try
            {
                CountyList = string.Join(", ", dt.AsEnumerable()
                                     .Select(x => x["COUNTY"].ToString())
                                     .ToArray());
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
                if (StateAdvertisements.Count > 0)
                {
                    ClientSKForRating += string.Join(",", StateAdvertisements.Select(ad => ad["CLIENT_SK"].ToString()));
                }
                if (NeighAdvertisements.Count > 0)
                {
                    if (ClientSKForRating != "")
                        ClientSKForRating += ",";
                    ClientSKForRating += string.Join(",", NeighAdvertisements.Select(ad => ad["CLIENT_SK"].ToString()));
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
        public string CityList { get; set; }
        public string CountyList { get; set; }
        public IHtmlString ItemDesc { get; set; }
        public List<DataRow> RelatedCategories { get; set; }
        public List<DataRow> StateAdvertisements { get; set; }
        public List<DataRow> NeighAdvertisements { get; set; }
        public List<DataRow> OtherAdvertisements { get; set; }
        public List<DataRow> ClientRatings { get; set; }
        public string ApiPath { get; set; }
    }
}