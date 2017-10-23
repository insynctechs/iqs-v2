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


    }
}