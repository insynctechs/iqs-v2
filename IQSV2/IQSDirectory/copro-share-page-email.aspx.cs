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
	public partial class copro_share_page_email : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //rootDirPath = Request.QueryString["p"].ToString();
                //Captcha1.UrlPath = rootDirPath;
                txtTitle.Value = Request.QueryString["title"].ToString();
                txtDescription.Value = Request.QueryString["des"].ToString();
                txtUrl.Value = Request.QueryString["url"].ToString();

                
                //string attrs = "";
                //sj added javascript validation to button click
                //attrs += " return jqClick();";
                //btnSubmit.Attributes.Add("onClick", attrs);
            }
      
                /*
                if (!IsPostBack)
                {
                    //rootDirPath = Request.QueryString["p"].ToString();
                    //Captcha1.UrlPath = rootDirPath;
                    txtTitle.Value = Request.QueryString["title"].ToString();
                    txtDescription.Value = Request.QueryString["des"].ToString();
                    txtUrl.Value = Request.QueryString["url"].ToString();
                    this.Page.Header.Controls.Add(new LiteralControl("<link rel='stylesheet' type='text/css' href='Content/styler.css' />"));
                    this.Page.Header.Controls.Add(new LiteralControl("<link rel='stylesheet' type='text/css' href='Content/jquery-ui.css' />"));
                    this.Page.Header.Controls.Add(new LiteralControl("<link rel='stylesheet' type='text/css' href='Content/jquery.tagsinput.css' />"));
                    this.Page.Header.Controls.Add(new LiteralControl("<script type='text/javascript' src='Scripts/jquery-1.7.2.min.js'></script>"));
                    this.Page.Header.Controls.Add(new LiteralControl("<script type='text/javascript' src='Scripts/jquery.tagsinput.js'></script>"));
                    this.Page.Header.Controls.Add(new LiteralControl("<script type='text/javascript' src='Scripts/jquery-ui.js'></script>"));
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("<script type='text/javascript'>");
                    sb.AppendLine("$(document).ready(function(){");
                    sb.AppendLine("$('#txtTo').tagsInput({");
                    sb.AppendLine("'width': '360px',");
                    sb.AppendLine("'height': '60px',");
                    sb.AppendLine("'defaultText': 'add email',");
                    sb.AppendLine("});");
                    sb.AppendLine("$('#lnkSend').click(function(){");
                    sb.AppendLine("if ($.trim($('#txtName').val()) == '') {");
                    sb.AppendLine("alert('Please Enter Name');");
                    sb.AppendLine("$('#txtName').focus();");
                    sb.AppendLine("return false;");
                    sb.AppendLine("}");
                    sb.AppendLine("if ($.trim($('#txtFrom').val()) == '') {");
                    sb.AppendLine("alert('Please Enter Email');");
                    sb.AppendLine("$('#txtFrom').focus();");
                    sb.AppendLine("return false;");
                    sb.AppendLine("}");
                    sb.AppendLine("if (!isValidEmailAddress($('#txtFrom').val())) {");
                    sb.AppendLine("alert('Enter a Valid Email');");
                    sb.AppendLine("$('#txtFrom').focus();");
                    sb.AppendLine("return false;");
                    sb.AppendLine("}");
                    sb.AppendLine("if ($.trim($('#txtTo').val()) == '') {");
                    sb.AppendLine("alert('Please Enter Receiver(s) Email');");
                    sb.AppendLine("$('#txtTo').focus();");
                    sb.AppendLine("return false;");
                    sb.AppendLine("}");
                    sb.AppendLine("if ($.trim($('#Captcha1_txtCaptcha').val()) == '') {");
                    sb.AppendLine("alert('Please Enter Code');");
                    sb.AppendLine("$('#Captcha1_txtCaptcha').focus();");
                    sb.AppendLine("return false;");
                    sb.AppendLine("}");
                    sb.AppendLine("$('#lnkSend').fadeOut(function(){$('.spanwait').show();});");
                    sb.AppendLine("var list = [$('#txtName').val(), $('#txtFrom').val(), $('#txtTo').val(), $('#txtTitle').val(), $('#txtDescription').val(), $('#txtUrl').val(), $('#Captcha1_txtCaptcha').val()];");
                    sb.AppendLine("var jsonText = JSON.stringify({ list: list });");
                    sb.AppendLine("$.ajax({");
                    sb.AppendLine("type: 'POST',");
                    //sb.AppendLine("url: '" + rootDirPath + "controls/reviewmanager.aspx/sendpagebyemail',");
                    sb.AppendLine("data: jsonText,");
                    sb.AppendLine("contentType: 'application/json; charset=utf-8',");
                    sb.AppendLine("dataType: 'json',");
                    sb.AppendLine("async: true,");
                    sb.AppendLine("cache: false,");
                    sb.AppendLine("success: function (msg) {");
                    sb.AppendLine("if (msg == 'Success') {");
                    sb.AppendLine("$('#divRegForm').slideUp('slow', function() {");
                    sb.AppendLine("$('#divSuccess').slideDown();");
                    sb.AppendLine("});return false;");
                    sb.AppendLine("}");
                    sb.AppendLine("else if (msg == 'Captcha') {");
                    sb.AppendLine("$('#lnkSend').fadeIn(function(){$('.spanwait').hide();alert('The code you entered was not correct!!');});");
                    sb.AppendLine("return false;");
                    sb.AppendLine("}");
                    sb.AppendLine("else {");
                    sb.AppendLine("alert(msg);");
                    sb.AppendLine("$('.spanwait').hide();");
                    sb.AppendLine("$('#lnkSend').show();");
                    sb.AppendLine("return false;");
                    sb.AppendLine("}");
                    sb.AppendLine("},");
                    sb.AppendLine("failure: function () {");
                    sb.AppendLine("alert('Request Failed. Try Again.');");
                    sb.AppendLine("$('.spanwait').hide();");
                    sb.AppendLine("$('#lnkSend').show();");
                    sb.AppendLine("return false;");
                    sb.AppendLine("}");
                    sb.AppendLine("});");
                    sb.AppendLine("return false;");
                    sb.AppendLine("});");
                    sb.AppendLine("$('#lnkClose').click(function(){");
                    sb.AppendLine("parent.$.fancybox.close();");
                    sb.AppendLine("return false;");
                    sb.AppendLine("});");
                    sb.AppendLine("});");
                    sb.AppendLine("</script>");
                    this.Page.Header.Controls.Add(new LiteralControl(sb.ToString()));
                }
                */
            }
            protected void btnSubmit_Click(object sender, EventArgs e)
            {
                try
                {
                /*
                string[] keys = Request.Form.AllKeys;
                for (int i = 0; i < keys.Length; i++)
                {
                    Response.Write(keys[i] + ": " + Request.Form[keys[i]] + "<br>");
                }
                
                string _FromAddress = Request.Form["txtFrom"].ToString();                                    
                string _toAddress = Request.Form["txtTo"].Trim().ToString();
                string _Subject = "[IQS DIRECTORY] - " + txtTitle.Value.ToUpper().Replace("%20", " ");
                string _strMailBody = "<a href = '" + txtUrl.Value.ToString() + "' > " + txtTitle.Value.Replace(" % 20", " ") + "</a><br/><br/>";
                _strMailBody += "<br/><br/>"+txtDescription.Value.ToString();
                _strMailBody += "<br/><br/><a href='" + txtUrl.Value.ToString() + "'><img alt='IQS Directory' src='http://www.iqsdirectory.com/images/iqsdirectory_home_logo.png' /></a>";
                _strMailBody += "<br/><br/> Thanks & Regards <br/>" + Request.Form["txtName"].ToString();
                //sendMailWithAttachment(_FromAddress, _toAddress, string.Empty, string.Empty, _Subject, _strMailBody, true);
                bool mailstatus = Utils.SendMail("sumi@insynctechs.com", "sumi@insynctechs.com, sumiajit@gmail.com", "linda@insynctechs.com", string.Empty, _Subject, _strMailBody, true);
                */
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

        #region SMTP email sending with attachment code
        private void sendMailWithAttachment(string strFromAddress, string strToAddress, string strCCAddress, string strBCCAddress, string strSubject, string strBodyContent, bool IsBodyHtml)
        {
            string strUsername = System.Configuration.ConfigurationManager.AppSettings["MailServerUsername"];
            string strPassword = System.Configuration.ConfigurationManager.AppSettings["MailServerpassword"];
            //Create a new MailMessage object and specify the"From" and "To" addresses
            System.Net.Mail.MailMessage Email = new System.Net.Mail.MailMessage();
            Email.From = new MailAddress(strFromAddress.ToString());
            Email = EmailAddressCollection(strToAddress.ToString(), "TO", ref Email);
            //Email.To.Add(new MailAddress("globalforce-test@industrialquicksearch.com"));
            if (ValidateMailAddress(ref strCCAddress))
                Email = EmailAddressCollection(strCCAddress.ToString(), "CC", ref Email);
            if (ValidateMailAddress(ref strBCCAddress))
                Email = EmailAddressCollection(strBCCAddress.ToString(), "BCC", ref Email);
            Email.Subject = strSubject.ToString();
            Email.Body = strBodyContent.ToString();
            Email.IsBodyHtml = IsBodyHtml;
            System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient();

            if (strUsername.Trim().Length > 0 & strPassword.Trim().Length > 0)
            {
                //This object stores the authentication values
                System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(strUsername, strPassword);
                mailClient.UseDefaultCredentials = false;
                mailClient.Credentials = basicAuthenticationInfo;
            }
            //Put your own, or your ISPs, mail server name onthis next line
            mailClient.Host = System.Configuration.ConfigurationManager.AppSettings["MailServerIP"];
            mailClient.Send(Email);
            //divStatus.InnerHtml = "<h1>Mail Succesfully sent.</h1>";
            //divRegForm.Style.Add("display", "none");
        }
        #endregion

        #region MailAddressCollection
        private static MailMessage EmailAddressCollection(string EmailAddress, string type, ref MailMessage MailObject)
        {
            char[] _MailSeparator = new char[] { Convert.ToChar(System.Configuration.ConfigurationManager.AppSettings["MailAddressSeparator"]) };
            switch (type.ToUpper())
            {
                case "TO":
                    {
                        string[] _address = EmailAddress.Split(_MailSeparator);
                        foreach (string _MailID in _address)
                        {
                            if (_MailID != "")
                                MailObject.To.Add(new MailAddress(_MailID));
                        }
                        break;
                    }
                case "CC":
                    {
                        string[] _address = EmailAddress.Split(_MailSeparator);
                        foreach (string _MailID in _address)
                        {
                            if (_MailID != "")
                                MailObject.CC.Add(new MailAddress(_MailID));
                        }
                        break;
                    }
                case "BCC":
                    {
                        string[] _address = EmailAddress.Split(_MailSeparator);
                        foreach (string _MailID in _address)
                        {
                            if (_MailID != "")
                                MailObject.Bcc.Add(new MailAddress(_MailID));
                        }
                        break;
                    }
            }
            return MailObject;
        }
        #endregion

        #region ValidateMailAddress
        private static bool ValidateMailAddress(ref string mailaddress)
        {
            return (string.IsNullOrEmpty(mailaddress) ? false : true);
        }
        #endregion
    }
}