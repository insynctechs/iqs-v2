<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StateSearch.aspx.cs" Inherits="IQSDirectory.StateSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%:RootPath %>Content/category_styles.css" rel="stylesheet" media='screen'/>
    <link href='<%:RootPath %>Content/stylerprint.css' rel='stylesheet' type='text/css' media='print' />
    <link href='<%:RootPath %>Content/jquery.fancybox-1.3.4.css' rel='Stylesheet' type='text/css' media='screen' />
    <%: Scripts.Render("~/bundles/ScriptStateSearch") %>
    <section id='seccat'>
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
            <li><a rel=nofollow class="iframe lnkmail mail"
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
    <h1 itemprop='name'>Metal Stampings Manufacturers and Companies</h1>
    <div class="desc" itemprop='description'>IQS Directory provides a comprehensive list of metal stamping manufacturers and
        suppliers. Use our website to review and source top metal stamping manufacturers with roll over ads and detailed product descriptions. Find metal stamping companies that can design, engineer, and manufacture metal stampings to your companies specifications. Then contact the metal stamping companies through our quick and easy request for quote form. Website links, company profile, locations, phone, product videos and product information is provided for each company. Access customer reviews and keep up to date with product new articles. Whether you are looking for metal stampers, short run stampings, aluminum stampings, or customized metal stampings of every type, this is the resource for you.</div>
</section>

<section id='secrelcat'>
    <h2>Related Categories</h2>
    <ul id="ulRelatedCategories">
        <li><a href="http://www.iqsdirectory.com/axial-fans/">Axial Fans</a></li>
        <li><a href="http://www.iqsdirectory.com/high-velocity-fans/">High Velocity Fans</a></li>
        <li><a href="http://www.iqsdirectory.com/air-pollution-control/">Air Pollution Control</a></li>
        <li><a href="http://www.iqsdirectory.com/industrial-blowers/">Industrial Blowers</a></li>
        <li><a href="http://www.iqsdirectory.com/dust-collector/">Dust Collector</a></li>
        <li><a href="http://www.iqsdirectory.com/fan-manufacturers/">Fan Manufacturers</a></li>
        <li><a href="http://www.iqsdirectory.com/air-blowers/">Air Blowers</a></li>
        <li><a href="http://www.iqsdirectory.com/high-pressure-blowers/">High Pressure Blowers</a></li>
        <li><a href="http://www.iqsdirectory.com/air-compressors/">Air Compressors</a></li>
        <li><a href="http://www.iqsdirectory.com/vacuum-cleaners/">Vacuum Cleaners</a></li>
    </ul>
</section>
    <input type="hidden" runat="server" id="hdnSearchCondition" />
    <input type="hidden" runat="server" id="hdnCurrentFacet" />
    <input type="hidden" runat="server" id="hdnFacetSk" />
    <input type="hidden" runat="server" id="hdnUrl" />
    <input type="hidden" runat="server" id="hdnCountry" />
    <input type="hidden" runat="server" id="hdnCategorySK" />
    <input type="hidden" runat="server" id="hdnCategoryName" />

    <section id='secadpage' class="adlist_section boxnone">
        <div class="div_buttons"><a href="http://www.iqsdirectory.com/directoryrfq.aspx?CategorySK=119&amp;ClientSK="
                                  id="l
        nkRFQ" class="iframe lnkrfq" style="float:left;">Request For Quote</a><a
                href="http://www.iqsdirectory.com/metal-stampings/" id="lnkBack" class="iframe"
                style="float:left; ">Go To Metal Stampings Home</a></div>

        <ul class="adlist_ul">
            <h2>Blowers Companies Serving Wisconsin</h2>
            <li>
                <header>
                    <h3 class='cname'>
                        <a rel='nofollow' alt='Micro Forms' title='Micro Forms' target='_blank' href='http://www.mforms.com' onmouseover="loadWebPreview('http://www.mforms.com', this)">Micro Forms, Inc.</a>
                        <span>Garland, TX</span>
                        <span>972-494-1313</span>
                    </h3>
                    <a href='http://www.iqsdirectory.com/directoryrfq.aspx?CategorySK=119&amp;ClientSK=62009' class='btnrfq'
                            >Request For Quote</a>
                    <a rel='nofollow' class='btncopro'   alt='Micro Forms Profile' title='Micro Forms Profile' id='ID250363' href='http://www.iqsdirectory.com/profile/micro-forms-62009/'>View Company Profile</a>
                </header>
                <p class='cdesc'>We take pride in all our metal stampings. We strive to
                    provide the best quality and most cost-effective solutions for all our customers. We are not simply satisfied with doing the same things over and over, but constantly look for new ways to improve our manufacturing methods. Find out how we can help you by visiting our website for more info!
                </p>
                <div class='divRate'>
                    <div id='divRate62009' class="divratingclientmain">
                        <script language='javascript' type='text/javascript'>
                            $(document).ready(function () {$('input[type=radio].star62009').rating({required: true});});
                        </script>
                    <span class="spanreadreviews">
                        <a alt='Micro Forms Profile' title='Micro Forms Profile' id='ID250363' href='http://www.iqsdirectory.com/profile/micro-forms-62009/'>Read Reviews</a>
                    </span>
                    <span class='divratingclient'>
                        <input name='star62009' type='radio' class='star62009' value='1' title='1'/><input name='star62009' type='radio' class='star62009' value='2' title='2'/><input name='star62009' type='radio' class='star62009' value='3' title='3'/><input name='star62009' type='radio' class='star62009' value='4' title='4'/><input name='star62009' type='radio' class='star62009' value='5' title='5'/>
                    </span>
                    </div>
                </div>
            </li>
            <h2>Blowers Companies Serving michigan</h2>
            <li>
                <header>
                    <h3 class='cname'>
                        <a rel='nofollow' alt='Micro Forms' title='Micro Forms' target='_blank' href='http://www.mforms.com' onmouseover="loadWebPreview('http://www.mforms.com', this)">Micro Forms, Inc.</a>
                        <span>Garland, TX</span>
                        <span>972-494-1313</span>
                    </h3>
                    <a href='http://www.iqsdirectory.com/directoryrfq.aspx?CategorySK=119&amp;ClientSK=62009' class='btnrfq'
                            >Request For Quote</a>
                    <a rel='nofollow' class='btncopro'   alt='Micro Forms Profile' title='Micro Forms Profile' id='ID250363' href='http://www.iqsdirectory.com/profile/micro-forms-62009/'>View Company Profile</a>
                </header>
                <p class='cdesc'>We take pride in all our metal stampings. We strive to
                    provide the best quality and most cost-effective solutions for all our customers. We are not simply satisfied with doing the same things over and over, but constantly look for new ways to improve our manufacturing methods. Find out how we can help you by visiting our website for more info!
                </p>
                <div class='divRate'>
                    <div id='divRate62009' class="divratingclientmain">
                        <script language='javascript' type='text/javascript'>
                            $(document).ready(function () {$('input[type=radio].star62009').rating({required: true});});
                        </script>
                    <span class="spanreadreviews">
                        <a alt='Micro Forms Profile' title='Micro Forms Profile' id='ID250363' href='http://www.iqsdirectory.com/profile/micro-forms-62009/'>Read Reviews</a>
                    </span>
                    <span class='divratingclient'>
                        <input name='star62009' type='radio' class='star62009' value='1' title='1'/><input name='star62009' type='radio' class='star62009' value='2' title='2'/><input name='star62009' type='radio' class='star62009' value='3' title='3'/><input name='star62009' type='radio' class='star62009' value='4' title='4'/><input name='star62009' type='radio' class='star62009' value='5' title='5'/>
                    </span>
                    </div>
                </div>
            </li>
            <%--<script language='javascript' type='text/javascript'>$(document).ready(function () {LoadCompanyTotalRatingByArray('62009,56037,73870,69339,71667,77096');});</script>--%>

        </ul>
        <aside>
            <iframe id='preview_iframe' class='foriframe' src='./images/cardboard-placeholder.jpg' scrolling='no'>
            </iframe>
            <div class='foriframe' id='iframe_mask' style='position:absolute; cursor:pointer;'></div>
        </aside>
    </section>

    <section id='secad_canada' >
    <h2>WE LIST FOR OTHER COUNTRIES TOO!</h2>
        <h3>Checkout the blower manfuctures in CANADA STATES</h3>
        <ul>
            <li><a href="">Ontario(3)</a></li>
            <li><a href="">Qubec(4)</a></li>
            <li><a href="">British Columbia(3)</a></li>
        </ul>
    </section>

    <script type='text/javascript'>
        $(document).ready(function () {
            $.get('../StateSearch.html', function (data) {
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

</asp:Content>
