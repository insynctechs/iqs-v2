//using IQS.Service;
//using IQS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using IQSDirectory.Helpers;
using System.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.IO;
namespace IQSDirectory
{
    public partial class RFQConfirmation : System.Web.UI.Page
    {
        //SELECT TOP(1) * FROM dbo.IQS_TRACKING_SCRIPT ORDER BY SCRIPT_PK;
        //ICategoriesService objICategoriesService;
       // string DirectoryWebSiteURL = System.Configuration.ConfigurationManager.AppSettings["DirectoryWebSiteURL"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string CategorySK = string.Empty;
                    string strRequestIP = string.Empty;
                    //if (DirectoryWebSiteURL.Trim().LastIndexOf("/") != DirectoryWebSiteURL.Trim().Length - 1)
                      //  DirectoryWebSiteURL = DirectoryWebSiteURL + "/";
                    if (Request.QueryString["CategorySK"] != null)
                    {
                        CategorySK = Request.QueryString["CategorySK"].ToString();
                        strRequestIP = Request.QueryString["RequestIP"].ToString();
                        //LoadScript(CategorySK); -- Commented much earlier

                        //LoadTrackingScripts(CategorySK);

                        //lblRequestID.Text = "RequestIP: " + strRequestIP;
                    }
                    else
                    {
                        //lnkBack.Attributes.Add("href", DirectoryWebSiteURL);
                    }
                }

            }
            catch (Exception ex)
            {
                //CommonLogger.Error("RFQConfirmation: Browser--> " + Request.UserAgent.ToString());
                //CommonLogger.Error("RFQConfirmation.Page_Load()", ex);
                throw ex;
            }
            finally
            {
            }
        }

        #region "Load Tracking Scripts"
        private void LoadTrackingScripts(string CategorySK)
        {
            try
            {
                if (CategorySK != "")
                {
                    /*
                    SJ Commented
                    objICategoriesService = DelegateFactory.Current.CategoriesService;
                    DataTable dt = objICategoriesService.GetKeywordInformations(new object[] { CategorySK, "Directory" });
                    dt = objICategoriesService.LoadDisplayName(Convert.ToInt32(CategorySK));
                    */
                    /*DataRow[] dr = dt.Select("META_TAG_ID='OUTBOUNDSCRIPT'");
                    if (dr.Length > 0)
                    {
                        if (dr[0]["DESCRIPTION"].ToString() != null)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "outboundscript", dr[0]["DESCRIPTION"].ToString(), false);
                            //attrs += "outboundTracker(); ";

                            if (dr[0]["DESCRIPTION"].ToString().Contains("msnScript"))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "outboundscript1", "<script language=\"javascript\" type=\"text/javascript\"> msnScript();</script>", false);
                            }
                        }
                    }*/

                    //lnkBack.Attributes.Add("href", DirectoryWebSiteURL + dt.Rows[0]["NAME"].ToString());
                }
            }
            catch
            {
                //Response.Redirect(DirectoryWebSiteURL);
            }
        }
        #endregion

        private void LoadScript(string strCategorySK)
        {
            /*
            DataTable _dtCompanyDetails = null;
            ICategoriesService objICategoriesService;
            try
            {
                objICategoriesService = DelegateFactory.Current.CategoriesService;
                if (strCategorySK != string.Empty && strCategorySK != "")
                {
                    _dtCompanyDetails = objICategoriesService.LoadCompanyDetails(Convert.ToInt32(strCategorySK));
                    if (_dtCompanyDetails.Rows.Count > 0)
                    {
                        pnlOutBound.Controls.Add(new LiteralControl(_dtCompanyDetails.Rows[0]["Outbound"].ToString()));
                        if (_dtCompanyDetails.Rows[0]["Outbound"] != null && _dtCompanyDetails.Rows[0]["Outbound"].ToString() != "")
                        {
                            //Page.RegisterStartupScript("TrackingScript", "<script>javascript:outboundTracker();</script>");
                            if (_dtCompanyDetails.Rows[0]["Outbound"].ToString().Contains("msnScript"))
                            {
                                //Page.RegisterStartupScript("TrackingScript", "<script>javascript:msnScript();</script>");
                            }
                        }
                    }
                }
            }
            catch (BaseException bex)
            {
                //CommonLogger.Error("RFQConfirmation: Browser--> " + Request.UserAgent.ToString() + " CategorySk-->" + strCategorySK);
                //CommonLogger.Error("RFQConfirmation.LoadScript()", bex);

            }
            catch (Exception ex)
            {
                CommonLogger.Error("RFQConfirmation: Browser--> " + Request.UserAgent.ToString() + " CategorySk-->" + strCategorySK);
                CommonLogger.Error("RFQConfirmation.LoadScript()", ex);
                throw new BaseException(ex.Message);
            }
            finally
            {
                objICategoriesService = null;
                _dtCompanyDetails = null;
            }
            */
        }

    }
}