<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="CategoryPage1.aspx.cs" inherits="IQSDirectory.CategoryPage1" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <!--<link href="<%:RootPath %>content/category_styles.css" rel="stylesheet" media='screen'/>
    <link href='<%:RootPath %>content/stylerprint.css' rel='stylesheet' type='text/css' media='print' />-->
    <asp:PlaceHolder runat="server">
        <%--: Styles.Render("~/bundles/SiteMasterStyles") --%>
        <%--: Scripts.Render("~/bundles/SiteMasterScripts") --%>
        <%--: Scripts.RenderFormat("<script type=\"text/javascript\" src=\"{0}\" defer></script>", "~/bundles/CategoryScripts")--%>
    </asp:PlaceHolder>
<script src='<%:RootPath %>scripts/category_page1.js' defer type='text/javascript'></script>
    

    <section id='section-color' itemscope="" itemtype="https://schema.org/Product">
         <div class="row container" >
        <h1 class="white-text" style="padding-top:5px;margin-bottom:0px;" itemprop='name'><%: H1Text %></h1>
        <div class="desc" itemprop='description'><p style="font-size:12px;"><strong><%: ItemDesc %></strong></p>
</div>
             </div>
    </section>

    <section id='section-related'>
         <div class="related-cat-wrapper">
    <div class="col s12"><strong>Related Categories</strong>
            <% foreach (var dr in RelatedCategories)
                {  %>
            <a href="<%: dr["CATEGORY_URL"].ToString() %>" class="breadcrumb"><%: dr["DISPLAYNAME"].ToString() %></a>
            <% } %>
        </div></div>
    </section>

    <div class="section" style="background-color:#f1f1f1;padding-top:0px;">
 <div class="container"> 
	   
	    <div class="row" style="width:98%; margin:0px auto;">
     <% if (Tier1Advertisements.Count > 0) { %>
    <section id='sectier1' class="adlist_section">
        <ul class="adlist_ul" style="padding-top: 67.5px;">
            <% foreach (var drT1Ad in Tier1Advertisements)
                {%>
            <li itemscope itemtype="https://schema.org/Place">
                <header>
                    <h3 class='cname'>
                        <a rel='nofollow' title='<%: drT1Ad["FORMATED_NAME"] %>' target='_blank' href='<%: drT1Ad["COMPANY_URL"] %>' onmouseover="loadWebPreview('<%: drT1Ad["COMPANY_URL"] %>','1', '<%: drT1Ad["IMAGE"] %>');hitsLinkTrack('<%: drT1Ad["HITSLINK"] %>')" itemprop="url"><span itemprop="name"><%= drT1Ad["CLIENT_NAME"] %></span></a>
                        <span itemprop="address" class="addr" itemscope itemtype="https://schema.org/PostalAddress">
                            <span itemprop="addressLocality"><%: drT1Ad["CITY_STATE"] %></span>
                            <!--<span itemprop="telephone"><%: drT1Ad["PHONE"] %></span>-->
                        </span>
                    </h3>
                    <div class="buttons">
                        <a href='<%:RootPath %>directoryrfq.aspx?CategorySK=<%: CategorySK %>&amp;ClientSK=<%: drT1Ad["CLIENT_SK"] %>' class='iframe btnrfq hoverable btn-small waves-effect waves-light orange'>Request For Quote</a>
                   
                         <a class='btncopro hoverable btn-small waves-effect waves-light orange' title='<%: drT1Ad["FORMATED_NAME"] %> Profile' id='ID<%: drT1Ad["ADVERTISEMENT_SK"] %>' href='<%:RootPath %><%: drT1Ad["PROFILE_URL"] %>'>View Company Profile</a>
                 
                      <a class='btnCAD hoverable btn-small waves-effect waves-light orange' target="_blank" href='<%: drT1Ad["CAD_URL"] %>'>View CAD Drawings</a>
                        </div>
                </header>
                <p class='cdesc' itemprop="description"><%: drT1Ad["ADDESCRIPTION"] %></p>
                <div class='divRate'>
                    <div id='divRate<%: drT1Ad["CLIENT_SK"] %>' class="divratingclientmain">
                        <script type='text/javascript'>
                            $(document).ready(function () { $('input[type=radio].star<%: drT1Ad["CLIENT_SK"] %>').rating({ required: true }); });
                        </script>
                        <span class="spanreadreviews">
                            <a title='<%: drT1Ad["FORMATED_NAME"] %> Profile' id='ID<%: drT1Ad["CLIENT_SK"] %>' href='<%:RootPath %><%: drT1Ad["PROFILE_URL"] %>'>Read Reviews</a>
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
              <% foreach (var drT2Ad in Tier2Advertisements)
              {%>
            <li itemscope itemtype="https://schema.org/Place">
                <header>
                    <h3 class='cname'>
                        <a rel='nofollow' title='<%: drT2Ad["FORMATED_NAME"] %>' target='_blank' href='<%: drT2Ad["COMPANY_URL"] %>' onmouseover="loadWebPreview('<%: drT2Ad["COMPANY_URL"] %>','2', '<%: drT2Ad["IMAGE"] %>');hitsLinkTrack('<%: drT2Ad["HITSLINK"] %>');" itemprop="url"><span itemprop="name"><%= drT2Ad["CLIENT_NAME"] %></span></a>
                         <span itemprop="address" class="addr" itemscope itemtype="https://schema.org/PostalAddress">
                             <span itemprop="addressLocality"><%: drT2Ad["CITY_STATE"] %></span>
                             <!--<span itemprop="telephone"><%: drT2Ad["PHONE"] %></span>-->
                         </span>
                    </h3>
                    <div class="buttons">
                          <a href='<%:RootPath %>directoryrfq.aspx?CategorySK=<%: CategorySK %>&amp;ClientSK=<%: drT2Ad["CLIENT_SK"] %>' class='iframe btnrfq hoverable btn-small waves-effect waves-light orange'>Request For Quote</a>
                   
                         <a class='btncopro hoverable btn-small waves-effect waves-light orange' title='<%: drT2Ad["FORMATED_NAME"] %> Profile' id='ID<%: drT2Ad["ADVERTISEMENT_SK"] %>' href='<%:RootPath %><%: drT2Ad["PROFILE_URL"] %>'>View Company Profile</a>
                 
                  
                       <a class='btnCAD hoverable btn-small waves-effect waves-light orange' target='_blank' href='<%: drT2Ad["CAD_URL"] %>'>View CAD Drawings</a>
                        </div>
                </header>
                <p class='cdesc' itemprop="description"><%: drT2Ad["ADDESCRIPTION"] %></p>
                <div class='divRate'>
                    <div id='divRate<%: drT2Ad["CLIENT_SK"] %>' class="divratingclientmain">
                       <script type='text/javascript'>
                            $(document).ready(function () { $('input[type=radio].star<%: drT2Ad["CLIENT_SK"] %>').rating({ required: true }); });
                        </script>
                        <span class="spanreadreviews">
                            <a title='<%: drT2Ad["FORMATED_NAME"] %> Profile' id='ID<%: drT2Ad["CLIENT_SK"] %>' href='<%:RootPath %><%: drT2Ad["PROFILE_URL"] %>'>Read Reviews</a>
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
            <% if (Tier1Advertisements.Count > 0) { %>
         <a href="<%:RootPath %><%: CategoryName %>/<%: CategoryName %>-2/" target="_blank" id="download-button" class="hoverable btn-large waves-effect waves-light orange">More <%: DisplayName %> Companies</a>
        <% } %>
                    </ul>
        
        <aside>
            <div id="preview1" class="forpreview"> 
            <img src='<%:RootPath %>images/cardboard-placeholder.jpg' alt="Mouse Over Company Names to see their previews" title="Mouse Over Company Names to see their previews" /></div>
        </aside>
    </section>
    <% } %>
    </div>
     </div>
    
    
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
         <div id="blue-section" style="padding-bottom:5px;">
	   <div class="container center"> 
	
	   <h1 class="white-text"><i class="far fa-building"></i> FEATURED COMPANY</h1>
	  
	   </div>
	    
		  </div>
    <section id='prodinfo' class="section featured-co" style="background-color:#f1f1f1;">
        <%: ProductInformation %>
        </section>
    <% }
    else
    { %>
    
    <section id='secartmain'>
        <!-- Dynamic Articles and Press Releases -->
        <% if (Articles.Count > 0)
    { %>
        <div id="blue-section">
    <div class="blue-wrapper">
            <h4>ARTICLES AND PRESS RELEASES</h4>
         
            
                <%  int cntart = 0;
                    foreach (var art in Articles)
                    { %>
                        <%-- if (cntart % 3 == 0)
                            { --%> <!--<div class="col-group3">--> <%-- } --%>
                <div>
   <div class="card blue-grey darken-1">
        <div class="card-content white-text">
          <span class="card-title"><a href="<%: art["URL"].ToString() %>"><%= art["HEADING"].ToString() %></a></span><br>
<span class="new badge" data-badge-caption="<%= art["NAME"].ToString() %>"></span>
          <p><%= art["DISPLAYDESC"].ToString() %> <a href="<%: art["URL"].ToString() %>" target="_blank" id="download-button" class="btn waves-effect waves-light orange"> Read More </a></p>
        </div>
        
      </div></div> <%-- if (cntart % 3 == 2)
                            { --%> <!--</div>--> <%-- } cntart++; --%>
                
                <% } %>
           

         </div>
        </div>
        <% } %>
    </section>
       
     <div class="section" id="industry-info" >
	  <div class="container"> 
    

      <div class="row">
	  <h1><i class="material-icons" style="font-size:40px;float:left;padding-right:5px;">business</i>Industry Information</h1>
            <%= IndustryInformation %>
        </div></div></div>
    <% } %>
   </div>

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
 

