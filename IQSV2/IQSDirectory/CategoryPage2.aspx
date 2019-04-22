<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="CategoryPage2.aspx.cs" inherits="IQSDirectory.CategoryPage2" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:PlaceHolder runat="server">
        <%--: Styles.Render("~/bundles/SiteMasterStyles") --%>
        <%--: Scripts.Render("~/bundles/SiteMasterScripts") --%>
        <%--: Scripts.RenderFormat("<script type=\"text/javascript\" src=\"{0}\" defer></script>", "~/bundles/StateSearchScripts") --%>
        <script src='<%:RootPath %>scripts/category_page2.js' defer type='text/javascript'></script>
    </asp:PlaceHolder>
    <div id="section-color">

  <div class="row container" style="margin-bottom:0px;"  itemscope="" itemtype="https://schema.org/Thing">
    <h1 class="white-text" style="padding-top:5px;margin-bottom:0px;" itemprop='name'><%: H1Text %></h1>
    <p style="font-size:12px;" itemprop='description'><strong><%: ItemDesc %></strong></p>
  </div>
	</div>
    <div id="section-related">
  <div class="related-cat-wrapper">
    <div class="col s12"><strong><a href="<%:RootPath %><%: CategoryName %>/" class="breadcrumb" style="font-size:14px;">Go To <%: DisplayName %> Manufacturers and Companies Home</a></strong>
       
       </div>
  </div>
  </div>   
       <div class="section" style="background-color:#f1f1f1;padding-top:0px;">
 <div class="container"> 
	   
	    <div class="row" >
    <% if (TierAdvertisements.Count > 0) { %> <a class="hoverable waves-effect waves-light orange btn iframe btnrfq" id="lnkRFQ" href="<%:RootPath %>directoryrfq.aspx?CategorySK=<%: CategorySK %>&amp;ClientSK=" style="position:relative;float:left;margin-top:20px;margin-bottom:-20px;">Request for Quote</a>

    <section id='secpage2' class="adlist_section" style="padding-top:50px;" >
        
        <ul class="adlist_ul">
            <% foreach (var drTAd in TierAdvertisements)
                {%>
            <li itemscope itemtype="https://schema.org/Place">
                <header>
                    <h3 class='cname'>
                        <a rel='nofollow' title='<%: drTAd["FORMATED_NAME"] %>' target='_blank' href='<%: drTAd["COMPANY_URL"] %>' onmouseover="loadWebPreview('<%: drTAd["COMPANY_URL"] %>', '<%: drTAd["IMAGE"] %>');hitsLinkTrack('<%: drTAd["HITSLINK"] %>')" itemprop="url"><span itemprop="name"><%= drTAd["CLIENT_NAME"] %></span></a>
                        <span itemprop="address" class="addr" itemscope itemtype="https://schema.org/PostalAddress">
                            <span itemprop="addressLocality"><%: drTAd["CITY_STATE"] %></span>
                            <!--<span itemprop="telephone"><%: drTAd["PHONE"] %></span>-->
                        </span>
                    </h3>
                    <div class="buttons">
                    <!--<a href='<%:RootPath %>directoryrfq.aspx?CategorySK=<%: CategorySK %>&amp;ClientSK=<%: drTAd["CLIENT_SK"] %>' class='iframe btnrfq hoverable btn-small waves-effect waves-light orange'>Request For Quote</a>
                    <a class='btncopro hoverable btn-small waves-effect waves-light orange' title='<%: drTAd["FORMATED_NAME"] %> Profile' id='ID<%: drTAd["ADVERTISEMENT_SK"] %>' href='<%:RootPath %><%: drTAd["PROFILE_URL"] %>'>View Company Profile</a>-->
                   <a rel='nofollow' class='btnCAD2 hoverable btn-small waves-effect waves-light orange' target="_blank" href='<%: drTAd["CAD_URL"] %>'>View CAD Drawings</a>
                        </div>
                </header>
                <p class='cdesc' itemprop="description"><%: drTAd["ADDESCRIPTION"] %></p>
                <div class='divRate'>
                    <div id='divRate<%: drTAd["CLIENT_SK"] %>' class="divratingclientmain">
                        <script type='text/javascript'>
                            $(document).ready(function () { $('input[type=radio].star<%: drTAd["CLIENT_SK"] %>').rating({ required: true }); });
                        </script>
                        <span class="spanreadreviews">
                            <a title='<%: drTAd["FORMATED_NAME"] %> Profile' id='ID<%: drTAd["CLIENT_SK"] %>' href='<%:RootPath %><%: drTAd["PROFILE_URL"] %>'>Read Reviews</a>
                        </span>
                        <span class='divratingclient'>
                        <input name='star<%: drTAd["CLIENT_SK"] %>' type='radio' class='star<%: drTAd["CLIENT_SK"] %>' value='1' title='1'/>
                        <input name='star<%: drTAd["CLIENT_SK"] %>' type='radio' class='star<%: drTAd["CLIENT_SK"] %>' value='2' title='2'/>
                        <input name='star<%: drTAd["CLIENT_SK"] %>' type='radio' class='star<%: drTAd["CLIENT_SK"] %>' value='3' title='3'/>
                        <input name='star<%: drTAd["CLIENT_SK"] %>' type='radio' class='star<%: drTAd["CLIENT_SK"] %>' value='4' title='4'/>
                        <input name='star<%: drTAd["CLIENT_SK"] %>' type='radio' class='star<%: drTAd["CLIENT_SK"] %>' value='5' title='5'/>
                        </span>
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
            </div></div></div>
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
