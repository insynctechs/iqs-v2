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
    public partial class DirectoryRFQ : System.Web.UI.Page
    {

        #region " Variable Declaration "
        WebApiHelper wHelper = new WebApiHelper();
        DataTable _dtCompanyDetails;
        string _strCheckBtnId = null;
        string _strCompanyname = null;
        string[] _strArrSelectedValues = null;
        char[] chrsplit = new char[] { '^' };
        char[] chrsplitDetail = new char[] { '|' };
        string _RequestIP = System.Web.HttpContext.Current.Request.UserHostAddress;
        
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                WebURL = wHelper.WebUrl;
                
                
                if (Request.QueryString["CategorySK"] != null)// && Double.Parse(Request.QueryString["CategorySK"].ToString()))
                {
                    hdnCategorySK.Value = Request.QueryString["CategorySK"].ToString();
                    if (Request.QueryString["ClientSK"] != null && Request.QueryString["ClientSK"].ToString() != "")// && Double.Parse(Request.QueryString["ClientSK"].ToString()))
                    {
                        hdnRFQClientSK.Value = Request.QueryString["ClientSK"].ToString();
                    }
                    //frmRFQ.Action = WebURL + "DirectoryRFQ.aspx?CategorySK="+hdnCategorySK.Value+"&ClientSK=" + hdnRFQClientSK.Value;
                    LoadCompanyDetails();
                    if (!IsPostBack)
                    {
                        LoadHeaderDetails();
                        LoadTrackingScripts();
                    }
                }
                else if (Request.QueryString["ClientSK"] != null)
                {
                    hdnRFQClientSK.Value = Request.QueryString["ClientSK"].ToString();
                    LoadCompanyDetails();
                    if (!IsPostBack)
                    {
                        //LoadHeaderDetails();
                        lblRFQSubHeading.Text = "Please fill out the following form to submit a Request for Quote";
                        LoadTrackingScripts();
                    }
                }
            }
            catch (Exception ex)
            {
                //CommonLogger.Error("DirectoryRFQ: Browser--> " + Request.UserAgent.ToString() + " CategorySk-->" + hdnCategorySK.Value + " ClientSK-->" + hdnRFQClientSK.Value);
                //CommonLogger.Error("DirectoryRFQ.Page_Load()", ex);
                throw ex;
            }
            finally
            {
            }

        }

        #region "Load Tracking Scripts"
        private void LoadTrackingScripts()
        {
            
            string attrs = "";
            //sj added javascript validation to button click

            attrs += "return SetSelectedValues();";
            btnSubmit.Attributes.Add("onclick", attrs);


        }
        #endregion

        #region " Load Header Details "
        public void LoadHeaderDetails()
        {
            DataTable _dtDisplayName;
            try
            {

                var urlGetId = string.Format("api/RFQ/GetCategoryDisplayName?CategorySK=" + hdnCategorySK.Value);
                _dtDisplayName = wHelper.GetDataTableFromWebApi(urlGetId);
                
                if (_dtDisplayName.Rows.Count != 0)
                {
                    hdnCompanyName.Value = _dtDisplayName.Rows[0][0].ToString();
                    hdnCategoryDisplayName.Value = _dtDisplayName.Rows[0][1].ToString();
                }
                //this.Page.Title = hdnCategoryDisplayName.Value + " RFQ";
                PageTitle = hdnCategoryDisplayName.Value + " RFQ";
                PageKeyword = "RFQ from " + hdnCategoryDisplayName.Value + " Manufacturers, " + hdnCategoryDisplayName.Value + " Manufacturers, " + hdnCategoryDisplayName.Value + " Suppliers";
                PageDescription = " Request Immediate Quotes from " + hdnCategoryDisplayName.Value + " Manufacturers & Suppliers.";
                lblCategoryHeading.Text = hdnCategoryDisplayName.Value + " RFQ";
                hylnkCategory.Text = hdnCategoryDisplayName.Value;
                hylnkCategory.NavigateUrl = wHelper.WebUrl + hdnCompanyName.Value + "/";//To be filled later
            }
            catch (Exception ex)
            {
                /*CommonLogger.Error("DirectoryRFQ: Browser--> " + Request.UserAgent.ToString() + " CategorySk-->" + hdnCategorySK.Value + " ClientSK-->" + hdnRFQClientSK.Value);
                CommonLogger.Error("DirectoryRFQ.LoadHeaderDetails()", ex);*/
                throw ex;
            }
            finally
            {
                _dtDisplayName = null;
                
            }
        }
        #endregion

        #region " Load Company Details "
        /// <summary>
        ///EmailID ,SequenceNo,Tiersk , ClientSk 
        /// </summary>
        public void LoadCompanyDetails()
        {
            pnlCategories.Controls.Clear();
            int _iCount = 0;
            string[] _strArrRetainedvalues;
            try
            {
                var urlGetId = "" ;
                if (hdnCategorySK.Value != "")
                    urlGetId =  string.Format("api/RFQ/GetCompanyDetailsForCategory?CategorySK=" + hdnCategorySK.Value);
                else
                    urlGetId = string.Format("api/RFQ/GetCompanyDetailsForClient?ClientSK=" + hdnRFQClientSK.Value);
                _dtCompanyDetails = wHelper.GetDataTableFromWebApi(urlGetId);

                //Code added by GFI on 03/29
                // #### To remove other companies in the list if the RFQ clientid is 68981
                // ### (RDM Industrial Products Inc. (CA))
                if (hdnRFQClientSK.Value != "" && hdnRFQClientSK.Value == "68981")
                {

                    DataTable _dtCompanyDetails1;
                    _dtCompanyDetails1 = _dtCompanyDetails.Clone();
                    for (int jj = 0; jj < _dtCompanyDetails.Rows.Count; jj++)
                    {
                        if (hdnRFQClientSK.Value == _dtCompanyDetails.Rows[jj].ItemArray[4].ToString())
                        {
                            _dtCompanyDetails1.ImportRow(_dtCompanyDetails.Rows[jj]);
                            break;
                        }
                    }
                    _dtCompanyDetails.Clear();
                    _dtCompanyDetails = _dtCompanyDetails1.Copy();

                }
                // End of GFI change 03/29

                pnlCategories.Controls.Add(new LiteralControl("<table id='tblCategories' width='300' cellspacing='1' cellpadding='2'> "));
                foreach (DataRow row in _dtCompanyDetails.Rows)
                {
                    pnlCategories.Controls.Add(new LiteralControl("<tr>"));
                    pnlCategories.Controls.Add(new LiteralControl("<td width='1'>"));
                    CheckBox chkSelect = new CheckBox();
                    chkSelect.ID = "chkSelect" + _iCount;
                    if (_strCheckBtnId == null)
                    {
                        _strCheckBtnId = chkSelect.ID;
                    }
                    else
                    {
                        _strCheckBtnId = _strCheckBtnId + "`" + chkSelect.ID;
                    }
                    pnlCategories.Controls.Add(chkSelect);
                    pnlCategories.Controls.Add(new LiteralControl("</td>"));
                    pnlCategories.Controls.Add(new LiteralControl("<td width='300'>"));
                    Label lblCompanyName = new Label();
                    lblCompanyName.ID = "lblCategories" + _iCount;
                    lblCompanyName.Text = row.ItemArray[0].ToString() + " (" + row.ItemArray[5].ToString() + ")";
                    if (_strCompanyname == null)
                    {
                        _strCompanyname = row.ItemArray[0].ToString();
                    }
                    else
                    {
                        _strCompanyname = _strCompanyname + "`" + row.ItemArray[0].ToString();
                    }
                    pnlCategories.Controls.Add(lblCompanyName);
                    HiddenField hdnPnlEmailid = new HiddenField();
                    hdnPnlEmailid.ID = "hdnPnlEmailid" + _iCount;
                    hdnPnlEmailid.Value = row.ItemArray[1].ToString() + "|" + row.ItemArray[2].ToString() + "|" + row.ItemArray[3].ToString() + "|" + row.ItemArray[4].ToString();
                    pnlCategories.Controls.Add(hdnPnlEmailid);
                    if (hdnRFQClientSK.Value != "")
                    {
                        if (hdnRFQClientSK.Value == row.ItemArray[4].ToString())
                        {
                            chkSelect.Checked = true;
                        }
                    }
                    if (hdnSelectedtext.Value != "")
                    {
                        ReturnClient();
                        _strArrRetainedvalues = hdnRetainClientSK.Value.Split(chrsplitDetail);
                        for (int j = 0; j < _strArrRetainedvalues.Length; j++)
                        {
                            if (_strArrRetainedvalues[j] == row.ItemArray[4].ToString())
                            {
                                chkSelect.Checked = true;
                            }
                        }
                        hdnRetainClientSK.Value = "";
                    }
                    pnlCategories.Controls.Add(new LiteralControl("</td>"));
                    pnlCategories.Controls.Add(new LiteralControl("</tr>"));
                    _iCount = _iCount + 1;
                }
                pnlCategories.Controls.Add(new LiteralControl("</table> "));
                btnChkAll.Attributes.Add("onClick", " return CheckAll('" + _strCheckBtnId + "')");

            }
            /*catch (BaseException bex)
            {
                CommonLogger.Error("DirectoryRFQ: Browser--> " + Request.UserAgent.ToString() + " CategorySk-->" + hdnCategorySK.Value + " ClientSK-->" + hdnRFQClientSK.Value);
                CommonLogger.Error("DirectoryRFQ.LoadCompanyDetails()", bex);
                ctrlErrorMessage.SetError(bex.Message.ToString(), "red");
            }*/
            catch (Exception ex)
            {
                /*CommonLogger.Error("DirectoryRFQ: Browser--> " + Request.UserAgent.ToString() + " CategorySk-->" + hdnCategorySK.Value + " ClientSK-->" + hdnRFQClientSK.Value);
                CommonLogger.Error("DirectoryRFQ.LoadCompanyDetails()", ex);
                throw new BaseException(ex.Message);*/
            }
            finally
            {
               
                _dtCompanyDetails = null;
            }

        }
        #endregion

        #region " Submit Button Click "
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           
            divip_error.Visible = false;
                    if(Utils.isvalidIpAccess()==true)
                    { 
                        //Captcha1.CheckEnteredValue();
                        InsertMethod();
                        sendMail();
                    }
                    else
                    {
                        divip_error.Visible = true;
                        OnSecurityFailure(2);
                    }
            
        }

        public void OnSecurityFailure(int ischeckbox)
        {
            if (ischeckbox == 1)
                rfqmessage.InnerText = "Sorry! Please check the box confirming that you are interested in getting a quote and that this is not a spam.";
            else if (ischeckbox == 2)
                rfqmessage.InnerText = "Sorry! The Use of this Form is Restricted - Please Contact IQSDirectory with Questions.";
            else
                rfqmessage.InnerText = "Sorry! This looks like a spam submission! You are not allowed.";
            ClearHiddenFields();
            LoadCompanyDetails();
        }
        #endregion

        #region " Clear Hidden Fields "
        public void ClearControls()
        {
            hdnClientSK.Value = "";
            hdnEmailId.Value = "";
            hdnTierSK.Value = "";
            hdnSequenceNo.Value = "";
            hdnSelectedtext.Value = "";
            txtCompanyName.Text = "";
            txtContactName.Text = "";
            txtContactEmail.Text = "";
            txtContactPhone.Text = "";            
            txtContactCity.Text = "";          
            txtDescription.Value = "";
            
        }

        public void ClearHiddenFields()
        {
            hdnClientSK.Value = "";
            hdnEmailId.Value = "";
            hdnTierSK.Value = "";
            hdnSequenceNo.Value = "";
            
        }
        #endregion

        #region " Retain Values "
        public string ReturnClient()
        {
            int count = 0;
            _strArrSelectedValues = hdnSelectedtext.Value.Split(chrsplit);
            for (count = 0; count <= _strArrSelectedValues.Length - 1; count++)
            {
                string[] _strArrDetails = _strArrSelectedValues[count].Split(chrsplitDetail);
                if (hdnRetainClientSK.Value == "")
                {
                    hdnRetainClientSK.Value = _strArrDetails[4];
                }
                else
                {
                    hdnRetainClientSK.Value = hdnRetainClientSK.Value + "|" + _strArrDetails[4];
                }
            }
            return hdnRetainClientSK.Value;
        }
        #endregion

        #region " Insert into RFQ tables "
        private void InsertMethod()
        {
            int _iRFQHeader;
            
            string[] companyDetails = hdnSelectedtext.Value.Split(chrsplit);

            int count = 0;
            try
            {
                if (hdnCategorySK.Value == "")
                {
                    hdnCategorySK.Value = hdnRFQClientSK.Value;
                }
                
                // CommonLogger.Info("Insertion to RFQHeader for category SK: " + hdnCategorySK.Value);
                var urlGetId = string.Format("api/RFQ/InsertRFQ?CategorySK=" + hdnCategorySK.Value + "&CompanyName="+ txtCompanyName.Text.Trim() + "&ContactName="+ txtContactName.Text.Trim() + "&Email="+ txtContactEmail.Text.Trim() + "&Address="+ txtContactCity.Text.Trim() + "&Phone="+ txtContactPhone.Text.Trim() + "&Comments="+ txtDescription.Value.Trim()  + "&RequestIP="+ _RequestIP);
                _iRFQHeader = wHelper.GetExecuteNonQueryResFromWebApi(urlGetId);
                 if (_iRFQHeader != 0)
                {
                    for (count = 0; count < companyDetails.Length; count++)
                    {

                        string[] Details = companyDetails[count].Split(chrsplitDetail);
                        var urlGetId1 = string.Format("api/RFQ/InsertRFQClientDetails?RFQHeaderSK="+ _iRFQHeader + "&ClientSK="+ Details[4] + "&SequenceNo="+ Details[2] + "&TierSK="+ Details[3]);
                        int res1 = wHelper.GetExecuteNonQueryResFromWebApi(urlGetId1);
                    }
                    
                    //CommonLogger.Info("Insertion completed");
                }
                else
                {
                    //ctrlErrorMessage.SetError(Utils.GetNodeValue("TransactionFailure", "include/Message.xml"), "red");
                    ClearHiddenFields();
                    LoadCompanyDetails();
                }
            }
            /*catch (BaseException bex)
            {
                CommonLogger.Error("DirectoryRFQ: Browser--> " + Request.UserAgent.ToString() + " CategorySk-->" + hdnCategorySK.Value + " ClientSK-->" + hdnRFQClientSK.Value);
                CommonLogger.Error("InsertMethod", bex);
                ctrlErrorMessage.SetError(bex.Message.ToString(), "red");
            }*/
            catch (Exception ex)
            {
                /*CommonLogger.Error("DirectoryRFQ: Browser--> " + Request.UserAgent.ToString() + " CategorySk-->" + hdnCategorySK.Value + " ClientSK-->" + hdnRFQClientSK.Value);
                CommonLogger.Error("InsertMethod", ex);
                throw new BaseException(ex.Message);*/
            }
            finally
            {
                
            }
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

            //Response.Write(strUsername + "---" + strPassword);

            /*strFileName has a attachment file name for 
              attachment process. */
            string strFileName = null;

            /* Begining of Attachment1 process   & 
                       Check the first open file dialog for a attachment */
            if (inpAttachment1.PostedFile != null)
            {
                /* Get a reference to PostedFile object */
                HttpPostedFile attFile = inpAttachment1.PostedFile;
                /* Get size of the file */
                int attachFileLength = attFile.ContentLength;
                /* Make sure the size of the file is > 0  */
                if (attachFileLength > 0)
                {
                    /* Get the file name */
                    strFileName = Path.GetFileName(inpAttachment1.PostedFile.FileName);
                    inpAttachment1.PostedFile.InputStream.Seek(0, SeekOrigin.Begin);
                    /* Create the email attachment with the uploaded file */
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(inpAttachment1.PostedFile.InputStream, strFileName, inpAttachment1.PostedFile.ContentType);
                    /* Attach the newly created email attachment */
                    Email.Attachments.Add(attach);

                }
            }

            if (inpAttachment2.PostedFile != null)
            {
                /* Get a reference to PostedFile object */
                HttpPostedFile attFile = inpAttachment2.PostedFile;
                /* Get size of the file */
                int attachFileLength = attFile.ContentLength;
                /* Make sure the size of the file is > 0  */
                if (attachFileLength > 0)
                {
                    /* Get the file name */
                    strFileName = Path.GetFileName(inpAttachment2.PostedFile.FileName);
                    inpAttachment2.PostedFile.InputStream.Seek(0, SeekOrigin.Begin);
                    /* Create the email attachment with the uploaded file */
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(inpAttachment2.PostedFile.InputStream, strFileName, inpAttachment2.PostedFile.ContentType);
                    /* Attach the newly created email attachment */
                    Email.Attachments.Add(attach);

                }
            }

            if (inpAttachment3.PostedFile != null)
            {
                /* Get a reference to PostedFile object */
                HttpPostedFile attFile = inpAttachment3.PostedFile;

                /* Get size of the file */
                int attachFileLength = attFile.ContentLength;
                /* Make sure the size of the file is > 0  */
                if (attachFileLength > 0)
                {
                    /* Get the file name */
                    strFileName = Path.GetFileName(inpAttachment3.PostedFile.FileName);
                    inpAttachment3.PostedFile.InputStream.Seek(0, SeekOrigin.Begin);
                    /* Create the email attachment with the uploaded file */
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(inpAttachment3.PostedFile.InputStream, strFileName, inpAttachment3.PostedFile.ContentType);
                    /* Attach the newly created email attachment */
                    Email.Attachments.Add(attach);

                }
            }


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


        }
        #endregion


        #region " Sending Mail "
        private void sendMail()
        {

            try
            {
                string _strMailBody = null;
                string _toAddress = string.Empty;
                string _ccAddress = string.Empty;
                string _FromAddress = string.Empty;
                string _Subject = string.Empty;

                string[] companyDetails = hdnSelectedtext.Value.Split(chrsplit);
                int count = 0;
                for (count = 0; count < companyDetails.Length; count++)
                {
                    string[] Details = companyDetails[count].Split(chrsplitDetail);
                    //CommonLogger.Info("Sending mail for RFQ: " + "From mail id: " + txtContactEmail.Text.Trim().ToString() + "To Mail Id: " + Details[1] + "CC Mail Id: " + System.Configuration.ConfigurationManager.AppSettings["RFQCCMailID"] + "Mail Server IP: " + System.Configuration.ConfigurationManager.AppSettings["MailServerIP"]);
                    _strMailBody = "Name : " + Server.HtmlEncode(txtContactName.Text.Trim()) + "<br>" + "Company : " + txtCompanyName.Text.Trim() + "<br>" + "Email : " + Server.HtmlEncode(txtContactEmail.Text.Trim()) + "<br>" + "Contact Phone : " + Server.HtmlEncode(txtContactPhone.Text.Trim()) + "<br>" + "City, State : " + Server.HtmlEncode(txtContactCity.Text.Trim()) + "<br>" + "Specifications/Questions/RFQ : " + Server.HtmlEncode(txtDescription.Value.Trim()) + "<br>" + "Request IP:" + _RequestIP;

                    _FromAddress = txtContactEmail.Text.Trim().ToString();
                    _Subject = Utils.RFQSubject;
                    if (Utils.RFQTestMode == "true")
                    {
                        _toAddress = Utils.TestEmailTo;
                        _ccAddress = Utils.TestEmailCC;
                        _Subject = Utils.TestEmailSubjectPrefix + _Subject;

                    }
                    else
                    {
                        _toAddress = ((Details[1].Equals("N/A")) | (Details[1].Equals("NULL"))) ? Utils.RFQAlternateMailID : Details[1];
                        _ccAddress = Utils.RFQCCMailID;

                    }
                                       
                    
                    sendMailWithAttachment(_FromAddress, _toAddress, _ccAddress, string.Empty, _Subject, _strMailBody, true);
                    

                    //CommonLogger.Info("Sending mail completed");
                }
                //Response.Redirect("RFQConfirmation.aspx?CategorySk=" + hdnCategorySK.Value, false);
                Response.Redirect("RFQConfirmation.aspx?CategorySk=" + hdnCategorySK.Value + "&RequestIP=" + _RequestIP, false);
            }
            /*catch (BaseException bex)
            {
                CommonLogger.Error("DirectoryRFQ: Browser--> " + Request.UserAgent.ToString() + " CategorySk-->" + hdnCategorySK.Value + " ClientSK-->" + hdnRFQClientSK.Value);
                CommonLogger.Error("sendMail", bex);
                ctrlErrorMessage.SetError(bex.Message.ToString(), "red");
            }*/
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

        #endregion

        protected void btnSend_Click(object sender, EventArgs e)
        {
            divip_error.Visible = false;
            if (Utils.isvalidIpAccess() == true)
            {
                //Captcha1.CheckEnteredValue();
                InsertMethod();
                sendMail();
            }
            else
            {
                divip_error.Visible = true;
                OnSecurityFailure(2);
            }

        }
        public string WebURL { get; set; }
        public string PageTitle { get; set; }
        public string PageKeyword { get; set; }
        public string PageDescription { get; set; }
    }

}