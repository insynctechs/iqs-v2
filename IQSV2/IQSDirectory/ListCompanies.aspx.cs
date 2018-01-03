using IQSDirectory.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQSDirectory
{
    public partial class ListCompanies : System.Web.UI.Page
    {
        WebApiHelper wHelper = new WebApiHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(ValidatePage() ==false)
            {
                Response.Redirect("~");
            }
            GenerateMetaTagsAndScripts();
            this.Master.BindMeta();
            LoadData();
           
        }

        private bool ValidatePage()
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;
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
            string srh = url.Substring(url.ToLower().IndexOf("listcompanies"));
            object[] queryVals = srh.Split('/');
            if (queryVals.Length == 1)
            {
                Response.StatusCode = 301;
                Response.Redirect("listCompanies/a/1");
                Response.End();
            }
            else if (queryVals.Length == 3)
            {
                SrhLetter = queryVals[1].ToString();
                if (Regex.IsMatch(queryVals[2].ToString(), @"^[0-9]+$"))
                {
                    SrhPage = Convert.ToInt32(queryVals[2].ToString());
                }
                else
                {
                    SrhPage = 1;
                }
                return true;
            }
            return false;
        }

        private void GenerateMetaTagsAndScripts()
        {
            this.Master.PageIndex = new HtmlString("<meta name='robots' content='index,follow'>");
            this.Master.PageDescription = "";
            this.Master.PageTitle = "Directory Of Companies";
            this.Master.PageKeywords = "";
            this.Master.HeadScript = new HtmlString("<!-- Google Tag Manager --><script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':new Date().getTime(),event:'gtm.js'});var f = d.getElementsByTagName(s)[0], j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async=true;j.src='https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j, f);})(window, document,'script','dataLayer','GTM-NGZWMKN');</script><!-- End Google Tag Manager -->");
            this.Master.BodyOpenScript = new HtmlString("<!-- Google Tag Manager (noscript) --><noscript><iframe src = 'https://www.googletagmanager.com/ns.html?id=GTM-NGZWMKN' height='0' width='0' style ='display:none;visibility:hidden' ></iframe ></noscript ><!--End Google Tag Manager(noscript)--> ");
            this.Master.HitsLinkScript = new HtmlString("<!-- Industrial Quick Search Referring Site Stats web tools statistics hit counter code --><script type = 'text/javascript' id = 'wa_u' ></script><script type='text/javascript' >//<![CDATA[ wa_account = '968E8C9B968D9A9C8B908D86'; wa_location = 29;     wa_pageName = location.pathname;   document.cookie = '__support_check=1;path=/'; wa_hp = 'http'; wa_rf = document.referrer; wa_sr = window.location.search;  wa_tz = new Date(); if (location.href.substr(0, 6).toLowerCase() == 'https:')      wa_hp = 'https'; wa_data = '&an=' + escape(navigator.appName) + '&sr=' + escape(wa_sr) + '&ck=' + document.cookie.length + '&rf=' + escape(wa_rf) + '&sl=' + escape(navigator.systemLanguage) +  '&av=' + escape(navigator.appVersion) + '&l=' + escape(navigator.language) +  '&pf=' + escape(navigator.platform) + '&pg=' + escape(wa_pageName);   wa_data = wa_data + '&cd=' +  screen.colorDepth + '&rs=' + escape(screen.width + ' x ' + screen.height) + '&tz=' + wa_tz.getTimezoneOffset() + '&je=' + navigator.javaEnabled();  wa_img = new Image(); wa_img.src = wa_hp + '://loc1.hitsprocessor.com/statistics.asp' +'?v=1&s=' + wa_location + '&eacct=' + wa_account + wa_data + '&tks=' + wa_tz.getTime(); document.cookie = '__support_check=1;path=/;expires=Thu, 01-Jan-1970 00:00:01 GMT'; document.getElementById('wa_u').src = wa_hp + '://loc1.hitsprocessor.com/track.js'; //]]></script>");
        }

        private void LoadData()
        {
            ApiPath = wHelper.ApiUrl;
            RootPath = "../../../";

            var url = string.Format("api/Clients/GetListCompanies?SrhLetter=" + SrhLetter);
            DataTable dt = wHelper.GetDataTableFromWebApi(url);
            try
            {
                if(dt!= null)
                {
                    int RecPerPage = 30;
                    int Limit = RecPerPage;
                    int TotalRecs = dt.Rows.Count;
                    TotalPages = (int)Math.Ceiling((double)TotalRecs / (double)RecPerPage);
                    if (SrhPage < 1 || SrhPage > TotalPages)
                        SrhPage = 1;

                    if ((SrhPage) == TotalPages && (TotalRecs % RecPerPage) > 0)
                        Limit = ((SrhPage-1) * RecPerPage) + (TotalRecs % RecPerPage);
                    else
                        Limit = (SrhPage) * RecPerPage;

                    int Start = (SrhPage-1) * RecPerPage;

                    dt.Columns.Add("FORMATED_URL");
                    dt.Columns.Add("CITYSTATE");
                    dt.AsEnumerable().ToList().ForEach(r => {
                        r["FORMATED_URL"] = Utils.ReplaceContent(r["URL"].ToString(), 0);
                        r["CITYSTATE"] = ((r["state"].ToString() != "") & (r["city"].ToString() != "")) ? r["city"].ToString() + "," + r["state"].ToString() : r["city"].ToString().Equals(string.Empty) ? Convert.ToString(r["state"]) : Convert.ToString(r["city"]);
                    });
                    CompaniesList = dt.AsEnumerable().ToList().Skip(Start).Take(Limit).ToList();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error while creating directory page" + ex.Message);
            }
        }

        public string RootPath { get; set; }
        public string ApiPath { get; set; }
        public string SrhLetter { get; set; }
        public int SrhPage { get; set; }
        public int TotalPages { get; set; }
        public List<DataRow> CompaniesList { get; set; }
    }
}