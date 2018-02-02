<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="copro-page-email-thankyou.aspx.cs" Inherits="IQSDirectory.copro_page_email_thanku" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Thanks for Contacting Company</title>
    <META NAME="ROBOTS" CONTENT="NOINDEX, NOFOLLOW">
    <!-- Google Tag Manager -->
<script>(function (w, d, s, l, i) {
    w[l] = w[l] || []; w[l].push({
        'gtm.start':
        new Date().getTime(), event: 'gtm.js'
    }); var f = d.getElementsByTagName(s)[0],
        j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
            'https://www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
    })(window, document, 'script', 'dataLayer', 'GTM-NGZWMKN');</script>
<!-- End Google Tag Manager -->

    <link href="Content/form_styles.css" rel="stylesheet" />
     <script src='Scripts/jquery-1.7.2.min.js' type='text/javascript' ></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnclose').click(function () {
                parent.$.fancybox.close();
            });
        });
    </script>
</head>
<body class="rfqbody">
    <!-- Google Tag Manager (noscript) -->
<noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-NGZWMKN"
height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
<!-- End Google Tag Manager (noscript) -->

    <form id="form1" runat="server" >
        
        <div id="rfqouterwrapper">
<div class="RFQConfirmation rfqsubhead">
            <p></p>
            <p>Your Email has been sent successfully.</p>
            <p>Please wait for a response from the companies you have contacted.</p>
            <p>Thank you for contacting us.
            
            <br />
            <p><input type="button" id="btnclose" value="Close" class="RFQSend" style="margin-left:0px;" /></p>
            </div>
			  </div>
       
    </form>
</body>
</html>
