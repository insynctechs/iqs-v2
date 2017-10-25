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

        [WebMethod(EnableSession = true)]
        public static string getloginusername()
        {
            string[] str = { HttpContext.Current.Session["CommenterId"].ToString(), HttpContext.Current.Session["CommenterName"].ToString() };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(str);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public static string checkcommenteractive(List<string> list)
        {
            try
            {
                string UserId = list[0];
                WebApiHelper wHelper = new WebApiHelper();
                var url = string.Format("api/Reviews/GetCommenterActiveValue?UserId=" + UserId + "&json=0");
                int status = wHelper.GetExecuteNonQueryResFromWebApi(url);
                if (status == 0)
                {
                    HttpContext.Current.Session.RemoveAll();
                    HttpContext.Current.Session.Clear();
                    return "Invalid";
                }
                else
                {
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        public static string writereview(List<string> list)
        {
            try
            {
                string CommenterId = list[0];
                string Rating = list[1];
                string Title = list[2];
                string Review = list[3];
                string ClientSk = list[4];
                string rootDirPath = list[5];

                WebApiHelper wHelper = new WebApiHelper();
                var url = string.Format("api/Reviews/GetProfanity?word=" + "&json=0");
                DataTable dtProfanity = wHelper.GetDataTableFromWebApi(url);

                bool wordFound = false;
                string TitlePadded = " " + Title + " ";
                string ReviewPadded = " " + Review + " ";
                foreach (DataRow dr in dtProfanity.Rows)
                {
                    //if (Title.ToLower().Contains(dr["ProfanityWord"].ToString().ToLower()))
                    if (Regex.IsMatch(TitlePadded, @"[^a-zA-Z]" + dr["ProfanityWord"].ToString() + @"[^a-zA-Z]", RegexOptions.Multiline | RegexOptions.IgnoreCase) == true)
                    {
                        wordFound = true;
                        break;
                    }
                    //if (Review.ToLower().Contains(dr["ProfanityWord"].ToString().ToLower()))
                    if (Regex.IsMatch(ReviewPadded, @"[^a-zA-Z]" + dr["ProfanityWord"].ToString() + @"[^a-zA-Z]", RegexOptions.Multiline | RegexOptions.IgnoreCase) == true)
                    {
                        wordFound = true;
                        break;
                    }
                }
                if (wordFound == true)
                {
                    HttpContext.Current.Session.Remove("CommenterId");
                    HttpContext.Current.Session.Remove("CommenterName");
                    url = string.Format("api/Reviews/DisableCommenter?UserId=" + CommenterId + "&json=0");
                    string disable_res = wHelper.GetExecuteNonQueryStringResFromWebApi(url);
                    return "Foul Word";
                }
                else
                {
                    Title = Title.Replace("iqsdirectory.com", "");
                    Review = Review.Replace("iqsdirectory.com", "");
                    url = string.Format("api/Reviews/WriteReview?UserId=" + CommenterId + "&Rating=" + Rating + "&Title="+ Title+"&Review="+ Review + "&Client_SK="+ ClientSk +"&json=0");
                    DataTable dt = wHelper.GetDataTableFromWebApi(url);
                    
                    if (dt == null)
                    {
                        return "Invalid";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        return "Invalid";
                    }
                    else
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<div class='divComments' id='divCommentid'><input type='hidden' id='hdCommentId' value='" + dt.Rows[0]["CommentId"].ToString() + "' />");
                        sb.Append("<input type='hidden' id='hdCommenter' value='" + dt.Rows[0]["CName"].ToString() + "' />");
                        sb.Append("<div class='review_title_wrapper'>");
                        sb.Append("<h2>" + dt.Rows[0]["Title"].ToString() + "</h2>");
                        sb.Append("<div class='review_meta_wrapper'><h3>By <span>" + dt.Rows[0]["CName"].ToString() + "</span>- <span>" + dt.Rows[0]["CDate"].ToString() + "</span></h3></div>");
                        sb.Append("</div>");
                        sb.Append("<span class='review_rating_wrapper'>");
                        sb.Append("<input name='star1' type='radio' class='commentstar" + dt.Rows[0]["CommentId"].ToString() + "' value='1' title='1'/>");
                        sb.Append("<input name='star1' type='radio' class='commentstar" + dt.Rows[0]["CommentId"].ToString() + "' value='2' title='2'/>");
                        sb.Append("<input name='star1' type='radio' class='commentstar" + dt.Rows[0]["CommentId"].ToString() + "' value='3' title='3'/>");
                        sb.Append("<input name='star1' type='radio' class='commentstar" + dt.Rows[0]["CommentId"].ToString() + "' value='4' title='4'/>");
                        sb.Append("<input name='star1' type='radio' class='commentstar" + dt.Rows[0]["CommentId"].ToString() + "' value='5' title='5'/>");
                        sb.Append("</span>");
                        sb.Append("<div style='clear:both;'></div>");
                        sb.Append("<div class='review_content_wrapper'>" + dt.Rows[0]["Review"].ToString() + "</div>");
                        sb.Append("<div id='divCom" + dt.Rows[0]["CommentId"].ToString() + "' class='review_action_wrapper'>");
                        sb.Append("<span class='spnHelpful'>Was this helpful? ");
                        sb.Append("<a class='lnkHelpful' href='#Helpful'>");
                        sb.Append("<img alt='Yes' src='" + rootDirPath + "images/helpful_button.png' >");
                        sb.Append("</a>");
                        sb.Append("</span>");
                        sb.Append("<span class='spnHelpCount' >");
                        sb.Append(dt.Rows[0]["Helpful"].ToString() + "</span><span class='spnHelpCountDesc'>&nbsp;people found this review useful");
                        sb.Append("</span>");
                        sb.Append("<span>");
                        sb.Append("<a class='lnkReply' href='#Reply'>");
                        sb.Append("<img alt='Yes' src='" + rootDirPath + "images/reply_button.png' >");
                        sb.Append("</a>");
                        sb.Append("</span>");
                        sb.Append("</div>");
                        sb.Append("<script language='javascript' type='text/javascript'>");
                        sb.Append("$('input[type=radio].commentstar" + dt.Rows[0]["CommentId"].ToString() + "').rating({");
                        sb.Append("required: true");
                        sb.Append("});");
                        //int Rating = Convert.ToInt16(dt.Rows[0]["Rating"].ToString());
                        //if (RatingReceived > 0)
                        sb.Append("$('input[type=radio].commentstar" + dt.Rows[0]["CommentId"].ToString() + "').rating('select', " + (Convert.ToInt16(dt.Rows[0]["Rating"].ToString()) - 1).ToString() + ", false);");
                        sb.Append("$('input[type=radio].commentstar" + dt.Rows[0]["CommentId"].ToString() + "').rating('disable');");
                        sb.Append("</script>");
                        //if(RatingReceived>0)
                        sb.Append("</div>");
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        string json = jss.Serialize(sb.ToString());
                        string clientEmail = "";
                        if (dt.Rows[0]["NOTIFY_CLIENTS"].ToString() == "Y")
                        {
                            if (dt.Rows[0]["EMAIL_ADDRESS"].ToString() != "" && dt.Rows[0]["EMAIL_ADDRESS"].ToString() != "N/A")
                            {
                                clientEmail = dt.Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }
                        string mailStat = SendReviewMail(dt.Rows[0]["CName"].ToString(), dt.Rows[0]["Email"].ToString(), dt.Rows[0]["CDate"].ToString(), dt.Rows[0]["Rating"].ToString(), dt.Rows[0]["Title"].ToString(), dt.Rows[0]["Review"].ToString(), dt.Rows[0]["NAME"].ToString(), clientEmail);
                        return json;
                    }
                }
                                
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
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
                var url = string.Format("api/Reviews/GetCommentersLogin?Email=" + Email + "&Password=" + Password + "&json=0");
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


        public static string SendReviewMail(string CName, string CEmail, string CDate, string Rating, string Title, string Review, string Company, string ClientEmail)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine("Hi, ");
                sb.AppendLine("<br/>");
                sb.AppendLine("New review for " + Company);
                sb.AppendLine("<br/>");
                sb.AppendLine("Posted by : " + CName + " (" + CEmail + ")");
                sb.AppendLine("<br/>");
                sb.AppendLine("Posted date : " + CDate);
                sb.AppendLine("<br/>");
                sb.AppendLine("Title : " + Title);
                sb.AppendLine("<br/>");
                sb.AppendLine("Rating : " + Rating);
                sb.AppendLine("<br/>");
                sb.AppendLine("Review : " + Review);
                sb.AppendLine("<br/><br/>");
                sb.AppendLine("Thanks and Regards");
                sb.AppendLine("<br/>");
                sb.AppendLine("IQS Directory Administrator");
                
                string _fromAddress = System.Configuration.ConfigurationManager.AppSettings["ReviewUserRegisterMailID"].ToString();
                string _toAddress = System.Configuration.ConfigurationManager.AppSettings["ReviewUserRegisterTo"].ToString();
                string _ccAddress = System.Configuration.ConfigurationManager.AppSettings["ReviewUserRegisterCC"].ToString();
                string _bccAddress = System.Configuration.ConfigurationManager.AppSettings["ReviewUserRegisterBCC"].ToString();
                Utils.SendMail(_fromAddress, _toAddress, _ccAddress, _bccAddress, "[IQS DIRECTORY] COMPANY PROFILE - NEW REVIEW POSTED", sb.ToString(), true);
                //IQS.Utility.Utils.SendMail(_fromAddress, "njerry@iforceproservices.com", "", "njerry@iforceproservices.com,mbbinil@iforceproservices.com", "[IQS DIRECTORY] COMPANY PROFILE - NEW REVIEW POSTED", sb.ToString(), true);
                if (ClientEmail != "")
                    Utils.SendMail(_fromAddress, ClientEmail, "", "", "[IQS DIRECTORY] COMPANY PROFILE - NEW REVIEW POSTED", sb.ToString(), true);
                //Utils.SendMail(_fromAddress, ClientEmail, "", "", "[IQS DIRECTORY] COMPANY PROFILE - NEW REVIEW POSTED", sb.ToString(), true);
                return "mail sent";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}