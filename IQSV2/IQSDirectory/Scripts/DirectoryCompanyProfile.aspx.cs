using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using IQS.Service;
using IQS.Utility;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;

public partial class DirectoryCompanyProfile : System.Web.UI.Page
{
    public static string rootDirPath = "";
    public static string CompRating = "";
    public static string CompCount = ""; 
    public static string shareURL = "";
    public static string strDirectoryCompanyProfilePath = "http://www.iqsdirectory.com/";
    string compName = "";
    int clientSK = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ParsingURL();
            if (clientSK > 0)
            {
                FetchData();
            }
            else
            {
                Server.ClearError();
                Response.Clear();
                Response.Status = "301 Moved Permanently";
                Response.Redirect("~", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            
        }
    }

    private void FetchData()
    {
        IProfileService objIProfileService = DelegateFactory.Current.ProfileService;
        bool hasRedirected = false;
        try
        {
            DataSet dsProfile = objIProfileService.GetClientProfileDetails(clientSK);
            //Response.Write("<!--"+dsProfile.Tables.Count.ToString()+"-->");
            hdnProfileClientSk.Value = lblProfileClientSk.Text;
            
            if (dsProfile.Tables[0].Rows.Count > 0)
            {
                //Response.Write(lblProfileClientSk.Text);
                string qryString = Request.QueryString[0].Trim('/');
                Response.Write("<!--" +dsProfile.Tables[0].Rows[0]["THIS_COPRO_URL"].ToString().Trim('/') + "!=" + qryString + "-->");
                if (dsProfile.Tables[0].Rows[0]["THIS_COPRO_URL"].ToString().Trim('/') != qryString)
                {
                    /*Response.Clear();
                    Response.ClearHeaders();
                    Response.Buffer = false;*/
                    Server.ClearError();
                    hasRedirected = true;
                    Response.Clear();
                    Response.Status = "301 Moved Permanently";
                    Response.Redirect(strDirectoryCompanyProfilePath + dsProfile.Tables[0].Rows[0]["THIS_COPRO_URL"].ToString(), false);
                    Context.ApplicationInstance.CompleteRequest();

                
                 
                }
                if (hasRedirected == false)
                {
                    CompRating = dsProfile.Tables[0].Rows[0]["RATINGAVG"].ToString();
                    CompCount = dsProfile.Tables[0].Rows[0]["RATINGCOUNT"].ToString();
                    if (dsProfile.Tables[0].Rows[0]["SHOW_REVIEWS"].ToString() != "Y")
                        ctrlCompanyProfileReviews.Visible = false;
                    this.Page.Title = Utils.ReplaceContent(dsProfile.Tables[0].Rows[0]["Name"].ToString(), 1);
                    compName = Utils.FormatCompanyWebsiteLink(dsProfile.Tables[0].Rows[0]["Name"].ToString());
                    AddMetaTag(dsProfile.Tables[0].Rows[0]);
                    if (dsProfile.Tables.Count >= 8)
                        AddHeaders(dsProfile.Tables[6], dsProfile.Tables[7]);
                    else
                        AddHeaders(null, dsProfile.Tables[dsProfile.Tables.Count - 1]);
                    AddCompanyDetails(dsProfile.Tables[0].Rows[0]); 
                    //return;
                    AddCompanyUrls(dsProfile.Tables[1], dsProfile.Tables[0].Rows[0]);
                    AddCompanyAddress(dsProfile.Tables[2], dsProfile.Tables[0].Rows[0]);
                    AddGoogleMap(dsProfile.Tables[0].Rows[0]);
                    AddCompanyResources(dsProfile.Tables[3], dsProfile.Tables[0].Rows[0]);
                    AddRelatedClients(dsProfile.Tables[5], dsProfile.Tables[0].Rows[0]["CTYPE"].ToString());
                    AddAdditionalInfo(dsProfile.Tables[4]);
                    AddTradeNames(dsProfile.Tables[0].Rows[0]);
                    litScript.Text = "<script src='" + hdnRootPath.Value + "iqs_tracker/js/tracker.js?client_id=-1' type='text/javascript' ></script>";
                }
            }
            else
            {
                if (hasRedirected == false)
                {
                    Server.ClearError();
                    hasRedirected = true;
                    Response.Clear();
                    Response.Status = "301 Moved Permanently";
                    Response.Redirect(strDirectoryCompanyProfilePath, false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            
        }
        catch (Exception ex)
        {
            CommonLogger.Error("ClientProfile", ex);
            throw ex;
        }
    }

    private void ParsingURL()
    {
        bool IsInvalidSearch = false;
        

        if (Request.QueryString.Count == 0)
        {
            IsInvalidSearch = true;
        }
        else if (Request.QueryString["urlpath"] == null)
        {
            IsInvalidSearch = true;
        }
        else if (!Request.QueryString[0].ToString().ToUpper().Contains("PROFILE/") && !Request.QueryString[0].ToString().ToUpper().Contains("COPRO/"))
        {
            IsInvalidSearch = true;
        }

        else
        {
            string qryString = Request.QueryString[0].Trim('/');
            
            try
            {
                string[] strCopro = qryString.Split('/');
                if (strCopro.Length == 2)
                {
                    if (strCopro[0].ToUpper() == "PROFILE" || strCopro[0].ToUpper() == "COPRO")
                    {
                        Int64 outVal;
                        if (strCopro[0].ToUpper() == "PROFILE")
                        {
                            string[] strCopro1 = strCopro[1].Split('-');
                            int coprolen = strCopro1.Length - 1;
                            if (Int64.TryParse(strCopro1[coprolen], out outVal))
                            {
                                lblProfileClientSk.Text = strCopro1[coprolen].ToString();
                                clientSK = Convert.ToInt32(lblProfileClientSk.Text);
                                shareURL = strDirectoryCompanyProfilePath + qryString + "/";
                            }
                            else
                            {
                                IsInvalidSearch = true;
                            }
                        }
                        else if (strCopro[0].ToUpper() == "COPRO")
                        {
                            string[] strCopro1 = strCopro[1].Split('-');
                            if (Int64.TryParse(strCopro1[0], out outVal))
                            {
                                lblProfileClientSk.Text = strCopro1[0].ToString();
                                clientSK = Convert.ToInt32(lblProfileClientSk.Text);
                                /*string cname = qryString.Replace("copro/", "profile/");
                                cname = cname.Replace(strCopro[0].ToString() + "-", "");
                                Response.Status = "301 Moved Permanently";
                                Response.Redirect(strDirectoryCompanyProfilePath + cname + "-" + strCopro[0].ToString() + "/");*/
                            }
                            else
                            {
                                IsInvalidSearch = true;
                            }
                        }
                        else
                        {
                            IsInvalidSearch = true;
                        }
                    }
                    else
                    {
                        IsInvalidSearch = true;
                    }
                }
                else
                {
                    IsInvalidSearch = true;
                }
            }
            catch
            {
                IsInvalidSearch = true;
            }
        }

        if (IsInvalidSearch == true)
        {
            Server.ClearError();
            Response.Clear();
            Response.Status = "301 Moved Permanently";
            Response.Redirect("~", false);
            Context.ApplicationInstance.CompleteRequest();
            
        }
    }

    private string getIPState(string address)
    {
        WebRequest request = (HttpWebRequest)WebRequest.Create(address);
        request.Timeout = 500;
        string s = "";
       // return s;
        try
        {
            using (WebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response != null)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                        string json = reader.ReadToEnd().ToString();
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        Dictionary<string, string> obj = jss.Deserialize<Dictionary<string, string>>(json);
                        s = obj["region_code"].ToString().Trim(); //"Carnegie, IN";
                        return s;
                    }
                }
                else
                {
                    return s;
                }
            }
        }
        catch (WebException ex)
        {
            s = "";
            return s;
        }


    }

    private void AddMetaTag(DataRow dr)
    {
        HtmlMeta htmlMeta = new HtmlMeta();
        htmlMeta.Name = "keywords";
        htmlMeta.Content = dr["Name"].ToString() + ", " + dr["Name"].ToString() + " in " + dr["CITY"].ToString() + ", " + dr["Name"].ToString() + " in " + dr["STATE"].ToString() + ", " + dr["Name"].ToString() + " in " + dr["CITY"].ToString() + " " + dr["STATE"].ToString() + ", " + dr["Name"].ToString() + " " + dr["ADDRESS"].ToString();
        this.Page.Header.Controls.AddAt(1, htmlMeta);

        htmlMeta = new HtmlMeta();
        htmlMeta.Name = "description";
        htmlMeta.Content = "Find information on " + dr["Name"].ToString() + " on IQSdirectory. Request information on " + dr["Name"].ToString() + " located at " + dr["ADDRESS"].ToString() + "," + dr["CITY"].ToString() + "," + dr["STATE"].ToString() + ".";
        this.Page.Header.Controls.AddAt(1, htmlMeta);
        string index = "NOINDEX";
        string nofollow = "FOLLOW";
        if (dr["ISINDEX"].ToString() == "Y")
            index = "INDEX";
        //if (dr["ISNOFOLLOW"].ToString() == "Y")
            nofollow = "FOLLOW";
        this.Page.Header.Controls.AddAt(1, new LiteralControl("<meta name='robots' content='" + index + "," + nofollow + "'>"));
    }

    private void AddHeaders(DataTable dtGoogle, DataTable dtScripts)
    {
        rootDirPath = "";
        string url = Request.QueryString[0];
        int slashCount = url.Split('/').Length - 1;
        for (int i = 0; i < slashCount - 1; i++)
            rootDirPath += "../";
        hdnRootPath.Value = rootDirPath;

        if (dtScripts != null)
        {
            if (dtScripts.Rows.Count > 0)
            {
                if (dtScripts.Rows[0]["BODY_START_SCRIPT"].ToString() != "")
                     this.Page.Header.Controls.Add(new LiteralControl(dtScripts.Rows[0]["HEAD_SCRIPT"].ToString()));
                if (dtScripts.Rows[0]["BODY_START_SCRIPT"].ToString() != "")
                    bodyTopScripts.InnerHtml = dtScripts.Rows[0]["BODY_START_SCRIPT"].ToString();
                if (dtScripts.Rows[0]["BODY_BFR_CLOSE_SCRIPT"].ToString() != "")
                    bodyBottomScripts.InnerHtml = dtScripts.Rows[0]["BODY_BFR_CLOSE_SCRIPT"].ToString();
                

            }
           
        }

       

        //this.Page.Header.Controls.Add(new LiteralControl("<link href='" + rootDirPath + "css/cstyle.css'  rel='Stylesheet' type='text/css' />"));
        this.Page.Header.Controls.Add(new LiteralControl("<link href='" + rootDirPath + "css/publish_styles.css'  rel='Stylesheet' type='text/css' />"));
        this.Page.Header.Controls.Add(new LiteralControl("<link defer href='" + rootDirPath + "css/jquery-ui.css'  rel='Stylesheet' type='text/css' media='screen' />"));
        this.Page.Header.Controls.Add(new LiteralControl("<link defer href='" + rootDirPath + "css/jquery.fancybox-1.3.4.css'  rel='Stylesheet' type='text/css' media='screen' />"));
        this.Page.Header.Controls.Add(new LiteralControl("<script async src='" + rootDirPath + "js/respond.min.js' type='text/javascript'></script>"));
        //this.Page.Header.Controls.Add(new LiteralControl("<script src='" + rootDirPath + "js/html5shiv.js' type='text/javascript'></script>"));
        //this.Page.Header.Controls.Add(new LiteralControl("<script src='" + rootDirPath + "js/html5shiv-printshiv.js' type='text/javascript'></script>"));
        this.Page.Header.Controls.Add(new LiteralControl("<script async defer src='" + rootDirPath + "js/common.min.js' type='text/javascript' ></script>"));
        this.Page.Header.Controls.Add(new LiteralControl("<script src='" + rootDirPath + "js/jquery-1.7.2.min.js' type='text/javascript' ></script>"));
        this.Page.Header.Controls.Add(new LiteralControl("<script async src='" + rootDirPath + "js/json2.js' type='text/javascript' ></script>"));
        this.Page.Header.Controls.Add(new LiteralControl("<script async defer src='" + rootDirPath + "js/jquery.fancybox-1.3.4.js' type='text/javascript' ></script>"));
        this.Page.Header.Controls.Add(new LiteralControl("<script async src='" + rootDirPath + "js/jquery.rating.pack.js' type='text/javascript' ></script>"));
        this.Page.Header.Controls.Add(new LiteralControl("<script async defer src='" + rootDirPath + "js/jquery.cookie.js' type='text/javascript' ></script>"));
        this.Page.Header.Controls.Add(new LiteralControl("<script src='" + rootDirPath + "js/jquery-ui.js' type='text/javascript' ></script>"));
        //this.Page.Header.Controls.Add(new LiteralControl("<script defer src='" + rootDirPath + "js/all.js' type='text/javascript' ></script>"));
        this.Page.Header.Controls.Add(new LiteralControl("<script src='https://connect.facebook.net/en_US/all.js'></script>"));
        this.Page.Header.Controls.Add(new LiteralControl("<script defer src='" + rootDirPath + "js/directoryprofile.js' type='text/javascript'></script>"));
        
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<script type='text/javascript'>");
        sb.AppendLine("$(document).ready(function () {");
        sb.AppendLine("$.get('" + rootDirPath + "statesearch.htm', function (data) {");
        sb.AppendLine("$('#headsearch').html(data);");
        sb.AppendLine("});");
        sb.AppendLine("insertCoproClick($('#hdnProfileClientSk').val());");
        sb.AppendLine("$('.lnkmail').fancybox({'height':350,'width':400,'onStart':function(){$('body').css('overflow','hidden');},'onClosed':function(){$('body').css('overflow','auto');},'hideOnOverlayClick':false});");
        sb.AppendLine("$('.lnkmail').bind('contextmenu', function(e){return false;});");
        sb.AppendLine("});");
        //sb.AppendLine("hs.graphicsDir = '" + rootDirPath + "highslide/graphics/'");
        //sb.AppendLine("hs.showCredits = false;");
        sb.AppendLine("</script>");
        this.Page.Header.Controls.Add(new LiteralControl(sb.ToString()));
        
           

        sb = new StringBuilder();
        sb.AppendLine("<script type=\"text/javascript\">");
        sb.AppendLine("function popupwindow(url, title, w, h) {");
        sb.AppendLine("var left = (screen.width/2)-(w/2);");
        sb.AppendLine("var top = (screen.height/2)-(h/2);");
        sb.AppendLine("window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width='+w+', height='+h+', top='+top+', left='+left);");
        sb.AppendLine("}");
        /*sb.AppendLine("FB.init({");
        sb.AppendLine("appId: '221653014637602',");
        sb.AppendLine("channelUrl: '" + HttpContext.Current.Request.Url.AbsoluteUri + "channel.html',");
        sb.AppendLine("scope: 'id,name,email',");
        sb.AppendLine("status: true,");
        sb.AppendLine("cookie: true,");
        sb.AppendLine("xfbml: true");
        sb.AppendLine("});");*/
        sb.AppendLine("function postToFeed(cname, title, review) {");
        sb.AppendLine("var lnkurl = $(location).attr('href');");
        sb.AppendLine("var obj = {");
        sb.AppendLine("method: 'feed',");
        sb.AppendLine("redirect_uri: '" + strDirectoryCompanyProfilePath + "',");
        sb.AppendLine("link: lnkurl,");
        sb.AppendLine("picture: 'http://www.iqsdirectory.com/images/iqsdirectory_home_logo.png',");
        sb.AppendLine("name: cname,");
        sb.AppendLine("caption: title,");
        sb.AppendLine("description: review");
        sb.AppendLine("};");
        sb.AppendLine("function callback(response) {");
        sb.AppendLine("}");
        sb.AppendLine("FB.ui(obj, callback);");
        sb.AppendLine("}");
        sb.AppendLine("</script>");
        this.Page.Header.Controls.Add(new LiteralControl(sb.ToString()));
        this.Page.Header.Controls.Add(new LiteralControl("<script defer src='" + rootDirPath + "js/move_top.js' type='text/javascript'></script>"));
             
        sb = new StringBuilder();
        string title = "", metdesc= "";
        sb.AppendLine("<div class='headRel'>Share This Page On</div>");
        sb.AppendLine("<div style='width:96%; padding:10px 2%; height:auto; overflow:hidden; padding:'>");
        sb.AppendLine("<a class=\"iqsnewsroom\" href=\"http://blog.iqsdirectory.com\" target=\"_blank\" >");
        sb.AppendLine("IQS Newsroom</a>");

        sb.AppendLine("<a rel=nofollow href=\"https://plus.google.com/share?url=" + shareURL + "\" onclick=\"javascript:popupwindow(this.href,'',600,600);return false;\">");
        sb.AppendLine("<img src=\"" + rootDirPath + "images/google.png\" alt=\"Google+\" title='Google+' /></a>");
        //twitter
        sb.AppendLine("<a rel=nofollow href=\"https://twitter.com/share?url=" + shareURL + "&text=%23" + title + ". " + metdesc + "\" onclick=\"javascript:popupwindow(this.href,'',600,400);return false;\" >");
        sb.AppendLine("<img src=\"" + rootDirPath + "images/twitter.png\" alt=\"Twitter\" title=\"Twitter\"/></a>");
        //linkedin
        sb.AppendLine("<a rel=nofollow href=\"http://www.linkedin.com/shareArticle?mini=true&url=" + shareURL + "&title=" + title + "&summary=" + metdesc + "&source=" + strDirectoryCompanyProfilePath + "\" target='_top' onclick=\"javascript:popupwindow(this.href,'',600,400);return false;\">");
        sb.AppendLine("<img src=\"" + rootDirPath + "images/linkedin.png\" alt=\"Linked In\" title=\"Linked In\" /></a>");
        //facebook
        sb.AppendLine("<a rel=nofollow href=\"#\" onclick=\"javascript:postToFeed('" + title + "','" + strDirectoryCompanyProfilePath + "','" + metdesc + "');return false;\">");
        sb.AppendLine("<img src=\"" + rootDirPath + "images/facebook.png\" alt=\"FB\" title=\"FB\"/></a>");
        //mail
        sb.AppendLine("<a rel=nofollow class=\"iframe lnkmail\" href=\"" + rootDirPath + "controls/MailSend.aspx?p=" + rootDirPath + "&title=" + title + "&des=" + metdesc + "&url=" + shareURL + "\">");
        sb.AppendLine("<img src=\"" + rootDirPath + "images/mail.png\" alt=\"Mail\" title=\"Mail\"/></a>");
        //print
        sb.AppendLine("<a rel=nofollow href=\"\" onclick=\"javascript:window.print();return false;\">");
        sb.AppendLine("<img src=\"" + rootDirPath + "images/print.png\" alt=\"Print\" title=\"Print\"/></a>");
        sb.AppendLine("</div>");
        divSocial.InnerHtml = sb.ToString();

        /*if (dtGoogle != null)
        {
            if (dtGoogle.Rows.Count > 0)
            {
                sb = new StringBuilder();
                sb.AppendLine("<script type='text/javascript'>");
                sb.AppendLine(dtGoogle.Rows[0]["DESCRIPTION"].ToString());
                sb.AppendLine("</script>");
                this.Page.Header.Controls.Add(new LiteralControl(sb.ToString()));
            }
        }*/
    }

    private void AddCompanyDetails(DataRow dr)
    {
        divCompName.InnerHtml = Utils.ReplaceContent(dr["Name"].ToString(), 1);
        divEmailCName.InnerHtml = "<h2>Email " + Utils.ReplaceContent(dr["Name"].ToString(), 1) + "</h2>";
        if (dr["COPRO_VIDEO"].ToString() != "")
        {
            string sName = "";
            if (Regex.IsMatch(dr["COPRO_VIDEO"].ToString(), @"^http:\/\/(?:www\.)?youtube.com\/watch\?(?=.*v=\w+)(?:\S+)?$", RegexOptions.Singleline | RegexOptions.IgnoreCase))
            {
                Uri ytUri = new Uri(dr["COPRO_VIDEO"].ToString());
                sName = HttpUtility.ParseQueryString(ytUri.Query).Get("V".ToLower());
            }
            else if (Regex.IsMatch(dr["COPRO_VIDEO"].ToString(), @"^http:\/\/(?:www\.)?youtu.be\/([\w-]{10,12})$", RegexOptions.Singleline | RegexOptions.IgnoreCase))
            {
                sName = Regex.Replace(dr["COPRO_VIDEO"].ToString(), @"http:\/\/(?:www\.)?youtu.be\/", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            }
            if (sName != "")
            {
                divVideo.Attributes.Add("style", "display:block");
                divyoutube.Attributes.Add("style", "background-image:url(http://img.youtube.com/vi/" + sName + "/0.jpg); background-size:100% auto;");
                lnkViewVideo.Attributes.Add("href", rootDirPath + "controls/coprovideo.htm?v=" + sName + "&comp=" + dr["NAME"].ToString());
                divDescMaster.Attributes.Add("class", "divfloatleft coprorightdiv");
            }
        }
        string strdescr = Utils.ReplaceContent(dr["DESCRIPTION"].ToString(), 1);
        strdescr =  "<p>" + strdescr.Replace(Environment.NewLine, "</p><p>").Replace("</p><p>", "</p>");
        divDescription.InnerHtml = strdescr;

    }

    private void AddCompanyUrls(DataTable dt, DataRow drProf)
    {
        string noFollow = "", outBound = "";
        if (drProf["META_TAG_Value"].ToString() != "")
        {
            if (drProf["ISOUTBOUNDSCRIPT"].ToString() == "Y")
            {
                //divoutbound.InnerHtml = drProf["META_TAG_Value"].ToString();
                if (drProf["META_TAG_Value"].ToString().Contains("msnTracker"))
                {
                    //outBound = " onClick='outboundtracker(); msnTracker();' ";
                }
                else
                {
                   // outBound = " onClick='outboundtracker();' ";
                }
            }
        }
        if (drProf["ISNOFOLLOW"].ToString() == "Y")
            noFollow = " rel='nofollow' ";
        StringBuilder sb = new StringBuilder();
        /*foreach (DataRow dr in dt.Rows)
        {
            if (Convert.ToBoolean(dr.ItemArray[1].ToString()) == true)
            {
                sb.Append("<span class='DPFCompanyResource1'>" + dr.ItemArray[0].ToString() + "</span>,");
            }
            else
            {
                string addHttp = dr.ItemArray[0].ToString().ToLower().StartsWith("http://") ? "" : "http://";
                sb.Append("<a " + noFollow + outBound + "alt='" + compName + "' title='" + compName + "' href='" + addHttp + dr.ItemArray[0].ToString() + "' class='DPFCompanyResource1' target='_blank' >" + dr.ItemArray[0].ToString() + "</a>" + ",");
            }
        }*/
        string mainurl = "";
        foreach (DataRow dr in dt.Rows)
        {
            if (Convert.ToBoolean(dr.ItemArray[1].ToString()) == true)
            {
                sb.Append("<span class='DPFCompanyResource1'>" + dr.ItemArray[0].ToString() + "</span>,");
            }
            else
            {
                string addHttp = dr.ItemArray[0].ToString().ToLower().StartsWith("http://") ? "" : "http://";
                string thisurl = addHttp + dr.ItemArray[0].ToString();
                if (mainurl == "")
                    mainurl = thisurl;
                else if (mainurl != "" && thisurl.Length < mainurl.Length)
                    mainurl = thisurl;

            }
        }
        if (mainurl != "")
        {
            if (clientSK == 63659) //Client wants only the main host url in the display and the landing url buried in href
            {
                string[] urldisp = mainurl.Replace("http://", "").Split('/');               
                sb.Append("<a " + noFollow + "alt='" + compName + "' title='" + compName + "' href='" + mainurl + "' class='DPFCompanyResource1' target='_blank' >" + urldisp[0] + "</a>" + ",");
                
            }
            else
            sb.Append("<a " + noFollow + "alt='" + compName + "' title='" + compName + "' href='" + mainurl + "' class='DPFCompanyResource1' target='_blank' >" + mainurl.Replace("http://", "") + "</a>" + ",");
        }
        divCompUrls.InnerHtml = sb.ToString().TrimEnd(',') + "<meta itemprop='url' content='" + mainurl + "'/>";
    }

    private void AddCompanyAddress(DataTable dt, DataRow dr)
    {
        if (dr["COPRO_IMAGE"].ToString() != "")
        {
            string coproImgUrl = rootDirPath + @"images\profimages\" + dr["COPRO_IMAGE"].ToString();
            divImage.InnerHtml = "<img class='coproimgsize' src='" + coproImgUrl + "' alt='" + compName + "' title='" + compName + "' itemprop='logo' />";
            divImage.Attributes.Add("style", "display:block");
            divPhone.Attributes.Add("class", "adress_spacer");
        }
        StringBuilder sb = new StringBuilder();
        string phone = "";
        foreach (DataRow drPhone in dt.Rows)
            phone += drPhone.ItemArray[0].ToString() + ",";
        if (phone.Length > 0)
            sb.AppendLine("<b>Phone: </b>" + phone.TrimEnd(',') + "<br/>");
        if (dr["FAX_NUMBER"].ToString() != "")
            sb.AppendLine("<b>Fax: </b>" + dr["FAX_NUMBER"].ToString());
        divPhone.InnerHtml = sb.ToString();

        lblAddress.Text = (dr["CITY"].ToString() != "") ? dr["CITY"].ToString() : string.Empty;
        lblAddress.Text = (dr["STATE"].ToString() != "") ? lblAddress.Text.Trim() + ", " + dr["STATE"].ToString() : dr["STATE"].ToString();
        lblAddress.Text = lblAddress.Text.Trim() + " " + dr["ZIP"].ToString();
        if (dr["ADDRESS"].ToString() != "")
            lblAddress.Text = dr["ADDRESS"].ToString().Trim() + "<br/>" + lblAddress.Text;
    }

    private void AddGoogleMap(DataRow dr)
        {
        string strAdd = dr["Address"].ToString() + "," + dr["CITY"].ToString() + "," + dr["STATE"].ToString() + "," + dr["ZIP"].ToString();
        lnkViewMap.Attributes.Add("href", rootDirPath + "controls/copromap.htm?address=" + strAdd + "&comp=" + dr["NAME"].ToString());
    }

    private void AddCompanyResources(DataTable dt, DataRow drProf)
    {
        if (dt.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            string noFollow = "", outBound = "";
            // if (drProf["ISOUTBOUNDSCRIPT"].ToString() == "Y")
                //outBound = " onClick='outboundtracker();' ";
            if (drProf["ISNOFOLLOW"].ToString() == "Y")
                noFollow = " rel='nofollow' ";
            DataRow[] drArr = dt.Select("WEBSITE_TYPE='BOTH' OR WEBSITE_TYPE='DIRECTORY'");
            sb.AppendLine("<b>Company Resources: </b>");
            int i = 0;
            foreach (DataRow dArr in drArr)
            {
                if (i == 1)
                    sb.AppendLine("<span class='linkBlue'>|</span>");
                string addHttp = dArr.ItemArray[0].ToString().ToLower().StartsWith("http://") ? "" : "http://";
                sb.AppendLine("<a href ='" + addHttp + dArr.ItemArray[2].ToString() + "' " + noFollow + " target='_blank' class='DPFCompanyresourceLink' >" + dArr.ItemArray[1].ToString() + "</a>");
            }
            divResources.InnerHtml = sb.ToString();
            divResources.Attributes.Add("style", "display:block;");
        }
    }

    private void AddRelatedClients(DataTable dt, string CType)
    {
        //WebClient wc = new WebClient();
        string json = "", citycode = "";
        try
        {
            /*json = wc.DownloadString("http://api.hostip.info/get_json.php");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, string> obj = jss.Deserialize<Dictionary<string, string>>(json);
            citycode = obj["city"].ToString(); //"Carnegie, IN";
            string[] city = citycode.ToString().Split(',');
            if (city.Length > 1)
                citycode = city[1].Trim().ToString();*/
            citycode = getIPState("https://freegeoip.net/json/");
        }
        catch (Exception)
        {
        }
        //citycode = "PA";
        StringBuilder sb = new StringBuilder();
        string printed = "";
        DataRow[] drSel = dt.Select("RATECOUNT > 0 OR RATESUM > 0", "RATESUM DESC, RATECOUNT DESC, CLIENT_NAME ASC");
        foreach (DataRow dr in drSel)
        {
            if (dr["CLIENT_SK"].ToString() == hdnProfileClientSk.Value)
                sb.AppendLine("<li>" + dr["CLIENT_NAME"].ToString() + "</li>");
            else
                sb.AppendLine("<li><a href='" + rootDirPath + dr["DESCRIPTION"].ToString().Trim('/') + "/'  target='_blank'>" + dr["CLIENT_NAME"].ToString() + "</a></li>");
            printed += dr["CLIENT_SK"].ToString() + ",";
        }
        string qry = "";
        if (printed.TrimEnd(',') != "")
            qry = "AND CLIENT_SK NOT IN (" + printed.TrimEnd(',') + ")";
        drSel = dt.Select("STATE = '" + citycode.ToUpper() + "' " + qry, "CLIENT_NAME ASC");
        foreach (DataRow dr in drSel)
        {
            if (dr["CLIENT_SK"].ToString() == hdnProfileClientSk.Value)
                sb.AppendLine("<li>" + dr["CLIENT_NAME"].ToString() + "</li>");
            else
                sb.AppendLine("<li><a href='" + rootDirPath + dr["DESCRIPTION"].ToString().Trim('/') + "/'  target='_blank'>" + dr["CLIENT_NAME"].ToString() + "</a></li>");
            printed += dr["CLIENT_SK"].ToString() + ",";
        }
        if (printed.TrimEnd(',') != "")
            drSel = dt.Select("CLIENT_SK NOT IN (" + printed.TrimEnd(',') + ")", "CLIENT_NAME ASC");
        else
            drSel = dt.Select("", "CLIENT_NAME ASC");
        foreach (DataRow dr in drSel)
        {
            if (dr["CLIENT_SK"].ToString() == hdnProfileClientSk.Value)
                sb.AppendLine("<li>" + dr["CLIENT_NAME"].ToString() + "</li>");
            else
                sb.AppendLine("<li><a href='" + rootDirPath + dr["DESCRIPTION"].ToString().Trim('/') + "/'  target='_blank'>" + dr["CLIENT_NAME"].ToString() + "</a></li>");
            printed += dr["CLIENT_SK"].ToString() + ",";
        }
        ulRelated.InnerHtml = sb.ToString();
        string CTypeEnd = "";
        if (CType.Length > 0)
            CTypeEnd = (CType.Substring(CType.Length - 1).ToUpper() == "S") ? "" : "s";
        else
            CType = "Manufacturers";
        divRelatedCap.InnerHtml = "Find Related " + CType + CTypeEnd;
        if (dt.Rows.Count > 0)
            divRelated.Attributes.Add("style", "display:block;");
    }

    private void AddAdditionalInfo(DataTable dt)
    {
        StringBuilder sbLeft = new StringBuilder();
        StringBuilder sbRight = new StringBuilder();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i % 2 == 0)
            {
                sbLeft.AppendLine("<li><a href='" + rootDirPath + dt.Rows[i][2].ToString().TrimStart('/') + "/'  target='_blank' >" + dt.Rows[i][0].ToString() + "</a></li>");
            }
            else
            {
                sbRight.AppendLine("<li><a href='" + rootDirPath + dt.Rows[i][2].ToString().TrimStart('/') + "/'  target='_blank' >" + dt.Rows[i][0].ToString() + "</a></li>");
            }
        }
        ulAddLeft.InnerHtml = sbLeft.ToString();
        ulAddRight.InnerHtml = sbRight.ToString();
        if (dt.Rows.Count > 0)
            divAddInfo.Attributes.Add("style", "display:block;");
    }
    private void AddTradeNames(DataRow dr)
    {
        StringBuilder sbLeft = new StringBuilder();
        StringBuilder sbRight = new StringBuilder();
        string desc = Utils.ReplaceContent(dr["TRADE_NAMES"].ToString().Trim(), 1);
        string[] tradeName = new string[100];
        tradeName = desc.Split(',');
        for (int i = 0; i < tradeName.Length; i++)
        {
            if (i % 2 == 0)
            {
                sbLeft.AppendLine("<li>" + tradeName[i].ToString() + "</li>");
            }
            else
            {
                sbRight.AppendLine("<li>" + tradeName[i].ToString() + "</li>");
            }
        }
        if (dr["TRADE_NAMES"].ToString().Length > 0)
            divTradeNames.Attributes.Add("style", "display:block;");
    }
}