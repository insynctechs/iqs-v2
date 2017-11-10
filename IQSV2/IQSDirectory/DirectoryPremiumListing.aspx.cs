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

    }
}