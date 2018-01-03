﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
namespace IQSDirectory
{
    public partial class DirectoryPremiumListingThankYou : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hdnCategoryname.Value = Request.QueryString["CategoryName"];
                hdnCompanyName.Value = Request.QueryString["CompanyName"];
                hdnCompanyPhone.Value = Request.QueryString["CompanyPhone"];
                hdnCompanyWebsite.Value = Request.QueryString["CompanyWebsite"];
                hdnContactname.Value = Request.QueryString["ContactName"];
                hdnContacttitle.Value = Request.QueryString["ContactTitle"];
                hdnTotalAmount.Value = Request.QueryString["Amount"];
                //GetCategory();
                GenerateMetaTagsAndScripts();
                this.Master.BindMeta();
                LoadDetails();

            }
            catch (Exception ex)
            {
                //CommonLogger.Error(ex.Message);
                //throw new BaseException(ex.Message);
            }
        }

        #region " Load Details "
        public void LoadDetails()
        {
            try
            {
                string _strMail = System.Configuration.ConfigurationManager.AppSettings["ListYourCompanyToMailID"];
                string _strDateFormat = DateTime.Now.ToString("D");
                string _strTime = DateTime.Now.ToString("T");
                lblHeading.Text = "Below is what you submitted to " + _strMail + " on " + _strDateFormat + " at " + _strTime;
                lblCategoryName.Text = hdnCategoryname.Value.ToString();
                lblCompanyName.Text = hdnCompanyName.Value.ToString();
                lblCompanyPhone.Text = hdnCompanyPhone.Value.ToString();
                lblCompanyWebsite.Text = hdnCompanyWebsite.Value.ToString();
                lblContactname.Text = hdnContactname.Value.ToString();
                lblContacttitle.Text = hdnContacttitle.Value.ToString();
                if (hdnTotalAmount.Value.ToString() != "")
                {
                    trAmount.Visible = true;
                    lblTotalAmount.Text = hdnTotalAmount.Value;
                }
            }
            catch (Exception ex)
            {
                //CommonLogger.Error(ex.Message);
                //throw new BaseException(ex.Message);
            }
            finally
            {
            }
        }
        #endregion

        private void GenerateMetaTagsAndScripts()
        {
            this.Master.PageIndex = new HtmlString("<meta name='robots' content='index,follow'>");
            this.Master.PageDescription = "";
            this.Master.PageTitle = "IQSDirectory | Thanks for Listing";
            this.Master.PageKeywords = "";
            this.Master.HeadScript = new HtmlString("<!-- Google Tag Manager --><script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':new Date().getTime(),event:'gtm.js'});var f = d.getElementsByTagName(s)[0], j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async=true;j.src='https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j, f);})(window, document,'script','dataLayer','GTM-NGZWMKN');</script><!-- End Google Tag Manager -->");
            this.Master.BodyOpenScript = new HtmlString("<!-- Google Tag Manager (noscript) --><noscript><iframe src = 'https://www.googletagmanager.com/ns.html?id=GTM-NGZWMKN' height='0' width='0' style ='display:none;visibility:hidden' ></iframe ></noscript ><!--End Google Tag Manager(noscript)--> ");
            this.Master.HitsLinkScript = new HtmlString("<!-- Industrial Quick Search Referring Site Stats web tools statistics hit counter code --><script type = 'text/javascript' id = 'wa_u' ></script><script type='text/javascript' >//<![CDATA[ wa_account = '968E8C9B968D9A9C8B908D86'; wa_location = 29;     wa_pageName = location.pathname;   document.cookie = '__support_check=1;path=/'; wa_hp = 'http'; wa_rf = document.referrer; wa_sr = window.location.search;  wa_tz = new Date(); if (location.href.substr(0, 6).toLowerCase() == 'https:')      wa_hp = 'https'; wa_data = '&an=' + escape(navigator.appName) + '&sr=' + escape(wa_sr) + '&ck=' + document.cookie.length + '&rf=' + escape(wa_rf) + '&sl=' + escape(navigator.systemLanguage) +  '&av=' + escape(navigator.appVersion) + '&l=' + escape(navigator.language) +  '&pf=' + escape(navigator.platform) + '&pg=' + escape(wa_pageName);   wa_data = wa_data + '&cd=' +  screen.colorDepth + '&rs=' + escape(screen.width + ' x ' + screen.height) + '&tz=' + wa_tz.getTimezoneOffset() + '&je=' + navigator.javaEnabled();  wa_img = new Image(); wa_img.src = wa_hp + '://loc1.hitsprocessor.com/statistics.asp' +'?v=1&s=' + wa_location + '&eacct=' + wa_account + wa_data + '&tks=' + wa_tz.getTime(); document.cookie = '__support_check=1;path=/;expires=Thu, 01-Jan-1970 00:00:01 GMT'; document.getElementById('wa_u').src = wa_hp + '://loc1.hitsprocessor.com/track.js'; //]]></script>");
        }
    }
}