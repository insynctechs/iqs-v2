<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DirectoryPremiumListing.aspx.cs" Inherits="IQSDirectory.DirectoryPremiumListing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link href="Content/publish_styles.css" rel="stylesheet" />
<div  id="outerWrapper">
<div id="contentWrapper">
<div id="contentHome">
<h1 style="
    font-size: 2.7em;
    font-style: italic;
    text-transform: uppercase;
    color: #2a434a;
    text-shadow: 0px 1px 1px #fb9622; 
    font-weight: bold;
">Premium Advertising Features &amp; Benefits on IQS Directory</h1>
<p>Thank you for your interest in finding out more about IQS&reg; Directory and its vertical product and services websites.</p>
<p>Our sites are optimized along with paid placement on many key words for your industry and we target those keywords most searched by your industry by detailed analysis by our Search Engine Optimizers. We also buy many key words on search engines so to make sure your company gets maximum exposure.</p>
<p><b>We provide full keyword coverage and the next best level of comparative search after Google for one low price!!!</b></p>
<p><b>Submit the form with your company information for a custom Premium Advertising Quote.</b></p>
<!--
<img src="commonImages/premium.gif"  alt="IQSDirectory" />
<br /><br />

<h2>Please review the list of categories below and select any categories you'd like to be listed on: </h2>
<p>
	Please review the list of categories below and select any categories you'd like to get information regarding 
    advertising. Please note that you will also be included on all sub-categories if you so desire at no 
    additional cost.  Premium listings include an ad and RFQ feature.
</p>
<br />
 
 <div class="contentListing">  
  <div class="ListingContIn">
    <asp:Panel ID="pnlCategories" runat="server" Width="865px"></asp:Panel>
</div>
</div>
-->
<div class="clearfix" ></div> 
<div class="dividerH "> </div>
<h1>Enter your contact information:</h1>
<div id="profileList">
<div class="listContInfo ">
<p class="labelFrmlist1">Company Name:<span class="requireD">*</span><asp:TextBox ID="txtCompanyName" MaxLength="200" runat="server" CssClass="lisTextbox"></asp:TextBox></p>
<p class="labelFrmlist">Company Phone:<span class="requireD">*</span><asp:TextBox MaxLength="50" ID="txtCompanyPhone" runat="server" CssClass="lisTextbox"></asp:TextBox></p>     
<p class="labelFrmlist">Company Web Site URL:<span class="requireD">*</span> <asp:TextBox ID="txtCompanyWebsite" MaxLength="100" runat="server" CssClass="lisTextbox" Text="http://"></asp:TextBox></p>   
<p class="labelFrmlist">Product/Service Area: <asp:TextBox ID="txtProductArea" MaxLength="100" runat="server" CssClass="lisTextbox"></asp:TextBox></p>
<p class="labelFrmlist">Contact Name:<span class="requireD">*</span> <asp:TextBox ID="txtContactName" MaxLength="200" runat="server" CssClass="lisTextbox" ></asp:TextBox></p>       
<p class="labelFrmlist">Contact Title:<asp:TextBox ID="txtContactTitle" MaxLength="20" runat="server" CssClass="lisTextbox"></asp:TextBox></p>  
<p class="labelFrmlist">Contact E-mail Address:<span class="requireD">*</span> <asp:TextBox ID="txtContactEmailAddress" MaxLength="50" runat="server" CssClass="lisTextbox"></asp:TextBox></p>   
</div>
<div class="dividerH  "> </div>
<div class="clearfix" ></div> 
<div class="listSubmit">
<asp:Button ID="btnSubmit1" runat="server" CssClass="buttonBg" Text="Submit" OnClientClick="return Validate();" OnClick="btnSubmit_Click"/>
<input type="button" class="buttonBg"  value="Reset" id="btnReset" onclick="return Reset();" />
</div>
<div class="reqText">
<span class="requireD"> * Indicates Required Fields </span>
</div>

 </div>
</div>
<input type="hidden" id="hdnCategoryName" name="hdnCategoryName" runat="server" />
<!--<input type="hidden" id="hdnCategoryAlphabet" name="hdnCategoryAlphabet" runat="server" />
<input type="hidden" id="hdnAlphabetColor" name="hdnAlphabetColor" runat="server" />-->
</div>
</div>
</asp:Content>
