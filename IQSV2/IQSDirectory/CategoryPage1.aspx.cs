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
using System.Text;
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
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            if(url.IndexOf("/", url.Length - 1) > -1)
            {
                url = url.Remove(url.Length - 1);
                Response.StatusCode = 301;
                Response.Redirect(url);
                Response.End();
            }
            RootPath = "";
            CategoryName = url.Split('/').Last();
            ShareURL = HttpContext.Current.Request.Url.AbsoluteUri;
            DirectoryURL = HttpContext.Current.Request.Url.Authority;
            var urlGetId = string.Format("api/CategoryPages/GetCategoryIdByName?DisplayName=" + CategoryName);
            DataTable dt = wHelper.GetDataTableFromWebApi(urlGetId);
            if (dt.Rows.Count > 0)
            {
                //Response.Write("Category");
                DisplayCategory(dt.Rows[0]["Category_SK"].ToString());
            }
            else
            {
                Response.StatusCode = 301;
                Response.Redirect(Utils.WebURL, true);
                Response.End();
            }

        }

        private void DisplayCategory(string CategoryID)
        {
            var url = string.Format("api/CategoryPages/GetCategoryPage1Details?CategorySK=" + CategoryID + "&WebsiteType=Directory");
            DataSet ds =  wHelper.GetDataSetFromWebApi(url);
            if(ds!= null)
            {
                GenerateHeader(ds.Tables[0]);
                
                GenerateRelatedCategories(ds.Tables[1]);
                GenerateProfile(ds.Tables[2]);
                if (ds.Tables[0].Rows[0]["DISPLAY_PRODUCT"].ToString() == "Y")
                    GenerateProductPage(ds.Tables[3], ds.Tables[2], ds.Tables[5]);
                else
                {
                    GenerateAdvertisements(ds.Tables[3]);
                    GenerateIndustryInformation(ds.Tables[5]);
                }
                GenerateMetaTagsAndScripts(ds.Tables[4],ds.Tables[6]);
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

        private void GenerateIndustryInformation(DataTable dt)
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

        private void GenerateMetaTagsAndScripts(DataTable dtMeta, DataTable dtScripts)
        {
                        
            DataRow[] dr;

            this.Master.PageIndex = new HtmlString("<meta name='robots' content='index,follow'>");

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

            this.Master.BindMeta();

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

        #region "GenerateProductPage"
        private void GenerateProductPage(DataTable dtAdvertisements, DataTable dtWebsiteLinks, DataTable dtSection)
        {
            //string _defaultvalue = "##UserInput"; GetConfigValuesbyKey("ProductPagePrefixTag");
            //int _initialTag = 1; Convert.ToInt32(GetConfigValuesbyKey("TagInitialValue"));
            string _content = string.Empty;
            
            string inputvalue = string.Empty;
            
            DataRow[] drProd = dtSection.Select("SECTION_ID ='Product Description'");
            string productPage = "";
            if (drProd.Length > 0)
            {
                productPage = drProd[0]["DESCRIPTION"].ToString();
            }
            DataRow[] drInd = dtSection.Select("SECTION_ID ='Industry Information'");
            if (drInd.Length > 0)
            {
                
                productPage = productPage.Replace("##UserInput_IndustryInformation", drInd[0]["DESCRIPTION"].ToString());
                ProductInformation =    new HtmlString(productPage);
            }
            
            /*//strDirectoryFile.AppendLine(_row.Length.ToString());
            if (_row.Length > 0)
            {
                ProductInformation = _row[0].ItemArray[2].ToString();
                for (int index = 0; index <= dtAdvertisements.Rows.Count - 1; index++)
                {
                    int AdvertisementSK = Int32.Parse(dtAdvertisements.Rows[index].ItemArray[1].ToString());
                    DataRow[] websiteLinks = dtWebsiteLinks.Select("ADVERTISEMENT_SK =" + AdvertisementSK);
                    DataRow[] EmailWebSiteLinks = dtWebsiteLinks.Select("ADVERTISEMENT_SK =" + AdvertisementSK + "and  ENTITY_ATTRIBUTE_ID='E-MAIL'");
                    string rfq_Path = ConfigurationManager.AppSettings["DirectoryRFQPath"].ToLower();
                    rfq_Path += "?CategorySK=" + CategorySK;
                    rfq_Path += "&amp;ClientSK=" + dtAdvertisements.Rows[index].ItemArray[4].ToString();

                    string clientname = dtAdvertisements.Rows[index].ItemArray[5].ToString();
                    imagePath = ConfigurationManager.AppSettings["DirectoryImagePath"].ToString().ToLower() + dtCategoryDesc.Rows[0].ItemArray[0].ToString().ToLower() + "/images/" + dtAdvertisements.Rows[index].ItemArray[2].ToString().ToLower();

                    inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("RFQImageTag");
                    //************ removed target = blank  on 24th mar 2010 ****************
                    _content = _content.Replace(inputvalue, "<a  href='" + rfq_Path + "'><img alt=\"IQSDirectory\" title =\"IQSDirectory\" src='../commonimages/requestforbtn.gif' border='0' /></a>");
                    inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("bannerImageTag");
                    _content = _content.Replace(inputvalue, "<img alt=\"IQSDirectory\" id='PlaceHolder_Tier1'  src='" + imagePath + "' />");
                    inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("CompanyNameTag");
                    if (strOutBoundTrackingscript.Trim().Length > 0)
                    {
                        //************ removed target = blank  on 24th mar 2010 ****************
                        if (strOutBoundTrackingscript.Contains("msnTracker"))
                            _content = _content.Replace(inputvalue, "<a  alt='" + clientname + "' id='" + "ID" + dtAdvertisements.Rows[index].ItemArray[1].ToString() + "' onClick=\"outboundTracker(); msnTracker();\"  href='" + strDirectoryCompanyProfilePath + Utils.ReplaceContent(EmailWebSiteLinks[0]["DESCRIPTION"].ToString(), 0) + "' >" + clientname + "</a><br />");
                        else
                            _content = _content.Replace(inputvalue, "<a  alt='" + clientname + "' id='" + "ID" + dtAdvertisements.Rows[index].ItemArray[1].ToString() + "' onClick=\"outboundTracker()\"  href='" + strDirectoryCompanyProfilePath + Utils.ReplaceContent(EmailWebSiteLinks[0]["DESCRIPTION"].ToString(), 0) + "' >" + clientname + "</a><br />");
                    }
                    else
                        //************ removed target = blank  on 24th mar 2010 ****************
                        _content = _content.Replace(inputvalue, "<a alt='" + clientname + "' id='" + "ID" + dtAdvertisements.Rows[index].ItemArray[1].ToString() + "'  href='" + strDirectoryCompanyProfilePath + Utils.ReplaceContent(clientname, 3) + "' >" + clientname + "</a><br />"); //Utils.ReplaceContent(dtAdvertisements.Rows[index].ItemArray[3].ToString(), 0) 
                    inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("AddressTag");
                    _content = _content.Replace(inputvalue, dtAdvertisements.Rows[index].ItemArray[6].ToString());
                    inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("FaxTag");
                    _content = _content.Replace(inputvalue, dtAdvertisements.Rows[index].ItemArray[7].ToString());
                    inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("RFQLinkTag");
                    //************ removed target = blank  on 24th mar 2010 ****************
                    _content = _content.Replace(inputvalue, "<a  href='" + rfq_Path + "'>Request For Quote</a>");
                    inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("AdvDescriptionTag");
                    _content = _content.Replace(inputvalue, dtAdvertisements.Rows[index].ItemArray[0].ToString());
                    inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("EmailIDTag");
                    _content = _content.Replace(inputvalue, "<a href='mailto:" + dtAdvertisements.Rows[index].ItemArray[9].ToString() + "'>" + dtAdvertisements.Rows[index].ItemArray[9].ToString() + "</a>");
                    inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("ZipTag");
                    _content = _content.Replace(inputvalue, dtAdvertisements.Rows[index].ItemArray[10].ToString());
                    inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("ClientURLTag");
                    _content = _content.Replace(inputvalue, (dtAdvertisements.Rows[index].ItemArray[3].ToString().StartsWith("http://")) ? "<a href='" + dtAdvertisements.Rows[index].ItemArray[3].ToString() + "'>" + dtAdvertisements.Rows[index].ItemArray[3].ToString().Substring(8, dtAdvertisements.Rows[index].ItemArray[3].ToString().Length - 8) + "</a>" : "<a href='" + dtAdvertisements.Rows[index].ItemArray[3].ToString() + "'>" + dtAdvertisements.Rows[index].ItemArray[3].ToString() + "</a>");


                    foreach (DataRow drLinks in websiteLinks)
                    {
                        string _link = string.Empty;
                        switch (drLinks["ENTITY_ATTRIBUTE_ID"].ToString())
                        {
                            case "E-MAIL":
                                string strEmailDes = Convert.ToString(drLinks["DESCRIPTION"]);
                                strEmailDes = strEmailDes.Replace("®", "&reg;");
                                strDirectoryFile.AppendLine("");
                                inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("EmailLinkTag");
                                //************ removed target = blank  on 24th mar 2010 ****************
                                _link = "<a  href='" + strDirectoryCompanyProfilePath.ToLower() + strEmailDes.ToLower() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>";
                                _content = _content.Replace(inputvalue, _link);
                                break;
                            case "DRAWING":
                                inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("DrawingTag");
                                //************ removed target = blank  on 24th mar 2010 ****************
                                _link = (drLinks["DESCRIPTION"].ToString().ToLower().StartsWith("http://")) ? "<a  rel='nofollow' href='" + drLinks["DESCRIPTION"].ToString().ToLower() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>" : "<a  rel='nofollow' href='http://" + drLinks["DESCRIPTION"].ToString() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>";
                                _content = _content.Replace(inputvalue, _link);
                                break;
                            case "CATALOG":
                                inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("CatalogTag");
                                //************ removed target = blank  on 24th mar 2010 ****************
                                _link = (drLinks["DESCRIPTION"].ToString().ToLower().StartsWith("http://")) ? "<a rel='nofollow' href='" + drLinks["DESCRIPTION"].ToString().ToLower() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>" : "<a  rel='nofollow' href='http://" + drLinks["DESCRIPTION"].ToString() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>";
                                _content = _content.Replace(inputvalue, _link);
                                break;
                            case "E-COMMERCE":
                                inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("E-commerceTag");
                                //************ removed target = blank  on 24th mar 2010 ****************
                                _link = (drLinks["DESCRIPTION"].ToString().ToLower().StartsWith("http://")) ? "<a rel='nofollow' href='" + drLinks["DESCRIPTION"].ToString().ToLower() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>" : "<a rel='nofollow' href='http://" + drLinks["DESCRIPTION"].ToString() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>";
                                _content = _content.Replace(inputvalue, _link);
                                break;
                            case "CERTIFICATION":
                                inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("CertificationTag");
                                //************ removed target = blank  on 24th mar 2010 ****************
                                _link = (drLinks["DESCRIPTION"].ToString().ToLower().StartsWith("http://")) ? "<a  rel='nofollow' href='" + drLinks["DESCRIPTION"].ToString().ToLower() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>" : "<a rel='nofollow' href='http://" + drLinks["DESCRIPTION"].ToString() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>";
                                _content = _content.Replace(inputvalue, _link);
                                break;
                            case "DISTRIBUTOR":
                                inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("DistributorsTag");
                                //************ removed target = blank  on 24th mar 2010 ****************
                                _link = (drLinks["DESCRIPTION"].ToString().ToLower().StartsWith("http://")) ? "<a rel='nofollow' href='" + drLinks["DESCRIPTION"].ToString().ToLower() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>" : "<a rel='nofollow' href='http://" + drLinks["DESCRIPTION"].ToString() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>";
                                _content = _content.Replace(inputvalue, _link);
                                break;
                            case "PRESS RELEASE":
                                inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("PressreleaseTag");
                                //************ removed target = blank  on 24th mar 2010 ****************
                                _link = (drLinks["DESCRIPTION"].ToString().ToLower().StartsWith("http://")) ? "<a rel='nofollow' href='" + drLinks["DESCRIPTION"].ToString().ToLower() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>" : "<a rel='nofollow' href='http://" + drLinks["DESCRIPTION"].ToString() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>";
                                _content = _content.Replace(inputvalue, _link);
                                break;
                            case "REPS":
                                inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("RepsTag");
                                //************ removed target = blank  on 24th mar 2010 ****************
                                _link = (drLinks["DESCRIPTION"].ToString().ToLower().StartsWith("http://")) ? "<a  rel='nofollow' href='" + drLinks["DESCRIPTION"].ToString().ToLower() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>" : "<a rel='nofollow' href='http://" + drLinks["DESCRIPTION"].ToString() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>";
                                _content = _content.Replace(inputvalue, _link);
                                break;
                            case "CAPABILITIES":
                                inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("CapabilitiesTag");
                                //************ removed target = blank  on 24th mar 2010 ****************
                                _link = (drLinks["DESCRIPTION"].ToString().ToLower().StartsWith("http://")) ? "<a rel='nofollow' href='" + drLinks["DESCRIPTION"].ToString().ToLower() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>" : "<a  rel='nofollow' href='http://" + drLinks["DESCRIPTION"].ToString().ToLower() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>";
                                _content = _content.Replace(inputvalue, _link);
                                break;
                            case "EQUIPMENT":
                                inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("EquipmentTag");
                                //************ removed target = blank  on 24th mar 2010 ****************
                                _link = (drLinks["DESCRIPTION"].ToString().ToLower().StartsWith("http://")) ? "<a  rel='nofollow' href='" + drLinks["DESCRIPTION"].ToString().ToLower() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>" : "<a rel='nofollow' href='http://" + drLinks["DESCRIPTION"].ToString().ToLower() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>";
                                _content = _content.Replace(inputvalue, _link);
                                break;
                            case "VIDEO URL":
                                inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("VideourlTag");
                                //************ removed target = blank  on 24th mar 2010 ****************
                                _link = (drLinks["DESCRIPTION"].ToString().ToLower().StartsWith("http://")) ? "<a  rel='nofollow' href='" + drLinks["DESCRIPTION"].ToString().ToLower() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>" : "<a  rel='nofollow' href='http://" + drLinks["DESCRIPTION"].ToString().ToLower() + "' >" + drLinks["ENTITY_ATTRIBUTE_ID"].ToString() + "</a>";
                                _content = _content.Replace(inputvalue, _link);
                                break;
                        }
                    }
                    if (GetConfigValuesbyKey("Seperatordisplayflag") == "Y")
                    {
                        inputvalue = _defaultvalue + (_initialTag) + GetConfigValuesbyKey("SeperatorTag");
                        _content = _content.Replace(inputvalue, "<div class=\"borderlist clearfix\"></div>");
                    }
                    _initialTag = _initialTag + 1;
                }
            }
            if (_rowindustryinformation.Length > 0)
            {
                inputvalue = _defaultvalue + GetConfigValuesbyKey("IndustryInformationcontentTag");
                _content = _content.Replace(inputvalue, _rowindustryinformation[0].ItemArray[2].ToString());
            }
            if (GetConfigValuesbyKey("Seperatordisplayflag") == "N")
                _content = _content + "<div class=\"borderlist clearfix\"></div>";
            strDirectoryFile.AppendLine(_content);*/

            // generateAdditionalCompaniesLinks();
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