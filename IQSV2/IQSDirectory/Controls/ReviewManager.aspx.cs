using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.Script.Serialization;
using System.Net.Mail;
using System.Text.RegularExpressions;
using IQSDirectory.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IQSDirectory
{
    public partial class ReviewManager : System.Web.UI.Page
    {

        public class RootObject
        {
            public List<string> list { get; set; }
            public string doaction { get; set; }
            public string returntype { get; set; }
        }

        WebApiHelper wHelper = new WebApiHelper();

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod(EnableSession = true)]
        public static string checkloginstate()
        {
            if (HttpContext.Current.Session["CommenterId"] == null)
                return "false";
            else
                return "true";
        }

        [WebMethod(EnableSession = true)]
        public static string logoutsession()
        {
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Clear();
            return "success";
        }

        [WebMethod(EnableSession = true)]
        public static string registercommenter(List<string> list)
        {
            try
            {
                /*string DesiredName = list[0];
                string FullName = list[1];
                string Email = list[2];
                string Password = list[3];*/
                //WebApiHelper wHelper = new WebApiHelper();
                

                //var url = string.Format("api/Reviews/InsertCommenter?DesiredName=" + DesiredName + "&FullName=" + FullName + "&Email=" + Email + "&Password=" + Password + "&SystemIp=" + HttpContext.Current.Request.UserHostAddress + "&Active=1");

                /*
                var url = string.Format("api/Reviews/InsertCommenter?DesiredName=" + DesiredName + "&FullName=" + FullName + "&Email=" + Email + "&Password=" + Password + "&SystemIp=" + HttpContext.Current.Request.UserHostAddress + "&Active=1");
                string RetVal = "1";// wHelper.GetExecuteNonQueryStringResFromWebApi(url);
                if (RetVal == "Success")
                {
                    string mailStat = SendRegistrationMail(FullName, Email, Password);
                    //string ipStat = objCommentService.InsertSystemIp(new object[] { HttpContext.Current.Request.UserHostAddress });
                }
                return RetVal;*/
                //HttpContext.Current.Response.Write("Test");
                return "test";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        

        public static string SendRegistrationMail(string FullName, string toEmail, string Password)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine("Dear " + FullName + ", ");
                sb.AppendLine("<br/><br/>");
                sb.AppendLine("Thank you for registering with IQS.");
                sb.AppendLine("<br/><br/>");
                sb.AppendLine("Your login details for posting comments and reviews with us are: ");
                sb.AppendLine("<br/>");
                sb.AppendLine("Email address: " + toEmail + "<br/>");
                sb.AppendLine("Password: " + Password + "<br/>");
                sb.AppendLine("<br/>");
                sb.AppendLine("Please keep this email as it contains your login details.");
                sb.AppendLine("<br/><br/>");

                sb.AppendLine("Thanks and Regards");
                sb.AppendLine("<br/>");
                sb.AppendLine("IQS Directory Administrator");
                string _fromAddress = System.Configuration.ConfigurationManager.AppSettings["ReviewUserRegisterMailID"].ToString();
                string _ccAddress = System.Configuration.ConfigurationManager.AppSettings["ReviewUserRegisterTo"].ToString();
                Utils.SendMail(_fromAddress, toEmail, _ccAddress, string.Empty, "[IQS DIRECTORY] COMPANY PROFILE REVIEW - USER REGISTRATION", sb.ToString(), true);
                //IQS.Utility.Utils.SendMail(_fromAddress, toEmail, "njerry@iforceproservices.com", string.Empty, "[IQS DIRECTORY] COMPANY PROFILE REVIEW - USER REGISTRATION", sb.ToString(), true);
                return sb.ToString();
                
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        public static string userlogin(List<string> list)
        {
            try
            {
                //RootObject obj = JsonConvert.DeserializeObject<RootObject>(jData.ToString());
                //object[] data = obj.list.ToArray();
                
                string Email = list[0].ToString();
                string Password = list[1].ToString();
                WebApiHelper wHelper = new WebApiHelper();
                var url = string.Format("api/Reviews/CommentersLogin?Email=" + Email + "&Password=" + Password + "&json=0");
                DataSet ds = wHelper.GetDataSetFromWebApi(url);
                //return "Dataset";
                if (ds != null)
                {
                    if (ds.Tables.Count == 0)
                    {
                        return "Invalid";
                    }
                    else if (ds.Tables[0].Rows.Count == 0)
                    {
                        return "Invalid";
                    }
                    else
                    {
                        HttpContext.Current.Session["CommenterId"] = ds.Tables[0].Rows[0]["UserId"].ToString();
                        HttpContext.Current.Session["CommenterName"] = ds.Tables[0].Rows[0]["DesiredName"].ToString();
                        return "Success";
                    }
                    
                }
                return "Invalid";
            }
            catch (Exception ex)
            {
                return "error";// ex.Message.ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        public string copropageemail(List<string> list)
        {
        
             try
                {
              

                if (Utils.isvalidIpAccess()== true )
                {
                    
                    string FirstName = list[0];
                    string LastName = list[1];
                    string EmailAddress = list[2];
                    string CompanyName = list[3];
                    string Zip = list[4];
                    string Subject = list[5];
                    string Message = list[6];
                    string ClientSk = list[7];
                   

                    string _RequestIP = System.Web.HttpContext.Current.Request.UserHostAddress;

                    //insert profile request into form
                    var urlGetId = string.Format("api/Clients/InsertDirectoryProfileEmailDetails?FirstName=" + FirstName+"&LastName="+LastName+"&EmailAddress="+EmailAddress+"&CompanyName="+CompanyName+"&Zip="+Zip+"&Subject="+Subject+"&Message="+Message+"&ClientSk="+ClientSk+"&RequestIp="+_RequestIP);
                    int intIsSucess = wHelper.GetExecuteNonQueryResFromWebApi(urlGetId);

                    if (intIsSucess != 0)
                    {
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
                        /*
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
                        */
                        Utils.SendMail("sumi@insynctechs.com", "sumi@insynctechs.com,linda@insynctechs.com", "sumiajit@gmail.com", string.Empty, _Subject, strEmailContent.ToString(), true);
                        //Utils.SendMail(_FromAddress, _toAddress, _ccAddress, "", _Subject, strEmailContent.ToString(), true);
                        return "Success";
                    }
                    else
                        return "Error";
                }
                else  //invalid ip access
                {
                    //ip_error.InnerText = "The Use of this Form is Restricted - Please Contact IQSDirectory with Questions.";
                    //ip_error.InnerHtml = "The Use of this Form is Restricted - Please Contact IQSDirectory with Questions.";
                    return "Country";

                }
            }
            catch (Exception ex)
            {
                /* CommonLogger.Error("DirectoryRFQ: Browser--> " + Request.UserAgent.ToString() + " CategorySk-->" + hdnCategorySK.Value + " ClientSK-->" + hdnRFQClientSK.Value);
                    CommonLogger.Error("sendMail", ex);
                    throw new BaseException(ex.Message);*/
                return "Error";
            }
            finally
            {
               
            }
        }
    }
}