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
using System.Net;
using System.Linq;

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
                string DesiredName = list[0];
                string FullName = list[1];
                string Email = list[2];
                string Password = list[3];

                WebApiHelper wHelper = new WebApiHelper();       
                var url = string.Format("api/Reviews/InsertCommenter?DesiredName=" + DesiredName + "&FullName=" + FullName + "&Email=" + Email + "&Password=" + Password + "&SystemIp=" + HttpContext.Current.Request.UserHostAddress + "&Active=1");
                //return url;
                string RetVal = wHelper.GetExecuteNonQueryStringResFromWebApi(url);
                if (RetVal == "Success")
                {
                    string mailStat = SendRegistrationMail(FullName, Email, Password);
                    //string ipStat = objCommentService.InsertSystemIp(new object[] { HttpContext.Current.Request.UserHostAddress });
                }
                return RetVal;
                
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
                        
                        string JSONString = JsonConvert.SerializeObject(dt);
                        string clientEmail = "";
                        if (dt.Rows[0]["NOTIFY_CLIENTS"].ToString() == "Y")
                        {
                            if (dt.Rows[0]["EMAIL_ADDRESS"].ToString() != "" && dt.Rows[0]["EMAIL_ADDRESS"].ToString() != "N/A")
                            {
                                clientEmail = dt.Rows[0]["EMAIL_ADDRESS"].ToString();
                            }
                        }
                        string mailStat = SendReviewMail(dt.Rows[0]["CName"].ToString(), dt.Rows[0]["Email"].ToString(), dt.Rows[0]["CDate"].ToString(), dt.Rows[0]["Rating"].ToString(), dt.Rows[0]["Title"].ToString(), dt.Rows[0]["Review"].ToString(), dt.Rows[0]["NAME"].ToString(), clientEmail);
                        return JSONString;
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
        public static string fbuserlogin(List<string> list)
        {
            try
            {
                string Email = list[0];
                string UName = list[1];
                string UId = list[2];
                WebApiHelper wHelper = new WebApiHelper();
                var url = string.Format("api/Reviews/GetFBCommentersLogin?DesiredName="+UName+"&FullName="+UId+"&Email="+Email+"&Password=&SystemIp="+ HttpContext.Current.Request.UserHostAddress + "&Active=1");
                DataSet ds = wHelper.GetDataSetFromWebApi(url);
                if (ds.Tables.Count == 0)
                {
                    return "Invalid";
                }
                else
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        return "Invalid";
                    }
                    else
                    {
                        if (Convert.ToBoolean(dt.Rows[0]["Active"]) == true)
                        {
                            HttpContext.Current.Session["CommenterId"] = dt.Rows[0]["UserId"].ToString();
                            HttpContext.Current.Session["CommenterName"] = dt.Rows[0]["DesiredName"].ToString();
                            HttpContext.Current.Session["FBStatus"] = "1";
                            return "Success";
                        }
                        else
                        {
                            return "InActive";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        public static string writereviewreply(List<string> list)
        {
            try
            {
                string CommenterId = list[0];
                string CommentId = list[1];
                string SubReview = list[2];
                string CommentType = list[3];
                string ReplyTo = list[4];
                WebApiHelper wHelper = new WebApiHelper();
                var url = string.Format("api/Reviews/GetProfanity?word=" + "&json=0");
                DataTable dtProfanity = wHelper.GetDataTableFromWebApi(url);
                
                bool wordFound = false;
                string SubReviewPadded = " " + SubReview + " ";
                foreach (DataRow dr in dtProfanity.Rows)
                {
                    //if (SubReview.ToLower().Contains(dr["ProfanityWord"].ToString().ToLower()))
                    if (Regex.IsMatch(SubReviewPadded, @"[^a-zA-Z]" + dr["ProfanityWord"].ToString() + @"[^a-zA-Z]", RegexOptions.Multiline | RegexOptions.IgnoreCase) == true)
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
                SubReview = SubReview.Replace("iqsdirectory.com", "");
                url = string.Format("api/Reviews/WriteReviewReply?UserId=" + CommenterId + "&CommentId=" + CommentId + " &Review=" + SubReview + "&CommentType=" + CommentType + "&json=0");
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
                    /*System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    
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
                    string mailStat = SendReviewReplyMail(dt.Rows[0]["CName"].ToString(), dt.Rows[0]["Email"].ToString(), dt.Rows[0]["SCDate"].ToString(), dt.Rows[0]["Review"].ToString(), dt.Rows[0]["NAME"].ToString(), clientEmail, ReplyTo);
                    //return json;
                    return sb.ToString();*/
                    string JSONString = JsonConvert.SerializeObject(dt);
                    string clientEmail = "";
                    if (dt.Rows[0]["NOTIFY_CLIENTS"].ToString() == "Y")
                    {
                        if (dt.Rows[0]["EMAIL_ADDRESS"].ToString() != "" && dt.Rows[0]["EMAIL_ADDRESS"].ToString() != "N/A")
                        {
                            clientEmail = dt.Rows[0]["EMAIL_ADDRESS"].ToString();
                        }
                    }
                    string mailStat = SendReviewReplyMail(dt.Rows[0]["CName"].ToString(), dt.Rows[0]["Email"].ToString(), dt.Rows[0]["SCDate"].ToString(), dt.Rows[0]["Review"].ToString(), dt.Rows[0]["NAME"].ToString(), clientEmail, ReplyTo);
                    return JSONString;
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        public static string fetchcomments(List<string> list)
        {
            try
            {
                string clientsk = list[0];
                string lastid = list[1];

                WebApiHelper wHelper = new WebApiHelper();
                var url = string.Format("api/Reviews/GetReviews?Client_SK=" + clientsk + "&LastCommentId=" + lastid + "&json=0");
                DataSet ds = wHelper.GetDataSetFromWebApi(url);
                if (ds == null)
                {
                    return "Invalid";
                }
                else if (ds.Tables.Count == 0)
                {
                    return "LastRecord";
                }
                else if (ds.Tables[0].Rows.Count == 0)
                {
                    return "LastRecord";
                }
                else
                {
                    DataTable dt = ds.Tables[0];
                    string JSONString = JsonConvert.SerializeObject(dt);
                    return JSONString;                    
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        

        

        [WebMethod(EnableSession = true)]
        public static string fetchsubcomments(List<string> list)
        {
            try
            {
                string CommentId = list[0];
               
                List<object> CommentObj = new List<object>();

                WebApiHelper wHelper = new WebApiHelper();
                var url = string.Format("api/Reviews/GetReviewReplies?CommentId=" + CommentId + "&json=0");
                DataSet ds = wHelper.GetDataSetFromWebApi(url);
                if (ds == null)
                {
                    return "Invalid";
                }
                else if (ds.Tables.Count == 0)
                {
                    return "LastRecord";
                }
                else if (ds.Tables[0].Rows.Count == 0)
                {
                    return "LastRecord";
                }
                else
                {
                    DataTable dt = ds.Tables[0];
                    string JSONString = JsonConvert.SerializeObject(dt);
                    return JSONString;
                }                

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        

        [WebMethod(EnableSession = true)]
        public static string getcompanytotalrating(List<string> list)
        {
            try
            {
                string ClientSK = list[0];
                WebApiHelper wHelper = new WebApiHelper();
                var url = string.Format("api/Reviews/GetCompanyTotalRating?ClientSK=" + ClientSK  + "&json=0");
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
                    string[] str = { (Convert.ToInt16(dt.Rows[0][0].ToString()) - 1).ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString() };
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    string json = jss.Serialize(str);
                    return json;
                }
                //return "totalrate";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        public static string getcompanytotalratingbyarray(List<string> list)
        {
            try
            {
                string ClientSK = list[0];
                WebApiHelper wHelper = new WebApiHelper();
                var url = string.Format("api/Reviews/GetCompanyRatingByArray?ClientSkArray=" + ClientSK + "&json=0");
                DataTable dt = wHelper.GetDataTableFromWebApi(url); 
                if(dt == null)
                {
                    return "Invalid";
                }
                else if (dt.Rows.Count == 0)
                {
                    return "Invalid";
                }
                else
                {
                    List<object> lo = new List<object>();
                    foreach (DataRow dr in dt.Rows)
                        lo.Add(new object[] { dr[0].ToString(), (Convert.ToInt16(dr[1].ToString()) - 1).ToString(), dr[2].ToString() });
                    return JsonConvert.SerializeObject(lo);
                    /*JavaScriptSerializer jss = new JavaScriptSerializer();
                    string json = jss.Serialize(lo);
                    return json;*/
                }

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        public static string submithelpful(List<string> list)
        {
            try
            {
                string CommentId = list[0];
                WebApiHelper wHelper = new WebApiHelper();
                var url = string.Format("api/Reviews/TagReviewHelpful?CommentId=" + CommentId + "&json=0");
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
                    return dt.Rows[0][0].ToString();
                }
                
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        public static string updatereviewrating(List<string> list)
        {
            try
            {
                string CommentId = list[0];
                string RateReceived = list[1];
                WebApiHelper wHelper = new WebApiHelper();
                var url = string.Format("api/Reviews/UpdateReviewRating?CommentId=" + CommentId + "&Rate=" + RateReceived+ "&json=0");
                string res= wHelper.GetExecuteNonQueryStringResFromWebApi(url);
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        public static string userforgotpassword(List<string> list)
        {
            try
            {
                string Email = list[0];
                WebApiHelper wHelper = new WebApiHelper();
                var url = string.Format("api/Reviews/GetCommenterByEmail?Email=" + Email + "&json=0");
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
                    string mailStat = SendForgotPasswordMail(dt.Rows[0]["FullName"].ToString(), Email, dt.Rows[0]["Password"].ToString());
                    return "Success";
                }

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        
        [WebMethod(EnableSession = true)]
        public static string sendcoproemail(List<string> list)
        {            
             try
                {
                WebApiHelper wHelper = new WebApiHelper();                   
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

                        urlGetId = string.Format("api/Clients/GetClientNameEmailById?ClientSk=" + ClientSk );
                        DataTable dt = wHelper.GetDataTableFromWebApi(urlGetId);
                        string clientEmail = dt.Rows[0]["EMAILADDRESS"].ToString();

                        clientEmail = "";

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
                        
                        //bool mailstatus = Utils.SendMail("admin@industrialquicksearch.com", "sumi@insynctechs.com", "linda@insynctechs.com", string.Empty, _Subject, strEmailContent.ToString(), true);
                        bool mailstatus = Utils.SendMail(_FromAddress, _toAddress, _ccAddress, "", _Subject, strEmailContent.ToString(), true);
                        if (mailstatus == true)
                             return "Success";
                         else
                             return "MailError";
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
                return "Error1";
            }
            finally
            {
               
            }
            
        }

        [WebMethod(EnableSession = true)]
        public static string coprosharepage_email(List<string> list)
        {
            try
            {
                WebApiHelper wHelper = new WebApiHelper();
                if (Utils.isvalidIpAccess() == true)
                {
                    string _FromName = list[0];
                    string _FromAddress = list[1];
                    string _toAddress = list[2];
                    string _title = list[3];
                    string _url = list[4];
                    string _description = list[5];

                    string _Subject = "[IQS DIRECTORY] - " + _title.ToUpper().Replace("%20", " ");
                    string _strMailBody = "<a href = '" + _url.ToString() + "' > " + _title.Replace(" % 20", " ") + "</a><br/><br/>";
                    _strMailBody += "<br/><br/>" + _description.ToString();
                    _strMailBody += "<br/><br/><a href='" + _url.ToString() + "'><img alt='IQS Directory' src='http://www.iqsdirectory.com/images/iqsdirectory_home_logo.png' /></a>";
                    _strMailBody += "<br/><br/> Thanks & Regards <br/>" + _FromName.ToString();
                    //sendMailWithAttachment(_FromAddress, _toAddress, string.Empty, string.Empty, _Subject, _strMailBody, true);
                    bool mailstatus = Utils.SendMail("sumi@insynctechs.com", "sumi@insynctechs.com, sumiajit@gmail.com", "linda@insynctechs.com", string.Empty, _Subject, _strMailBody, true);
                        if (mailstatus == true)
                            return "Success";
                        else
                            return "MailError";
                    
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
                return "Error1";
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
                bool res = Utils.SendMail(_fromAddress, toEmail, _ccAddress, string.Empty, "[IQS DIRECTORY] COMPANY PROFILE REVIEW - USER REGISTRATION", sb.ToString(), true);
                //IQS.Utility.Utils.SendMail(_fromAddress, toEmail, "njerry@iforceproservices.com", string.Empty, "[IQS DIRECTORY] COMPANY PROFILE REVIEW - USER REGISTRATION", sb.ToString(), true);
                return res.ToString();

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
                /*Utils.SendMail(_fromAddress, _toAddress, _ccAddress, _bccAddress, "[IQS DIRECTORY] COMPANY PROFILE - NEW REVIEW POSTED", sb.ToString(), true);
                //IQS.Utility.Utils.SendMail(_fromAddress, "njerry@iforceproservices.com", "", "njerry@iforceproservices.com,mbbinil@iforceproservices.com", "[IQS DIRECTORY] COMPANY PROFILE - NEW REVIEW POSTED", sb.ToString(), true);
                if (ClientEmail != "")
                    Utils.SendMail(_fromAddress, ClientEmail, "", "", "[IQS DIRECTORY] COMPANY PROFILE - NEW REVIEW POSTED", sb.ToString(), true);
                //Utils.SendMail(_fromAddress, ClientEmail, "", "", "[IQS DIRECTORY] COMPANY PROFILE - NEW REVIEW POSTED", sb.ToString(), true);
                
                */
                return "mail sent";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static string SendReviewReplyMail(string CName, string CEmail, string CDate, string Review, string Company, string ClientEmail, string ReplyTo)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine("Hi, ");
                sb.AppendLine("<br/>");
                sb.AppendLine("New reply for " + Company);
                sb.AppendLine("<br/>");
                sb.AppendLine("Posted by : " + CName + " (" + CEmail + ")");
                sb.AppendLine("<br/>");
                sb.AppendLine("In reply to : " + ReplyTo);
                sb.AppendLine("<br/>");
                sb.AppendLine("Posted date : " + CDate);
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
                /*Utils.SendMail(_fromAddress, _toAddress, _ccAddress, _bccAddress, "[IQS DIRECTORY] COMPANY PROFILE - NEW REPLY POSTED", sb.ToString(), true);
                //IQS.Utility.Utils.SendMail(_fromAddress, "njerry@iforceproservices.com", "", "njerry@iforceproservices.com,mbbinil@iforceproservices.com", "[IQS DIRECTORY] COMPANY PROFILE - NEW REPLY POSTED", sb.ToString(), true);
                if (ClientEmail != "")
                    Utils.SendMail(_fromAddress, ClientEmail, "", "", "[IQS DIRECTORY] COMPANY PROFILE - NEW REPLY POSTED", sb.ToString(), true);
                */
                return "mail sent";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static string SendForgotPasswordMail(string FullName, string toEmail, string Password)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine("Dear " + FullName + ", ");
                sb.AppendLine("<br/><br/>");
                sb.AppendLine("Your requested login details for posting comments and reviews with IQS are: <br/>");
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
                //Utils.SendMail(_fromAddress, toEmail, "jpratt@industrialquicksearch.com", string.Empty, "[IQS DIRECTORY] COMPANY PROFILE REVIEW - USER REGISTRATION", sb.ToString(), true);
                Utils.SendMail(_fromAddress, toEmail, "", string.Empty, "[IQS DIRECTORY] COMPANY PROFILE REVIEW - USER REGISTRATION", sb.ToString(), true);
                return "mail sent";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        public static string premiumlistingemail(List<string> list)
        {
            try
            {               
                string _CompanyName = list[0];
                string _CompanyPhone = list[1];
                string _CompanyWebsite = list[2];
                string _ProductArea = list[3];
                string _ContactName = list[4];
                string _ContactTitle = list[5];
                string _ContactEmailAddress = list[6];
                string _CategoryName = list[7];

                string _strMailBody = null;
                string _toAddress = string.Empty;
                string _FromAddress = string.Empty;
                string _Subject = string.Empty;

                _strMailBody = "Suggested IQSDirectory Site : " + _CategoryName + "<br>" + "Company Name : " + _CompanyName + "<br>" + "Company Phone : " + _CompanyPhone + "<br>" + "Company Website : " + _CompanyWebsite + "<br>" + "Product/Service Area : " + _ProductArea + "<br>" + "Contact : " + _ContactName + "<br>" + "Contact Title : " + _ContactTitle + "<br>" + "Contact Email : " + _ContactEmailAddress;
                _strMailBody = _strMailBody + "<br><br>" + "Best Regards" + "<br>" + _ContactName;
                _toAddress = System.Configuration.ConfigurationManager.AppSettings["ListYourCompanyToMailID"];
                _FromAddress = _ContactEmailAddress;
                //System.Configuration.ConfigurationManager.AppSettings["ListYourCompanyFromMailID"];
                _Subject = System.Configuration.ConfigurationManager.AppSettings["ListYourCompanyPremiumSubject"];
                //Utils.SendMail(_FromAddress, _toAddress, string.Empty, string.Empty, _Subject, _strMailBody, true);
                bool mailstatus = Utils.SendMail(_FromAddress, "sumi@insynctechs.com", string.Empty, string.Empty, _Subject, _strMailBody, true);   

                        if (mailstatus == true)
                            return "Success";
                        else
                            return "MailError";
              
            }
            catch (Exception ex)
            {
                return "Error1";
            }
            finally
            {

            }

        }

    }
}