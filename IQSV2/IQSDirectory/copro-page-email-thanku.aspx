<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="copro-page-email-thanku.aspx.cs" Inherits="IQSDirectory.copro_page_email_thanku" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RFQ Confirmation</title>
    <META NAME="ROBOTS" CONTENT="NOINDEX, NOFOLLOW">
    <link href="css/form_styles.css" rel="stylesheet" type="text/css" />
     <script src='js/jquery-1.7.2.min.js' type='text/javascript' ></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnclose').click(function () {
                parent.$.fancybox.close();
            });
        });
    </script>
</head>
<body class="rfqbody">
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
