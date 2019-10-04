using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using IQSDirectory.Helpers;
using System.Data;

namespace IQSDirectory.Controls
{
    public partial class copro_page_email_v1 : System.Web.UI.UserControl
    {
        WebApiHelper wHelper = new WebApiHelper();
        string clientEmail = "";
        string clientName = "";
        public string RootPath { get; set; }
        public static string rootDirPath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)             
            {
                //string ClientSk = "57425";
                //string ClientSk = Request.QueryString["ClientSK"].ToString();
                string url = HttpContext.Current.Request.Url.AbsolutePath;
                if (url.IndexOf("/", url.Length - 1) > -1)
                {
                    url = url.Remove(url.Length - 1);
                }
                string copro = url.Split('/').Last();
                string ClientSk = copro.Substring(copro.LastIndexOf('-')).Trim('-');
                var urlGetId = string.Format("api/Clients/GetClientNameEmailById?ClientSk=" + ClientSk);
                DataTable dtEmail = wHelper.GetDataTableFromWebApi(urlGetId);
                clientEmail = dtEmail.Rows[0]["EMAIL_ADDRESS"].ToString();
                clientName = dtEmail.Rows[0]["NAME"].ToString();
                divEmailCName.InnerHtml = "Email " + clientName + "";

                //rootDirPath = ((HtmlInputHidden)this.Parent.FindControl("hdnRootPath")).Value;
                //string attrs = "";
                //sj added javascript validation to button click
                // attrs += " return jqClick();";
                //btnSubmit.Attributes.Add("onClick", attrs);

            }
        }
        
        
    }
}