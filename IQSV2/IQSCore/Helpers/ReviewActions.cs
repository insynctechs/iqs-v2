using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IQSCore.Helpers
{
    public class ReviewActions
    {
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

                string _fromAddress;
                string _ccAddress;
                if (System.Configuration.ConfigurationManager.AppSettings["MailTestMode"].ToString() == "true")
                {
                    _fromAddress = System.Configuration.ConfigurationManager.AppSettings["TestRegisterMailID"].ToString();
                    _ccAddress = System.Configuration.ConfigurationManager.AppSettings["TestRegisterTo"].ToString();
                }
                else
                {
                    _fromAddress = System.Configuration.ConfigurationManager.AppSettings["RegisterMailID"].ToString();
                    _ccAddress = System.Configuration.ConfigurationManager.AppSettings["RegisterTo"].ToString();
                }
                ApiUtils.SendMail(_fromAddress, toEmail, _ccAddress, string.Empty, "[IQS DIRECTORY] COMPANY PROFILE REVIEW - USER REGISTRATION", sb.ToString(), true);
                return "mail sent";

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

    }
}