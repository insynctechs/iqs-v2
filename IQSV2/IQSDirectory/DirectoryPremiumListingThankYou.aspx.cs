using System;
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
    }
}