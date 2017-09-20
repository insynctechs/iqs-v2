using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net.Http;
using IQSDirectory.Helpers;
using System.Threading.Tasks;
using System.Data;
using System.Dynamic;
using Newtonsoft.Json;

namespace IQSDirectory
{
    public partial class CategoryPage1 : System.Web.UI.Page
    {
        WebApiHelper wHelper = new WebApiHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CheckCategory();
                //DisplayCategory();                
            }
        }

        private void CheckCategory()
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            if(url.IndexOf("/", url.Length - 1) > -1)
            {
                url = url.Remove(url.Length - 1);
            }
            string category = url.Split('/').Last();
            //Response.Write(category);
            var urlGetId = string.Format("api/CategoryPages/GetCategoryIdByName?DisplayName=" + category);
            DataTable dt = wHelper.GetDataTableFromWebApi(urlGetId);
            DisplayCategory(dt.Rows[0]["Category_SK"].ToString());
        }

        private void DisplayCategory(string CategoryID)
        {
            var url = string.Format("api/CategoryPages/GetCategoryPage1Details?CategorySK=" + CategoryID + "&WebsiteType=Directory");
            DataSet ds =  wHelper.GetDataSetFromWebApi(url);
            if(ds!= null)
            {
                GenerateHeader(ds.Tables[0]);
                GenerateRelatedCategories(ds.Tables[2]);
                GenerateProfile(ds.Tables[3]);
                GenerateAdvertisements(ds.Tables[4]);
                GenerateIndustryInformation(ds.Tables[6]);
            }
        }

        private void GenerateHeader(DataTable dt)
        {
            CategorySK = dt.Rows[0]["CATEGORY_SK"].ToString();
            H1Text = dt.Rows[0]["H1DISPLAY_NAME"].ToString();
            DisplayName = dt.Rows[0]["DISPLAY_NAME"].ToString();
            ItemDesc = new HtmlString(dt.Rows[0]["DESCRIPTION"].ToString());
        }

        private void GenerateRelatedCategories(DataTable dt)
        {
            RelatedCategories = dt.AsEnumerable().ToList();
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
                    p => p["ADVERTISEMENT_SK"].ToString() == r["ADVERTISEMENT_SK"].ToString() 
                    && p["ENTITY_ATTRIBUTE_ID"].ToString() == "E-MAIL")
                    .Select(p => p["DESCRIPTION"].ToString()).FirstOrDefault(),0);
            });

            Tier1Advertisements = dt.Select("TIER_SK=1").ToList();
            Tier2Advertisements = dt.Select("TIER_SK=2").ToList();

            GetClientSkForRating(Tier1Advertisements, Tier2Advertisements);
        }

        private void GetClientSkForRating(List<DataRow> tier1Advertisements, List<DataRow> tier2Advertisements)
        {
            string ClientSKForRating = "";
            if (tier1Advertisements.Count > 0)
            {
                ClientSKForRating += string.Join(",", tier1Advertisements.Select(ad => ad["CLIENT_SK"].ToString()));
            }
            if(Tier2Advertisements.Count > 0)
            {
                if (ClientSKForRating != "")
                    ClientSKForRating += ",";
                ClientSKForRating += string.Join(",", tier2Advertisements.Select(ad => ad["CLIENT_SK"].ToString()));
            }
            if(ClientSKForRating != "")
            {
                var url = string.Format("api/CompanyRatings/GetCompanyRatingByArray?ClientSkArray=" + ClientSKForRating);
                DataTable dt = wHelper.GetDataTableFromWebApi(url);
                ClientRatings = dt.Select("SHOW_REVIEWS='Y'").ToList();
            }
        }

        private void GenerateIndustryInformation(DataTable dt)
        {
            DataRow[] dr = dt.Select("SECTION_ID ='Industry Information'");
            if (dr != null)
            {
                IndustryInformation = new HtmlString(dr[0]["DESCRIPTION"].ToString());
            }
        }

        public string CategorySK { get; set; }
        public string H1Text { get; set; }
        public string DisplayName { get; set; }
        public IHtmlString ItemDesc { get; set; }
        public List<DataRow> RelatedCategories { get; set; }
        public List<DataRow> Tier1Advertisements { get; set; }
        public List<DataRow> Tier2Advertisements { get; set; }
        public List<DataRow> ProfileLinks { get; set; }
        public IHtmlString IndustryInformation { get; set; }
        public List<DataRow> ClientRatings { get; set; }
    }
}