<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="CategoryPage1.aspx.cs" inherits="IQSDirectory.CategoryPage1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href='Content/stylerprint.css' rel='stylesheet' type='text/css' media='print' />
    <link href='Content/jquery.fancybox-1.3.4.css' rel='Stylesheet' type='text/css' media='screen' />

    <script src='Scripts/jquery.rating.pack.js' type='text/javascript'></script>
    <script src='Scripts/jquery.fancybox-1.3.4.js' type='text/javascript'></script>
    <script src='Scripts/fb.js' type='text/javascript'></script>
    <script src='Scripts/category_page1.js' type='text/javascript'></script>
    <script src='Scripts/move_top.js' type='text/javascript'></script>


    <section id='seccat'>
        <div id="social">
            <span>Share this page on</span>
            <ul>
                <li><a href='http://blog.iqsdirectory.com/' target='_blank' class="iqs">IQS</a></li>
                <li><a class="google" rel="nofollow"
                    href="https://plus.google.com/share?url=http://www.iqsdirectory.com/metal-stampings" onclick="javascript:popupwindow(this.href,'',600,600);return false;">Google</a></li>
                <li><a class="twitter" rel="nofollow"
                    href="https://twitter.com/share?url=http://www.iqsdirectory.com/metal-stampings&text=%23Top 10 Metal Stampings Companies + Services [2017 Updated]. Easily locate 10 metal stampings companies. Complex, requirements, fast rfq, design, experience, iso certified, drawings, quick, quote, customized." onclick="javascript:popupwindow(this.href,'',600,400);return false;">Twitter</a></li>
                <li><a class="linkedin" rel="nofollow"
                    href="http://www.linkedin.com/shareArticle?mini=true&url=http://www.iqsdirectory.com/metal-stampings&title=Top 10 Metal Stampings Companies + Services [2017 Updated]&summary=Easily locate 10 metal stampings companies. Complex, requirements, fast rfq, design, experience, iso certified, drawings, quick, quote, customized.&source=http://www.iqsdirectory.com/" onclick="javascript:popupwindow(this.href,'',600,400);return false;">LinedIn</a></li>
                <li><a class="facebook" rel="nofollow" href=""
                    onclick="javascript:postToFeed('Top 10 Metal Stampings Companies + Services [2017 Updated]','http://www.iqsdirectory.com/','Easily locate 10 metal stampings companies. Complex, requirements, fast rfq, design, experience, iso certified, drawings, quick, quote, customized.');return false;">Facebook</a></li>
                <li><a rel="nofollow" class="iframe lnkmail mail"
                    href="../controls/MailSend.aspx?p=../&title=Top 10 Metal Stampings Companies + Services [2017 Updated]&des=Easily locate 10 metal stampings companies. Complex, requirements, fast rfq, design, experience, iso certified, drawings, quick, quote, customized.&url=http://www.iqsdirectory.com/metal-stampings">Mail</a></li>
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

    <section id='secrelcat'>
        <h2>Related Categories</h2>
        <ul id="ulRelatedCategories">
            <% foreach (var dr in RelatedCategories)
                {  %>
            <li><a href="<%: dr["CATEGORY_URL"].ToString() %>"><%: dr["DISPLAY_NAME"].ToString() %></a></li>
            <% } %>
        </ul>
    </section>

    <section id='sectier1' class="adlist_section">
        <ul class="adlist_ul">
            <% foreach (var drT1Ad in Tier1Advertisements)
                {%>
            <li>
                <header>
                    <h3 class='cname'>
                        <a rel='nofollow' alt='<%: drT1Ad["FORMATED_NAME"] %>' title='<%: drT1Ad["FORMATED_NAME"] %>' target='_blank' href='<%: drT1Ad["COMPANY_URL"] %>' onmouseover="loadWebPreview('<%: drT1Ad["COMPANY_URL"] %>','1', this)"><%: drT1Ad["CLIENT_NAME"] %></a>
                        <span><%: drT1Ad["CITY_STATE"] %></span>
                        <span><%: drT1Ad["PHONE"] %></span>
                    </h3>
                    <a href='directoryrfq.aspx?CategorySK=<%: CategorySK %>&amp;ClientSK=<%: drT1Ad["CLIENT_SK"] %>' class='btnrfq'>Request For Quote</a>
                    <a rel='nofollow' class='btncopro' alt='<%: drT1Ad["FORMATED_NAME"] %> Profile' title='<%: drT1Ad["FORMATED_NAME"] %> Profile' id='ID<%: drT1Ad["ADVERTISEMENT_SK"] %>' href='<%: drT1Ad["PROFILE_URL"] %>'>View Company Profile</a>
                </header>
                <p class='cdesc'><%: drT1Ad["ADDESCRIPTION"] %></p>
                <div class='divRate'>
                    <div id='divRate<%: drT1Ad["CLIENT_SK"] %>' class="divratingclientmain">
                        <script language='javascript' type='text/javascript'>
                            $(document).ready(function () { $('input[type=radio].star<%: drT1Ad["CLIENT_SK"] %>').rating({ required: true }); });
                        </script>
                        <span class="spanreadreviews">
                            <a alt='<%: drT1Ad["FORMATED_NAME"] %> Profile' title='<%: drT1Ad["FORMATED_NAME"] %> Profile' id='ID<%: drT1Ad["CLIENT_SK"] %>' href='<%: drT1Ad["PROFILE_URL"] %>'>Read Reviews</a>
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
            <iframe id='preview_iframe1' class='foriframe' src='images/cardboard-placeholder.jpg' scrolling='no'></iframe>
            <div class='foriframe' id='iframe_mask1' style='position: absolute; cursor: pointer;'></div>
        </aside>
    </section>



    <section id='sectier2' class="adlist_section">
        <ul class="adlist_ul">
            <% foreach (var drT2Ad in Tier2Advertisements)
                {%>
            <li>
                <header>
                    <h3 class='cname'>
                        <a rel='nofollow' alt='<%: drT2Ad["FORMATED_NAME"] %>' title='<%: drT2Ad["FORMATED_NAME"] %>' target='_blank' href='<%: drT2Ad["COMPANY_URL"] %>' onmouseover="loadWebPreview('<%: drT2Ad["COMPANY_URL"] %>','2', this)"><%: drT2Ad["CLIENT_NAME"] %></a>
                        <span><%: drT2Ad["CITY_STATE"] %></span><span><%: drT2Ad["PHONE"] %></span>
                    </h3>
                    <a href='directoryrfq.aspx?CategorySK=<%: CategorySK %>&amp;ClientSK=<%: drT2Ad["CLIENT_SK"] %>' class='btnrfq'>Request For Quote</a>
                    <a rel='nofollow' class='btncopro' alt='<%: drT2Ad["FORMATED_NAME"] %> Profile' title='<%: drT2Ad["FORMATED_NAME"] %> Profile' id='ID<%: drT2Ad["ADVERTISEMENT_SK"] %>' href='<%: drT2Ad["PROFILE_URL"] %>'>View Company Profile</a>
                </header>
                <p class='cdesc'><%: drT2Ad["ADDESCRIPTION"] %></p>
                <div class='divRate'>
                    <div id='divRate<%: drT2Ad["CLIENT_SK"] %>' class="divratingclientmain">
                        <script language='javascript' type='text/javascript'>
                            $(document).ready(function () { $('input[type=radio].star<%: drT2Ad["CLIENT_SK"] %>').rating({ required: true }); });
                        </script>
                        <span class="spanreadreviews">
                            <a alt='<%: drT2Ad["FORMATED_NAME"] %> Profile' title='<%: drT2Ad["FORMATED_NAME"] %> Profile' id='ID<%: drT2Ad["CLIENT_SK"] %>' href='<%: drT2Ad["PROFILE_URL"] %>'>Read Reviews</a>
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
            <iframe id='preview_iframe2' class='foriframe' src='images/cardboard-placeholder.jpg' scrolling='no'></iframe>
            <div class='foriframe' id='iframe_mask2' style='position: absolute; cursor: pointer;'></div>
        </aside>
    </section>

    <script type='text/javascript'>
        $(document).ready(function ()
        {
            <% foreach (var cr in ClientRatings)
                {%>
            $('input[type=radio].star' + <%: cr["CLIENT_SK"].ToString() %>).rating('enable');
            $('input[type=radio].star' + <%: cr["CLIENT_SK"].ToString() %>).rating('select', parseInt(<%: cr["RATING"].ToString() %>), false);
            $('input[type=radio].star' + <%: cr["CLIENT_SK"].ToString() %>).rating('disable');
            if (parseInt(<%: cr["RATING"].ToString() %>) > -1) { $('#divRate' + <%: cr["CLIENT_SK"].ToString() %>).show(); }
            <% } %>
        });
    </script>

    <section id='secaddcomp'>
        <header id='secsepaddcomp'>
            <a class='addcomp' href='http://www.iqsdirectory.com/metal-stampings/metal-stampings-2.htm'><span>More <%: DisplayName %> Companies</span>
                <img src='images/barrow.png' alt='More <%: DisplayName %> Companies' title='More <%: DisplayName %> Companies'></a>
        </header>
    </section>

    <section id='secartmain'>
        <!-- Dynamic Articles and Press Releases -->
        <section id="secarticle">
            <h2 id="secarthead">ARTICLES AND PRESS RELEASES</h2>


            <ul>
                <li>

                    <h3><a href="http://blog.iqsdirectory.com/process-equipment/plastec-ventilation/"
                        target="_blank">Proven Ventilation Systems from Plastec Ventilation</a></h3>
                    <aside>
                        Process Equipment
                    </aside>
                    <p>
                        Blowers Plastec Ventilation, Inc. is the North American subsidiary of SEAT Ventilation, locate
                        d in Verniolle, France. We offer durable, cost effective, and low noise industrial blowers.
                        PLASTEC exhaust systems are currently distributed in 70 countries and across 5 continents. Our extensive experience in the field of fume extraction systems means we can help find the best solution for your needs. Read more......
                    </p>

                </li>
                <li>

                    <h3><a href="http://blog.iqsdirectory.com/process-equipment/blowers-6/" target="_blank">The Ins a
                        nd Outs of Regenerative Blowers</a></h3>
                    <aside>
                        Process Equipment
                    </aside>
                    <p>
                        Vacuum blowers work just like a vacuum, but rather than suck a material up, the blowers blow m
                        aterials out. Many industrial vacuums can become both blowers or suckers, depending on how
                        the system is set up. One type of vacuum blower that many factories use is the regenerative blower. A regenerative blower is the best way to move large volumes of air at low pressure. A regenerative blower works differently from a displacement compressor or traditional vacuum pump. A regenerative blower moves air molecules through a non-positive displacement system, which...
                    </p>

                </li>


            </ul>

        </section>
    </section>

    <section id='secininfo'>
        <header>Industry Information</header>
        <article>
            <%: IndustryInformation %>
        </article>
    </section>

    <script type='text/javascript'>
        $(document).ready(function () {
            $.get('StateSearch.html', function (data) {
                $('#secsbox').html(data);
                $('#txtsearch').val('Metal Stampings');
                $('#txtsearch').attr('class', 'txtsearchsel');
            });
        });
    </script>
    <input type='hidden' id='hdnApiPath' value='<%: ApiPath %>' />
    <input type='hidden' id='hdnCategorySK' value='<%: CategorySK %>' />
</asp:Content>
