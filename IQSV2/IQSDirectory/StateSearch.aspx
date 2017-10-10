<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StateSearch.aspx.cs" Inherits="IQSDirectory.StateSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%:RootPath %>Content/category_styles.css" rel="stylesheet" media='screen'/>
    <link href='<%:RootPath %>Content/stylerprint.css' rel='stylesheet' type='text/css' media='print' />
    <link href='<%:RootPath %>Content/jquery.fancybox-1.3.4.css' rel='Stylesheet' type='text/css' media='screen' />

    <script src='<%:RootPath %>Scripts/jquery.rating.pack.js' type='text/javascript'></script>
    <script src='<%:RootPath %>Scripts/jquery.fancybox-1.3.4.js' type='text/javascript'></script>
    <script src='<%:RootPath %>Scripts/fb.js' type='text/javascript'></script>
    <script src='<%:RootPath %>Scripts/category_page2.js' type='text/javascript'></script>
    <script src='<%:RootPath %>Scripts/move_top.js' type='text/javascript'></script>
    <section id='seccat'>
    <div id="social">
        <span>Share this page on</span>
        <ul>
            <li><a href='http://blog.iqsdirectory.com/' target='_blank' class="iqs">IQS</a></li>
            <li><a class="google" 
                   href="https://plus.google.com/share?url=<%: ShareURL %>" onclick="javascript:popupwindow(this.href,'',600,600);return false;">
                Google</a></li>
            <li><a class="twitter" 
                   href="https://twitter.com/share?url=<%: ShareURL %>&text=%23<%: CategoryTitle %>. <%: MetaDesc %>" onclick="javascript:popupwindow(this.href,'',600,400);return false;" >
                Twitter</a></li>
            <li><a class="linkedin" 
                   href="http://www.linkedin.com/shareArticle?mini=true&url=<%: ShareURL %>&title=<%: CategoryTitle %>&summary=<%: MetaDesc %>&source=<%: DirectoryURL %>" onclick="javascript:popupwindow(this.href,'',600,400);return false;">
                LinedIn</a></li>
            <li><a class="facebook"  href=""
                   onclick="javascript:postToFeed('<%: CategoryTitle %>','<%:DirectoryURL %>','<%: MetaDesc %>');return false;">
                Facebook</a></li>
            <li><a  class="iframe lnkmail mail"
                   href="../controls/MailSend.aspx?p=../&title=<%: CategoryTitle %>&des=<%: MetaDesc %>&url=<%: ShareURL %>">
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
    <h1 itemprop='name'><%: DisplayName %></h1>
    <div class="desc" itemprop='description'><%: ItemDesc %></div>
</section>

<section id='secrelcat'>
    <h2>Related Categories</h2>
    <ul id="ulRelatedCategories">
        <% foreach (var dr in RelatedCategories)
                {  %>
            <li><a href="<%:RootPath %><%: dr["CATEGORY_URL"].ToString() %>"><%: dr["DISPLAY_NAME"].ToString() %></a></li>
        <% } %>
    </ul>
</section>

    <section id='secadpage' class="adlist_section boxnone">
        <div class="div_buttons"><a href="<%:RootPath %>directoryrfq.aspx?CategorySK=<%: CategorySK %>&amp;ClientSK="
                                  id="lnkRFQ" class="iframe lnkrfq" style="float:left;">Request For Quote</a>
            <a href="<%:RootPath %><%: CategoryName %>/" id="lnkBack" class="iframe"
                style="float:left; ">Go To <%: H1Text %> Home</a></div>
        <ul class="adlist_ul">
            <h2><%: H1Text %> Companies Serving <%: CurrentState %></h2>
            <% foreach (var stAd in StateAdvertisements)
                { %>
            <li>
                <header>
                    <h3 class='cname'>
                        <a rel='nofollow' alt='<%: stAd["FORMATED_NAME"] %>' title='<%: stAd["FORMATED_NAME"] %>' target='_blank' href='<%: stAd["COMPANY_URL"] %>' onmouseover="loadWebPreview('<%: stAd["COMPANY_URL"] %>', this)"><%: stAd["CLIENT_NAME"] %></a>
                        <span><%: stAd["CITY_STATE"] %></span>
                        <span><%: stAd["PHONE"] %></span>
                    </h3>
                   
                    <a rel='nofollow' class='btncopro'   alt='<%: stAd["FORMATED_NAME"] %> Profile' title='<%: stAd["FORMATED_NAME"] %> Profile' id='ID<%: stAd["ADVERTISEMENT_SK"] %>' href='<%:RootPath %><%: stAd["COPRA_PATH"] %>'>View Company Profile</a>
                </header>
                <p class='cdesc'><%: stAd["ADDESCRIPTION"] %>
                </p>
                <div class='divRate'>
                    <div id='divRate<%: stAd["CLIENT_SK"] %>' class="divratingclientmain">
                        <script language='javascript' type='text/javascript'>
                            $(document).ready(function () { $('input[type=radio].star<%: stAd["CLIENT_SK"] %>').rating({required: true});});
                        </script>
                    <span class="spanreadreviews">
                        <a alt='<%: stAd["FORMATED_NAME"] %> Profile' title='<%: stAd["FORMATED_NAME"] %> Profile' id='ID<%: stAd["CLIENT_SK"] %>' href='<%:RootPath %><%: stAd["COPRA_PATH"] %>'>Read Reviews</a>
                    </span>
                    <span class='divratingclient'>
                        <input name='star<%: stAd["CLIENT_SK"] %>' type='radio' class='star<%: stAd["CLIENT_SK"] %>' value='1' title='1'/>
                        <input name='star<%: stAd["CLIENT_SK"] %>' type='radio' class='star<%: stAd["CLIENT_SK"] %>' value='2' title='2'/>
                        <input name='star<%: stAd["CLIENT_SK"] %>' type='radio' class='star<%: stAd["CLIENT_SK"] %>' value='3' title='3'/>
                        <input name='star<%: stAd["CLIENT_SK"] %>' type='radio' class='star<%: stAd["CLIENT_SK"] %>' value='4' title='4'/>
                        <input name='star<%: stAd["CLIENT_SK"] %>' type='radio' class='star<%: stAd["CLIENT_SK"] %>' value='5' title='5'/>
                    </span>
                    </div>
                </div>
            </li>
            <% } %>
            <% 
                string scode = "", precode = "";
                foreach (var nghAd in NeighAdvertisements)
                { 
                    scode = nghAd["theState"].ToString();
                    %>
            <% if (scode != precode)
                { %>
            <h2><%: H1Text %> Companies Serving <%: nghAd["STATECODE"].ToString()  %></h2>
            <%} %>
            <li>
                <header>
                    <h3 class='cname'>
                        <a rel='nofollow' alt='<%: nghAd["FORMATED_NAME"] %>' title='<%: nghAd["FORMATED_NAME"] %>' target='_blank' href='<%: nghAd["COMPANY_URL"] %>' onmouseover="loadWebPreview('<%: nghAd["COMPANY_URL"] %>', this)"><%: nghAd["CLIENT_NAME"] %></a>
                        <span><%: nghAd["CITY_STATE"] %></span>
                        <span><%: nghAd["PHONE"] %></span>
                    </h3>
                    <a href='<%:RootPath %>directoryrfq.aspx?CategorySK=<%: CategorySK %>&amp;ClientSK=<%: nghAd["CLIENT_SK"] %>' class='btnrfq'
                            >Request For Quote</a>
                    <a rel='nofollow' class='btncopro'   alt='<%: nghAd["FORMATED_NAME"] %> Profile' title='<%: nghAd["FORMATED_NAME"] %> Profile' id='ID<%: nghAd["ADVERTISEMENT_SK"] %>' href='<%:RootPath %><%: nghAd["COPRA_PATH"] %>'>View Company Profile</a>
                </header>
                <p class='cdesc'><%: nghAd["ADDESCRIPTION"] %>
                </p>
                <div class='divRate'>
                    <div id='divRate<%: nghAd["CLIENT_SK"] %>' class="divratingclientmain">
                        <script language='javascript' type='text/javascript'>
                            $(document).ready(function () { $('input[type=radio].star<%: nghAd["CLIENT_SK"] %>').rating({ required: true }); });
                        </script>
                    <span class="spanreadreviews">
                        <a alt='<%: nghAd["FORMATED_NAME"] %> Profile' title='<%: nghAd["FORMATED_NAME"] %> Profile' id='ID<%: nghAd["CLIENT_SK"] %>' href='<%:RootPath %><%: nghAd["COPRA_PATH"] %>'>Read Reviews</a>
                    </span>
                    <span class='divratingclient'>
                        <input name='star<%: nghAd["CLIENT_SK"] %>' type='radio' class='star<%: nghAd["CLIENT_SK"] %>' value='1' title='1'/>
                        <input name='star<%: nghAd["CLIENT_SK"] %>' type='radio' class='star<%: nghAd["CLIENT_SK"] %>' value='2' title='2'/>
                        <input name='star<%: nghAd["CLIENT_SK"] %>' type='radio' class='star<%: nghAd["CLIENT_SK"] %>' value='3' title='3'/>
                        <input name='star<%: nghAd["CLIENT_SK"] %>' type='radio' class='star<%: nghAd["CLIENT_SK"] %>' value='4' title='4'/>
                        <input name='star<%: nghAd["CLIENT_SK"] %>' type='radio' class='star<%: nghAd["CLIENT_SK"] %>' value='5' title='5'/>
                    </span>
                    </div>
                </div>
            </li>
            <% 
                precode = nghAd["theState"].ToString();
                } %>
            <%--<script language='javascript' type='text/javascript'>$(document).ready(function () {LoadCompanyTotalRatingByArray('62009,56037,73870,69339,71667,77096');});</script>--%>

        </ul>
        <aside>
            <!--<iframe id='preview_iframe' class='foriframe' src='../images/cardboard-placeholder.jpg' scrolling='no'></iframe>
            <div class='foriframe' id='iframe_mask' style='position: absolute; cursor: pointer;'></div>-->
            <div id="preview1" class="forpreview"> <!--<img src='https://image.thum.io/get/http://www.google.com/' /> -->
            <img src='<%:RootPath %>images/cardboard-placeholder.jpg' alt="Mouse Over Company Names to see their previews" title="Mouse Over Company Names to see their previews" /></div>
        </aside>
    </section>
    <% if (OtherAdvertisements.Count > 0) { %>
    <section id='secad_canada' >
    <h2>WE LIST FOR OTHER COUNTRIES TOO!</h2>
        <h3><%: H1Text %> manufacturers in CANADA STATES</h3>
        <ul>
            <% foreach (var othAd in OtherAdvertisements)
                { %>
            <li><a href="<%: RootPath %><%: CategoryName %>/<%: othAd["SEARCH_URL"].ToString() %>"><%: othAd["NAME"].ToString() %>(<%: othAd["NUMBER_OF_CLIENTS"].ToString() %>)</a></li>
            <% } %>
        </ul>
    </section>
    <% } %>
    <script type='text/javascript'>
        $(document).ready(function () {
            $.get($('#hdnSrhRootPath').val()+'StateSearch.html', function (data) {
                $('#secsbox').html(data);
                $('#txtsearch').val('<%: DisplayName %>');
                $('#txtsearch').attr('class', 'txtsearchsel');
            });

            $('.btnrfq').fancybox({ 'height': 600, 'width': 800, 'onStart': function () { $('body').css('overflow', 'hidden'); }, 'onClosed': function () { $('body').css('overflow', 'auto'); }, 'hideOnOverlayClick': false });
            $('.btnrfq').bind('contextmenu', function (e) { return false; });
            $('.btnrfq').css('display', 'block');
        });
    </script>
    <input type='hidden' id='hdnApiPath' value='<%: ApiPath %>' />
    <input type='hidden' id='hdnCategorySK' value='<%: CategorySK %>' />
    <input type='hidden' id='hdnSrhRootPath' value="<%: RootPath %>" />
</asp:Content>
