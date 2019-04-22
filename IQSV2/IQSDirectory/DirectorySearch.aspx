<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="DirectorySearch.aspx.cs" inherits="IQSDirectory.DirectorySearch" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--<link href="<%:RootPath %>content/form_styles.css" rel="stylesheet" />
    <link href="<%:RootPath %>content/search_styles.css" rel="stylesheet" />
    <link href='<%:RootPath %>content/stylerprint.css' rel='stylesheet' type='text/css' media='print' />  -->
    <!--<script src='<%:RootPath %>scripts/jquery.fancybox-1.3.4.js' type='text/javascript'></script>
    <script src='<%:RootPath %>scripts/move_top.js' type='text/javascript'></script>-->
    <div id="search-results" class="section" > 		  
  
	   <div class="container"> 
	   
            <div class="row"><h1><%= PageTitle %></h1></div>
            
                <section id="Results" class="row">
                    <% if (ProductList.Count() > 0)
                        { %>
                    <h5>PRODUCT/SERVICE CATEGORIES</h5>
                    <ul class="prodlist">
                    <% foreach (var pl in ProductList)
                        { %>
                    <li>
                        <h3><a href='<%: pl["URL"].ToString() %>' title="<%: pl["TITLE"].ToString() %>"><%: pl["TITLE"].ToString() %></a></h3>
                        <p><%: pl["MDESC"].ToString()  %></p>
                        <a href='<%: pl["URL"].ToString() %>'><%: pl["URL"].ToString() %></a>
                    </li>
                    <% } %></ul><br /><br />
                    <% } %>
                    <% if (CompanyList.Count() > 0)
                        { %>
                    <h5>COMPANY ADVERTISERS</h5>
                    <ul class="prodlist">
                    <% foreach (var cl in CompanyList)
                        { %>
                    <li>
                        
                            
                                <div class="leftbox">
                                        <h3>
                                        <a href='<%: cl["WEBSITE"].ToString() %>' rel="nofollow" target='_blank' title='<%: cl["TITLE"].ToString() %>' ><%: cl["TITLE"].ToString() %></a><br>
                                        </h3>
                                        <div class="contact"><span><%: cl["CCITY"].ToString() %>, <%: cl["CSTATE"].ToString() %></span>

                                        <span><%: cl["PHONE"].ToString() %></span></div>
                                    </div>
                        <div class='buttons rightbox'>
                                    
                                        <% if (cl["NORDER"].ToString() == "2")
                                            { %>
                                        <a class='hoverable btn waves-effect waves-light orange btnrfq iframe' href='<%=RootPath %>directoryrfq.aspx?ClientSK=<%: cl["CID"].ToString() %>' >Request For Quote</a>
                                        <% } %>
                                    
                                    <a href='<%: cl["URL"].ToString() %>' title='<%: cl["FORMATED_TITLE"].ToString() %> Profile' class='hoverable btn waves-effect waves-light orange lnkviewcopro'>View Company Profile</a>
                                        
                                    </div>
                                    <p ><%: cl["MDESC"].ToString() %></p>
                               
                                
                                
                    </li>
                    <% } %></ul>
                    <% } %>
                    <% if (OtherList.Count() > 0)
                        { %>
                    <h2>OTHER COMPANIES</h2><ul class="prodlist">
                    <% foreach (var ol in OtherList)
                        { %>
                    <li>
                        <h3><a target="_blank" rel="nofollow" href='<%: ol["URL"].ToString() %>'><%: ol["TITLE"].ToString() %></a></h3>
                        <p><%: ol["MDESC"].ToString()  %></p>
                        <a class='searchdivlink' href='<%: ol["URL"].ToString() %>'><%: ol["URL"].ToString() %></a>
                    </li>
                    <% } %></ul>
                    <% } %>
                </section>
                <ul class="pagination" >
                    <% if (PageCount > 0)
                        { %>
                            <% if (StartPage != 1) {  var cUrl = PgSrhUrl + (1).ToString();%>
                            <li class="waves-effect" ><a href="<%: cUrl %>"><i class="material-icons">chevron_left</i></a></li>
                        <% } else {  %> <li class="disabled"  ><a href="#!"><i class="material-icons">chevron_left</i></a></li>
                        <% } %>
                        <% for (int i = 0; i < PageCount; i++)
                            { %>
                            <% if (StartPage.ToString() == (i + 1).ToString())
                                { %>
                                <li class="active"><a href="#!"><%= (i + 1).ToString() %></a></li>
                            <% }
                                else
                                {
                                    var cUrl = PgSrhUrl + (i + 1).ToString();
                                    %>
                                <li class="waves-effect"><a href='<%: cUrl %>'> <%= (i + 1).ToString() %></a></li>
                            <%} %>
                            
                        <% } %>
                    <% if (StartPage != PageCount) {  var cUrl = PgSrhUrl + (PageCount).ToString();%>
                            <li class="waves-effect" ><a href="<%: cUrl %>"><i class="material-icons">chevron_right</i></a></li>
                        <% } else {  %> <li class="disabled"  ><a href="#!"><i class="material-icons">chevron_right</i></a></li>
                        <% } %>
                         
                        
                    <% } %>
                </ul>

                <script type='text/javascript'>
                    $(document).ready(function () {
                        $.get($('#hdnSrhRootPath').val() + 'statesearch.html', function (data) {
                            $('#searchBarDir').html(data);
                        });
                        $('.btnrfq').fancybox({ 'height': 600, 'width': 800, 'onStart': function () { $('body').css('overflow', 'hidden'); }, 'onClosed': function () { $('body').css('overflow', 'auto'); }, 'hideOnOverlayClick': false });
                        //$('.btnrfq').bind('contextmenu', function (e) { return false; });
                        
                        
                    });

    </script>
            </div>
        </div>
    
    
     <input type='hidden' id='hdnApiPath' value='<%: ApiPath %>' />
    <input type='hidden' id='hdnCategorySK' value='<%: CategorySK %>' />
      <input type='hidden' id='hdnSrhRootPath' value="<%: RootPath %>" />
</asp:Content>

