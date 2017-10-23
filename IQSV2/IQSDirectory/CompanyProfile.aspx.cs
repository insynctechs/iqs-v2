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
using System.Web.UI.HtmlControls;

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
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            if (url.IndexOf("/", url.Length - 1) > -1)
            {
                url = url.Remove(url.Length - 1);
            }
            string copro = url.Split('/').Last();
            string Client_SK = copro.Substring(copro.LastIndexOf('-')).Trim('-');
            var urlClientInfo = string.Format("api/Clients/GetClientProfileDetails?Client_SK=" + Client_SK);
            DataSet ds = wHelper.GetDataSetFromWebApi(urlClientInfo);
            AddCompanyDetails(ds.Tables[0].Rows[0]);
            AddMetaTag(ds.Tables[0].Rows[0]);
        }

        private void AddCompanyDetails(DataRow dr)
        {
            ClientSK = dr["CLIENT_SK"].ToString();
            ClientName = Utils.FormatCompanyWebsiteLink(dr["NAME"].ToString());
            CompRating = dr["RATINGAVG"].ToString(); ;
            CompCount = dr["RATINGCOUNT"].ToString(); ;
            ShowReviews = dr["SHOW_REVIEWS"].ToString();
            ClientDesc = Utils.ReplaceContent(dr["DESCRIPTION"].ToString(), 1);
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
                    VideoStyle = new HtmlString("style='display:block'");
                    YoutubeStyle = new HtmlString("style='background-image:url(http://img.youtube.com/vi/" + sName + "/0.jpg); background-size:100% auto;'");
                    VideoLink = new HtmlString(RootPath + "coprovideo.html?v=" + sName + "&comp=" + ClientName);
                    DescClass = new HtmlString("class='col2 coprorightdiv'");
                }
                else
                {
                    VideoStyle = new HtmlString("style='display:none'");
                    YoutubeStyle = new HtmlString("style='background-size:100% auto;'");
                    VideoLink = new HtmlString("#");
                    DescClass = new HtmlString("class='col2'");
                }
            }
        }

        private void AddMetaTag(DataRow dr)
        {
            Master.PageTitle = Utils.FormatCompanyWebsiteLink(dr["Name"].ToString());
            Master.PageKeywords = dr["Name"].ToString() + ", " + dr["Name"].ToString() + " in " + dr["CITY"].ToString() + ", " + dr["Name"].ToString() + " in " + dr["STATE"].ToString() + ", " + dr["Name"].ToString() + " in " + dr["CITY"].ToString() + " " + dr["STATE"].ToString() + ", " + dr["Name"].ToString() + " " + dr["ADDRESS"].ToString();
            Master.PageDescription = "Find information on " + dr["Name"].ToString() + " on IQSdirectory. Request information on " + dr["Name"].ToString() + " located at " + dr["ADDRESS"].ToString() + "," + dr["CITY"].ToString() + "," + dr["STATE"].ToString() + ".";
            Master.BindMeta();
        }

        public string RootPath { get; set; }
        public string DisplayName { get; set; }
        public string CategorySK { get; set; }
        public string ClientSK { get; set; }
        public string ClientName { get; set; }
        public string CompRating { get; set; }
        public string CompCount { get; set; }
        public string ClientDesc { get; set; }
        public string ShowReviews { get; set; }
        public string ApiPath { get; set; }
        public IHtmlString VideoStyle { get; set; }
        public IHtmlString YoutubeStyle { get; set; }
        public IHtmlString VideoLink { get; set; }
        public IHtmlString DescClass { get; set; }
    }
}