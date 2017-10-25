using System;
using IQSDirectory.Helpers;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Web.UI.HtmlControls;
namespace IQSDirectory.Controls
{
    public partial class copro_page_email : System.Web.UI.UserControl
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
                string ClientSk = "57425";
                //string ClientSk = Request.QueryString["ClientSK"].ToString();
                var urlGetId = string.Format("api/Clients/GetClientNameEmailById?ClientSk=" + ClientSk);
                DataTable dtEmail = wHelper.GetDataTableFromWebApi(urlGetId);
                clientEmail = dtEmail.Rows[0]["EMAIL_ADDRESS"].ToString();
                clientName = dtEmail.Rows[0]["NAME"].ToString();
                divEmailCName.InnerHtml = "<h2>Email " + clientName + "</h2>";

                //rootDirPath = ((HtmlInputHidden)this.Parent.FindControl("hdnRootPath")).Value;
                //string attrs = "";
                //sj added javascript validation to button click
                // attrs += " return jqClick();";
                //btnSubmit.Attributes.Add("onClick", attrs);

            }
        }
        
        
    }
}