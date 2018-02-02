using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.UI;

namespace IQSDirectory
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ApiPath = ConfigurationManager.AppSettings["Api"].ToString();
                WebURL = ConfigurationManager.AppSettings["WebURL"].ToString();
            }
        }
        
        public void BindMeta()
        {
            System.Text.StringBuilder strMetaTag = new System.Text.StringBuilder();
            strMetaTag.AppendFormat(@"<meta content='{0}' name='description'/>", PageDescription);
            strMetaTag.Append(Environment.NewLine);
            strMetaTag.AppendFormat(@"<meta content='{0}' name='keywords'/>", PageKeywords);
            strMetaTag.Append(Environment.NewLine);
            strMetaTag.AppendFormat(@"<meta content='{0}' name='og:title'/>", PageTitle);
            strMetaTag.Append(Environment.NewLine);
            strMetaTag.AppendFormat(@"<meta content='{0}' name='og:description'/>", PageDescription);
            strMetaTag.Append(Environment.NewLine);
            strMetaTag.AppendFormat(@"<meta content='{0}' name='og:image'/>", WebURL + "images/iqs_logo.gif");
            strMetaTag.Append(Environment.NewLine);
            strMetaTag.AppendFormat(@"<meta content='{0}' name='og:image:type'/>", "image/gif");
            strMetaTag.Append(Environment.NewLine);
            strMetaTag.AppendFormat(@"<meta content='{0}' name='og:image:width'/>", "348");
            strMetaTag.Append(Environment.NewLine);
            strMetaTag.AppendFormat(@"<meta content='{0}' name='og:image:height'/>", "79");
            strMetaTag.Append(Environment.NewLine);
            PageMeta = strMetaTag.ToString();
        }

        public string ApiPath { get; set; }
        public string WebURL { get; set; }
        public string PageTitle { get; set; }
        public string PageDescription { get; set; }
        public string PageMeta { get; set; }
        public string PageKeywords { get; set; }
        public IHtmlString HitsLinkScript { get; set; }
        public IHtmlString PageIndex { get; set; }
        public IHtmlString HeadScript { get; set; }
        public IHtmlString BodyOpenScript { get; set; }
        public IHtmlString BodyCloseScript { get; set; }
        public IHtmlString BodyAfterCloseScript { get; set; }


    }
}