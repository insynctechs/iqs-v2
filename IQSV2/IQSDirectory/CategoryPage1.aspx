﻿<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="CategoryPage1.aspx.cs" inherits="IQSDirectory.CategoryPage1" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%:RootPath %>content/category_styles.css" rel="stylesheet" media='screen'/>
    <link href='<%:RootPath %>content/stylerprint.css' rel='stylesheet' type='text/css' media='print' />
    <link href='<%:RootPath %>content/jquery.fancybox-1.3.4_min.css' rel='Stylesheet' type='text/css' media='screen' />

    <script src='<%:RootPath %>scripts/jquery.rating.pack.js' defer type='text/javascript'></script>
    <script src='<%:RootPath %>scripts/jquery.fancybox-1.3.4.js' defer type='text/javascript'></script>
    <script src='<%:RootPath %>scripts/fb.js' async defer type='text/javascript'></script>
    <script src='<%:RootPath %>scripts/category_page1.js' async defer type='text/javascript'></script>
    <script src='<%:RootPath %>scripts/move_top.js' async defer type='text/javascript'></script>


    <section id='seccat'>
        <div id="social">
            <span>Share this page on</span>
            <ul>
                <li><a href='http://blog.iqsdirectory.com/' target='_blank' class="iqs">IQS</a></li>
                <li><a class="google" 
                    href="https://plus.google.com/share?url=<%: ShareURL %>" onclick="javascript:popupwindow(this.href,'',600,600);return false;">Google</a></li>
                <li><a class="twitter" 
                    href="https://twitter.com/share?url=<%: ShareURL %>&text=%23<%: CategoryTitle %>. <%: MetaDesc %>" onclick="javascript:popupwindow(this.href,'',600,400);return false;">Twitter</a></li>
                <li><a class="linkedin" 
                    href="http://www.linkedin.com/shareArticle?mini=true&url=<%: ShareURL %>&title=<%: CategoryTitle %>&summary=<%: MetaDesc %>&source=<%: DirectoryURL %>" onclick="javascript:popupwindow(this.href,'',600,400);return false;">LinkedIn</a></li>
                <li><a class="facebook"  href=""
                    onclick="javascript:postToFeed('<%: CategoryTitle %>','<%:DirectoryURL %>','<%: MetaDesc %>');return false;">Facebook</a></li>
                <li><a class="iframe lnkmail mail"
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

    <section id='secrelcat'>
        <h2>Related Categories</h2>
        <ul id="ulRelatedCategories">
            <% foreach (var dr in RelatedCategories)
                {  %>
            <li><a href="<%: dr["CATEGORY_URL"].ToString() %>"><%: dr["DISPLAYNAME"].ToString() %></a></li>
            <% } %>
        </ul>
    </section>

     <% if (Tier1Advertisements.Count > 0) { %>
    <section id='sectier1' class="adlist_section">
        <ul class="adlist_ul">
            <% foreach (var drT1Ad in Tier1Advertisements)
                {%>
            <li>
                <header>
                    <h3 class='cname'>
                        <a rel='nofollow' title='<%: drT1Ad["FORMATED_NAME"] %>' target='_blank' href='<%: drT1Ad["COMPANY_URL"] %>' onmouseover="loadWebPreview('<%: drT1Ad["COMPANY_URL"] %>','1', '<%: drT1Ad["IMAGE"] %>');hitsLinkTrack('<%: drT1Ad["HITSLINK"] %>')"><%= drT1Ad["CLIENT_NAME"] %></a>
                        <span><%: drT1Ad["CITY_STATE"] %></span>
                        <span><%: drT1Ad["PHONE"] %></span>
                    </h3>
                    <a href='<%:RootPath %>directoryrfq.aspx?CategorySK=<%: CategorySK %>&amp;ClientSK=<%: drT1Ad["CLIENT_SK"] %>' class='iframe btnrfq'>Request For Quote</a>
                    <a rel='nofollow' class='btncopro' title='<%: drT1Ad["FORMATED_NAME"] %> Profile' id='ID<%: drT1Ad["ADVERTISEMENT_SK"] %>' href='<%:RootPath %><%: drT1Ad["PROFILE_URL"] %>'>View Company Profile</a>
                </header>
                <p class='cdesc'><%: drT1Ad["ADDESCRIPTION"] %></p>
                <div class='divRate'>
                    <div id='divRate<%: drT1Ad["CLIENT_SK"] %>' class="divratingclientmain">
                        <script type='text/javascript'>
                            $(document).ready(function () { $('input[type=radio].star<%: drT1Ad["CLIENT_SK"] %>').rating({ required: true }); });
                        </script>
                        <span class="spanreadreviews">
                            <a title='<%: drT1Ad["FORMATED_NAME"] %> Profile' id='ID<%: drT1Ad["CLIENT_SK"] %>' href='<%: drT1Ad["PROFILE_URL"] %>'>Read Reviews</a>
                        </span>
                        <span class='divratingclient'>
                        <input name='star<%: drT1Ad["CLIENT_SK"] %>' type='radio' class='star<%: drT1Ad["CLIENT_SK"] %>' value='1' title='1'/>
                        <input name='star<%: drT1Ad["CLIENT_SK"] %>' type='radio' class='star<%: drT1Ad["CLIENT_SK"] %>' value='2' title='2'/>
                        <input name='star<%: drT1Ad["CLIENT_SK"] %>' type='radio' class='star<%: drT1Ad["CLIENT_SK"] %>' value='3' title='3'/>
                        <input name='star<%: drT1Ad["CLIENT_SK"] %>' type='radio' class='star<%: drT1Ad["CLIENT_SK"] %>' value='4' title='4'/>
                        <input name='star<%: drT1Ad["CLIENT_SK"] %>' type='radio' class='star<%: drT1Ad["CLIENT_SK"] %>' value='5' title='5'/>
                        </span>
                    </div>
                </div>
            </li>
            <% } %>
            <%--<script language='javascript' type='text/javascript'>$(document).ready(function () {LoadCompanyTotalRatingByArray('62009,56037,73870,69339,71667,77096');});</script>--%>
        </ul>
        <aside>
           <!--
            <iframe id='preview_iframe1' class='foriframe' src='images/cardboard-placeholder.jpg' scrolling='no'></iframe>
            <div class='foriframe' id='iframe_mask1' style='position: absolute; cursor: pointer;'></div>
            -->
            <div id="preview1" class="forpreview"> <!--<img src='https://image.thum.io/get/http://www.google.com/' /> -->
            <img src='<%:RootPath %>images/cardboard-placeholder.jpg' alt="Mouse Over Company Names to see their previews" title="Mouse Over Company Names to see their previews" /></div>
        </aside>
    </section>
    <% } %>

    <% if (Tier2Advertisements.Count > 0) { %>
    <section id='sectier2' class="adlist_section">
        <ul class="adlist_ul">
            <% foreach (var drT2Ad in Tier2Advertisements)
              {%>
            <li>
                <header>
                    <h3 class='cname'>
                        <a rel='nofollow' title='<%: drT2Ad["FORMATED_NAME"] %>' target='_blank' href='<%: drT2Ad["COMPANY_URL"] %>' onmouseover="loadWebPreview('<%: drT2Ad["COMPANY_URL"] %>','2', '<%: drT2Ad["IMAGE"] %>');hitsLinkTrack('<%: drT2Ad["HITSLINK"] %>');"><%= drT2Ad["CLIENT_NAME"] %></a>
                        <span><%: drT2Ad["CITY_STATE"] %></span><span><%: drT2Ad["PHONE"] %></span>
                    </h3>
                    <a href='<%:RootPath %>directoryrfq.aspx?CategorySK=<%: CategorySK %>&amp;ClientSK=<%: drT2Ad["CLIENT_SK"] %>' class='iframe btnrfq'>Request For Quote</a>
                    <a rel='nofollow' class='btncopro' title='<%: drT2Ad["FORMATED_NAME"] %> Profile' id='ID<%: drT2Ad["ADVERTISEMENT_SK"] %>' href='<%:RootPath %><%: drT2Ad["PROFILE_URL"] %>'>View Company Profile</a>
                </header>
                <p class='cdesc'><%: drT2Ad["ADDESCRIPTION"] %></p>
                <div class='divRate'>
                    <div id='divRate<%: drT2Ad["CLIENT_SK"] %>' class="divratingclientmain">
                        <script type='text/javascript'>
                            $(document).ready(function () { $('input[type=radio].star<%: drT2Ad["CLIENT_SK"] %>').rating({ required: true }); });
                        </script>
                        <span class="spanreadreviews">
                            <a title='<%: drT2Ad["FORMATED_NAME"] %> Profile' id='ID<%: drT2Ad["CLIENT_SK"] %>' href='<%: drT2Ad["PROFILE_URL"] %>'>Read Reviews</a>
                        </span>
                        <span class='divratingclient'>
                            <input name='star<%: drT2Ad["CLIENT_SK"] %>' type='radio' class='star<%: drT2Ad["CLIENT_SK"] %>' value='1' title='1' />
                            <input name='star<%: drT2Ad["CLIENT_SK"] %>' type='radio' class='star<%: drT2Ad["CLIENT_SK"] %>' value='2' title='2' />
                            <input name='star<%: drT2Ad["CLIENT_SK"] %>' type='radio' class='star<%: drT2Ad["CLIENT_SK"] %>' value='3' title='3' />
                            <input name='star<%: drT2Ad["CLIENT_SK"] %>' type='radio' class='star<%: drT2Ad["CLIENT_SK"] %>' value='4' title='4' />
                            <input name='star<%: drT2Ad["CLIENT_SK"] %>' type='radio' class='star<%: drT2Ad["CLIENT_SK"] %>' value='5' title='5' /></span>
                    </div>
                </div>
            </li>
            <% } %>
            <%--<script language='javascript' type='text/javascript'>$(document).ready(function () { LoadCompanyTotalRatingByArray('65819,73262,60803,76582'); });</script>--%>
        </ul>
        <aside>
            <!--
            <iframe id='preview_iframe2' class='foriframe' src='images/cardboard-placeholder.jpg' scrolling='no'></iframe>
            <div class='foriframe' id='iframe_mask2' style='position: absolute; cursor: pointer;'></div>
            -->
             <div id="preview2" class="forpreview" > <!--<img src='https://image.thum.io/get/http://www.google.com/' /> -->
            <img src='<%:RootPath %>images/cardboard-placeholder.jpg' alt="Mouse Over Company Names to see their previews" title="Mouse Over Company Names to see their previews" /></div>
        </aside>
    </section>
    <% } %>
    <script type='text/javascript'>
        $(document).ready(function ()
        {
            <% foreach (var cr in ClientRatings)
                {%>
            $('input[type=radio].star' + <%: cr["CLIENT_SK"].ToString() %>).rating('enable');
            $('input[type=radio].star' + <%: cr["CLIENT_SK"].ToString() %>).rating('select', parseInt(<%: cr["RATING"].ToString() %>)-1, false);
            $('input[type=radio].star' + <%: cr["CLIENT_SK"].ToString() %>).rating('disable');
            if (parseInt(<%: cr["RATING"].ToString() %>) > -1) { $('#divRate' + <%: cr["CLIENT_SK"].ToString() %>).show(); }
            <% } %>
        });
    </script>
   <% if (ProductInformation != null)
       { %> 
    <section id='prodinfo'>
        <%: ProductInformation %>
        </section>
    <% }
    else
    { %>
    <section id='secaddcomp'>
        <header id='secsepaddcomp'>
            <a class='addcomp' href='<%:RootPath %><%: CategoryName %>/<%: CategoryName %>-2/'><span>More <%: DisplayName %> Companies</span>
                <img src='<%:RootPath %>images/barrow.png' alt='Click' title='More <%: DisplayName %> Companies'></a>
        </header>
    </section>
    
    <section id='secartmain'>
        <!-- Dynamic Articles and Press Releases -->
        <% if (Articles.Count > 0)
    { %>
        <section id="secarticle">
            <h2 id="secarthead">ARTICLES AND PRESS RELEASES</h2>

            <ul>
                <% foreach (var art in Articles)
    { %>
                <li>

                    <h3><a href="<%: art["URL"].ToString() %>"
                        target="_blank"><%= art["HEADING"].ToString() %></a></h3>
                    <aside>
                        <%= art["NAME"].ToString() %>
                    </aside>
                    <p>
                        <%= art["DISPLAYDESC"].ToString() %>
                    </p>

                </li>
                <% } %>
            </ul>

        </section>
        <% } %>
    </section>
     <section id='secininfo'>
        <header>Industry Information</header>
        <article>
            <%= IndustryInformation %>
        </article>
    </section>
    <% } %>
   

    <script type='text/javascript'>
        $(document).ready(function () {
            $.get($('#hdnSrhRootPath').val() + 'StateSearch.html', function (data) {
                $('#secsbox').html(data);
                $('#txtsearch').val('<%: DisplayName %>');
                $('#txtsearch').attr('class', 'txtsearchsel');
            });

            $('.btnrfq').fancybox({'height':600,'width':800,'onStart':function(){$('body').css('overflow','hidden');},'onClosed':function(){$('body').css('overflow','auto');},'hideOnOverlayClick':false});
            $('.btnrfq').bind('contextmenu', function(e){return false;});
            

            $('.lnkmail').fancybox({ 'height': 420, 'width': 400, 'onStart': function () { $('body').css('overflow', 'hidden'); }, 'onClosed': function () { $('body').css('overflow', 'auto'); }, 'hideOnOverlayClick': false });
            $('.lnkmail').bind('contextmenu', function (e) { return false; });
        });
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
 

