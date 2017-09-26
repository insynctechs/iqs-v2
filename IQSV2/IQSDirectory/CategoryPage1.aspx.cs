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
using System.Text.RegularExpressions;

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
            }
        }

        private void CheckCategory()
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            if(url.IndexOf("/", url.Length - 1) > -1)
            {
                url = url.Remove(url.Length - 1);
            }
            CategoryName = url.Split('/').Last();
            ShareURL = HttpContext.Current.Request.Url.AbsoluteUri;
            DirectoryURL = HttpContext.Current.Request.Url.Authority;
            var urlGetId = string.Format("api/CategoryPages/GetCategoryIdByName?DisplayName=" + CategoryName);
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
                GenerateMetaTags(ds.Tables[5]);
                DisplayArticles();
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

        private void GenerateIndustryInformation(DataTable dt)
        {
            DataRow[] dr = dt.Select("SECTION_ID ='Industry Information'");
            if (dr != null)
            {
                IndustryInformation = new HtmlString(dr[0]["DESCRIPTION"].ToString());
            }
        }

        private void GenerateMetaTags(DataTable dt)
        {
            DataRow[] dr = dt.Select("META_TAG_ID = 'TITLE'");
            if (dr != null)
            {
                Title = dr[0]["DESCRIPTION"].ToString();
                Title = Title.Replace("–", "-");
                Title = Title.Replace("&", "&amp;");
            }

            dr = dt.Select("META_TAG_ID = 'DESCRIPTION'");
            if (dr != null)
            {
                MetaDesc = dr[0]["DESCRIPTION"].ToString();
            }
        }

        private void DisplayArticles()
        {
            DataTable dtNw = GetArticles();
            DataTable dtBl = GetBlogs();

            DataTable dtBlog = new DataTable();
            dtBlog.Columns.Add("HEADING");
            dtBlog.Columns.Add("DESCRIPTION");
            dtBlog.Columns.Add("DATE_CREATED");
            dtBlog.Columns.Add("ARTICLE_CATEGORY_SK");
            dtBlog.Columns.Add("ARTICLE_SK");
            dtBlog.Columns.Add("NAME");
            dtBlog.Columns.Add("ATYPE");
            dtBlog.AcceptChanges();

            DataTable dtNews = new DataTable();
            dtNews.Columns.Add("HEADING");
            dtNews.Columns.Add("DESCRIPTION");
            dtNews.Columns.Add("DATE_CREATED");
            dtNews.Columns.Add("ARTICLE_CATEGORY_SK");
            dtNews.Columns.Add("ARTICLE_SK");
            dtNews.Columns.Add("NAME");
            dtNews.Columns.Add("ATYPE");
            dtNews.AcceptChanges();

            if (dtBl != null)
            {
                foreach (DataRow dr in dtBl.Rows)
                {
                    dtBlog.Rows.Add(new object[] { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString() });
                }
            }

            if (dtNw != null)
            {
                foreach (DataRow dr in dtNw.Rows)
                {
                    dtBlog.Rows.Add(new object[] { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString() });
                }
            }
            dtBlog.AcceptChanges();
            dtNews.Rows.Clear();
            List<int> generated = new List<int>();
            if (dtBlog.Rows.Count <= 6)
            {
                for (int i = 0; i < dtBlog.Rows.Count; i++)
                {
                    dtNews.Rows.Add(new object[] { dtBlog.Rows[i][0].ToString(), dtBlog.Rows[i][1].ToString(), dtBlog.Rows[i][2].ToString(), dtBlog.Rows[i][3].ToString(), dtBlog.Rows[i][4].ToString(), dtBlog.Rows[i][5].ToString(), dtBlog.Rows[i][6].ToString() });
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    int rnd;
                    do
                    { rnd = (int)new Random().Next(1, dtBlog.Rows.Count); } while (generated.Contains(rnd));
                    generated.Add(rnd);
                    dtNews.Rows.Add(new object[] { dtBlog.Rows[rnd][0].ToString(), dtBlog.Rows[rnd][1].ToString(), dtBlog.Rows[rnd][2].ToString(), dtBlog.Rows[rnd][3].ToString(), dtBlog.Rows[rnd][4].ToString(), dtBlog.Rows[rnd][5].ToString(), dtBlog.Rows[rnd][6].ToString() });
                }
            }

            dtNews.Columns.Add("URL");
            dtNews.Columns.Add("DISPLAYDESC");
            foreach(DataRow drN in dtNews.Rows)
            {
                if (drN["ATYPE"].ToString() == "NEWS")
                {
                    drN["URL"] = wHelper.NewsDirectory + drN["ARTICLE_CATEGORY_SK"] + "/" + drN["ARTICLE_SK"].ToString() + "/";
                }
                else
                {
                    drN["URL"] = wHelper.BlogDirectory + drN["ARTICLE_CATEGORY_SK"] + "/" + drN["ARTICLE_SK"].ToString() + "/";
                }
                drN["DISPLAYDESC"] = Utils.FirstWords(Regex.Replace(drN["DESCRIPTION"].ToString(), "<.*?>", string.Empty).Trim().Replace("\r\n", "").Replace("\t", " "), 90) + "...";
            }

            Articles = dtNews.AsEnumerable().ToList();
        }

        private DataTable GetArticles()
        {
            var url = string.Format("api/Articles/GetLatestArticles?CategoryName=" + CategoryName);
            return wHelper.GetDataTableFromWebApi(url);
        }

        private DataTable GetBlogs()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("HEADING");
            dt.Columns.Add("DESCRIPTION");
            dt.Columns.Add("DATE_CREATED");
            dt.Columns.Add("ARTICLE_CATEGORY_SK");
            dt.Columns.Add("ARTICLE_SK");
            dt.Columns.Add("NAME");
            dt.Columns.Add("ATYPE");
            dt.AcceptChanges();
            try
            {
                string url = wHelper.BlogDirectory +  "iqssearch/getlatestblogs.php?c=" + CategoryName;
                string json = new System.Net.WebClient().DownloadString(url);
                List<object[]> li = JsonConvert.DeserializeObject<List<object[]>>(json);
                foreach (object[] obj in li)
                {
                    dt.Rows.Add(new object[] { obj[0].ToString(), obj[1].ToString(), obj[2].ToString(), obj[3].ToString(), obj[4].ToString(), obj[5].ToString(), obj[6].ToString() });
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string CategoryName { get; set; }
        public string CategorySK { get; set; }
        public string Title { get; set; }
        public string MetaDesc { get; set; }
        public string H1Text { get; set; }
        public string DisplayName { get; set; }
        public string ShareURL { get; set; }
        public string DirectoryURL { get; set; }
        public IHtmlString ItemDesc { get; set; }
        public List<DataRow> RelatedCategories { get; set; }
        public List<DataRow> Tier1Advertisements { get; set; }
        public List<DataRow> Tier2Advertisements { get; set; }
        public List<DataRow> ProfileLinks { get; set; }
        public List<DataRow> Articles { get; set; }
        public IHtmlString IndustryInformation { get; set; }
        public List<DataRow> ClientRatings { get; set; }
        public string ApiPath { get; set; }
    }
}