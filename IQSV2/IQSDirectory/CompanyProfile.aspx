<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompanyProfile.aspx.cs" Inherits="IQSDirectory.CompanyProfile" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ register src="~/Controls/copro-page-email.ascx" tagprefix="uc1" tagname="copropageemail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <script src="/Scripts/jquery.cookie.js"></script>
    <script src="/Scripts/jquery.validate.js"></script>
    <script src="/Scripts/company_profile.js"></script>
    <script src="/Scripts/fb.js"></script>    
   <asp:PlaceHolder runat="server">
        <%--: Styles.Render("~/bundles/StyleCopro") %>
        <%--: Scripts.Render("~/bundles/SiteMasterScripts") --%>
        <%--: Scripts.RenderFormat("<script type=\"text/javascript\" src=\"{0}\" defer></script>", "~/bundles/ScriptCopro") --%>
    </asp:PlaceHolder>
    <input type='hidden' id='hdnEmailCaptcha' value="no" />

    

     <div id="copro-main" class="section" style="padding-top:50px;">
  		 
        <section class="container" itemscope itemtype='https://schema.org/LocalBusiness'>
            <div class="row">
        <div class="col s12 m12 l8 xl8"><h1 itemprop="name"><%: ClientName %></h1></div>
        <% if (ShowReviews == "Y")
            { %>
        <div class="col s12 m12 l4 xl4"><div class='divratingnew'><span  id='spanRateHead'>OVERALL RATING: </span>
            <span  id='spanTopRate' class='spanratingclient'>
                        <input name='topstar' type='radio' class='topcommentstar' value='1' title='1'/>
                        <input name='topstar' type='radio' class='topcommentstar' value='2' title='2'/>
                        <input name='topstar' type='radio' class='topcommentstar' value='3' title='3'/>
                        <input name='topstar' type='radio' class='topcommentstar' value='4' title='4'/>
                        <input name='topstar' type='radio' class='topcommentstar' value='5' title='5'/>
                        </span><span id='spanRateNum' class='spanratingnumnew'><b><%: CompRating %></b>/5</span>
        </div></div>
            <script type='text/javascript'>
                $(document).ready(function () {
                    $('input[type=radio].topcommentstar').rating({ required: true });
                    $('input[type=radio].topcommentstar').rating('enable');
                    $('input[type=radio].topcommentstar').rating('select', 0, false);
                    $('input[type=radio].topcommentstar').rating('disable');
                    if (parseInt(<%: CompRating %>) > 0) { $('#spanTopRate').show(); }
                });
             </script></div>
            <% } %>
             <% if (CompCount != "0")
            { %>
        <div class="divagrating" itemprop="aggregateRating" itemscope="itemscope" itemtype="https://schema.org/AggregateRating">
            <meta itemprop="bestRating" content="5"/>
            <meta itemprop="worstRating" content="1"/>
            <meta itemprop="ratingValue" content='<%: CompRating %>'/>
            <meta itemprop="ratingCount" content='<%: CompCount %>'/>
            <!--<meta itemprop="reviewCount" content='0'/>-->

        </div>
             <% } %>
        <div class='row'>
        <div class="col s12 m6 l4 xl3">
            
            <!-- class="col s12 m6 l4" <div style="float:left; width:55%;">-->
             <% if (LogoLink != "")
                 { %>
            <div id="divImage"><img class="clogo" src="<%= LogoLink %>" alt="<%: ClientNameFormatted %>" title="<%: ClientNameFormatted %>" itemprop="image"></div>
            <% } %>
            <div id="divCompUrls"><%= WebsiteLink %></div>
            <div id="divPhone" >
                <% if(Phone != "") { %>
                <b>Phone: </b><span itemprop="telephone"><%= Phone %></span><br>
                <% } %>
                <% if(Fax != "") { %>
                <b>Fax: </b><%= Fax %>
                <% } %>
            </div><br />
            <div id="divAddress">
                <img src="<%:RootPath %>/images/markera.png" alt="Geo Location Marker" title="Geo Location Marker" style="float:left;padding-right:10px;" />
                <div id="lblAddress" itemprop="address"itemscope itemtype="https://schema.org/PostalAddress">
                    <span itemprop="addressLocality"><%= Address %></span>
                <span class="viewMapCopro">
                <a href="<%:RootPath %>copro-map.html?address=<%=MapAddress %>&comp=<%=ClientNameFormatted %>" id="lnkViewMap" class="iframe coproviewmap">View Map</a></span>
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
            <% if (VideoLink.ToString() != "" && VideoLink.ToString() != "#")
                { %>
            <div id="divVideo" class="col s12 m6 l3 xl2" >

                <div id="divYoutube" class="" <%: YoutubeStyle %> >
                    <a id="lnkViewVideo" href="<%: VideoLink %>" class="iframe coproviewvideo "><img src="<%:RootPath %>images/coproplay.png" alt="Play Video" title="Play Video"></a>
                </div>
                
                <script type="text/javascript">
                    $(document).ready(function () {
                        $('#divYoutube').on('click', null, function () {
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
        
        
        <div class="col s12 m12 l5 xl7" itemprop="description" > <!--col s12 m12 l8-->
            <%: ClientDesc %>
        </div>
        </div>
        <div class="row">
        <div id="divResources"></div>
        </div>
    </section>
</div>
<div class="section" style="background-color:#CCCCCC;">
    <div class="container">
    <section class="row">
        <div id="copro-articles" class="col s12 m6 l7">
            <%if (Articles != null) { if (Articles.Count > 0)
            { %>
            <h4><i class="far fa-newspaper"></i> ARTICLES AND PRESS RELEASES</h4>
                
                    <% foreach (var dr in Articles)
                    { %>
                    <div>
   <div class="card blue-grey darken-1">
        <div class="card-content white-text">
          <span class="card-title">
                       
                            <a href="<%=dr["URL"]%>" target="_blank"><%= dr["HEADING"]%></a></span><br>
                        <span class="new badge" data-badge-caption="<%=dr["NAME"]%> - <%=dr["DATE"]%>"></span>
                        <p><%=dr["DESC"]%></p> <a href="<%=dr["URL"]%>" target="_blank" id="download-button" class="btn waves-effect waves-light orange"> Read More </a>
        </div>
        
      </div></div>    
                        
                        
                            
                        
                    
                    <% } %>
                
            
            <% } } %>
            
            
        </div>
        <div class="col s12 m6 l5">
            <div id="divEmail" class="form-bg">
                <uc1:copropageemail runat="server" id="copropageemail" />
                
            </div>
            <br />
            <% if (RelatedCompaniesList != "" && RelatedCompaniesList != null)
                     { %>
            <div id="divRelated" class="related white-text" >
                <h5><%= RelatedCompaniesHead %></h5>
                <ul class="profile"><%= RelatedCompaniesList %></ul>                
            </div>
            <% } %>
            <% if (RelatedCategories.Count > 0)
                     { %>
            <div id="divAddInfo" class="related white-text" >
                <h5>This Company Can Be Found On</h5>

                    <ul class="twocols profile">
                        <% foreach (var dr in RelatedCategories)
                     {  %>
                        <li><i class="fas fa-arrow-right"></i><a href="<%:RootPath %><%: dr["CATEGORYNAME"].ToString() %>/" target="_blank"><%: dr["NAME"].ToString() %></a></li>
                   
                    <% } %>    
                    </ul>
                
            </div>
            <% } %>
           
            
            <!--<div id="recaptcha2"></div>-->
        </div>
    </section>
        </div>
</div>

<div class="section">
<div class="container">
<div id="commentForm" class="row" style="width:90%;margin:0px auto;">
    
<!--<div id="secreviews" class="col s12 m8 l8">-->
    <div id="secreviews" class="">
<h5 style="font-size:40px;"><i class="fas fa-comments"></i> Company Reviews</h5>
     <div class="review_main_left">
                        <div class="review_main_left_inner" id="divCompReview">
                            <input name="star1" type="radio" class="totalreviewstar" value="1" title="1"/>
    <input name="star1" type="radio" class="totalreviewstar" value="2" title="2"/>
    <input name="star1" type="radio" class="totalreviewstar" value="3" title="3"/>
    <input name="star1" type="radio" class="totalreviewstar" value="4" title="4"/>
    <input name="star1" type="radio" class="totalreviewstar" value="5" title="5"/>
                        </div>
                        <div class="main_review_count">Based on <span id="divTotalReviewCount">1</span> reviews</div>
                    </div>
                    <div class="review_main_right">
                        <a href="#WriteReview" id="lnkWriteReview" class="btn waves-effect waves-light orange white-text">Write A Review</a>
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
                
</div>

    <div style="display:none;">
                    <a id="lnkRegBox" href="<%:RootPath %>controls/registercommenter.aspx?p=<%:RootPath %>" title="Login">Login</a>
                    <a id="lnkReviewBox" href="<%:RootPath %>controls/writecomment.aspx?p=<%:RootPath %>" title="WriteAReview">Write A Review</a>
                    <a id="lnkReplyBox" href="<%:RootPath %>controls/writesubcomment.aspx?p=<%:RootPath %>" title="ReplyReview"></a>

                </div>
    
        
      </div>
    </div>
</div>
    
    <script type='text/javascript'>
                    var recaptcha1;
                    var recaptcha2;
                    var recaptcha3;
                
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
                recaptcha3 = grecaptcha.render('recaptcha3', {
                    'sitekey': '6Lc72zMUAAAAABk1ajqMH-ThUswu6BIps5JS10s_', //Replace this with your Site key
                    'theme': 'light',
                    'callback': verify_callback3
                });
            }
        }
    </script>
    <script type='text/javascript'>
        $(document).ready(function () {
            $.get($('#hdnSrhRootPath').val() + 'statesearch.html', function (data) {
                $('#searchBarDir').html(data);
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
            //alert("Captcha 3=" + response);
            $('#hiddenRecaptcha3').val(response);
            $("#frmRegCommenter").valid();
        }
    </script>
    
    <input type='hidden' id='hdnApiPath' value='<%: ApiPath %>' />
    <input type='hidden' id='hdnCategorySK' value='<%: CategorySK %>' />
    <input type='hidden' id='hdnProfileClientSk' value="<%: ClientSK %>" />
    <input type='hidden' id='hdnSrhRootPath' value="<%: RootPath %>" />
    
    <script src="https://www.google.com/recaptcha/api.js?onload=captchaCallBack&render=explicit" async defer></script>
</asp:Content>
