using System;
using IQSDirectory.Helpers;
using System.Data;
using System.Net.Mail;
using System.Text;

namespace IQSDirectory.Controls
{
    public partial class copro_page_email : System.Web.UI.UserControl
    {
        WebApiHelper wHelper = new WebApiHelper();
        string clientEmail = "";
        string clientName = "";

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
                string attrs = "";
                //sj added javascript validation to button click
                attrs += " return jqClick();";
                btnSubmit.Attributes.Add("onClick", attrs);

            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (Utils.isvalidIpAccess() == true)
                {
                    string FirstName = Request.Form["txtFirstName"].ToString();
                    string LastName = Request.Form["txtLastName"].ToString();
                    string EmailAddress = Request.Form["txtEmailAddress"].ToString();
                    string Zip = Request.Form["txtZip"].ToString();
                    string Subject = Request.Form["txtSubject"].ToString();
                    string Message = Request.Form["txtMessage"].ToString();
                    string _RequestIP = System.Web.HttpContext.Current.Request.UserHostAddress;

                    StringBuilder strEmailContent = new StringBuilder();
                    strEmailContent.AppendLine("<table width='100%' align='center'>");
                    strEmailContent.AppendLine("<tr>");
                    strEmailContent.AppendLine("<td align='left' width='20%'><b>Address:</b>");
                    strEmailContent.AppendLine("</td>");
                    strEmailContent.AppendLine("<td align='left' width='80%'>");
                    strEmailContent.AppendLine(FirstName + " " + LastName + ", " + EmailAddress + ", " + Zip + " <BR>");
                    strEmailContent.AppendLine("</td></tr>");
                    strEmailContent.AppendLine("<tr>");
                    strEmailContent.AppendLine("<td align='left' width='20%'><b>Subject:</b>");
                    strEmailContent.AppendLine("</td>");
                    strEmailContent.AppendLine("<td align='left' width='80%'>");
                    strEmailContent.AppendLine(Subject + "<BR>");
                    strEmailContent.AppendLine("</td></tr>");

                    strEmailContent.AppendLine("<tr>");
                    strEmailContent.AppendLine("<td align='left' width='20%'><b>Message:</b>");
                    strEmailContent.AppendLine("</td>");
                    strEmailContent.AppendLine("<td align='left' width='80%'>");
                    strEmailContent.AppendLine(Message + "<BR>");
                    strEmailContent.AppendLine("</td></tr>");


                    strEmailContent.AppendLine("<tr>");
                    strEmailContent.AppendLine("<td align='left' width='20%'><b>Additional Info:</b>");
                    strEmailContent.AppendLine("</td>");
                    strEmailContent.AppendLine("<td align='left' width='80%'>");

                    strEmailContent.AppendLine("<tr>");
                    strEmailContent.AppendLine("<td align='left' width='20%'><b>RequestIP:</b>");
                    strEmailContent.AppendLine("<td>");
                    strEmailContent.AppendLine(_RequestIP);//changes on FEB/17/2010 to display the Request host IP
                    strEmailContent.AppendLine("</td>");
                    strEmailContent.AppendLine("</tr>");

                    strEmailContent.AppendLine("<tr>");
                    strEmailContent.AppendLine("<td align='left' width='100%' colspan='2'><br>");
                    strEmailContent.AppendLine("Best Regards,<br>");
                    //strEmailContent.AppendLine(System.Configuration.ConfigurationManager.AppSettings["IQSEmployeeName"].ToString() + "<br>");
                    strEmailContent.AppendLine(FirstName + " " + LastName + "<br>");
                    strEmailContent.AppendLine("</td>");
                    strEmailContent.AppendLine("</tr>");
                    strEmailContent.AppendLine("</table>");

                    string _toAddress = string.Empty;
                    string _ccAddress = string.Empty;
                    string _FromAddress = string.Empty;
                    string _Subject = string.Empty;
                    _FromAddress = EmailAddress;
                    if (clientEmail != null && clientEmail != "N/A")
                    {
                        _toAddress = clientEmail;
                        _ccAddress = wHelper.ProfileCCEmailAddress; //System.Configuration.ConfigurationManager.AppSettings["ProfileCCEmailAddress"].ToString();
                        _Subject = wHelper.ProfileEmailSubject; // System.Configuration.ConfigurationManager.AppSettings["ProfileEmailSubject"].ToString();
                        //CommonLogger.Info("Sending mail for Directory Profile PageEmail: " + "From mail id: " + EmailAddress + "To Mail Id: " + dsEmail.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString() + "CC Mail Id: " + System.Configuration.ConfigurationManager.AppSettings["ProfileCCEmailAddress"] + "Mail Server IP: " + System.Configuration.ConfigurationManager.AppSettings["MailServerIP"]);
                    }
                    else
                    {
                        _toAddress = wHelper.ProfileCCEmailAddress; //System.Configuration.ConfigurationManager.AppSettings["ProfileCCEmailAddress"].ToString();
                        _Subject = wHelper.ProfileNonExistEmailSubject; //System.Configuration.ConfigurationManager.AppSettings["ProfileNonExistEmailSubject"].ToString();
                        //CommonLogger.Info("Sending mail for Directory Profile PageEmail: " + "From mail id: " + EmailAddress + "To Mail Id: " + System.Configuration.ConfigurationManager.AppSettings["ProfileCCEmailAddress"].ToString() + "CC Mail Id: " + System.Configuration.ConfigurationManager.AppSettings["ProfileCCEmailAddress"] + "Mail Server IP: " + System.Configuration.ConfigurationManager.AppSettings["MailServerIP"]);
                    }
                    Utils.SendMail("sumi@insynctechs.com", "sumi@insynctechs.com,linda@insynctechs.com", "sumiajit@gmail.com",string.Empty, _Subject, strEmailContent.ToString(), true);
                    //Utils.SendMail(_FromAddress, _toAddress, _ccAddress, "", _Subject, strEmailContent.ToString(), true);
                    divStatus.InnerHtml = "Email sent succesfully";

                    txtFirstName.Value = "";
                    txtLastName.Value = "";
                    txtEmailAddress.Value = "";
                    txtCompanyName.Value = "";
                    txtZip.Value = "";
                    txtSubject.Value = "";
                    txtMessage.Value = "";
                    Response.Write("<script>window.open('copro-page-email-thanku.aspx', 'popup', 'top=0,left=0,width=500,height=400,scrollbars=no,menubar=no,toolbar=no,resizable=no,location=no,titlebar=no');</script>");

                }
                else  //invalid ip access
                {
                    //ip_error.InnerText = "The Use of this Form is Restricted - Please Contact IQSDirectory with Questions.";
                    ip_error.InnerHtml = "The Use of this Form is Restricted - Please Contact IQSDirectory with Questions.";

                }
            }
            catch (Exception ex)
            {
                /* CommonLogger.Error("DirectoryRFQ: Browser--> " + Request.UserAgent.ToString() + " CategorySk-->" + hdnCategorySK.Value + " ClientSK-->" + hdnRFQClientSK.Value);
                 CommonLogger.Error("sendMail", ex);
                 throw new BaseException(ex.Message);*/
            }
            finally
            {
            }
        }
        
    }
}