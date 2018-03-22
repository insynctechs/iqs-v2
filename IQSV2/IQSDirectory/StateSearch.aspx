<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StateSearch.aspx.cs" Inherits="IQSDirectory.StateSearch" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%:RootPath %>content/category_styles.css" rel="stylesheet" media='screen'/>
    <link href='<%:RootPath %>content/stylerprint.css' rel='stylesheet' type='text/css' media='print' />
    <link href='<%:RootPath %>content/jquery.fancybox-1.3.4_min.css' rel='Stylesheet' type='text/css' media='screen' />

    <script src='<%:RootPath %>scripts/jquery.rating.pack.js' defer type='text/javascript'></script>
    <script src='<%:RootPath %>scripts/jquery.fancybox-1.3.4.js' defer type='text/javascript'></script>
    <script src='<%:RootPath %>scripts/fb.js' async defer type='text/javascript'></script>
    <script src='<%:RootPath %>scripts/category_page2.js' async defer type='text/javascript'></script>
    <script src='<%:RootPath %>scripts/move_top.js' async defer type='text/javascript'></script>
    <section id='seccat' itemscope="" itemtype="http://schema.org/Product">
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
                LinkedIn</a></li>
            <li><a class="facebook"  href=""
                   onclick="javascript:postToFeed('<%: CategoryTitle %>','<%:DirectoryURL %>','<%: MetaDesc %>');return false;">
                Facebook</a></li>
            <li><a  class="lnkmail mail"
                   href="<%:RootPath %>share-page-email.aspx?p=../&title=<%: CategoryTitle %>&des=<%: MetaDesc %>&url=<%: ShareURL %>">
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
            <li><a href="<%: dr["CATEGORY_URL"].ToString() %>"><%= dr["DISPLAYNAME"].ToString() %></a></li>
        <% } %>
    </ul>
</section>

    <section id='secadpage' class="adlist_section boxnone">
        <div class="div_buttons"><a href="<%:RootPath %>directoryrfq.aspx?CategorySK=<%: CategorySK %>&amp;ClientSK="
                                  id="lnkRFQ" class="lnkrfq iframe">Request For Quote</a>
            <a href="<%:RootPath %><%: CategoryName %>/" id="lnkBack" >Go To <%: H1Text %> Home</a></div>
        <div class="clearfix"></div>
        <ul class="adlist_ul">
            <li><h2><%: H1Text %> Companies Serving <%: StateName %></h2></li>
    <% if (StateAdvertisements.Count > 0)
    {
        foreach (var stAd in StateAdvertisements)
        { %>
            <li itemscope itemtype="http://schema.org/Place">
                <header>
                    <h3 class='cname'>
                        <a rel='nofollow' title='<%= stAd["FORMATED_NAME"] %>' target='_blank' href='<%: stAd["COMPANY_URL"] %>' onmouseover="loadWebPreview('<%: stAd["COMPANY_URL"] %>', '<%: stAd["IMAGE"] %>');hitsLinkTrack('<%: stAd["HITSLINK"] %>')" itemprop="url"><span itemprop="name"><%= stAd["CLIENT_NAME"] %></span></a>
                        <span itemprop="address" class="addr" itemscope itemtype="http://schema.org/PostalAddress">
                             <span itemprop="addressLocality"><%: stAd["CITY_STATE"] %></span>
                        <span itemprop="telephone"><%: stAd["PHONE"] %></span></span>
                    </h3>
                   <div class="buttons">
                    <a class='btncopro' title='<%: stAd["FORMATED_NAME"] %> Profile' id='ID<%: stAd["ADVERTISEMENT_SK"] %>' href='<%:RootPath %><%: stAd["COPRA_PATH"] %>'>View Company Profile</a>
                     <% if (stAd["CAD_URL"].ToString()!="" && stAd["CAD_URL"].ToString()!="http://")
                        { %><a class='btnCAD2' href='<%: stAd["CAD_URL"] %>' target='_blank' >View CAD Drawings</a><% } %>
                       </div>
                </header>
                <p class='cdesc' itemprop="description"><%= stAd["ADDESCRIPTION"] %>
                </p>
                <div class='divRate'>
                    <div id='divRate<%: stAd["CLIENT_SK"] %>' class="divratingclientmain">
                        <script type='text/javascript'>
                            $(document).ready(function () { $('input[type=radio].star<%: stAd["CLIENT_SK"] %>').rating({required: true});});
                        </script>
                    <span class="spanreadreviews">
                        <a title='<%: stAd["FORMATED_NAME"] %> Profile' id='ID<%: stAd["CLIENT_SK"] %>' href='<%:RootPath %><%: stAd["COPRA_PATH"] %>'>Read Reviews</a>
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
            <% }
    }
    else
    { %>
            <li><p>No companies under the searched state. You can see below the companies serving under neighboring states if any.</p></li>
     <% } %>
            <% 
                string scode = "", precode = "";
                foreach (var nghAd in NeighAdvertisements)
                { 
                    scode = nghAd["theState"].ToString();
                    %>
            <% if (scode != precode)
                { %>
            <li><h2><%: H1Text %> Companies Serving <%: nghAd["STATECODE"].ToString()  %></h2></li>
            <%} %>
            <li itemscope itemtype="http://schema.org/Place">
                <header>
                    <h3 class='cname'>
                        <a rel='nofollow' title='<%= nghAd["FORMATED_NAME"] %>' target='_blank' href='<%: nghAd["COMPANY_URL"] %>' onmouseover="loadWebPreview('<%: nghAd["COMPANY_URL"] %>', this);hitsLinkTrack('<%: nghAd["HITSLINK"] %>');"itemprop="url"><span itemprop="name"><%= nghAd["CLIENT_NAME"] %></span></a>
                         <span itemprop="address" class="addr" itemscope itemtype="http://schema.org/PostalAddress">
                             <span itemprop="addressLocality"><%= nghAd["CITY_STATE"] %></span>
                        <span itemprop="telephone"><%= nghAd["PHONE"] %></span></span>
                    </h3>
                    <div class="buttons">
                    <a class='btncopro' title='<%: nghAd["FORMATED_NAME"] %> Profile' id='ID<%: nghAd["ADVERTISEMENT_SK"] %>' href='<%:RootPath %><%: nghAd["COPRA_PATH"] %>'>View Company Profile</a>
                 <% if (nghAd["CAD_URL"].ToString()!="" && nghAd["CAD_URL"].ToString()!="http://")
                        { %><a class='btnCAD2' href='<%: nghAd["CAD_URL"] %>' target='_blank' >View CAD Drawings</a><% } %>
                        </div>
                </header>
                <p class='cdesc' itemprop="description"><%: nghAd["ADDESCRIPTION"] %>
                </p>
                <div class='divRate'>
                    <div id='divRate<%: nghAd["CLIENT_SK"] %>' class="divratingclientmain">
                        <script type='text/javascript'>
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
    
    <section id='secad_canada' >
        <% if (CityList != "")
            { %>
        <h3>ADDITIONAL CITIES WE SERVE INCLUDE:</h3>
        <p><%=CityList %></p>
        <% } %>
        <% if (CountyList != "")
            { %>
        <h3>ADDITIONAL COUNTIES WE SERVE INCLUDE:</h3>
        <p><%=CountyList %></p>
        <% } %>
        <% if (OtherAdvertisements.Count > 0) { %>
    <h3>WE LIST FOR OTHER COUNTRIES TOO!</h3>
        <h2><%: H1Text %> manufacturers in CANADA</h2>
        <ul>
            <% foreach (var othAd in OtherAdvertisements)
                { %>
            <li><a href="<%: RootPath %><%: CategoryName %>/<%: othAd["SEARCH_URL"].ToString() %>"><%= othAd["NAME"].ToString() %>(<%: othAd["NUMBER_OF_CLIENTS"].ToString() %>)</a></li>
            <% } %>
        </ul><% } %>
    </section>
    
    <script type='text/javascript'>
        $(document).ready(function () {
            $.get($('#hdnSrhRootPath').val()+'StateSearch.html', function (data) {
                $('#secsbox').html(data);
                $('#txtsearch').val('<%: DisplayName %>');
                $('#txtsearch').attr('class', 'txtsearchsel');
                $('#combo').val('<%: StateName %>, <%: CountryCode %>');
            });

            <% foreach (var cr in ClientRatings)
                {%>
            $('input[type=radio].star' + <%: cr["CLIENT_SK"].ToString() %>).rating('enable');
            $('input[type=radio].star' + <%: cr["CLIENT_SK"].ToString() %>).rating('select', parseInt(<%: cr["RATING"].ToString() %>)-1, false);
            $('input[type=radio].star' + <%: cr["CLIENT_SK"].ToString() %>).rating('disable');
            if (parseInt(<%: cr["RATING"].ToString() %>) > -1) { $('#divRate' + <%: cr["CLIENT_SK"].ToString() %>).show(); }
            <% } %>

            $('.lnkrfq').fancybox({ 'height': 600, 'width': 800, 'onStart': function () { $('body').css('overflow', 'hidden'); }, 'onClosed': function () { $('body').css('overflow', 'auto'); }, 'hideOnOverlayClick': false });
            $('.lnkrfq').bind('contextmenu', function (e) { return false; });
            

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
