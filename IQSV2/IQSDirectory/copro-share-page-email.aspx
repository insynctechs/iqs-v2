﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="copro-share-page-email.aspx.cs" Inherits="IQSDirectory.copro_share_page_email" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta name="robots" content="noindex, nofollow" />
    <link href="../../Content/form_styles.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />        
     <!--include jQuery -->  
    <!--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"  type="text/javascript"></script>   -->
    <script src="//ajax.aspnetcdn.com/ajax/jquery.validate/1.12.0/jquery.validate.min.js"  type="text/javascript"></script>   
    <!--include jQuery Validation Plugin-->       
    <script type="text/javascript"  src="../../Scripts/copro-share-page-email.js"></script>    
</head>
<body class="mailbody">
    <div id="divRegForm" runat="server">
    
        <header><h1>Send this page by mail.</h1></header>
    
        <form id="frmShare" runat="server">
         
             <ul>
                <li>Name:<span class="require">* </span></li>
                <li><input type="text" id="txtName" name="txtName"  class="commenttextbox" /></li>
                <li>E-mail Address:<span class="require">* </span></li>
                <li><input type="text" id="txtFrom" name="txtFrom"  class="commenttextbox" /></li>
                <li>Receiver(s) Mail Address :<span class="require">* </span></li>
                <li><input type="text" id="txtTo" name="txtTo"  class="commenttextbox" /></li>
                <li><!--<div class="g-recaptcha" data-sitekey="6Lc72zMUAAAAABk1ajqMH-ThUswu6BIps5JS10s_"  ></div>
                     <input type="hidden" class="hiddenRecaptcha required" name="hiddenRecaptcha" id="hiddenRecaptcha"  data-callback="recaptchaCallback"  /> 
                -->
                    <div id="recaptcha2"></div>
                    <input type="hidden" class="hiddenRecaptcha2 required" name="hiddenRecaptcha2" id="hiddenRecaptcha2"  data-callback="verify_callback2"  /> 
         
                </li>
                <li>
                 <input type="button" id="btnShare"  class="buttonBg" value="Send"  />
                <!--<a href="#Close" id="lnkClose"><img src="images/close.png" alt="Close" /></a>-->
                </li>
                 <!--<asp:Button ID="btnSubmit" runat="server" Text="Send"  /></li><!--onclick="btnSubmit_Click"-->
            </ul>
                
                <!--<li><a href="#Send" id="lnkSend" class="mailsend"><img src="/images/submit_button.png" alt="Send" /></a></li>-->
                <!--<span class="spanwait">Please wait <img style="vertical-align:middle;" src="/images/dots.gif" alt="Wait" /></span> -->
              <input type='hidden' id="txtTitle" runat="server" />
              <input type='hidden' id="txtDescription" runat="server" />
              <input type='hidden' id="txtUrl" runat="server" />
                
           
          </form>
    
</div>
<div id="divSuccess" class="register_success">
    <header><h2>Mail Sent Successfully !!</h2></header>
    <a href="#Close" id="lnkClose"><img src="/images/close.png" alt="Close" /></a>
</div>
 
    <script type='text/javascript'>
       // $(document).ready(function () {
        //this will insert recaptcha in the form
            captchaCallBack();
        //});        

    </script>
</body>
</html>
