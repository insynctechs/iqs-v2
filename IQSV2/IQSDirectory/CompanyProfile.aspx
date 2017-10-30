<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompanyProfile.aspx.cs" Inherits="IQSDirectory.CompanyProfile" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ register src="~/Controls/copro-page-email.ascx" tagprefix="uc1" tagname="copropageemail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%:RootPath %>Content/form_styles.css" rel="stylesheet" />
    <!-- <link rel="stylesheet" href="//code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />     -->
    
    
    <link href="<%:RootPath %>Content/copro_styles.css" rel="stylesheet" />
    <link href='<%:RootPath %>Content/stylerprint.css' rel='stylesheet' type='text/css' media='print' />
    <link href='<%:RootPath %>Content/jquery.fancybox-1.3.4.css' rel='Stylesheet' type='text/css' media='screen' />

    <script src='<%:RootPath %>Scripts/jquery.rating.pack.js' type='text/javascript'></script>
    <script src='<%:RootPath %>Scripts/jquery.fancybox-1.3.4.js' type='text/javascript'></script>
    <script src='<%:RootPath %>Scripts/fb.js' type='text/javascript'></script>
    <script src='<%:RootPath %>Scripts/jquery.cookie.js' type='text/javascript' ></script>
    <script src='<%:RootPath %>Scripts/company_profile.js' type='text/javascript' ></script>
    <script src='<%:RootPath %>Scripts/move_top.js' type='text/javascript'></script>
   <!-- <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"  type="text/javascript"></script>   -->
    <script src="//ajax.aspnetcdn.com/ajax/jquery.validate/1.12.0/jquery.validate.min.js"  type="text/javascript"></script>   
    <input type='hidden' id='hdnEmailCaptcha' value="no" />

    <div id='content_wrapper'>
        <section class="row1" itemscope itemtype='http://schema.org/LocalBusiness'>
        <h1 itemprop="name"><%= ClientName %></h1>
        <% if (ShowReviews == "Y")
            { %>
        <div class='divratingnew'><span  id='spanTopRate' class='spanratingclient'>
                        <input name='topstar' type='radio' class='topcommentstar' value='1' title='1'/>
                        <input name='topstar' type='radio' class='topcommentstar' value='2' title='2'/>
                        <input name='topstar' type='radio' class='topcommentstar' value='3' title='3'/>
                        <input name='topstar' type='radio' class='topcommentstar' value='4' title='4'/>
                        <input name='topstar' type='radio' class='topcommentstar' value='5' title='5'/>
                        </span><span id='spanRateNum' class='spanratingnumnew'><%: CompRating %>/5</span>
        </div>
            <script type='text/javascript'>
                $(document).ready(function () {
                    $('input[type=radio].topcommentstar').rating({ required: true });
                    $('input[type=radio].topcommentstar').rating('enable');
                    $('input[type=radio].topcommentstar').rating('select', 0, false);
                    $('input[type=radio].topcommentstar').rating('disable');
                    if (parseInt(<%: CompRating %>) > -1) { $('#spanTopRate').show(); }
                });
             </script>
            <% } %>
        <div class="divagrating" itemprop="aggregateRating" itemscope="itemscope" itemtype="http://schema.org/AggregateRating">
            <meta itemprop="bestRating" content="5"/>
            <meta itemprop="worstRating" content="1"/>
            <meta itemprop="ratingValue" content='<%: CompRating %>'/>
            <meta itemprop="ratingCount" content='<%: CompCount %>'/>
            <!--<meta itemprop="reviewCount" content='0'/>-->

        </div>
        <div class='cleardiv'></div>
        <div class="col1">
             <% if (LogoLink != "")
                 { %>
            <div id="divImage"><img class="clogo" src="<%= LogoLink %>" alt="<%: ClientNameFormatted %>" title="<%: ClientNameFormatted %>" itemprop="logo"></div>
            <% } %>
            <% if (VideoLink.ToString() != "" && VideoLink.ToString() != "#")
                { %>
            <div id="divVideo" >

                <div id="divYoutube" class="container" <%: YoutubeStyle %> >
                    <img src="<%:RootPath %>images/coproplay.png" alt="Play Video" title="Play Video">
                </div>
                <a  id="lnkViewVideo" href="<%: VideoLink %>" class="iframe coproviewvideo ">View Video</a>
                <script type="text/javascript">
                    $(document).ready(function () {
                        $('#divYoutube').live('click', function () {
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
                            'width': 900,
                            'height': 585,
                            'hideOnContentClick': false,
                            'transitionIn': 'none',
                            'transitionOut': 'none'
                        });
                    });
                </script>

            </div>
            <% } %>
            <div id="divCompUrls"><%= WebsiteLink %></div>
            <div id="divPhone" >
                <% if(Phone != "") { %>
                <b>Phone: </b><%= Phone %><br>
                <% } %>
                <% if(Fax != "") { %>
                <b>Fax: </b><%= Fax %>
                <% } %>
            </div>
            <div id="divAddress">
                <img src="<%:RootPath %>/images/markera.png" alt="Geo Location Marker" title="Geo Location Marker" />
                <div id="lblAddress"><%= Address %>
                <br>
                <a href="<%:RootPath %>copro-map.html?address=<%=MapAddress %>&comp=<%=ClientName %>" id="lnkViewMap" class="iframe coproviewmap">View Map</a>
                </div>
                    <script type="text/javascript">
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
        
        <div class="col2" itemprop="description" >
            <p><%= ClientDesc %></p>
        </div>

        <div class="cleardiv"></div>
        <div id="divResources"></div>
        <div class="cleardiv"></div>
    </section>
    <section class="row2">
        <div class="col1">
            <%if (Articles != null) { if (Articles.Count > 0)
            { %>
            <hr/>
            <section id="secarticles">
                <h2>Articles and Press Releases</h2>
                <ul>
                    <% foreach (var dr in Articles)
                    { %>
                    <li>
                        <h3>
                            <a href="<%=dr["URL"]%>" target="_blank"><%= dr["HEADING"]%></a></h3>
                        <aside>
                            <%=dr["DATE"]%> - <%=dr["NAME"]%>
                        </aside>
                        <p>
                            <%=dr["DESC"]%>
                        </p>
                    </li>
                    <% } %>
                </ul>
            </section>
            <% } } %>
            <hr/>
            <div id="commentForm">
                <section id="secreviews">
                    <h2>Company Reviews</h2>
                    <div class="review_main_left">
                        <div class="review_main_left_inner" id="divCompReview">
                            <input name="star1" type="radio" class="totalreviewstar" value="1" title="1"/>
    <input name="star1" type="radio" class="totalreviewstar" value="2" title="2"/>
    <input name="star1" type="radio" class="totalreviewstar" value="3" title="3"/>
    <input name="star1" type="radio" class="totalreviewstar" value="4" title="4"/>
    <input name="star1" type="radio" class="totalreviewstar" value="5" title="5"/>
                        </div>
                        <h3 class="main_review_count"><span id="divTotalReviewCount">1</span> reviews</h3>
                    </div>
                    <div class="review_main_right">
                        <a href="#WriteReview" id="lnkWriteReview" class="large">Write A Review</a>
                        <br>
                        <div id="divLogout"><a href="#Logout" id="lnkLogout">Logout</a></div>
                        <div id="divWriteReviewErr" class="requireD"></div>
                    </div>
                    <div class="cleardiv"></div>
                    <hr/>
                    <section id="divCommentDisp">
                        
                    </section>
                    <input type="hidden" id="hidLastFetchId" value="">
                    <input type="hidden" id="hidLastRecord" value="">
                    <input type="hidden" id="hidCommentType" value="">
                    <input type="hidden" id="hidCommentId" value="">
                    <input type="hidden" id="hidCommentedBy" value="">
                    <input type="hidden" id="hidRootPath" value="<%:RootPath %>">
                </section>
                <div style="display:none;">
                    <a id="lnkRegBox" href="<%:RootPath %>controls/registercommenter.aspx?p=<%:RootPath %>" title="Login">Login</a>
                    <a id="lnkReviewBox" href="<%:RootPath %>controls/writecomment.aspx?p=<%:RootPath %>" title="WriteAReview">Write A Review</a>
                    <a id="lnkReplyBox" href="<%:RootPath %>controls/writesubcomment.aspx?p=<%:RootPath %>" title="ReplyReview"></a>

                </div>
            </div>
        </div>
        <div class="col2">
            <div id="social">
                <span>Share this page on</span>
                <ul>
                    <li><a href='http://blog.iqsdirectory.com/' target='_blank' class="iqs">IQS</a></li>
                    <li><a class="google" rel=nofollow
                           href="https://plus.google.com/share?url=<%: ShareURL %>" onclick="javascript:popupwindow(this.href,'',600,600);return false;">
                        Google</a></li>
                    <li><a class="twitter" rel=nofollow
                           href="https://twitter.com/share?url=<%: ShareURL %>&text=%23<%: Master.PageTitle %>. <%: Master.PageDescription %>" onclick="javascript:popupwindow(this.href,'',600,400);return false;" >
                        Twitter</a></li>
                    <li><a class="linkedin" rel=nofollow
                           href="http://www.linkedin.com/shareArticle?mini=true&url=<%: ShareURL %>&title=<%= Master.PageTitle %>&summary=<%: Master.PageDescription %>&source=<%: DirectoryURL %>" onclick="javascript:popupwindow(this.href,'',600,400);return false;">
                        LinedIn</a></li>
                    <li><a class="facebook" rel=nofollow href=""
                           onclick="javascript:postToFeed('<%: Master.PageTitle %>','<%:DirectoryURL %>','<%: Master.PageDescription %>');return false;">
                        Facebook</a></li>
                    <li><a rel=nofollow onclick="" class="lnkmail mail"
                           href="<%:RootPath %>copro-share-page-email.aspx?p=../../&title=<%: Master.PageTitle %>&des=<%: Master.PageDescription %>&url=<%: ShareURL %>">
                        Mail</a></li>
                    <li><a href="" class="print" onclick="javascript:window.print();return false;">
                        Print</a></li>
                </ul>
                <script type="text/javascript">
                    function popupwindow(url, title, w, h) {
                        var left = (screen.width / 2) - (w / 2);
                        var top = (screen.height / 2) - (h / 2);
                        window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
                    }
                    FB.init({
                        appId: '221653014637602',
                        channelUrl: 'http://www.iqsdirectory.com/channel.html',
                        scope: 'id,name,email',
                        status: true,
                        cookie: true,
                        xfbml: true
                    });
                    function postToFeed(cname, title, review) {
                        var lnkurl = $(location).attr('href');
                        var obj = {
                            method: 'feed',
                            redirect_uri: 'http://www.iqsdirectory.com/',
                            link: lnkurl,
                            picture: 'http://www.iqsdirectory.com/images/iqsdirectory_home_logo.png',
                            name: cname,
                            caption: title,
                            description: review
                        };
                        function callback(response) {
                        }
                        FB.ui(obj, callback);
                    }
                </script>
            </div>
            <% if (RelatedCompaniesList != "" && RelatedCompaniesList != null)
                     { %>
            <div id="divRelated" class="rightbox clearfix" >
                <span><%= RelatedCompaniesHead %></span>
                <ul><%= RelatedCompaniesList %></ul>
                <div class="clearfix"></div>
            </div>
            <% } %>
            <% if (RelatedCategories.Count > 0)
                     { %>
            <div id="divAddInfo" class="rightbox clearfix" >
                <span>This Company Can Be Found On</span>

                <div class="col">
                    <ul class="twocols">
                        <% foreach (var dr in RelatedCategories)
                     {  %>
                        <li><a href="http://<%: dr["CATEGORY_URL"].ToString() %>" target="_blank"><%: dr["NAME"].ToString() %></a></li>
                   
                    <% } %>    
                    </ul>
                </div>
                
                <div class="clearfix"></div>
            </div>
            <% } %>
           
            <div id="divEmail">
                <uc1:copropageemail runat="server" id="copropageemail" />
                
            </div>
            <!--<div id="recaptcha2"></div>-->
        </div>
    </section>
</div>
    <script type='text/javascript'>
                    var recaptcha1;
                    var recaptcha2;
                    var captchaReg;
        var captchaCallBack = function () {
            //alert("thx lord jesus" + $("#hdnEmailCaptcha").val());
            if ($("#hdnEmailCaptcha").val() != 'yes') {
                //alert("inside if");
                recaptcha1 = grecaptcha.render('recaptcha1', {
                    'sitekey': '6Lc72zMUAAAAABk1ajqMH-ThUswu6BIps5JS10s_', //Replace this with your Site key
                    'theme': 'light',
                    'callback':verify_callback1
                });
                $("#hdnEmailCaptcha").val('yes');
            }
            if ($("#recaptcha2").length > 0) {
                recaptcha2 = grecaptcha.render('recaptcha2', {
                    'sitekey': '6Lc72zMUAAAAABk1ajqMH-ThUswu6BIps5JS10s_', //Replace this with your Site key
                    'theme': 'light',
                    'callback': verify_callback2
                });
            }
            if ($("#recaptcha3").length > 0) {
                captchaReg = grecaptcha.render('recaptcha3', {
                    'sitekey': '6Lc72zMUAAAAABk1ajqMH-ThUswu6BIps5JS10s_', //Replace this with your Site key
                    'theme': 'light'
                });
            }
        }
    </script>
    <script type='text/javascript'>
        $(document).ready(function () {
            $.get($('#hdnSrhRootPath').val() + 'StateSearch.html', function (data) {
                $('#secsbox').html(data);
            });

            $('.lnkmail').fancybox({ 'height': 420, 'width': 400, 'onStart': function () { $('body').css('overflow', 'hidden'); }, 'onClosed': function () { $('body').css('overflow', 'auto'); }, 'hideOnOverlayClick': false });
            $('.lnkmail').bind('contextmenu', function (e) { return false; });

        });        
        function verify_callback2(response) {
            //alert("Captcha 2="+response);
            $('#hiddenRecaptcha2').val(response);
            $("#frmShare").valid();
        }
        function verify_callback3(response) {
            alert("Captcha 3=" + response);
            $('#hiddenRecaptcha3').val(response);
            $("#frmRegCommeter").valid();
        }
    </script>
    
    <input type='hidden' id='hdnApiPath' value='<%: ApiPath %>' />
    <input type='hidden' id='hdnCategorySK' value='<%: CategorySK %>' />
    <input type='hidden' id='hdnProfileClientSk' value="<%: ClientSK %>" />
    <input type='hidden' id='hdnSrhRootPath' value="<%: RootPath %>" />
    
    <script src="https://www.google.com/recaptcha/api.js?onload=captchaCallBack&render=explicit" async defer></script>
</asp:Content>
