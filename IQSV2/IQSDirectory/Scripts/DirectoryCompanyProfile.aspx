<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DirectoryCompanyProfile.aspx.cs" Inherits="DirectoryCompanyProfile" %>
<%@ Register Src='~/Controls/ProfSendEmail.ascx' TagName='ProfSendEmail' TagPrefix='IQSC' %>
<%@ Register Src='~/Controls/CompanyProfileReviews.ascx' TagName='CompanyProfileReviews' TagPrefix='IQSR' %>
<%@ Register Src='~/Controls/CompanyProfileArticles.ascx' TagName='CompanyProfileArticles' TagPrefix='IQSART' %>
<%@ Register Src='~/Controls/RatingControl.ascx' TagName='RatingControl' TagPrefix='IQSRATING' %>
<%@ Register Src='~/Controls/ErrorMessage.ascx' TagName='ErrorMessage' TagPrefix='IQS' %>
<!DOCTYPE html>
<html lang='en-US'>
<head runat="server">
    <title></title>
    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
<meta name='viewport' content='width=device-width, initial-scale=1.0' />
<!--[if lt IE 9]>
<script src='//html5shiv.googlecode.com/svn/trunk/html5.js'></script>
<![endif]-->
<meta name='copyright' content='2015 Industrial Quick Search Manufacturing Directory' />

<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
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
    #header {overflow:hidden;}
    #outerWrapper #header #searchBar {margin-top:13px; overflow:hidden;}
    .searchBtG{margin-right:15px;}
    </style>
    <![endif]-->
    <!--[if IE 7]>
    <style type='text/css'>
    #outerWrapper #header #searchBar {margin-top:18px; overflow:hidden;}
    .searchBtG{margin-right:15px;}
    #footer img {margin-top:-10px;}
    .searchBtG{margin-right:15px;}
    </style>
    <![endif]-->
   

</head>
<body>
<div id="bodyTopScripts" runat="server" style="display:none;"></div>
<!-- Google Tag Manager (noscript) -->
<noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-NGZWMKN"
height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
<!-- End Google Tag Manager (noscript) -->

<div id="fb-root"></div>
<script type="text/javascript">    /*(function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/all.js#xfbml=1";
        fjs.parentNode.insertBefore(js, fjs);
    } (document, 'script', 'facebook-jssdk'));*/</script>
    <div id='outerWrapper'>
        <div id='header'>
            <div id='logo'>
                <a href='/'><img src='<%=rootDirPath %>commonimages/igs_logo.gif' alt='IQSDirectory' title='IQSDirectory' id='ImgHeaderLogo' /></a>
            </div>
            <div id='headsearchdiv'>
                <div id='headsearch'></div>
                <nav><ul>
<li><a href='http://www.iqsdirectory.com/'>Home</a></li>
<li><a href='http://www.iqsdirectory.com/DirectoryHomeListYourCompany.aspx'>List Your Company</a></li>

</ul></nav>
            </div>
        </div>
        <div id='contentWrapper'>
            <div id='contentHome'>
                <IQS:ErrorMessage runat='server' EnableViewState='false' ID='ctrlProfileErrorMessage'/>
                <div class='profileTop' itemscope itemtype='http://schema.org/LocalBusiness'>
                <div class='divratingnew'>
                    <h1>
                        <span itemprop='name' class='spantoptitle' runat="server" id="divCompName"></span>
                    </h1>
                </div>
                <div class='divratingnew'><span  id='spanTopRate' class='spanratingclient'>
                    <input name='topstar' type='radio' class='topcommentstar' value='1' title='1'/>
                    <input name='topstar' type='radio' class='topcommentstar' value='2' title='2'/>
                    <input name='topstar' type='radio' class='topcommentstar' value='3' title='3'/>
                    <input name='topstar' type='radio' class='topcommentstar' value='4' title='4'/>
                    <input name='topstar' type='radio' class='topcommentstar' value='5' title='5'/>
                    </span><span id='spanRateNum' class='spanratingnumnew'></span>
                 </div>
                 <div class="divagrating" itemprop="aggregateRating" itemscope="itemscope" itemtype="http://schema.org/AggregateRating">
                    <meta itemprop="bestRating" content="5"/>
                    <meta itemprop="worstRating" content="1"/>
                    <meta itemprop="ratingValue" content='<%=CompRating %>'/>                    
                    <meta itemprop="ratingCount" content='<%=CompCount %>'/>
                    <!--<meta itemprop="reviewCount" content='<%=CompCount %>'/>-->                 
                    
                </div>
                <div class='cleardiv'></div>
                <div class="divfloatleft coproleftdiv" >
                <div style="display:none; margin-bottom:5px;" id="divImage" runat="server">
                </div>
                    <div runat="server" id="divVideo" style="display:none;">
                        
                            <div id='divyoutube' class='youtubedivsize' style='background-size: 100%;' runat="server">
                                <img class='youtubeimgsize' src='<%=rootDirPath %>images/coproplay.png' alt='Play Video' title='Play Video' />
                            </div>
                            <a style='display:none;' class='iframe' id='lnkViewVideo' runat="server">View Video</a>
                            <script language='javascript' type='text/javascript'>
                                $(document).ready(function () {
                                    $('#divyoutube').live('click',function(){ 
                                        $('#lnkViewVideo').trigger('click'); 
                                    });
                                    $('#lnkViewVideo').fancybox({
                                        'padding': 0,
                                        'showCloseButton': true,
                                        'modal': true,
                                        'overlayShow': false,
                                        'overlayOpacity': 0,
                                        'centerOnScroll': false,
                                        'titleShow': false,
                                        'autoScale': false,
                                        'width':900,
                                        'height':585,
                                        'hideOnContentClick':false,
                                        'transitionIn' : 'none',
                                        'transitionOut' : 'none'
                                    });
                                });
                            </script>
                        
                    </div>
                    <span id="divCompUrls" runat="server">
                
            </span>
            <div class='compprof_addrwrapper'>
                
                <div id="divPhone" runat="server">
                </div>
                <div class='divLeft adress_spacer'>
                <div class='divLeft mapImg'><img src='<%=rootDirPath %>commonimages/markera.png' alt='Geo Location Marker' title='Geo Location Marker'/></div>
                <div class='divLeft'>
                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                    <br/>
                    <a class='iframe coproviewmap' href='controls/copromap.htm?address=' id='lnkViewMap' runat="server">View Map</a>
                    <script language='javascript' type='text/javascript'>
                        $(document).ready(function () {
                            $('#lnkViewMap').fancybox({
                                'padding': 0,
                                'showCloseButton': true,
                                'modal': true,
                                'titleShow': false,
                                'autoScale': false,
                                'width': 406,
                                'height': 366,
                                'transitionIn': 'none',
                                'transitionOut': 'none'
                            });
                        });
                    </script>
                </div>
            </div>
            </div>
                    </div>
                    <div class="divfloatleft coprorightdiv" runat="server" id="divDescMaster">
                        <p>
                            <span itemprop='description' runat="server" id="divDescription"></span>
                        </p>
                    </div>
                
            <div class='cleardiv'></div>
            
            
            <div class='cleardiv'></div>
            <p id="divResources" runat="server" style="display:none;">
                <b>Company Resources: </b>
                <span class='linkBlue'>|</span>
                <a href ='/' target='_blank' class='DPFCompanyresourceLink' >CATALOG</a>
            </p>
            </div>
            <div class='profileLeft divLeft' >
            <div id="divArtMain" runat="server">
            <hr />
            <div id ='commentForm'>
                <IQSART:CompanyProfileArticles ID='CompanyProfileArticles' runat='server' />
            </div>
            <div class="cleardiv"></div>
            </div>
            <hr/>
            <div id ='commentForm'>
                <IQSR:CompanyProfileReviews ID='ctrlCompanyProfileReviews' runat='server' />
            </div>
        </div>
        <div class='profileRight divLeft'>
            <div style="clear:both;margin-left: -10px;">
               <!-- <div class="fb-like-box" data-href="http://www.facebook.com/iqsdirectory" data-send="false" data-stream="false" data-header="false" data-width="400" data-show-faces="false"></div>-->
            </div>
            <div id='ctrlDynamicContent_pnlDynamicContent' class='dynamiccontent'>
                <div class='profileRc clearfix' id="divSocial" runat="server">
                    
                </div>
                 <div class='profileRc clearfix' id="divRelated" runat="server" style="display:none;">
                    <div class='headRel' runat='server' id='divRelatedCap'>Find Related Manufacturers</div>
                    <div class='listRel'>
                        <ul id="ulRelated" runat="server">
                        </ul>
                        <div class='clearfix'></div>
                    </div>
                </div>
                <div class='profileRc clearfix' id="divAddInfo" runat="server" style="display:none;">
                    <div class='headRel'>This Company Can Be Found On</div>
                    <div class='listRel'>
                        <div class='divLeft listRelIn'>
                            <ul id="ulAddLeft" runat="server">
                            </ul>
                        </div>
                        <div class='divRight listRelIn'>
                            <ul id="ulAddRight" runat="server">
                            </ul>
                        </div>
                        <div class='clearfix'></div>
                    </div>
                </div>
                <div class='profileRc clearfix' id="divTradeNames" runat="server" style="display:none;">
                    <div class='headRel'>Tradenames</div>
                    <div class='listRel'>
                        <div class='divLeft listRelIn profBotLink'>
                            <ul id="ulTradeLeft" runat="server">
                                
                            </ul>
                        </div>
                        <div class='divRight listRelIn profBotLink'>
                            <ul id="ulTradeRight" runat="server">
                                
                            </ul>
                        </div>
                        <div class='clearfix'></div>
                    </div>
                </div>
            </div>
            <form id="DirectoryProfile" name='DirectoryProfile' runat='server'>
                <div id='profileForm' class='profilestyle'>
                    <span class='divLeft'><img src='<%=rootDirPath %>commonimages/mailicon.png' alt='Mail' title='Mail' class='h1img' /></span>
                    <span class='requireD divfloatright'>* Indicates Required Fields</span>
                    <span class='divLeft h1txt' id="divEmailCName" runat="server"></span>
                    <IQSC:ProfSendEmail ID='ctrlProfSendEmail' runat='server' />
                </div>
            </form>
            <div class='clearfix' ></div>
        </div>
    </div>
    <div class='clearfix' ></div>
    </div>
    </div>
    <div class="clearfix"> </div>
    <!--<div id="footer" itemscope itemtype="http://schema.org/Organization" >
    <div class='footer'>
        
        

        <div id='footerRow1Col3' >
            <span itemprop="telephone">877-977-5377</span><br />
            <span itemprop="name">IQS Directory</span> 
        </div>      <div class='sitemap'><a href='http://www.iqsdirectory.com/sitemap/sitemap.htm' rel='follow'>Site Map</a></div>
                <div id='flogo' ><a href='http://www.iqsdirectory.com' itemprop='url' ><img id="footerlogo" title="IQSDirectory" alt="IQSDirectory" src="<%=rootDirPath %>images/iqsdirectory_footer_logo.jpg" itemprop="logo" border="0" /></a></div>
                <div class='clearfix'></div>
    </div>
    </div>-->
    
     <div id="new-footer" itemscope itemtype="http://schema.org/Organization">
  <ul id="footerlinks">

<li class="one">
	<span>IQS&reg; Directory</span>
     <a href="http://www.iqsdirectory.com/about-us.html">About Us</a><br>
     <a href="http://www.iqsdirectory.com/testimonial.htm">Testimonials</a><br>
     <a href="http://www.iqsdirectory.com/directoryhomelistyourcompany.aspx">Advertise Your Company</a><br />
	<a href="http://www.iqsdirectory.com/directorylistcompanies.aspx">Directory of Companies</a><br />
     <a href="http://www.iqsdirectory.com/sitemap/sitemap.htm">Directory Sitemap</a><br />
</li>

<li class="two">
	<span>Resources</span>
    <a href='http://www.iqsdirectory.com/resources/home/' >Resource Center</a><br />
	<a href="http://blog.iqsdirectory.com/" target="blank">IQS&reg; Newsroom</a><br />
	<a href="http://www.iqsdirectory.com/industry-associations.html">Manufacturing Associations</a><br />
     <a href="http://www.iqsdirectory.com/industry-tradeshows.html">Industry Tradeshows</a><br />
     <a href="http://www.iqsdirectory.com/faq.html">FAQ</a>
</li>

<li class="three">
	<span>Marketing Services</span>
     <a href="http://www.iqsdirectory.com/web-marketing/seo/" target="_blank">Search Engine Optimization</a><br>
	<a href="http://www.iqsdirectory.com/web-marketing/social-media/" target="_blank">Social Media</a><br>
	<a href="http://www.iqsdirectory.com/web-marketing/ppc/" target="_blank">PPC Management</a><br />
     <a href="http://www.iqsdirectory.com/web-marketing/web-design/" target="_blank">Web Design</a><br />

</li>

<li class="four">
	<span>Connect with Us</span>
     <a href="http://google.com/+Iqsdirectory" target="_blank">Google+</a><br>
	<a href="https://twitter.com/IQSDirectory" target="_blank">Twitter</a><br>
	<a href="http://www.youtube.com/c/Iqsdirectory" target="_blank">YouTube</a><br />
     <a href="http://www.linkedin.com/company/iqsdirectory-com" target="_blank">LinkedIn</a><br />
	<a  href="https://www.facebook.com/IQSDirectory" target="_blank">Facebook</a>
</li>
</ul>
  <div class="sch-logo">
<a href="http://www.iqsdirectory.com" id='flogo' itemprop='url' ><img id="footerlogo" src="<%=rootDirPath %>images/iqsdirectory_home_logo.png" alt="IQS Directory" title="IQS Directory" itemprop="logo" /></a>
<a href="http://www.iqsdirectory.com/contact.html" class="contactlink"><img src="<%=rootDirPath %>images/contact-us1.png" alt="contact us image" title="contact us image" class="contactlink" /></a>
</div> 
  <div class="sch-org">
<a href="http://www.iqsdirectory.com/directorytermsconditions.htm">Terms and Conditions</a>
  <h3><span class="tele" itemprop="telephone">877-977-5377</span></h3>
  <span itemprop="name" >IQS Directory</span> 
  </div> 
  </div>
  <div id="back-top">
		<a href="#header">Move to Top</a>
	</div>
    <div id="divoutbound" runat="server" style="display:none;"></div>
    <div id="bodyBottomScripts" runat="server" style="display:none;"></div>
    <input type='hidden' id='hdnProfileClientSk' runat="server" />
    <asp:Label ID='lblProfileClientSk' runat='server' Visible='False' ></asp:Label>
    <input type='hidden' id='hdnRootPath' runat="server" />
    <input type='hidden' id='hdnCategorySK' value='0' />
    <input type='hidden' id='hdnCategory' value='0' />
    <input type='hidden' id='hdnParent' value='0' />
    <asp:Literal runat="server" ID="litScript"/>
</body>
</html>
