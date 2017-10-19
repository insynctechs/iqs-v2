<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="copro-page-email.aspx.cs" Inherits="IQSDirectory.copro_page_email" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta name="robots" content="noindex, nofollow" />
    <link href="Content/form_styles.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />    
    <!-- <link href="Content/styler.css" rel="stylesheet" /> -->
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />   
     <!--include jQuery -->  
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"  type="text/javascript"></script>   
    <!--include jQuery Validation Plugin-->  
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.12.0/jquery.validate.min.js"  type="text/javascript"></script>   
    <script type="text/javascript"  src="Scripts/copro-page-email.js"></script>
    <!--<script src='https://www.google.com/recaptcha/api.js' async defer></script>-->
</head>
<body class="mailbody">    
 <div id="divStatus" runat="server" ></div>
 <div id="profileForm" class="profilestyle" runat="server">
      <form id="frmShare" runat="server">
               <span class="divLeft"><img src="/images/mailicon.png" alt="Mail" title="Mail" class="h1img" /></span>
               <span class="require divfloatright">* Indicates require Fields</span>
               <span id="divEmailCName" class="divLeft h1txt" runat="server"><!--<h2>Email DAN-LOC Bolt &amp; Gasket</h2>--></span>

          <div class="profContInfo ">
<div class="ip_error" style="display:none;">The Use of this Form is Restricted - Please Contact IQSDirectory with
                    Questions.</div>
<div id="lblErr" style="clear:both;"></div><br />
<ul>
                <li>First Name:<span class="require">* </span></li>
                <li><input type="text" id="txtFirstName" class="commenttextbox" runat="server" /></li>
                <li>Last Name:<span class="require">* </span></li>
                <li><input type="text" id="txtLastName" class="commenttextbox" runat="server" /></li>
                <li>EMail Address :<span class="require">* </span></li>
                <li><input type="text" id="txtEmailAddress" class="commenttextbox" runat="server" /></li>
                <li>Company Name :<span class="require">* </span></li>
                <li><input type="text" id="txtCompanyName" class="commenttextbox" runat="server" /></li>                
                <li>Zip/Postal Code :<span class="require">* </span></li>
                <li><input type="text" id="txtZip" class="commenttextbox" runat="server" /></li>                
                <li>Subject :<span class="require">* </span></li>
                <li><input type="text" id="txtSubject" class="rfqtextboxsub width90" maxlength="200" /></li>
                <li>Message :<span class="require">* </span></li>
                <li><textarea id="txtMessage" class="TextCtrlArea width90" Height="64px"  style="height:64px;"></textarea></li>
                <li><div class="g-recaptcha" data-sitekey="6Lc72zMUAAAAABk1ajqMH-ThUswu6BIps5JS10s_"  ></div></li>                
            </ul>
<div style="float:left;padding-left: 25px;padding-top: 31px;" ><input type="hidden" name="val_ipblock" id="Hidden1" value="-1" runat="server" />
    <asp:Button ID="btnSubmit" runat="server" Text="Send"  onclick="btnSubmit_Click" CssClass="buttonBg"  />

</div>
</div>
<div class="clearfix" ></div>
<div class="reqText">
<div class="ProfilewarnTextRFQ">
    <p><span class="requireD">WARNING: This form is not to be used for solicitation. </span> Solicitation is a violation of the <a href="http://www.iqsdirectory.com/DirectoryTermsConditions.htm"> Terms and Conditions </a>  of this site. Solicitors will have their IP banned and reported to the FCC. </p><br /> 
</div>
</div>
</form>
</div>
         
</body>
</html>