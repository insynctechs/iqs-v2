<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DirectoryPremiumListing.aspx.cs" Inherits="IQSDirectory.DirectoryPremiumListing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link href="Content/publish_styles.css" rel="stylesheet" />
<link rel="stylesheet" href="//code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />    
<script src="//ajax.aspnetcdn.com/ajax/jquery.validate/1.12.0/jquery.validate.min.js"  type="text/javascript"></script>   
<script type="text/javascript" src="Scripts/DirectoryPremiumListing.js"></script>
<script src='https://www.google.com/recaptcha/api.js'></script>
<div id="contentHomePremium">
<h1>Premium Advertising Features &amp; Benefits on IQS Directory</h1>
<p>Thank you for your interest in finding out more about IQS&reg; Directory and its vertical product and services websites.</p>
<p>Our sites are optimized along with paid placement on many key words for your industry and we target those keywords most searched by your industry by detailed analysis by our Search Engine Optimizers. We also buy many key words on search engines so to make sure your company gets maximum exposure.</p>
<p><b>We provide full keyword coverage and the next best level of comparative search after Google for one low price!!!</b></p>
<p><b>Submit the form with your company information for a custom Premium Advertising Quote.</b></p>
<div class="clearfix" ></div> 
<div class="dividerH "> </div>
<h1>Enter your contact information:</h1>
<div id="profileList">

<p class="labelFrmlist1">Company Name:<span class="requireD">*</span><input type="text" ID="txtCompanyName" name="txtCompanyName" class="lisTextbox"  /></p>
<p class="labelFrmlist1">Company Phone:<span class="requireD">*</span><input type="text" ID="txtCompanyPhone" name="txtCompanyPhone" class="lisTextbox"  /></p>     
<p class="labelFrmlist1">Company Web Site URL:<span class="requireD">*</span><input type="text"  ID="txtCompanyWebsite" name="txtCompanyWebsite" class="lisTextbox" /></p>   
<p class="labelFrmlist1">Product/Service Area: <input type="text"  ID="txtProductArea" name="txtProductArea" class="lisTextbox"  /></p>
<p class="labelFrmlist1">Contact Name:<span class="requireD">*</span> <input type="text"   ID="txtContactName" name="txtContactName"  class="lisTextbox" /></p>       
<p class="labelFrmlist1">Contact Title:<input type="text"  ID="txtContactTitle" name="txtContactTitle"  class="lisTextbox" /></p>  
<p class="labelFrmlist1">Contact E-mail Address:<span class="requireD">*</span> <input type="text"  name="txtContactEmailAddress" id="txtContactEmailAddress"  class="lisTextbox" /></p>   
<div class="dividerH  "> </div>
<div class="g-recaptcha" data-sitekey="6Lc72zMUAAAAABk1ajqMH-ThUswu6BIps5JS10s_"  ></div>
<input type="hidden" class="hiddenRecaptcha required" name="hiddenRecaptcha" id="hiddenRecaptcha"  data-callback="recaptchaCallback"  /> 
<div class="clearfix" ></div> 
<div class="listSubmit">
<!-- <asp:Button ID="btnSubmit1" runat="server" CssClass="buttonBg" Text="Submit" OnClientClick="return Validate();" OnClick="btnSubmit_Click"/> -->
<input type="button" id="btnSubmit" EnableViewState="false" class="buttonBg" value="Submit"  />
<input type="button" class="buttonBg"  value="Reset" id="btnReset" onclick="return Reset();" />
</div>
<div class="reqText">
<span class="requireD"> * Indicates Required Fields </span>
</div>
</div>
<input type="hidden" id="hdnCategoryName" name="hdnCategoryName" runat="server" />
<!--<input type="hidden" id="hdnCategoryAlphabet" name="hdnCategoryAlphabet" runat="server" />
<input type="hidden" id="hdnAlphabetColor" name="hdnAlphabetColor" runat="server" />-->
</div>

<div id="successBlock" style="display:none">
    <table>
                    
                    <tr style="height:80px;">
                        <td class="page2listing" colspan="2">
                            <img src="images/premium.gif" alt='IQSDirectory'/>
                        </td>
                    </tr>
                    <tr style="height:40px;">
                        <td class="page2listingHeadTxt" colspan="2">Thank you! Information will be sent to you shortly. </td>
                    </tr>  
                     </table>                  
               
     
</div>
<div id="returnBlock"></div>
</asp:Content>
