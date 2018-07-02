<%@ Page Language="C#" AutoEventWireup="true"  ValidateRequest="false" %> <!--CodeFile="DirectoryListCompanies.aspx.cs" Debug="true"  Inherits="aspx_DirectoryListCompanies"-->
<% Response.Status = "404 Not Found"; %>
<!DOCTYPE html >
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<title>IQSDirectory - 404 Page</title>
<meta name="keywords" content="IQS Directory, 404 page" />
<meta name="description" content="The IQSDirectory page you're looking for cannot be found; we apologize for the invonvenience." />
<meta name="robots" content="noindex" />
<meta name="robots" content="nofollow" />
<link rel="shortcut icon" href="http://www.iqsdirectory.com/favicon.ico">
<link href="http://www.iqsdirectory.com/Css/404style.css" rel="stylesheet" type="text/css" />
<link href="css/searchbar.css" rel="stylesheet" type="text/css" />
<link href='css/jquery-ui.css'  rel='stylesheet' type='text/css' media='screen' />
<script src='js/jquery-1.7.2.min.js' type='text/javascript' ></script>
<script src='js/jquery-ui.js' type='text/javascript' ></script>
<script type='text/javascript'>
    $(document).ready(function () {
        $.get('searchcontrol.htm', function (data) {
            $('#searchBarDir').html(data);
        });
    });
</script>


</head>

<body>
	<div id="browserShadow"></div>
	<div id="gradient">&nbsp;</div>
<div id="wrapper">
    	<!-- main content left and right shadow -->
    	<div id="shadowLeft"></div>
        <div id="shadowRight"></div>
        <!-- end -->

  		<div id="header">
        	<div id="headerContent">
                <div id="logo">
                    <a href="http://www.iqsdirectory.com"><img border="0" src="http://www.iqsdirectory.com/images/iqsdirectory_home_logo.png" title="IQSDirectory Logo" alt="IQSDirectory logo" /></a>
                </div>
                <div id='searchBarDir'>

                </div><!-- ends "searchBar" -->
            </div><!-- ends "headerContent" -->
        </div><!-- ends "header" -->
        
      	<div class="divider">
        	<div class="dividerLeft"></div>
            <div class="dividerRight"></div>
        </div>
        
  		<div class="mainContainer">
	  		  <div id="bodyCopy">
			    <h1>Sorry, the page you are looking for cannot be found.</h1>
				<h2>A report has been sent to web maintenance.</h2>
                
                <ul id="suggestions">
                <h3>Suggestions:</h3>
<!--
                <li>Use the search box above to help you find what you are looking for.</li>
-->
                <li>Return to our <a href="http://www.iqsdirectory.com">Home Page</a>.</li>
               	<li>If you entered the URL manually, please check your spelling.</li>
                <li>Take a look at our <a href="http://www.iqsdirectory.com/sitemap/sitemap.htm">sitemap</a>.</li>
                </ul>
				<br />
                <br />
                <h2>We sincerely apologize for the inconvenience.</h2>
                <br />
	  	  	  </div><!-- ends "bodyCopy" --> 
			<div class="clear"></div>    
  		</div><!-- ends "mainContainer" --> 

<div id="footer">
        	<div id="footerContent">
                <div id="footerLogo">
                    <a href="http://www.iqsdirectory.com"><img src="http://www.iqsdirectory.com/Images/footer_logo.jpg" alt="IQS Directory footer logo" title="IQS Directory small logo" /></a>
                </div>
                <div id="footerLegal">
                IQS<sup>&reg;</sup> and Industrial Quick Search<sup>&reg;</sup> are Registered Trademarks of Industrial Quick Search, Inc.<br />
                    <a href="http://www.iqsdirectory.com/DirectoryTermsConditions.htm">Terms and Conditions</a><br />
                    <a href="http://www.iqsdirectory.com/sitemap/sitemap.htm">Site Map</a>
                </div>
                <div id="footerAddress">
                    Phone: 877-977-5377<br />
                    <a href="mailto:sales@industrialquicksearch.com">sales@industrialquicksearch.com</a><br />
                    1500 E. Beltline, Grand Rapids, MI 49506
                </div>
        	</div><!--ends footerContent --> 
            
            <!-- NOTE: Adds a lower shadow on the footer; only activate if page is relatively short -->
            <div id="footerShadow"></div>   
      
		</div><!-- ends "footer" --> 
</div><!-- ends "wrapper" -->

<input type='hidden' id='hdnRootPath' value='' />
</body>
</html>
