<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="DirectorySearch.aspx.cs" inherits="IQSDirectory.DirectorySearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%:RootPath %>Content/form_styles.css" rel="stylesheet" />
    <link href="<%:RootPath %>Content/publish_styles.css" rel="stylesheet" />
    <link href='<%:RootPath %>Content/stylerprint.css' rel='stylesheet' type='text/css' media='print' />
    <link href='<%:RootPath %>Content/jquery.fancybox-1.3.4.css' rel='Stylesheet' type='text/css' media='screen' />

    <script src='<%:RootPath %>Scripts/jquery.rating.pack.js' type='text/javascript'></script>
    <script src='<%:RootPath %>Scripts/jquery.fancybox-1.3.4.js' type='text/javascript'></script>
    <script src='<%:RootPath %>Scripts/fb.js' type='text/javascript'></script>
    <script src='<%:RootPath %>Scripts/jquery.cookie.js' type='text/javascript'></script>
    <script src='<%:RootPath %>Scripts/move_top.js' type='text/javascript'></script>
    <div class='searchdivtop'>
        <div id="divSearchMain">
            <h1><%: PageTitle %></h1>
            <div class="searchdivmain">

                <div id="divResults" runat="server">
                    <% if (ProductList.Count() > 0)
                        { %>
                    <div class='searchsubtitle'>PRODUCT/SERVICE CATEGORIES</div>
                    <% foreach (var pl in ProductList)
                        { %>
                    <div class='searchdivinner'>
                        <div class='searchdivtitle'><a href='<%: pl["URL"].ToString() %>'><%: pl["TITLE"].ToString() %></a></div>
                        <div class='searchdivdesc'><%: pl["MDESC"].ToString()  %></div>
                        <div><a class='searchdivlink' href='<%: pl["URL"].ToString() %>'><%: pl["URL"].ToString() %></a></div>
                    </div>
                    <% } %>
                    <% } %>
                    <% if (CompanyList.Count() > 0)
                        { %>
                    <div class='searchsubtitlecomp'>COMPANY ADVERTISERS</div>
                    <% foreach (var cl in CompanyList)
                        { %>
                    <div class='searchdivinnercomp'>
                        <div class='listResults clearfix'>
                            <div class='clearfix'>
                                <div class='viewdivsearch clearfix'>
                                    <div class='divLeft listingPage1LeftInsearch lnkurl'>
                                        <a href='<%: cl["WEBSITE"].ToString() %>' target='_blank' title='<%: cl["TITLE"].ToString() %>' alt='<%: cl["TITLE"].ToString() %>'><%: cl["TITLE"].ToString() %></a><br>
                                        <div><%: cl["CCITY"].ToString() %>, <%: cl["CSTATE"].ToString() %></div>
                                        <div><%: cl["PHONE"].ToString() %></div>
                                    </div>
                                    <div class='listingPage1RightIn'><%: cl["MDESC"].ToString() %></div>
                                </div>
                                <div class='viewdivsearch clearfix'>
                                    <div class='divLeft listingPage1LeftInsearch rfqlink linkBold'>
                                        <% if (cl["NORDER"].ToString() == "2")
                                            { %>
                                        <a class='lnkviewrfq iframe lnkrfq' href='directoryrfq.aspx?ClientSK=<%: cl["CID"].ToString() %>' style='display: block;'>Request For Quote</a>
                                        <% } %>
                                    </div>
                                    <div class='listingPage1RightIn viewWebsite linkBold'>
                                        <span class='spanviewcompanyprofile'>
                                            <h6><a href='<%: cl["URL"].ToString() %>' title='<%: cl["FORMATED_TITLE"].ToString() %> Profile' alt='<%: cl["TITLE"].ToString() %> Profile' class='lnkviewcopro'>View Company Profile</a></h6>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <% } %>
                    <% } %>
                    <% if (OtherList.Count() > 0)
                        { %>
                    <div class='searchsubtitlecomp'>OTHER COMPANIES</div>
                    <% foreach (var ol in OtherList)
                        { %>
                    <div class='searchdivinner'>
                        <div class='searchdivtitle'><a href='<%: ol["URL"].ToString() %>'><%: ol["TITLE"].ToString() %></a></div>
                        <div class='searchdivdesc'><%: ol["MDESC"].ToString()  %></div>
                        <div><a class='searchdivlink' href='<%: ol["URL"].ToString() %>'><%: ol["URL"].ToString() %></a></div>
                    </div>
                    <% } %>
                    <% } %>
                </div>
                <div class="searchdivpaging" id="divPaging" >
                    <% if (PageCount > 1)
                        { %>
                        <% if (StartPage != 1)
                            { %>
                            <span><a href='<%: PgPreURl %>'><<</a></span>
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
                                <span><a href='<%: cUrl %>'> <%= (i + 1).ToString() %></a></span>
                            <%} %>
                            
                        <% } %>
                        <% if (StartPage != PageCount)
                            { %>
                            <span><a href='<%: PgNxtURl %>'>>></a></span>
                        <%} %>
                    <% } %>
                </div>
                <script type='text/javascript'>
                    $(document).ready(function () {
                        $.get($('#hdnSrhRootPath').val() + 'StateSearch.html', function (data) {
                            $('#secsbox').html(data);
                        });

                    });

    </script>
            </div>
        </div>
    </div>
</asp:Content>

