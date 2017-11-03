<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="ListCompanies.aspx.cs" inherits="IQSDirectory.ListCompanies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="<%: RootPath %>Content/publish_styles.css" rel="stylesheet" media='screen'/>
    <link href='<%: RootPath %>Content/stylerprint.css' rel='stylesheet' type='text/css' media='print' />
    <link href='<%: RootPath %>Content/jquery.fancybox-1.3.4.css' rel='Stylesheet' type='text/css' media='screen' />

    <script src='<%: RootPath %>Scripts/jquery.rating.pack.js' type='text/javascript'></script>
    <script src='<%: RootPath %>Scripts/jquery.fancybox-1.3.4.js' type='text/javascript'></script>
    <script src='<%: RootPath %>Scripts/fb.js' type='text/javascript'></script>
    <script src='<%: RootPath %>Scripts/category_page1.js' type='text/javascript'></script>
    <script src='<%: RootPath %>Scripts/move_top.js' type='text/javascript'></script>

    <div id="outerWrapper">
        <div class="clearfix"></div>
        <div id="panelHome">
            <div class="headSiteMap">
                <h1>IQS Manufacturers Directory of Companies </h1>
                <h2>Links To Leading Industrial Manufacturing Companies, Suppliers and Distributors </h2>
            </div>
            <div class="siteMapLinks">
                <% for (char c = 'A'; c <= 'Z'; c++)
                    { %>
                    <% if (c.ToString() == SrhLetter)
                        { %>
                        <a href='<%: RootPath %>ListCompanies/<%: c.ToString() %>/1' class='active'><%: c.ToString() %></a> | 
                    <%} else { %>    
                        <a href='<%: RootPath %>ListCompanies/<%: c.ToString() %>/1'><%: c.ToString() %></a> | 
                    <%} %>
                <%} %>
            </div>

            <div class="siteMapPageLinks">
                <% for (int i = 1; i <= TotalPages; i++)
                    { %>
                    <% if (i == SrhPage)
                        { %>
                        <a href='<%: RootPath %>ListCompanies/<%: SrhLetter %>/<%: i.ToString() %>' class='active'><%: i.ToString() %></a> | 
                    <% }
                    else
                    { %>
                        <a href='<%: RootPath %>ListCompanies/<%: SrhLetter %>/<%: i.ToString() %>'><%: i.ToString() %></a> | 
                    <% } %>
                <%} %>
            </div>
            <div class="clearfix"></div>
            <div class="headSiteMap">
                <ul>
                    <% foreach (var comp in CompaniesList)
                        { %>
                    <li class="odd clearfix">
                        <ul>
                            <li class="divLeft left"><a href='<%: RootPath %><%: comp["FORMATED_URL"] %>'><%: comp["NAME"] %> </a></li>
                            <li class="divLeft right"><%: comp["CITYSTATE"] %></li>
                        </ul>
                    </li>
                    <%} %>
                </ul>
            </div>
        </div>
    </div>
    <script type='text/javascript'>
        $(document).ready(function () {
            $.get($('#hdnSrhRootPath').val() + 'StateSearch.html', function (data) {
                $('#secsbox').html(data);
            });
        });
    </script>
    <input type='hidden' id='hdnApiPath' value='<%: ApiPath %>' />
    <input type='hidden' id='hdnCategorySK' value='0' />
    <input type='hidden' id='hdnSrhRootPath' value="<%: RootPath %>" />
</asp:Content>
