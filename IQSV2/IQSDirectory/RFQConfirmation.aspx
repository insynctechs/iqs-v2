<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFQConfirmation.aspx.cs" Inherits="IQSDirectory.RFQConfirmation" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>RFQ Confirmation</title>
    <meta name="robots" content="noindex, nofollow" />
    <link href="<%: WebURL %>Content/publish_styles.css" rel="stylesheet" />
    <!-- Google Tag Manager -->

<!-- End Google Tag Manager -->

    <script src='<%: WebURL %>Scripts/jquery-1.7.2.min.js' type='text/javascript' ></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnclose').click(function () {
                parent.$.fancybox.close();
            });
        });
    </script>
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
</head>
<body class="rfqbody">
    <!-- Google Tag Manager (noscript) -->
<noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-NGZWMKN"
height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
<!-- End Google Tag Manager (noscript) --> 
    
    <form id="form1" runat="server">
        <div id="rfqouterwrapper">
<div class="RFQConfirmation rfqsubhead">
            <p><asp:Panel ID="pnlOutBound" runat="server"></asp:Panel></p>
            <p>Your RFQ has been processed.</p>
            <p>Please wait for a response from the companies you have contacted.</p>
            <p>Thank you for submitting your RFQ.
            <asp:Label ID="lblRequestID" runat="server"></asp:Label>
            <br />
            <p><input type="button" id="btnclose" value="Close" class="RFQSend" style="margin-left:0px;" /></p>
            </div>
			  </div>
            
    </form>     
</body>
</html>
