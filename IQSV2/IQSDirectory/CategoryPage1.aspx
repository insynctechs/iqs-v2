<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="CategoryPage1.aspx.cs" inherits="IQSDirectory.CategoryPage1" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link href="<%:RootPath %>content/category_styles.css" rel="stylesheet" media='screen'/>
    <link href='<%:RootPath %>content/stylerprint.css' rel='stylesheet' type='text/css' media='print' />
    <asp:PlaceHolder runat="server">
        <%--: Styles.Render("~/bundles/SiteMasterStyles") --%>
        <%--: Scripts.Render("~/bundles/SiteMasterScripts") --%>
        <%: Scripts.RenderFormat("<script type=\"text/javascript\" src=\"{0}\" defer></script>", "~/bundles/ScriptCategoryPage1") %>
    </asp:PlaceHolder>


    <section id='seccat' itemscope="" itemtype="https://schema.org/Product">
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
            <li itemscope itemtype="https://schema.org/Place">
                <header>
                    <h3 class='cname'>
                        <a rel='nofollow' title='<%: drT1Ad["FORMATED_NAME"] %>' target='_blank' href='<%: drT1Ad["COMPANY_URL"] %>' onmouseover="loadWebPreview('<%: drT1Ad["COMPANY_URL"] %>','1', '<%: drT1Ad["IMAGE"] %>');hitsLinkTrack('<%: drT1Ad["HITSLINK"] %>')" itemprop="url"><span itemprop="name"><%= drT1Ad["CLIENT_NAME"] %></span></a>
                        <span itemprop="address" class="addr" itemscope itemtype="https://schema.org/PostalAddress">
                            <span itemprop="addressLocality"><%: drT1Ad["CITY_STATE"] %></span>
                            <span itemprop="telephone"><%: drT1Ad["PHONE"] %></span>
                        </span>
                    </h3>
                    <div class="buttons">
                    <a href='<%:RootPath %>directoryrfq.aspx?CategorySK=<%: CategorySK %>&amp;ClientSK=<%: drT1Ad["CLIENT_SK"] %>' class='iframe btnrfq'>Request For Quote</a>
                    <a class='btncopro' title='<%: drT1Ad["FORMATED_NAME"] %> Profile' id='ID<%: drT1Ad["ADVERTISEMENT_SK"] %>' href='<%:RootPath %><%: drT1Ad["PROFILE_URL"] %>'>View Company Profile</a>
                    <% /*if (drT1Ad["CAD_URL"].ToString()!="" && drT1Ad["CAD_URL"].ToString()!="http://")
                        { */%><a class='btnCAD' target="_blank" href='<%: drT1Ad["CAD_URL"] %>'>View CAD Drawings</a><% //} %>
                        </div>
                </header>
                <p class='cdesc' itemprop="description"><%: drT1Ad["ADDESCRIPTION"] %></p>
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
                    </ul>
        <aside>
            <div id="preview1" class="forpreview"> 
            <img src='<%:RootPath %>images/cardboard-placeholder.jpg' alt="Mouse Over Company Names to see their previews" title="Mouse Over Company Names to see their previews" /></div>
        </aside>
    </section>
    <% } %>

    <% if (Tier2Advertisements.Count > 0) { %>
    <section id='sectier2' class="adlist_section">
        <ul class="adlist_ul">
            <% foreach (var drT2Ad in Tier2Advertisements)
              {%>
            <li itemscope itemtype="https://schema.org/Place">
                <header>
                    <h3 class='cname'>
                        <a rel='nofollow' title='<%: drT2Ad["FORMATED_NAME"] %>' target='_blank' href='<%: drT2Ad["COMPANY_URL"] %>' onmouseover="loadWebPreview('<%: drT2Ad["COMPANY_URL"] %>','2', '<%: drT2Ad["IMAGE"] %>');hitsLinkTrack('<%: drT2Ad["HITSLINK"] %>');" itemprop="url"><span itemprop="name"><%= drT2Ad["CLIENT_NAME"] %></span></a>
                         <span itemprop="address" class="addr" itemscope itemtype="https://schema.org/PostalAddress">
                             <span itemprop="addressLocality"><%: drT2Ad["CITY_STATE"] %></span><span itemprop="telephone"><%: drT2Ad["PHONE"] %></span>
                         </span>
                    </h3>
                    <div class="buttons">
                    <a href='<%:RootPath %>directoryrfq.aspx?CategorySK=<%: CategorySK %>&amp;ClientSK=<%: drT2Ad["CLIENT_SK"] %>' class='iframe btnrfq'>Request For Quote</a>
                    <a class='btncopro' title='<%: drT2Ad["FORMATED_NAME"] %> Profile' id='ID<%: drT2Ad["ADVERTISEMENT_SK"] %>' href='<%:RootPath %><%: drT2Ad["PROFILE_URL"] %>'>View Company Profile</a>
                 <% if (drT2Ad["CAD_URL"].ToString()!="" && drT2Ad["CAD_URL"].ToString()!="http://")
                        { %><a class='btnCAD' target='_blank' href='<%: drT2Ad["CAD_URL"] %>'>View CAD Drawings</a><% } %>
                        </div>
                </header>
                <p class='cdesc' itemprop="description"><%: drT2Ad["ADDESCRIPTION"] %></p>
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
                    </ul>
        <aside>
            
             <div id="preview2" class="forpreview" >             <img src='<%:RootPath %>images/cardboard-placeholder.jpg' alt="Mouse Over Company Names to see their previews" title="Mouse Over Company Names to see their previews" /></div>
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
    <% if (Tier1Advertisements.Count > 0)
        { %>
    <section id='secaddcomp'>
        <header id='secsepaddcomp'>
            <a class='addcomp' href='<%:RootPath %><%: CategoryName %>/<%: CategoryName %>-2/'><span>More <%: DisplayName %> Companies</span>
                <img src='<%:RootPath %>images/barrow.png' alt='Click' title='More <%: DisplayName %> Companies'></a>
        </header>
    </section>
    <% } %>
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
   

    <input type='hidden' id='hdnApiPath' value='<%: ApiPath %>' />
    <input type='hidden' id='hdnCategorySK' value='<%: CategorySK %>' />
    <input type='hidden' id='hdnSrhRootPath' value="<%: RootPath %>" />
    <input type='hidden' id='hdnCategoryName' value="<%: DisplayName %>" />
    <!-- HitsLink.com tracking script -->
    <script>
        var wa_hp = 'http' + (location.protocol == 'https:' ? 's' : '');
        document.getElementById('wa_u').src = wa_hp + '://counter.hitslink.com/track.js';
    </script>
    <script type="text/javascript" id="wa_u" defer></script> 
</asp:Content>
 

