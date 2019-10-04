using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IQSDirectory.Helpers;
using System.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace IQSDirectory
{
    public partial class CategoryPage1 : System.Web.UI.Page
    {
        WebApiHelper wHelper = new WebApiHelper();
        public DataTable dtNw;
        public DataTable dtBl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                InitializeVars();
                CheckCategory();
                
            }
        }

        private void InitializeVars()
        {

            Tier1Advertisements = new List<DataRow>();
            Tier2Advertisements = new List<DataRow>();
            ProfileLinks = new List<DataRow>();
            RelatedCategories = new List<DataRow>();
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
                    url = url.Remove(url.Length - 1);
                    RootPath = "../";

                }
                else
                {
                    url = url + '/';
                    Response.StatusCode = 301;
                    Response.Redirect(url);
                    Response.End();
                    RootPath = "./";
                }
                CategoryName = url.Split('/').Last();
                ShareURL = HttpContext.Current.Request.Url.AbsoluteUri;
                DirectoryURL = HttpContext.Current.Request.Url.Authority;
                var urlGetId = string.Format("api/CategoryPages/GetCategoryIdByName?DisplayName=" + CategoryName);
              
                DataTable dt = wHelper.GetDataTableFromWebApi(urlGetId);
                //CommonLogger.Info("url=" + url + "---;dt.Rows.Count=" + dt.Rows.Count + "--;SK=" + dt.Rows[0]["Category_SK"].ToString() + "=" + CategoryName);

                if (dt.Rows.Count > 0)
                {
                    //Response.Write("Category");
                    dtNw = GetArticles();
                    //dtBl = GetBlogs();
                    DisplayCategory(dt.Rows[0]["Category_SK"].ToString());
                }
                else
                {
                    Response.StatusCode = 301;
                    Response.Redirect(Utils.WebURL, true);
                    Response.End();
                }
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
                var url = string.Format("api/CategoryPages/GetCategoryPage1Details?CategorySK=" + CategoryID + "&WebsiteType=Directory&WebURL=" + Utils.WebURL);

                DataSet ds = wHelper.GetDataSetFromWebApi(url);
                if (ds != null)
                {
                    GenerateHeader(ds.Tables[0]);
                    //RegisterAsyncTask(new PageAsyncTask(GenerateHeader))

                    GenerateRelatedCategories(ds.Tables[1]);
                    GenerateProfile(ds.Tables[2]);
                    if (ds.Tables[0].Rows[0]["DISPLAY_PRODUCT"].ToString() == "Y")
                        GenerateProductPage(ds.Tables[3], ds.Tables[2], ds.Tables[5]);
                    else
                    {
                        GenerateAdvertisements(ds.Tables[3]);
                        GenerateIndustryInformation(ds.Tables[5]);
                    }
                    GenerateMetaTagsAndScripts(ds.Tables[4], ds.Tables[6]);
                    DisplayArticles();
                }
            }
                        
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }
        }

        private void GenerateHeader(DataTable dt)
        //private async Task GenerateHeader(DataTable dt)
        {
            try
            {
                ApiPath = wHelper.ApiUrl;
                //RootPath = HttpContext.Current.Request.ApplicationPath.ToString();
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
            try
            {
                dt.Columns.Add("FORMATED_NAME");
                dt.Columns.Add("FORMATED_URL");
                dt.Columns.Add("PROFILE_URL");
                dt.Columns.Add("NUM_PHONE");
                dt.AsEnumerable().ToList().ForEach(r =>
                {
                    r["FORMATED_NAME"] = Utils.FormatCompanyWebsiteLink(r["CLIENT_NAME"].ToString());
                    r["FORMATED_URL"] = Utils.ReplaceContent(r["COMPANY_URL"].ToString(), 0);
                    //r["NUM_PHONE"] = Regex.Replace(r["PHONE"].ToString(), "\\D", "");
                    string[] phoneList = r["PHONE"].ToString().Split(',').Where(x => x != null && x.Trim().Length > 0).Select(x => x.Trim()).ToArray(); 
                    foreach(string phone in phoneList)
                    {
                        r["NUM_PHONE"] += "<a itemprop='telephone' href='tel:+1-" + phone + "' >" + phone + "</a>";
                    }
                    //r["NUM_PHONE"] = r["NUM_PHONE"].ToString().TrimEnd(new char[] { ',', ' ' });
                    r["PROFILE_URL"] = Utils.ReplaceContent(ProfileLinks.Where(
                        p => p["ADVERTISEMENT_SK"].ToString() == r["ADVERTISEMENT_SK"].ToString()
                        && p["ENTITY_ATTRIBUTE_ID"].ToString() == "E-MAIL")
                        .Select(p => p["DESCRIPTION"].ToString()).FirstOrDefault(), 0);
                });
                if (dt.Rows.Count > 0)
                {
                    Tier1Advertisements = dt.Select("TIER_SK=1").ToList();
                    Tier2Advertisements = dt.Select("TIER_SK=2").ToList();
                    GetClientSkForRating(Tier1Advertisements, Tier2Advertisements);
                }
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }
        }

        private void GetClientSkForRating(List<DataRow> tier1Advertisements, List<DataRow> tier2Advertisements)
        {
            string ClientSKForRating = "";
            try
            {
                if (tier1Advertisements.Count > 0)
                {
                    ClientSKForRating += string.Join(",", tier1Advertisements.Select(ad => ad["CLIENT_SK"].ToString()));
                }
                if (Tier2Advertisements.Count > 0)
                {
                    if (ClientSKForRating != "")
                        ClientSKForRating += ",";
                    ClientSKForRating += string.Join(",", tier2Advertisements.Select(ad => ad["CLIENT_SK"].ToString()));
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

        private void GenerateIndustryInformation(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow[] dr = dt.Select("SECTION_ID ='Industry Information'");
                    if (dr != null)
                    {
                        IndustryInformation = new HtmlString(dr[0]["DESCRIPTION"].ToString());
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

            this.Master.PageIndex = new HtmlString("<meta name='robots' content='"+Utils.MetaRobots+"'>");
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

        private void DisplayArticles()
        {
            

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

            try
            {

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
                foreach (DataRow drN in dtNews.Rows)
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
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
            }

            Articles = dtNews.AsEnumerable().ToList();
        }

        private DataTable GetArticles()
        {
            try
            {
                var url = string.Format("api/Articles/GetLatestArticles?CategoryName=" + CategoryName);
                return wHelper.GetDataTableFromWebApi(url);
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
                return null;
            }
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
                CommonLogger.Info(ex.ToString());
                return null;
            }
        }

        #region "GenerateProductPage"
        private void GenerateProductPage(DataTable dtAdvertisements, DataTable dtWebsiteLinks, DataTable dtSection)
        {
            //string _defaultvalue = "##UserInput"; GetConfigValuesbyKey("ProductPagePrefixTag");
            //int _initialTag = 1; Convert.ToInt32(GetConfigValuesbyKey("TagInitialValue"));
            string _content = string.Empty;
            
            string inputvalue = string.Empty;

            try
            {

                DataRow[] drProd = dtSection.Select("SECTION_ID ='Product Description'");
                string productPage = "";
                if (drProd.Length > 0)
                {
                    productPage = drProd[0]["DESCRIPTION"].ToString();
                    productPage = productPage.Replace("FeaturedCompany.jpg", "featuredcompany.png");
                    productPage = productPage.Replace("featuredcompany.jpg", "featuredcompany.png");
                    productPage = productPage.Replace("Featuredcompany.jpg", "featuredcompany.png");
                }
                DataRow[] drInd = dtSection.Select("SECTION_ID ='Industry Information'");
                if (drInd.Length > 0)
                {

                    productPage = productPage.Replace("##UserInput_IndustryInformation", drInd[0]["DESCRIPTION"].ToString());                    
                    ProductInformation = new HtmlString(productPage);
                }
            }
            catch (Exception ex)
            {
                CommonLogger.Info(ex.ToString());
               
            }


        }
        #endregion




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
        public List<DataRow> RelatedCategories { get; set; }
        public List<DataRow> Tier1Advertisements { get; set; }
        public List<DataRow> Tier2Advertisements { get; set; }
        public List<DataRow> ProfileLinks { get; set; }
        public List<DataRow> Articles { get; set; }
        public IHtmlString IndustryInformation { get; set; }
        public IHtmlString ProductInformation { get; set; }
        public List<DataRow> ClientRatings { get; set; }
        public string ApiPath { get; set; }
        

    }
}