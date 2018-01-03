using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IQSDirectory.Helpers;

namespace IQSDirectory
{
    public partial class DirectoryPremiumListing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebApiHelper wHelper = new WebApiHelper();
            ApiPath = wHelper.ApiUrl;
            CategorySK = "0";
            RootPath = "./";
            GenerateMetaTagsAndScripts();
            this.Master.BindMeta();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            /*
            string _strMailBody = null;
            string _toAddress = string.Empty;
            string _FromAddress = string.Empty;
            string _Subject = string.Empty;

            _strMailBody = "Suggested IQSDirectory Site : " + hdnCategoryName.Value + "<br>" + "Company Name : " + txtCompanyName.Text + "<br>" + "Company Phone : " + txtCompanyPhone.Text + "<br>" + "Company Website : " + txtCompanyWebsite.Text + "<br>" + "Product/Service Area : " + txtProductArea.Text + "<br>" + "Contact : " + txtContactName.Text + "<br>" + "Contact Title : " + txtContactTitle.Text + "<br>" + "Contact Email : " + txtContactEmailAddress.Text;
            _strMailBody = _strMailBody + "<br><br>" + "Best Regards" + "<br>" + txtContactName.Text;
            _toAddress = System.Configuration.ConfigurationManager.AppSettings["ListYourCompanyToMailID"];
            _FromAddress = txtContactEmailAddress.Text;
            //System.Configuration.ConfigurationManager.AppSettings["ListYourCompanyFromMailID"];
            _Subject = System.Configuration.ConfigurationManager.AppSettings["ListYourCompanyPremiumSubject"];
            //Utils.SendMail(_FromAddress, _toAddress, string.Empty, string.Empty, _Subject, _strMailBody, true);
            Utils.SendMail("sumi@insynctechs.com", "sumiajit@gmail.com", string.Empty, string.Empty, _Subject, _strMailBody, true);   
            Response.Redirect("DirectoryListingThankYou.aspx?CategoryName=" + hdnCategoryName.Value + "&CompanyName=" + Server.UrlEncode(txtCompanyName.Text.Trim()) + "&CompanyPhone=" + Server.UrlEncode(txtCompanyPhone.Text.Trim()) + "&CompanyWebsite=" + Server.UrlEncode(txtCompanyWebsite.Text.Trim()) + "&ProductArea=" + Server.UrlEncode(txtProductArea.Text.Trim()) + "&ContactName=" + Server.UrlEncode(txtContactName.Text.Trim()) + "&ContactTitle=" + Server.UrlEncode(txtContactTitle.Text.Trim()) + "&Amount=");
            */
        }

        private void GenerateMetaTagsAndScripts()
        {
            this.Master.PageIndex = new HtmlString("<meta name='robots' content='index,follow'>");
            this.Master.PageDescription = "";
            this.Master.PageTitle = "IQSDirectory | Premium Listing";
            this.Master.PageKeywords = "";
            this.Master.HeadScript = new HtmlString("<!-- Google Tag Manager --><script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':new Date().getTime(),event:'gtm.js'});var f = d.getElementsByTagName(s)[0], j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async=true;j.src='https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j, f);})(window, document,'script','dataLayer','GTM-NGZWMKN');</script><!-- End Google Tag Manager -->");
            this.Master.BodyOpenScript = new HtmlString("<!-- Google Tag Manager (noscript) --><noscript><iframe src = 'https://www.googletagmanager.com/ns.html?id=GTM-NGZWMKN' height='0' width='0' style ='display:none;visibility:hidden' ></iframe ></noscript ><!--End Google Tag Manager(noscript)--> ");
            this.Master.HitsLinkScript = new HtmlString("<!-- Industrial Quick Search Referring Site Stats web tools statistics hit counter code --><script type = 'text/javascript' id = 'wa_u' ></script><script type='text/javascript' >//<![CDATA[ wa_account = '968E8C9B968D9A9C8B908D86'; wa_location = 29;     wa_pageName = location.pathname;   document.cookie = '__support_check=1;path=/'; wa_hp = 'http'; wa_rf = document.referrer; wa_sr = window.location.search;  wa_tz = new Date(); if (location.href.substr(0, 6).toLowerCase() == 'https:')      wa_hp = 'https'; wa_data = '&an=' + escape(navigator.appName) + '&sr=' + escape(wa_sr) + '&ck=' + document.cookie.length + '&rf=' + escape(wa_rf) + '&sl=' + escape(navigator.systemLanguage) +  '&av=' + escape(navigator.appVersion) + '&l=' + escape(navigator.language) +  '&pf=' + escape(navigator.platform) + '&pg=' + escape(wa_pageName);   wa_data = wa_data + '&cd=' +  screen.colorDepth + '&rs=' + escape(screen.width + ' x ' + screen.height) + '&tz=' + wa_tz.getTimezoneOffset() + '&je=' + navigator.javaEnabled();  wa_img = new Image(); wa_img.src = wa_hp + '://loc1.hitsprocessor.com/statistics.asp' +'?v=1&s=' + wa_location + '&eacct=' + wa_account + wa_data + '&tks=' + wa_tz.getTime(); document.cookie = '__support_check=1;path=/;expires=Thu, 01-Jan-1970 00:00:01 GMT'; document.getElementById('wa_u').src = wa_hp + '://loc1.hitsprocessor.com/track.js'; //]]></script>");
        }

        public string RootPath { get; set; }        
        public string CategorySK { get; set; }
        public string ApiPath { get; set; }
    }
}