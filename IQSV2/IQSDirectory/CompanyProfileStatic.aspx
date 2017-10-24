<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompanyProfileStatic.aspx.cs" Inherits="IQSDirectory.CompanyProfileStatic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%:RootPath %>Content/copro_styles.css" rel="stylesheet" />
    <link href='<%:RootPath %>Content/stylerprint.css' rel='stylesheet' type='text/css' media='print' />
    <link href='<%:RootPath %>Content/jquery.fancybox-1.3.4.css' rel='Stylesheet' type='text/css' media='screen' />

    <script src='<%:RootPath %>Scripts/jquery.rating.pack.js' type='text/javascript'></script>
    <script src='<%:RootPath %>Scripts/jquery.fancybox-1.3.4.js' type='text/javascript'></script>
    <script src='<%:RootPath %>Scripts/jquery.cookie.js' type='text/javascript' ></script>
    <script src='<%:RootPath %>Scripts/fb.js' type='text/javascript'></script>
    
    
    <script src='<%:RootPath %>Scripts/move_top.js' type='text/javascript'></script>

    <div id='content_wrapper'>
        <section class="row1" itemscope itemtype='http://schema.org/LocalBusiness'>
        <h1 itemprop="name">Brooks Instrument</h1>
        <div class='divratingnew'><span  id='spanTopRate' class='spanratingclient'>
                        <input name='topstar' type='radio' class='topcommentstar' value='1' title='1'/>
                        <input name='topstar' type='radio' class='topcommentstar' value='2' title='2'/>
                        <input name='topstar' type='radio' class='topcommentstar' value='3' title='3'/>
                        <input name='topstar' type='radio' class='topcommentstar' value='4' title='4'/>
                        <input name='topstar' type='radio' class='topcommentstar' value='5' title='5'/>
                        </span><span id='spanRateNum' class='spanratingnumnew'>5/5</span>
        </div>
        <div class="divagrating" itemprop="aggregateRating" itemscope="itemscope" itemtype="http://schema.org/AggregateRating">
            <meta itemprop="bestRating" content="5"/>
            <meta itemprop="worstRating" content="1"/>
            <meta itemprop="ratingValue" content='0'/>
            <meta itemprop="ratingCount" content='0'/>
            <!--<meta itemprop="reviewCount" content='0'/>-->

        </div>
        <div class='cleardiv'></div>
        <div class="col1">
            <div id="divImage"><img class="clogo" src="" alt="Brooks
             Instrument" title="Brooks Instrument" itemprop="logo"></div>
            <div id="divVideo">

                <div id="divyoutube" class="container" style="background-image:url(http://img.youtube.com/vi/jZdyKt4CbyY/0.jpg); background-size:100% auto;">
                    <img src="./images/coproplay.png" alt="Play Video" title="Play Video">
                </div>
                <a href="./coprovideo.html?v=jZdyKt4CbyY&comp=Brooks Instrument" id="lnkViewVideo" class="iframe coproviewvideo" >View Video</a>
                <script language="javascript" type="text/javascript">
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
            <div id="divCompUrls"><a rel="nofollow" alt="Brooks Instrument" title="Brooks Instrument" href="http://www.brooksinstrument.com/" class="DPFCompanyResource1" target="_blank">www.brooksinstrument.com</a><meta itemprop="url" content="http://www.brooksinstrument.com"></div>
            <div id="divPhone" ><b>Phone: </b>888-554-3569<br>
                <b>Fax: </b>215-362-3745
            </div>
            <div id="divAddress">
                <img src="images/markera.png" alt="Geo Location Marker" title="Geo Location Marker" />
                <div id="lblAddress">407 West Vine Street<br>Hatfield, PA 19440
                <br>
                <a href="./copro-map.html?address=407%20West%20Vine%20Street,Hatfield,PA,19440&amp;comp=Brooks%20Instrument" id="lnkViewMap" class="coproviewmap">View Map</a>
                </div>
                    <script language="javascript" type="text/javascript">
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

        <div class="col2" itemprop="description">
            <p>Our customers look to us for solutions to meet the needs for their flow meters! We offer flow meter solutions for a wide variety of industries, including oil and gas, solar, chemical processing, medical devices, semiconductors, and bio pharmaceuticals! We have a huge variety of flow meter products that rank at the top of their categories for reliability, accuracy, user preference and affordability! Visit our website to learn more about our company and products. We would love to help you today!</p><br><p></p><br><p></p><small>Find more flow solutions at <a title="Key Instruments" href="http://www.keyinstruments.com/" target="_blank">www.KeyInstruments.com</a></small>
            <p></p>
        </div>

        <div class="cleardiv"></div>
        <div id="divResources"></div>
        <div class="cleardiv"></div>
    </section>
    <section class="row2">
        <div class="col1">
            <hr/>
            <section id="secarticles">
                <h2>Articles and Press Releases</h2>
                <ul>
                    <li>
                        <h3>
                            <a href="http://news.iqsdirectory.com/Press-Releases/new-mt3809-variable-area-flow-meter-delivers-supreme-performance-in-extreme-conditions">New MT3809 Variable Area Flow Meter Delivers Supreme Performance in Extreme Conditions</a></h3>
                        <aside>
                            April 25, 2013 - Press Releases
                        </aside>
                        <p>
                            <b>The MT3809 provides unprecedented</b> flexibility to use a single flow meter regardless of application, process configuration or flow rate...
                            <br><br>
                        </p>
                    </li>
                </ul>
            </section>
            <hr/>
            <div id="commentForm">
                <section id="secreviews">
                    <h2>Company Reviews</h2>
                    <div class="review_main_left">
                        <div class="review_main_left_inner" id="divCompReview">
                            <div class="star-rating-control star-rating-readonly">
                                <div class="rating-cancel" style="display: none;">
                                    <a title="Cancel Rating"></a>
                                </div>

                                <div class="star-rating rater-0 totalreviewstar star-rating-applied star-rating-live star-rating-on"><a title="1">1</a></div>
                                <div class="star-rating rater-0 totalreviewstar star-rating-applied star-rating-live star-rating-on"><a title="2">2</a></div>
                                <div class="star-rating rater-0 totalreviewstar star-rating-applied star-rating-live star-rating-on"><a title="3">3</a></div>
                                <div class="star-rating rater-0 totalreviewstar star-rating-applied star-rating-live star-rating-on"><a title="4">4</a></div>
                                <div class="star-rating rater-0 totalreviewstar star-rating-applied star-rating-live star-rating-on"><a title="5">5</a></div>
                            </div>
                            <input name="star1" type="radio" class="totalreviewstar star-rating-applied star-rating-readonly" value="1" title="1" style="display: none;" disabled="disabled">
                            <input name="star1" type="radio" class="totalreviewstar star-rating-applied star-rating-readonly" value="2" title="2" style="display: none;" disabled="disabled">
                            <input name="star1" type="radio" class="totalreviewstar star-rating-applied star-rating-readonly" value="3" title="3" style="display: none;" disabled="disabled">
                            <input name="star1" type="radio" class="totalreviewstar star-rating-applied star-rating-readonly" value="4" title="4" style="display: none;" disabled="disabled">
                            <input name="star1" type="radio" class="totalreviewstar star-rating-applied star-rating-readonly" value="5" title="5" style="display: none;" checked="checked" disabled="disabled">

                        </div>
                        <h3 class="main_review_count"><span id="divTotalReviewCount">1</span> reviews</h3>
                    </div>
                    <div class="review_main_right">
                        <a href="#WriteReview" id="lnkWriteReview"><img src="./images/write_a_review_button.png" alt="Write A Review" title="Write A Review"></a>
                        <br>
                        <div id="divLogout"><a href="#Logout" id="lnkLogout">Logout</a></div>
                        <div id="divWriteReviewErr" class="requireD"></div>
                    </div>
                    <div class="cleardiv"></div>
                    <hr/>
                    <section id="divCommentDisp">
                        <div class="divComments" id="divCommentid" ><input type="hidden" id="hdCommentId" value="123"><input type="hidden" id="hdCommenter" value="TSA">
                            <div class="review_title_wrapper">
                                <h2>Great engineering experience</h2>
                                <div class="review_meta_wrapper">
                                    <h3>By <span>TSA</span>- <span>07/19/12</span></h3>
                                </div>
                            </div>
                            <div class="review_rating_wrapper">
                                <div class="star-rating-control star-rating-readonly">
                                    <div class="rating-cancel" style="display: none;"><a title="Cancel Rating"></a></div>
                                    <div class="star-rating rater-0 commentstar123 star-rating-applied star-rating-live star-rating-on"><a title="1">1</a></div>
                                    <div class="star-rating rater-0 commentstar123 star-rating-applied star-rating-live star-rating-on"><a title="2">2</a></div>
                                    <div class="star-rating rater-0 commentstar123 star-rating-applied star-rating-live star-rating-on"><a title="3">3</a></div>
                                    <div class="star-rating rater-0 commentstar123 star-rating-applied star-rating-live star-rating-on"><a title="4">4</a></div>
                                    <div class="star-rating rater-0 commentstar123 star-rating-applied star-rating-live star-rating-on"><a title="5">5</a></div>
                                </div>
                                <input name="star1" type="radio" class="commentstar123 star-rating-applied star-rating-readonly" value="1" title="1" disabled="disabled" style="display: none;">
                                <input name="star1" type="radio" class="commentstar123 star-rating-applied star-rating-readonly" value="2" title="2" disabled="disabled" style="display: none;">
                                <input name="star1" type="radio" class="commentstar123 star-rating-applied star-rating-readonly" value="3" title="3" disabled="disabled" style="display: none;">
                                <input name="star1" type="radio" class="commentstar123 star-rating-applied star-rating-readonly" value="4" title="4" disabled="disabled" style="display: none;">
                                <input name="star1" type="radio" class="commentstar123 star-rating-applied star-rating-readonly" value="5" title="5" checked="checked" disabled="disabled" style="display: none;">
                            </div>
                            <div class="clearfix"></div>
                            <div class="review_content_wrapper">With EFG you get great engineering experience to get the right fastener for your application. EFG’s recent growth through multi-company acquisitions has them on pace to become the leading domestic manufacturer of specialty fasteners in North America.</div>
                            <div id="divCom123" class="review_action_wrapper">
                                <span class="spnHelpful">Was this helpful? <a class="lnkHelpful" href="http://www.iqsdirectory.com/profile/elgin-fastener-group-58208/#Helpful"><img alt="Yes" src="./images/helpful_button.png"></a></span>
                                <span class="spnHelpCount">2</span><span class="spnHelpCountDesc">&nbsp;people found this review useful</span><span><a class="lnkReply" href="http://www.iqsdirectory.com/profile/elgin-fastener-group-58208/#Reply"><img alt="Yes" src="./images/reply_button.png"></a></span>
                                <div id="divReply123" style="padding-left:30px;"></div>
                            </div>
                        </div>
                    </section>
                    <input type="hidden" id="hidLastFetchId" value="123">
                    <input type="hidden" id="hidLastRecord" value="1">
                    <input type="hidden" id="hidCommentType" value="">
                    <input type="hidden" id="hidCommentId" value="0">
                    <input type="hidden" id="hidCommentedBy" value="">
                    <input type="hidden" id="hidRootPath" value="<%: RootPath %>">
                </section>
                <div style="display:block;">

                    <a id="lnkRegBox" href="controls/registercommenter" title="Login">Login</a>
                    <a id="lnkReviewBox" href="controls/writecomment" title="WriteAReview">Write A Review</a>
                    <a id="lnkReplyBox" href="controls/writesubcomment" title="ReplyReview"></a>

                </div>
            </div>
        </div>
        <div class="col2">
            <div id="social">
                <span>Share this page on</span>
                <ul>
                    <li><a href='http://blog.iqsdirectory.com/' target='_blank' class="iqs">IQS</a></li>
                    <li><a class="google" rel=nofollow
                           href="https://plus.google.com/share?url=http://www.iqsdirectory.com/metal-stampings" onclick="javascript:popupwindow(this.href,'',600,600);return false;">
                        Google</a></li>
                    <li><a class="twitter" rel=nofollow
                           href="https://twitter.com/share?url=http://www.iqsdirectory.com/metal-stampings&text=%23Top 10 Metal Stampings Companies + Services [2017 Updated]. Easily locate 10 metal stampings companies. Complex, requirements, fast rfq, design, experience, iso certified, drawings, quick, quote, customized." onclick="javascript:popupwindow(this.href,'',600,400);return false;" >
                        Twitter</a></li>
                    <li><a class="linkedin" rel=nofollow
                           href="http://www.linkedin.com/shareArticle?mini=true&url=http://www.iqsdirectory.com/metal-stampings&title=Top 10 Metal Stampings Companies + Services [2017 Updated]&summary=Easily locate 10 metal stampings companies. Complex, requirements, fast rfq, design, experience, iso certified, drawings, quick, quote, customized.&source=http://www.iqsdirectory.com/" onclick="javascript:popupwindow(this.href,'',600,400);return false;">
                        LinedIn</a></li>
                    <li><a class="facebook" rel=nofollow href=""
                           onclick="javascript:postToFeed('Top 10 Metal Stampings Companies + Services [2017 Updated]','http://www.iqsdirectory.com/','Easily locate 10 metal stampings companies. Complex, requirements, fast rfq, design, experience, iso certified, drawings, quick, quote, customized.');return false;">
                        Facebook</a></li>
                    <li><a rel=nofollow class="lnkmail mail"
                           href="../controls/MailSend.aspx?p=../&title=Top 10 Metal Stampings Companies + Services [2017 Updated]&des=Easily locate 10 metal stampings companies. Complex, requirements, fast rfq, design, experience, iso certified, drawings, quick, quote, customized.&url=http://www.iqsdirectory.com/metal-stampings">
                        Mail</a></li>
                    <li><a href="" class="print" onclick="javascript:window.print();return false;">
                        Print</a></li>
                </ul>
                <script type="text/javascript">
                    function popupwindow(url, title, w, h) {
                        var left = (screen.width/2)-(w/2);
                        var top = (screen.height/2)-(h/2);
                        window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width='+w+', height='+h+', top='+top+', left='+left);
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
            <div id="divRelated" class="rightbox clearfix" >
                <span>Find Related Manufacturers</span>
                <ul><li><a href="http://www.iqsdirectory.com/profile/b-and-d-cold-headed-products-68780/" target="_blank">B &amp; D Cold Headed Products</a></li>
                        <li><a href="http://www.iqsdirectory.com/profile/wayne-bolt-and-nut-67299/" target="_blank">Wayne Bolt &amp; Nut Company</a></li>
                        <li><a href="http://www.iqsdirectory.com/profile/affordable-fastener-supply-73818/" target="_blank">Affordable Fastener Supply Co.</a></li>
                        <li><a href="http://www.iqsdirectory.com/profile/assembly-products-55422/" target="_blank">Assembly Products, Inc.</a></li>
                        <li><a href="http://www.iqsdirectory.com/profile/associated-fastening-products-55428/" target="_blank">Associated Fastening Products, Inc.</a></li>
                        <li><a href="http://www.iqsdirectory.com/profile/dan-loc-bolt-and-gasket-57425/" target="_blank">DAN-LOC Bolt &amp; Gasket</a></li>
                        <li><a href="http://www.iqsdirectory.com/profile/dyson-70108/" target="_blank">Dyson Corporation</a></li>
                        <li><a href="http://www.iqsdirectory.com/profile/engineered-components-64082/" target="_blank">Engineered Components Company</a></li>
                        <li><a href="http://www.iqsdirectory.com/profile/ford-fasteners-67787/" target="_blank">Ford Fasteners, Inc.</a></li>
                        <li><a href="http://www.iqsdirectory.com/profile/ken-forging-60829/" target="_blank">Ken Forging</a></li>
                        <li><a href="http://www.iqsdirectory.com/profile/sc-fastening-systems-72612/" target="_blank">SC Fastening Systems, LLC.</a></li>
                        <li><a href="http://www.iqsdirectory.com/profile/southern-fasteners-and-supply-77748/" target="_blank">Southern Fasteners &amp; Supply, Inc.</a></li>
                        <li><a href="http://www.iqsdirectory.com/profile/stalcop-65498/" target="_blank">Stalcop</a></li>
                    </ul>
                <div class="clearfix"></div>
            </div>
            <div id="divAddInfo" class="rightbox clearfix" >
                <span>This Company Can Be Found On</span>

                <div class="col">
                    <ul class="twocols"><li><a href="http://www.iqsdirectory.com/bolts/" target="_blank">Bolts</a></li>
                        <li><a href="http://www.iqsdirectory.com/fasteners/" target="_blank">Fasteners</a></li>
                        <li><a href="http://www.iqsdirectory.com/hex-bolts/" target="_blank">Hex Bolts</a></li>
                        <li><a href="http://www.iqsdirectory.com/nut-manufacturers/" target="_blank">Nut Manufacturers</a></li>
                        <li><a href="http://www.iqsdirectory.com/screw-manufacturers/" target="_blank">Screw Manufacturers</a></li>
                        <li><a href="http://www.iqsdirectory.com/carriage-bolts/" target="_blank">Carriage Bolts</a></li>
                        <li><a href="http://www.iqsdirectory.com/metal-fasteners/" target="_blank">Metal Fasteners</a></li>
                        <li><a href="http://www.iqsdirectory.com/rivet-manufacturers/" target="_blank">Rivet Manufacturers</a></li>
                        <li><a href="http://www.iqsdirectory.com/thru-bolts/" target="_blank">Thru-Bolts</a></li>
                    </ul>
                </div>
                <div class="clearfix"></div>
            </div>
            <div id="divTradeNames" class="rightbox clearfix" style="display:none;">
                <span>Tradenames</span>
                <div class="col">
                    <ul id="ulTradeLeft">

                    </ul>
                </div>
                <div class="col">
                    <ul id="ulTradeRight">

                    </ul>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
        <div id="divEmail"></div>
    </section>
</div>
    <script type='text/javascript'>
        $(document).ready(function () {
            /*$.get($('#hdnSrhRootPath').val() + 'StateSearch.html', function (data) {
                $('#secsbox').html(data);
                $('#txtsearch').val('');
                $('#txtsearch').attr('class', 'txtsearchsel');
            });*/
                    });


        $(document).ready(function () {
            $('#hidRootPath').val($('#hdnSrhRootPath').val());
            
            $('#lnkRegBox').fancybox({
                'padding': 0,
                'showCloseButton': true,
                'modal': true,
                'titleShow': false
            });
            $('#lnkReviewBox').fancybox({
                'padding': 0,
                'showCloseButton': true,
                'modal': true,
                'titleShow': false
            });
            $('#lnkReplyBox').fancybox({
                'padding': 0,
                'showCloseButton': true,
                'modal': true,
                'titleShow': false
            });

           

            $('#lnkWriteReview').live('click', function () {
                $('#hidCommentType').val('Review');
                $('#lnkRegBox').trigger('click');
                
            });

            
            
        });

        

    </script>

    <input type='hidden' id='hdnApiPath' value='<%: ApiPath %>' />
    <input type='hidden' id='hdnCategorySK' value='<%: CategorySK %>' />
    <input type='hidden' id='hdnSrhRootPath' value="<%: RootPath %>" />
</asp:Content>