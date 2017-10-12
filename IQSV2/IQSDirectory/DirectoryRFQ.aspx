<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DirectoryRFQ.aspx.cs" Inherits="IQSDirectory.DirectoryRFQ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RFQ</title>
    <meta name="robots" content="noindex, nofollow" />
    <link href="Content/publish_styles.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script src="Scripts/jquery-1.7.2.min.js" type="text/javascript" ></script>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    <script type="text/javascript"  src="Scripts/jquery.validate.js"></script>
    <script type="text/javascript"  src="Scripts/DirectoryRFQ.js"></script>
    <script src='https://www.google.com/recaptcha/api.js'></script>
<!--[if IE 5]>
<style type='text/css'> 
/* IE 5 does not use the standard box model, so the column widths are overidden to render the page correctly. */
#outerWrapper #contentWrapper #rightColumn1 { width: 14em;}
</style>
<![endif]-->
<!--[if IE 6]>
<style type='text/css'>
/* The proprietary zoom property gives IE the hasLayout property which addresses several bugs. */
#outerWrapper #contentWrapper #content {  zoom: 1;} #panelHome .centreDiv {height:500px;}.borderlist {height:4px;background-color:#00000;max-height:4px;margin-top:10px;margin-bottom:10px;}
#outerWrapper #header #logo {float:left;width:28%;margin-right:72%;padding-left:10px;position:absolute;}
#outerWrapper #header #headRightInfo {height:64px;float:right;width:72%;position:absolute;}
#header {
overflow:hidden;
}

#outerWrapper #header #searchBar {margin-top:13px; overflow:hidden;}

.searchBtG
{
margin-right:15px;
}

</style>
<![endif]-->
<!--[if IE 7]>
<style type='text/css'>

#outerWrapper #header #searchBar {margin-top:18px; overflow:hidden;}
.searchBtG
{
margin-right:15px;
}

#footer img {
margin-top:-10px;

}

.searchBtG
{
margin-right:15px;
}
</style>
<![endif]-->
<!-- Industrial Quick Search Referring Site Stats web tools statistics hit counter code -->
<!--<script type="text/javascript" id="wa_u"></script>
<script type="text/javascript">//<![CDATA[
    wa_account = "CFCFADB9AE"; wa_location = 214;
    wa_pageName = location.pathname;  // you can customize the page name here
    document.cookie = '__support_check=1;path=/'; wa_hp = 'http';
    wa_rf = document.referrer; wa_sr = window.location.search;
    wa_tz = new Date(); if (location.href.substr(0, 6).toLowerCase() == 'https:')
        wa_hp = 'https'; wa_data = '&an=' + escape(navigator.appName) +
'&sr=' + escape(wa_sr) + '&ck=' + document.cookie.length +
'&rf=' + escape(wa_rf) + '&sl=' + escape(navigator.systemLanguage) +
'&av=' + escape(navigator.appVersion) + '&l=' + escape(navigator.language) +
'&pf=' + escape(navigator.platform) + '&pg=' + escape(wa_pageName);
    wa_data = wa_data + '&cd=' +
screen.colorDepth + '&rs=' + escape(screen.width + ' x ' + screen.height) +
'&tz=' + wa_tz.getTimezoneOffset() + '&je=' + navigator.javaEnabled();
    wa_img = new Image(); wa_img.src = wa_hp + '://loc1.hitsprocessor.com/statistics.asp' +
'?v=1&s=' + wa_location + '&eacct=' + wa_account + wa_data + '&tks=' + wa_tz.getTime();
    document.cookie = '__support_check=1;path=/;expires=Thu, 01-Jan-1970 00:00:01 GMT';
    document.getElementById('wa_u').src = wa_hp + '://loc1.hitsprocessor.com/track.js'; //]]>
</script>
<!-- End Industrial Quick Search Referring Site Stats statistics web tools hit counter code -->
</head>
<body class="rfqbody">
<span id="flogo" style="display:none;"></span>
        <div class="rfqouterwrapper">
                <div class="rfqcontent">
                    <div class="ip_error" id="divip_error" runat="server" visible="false">The Use of this Form is Restricted - Please Contact IQSDirectory with
                    Questions.</div>
                    <h1 align="center"><%--<IQS:ErrorMessage ID="ctrlErrorMessage" runat="server" />--%></h1>
		            <h1>
		                <asp:Label ID="lblCategoryHeading" runat="server"></asp:Label>
		            </h1>
                    <div class="rfqsubheading">
		            <asp:Label ID="lblRFQSubHeading" runat="server" Text="Please fill out the following form to submit a Request for Quote to any of the following companies listed on"></asp:Label>&nbsp;<asp:HyperLink ID="hylnkCategory" runat="server" ForeColor="blue" Target="_parent"></asp:HyperLink>
                    </div>
                    <span class="requireD">*Indicates required field</span>
	            </div>
            <form id="frmRFQ" runat="server">
            <div class="rfqpanel">
                <div class="rfqfrmstep1 divLeft">
                    <p class="rfqsubhead">1) Select Company Name(s):<span class="requireD">*</span></p>
                    <p class="first">
                        <asp:Panel ID="pnlCategories" runat="server"></asp:Panel>
                    </p>
                    <p class="first">
                         <input id="btnChkAll"  class="rfqbuttonbg" runat="server" value="Check All " type="button" />
                    </p>
                    <div class="warnTextRFQ2">
                    <span class="requireD">WARNING: This form is not to be used for solicitation.</span> Solicitation is a violation of the <a href="DirectoryTermsConditions.htm" target="_parent"> Terms and Conditions </a>  of this site. Solicitors will have their IP banned and reported to the FCC. <br /> 
                    </div>
                </div>
                <div class="rfqfrmstep2 divLeft"> 
                    <p class="rfqsubhead">2) Enter Your Contact Info:</p>
                    <div class="rfqdivcinforoot">
                    <div class="rfqdivcinfomain"><div class="rfqdivcinfo"><span>Company Name:</span><span class="requireD">*</span></div><span><asp:TextBox ID="txtCompanyName" CssClass="rfqtextbox" runat="server" MaxLength="200"></asp:TextBox></span></div>
                    <div class="rfqdivcinfomain"><div class="rfqdivcinfo"><span>Contact Name:</span><span class="requireD">*</span></div><span><asp:TextBox ID="txtContactName" CssClass="rfqtextbox" runat="server" MaxLength="200"></asp:TextBox></span></div>
                    <div class="rfqdivcinfomain"><div class="rfqdivcinfo"><span>Contact Email:</span><span class="requireD">*</span></div><span><asp:TextBox ID="txtContactEmail" CssClass="rfqtextbox" runat="server" MaxLength="50"></asp:TextBox></span></div>
                    <div class="rfqdivcinfomain"><div class="rfqdivcinfo"><span>Contact Phone:</span></div><span><asp:TextBox ID="txtContactPhone" runat="server" CssClass="rfqtextbox" MaxLength="30"></asp:TextBox></span></div>
                    <div class="rfqdivcinfomain"><div class="rfqdivcinfo"><span>City, State:</span><span class="requireD">*</span></div><span><asp:TextBox ID="txtContactCity" runat="server" CssClass="rfqtextbox" MaxLength="50"></asp:TextBox></span></div>
                    <div class="rfqdivcinfomain rfqwebtext"><div class="rfqdivcinfo"><span>Website:</span><span class="requireD">*</span></div><span><asp:TextBox ID="txtCompanyWeb" runat="server" CssClass="rfqtextbox  rfqwebtext" MaxLength="50"></asp:TextBox></span></div>
                    </div>

                    <div class="rfqfrmstep3">
                    <p class="rfqsubhead">3) Specifications/Questions:</p>
                        <div class="rfqdivcspecroot">
                            <div>What specifications and/or questions do you have for the manufacturer(s)?</div>
                            <div><textarea id="txtDescription" runat="server" rows="20" class="rfqTextCtrlArea" cols="20" onkeypress="fnMaxLength(event,this)"></textarea></div>
                            <div class="rfqdivspecatt"><b> Attachments if any</b>(<i>Maximum file size advisable is 2MB</i>)</div>
                            <div class="rfqdivspecattsub">Attachment1 :</div><div><input id="inpAttachment1"  type="file" size="46" class="rfqFileCtrlArea"  name="filMyFile" runat="server"></div>
                            <div class="rfqdivspecattsub">Attachment2 :</div><div><input id="inpAttachment2"  type="file" size="46" class="rfqFileCtrlArea" name="filMyFile" runat="server"></div>
                            <div class="rfqdivspecattsub">Attachment3 :</div><div><input id="inpAttachment3"  type="file" size="46" class="rfqFileCtrlArea" name="filMyFile" runat="server"></div>
                            
                           <!-- <div class="g-recaptcha" data-sitekey="6Lc72zMUAAAAABk1ajqMH-ThUswu6BIps5JS10s_"></div> -->
                           <!-- <input type="hidden" class="hiddenRecaptcha required" name="hiddenRecaptcha" id="hiddenRecaptcha" data-callback="recaptchaCallback" />-->
                            <!--<div class="rfqdivspecatt">Enter the text as shown in the box.</div>-->
                            <div><%--<uc1:Captcha ID="Captcha1" runat="server" OnSuccess="OnSuccess" OnFailure="OnFailure"/>--%></div>
                            <div id="rfqmessage" runat="server"></div>
                            <div class="rfqbuttonsnew">
                                <asp:Button ID="btnSubmit" runat="server" Text="Send" CssClass="RFQSend btnIpBlock" onclick="btnSubmit_Click"  />
                                <input type="hidden" name="val_ipblock" id="val_ipblock" value="-1" />
                                <input type="hidden" name="val_curip" id="val_curip" value="" runat="server" />
                                <input type="button" class="RFQSend" value="Reset"  onclick="ResetValues();" />
	                        </div>
                        </div>
                    </div>
               </div>
               </div>
            <div id="hiddencontrols">
                <input type="hidden" runat="server" id="hdnCategorySK" name="hdnCategorySK" />
                <input type="hidden" runat="server" id="hdnCategoryName" name="hdnCategoryname" />
                <input type="hidden" runat="server" id="hdnCategoryDisplayName" name="hdnCategoryDisplayName" />
                <input type="hidden" runat="server" id="hdnWebsiteType" name="hdnWebsiteType" />
                <input type="hidden" runat="server" id="hdnEmailId" name="hdnEmailId" />
                <input type="hidden" runat="server" id="hdnSequenceNo" name="hdnSequenceNo" />
                <input type="hidden" runat="server" id="hdnTierSK" name="hdnTierSK" />
                <input type="hidden" runat="server" id="hdnClientSK" name="hdnClientSK" />
                <input type="hidden" runat="server" id="hdnCompanyName" name="hdnCompanyName" />
                <input type="hidden" runat="server" id="hdnButtonId" name="hdnButtonId" />
                <input type="hidden" runat="server" id="hdnSelectedtext" name="hdnSelectedtext" />
                <input type="hidden" runat="server" id="hdnRFQClientSK" name="hdnRFQClientSK" />
                <input type="hidden" runat="server" id="hdnRetainClientSK" name="hdnRetainClientSK" />
                <input type="hidden" runat="server" id="hdnConfVal" name="hdnConfVal" />
                <input type='hidden' id='hdnRootPath' value='' />
            </div>
            </form>
        </div>
    

    
</body>
</html>
