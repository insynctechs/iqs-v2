<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="ListCompanies.aspx.cs" inherits="IQSDirectory.ListCompanies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Content/publish_styles.css" rel="stylesheet" media='screen'/>
    <link href='Content/stylerprint.css' rel='stylesheet' type='text/css' media='print' />
    <link href='Content/jquery.fancybox-1.3.4.css' rel='Stylesheet' type='text/css' media='screen' />

    <script src='Scripts/jquery.rating.pack.js' type='text/javascript'></script>
    <script src='Scripts/jquery.fancybox-1.3.4.js' type='text/javascript'></script>
    <script src='Scripts/fb.js' type='text/javascript'></script>
    <script src='Scripts/category_page1.js' type='text/javascript'></script>
    <script src='Scripts/move_top.js' type='text/javascript'></script>

    <div id="outerWrapper">
        <div class="clearfix"></div>
        <div id="panelHome">
            <div class="headSiteMap">
                <h1>IQS Manufacturers Directory of Companies </h1>
                <h2>Links To Leading Industrial Manufacturing Companies, Suppliers and Distributors </h2>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
    <script type='text/javascript'>
        $(document).ready(function () {
            $.get('StateSearch.html', function (data) {
                $('#secsbox').html(data);
            });
        });
    </script>
</asp:Content>
