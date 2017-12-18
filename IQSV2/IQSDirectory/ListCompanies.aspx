<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="ListCompanies.aspx.cs" inherits="IQSDirectory.ListCompanies" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="<%: RootPath %>Content/common_styles.css" rel="stylesheet" media='screen'/>
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
                <ul class="complist">
                    <% foreach (var comp in CompaniesList)
                        { %>
                    
                            <li><a href='<%: RootPath %><%: comp["FORMATED_URL"] %>'><%= comp["NAME"] %> </a><br /><%= comp["CITYSTATE"] %></li>
                           
                   
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
