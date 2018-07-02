<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="CategoryPage2.aspx.cs" inherits="IQSDirectory.CategoryPage2" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%:RootPath %>content/category_styles.css" rel="stylesheet" media='screen'/>
    <link href='<%:RootPath %>content/stylerprint.css' rel='stylesheet' type='text/css' media='print' />    

    <asp:PlaceHolder runat="server">
        <%--: Styles.Render("~/bundles/SiteMasterStyles") --%>
        <%--: Scripts.Render("~/bundles/SiteMasterScripts") --%>
        <%: Scripts.RenderFormat("<script type=\"text/javascript\" src=\"{0}\" defer></script>", "~/bundles/ScriptStateSearch") %>
    </asp:PlaceHolder>
    <section id='seccat' itemscope="" itemtype="https://schema.org/Product">        
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
            <li itemscope itemtype="https://schema.org/Place">
                <header>
                    <h3 class='cname'>
                        <a rel='nofollow' title='<%: drTAd["FORMATED_NAME"] %>' target='_blank' href='<%: drTAd["COMPANY_URL"] %>' onmouseover="loadWebPreview('<%: drTAd["COMPANY_URL"] %>', '<%: drTAd["IMAGE"] %>');hitsLinkTrack('<%: drTAd["HITSLINK"] %>')" itemprop="url"><span itemprop="name"><%= drTAd["CLIENT_NAME"] %></span></a>
                        <span itemprop="address" class="addr" itemscope itemtype="https://schema.org/PostalAddress">
                             <span itemprop="addressLocality"><%: drTAd["CITY_STATE"] %></span>
                        <span itemprop="telephone"><%: drTAd["PHONE"] %></span></span>
                    </h3>
                    <div class="buttons">
                      <a class='btncopro' title='<%: drTAd["FORMATED_NAME"] %> Profile' id='ID<%: drTAd["ADVERTISEMENT_SK"] %>' href='<%=RootPath %><%: drTAd["PROFILE_URL"] %>'>View Company Profile</a>
               
                        <a class='btnCAD2' target='_blank' href='<%: drTAd["CAD_URL"] %>'>View CAD Drawings</a>
                        
                </header>
                <p class='cdesc' itemprop="description"><%: drTAd["ADDESCRIPTION"] %></p>
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
           
        </ul>
        <aside>
           
            <div id="preview1" class="forpreview" >
            <img src='<%:RootPath %>images/cardboard-placeholder.jpg' alt="Mouse Over Company Names to see their previews" title="Mouse Over Company Names to see their previews" /></div>
        </aside>
    </section>
    <% } else { %>
     <section class="adlist_empty">
         <h4>No Additional Companies listed under this Product Category!</h4>
     </section>
    <% }  %>
    <script type='text/javascript'>
        $(document).ready(function () {
            
            <% foreach (var cr in ClientRatings)
                {%>
            $('input[type=radio].star' + <%: cr["CLIENT_SK"].ToString() %>).rating('enable');
            $('input[type=radio].star' + <%: cr["CLIENT_SK"].ToString() %>).rating('select', parseInt(<%: cr["RATING"].ToString() %>)-1, false);
            $('input[type=radio].star' + <%: cr["CLIENT_SK"].ToString() %>).rating('disable');
            if (parseInt(<%: cr["RATING"].ToString() %>) > -1) { $('#divRate' + <%: cr["CLIENT_SK"].ToString() %>).show(); }
            <% } %>
         });

        
        

    </script>
    <input type='hidden' id='hdnApiPath' value='<%: ApiPath %>' />
    <input type='hidden' id='hdnCategorySK' value='<%: CategorySK %>' />
    <input type='hidden' id='hdnSrhRootPath' value="<%: RootPath %>" />
    <input type='hidden' id='hdnCategoryName' value="<%: DisplayName %>" />
    <input type='hidden' id='hdnStateName' value="" />
     <!-- HitsLink.com tracking script -->
    <script>
        var wa_hp = 'http' + (location.protocol == 'https:' ? 's' : '');
        document.getElementById('wa_u').src = wa_hp + '://counter.hitslink.com/track.js';
    </script>
    <script type="text/javascript" id="wa_u" defer></script> 
</asp:Content>
