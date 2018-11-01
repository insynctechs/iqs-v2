<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="ListCompanies.aspx.cs" inherits="IQSDirectory.ListCompanies" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <div class="section" style="padding-top:50px;" >
  		  
  
	   <div class="container"> 
	   
            <div class="row"><h1>IQS Manufacturers Directory of Companies</h1>
			<h6>Links To Leading Industrial Manufacturing Companies, Suppliers and Distributors</h6><br></div>
            </div></div>
    <div class="section" style="background-color:#CCCCCC;">
        <div class="row center">
                    <div class="siteMapLinks">
                        <% for (char c = 'A'; c <= 'Z'; c++)
                    { %>
                    <% if (c.ToString() == SrhLetter)
                        { %>
                        <a href='<%: RootPath %>ListCompanies/<%: c.ToString() %>/1' class='active hoverable btn waves-effect waves-light orange'><%: c.ToString() %></a>
                    <%} else { %>    
                        <a href='<%: RootPath %>ListCompanies/<%: c.ToString() %>/1' class="hoverable btn waves-effect waves-light orange"><%: c.ToString() %></a>
                    <%} %>
                <%} %>
                        </div>
            </div>
        <div class="row center" style="margin-bottom:0px;">
            <ul class="pagination">
                 <% if (SrhPage != 1) {  %>
                            <li class="waves-effect" ><a href="<%: RootPath %>ListCompanies/<%: SrhLetter %>/<%: (SrhPage-1).ToString() %>/"><i class="material-icons">chevron_left</i></a></li>
                        <% } else {  %> <li class="disabled"  ><a href="#!"><i class="material-icons">chevron_left</i></a></li>
                        <% } %>
                <% for (int i = 1; i <= TotalPages; i++)
                    { %>
                    <% if (i == SrhPage)
                        { %>
                        <li class="active"><a href='<%: RootPath %>ListCompanies/<%: SrhLetter %>/<%: i.ToString() %>' class='active'><%: i.ToString() %></a></li>
                    <% }
                    else
                    { %>
                         <li class="waves-effect"><a href='<%: RootPath %>ListCompanies/<%: SrhLetter %>/<%: i.ToString() %>'><%: i.ToString() %></a></li>
                    <% } %>
                <%} %>
                <% if (SrhPage != TotalPages) {  %>
                            <li class="waves-effect" ><a href="<%: RootPath %>ListCompanies/<%: SrhLetter %>/<%: (SrhPage+1).ToString() %>/"><i class="material-icons">chevron_right</i></a></li>
                        <% } else {  %> <li class="disabled"  ><a href="#!"><i class="material-icons">chevron_right</i></a></li>
                        <% } %>
            </ul>
            </div></div>
    <div class="section">
<div class="container">
<div class="row center">
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
            $.get($('#hdnSrhRootPath').val() + 'statesearch.html', function (data) {
                $('#searchBarDir').html(data);
            });
        });
    </script>
    <input type='hidden' id='hdnApiPath' value='<%: ApiPath %>' />
    <input type='hidden' id='hdnCategorySK' value='0' />
    <input type='hidden' id='hdnSrhRootPath' value="<%: RootPath %>" />
</asp:Content>
