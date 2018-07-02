<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompanyProfile.aspx.cs" Inherits="IQSDirectory.CompanyProfile" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ register src="~/Controls/copro-page-email.ascx" tagprefix="uc1" tagname="copropageemail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <link href='<%:RootPath %>content/stylerprint.css' rel='stylesheet' type='text/css' media='print' />
   <asp:PlaceHolder runat="server">
        <%: Styles.Render("~/bundles/StyleCopro") %>
        <%--: Scripts.Render("~/bundles/SiteMasterScripts") --%>
        <%: Scripts.RenderFormat("<script type=\"text/javascript\" src=\"{0}\" defer></script>", "~/bundles/ScriptCopro") %>
    </asp:PlaceHolder>
    <input type='hidden' id='hdnEmailCaptcha' value="no" />

    <div id='content_wrapper'>
        <section class="row1" itemscope itemtype='https://schema.org/LocalBusiness'>
        <h1 itemprop="name"><%: ClientName %></h1>
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
                    if (parseInt(<%: CompRating %>) > 0) { $('#spanTopRate').show(); }
                });
             </script>
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
        <div class='cleardiv'></div>
        <div class="col1">
             <% if (LogoLink != "")
                 { %>
            <div id="divImage"><img class="clogo" src="<%= LogoLink %>" alt="<%: ClientNameFormatted %>" title="<%: ClientNameFormatted %>" itemprop="image"></div>
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
                <b>Phone: </b><span itemprop="telephone"><%= Phone %></span><br>
                <% } %>
                <% if(Fax != "") { %>
                <b>Fax: </b><%= Fax %>
                <% } %>
            </div>
            <div id="divAddress">
                <img src="<%:RootPath %>/images/markera.png" alt="Geo Location Marker" title="Geo Location Marker" />
                <div id="lblAddress" itemprop="address"itemscope itemtype="https://schema.org/PostalAddress">
                    <span itemprop="addressLocality"><%= Address %></span>
                <br>
                <a href="<%:RootPath %>copro-map.html?address=<%=MapAddress %>&comp=<%=ClientNameFormatted %>" id="lnkViewMap" class="iframe coproviewmap">View Map</a>
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
            <%: ClientDesc %>
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
                        <li><a href="<%:RootPath %><%: dr["CATEGORYNAME"].ToString() %>/" target="_blank"><%: dr["NAME"].ToString() %></a></li>
                   
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
