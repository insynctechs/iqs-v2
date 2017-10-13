<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MailSend.aspx.cs" Inherits="Controls_MailSend" %>
<%@ Register Src="~/Controls/CaptchaMailSend.ascx" TagName="CaptchaMailSend" TagPrefix="uc1" %>
<!DOCTYPE html>

<html lang='en-US'>
<head runat="server">
    <title>Mail Sending Form</title>
    <meta name="ROBOTS" content="NOINDEX, NOFOLLOW" />
    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <!--[if lt IE 9]>
        <script src='//html5shiv.googlecode.com/svn/trunk/html5.js'></script>
    <![endif]-->
    <script type="text/javascript">
        function isValidEmailAddress(emailAddress) {
            var pattern = new RegExp(/^(("[\w-+\s]+")|([\w-+]+(?:\.[\w-+]+)*)|("[\w-+\s]+")([\w-+]+(?:\.[\w-+]+)*))(@((?:[\w-+]+\.)*\w[\w-+]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][\d]\.|1[\d]{2}\.|[\d]{1,2}\.))((25[0-5]|2[0-4][\d]|1[\d]{2}|[\d]{1,2})\.){2}(25[0-5]|2[0-4][\d]|1[\d]{2}|[\d]{1,2})\]?$)/i);
            return pattern.test(emailAddress);
        };
    </script>
</head>
<body class='mailbody'>
<div id="divRegForm">
    <header><h1>Send this page by mail.</h1></header>
    <section>
        <form id="Form1" runat="server">
            <ul>
                <li>Name:<span class="require">* </span></li>
                <li><input type="text" id="txtName" class="commenttextbox" /></li>
                <li>E-mail Address:<span class="require">* </span></li>
                <li><input type="text" id="txtFrom" class="commenttextbox" /></li>
                <li>Receiver(s) Mail Address :<span class="require">* </span></li>
                <li><input type="text" id="txtTo" /></li>
                <li><uc1:CaptchaMailSend ID="Captcha1" EnableViewState="false" Style="float:left;" runat="server"/><a href="#Send" id="lnkSend" class="mailsend"><img src="<%=rootDirPath %>images/submit_button.png" alt="Send" /></a></li>
            </ul>

              <span class="spanwait">Please wait <img style="vertical-align:middle;" src="<%=rootDirPath %>images/dots.gif" alt="Wait" /></span>
              <input type='hidden' id="txtTitle" runat="server" />
              <input type='hidden' id="txtDescription" runat="server" />
              <input type='hidden' id="txtUrl" runat="server" />
          </form>
    </section>
</div>
<div id="divSuccess">
    <header><h2>Mail Sent Successfully !!</h2></header>
    <a href="#Close" id="lnkClose"><img src="<%=rootDirPath %>images/close.png" alt="Close" /></a>
</div>
</body>
</html>
