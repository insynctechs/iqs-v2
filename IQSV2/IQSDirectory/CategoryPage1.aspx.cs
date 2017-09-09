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
                DisplayCategory();                
            }
        }

        private void DisplayCategory()
        {
            var url = string.Format("api/CategoryPages/GetCategoryPage1Details?CategorySK=81&WebsiteType=Directory");
            DataSet ds =  wHelper.GetDataSetFromWebApi(url);
            if(ds!= null)
            {
                GenerateHeader(ds.Tables[0]);
                GenerateRelatedCategories(ds.Tables[2]);
                GenerateProfile(ds.Tables[3]);
                GenerateAdvertisements(ds.Tables[4]);
            }
        }

        private void GenerateHeader(DataTable dt)
        {
            CategorySK = dt.Rows[0]["CATEGORY_SK"].ToString();
            H1Text = dt.Rows[0]["H1DISPLAY_NAME"].ToString();
            DisplayName = dt.Rows[0]["DISPLAY_NAME"].ToString();
            ItemDesc = dt.Rows[0]["DESCRIPTION"].ToString();
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
        }

        public string CategorySK { get; set; }
        public string H1Text { get; set; }
        public string DisplayName { get; set; }
        public string ItemDesc { get; set; }
        public List<DataRow> RelatedCategories { get; set; }
        public List<DataRow> Tier1Advertisements { get; set; }
        public List<DataRow> Tier2Advertisements { get; set; }
        public List<DataRow> ProfileLinks { get; set; }
    }
}