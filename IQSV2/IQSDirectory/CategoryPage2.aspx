<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="CategoryPage2.aspx.cs" inherits="IQSDirectory.CategoryPage2" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/category_styles.css" rel="stylesheet" media='screen'/>
    <link href='../Content/stylerprint.css' rel='stylesheet' type='text/css' media='print' />
    <link href='../Content/jquery.fancybox-1.3.4.css' rel='Stylesheet' type='text/css' media='screen' />

    <script src='../Scripts/jquery.rating.pack.js' type='text/javascript'></script>
    <script src='../Scripts/jquery.fancybox-1.3.4.js' type='text/javascript'></script>
    <script src='../Scripts/fb.js' type='text/javascript'></script>
    <script src='../Scripts/category_page2.js' type='text/javascript'></script>
    <script src='../Scripts/move_top.js' type='text/javascript'></script>
    <section id='seccat'>
        <div id="social">
            <span>Share this page on</span>
            <ul>
                <li><a href='http://blog.iqsdirectory.com/' target='_blank' class="iqs">IQS</a></li>
                <li><a class="google" rel="nofollow"
                    href="https://plus.google.com/share?url=<%: ShareURL %>" onclick="javascript:popupwindow(this.href,'',600,600);return false;">Google</a></li>
                <li><a class="twitter" rel="nofollow"
                    href="https://twitter.com/share?url=<%: ShareURL %>&text=%23<%: CategoryTitle %>. <%: MetaDesc %>" onclick="javascript:popupwindow(this.href,'',600,400);return false;">Twitter</a></li>
                <li><a class="linkedin" rel="nofollow"
                    href="http://www.linkedin.com/shareArticle?mini=true&url=<%: ShareURL %>&title=<%: CategoryTitle %>&summary=<%: MetaDesc %>&source=<%: DirectoryURL %>" onclick="javascript:popupwindow(this.href,'',600,400);return false;">LinkedIn</a></li>
                <li><a class="facebook" rel="nofollow" href=""
                    onclick="javascript:postToFeed('<%: CategoryTitle %>','<%:DirectoryURL %>','<%: MetaDesc %>');return false;">Facebook</a></li>
                <li><a rel="nofollow" class="iframe lnkmail mail"
                    href="<%:RootPath %>share-page-email.aspx?p=../&title=<%: CategoryTitle %>&des=<%: MetaDesc %>&url=<%: ShareURL %>">Mail</a></li>
                <li><a href="" class="print" onclick="javascript:window.print();return false;">Print</a></li>
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
        <h1 itemprop='name'><%: H1Text %></h1>
        <div class="desc" itemprop='description'><%: ItemDesc %></div>
    </section>

    <section id='secgoto'>
        <article>
            <header><a href='<%:RootPath %><%: CategoryName %>'>Go To <%: DisplayName %> Manufacturers and Companies Home</a></header>
        </article>
    </section>

    <% if (TierAdvertisements.Count > 0) { %>
    <section id='secpage2' class="adlist_section">
        <div class="div_buttons"><a href="<%:RootPath %>directoryrfq.aspx?CategorySK=<%: CategorySK %>&amp;ClientSK="
                                  id="lnkRFQ" class="lnkrfq">Request For Quote</a></div>
        <ul class="adlist_ul">
            <% foreach (var drTAd in TierAdvertisements)
                {%>
            <li>
                <header>
                    <h3 class='cname'>
                        <a rel='nofollow' alt='<%: drTAd["FORMATED_NAME"] %>' title='<%: drTAd["FORMATED_NAME"] %>' target='_blank' href='<%: drTAd["COMPANY_URL"] %>' onmouseover="loadWebPreview('<%: drTAd["COMPANY_URL"] %>', this)"><%: drTAd["CLIENT_NAME"] %></a>
                        <span><%: drTAd["CITY_STATE"] %></span>
                        <span><%: drTAd["PHONE"] %></span>
                    </h3>
                    <!--<a href='directoryrfq.aspx?CategorySK=<%: CategorySK %>&amp;ClientSK=<%: drTAd["CLIENT_SK"] %>' class='btnrfq'>Request For Quote</a>-->
                    <a rel='nofollow' class='btncopro' alt='<%: drTAd["FORMATED_NAME"] %> Profile' title='<%: drTAd["FORMATED_NAME"] %> Profile' id='ID<%: drTAd["ADVERTISEMENT_SK"] %>' href='<%: drTAd["PROFILE_URL"] %>'>View Company Profile</a>
                </header>
                <p class='cdesc'><%: drTAd["ADDESCRIPTION"] %></p>
                <div class='divRate'>
                    <div id='divRate<%: drTAd["CLIENT_SK"] %>' class="divratingclientmain">
                        <script language='javascript' type='text/javascript'>
                            $(document).ready(function () { $('input[type=radio].star<%: drTAd["CLIENT_SK"] %>').rating({ required: true }); });
                        </script>
                        <span class="spanreadreviews">
                            <a alt='<%: drTAd["FORMATED_NAME"] %> Profile' title='<%: drTAd["FORMATED_NAME"] %> Profile' id='ID<%: drTAd["CLIENT_SK"] %>' href='<%: drTAd["PROFILE_URL"] %>'>Read Reviews</a>
                        </span>
                        <span class='divratingclient'>
                            <input name='star<%: drTAd["CLIENT_SK"] %>' type='radio' class='star<%: drTAd["CLIENT_SK"] %>' value='1' title='1' />
                            <input name='star<%: drTAd["CLIENT_SK"] %>' type='radio' class='star<%: drTAd["CLIENT_SK"] %>' value='2' title='2' />
                            <input name='star<%: drTAd["CLIENT_SK"] %>' type='radio' class='star<%: drTAd["CLIENT_SK"] %>' value='3' title='3' />
                            <input name='star<%: drTAd["CLIENT_SK"] %>' type='radio' class='star<%: drTAd["CLIENT_SK"] %>' value='4' title='4' />
                            <input name='star<%: drTAd["CLIENT_SK"] %>' type='radio' class='star<%: drTAd["CLIENT_SK"] %>' value='5' title='5' />
                        </span>
                    </div>
                </div>
            </li>
            <% } %>
            <%--<script language='javascript' type='text/javascript'>$(document).ready(function () { LoadCompanyTotalRatingByArray('62009,56037,73870,69339,71667,77096'); });</script>--%>

        </ul>
        <aside>
            <!--<iframe id='preview_iframe' class='foriframe' src='../images/cardboard-placeholder.jpg' scrolling='no'></iframe>
            <div class='foriframe' id='iframe_mask' style='position: absolute; cursor: pointer;'></div>-->
            <div id="preview1" class="forpreview" > <!--<img src='https://image.thum.io/get/http://www.google.com/' /> -->
            <img src='<%:RootPath %>images/cardboard-placeholder.jpg' alt="Mouse Over Company Names to see their previews" title="Mouse Over Company Names to see their previews" /></div>
        </aside>
    </section>
    <% } %>

    <script type='text/javascript'>
        $(document).ready(function () {
            $.get('../StateSearch.html', function (data) {
                $('#secsbox').html(data);
                $('#txtsearch').val('<%: DisplayName %>');
                $('#txtsearch').attr('class', 'txtsearchsel');
            });
         });

        $('.lnkmail').fancybox({ 'height': 420, 'width': 400, 'onStart': function () { $('body').css('overflow', 'hidden'); }, 'onClosed': function () { $('body').css('overflow', 'auto'); }, 'hideOnOverlayClick': false });
        $('.lnkmail').bind('contextmenu', function (e) { return false; });

    </script>
    <input type='hidden' id='hdnApiPath' value='<%: ApiPath %>' />
    <input type='hidden' id='hdnCategorySK' value='<%: CategorySK %>' />
    <input type='hidden' id='hdnSrhRootPath' value="<%: RootPath %>" />
</asp:Content>
