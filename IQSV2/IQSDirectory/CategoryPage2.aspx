<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="CategoryPage2.aspx.cs" inherits="IQSDirectory.CategoryPage2" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%:RootPath %>content/category_styles.css" rel="stylesheet" media='screen'/>
    <link href='<%:RootPath %>content/stylerprint.css' rel='stylesheet' type='text/css' media='print' />
    <link href='<%:RootPath %>content/jquery.fancybox-1.3.4_min.css' rel='Stylesheet' type='text/css' media='screen' />

    <script src='<%:RootPath %>scripts/jquery.rating.pack.js' type='text/javascript'></script>
    <script src='<%:RootPath %>scripts/jquery.fancybox-1.3.4.js' type='text/javascript'></script>
    <script src='<%:RootPath %>scripts/fb.js' async defer type='text/javascript'></script>
    <script src='<%:RootPath %>scripts/category_page2.js' async defer type='text/javascript'></script>
    <script src='<%:RootPath %>scripts/move_top.js' async defer type='text/javascript'></script>
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
                                  id="lnkRFQ" class="iframe btnrfq">Request For Quote</a></div>
        <ul class="adlist_ul">
            <% foreach (var drTAd in TierAdvertisements)
                {%>
            <li>
                <header>
                    <h3 class='cname'>
                        <a rel='nofollow' title='<%: drTAd["FORMATED_NAME"] %>' target='_blank' href='<%: drTAd["COMPANY_URL"] %>' onmouseover="loadWebPreview('<%: drTAd["COMPANY_URL"] %>', '<%: drTAd["IMAGE"] %>');hitsLinkTrack('<%: drTAd["HITSLINK"] %>')"><%= drTAd["CLIENT_NAME"] %></a>
                        <span><%: drTAd["CITY_STATE"] %></span>
                        <span><%: drTAd["PHONE"] %></span>
                    </h3>
                    <!--<a href='directoryrfq.aspx?CategorySK=<%: CategorySK %>&amp;ClientSK=<%: drTAd["CLIENT_SK"] %>' class='btnrfq'>Request For Quote</a>-->
                    <a rel='nofollow' class='btncopro' title='<%: drTAd["FORMATED_NAME"] %> Profile' id='ID<%: drTAd["ADVERTISEMENT_SK"] %>' href='<%=RootPath %><%: drTAd["PROFILE_URL"] %>'>View Company Profile</a>
                </header>
                <p class='cdesc'><%: drTAd["ADDESCRIPTION"] %></p>
                <div class='divRate'>
                    <div id='divRate<%: drTAd["CLIENT_SK"] %>' class="divratingclientmain">
                        
                        <span class="spanreadreviews">
                            <a title='<%: drTAd["FORMATED_NAME"] %> Profile' id='ID<%: drTAd["CLIENT_SK"] %>' href='<%: drTAd["PROFILE_URL"] %>'>Read Reviews</a>
                        </span>
                        <span class='divratingclient'>
                            <input name='star<%: drTAd["CLIENT_SK"] %>' type='radio' class='star<%: drTAd["CLIENT_SK"] %>' value='1' title='1' />
                            <input name='star<%: drTAd["CLIENT_SK"] %>' type='radio' class='star<%: drTAd["CLIENT_SK"] %>' value='2' title='2' />
                            <input name='star<%: drTAd["CLIENT_SK"] %>' type='radio' class='star<%: drTAd["CLIENT_SK"] %>' value='3' title='3' />
                            <input name='star<%: drTAd["CLIENT_SK"] %>' type='radio' class='star<%: drTAd["CLIENT_SK"] %>' value='4' title='4' />
                            <input name='star<%: drTAd["CLIENT_SK"] %>' type='radio' class='star<%: drTAd["CLIENT_SK"] %>' value='5' title='5' />
                        </span>
                        <script type='text/javascript'>
                            $(document).ready(function () { $('input[type=radio].star<%: drTAd["CLIENT_SK"] %>').rating({ required: true }); });
                        </script>
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
            $.get($('#hdnSrhRootPath').val() + 'StateSearch.html', function (data) {
                $('#secsbox').html(data);
                $('#txtsearch').val('<%: DisplayName %>');
                $('#txtsearch').attr('class', 'txtsearchsel');
            });
            <% foreach (var cr in ClientRatings)
                {%>
            $('input[type=radio].star' + <%: cr["CLIENT_SK"].ToString() %>).rating('enable');
            $('input[type=radio].star' + <%: cr["CLIENT_SK"].ToString() %>).rating('select', parseInt(<%: cr["RATING"].ToString() %>)-1, false);
            $('input[type=radio].star' + <%: cr["CLIENT_SK"].ToString() %>).rating('disable');
            if (parseInt(<%: cr["RATING"].ToString() %>) > -1) { $('#divRate' + <%: cr["CLIENT_SK"].ToString() %>).show(); }
            <% } %>
         });

        $('.lnkmail').fancybox({ 'height': 420, 'width': 400, 'onStart': function () { $('body').css('overflow', 'hidden'); }, 'onClosed': function () { $('body').css('overflow', 'auto'); }, 'hideOnOverlayClick': false });
        $('.lnkmail').bind('contextmenu', function (e) { return false; });

        $('.btnrfq').fancybox({ 'height': 600, 'width': 800, 'onStart': function () { $('body').css('overflow', 'hidden'); }, 'onClosed': function () { $('body').css('overflow', 'auto'); }, 'hideOnOverlayClick': false });
        $('.btnrfq').bind('contextmenu', function (e) { return false; });
        

    </script>
    <input type='hidden' id='hdnApiPath' value='<%: ApiPath %>' />
    <input type='hidden' id='hdnCategorySK' value='<%: CategorySK %>' />
    <input type='hidden' id='hdnSrhRootPath' value="<%: RootPath %>" />
     <!-- HitsLink.com tracking script -->
    <script>
        var wa_hp = 'http' + (location.protocol == 'https:' ? 's' : '');
        document.getElementById('wa_u').src = wa_hp + '://counter.hitslink.com/track.js';
    </script>
    <script type="text/javascript" id="wa_u" defer></script> 
</asp:Content>
