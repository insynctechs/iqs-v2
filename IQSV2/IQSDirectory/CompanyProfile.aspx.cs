using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using IQSDirectory.Helpers;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.Web.Script.Serialization;


namespace IQSDirectory
{
    public partial class CompanyProfile : System.Web.UI.Page
    {
        WebApiHelper wHelper = new WebApiHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            WebApiHelper wHelper = new WebApiHelper();
            if (!IsPostBack)
            {
                DisplayData();
            }
        }

        private void DisplayData()
        {
            RootPath = "../../";
            ApiPath = wHelper.ApiUrl;
            CategorySK = "0";
            ShareURL = HttpContext.Current.Request.Url.AbsoluteUri;
            DirectoryURL = HttpContext.Current.Request.Url.Authority;

            string url = HttpContext.Current.Request.Url.AbsolutePath;

            try
            {
                if (url.IndexOf("/", url.Length - 1) > -1)
                {
                    url = url.Remove(url.Length - 1);
                }
                string copro = url.Split('/').Last();
                string Client_SK = copro.Substring(copro.LastIndexOf('-')).Trim('-');
                var urlClientInfo = string.Format("api/Clients/GetClientProfileDetails?Client_SK=" + Client_SK);
                DataSet ds = wHelper.GetDataSetFromWebApi(urlClientInfo);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        AddCompanyDetails(ds.Tables[0].Rows[0], ds.Tables[1], ds.Tables[2]);
                        AddMetaTag(ds.Tables[0].Rows[0], ds.Tables[7]);
                        GenerateRelatedCategories(ds.Tables[4]);

                        if (ds.Tables[5].Rows.Count > 0)
                            GenerateOtherCompanies(ds.Tables[5], ds.Tables[0].Rows[0]["CTYPE"].ToString());
                        //GenerateTradeNames(ds.Tables[0].Rows[0]);
                        GenerateArticles();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }
        }

        private void AddCompanyDetails(DataRow dr, DataTable dtUrl, DataTable dtPhone)
        {
            try
            {
                ClientSK = dr["CLIENT_SK"].ToString();
                ClientName = dr["NAME"].ToString();// Utils.ReplaceContent(dr["NAME"].ToString(), 1);
                ClientNameFormatted = Utils.FormatCompanyWebsiteLink(dr["NAME"].ToString());
                CompRating = Convert.ToInt32(dr["RATINGAVG"].ToString()).ToString();
                CompCount = dr["RATINGCOUNT"].ToString();
                ShowReviews = dr["SHOW_REVIEWS"].ToString();
                string strdescr = dr["DESCRIPTION"].ToString();//Utils.ReplaceContent(dr["DESCRIPTION"].ToString(), 1);
                ClientDesc = new HtmlString("<p>" + strdescr.Replace(Environment.NewLine, "</p><p>").Replace("</p><p>", "</p>"));
                if (dr["COPRO_VIDEO"].ToString() != "")
                {
                    string sName = "";
                    if (Regex.IsMatch(dr["COPRO_VIDEO"].ToString(), @"^(http:|https:)\/\/(?:www\.)?youtube.com\/watch\?(?=.*v=\w+)(?:\S+)?$", RegexOptions.Singleline | RegexOptions.IgnoreCase))
                    {
                        Uri ytUri = new Uri(dr["COPRO_VIDEO"].ToString());
                        sName = HttpUtility.ParseQueryString(ytUri.Query).Get("V".ToLower());
                    }
                    else if (Regex.IsMatch(dr["COPRO_VIDEO"].ToString(), @"^(http:|https:)\/\/(?:www\.)?youtu.be\/([\w-]{10,12})$", RegexOptions.Singleline | RegexOptions.IgnoreCase))
                    {
                        sName = Regex.Replace(dr["COPRO_VIDEO"].ToString(), @"(http:|https:)\/\/(?:www\.)?youtu.be\/", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    }
                    if (sName != "")
                    {

                        YoutubeStyle = new HtmlString("style='background-image:url(//img.youtube.com/vi/" + sName + "/0.jpg); background-size:100% auto;'");
                        VideoLink = new HtmlString(RootPath + "coprovideo.html?v=" + sName + "&comp=" + ClientName);

                    }
                    else
                    {
                        YoutubeStyle = new HtmlString("background-size:100% auto;'");
                        VideoLink = new HtmlString("#");
                    }

                }
                else
                {
                    VideoLink = new HtmlString("#");
                }
                if (dr["COPRO_IMAGE"].ToString() != "")
                {
                    LogoLink = RootPath + @"images\profimages\" + dr["COPRO_IMAGE"].ToString();

                }
                else
                {
                    LogoLink = "";
                }

                Phone = ""; Fax = "";
                foreach (DataRow drPhone in dtPhone.Rows)
                    Phone += drPhone.ItemArray[0].ToString() + ",";
                if (Phone.Length > 0)
                    Phone = Phone.TrimEnd(',');

                if (dr["FAX_NUMBER"].ToString() != "")
                    Fax = dr["FAX_NUMBER"].ToString();


                Address = (dr["CITY"].ToString() != "") ? dr["CITY"].ToString() : string.Empty;
                Address = (dr["STATE"].ToString() != "") ? Address.Trim() + ", " + dr["STATE"].ToString() : Address;
                Address = (dr["ZIP"].ToString() != "") ? Address.Trim() + " " + dr["ZIP"].ToString() : Address;
                if (dr["ADDRESS"].ToString() != "")
                    Address = dr["ADDRESS"].ToString().Trim() + "<br/>" + Address;
                MapAddress = dr["Address"].ToString() + "," + dr["CITY"].ToString() + "," + dr["STATE"].ToString() + "," + dr["ZIP"].ToString();
                AddCompanyUrls(dtUrl, dr);
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }

        }

        private void AddMetaTag(DataRow dr, DataTable dtScripts)
        {
            try
            {
                Master.PageTitle = Utils.ReplaceContent(dr["Name"].ToString(), 1);
                Master.PageKeywords = dr["Name"].ToString() + ", " + dr["Name"].ToString() + " in " + dr["CITY"].ToString() + ", " + dr["Name"].ToString() + " in " + dr["STATE"].ToString() + ", " + dr["Name"].ToString() + " in " + dr["CITY"].ToString() + " " + dr["STATE"].ToString() + ", " + dr["Name"].ToString() + " " + dr["ADDRESS"].ToString();
                Master.PageDescription = "Find information on " + dr["Name"].ToString() + " on IQSdirectory. Request information on " + dr["Name"].ToString() + " located at " + dr["ADDRESS"].ToString() + "," + dr["CITY"].ToString() + "," + dr["STATE"].ToString() + ".";
                Master.PageIndex = new HtmlString("<meta name='robots' content='" + Utils.MetaRobots + "'>");


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
                Master.BindMeta();
            }
        }

        private void AddCompanyUrls(DataTable dt, DataRow drProf)
        {
            string noFollow = "", outBound = "";
            /* (drProf["META_TAG_Value"].ToString() != "")
            {
                if (drProf["ISOUTBOUNDSCRIPT"].ToString() == "Y")
                {
                    //divoutbound.InnerHtml = drProf["META_TAG_Value"].ToString();
                    if (drProf["META_TAG_Value"].ToString().Contains("msnTracker"))
                    {
                        //outBound = " onClick='outboundtracker(); msnTracker();' ";
                    }
                    else
                    {
                        // outBound = " onClick='outboundtracker();' ";
                    }
                }
            }*/
            try
            {
                if (drProf["ISNOFOLLOW"].ToString() == "Y")
                    noFollow = " rel='nofollow' ";
                StringBuilder sb = new StringBuilder();

                string mainurl = "";
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToBoolean(dr.ItemArray[1].ToString()) == true)
                    {
                        sb.Append("<span class='DPFCompanyResource1'>" + dr.ItemArray[0].ToString() + "</span>,");
                    }
                    else
                    {
                        string addHttp = "";
                        if (!dr.ItemArray[0].ToString().ToLower().StartsWith("https://") && !dr.ItemArray[0].ToString().ToLower().StartsWith("http://"))
                        {
                            addHttp = "http://";
                        }
                        string thisurl = addHttp + dr.ItemArray[0].ToString();
                        if (mainurl == "")
                            mainurl = thisurl;
                        else if (mainurl != "" && thisurl.Length < mainurl.Length)
                            mainurl = thisurl;

                    }
                }
                if (mainurl != "")
                {
                    if (Convert.ToInt32(ClientSK) == 63659) //Client wants only the main host url in the display and the landing url buried in href
                    {
                        string[] urldisp = mainurl.Replace("http://", "").Replace("https://", "").Split('/');
                        sb.Append("<a " + noFollow + "alt='" + ClientNameFormatted + "' title='" + ClientNameFormatted + "' href='" + mainurl + "' class='DPFCompanyResource1' target='_blank' >" + urldisp[0] + "</a>" + ",");

                    }
                    else
                        sb.Append("<a " + noFollow + "alt='" + ClientNameFormatted + "' title='" + ClientNameFormatted + "' href='" + mainurl + "' class='DPFCompanyResource1' target='_blank' >" + mainurl.Replace("http://", "") + "</a>" + ",");
                }
                WebsiteLink = sb.ToString().TrimEnd(',') + "<meta itemprop='url' content='" + mainurl + "'/>";
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }
        }

        private void GenerateRelatedCategories(DataTable dt)
        {
            RelatedCategories = dt.AsEnumerable().ToList();
        }

        private void GenerateOtherCompanies(DataTable dt, string CType)
        {
            WebClient wc = new WebClient();
            string json = "", citycode = "";
            try
            {
                json = wc.DownloadString("http://api.hostip.info/get_json.php");
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, string> obj = jss.Deserialize<Dictionary<string, string>>(json);
                citycode = obj["city"].ToString(); //"Carnegie, IN";
                string[] city = citycode.ToString().Split(',');
                if (city.Length > 1)
                    citycode = city[1].Trim().ToString();
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }
            //citycode = "PA";
            StringBuilder sb = new StringBuilder();
            string printed = "";
            try
            {
                DataRow[] drSel = dt.Select("RATECOUNT > 0 OR RATESUM > 0", "RATESUM DESC, RATECOUNT DESC, CLIENT_NAME ASC");
                foreach (DataRow dr in drSel)
                {
                    if (dr["CLIENT_SK"].ToString() == ClientSK)
                        sb.AppendLine("<li>" + dr["CLIENT_NAME"].ToString() + "</li>");
                    else
                        sb.AppendLine("<li><a href='" + RootPath + dr["DESCRIPTION"].ToString().Trim('/') + "/'  target='_blank'>" + dr["CLIENT_NAME"].ToString() + "</a></li>");
                    printed += dr["CLIENT_SK"].ToString() + ",";
                }
                string qry = "";
                if (printed.TrimEnd(',') != "")
                    qry = "AND CLIENT_SK NOT IN (" + printed.TrimEnd(',') + ")";
                drSel = dt.Select("STATE = '" + citycode.ToUpper() + "' " + qry, "CLIENT_NAME ASC");
                foreach (DataRow dr in drSel)
                {
                    if (dr["CLIENT_SK"].ToString() == ClientSK)
                        sb.AppendLine("<li>" + dr["CLIENT_NAME"].ToString() + "</li>");
                    else
                        sb.AppendLine("<li><a href='" + RootPath + dr["DESCRIPTION"].ToString().Trim('/') + "/'  target='_blank'>" + dr["CLIENT_NAME"].ToString() + "</a></li>");
                    printed += dr["CLIENT_SK"].ToString() + ",";
                }
                if (printed.TrimEnd(',') != "")
                    drSel = dt.Select("CLIENT_SK NOT IN (" + printed.TrimEnd(',') + ")", "CLIENT_NAME ASC");
                else
                    drSel = dt.Select("", "CLIENT_NAME ASC");
                foreach (DataRow dr in drSel)
                {
                    if (dr["CLIENT_SK"].ToString() == ClientSK)
                        sb.AppendLine("<li>" + dr["CLIENT_NAME"].ToString() + "</li>");
                    else
                        sb.AppendLine("<li><a href='" + RootPath + dr["DESCRIPTION"].ToString().Trim('/') + "/'  target='_blank'>" + dr["CLIENT_NAME"].ToString() + "</a></li>");
                    printed += dr["CLIENT_SK"].ToString() + ",";
                }
                RelatedCompaniesList = sb.ToString();
                string CTypeEnd = "";
                if (CType.Length > 0)
                    CTypeEnd = (CType.Substring(CType.Length - 1).ToUpper() == "S") ? "" : "s";
                else
                    CType = "Manufacturers";
                RelatedCompaniesHead = "Find Related " + CType + CTypeEnd;
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }

        }

        private void GenerateTradeNames(DataRow dr)
        {
            try
            {
                string desc = Utils.ReplaceContent(dr["TRADE_NAMES"].ToString().Trim(), 1);
                string[] tradeName = new string[100];
                tradeName = desc.Split(',');
                for (int i = 0; i < tradeName.Length; i++)
                {
                    TradeNames.Add(tradeName[i].ToString());
                }
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }

        }

        private void GenerateArticles()
        {
            var urlClientInfo = string.Format("api/Articles/GetArticlesByClientId?Client_SK=" + ClientSK);
            try
            {
                DataSet ds = wHelper.GetDataSetFromWebApi(urlClientInfo);
                if (ds != null)
                {

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];

                            dt.Columns.Add("URL");
                            dt.Columns.Add("DATE");
                            dt.Columns.Add("DESC");

                            dt.AsEnumerable().ToList().ForEach(r =>
                            {
                                r["URL"] = r["EXTERNAL_URL"].ToString() == "" ? wHelper.NewsDirectory + r["ARTICLE_CATEGORY_SK"] + "/" + r["ARTICLE_SK"].ToString() : r["EXTERNAL_URL"].ToString().ToLower().StartsWith("http://") ? r["EXTERNAL_URL"].ToString() : "http://" + r["EXTERNAL_URL"].ToString();
                                r["DATE"] = Convert.ToDateTime(r["DATE_CREATED"].ToString()).ToString("MMMM dd, yyyy");
                                r["DESC"] = Utils.FirstWords(Regex.Replace(r["DESCRIPTION"].ToString(), "<.*?>", string.Empty).Trim().Replace("\r\n", "").Replace("\t", " "), 90) + "...";
                            });
                            Articles = dt.AsEnumerable().ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }
        }

        public string RootPath { get; set; }
        public string DisplayName { get; set; }
        public string CategorySK { get; set; }
        public string ClientSK { get; set; }
        public string ClientName { get; set; }
        public string ClientNameFormatted { get; set; }
        public string CompRating { get; set; }
        public string CompCount { get; set; }
        public IHtmlString ClientDesc { get; set; }
        public string ShowReviews { get; set; }
        public string ApiPath { get; set; }
        public string ShareURL { get; set; }
        public string DirectoryURL { get; set; }

        public IHtmlString YoutubeStyle { get; set; }
        public IHtmlString VideoLink { get; set; }

        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string LogoLink { get; set; }
        public string WebsiteLink { get; set; }
        public string RelatedCompaniesList { get; set; }
        public string RelatedCompaniesHead { get; set; }
        public string MapAddress { get; set; }

        public List<DataRow> RelatedCategories { get; set; }
        public List<DataRow> RelatedCompanies { get; set; }
        public List<DataRow> Articles { get; set; }

        public List<string> TradeNames { get; set; }

    }
}