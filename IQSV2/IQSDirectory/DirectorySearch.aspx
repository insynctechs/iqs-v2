<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="DirectorySearch.aspx.cs" inherits="IQSDirectory.DirectorySearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%:RootPath %>Content/form_styles.css" rel="stylesheet" />
    <link href="<%:RootPath %>Content/search_styles.css" rel="stylesheet" />
    <link href='<%:RootPath %>Content/stylerprint.css' rel='stylesheet' type='text/css' media='print' />
    <link href='<%:RootPath %>Content/jquery.fancybox-1.3.4.css' rel='Stylesheet' type='text/css' media='screen' />    
    <script src='<%:RootPath %>Scripts/jquery.fancybox-1.3.4.js' type='text/javascript'></script>
    <script src='<%:RootPath %>Scripts/move_top.js' type='text/javascript'></script>
    <div id='content_wrapper'>
            <h1><%= PageTitle %></h1>
            
                <section id="Results" runat="server">
                    <% if (ProductList.Count() > 0)
                        { %>
                    <h2>PRODUCT/SERVICE CATEGORIES</h2>
                    <ul class="prodlist">
                    <% foreach (var pl in ProductList)
                        { %>
                    <li>
                        <h3><a href='<%: pl["URL"].ToString() %>'><%: pl["TITLE"].ToString() %></a></h3>
                        <p><%: pl["MDESC"].ToString()  %></p>
                        <a href='<%: pl["URL"].ToString() %>'><%: pl["URL"].ToString() %></a>
                    </li>
                    <% } %></ul>
                    <% } %>
                    <% if (CompanyList.Count() > 0)
                        { %>
                    <h2>COMPANY ADVERTISERS</h2>
                    <ul class="complist">
                    <% foreach (var cl in CompanyList)
                        { %>
                    <li>
                        
                            
                                
                                        <h3>
                                        <a href='<%: cl["WEBSITE"].ToString() %>' target='_blank' title='<%: cl["TITLE"].ToString() %>' alt='<%: cl["TITLE"].ToString() %>'><%: cl["TITLE"].ToString() %></a><br>
                                        </h3>
                                        <div class="contact"><span><%: cl["CCITY"].ToString() %>, <%: cl["CSTATE"].ToString() %></span>
                                        <span><%: cl["PHONE"].ToString() %></span></div>
                                    
                                    <p><%: cl["MDESC"].ToString() %></p>
                               
                                <div class='buttons'>
                                    
                                        <% if (cl["NORDER"].ToString() == "2")
                                            { %>
                                        <a class='btnrfq iframe' href='<%=RootPath %>directoryrfq.aspx?ClientSK=<%: cl["CID"].ToString() %>' >Request For Quote</a>
                                        <% } %>
                                    
                                    <a href='<%: cl["URL"].ToString() %>' title='<%: cl["FORMATED_TITLE"].ToString() %> Profile' alt='<%: cl["TITLE"].ToString() %> Profile' class='lnkviewcopro'>View Company Profile</a>
                                        
                                    </div>
                                
                    </li>
                    <% } %></ul>
                    <% } %>
                    <% if (OtherList.Count() > 0)
                        { %>
                    <h2>OTHER COMPANIES</h2><ul class="complist">
                    <% foreach (var ol in OtherList)
                        { %>
                    <li>
                        <h3><a href='<%: ol["URL"].ToString() %>'><%: ol["TITLE"].ToString() %></a></h3>
                        <p><%: ol["MDESC"].ToString()  %></p>
                        <a class='searchdivlink' href='<%: ol["URL"].ToString() %>'><%: ol["URL"].ToString() %></a>
                    </li>
                    <% } %></ul>
                    <% } %>
                </section>
                <section class="paging" >
                    <% if (PageCount > 1)
                        { %>
                        <% if (StartPage != 1)
                            { %>
                            <a href='<%: PgPreURl %>'><<</a>
                        <% } %>
                        <% for (int i = 0; i < PageCount; i++)
                            { %>
                            <% if (StartPage.ToString() == (i + 1).ToString())
                                { %>
                                <span><%= (i + 1).ToString() %></span>
                            <% }
                                else
                                {
                                    var cUrl = PgSrhUrl + (i + 1).ToString();
                                    %>
                                <a href='<%: cUrl %>'> <%= (i + 1).ToString() %></a>
                            <%} %>
                            
                        <% } %>
                        <% if (StartPage != PageCount)
                            { %>
                            <a href='<%: PgNxtURl %>'>>></a>
                        <%} %>
                    <% } %>
                </section>

                <script type='text/javascript'>
                    $(document).ready(function () {
                        $.get($('#hdnSrhRootPath').val() + 'StateSearch.html', function (data) {
                            $('#secsbox').html(data);
                        });
                        $('.btnrfq').fancybox({ 'height': 600, 'width': 800, 'onStart': function () { $('body').css('overflow', 'hidden'); }, 'onClosed': function () { $('body').css('overflow', 'auto'); }, 'hideOnOverlayClick': false });
                        $('.btnrfq').bind('contextmenu', function (e) { return false; });
                        

                    });

    </script>
            </div>
        
    
     <input type='hidden' id='hdnApiPath' value='<%: ApiPath %>' />
    <input type='hidden' id='hdnCategorySK' value='<%: CategorySK %>' />
      <input type='hidden' id='hdnSrhRootPath' value="<%: RootPath %>" />
</asp:Content>

