using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Net.Mail;


namespace IQSDirectory.Helpers
{
    public static class Utils
    {
        public static string FormatCompanyWebsiteLink(string CompanyName)
        {
            if (CompanyName == "" || CompanyName == null)
            {
                return String.Empty;
            }
            string[] starr = { " incorporated", " corporation", " industries", " industry", " international", " company", " companies", " llc", " inc", " corp" };
            string cname = CompanyName;
            cname = cname.Trim().Replace("&amp;", "&");
            cname = cname.Replace("–", " ");
            cname = cname.Replace("-", " ");//
            cname = cname.Replace("—", " ");
            cname = cname.Replace("–", " ");
            cname = cname.Replace("—", " ");
            cname = cname.Replace("-", " ");//
            cname = cname.Replace("&#151;", " ");
            cname = cname.Replace("&mdash;", " ");
            cname = cname.Replace("&#189;", "½");
            cname = cname.Replace("&#188;", "¼");
            cname = cname.Replace("&#190;", "¾");
            cname = cname.Replace("&quot;", "“");
            cname = cname.Replace("&quot;", "”");
            cname = cname.Replace("&#39;", "’");
            cname = cname.Replace("&deg;", "°");
            cname = cname.Replace("&deg;C", "ºC");
            cname = cname.Replace("&trade;", "™");
            cname = cname.Replace("&#246;", "ö");
            cname = cname.Replace("&#181;", "µ");
            cname = cname.Replace("&#252;", "ü");
            cname = cname.Replace("&#8226;", "•");
            cname = cname.Replace("&#169;", "©");
            cname = cname.Replace("&reg;", "®");
            cname = cname.Replace("&eacute;", "é");
            cname = cname.Replace(" ", " ");
            cname = cname.Replace("&", " and ");
            cname = cname.Replace(",", " ");
            cname = cname.Replace(".", "");
            cname = cname.Replace("/", " ");
            cname = cname.Replace("'", " ");
            cname = cname.Replace("’", " ");
            cname = cname.Replace("'", " ");
            cname = cname.Replace("’", " ");
            cname = cname.Replace("°", " ");
            cname = cname.Replace("™", " ");
            cname = cname.Replace("®", " ");
            cname = cname.Replace("#", " ");
            cname = cname.Replace("^", " ");
            cname = cname.Replace("$", " ");
            cname = cname.Replace("|", " ");
            cname = cname.Replace(":", " ");
            cname = cname.Replace(";", " ");
            cname = cname.Replace("<", " ");
            cname = cname.Replace(">", " ");
            cname = cname.Replace("?", " ");
            cname = cname.Replace("[", " ");
            cname = cname.Replace("]", " ");
            cname = cname.Replace("ö", "o");
            cname = cname.Replace("ä", "a");
            cname = cname.Replace("™", " ");
            cname = cname.Replace("\"", " ");
            cname = cname.Replace(@"\", " ");
            cname = cname.Replace("-", " ");//
            cname = cname.Replace("&#228", "a");
            cname = cname.Replace("&#151;", " ");
            cname = cname.Replace("&mdash;", " ");
            cname = cname.Replace("&#189;", " ");
            cname = cname.Replace("&#188;", " ");
            cname = cname.Replace("&#190;", " ");
            cname = cname.Replace("&quot;", " ");
            cname = cname.Replace("&quot;", " ");
            cname = cname.Replace("&#39;", " ");
            cname = cname.Replace("&deg;", " ");
            cname = cname.Replace("&deg;C", " ");
            cname = cname.Replace("&trade;", " ");
            cname = cname.Replace("&#246;", " ");
            cname = cname.Replace("&#181;", " ");
            cname = cname.Replace("&#252;", " ");
            cname = cname.Replace("&#8226;", " ");
            cname = cname.Replace("&#8482;", " ");
            cname = cname.Replace("&#169;", " ");
            cname = cname.Replace("&reg;", " ");
            cname = cname.Replace("&eacute;", " ");
            cname = cname.Replace("ü", "u");
            cname = cname.Replace("©", " ");
            cname = cname.Replace("é", "e");
            cname = cname.Replace("---", " ");
            cname = cname.Replace("--", " ");
            cname = Regex.Replace(cname, @"[^\w\s]", "");
            foreach (string s in starr)
            {
                if (cname.ToLower().Contains(s))
                    cname = cname.Replace(cname.Substring(cname.ToLower().IndexOf(s), s.Length), "");
            }
            //for co
            cname = System.Text.RegularExpressions.Regex.Replace(cname, @"(\sco)(\W|\s)*$", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            cname = cname.Replace("   ", " ");
            cname = cname.Replace("  ", " ");
            cname = cname.Trim();
            if (cname.Length >= 4)
            {
                if (cname.Substring(cname.Length - 4) == " and")
                    cname = cname.Substring(0, cname.Length - 4);
            }
            return cname;
        }

        public static string ReplaceContent(string Content, int type)
        {
            string _replacecontent = string.Empty;
            if(Content=="" || Content == null)
            {
                return String.Empty;
            }
            switch (type)
            {
                case 0:
                    _replacecontent = Content.Trim().Replace(" & ", " &amp; ");
                    _replacecontent = _replacecontent.Replace("™", "&trade;");
                    _replacecontent = _replacecontent.Replace("—", "&#151;");
                    _replacecontent = _replacecontent.Replace("\"", "&quot;");
                    _replacecontent = _replacecontent.Replace("®", "&reg;");
                    _replacecontent = _replacecontent.Replace("'", "&#96;");
                    _replacecontent = _replacecontent.Replace("ü", "&#252;");
                    _replacecontent = _replacecontent.Replace("™", "&trade;");
                    _replacecontent = _replacecontent.Replace("é", "&eacute;");
                    break;
                case 1:
                    _replacecontent = Content.Trim().Replace(" & ", " &amp; ");
                    _replacecontent = _replacecontent.Replace("</span> –", "</span> -");
                    _replacecontent = _replacecontent.Replace("</span>–", "</span> -");
                    _replacecontent = _replacecontent.Replace("–", "-");
                    _replacecontent = _replacecontent.Replace("-", "-");//
                    _replacecontent = _replacecontent.Replace("—", "-");
                    _replacecontent = _replacecontent.Replace("–", "-");
                    _replacecontent = _replacecontent.Replace("—", "-");
                    _replacecontent = _replacecontent.Replace("-", "-");//
                    _replacecontent = _replacecontent.Replace("&#151;", "-");
                    _replacecontent = _replacecontent.Replace("&mdash;", "-");
                    _replacecontent = _replacecontent.Replace(">“", ">&quot;");
                    _replacecontent = _replacecontent.Replace("> “", ">&quot;");
                    _replacecontent = _replacecontent.Replace("” <", "&quot;<");
                    _replacecontent = _replacecontent.Replace("”<", "&quot;<");
                    _replacecontent = _replacecontent.Replace("½", "&#189;");
                    _replacecontent = _replacecontent.Replace("¼", "&#188;");
                    _replacecontent = _replacecontent.Replace("¾", "&#190;");
                    _replacecontent = _replacecontent.Replace("“", "&quot;");
                    _replacecontent = _replacecontent.Replace("”", "&quot;");
                    _replacecontent = _replacecontent.Replace("'", "&#96;");
                    _replacecontent = _replacecontent.Replace("’", "&#39;");
                    _replacecontent = _replacecontent.Replace("°", "&deg;");
                    _replacecontent = _replacecontent.Replace("ºC", "&deg;C");
                    _replacecontent = _replacecontent.Replace("™", "&trade;");
                    _replacecontent = _replacecontent.Replace("ö", "&#246;");
                    _replacecontent = _replacecontent.Replace("µ", "&#181;");
                    _replacecontent = _replacecontent.Replace("ü", "&#252;");
                    _replacecontent = _replacecontent.Replace("•", "&#8226;");
                    _replacecontent = _replacecontent.Replace("©", "&#169;");
                    _replacecontent = _replacecontent.Replace("®", "&reg;");
                    _replacecontent = _replacecontent.Replace("é", "&eacute;");
                    _replacecontent = _replacecontent.Replace("&#xa0;", "&nbsp;");
                    break;
                case 2:
                    _replacecontent = Content.Trim().Replace(" & ", " &amp; ");
                    _replacecontent = _replacecontent.Replace("</span> –", "</span> -");
                    _replacecontent = _replacecontent.Replace("</span>–", "</span> -");
                    _replacecontent = _replacecontent.Replace("–", "-");
                    _replacecontent = _replacecontent.Replace("-", "-");//
                    _replacecontent = _replacecontent.Replace("—", "-");
                    _replacecontent = _replacecontent.Replace("–", "-");
                    _replacecontent = _replacecontent.Replace("—", "-");
                    _replacecontent = _replacecontent.Replace("-", "-");//
                    _replacecontent = _replacecontent.Replace("&#151;", "-");
                    _replacecontent = _replacecontent.Replace("&mdash;", "-");
                    _replacecontent = _replacecontent.Replace(">“", ">&quot;");
                    _replacecontent = _replacecontent.Replace("> “", ">&quot;");
                    _replacecontent = _replacecontent.Replace("” <", "&quot;<");
                    _replacecontent = _replacecontent.Replace("”<", "&quot;<");
                    _replacecontent = _replacecontent.Replace("½", "&#189;");
                    _replacecontent = _replacecontent.Replace("¼", "&#188;");
                    _replacecontent = _replacecontent.Replace("¾", "&#190;");
                    _replacecontent = _replacecontent.Replace("“", "&quot;");
                    _replacecontent = _replacecontent.Replace("”", "&quot;");
                    _replacecontent = _replacecontent.Replace("’", "&#39;");
                    _replacecontent = _replacecontent.Replace("°", "&deg;");
                    _replacecontent = _replacecontent.Replace("ºC", "&deg;C");
                    _replacecontent = _replacecontent.Replace("™", "&trade;");
                    _replacecontent = _replacecontent.Replace("ö", "&#246;");
                    _replacecontent = _replacecontent.Replace("µ", "&#181;");
                    _replacecontent = _replacecontent.Replace("ü", "&#252;");
                    _replacecontent = _replacecontent.Replace("•", "&#8226;");
                    _replacecontent = _replacecontent.Replace("©", "&#169;");
                    _replacecontent = _replacecontent.Replace("®", "&reg;");
                    _replacecontent = _replacecontent.Replace("é", "&eacute;");
                    _replacecontent = _replacecontent.Replace("&#xa0;", "&nbsp;");
                    break;
                case 3:
                    _replacecontent = Content.Trim().Replace("&amp;", "&");
                    _replacecontent = _replacecontent.Replace("–", "-");
                    _replacecontent = _replacecontent.Replace("-", "-");//
                    _replacecontent = _replacecontent.Replace("—", "-");
                    _replacecontent = _replacecontent.Replace("–", "-");
                    _replacecontent = _replacecontent.Replace("—", "-");
                    _replacecontent = _replacecontent.Replace("-", "-");//
                    _replacecontent = _replacecontent.Replace("&#151;", "-");
                    _replacecontent = _replacecontent.Replace("&mdash;", "-");
                    _replacecontent = _replacecontent.Replace("&#189;", "½");
                    _replacecontent = _replacecontent.Replace("&#188;", "¼");
                    _replacecontent = _replacecontent.Replace("&#190;", "¾");
                    _replacecontent = _replacecontent.Replace("&quot;", "“");
                    _replacecontent = _replacecontent.Replace("&quot;", "”");
                    _replacecontent = _replacecontent.Replace("&#39;", "’");
                    _replacecontent = _replacecontent.Replace("&deg;", "°");
                    _replacecontent = _replacecontent.Replace("&deg;C", "ºC");
                    _replacecontent = _replacecontent.Replace("&trade;", "™");
                    _replacecontent = _replacecontent.Replace("&#246;", "ö");
                    _replacecontent = _replacecontent.Replace("&#181;", "µ");
                    _replacecontent = _replacecontent.Replace("&#252;", "ü");
                    _replacecontent = _replacecontent.Replace("&#8226;", "•");
                    _replacecontent = _replacecontent.Replace("&#169;", "©");
                    _replacecontent = _replacecontent.Replace("&reg;", "®");
                    _replacecontent = _replacecontent.Replace("&eacute;", "é");
                    _replacecontent = _replacecontent.Replace(" ", "-");
                    _replacecontent = _replacecontent.Replace("&", "and");
                    _replacecontent = _replacecontent.Replace(",", "-");
                    _replacecontent = _replacecontent.Replace(".", "");
                    _replacecontent = _replacecontent.Replace("/", "-");
                    _replacecontent = _replacecontent.Replace("'", "-");
                    _replacecontent = _replacecontent.Replace("’", "-");
                    _replacecontent = _replacecontent.Replace("'", "-");
                    _replacecontent = _replacecontent.Replace("’", "-");
                    _replacecontent = _replacecontent.Replace("°", "-");
                    _replacecontent = _replacecontent.Replace("™", "-");
                    _replacecontent = _replacecontent.Replace("®", "-");
                    _replacecontent = _replacecontent.Replace("#", "-");
                    _replacecontent = _replacecontent.Replace("^", "-");
                    _replacecontent = _replacecontent.Replace("$", "-");
                    _replacecontent = _replacecontent.Replace("|", "-");
                    _replacecontent = _replacecontent.Replace(":", "-");
                    _replacecontent = _replacecontent.Replace(";", "-");
                    _replacecontent = _replacecontent.Replace("<", "-");
                    _replacecontent = _replacecontent.Replace(">", "-");
                    _replacecontent = _replacecontent.Replace("?", "-");
                    _replacecontent = _replacecontent.Replace("[", "-");
                    _replacecontent = _replacecontent.Replace("]", "-");
                    _replacecontent = _replacecontent.Replace("ö", "-");
                    _replacecontent = _replacecontent.Replace("™", "-");
                    _replacecontent = _replacecontent.Replace("\"", "-");
                    _replacecontent = _replacecontent.Replace(@"\", "-");
                    _replacecontent = _replacecontent.Replace("-", "-");//
                    _replacecontent = _replacecontent.Replace("&#151;", "-");
                    _replacecontent = _replacecontent.Replace("&mdash;", "-");
                    _replacecontent = _replacecontent.Replace("&#189;", "-");
                    _replacecontent = _replacecontent.Replace("&#188;", "-");
                    _replacecontent = _replacecontent.Replace("&#190;", "-");
                    _replacecontent = _replacecontent.Replace("&quot;", "-");
                    _replacecontent = _replacecontent.Replace("&quot;", "-");
                    _replacecontent = _replacecontent.Replace("&#39;", "-");
                    _replacecontent = _replacecontent.Replace("&deg;", "-");
                    _replacecontent = _replacecontent.Replace("&deg;C", "-");
                    _replacecontent = _replacecontent.Replace("&trade;", "-");
                    _replacecontent = _replacecontent.Replace("&#246;", "-");
                    _replacecontent = _replacecontent.Replace("&#181;", "-");
                    _replacecontent = _replacecontent.Replace("&#252;", "-");
                    _replacecontent = _replacecontent.Replace("&#8226;", "-");
                    _replacecontent = _replacecontent.Replace("&#169;", "-");
                    _replacecontent = _replacecontent.Replace("&reg;", "-");
                    _replacecontent = _replacecontent.Replace("&eacute;", "-");
                    _replacecontent = _replacecontent.Replace("ü", "u");
                    _replacecontent = _replacecontent.Replace("©", "-");
                    _replacecontent = _replacecontent.Replace("é", "e");
                    _replacecontent = _replacecontent.Replace("---", "-");
                    _replacecontent = _replacecontent.Replace("--", "-");
                    break;
            }
            return _replacecontent;
        }

        public static string FirstWords(string input, int numberWords)
        {
            try
            {
                int words = numberWords;
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == ' ')
                    {
                        words--;
                    }
                    if (words == 0)
                    {
                        return input.Substring(0, i);
                    }
                }
            }
            catch (Exception)
            {
            }
            return input;
        }

        private static HttpClient client;
        public static HttpClient Client
        {
            get
            {
                if (client == null)
                {
                    client = new HttpClient();
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Api"].ToString());
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                return client;
            }
        }

        #region Mail Component

        #region Sending Mail 
        public static bool SendMail(string strFromAddress, string strToAddress, string strCCAddress, string strBCCAddress, string strSubject, string strBodyContent, bool IsBodyHtml)
        {
            try
            {
                if (ValidateMailAddress(ref strFromAddress) & ValidateMailAddress(ref strToAddress) & ValidateMailAddress(ref strSubject) & ValidateMailAddress(ref strBodyContent))
                {
                    string strUsername = System.Configuration.ConfigurationManager.AppSettings["MailServerUsername"];
                    string strPassword = System.Configuration.ConfigurationManager.AppSettings["MailServerpassword"];
                    //Create a new MailMessage object and specify the"From" and "To" addresses
                    MailMessage Email = new System.Net.Mail.MailMessage();
                    Email.From = new MailAddress(strFromAddress.ToString());
                    Email = EmailAddressCollection(strToAddress.ToString(), "TO", ref Email);

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
                    return true;
                }
                else
                    return false;
                
            }
            catch (Exception ex)
            {
                //CommonLogger.Info("Failed to send a Mail for Following MailAddress: From :" + strFromAddress + "  ToAddress :");
                //CommonLogger.Error("Failed to send a Mail fro following reason" + ex.StackTrace);
                return false;
            }
        }
        #endregion

        #region ValidateMailAddress
        private static bool ValidateMailAddress(ref string mailaddress)
        {
            return (string.IsNullOrEmpty(mailaddress) ? false : true);
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


        #endregion

        #region valid_ip_access

        public static bool isvalidIpAccess()
        {
            try
            {
                /*string[] allowedCountries = new string[] { "US", "UM", "CA", "MX", "IN" };
                string ipaddress = GetIPAddress();
                string url = "http://ip-api.com/json/" + ipaddress;
                string json = new System.Net.WebClient().DownloadString(url);

                string[] jsplit = json.Split(',');
                if (jsplit[3] != null)
                {
                    string ctrstr = jsplit[3].ToString();
                    string ctr = ctrstr.Substring(ctrstr.IndexOf(':') + 1).Replace('"', ' ').Trim();
                    if (Array.IndexOf(allowedCountries, ctr) >= 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                    return false;*/

                string[] allowedCountries = new string[] { "US", "UM", "CA", "MX", "IN", "United States", "Canada", "Mexico","India" };
                string ipaddress = GetIPAddress();

                string url = "http://ip-api.com/json/" + ipaddress;
                string json = new System.Net.WebClient().DownloadString(url);
                string[] jsplit = json.Split(',');
                
                if (json != null || json != "")
                {
                    int pos = json.IndexOf("countryCode");
                    if (pos > 1)
                    {
                        int pos1 = json.IndexOf(",", pos + 1);
                        if (pos1 == -1)
                            pos1 = json.Length;
                        string ctrstr = json.Substring(pos, pos1 - pos);
                        string ctr = ctrstr.Substring(ctrstr.IndexOf(':') + 1).Replace('"', ' ').Trim();
                        //Response.Write("ctrstr=" + ctrstr + "<br>ctr=" + ctr);
                        if (Array.IndexOf(allowedCountries, ctr) >= 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        #endregion

        public static string ApiURL
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Api"].ToString();
            }
        }

        public static string WebURL
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["WebURL"].ToString();
            }
        }

        public static string MetaRobots
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MetaRobots"].ToString();
            }
        }

        public static string ProfileEmailTestMode
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ProfileEmailTestMode"].ToString();
            }
        }


        public static string IQSEmployeeName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["IQSEmployeeName"].ToString();
            }
        }
        public static string ProfileFromEmailAddress
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ProfileFromEmailAddress"].ToString();
            }
        }
        public static string ProfileCCEmailAddress
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ProfileCCEmailAddress"].ToString();
            }
        }
        public static string ProfileEmailSubject
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ProfileEmailSubject"].ToString();
            }
        }
        public static string ProfileNonExistEmailSubject
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ProfileNonExistEmailSubject"].ToString();
            }
        }

        public static string RFQTestMode
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["RFQTestMode"].ToString();
            }
        }

        public static string RFQFromMailID
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["RFQFromMailID"].ToString();
            }
        }

        public static string RFQCCMailID
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["RFQCCMailID"].ToString();
            }
        }

        public static string RFQAlternateMailID
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["RFQAlternateMailID"].ToString();
            }
        }

        public static string RFQSubject
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["RFQSubject"].ToString();
            }
        }

        public static string AttachmentDetail
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AttachmentDetail"].ToString();
            }
        }

        public static string ReviewTestMode
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ReviewTestMode"].ToString();
            }
        }
        public static string ReviewUserRegisterMailID
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ReviewUserRegisterMailID"].ToString();
            }
        }
        public static string ReviewUserRegisterSubject
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ReviewUserRegisterSubject"].ToString();
            }
        }
        public static string ReviewUserRegisterTo
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ReviewUserRegisterTo"].ToString();
            }
        }
        public static string ReviewUserRegisterCC
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ReviewUserRegisterCC"].ToString();
            }
        }
        public static string ReviewUserRegisterBCC
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ReviewUserRegisterBCC"].ToString();
            }
        }
        public static string ReviewEmailNewSubject
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ReviewEmailNewSubject"].ToString();
            }
        }
        public static string ReviewEmailNewReplySubject
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ReviewEmailNewReplySubject"].ToString();
            }
        }
        public static string ForgotPasswordSubject
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ForgotPasswordSubject"].ToString();
            }
        }


        public static string ListYourCompanyEmailTestMode
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ListYourCompanyEmailTestMode"].ToString();
            }
        }
        public static string ListYourCompanyPremiumSubject
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ListYourCompanyPremiumSubject"].ToString();
            }
        }
        public static string ListYourCompanyStandardSubject
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ListYourCompanyStandardSubject"].ToString();
            }
        }
        public static string ListYourCompanyFromMailID
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ListYourCompanyFromMailID"].ToString();
            }
        }
        public static string ListYourCompanyToMailID
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ListYourCompanyToMailID"].ToString();
            }
        }
        public static string ListYourCompanyCCMailID
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ListYourCompanyCCMailID"].ToString();
            }
        }
        


        public static string TestEmailTo
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["TestEmailTo"].ToString();
            }
        }
        public static string TestEmailCC
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["TestEmailCC"].ToString();
            }

        }
        public static string TestEmailBCC
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["TestEmailBCC"].ToString();
            }
        }
        public static string TestEmailSubjectPrefix
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["TestEmailSubjectPrefix"].ToString();
            }
        }

    }
}