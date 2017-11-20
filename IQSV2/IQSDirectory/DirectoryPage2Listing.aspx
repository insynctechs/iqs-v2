<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DirectoryPage2Listing.aspx.cs" Inherits="IQSDirectory.DirectoryPage2Listing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link href="./Content/publish_styles.css" rel="stylesheet" />
<link rel="stylesheet" href="./Content/jquery-ui.css" />   
<link href="./Content/jquery.fancybox-1.3.4.css" rel='Stylesheet' type='text/css' media='screen' />
<script src="//ajax.aspnetcdn.com/ajax/jquery.validate/1.12.0/jquery.validate.min.js"  type="text/javascript"></script>   
<script type="text/javascript" src="Scripts/DirectoryPage2Listing.js"></script>
<script src='./Scripts/jquery.fancybox-1.3.4.js' type='text/javascript'></script>
<script src='https://www.google.com/recaptcha/api.js'></script>
    <div id="outerWrapper">
    <div id="contentWrapper">
        
   <div id="contentList"><p class="pagetwo-heading">Page 2 Listing</p>
      <div class="left-block">
         <p>A Category listing includes the main category and its related sub-categories and usually consists of 12 additional listings easily identified on the main category under the heading of &quot;sub-categories.&quot; <b>A Top Listing</b> puts your company listing in the <b>top 3 positions;</b> also included are a company description and a direct link to your company website. <b>A Standard listing</b> positions your listing on a first-come, first serve basis; also included are a company description and a direct link to your company website.</p>
         <p><b>We provide full keyword coverage and the next best level of comparative search after Google for one low price!!!</b></p>
<p><b>Submit the form with your company information for a custom Page 2 Listing Quote.</b></p>
      <!--  
        <p>You have two options for listing on IQSDirectory's <b>Page 2 Listings</b> - a Top Listing and a Standard Listing:</p>
        <ul>
          <li><b>A Top Listing</b> puts your company listing in the <b>top 3 positions;</b> also included are a company description and a direct link to your company website.</li>
          <li><b>A Standard listing</b> positions your listing on a first-come, first serve basis; also included are a company description and a direct link to your company website.</li>
          <li><b>Your company</b> will be listed on page 2 of one category selected below with a Company Profile page including Google Map.</li>
        </ul>
        <!--
                <p>A Category listing includes the main category picked from below its related sub-categories and usually consists of 12 additional listings easily identified on the main category under the heading of &quot;sub-categories.&quot;</p>
        
        <p>You have two options for listing on IQSDirectory's <b>Page 2 Listings</b> - a Top Listing and a Standard Listing:</p>
        <ul>
          <li><b>A Top Listing</b> puts your company listing in the <b>top 3 positions;</b> also included are a company description and a direct link to your company website.</li>
          <li><b>A Standard listing</b> positions your listing on a first-come, first serve basis; also included are a company description and a direct link to your company website.</li>
          <li><b>Your company</b> will be listed on page 2 of one category selected below with a Company Profile page including Google Map.</li>
        </ul>
        -->
    </div>  
            

    <input type="hidden" id="hdnCategoryName" name="hdnCategoryName" runat="server" />
    <input type="hidden" id="hdnCategoryAlphabet" name="hdnCategoryAlphabet" runat="server" />
    <input type="hidden" id="hdnAlphabetColor" name="hdnAlphabetColor" runat="server" />
    <input type="hidden" id="hdnClientType" name="hdnClientType" runat="server" />
  
       <div class="dividerH "> </div>
        <div class="step1">
          <h1>1) Select type of Page 2 listing:</h1>
          <p>Please select the listing option below that you would like. By selecting an amount, you are agreeing to pay the seleted price (on next page) which will cover a <b>12 months listing period.</b> Once we receive payment, your company will be live on the site within 3 business days.</p>
          <label for="radioAmount" ><input type="radio"  name="radioAmount"  Value="$199">$199 Standard listing - 1st available<span style="color:red;">&nbsp;*</span> <span style="color:red;"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Please note, pricing is per category selection.</b></span><br /> </label>
          <label for="radioAmount" ><input type="radio"  name="radioAmount"   Value="$499" >$499 Top listing - 1st available<span style="color:red;">&nbsp;*</span> <span style="color:red;"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Please note, pricing is per category selection.</b></span></label>
          <br />
            <label id="radioAmount-error" class="error" for="radioAmount"></label>
          <br />
        <div class="clearfix" ></div>
        <div class="dividerH "> </div>
        <div class=" divLeft">
          <h1>2) Enter your contact information:</h1>
          <div id="profileList ">
            <div class="listContInfo ">
              <p class="labelFrmlist">Company Name:<span class="requireD">*</span>
                <input type="text" ID="txtCompanyName" name="txtCompanyName" class="lisTextbox" />
              </p>
              <p class="labelFrmlist">Company Phone:<span class="requireD">*</span>
                <input type="text"  ID="txtCompanyPhone" name="txtCompanyPhone" class="lisTextbox" />
              </p>
              <p class="labelFrmlist">Company Web Site URL:<span class="requireD">*</span>
                <input type="text" ID="txtCompanyWebsite" name="txtCompanyWebsite"  value="http://" class="lisTextbox" />
              </p>
              <p class="labelFrmlist">Contact Name:<span class="requireD">*</span>
                 <input type="text"  ID="txtContactName"  name="txtContactName" class="lisTextbox"  />
              </p>
              <p class="labelFrmlist">Contact Title:
                <input type="text"  ID="txtContactTitle" name="txtContactTitle" class="lisTextbox" />
              </p>
              <p class="labelFrmlist">Contact E-mail Address:<span class="requireD">*</span>
                <input type="text" ID="txtContactEmailAddress" name="txtContactEmailAddress" class="lisTextbox" />
              </p>
            </div>
            <div class="dividerH "> </div>
            <div class="companyDesc ">
              <h1>4) Enter a company description:</h1>
              <p>Please type in a 40 word description that best describes your company's services and capabalities. This text will be used as your listing's descriptive text. (This description may be edited by the IQSDirectory editorial staff to better fit the format of the relevent product directory).</p>
              <textarea id="txtDescription" name="txtDescription"  cols="100" rows="10" ></textarea>
            </div>
            <div class="clearfix" ></div>
            <div class="dividerH "> </div>
            <div class="g-recaptcha" data-sitekey="6Lc72zMUAAAAABk1ajqMH-ThUswu6BIps5JS10s_"  ></div>
            <input type="hidden" class="hiddenRecaptcha required" name="hiddenRecaptcha" id="hiddenRecaptcha"  data-callback="recaptchaCallback"  /> 
              <br />
    
              <input type="button" ID="btnSubmit" name="btnSubmit" Class="buttonBg" value="Submit" />
              &nbsp;
              <input type="button" class="buttonBg"  value="Reset" id="btnreset" onclick="return ResetStandardListing();" />
              <p>By submitting this form you agree to be included in the Industrial Quick Search&reg; mailing list, but may opt out this list at any time</p>
                      
            <div class="reqText"> <span class="requireD"> * Indicates Required Fields</span> </div>
         
        </div>
      </div>
      <div class="clearfix" ></div>
    </div>

   
    </div>
        
    </div>
    </div>
    <div id="successBlock" style="display:none">
       <p class="pagetwo-heading">Page 2 Listing</p>
        <div class="page2listingHeadTxt">You will be contacted soon to verify payment details.</div>                   
        </div>
   <div id="returnBlock" style="display:none"></div>
     <script type='text/javascript'>
         $(document).ready(function () {
             $.get($('#hdnWebURL').val() + 'StateSearch.html', function (data) {
                 $('#secsbox').html(data);
             });


             $('.lnkmail').fancybox({ 'height': 420, 'width': 400, 'onStart': function () { $('body').css('overflow', 'hidden'); }, 'onClosed': function () { $('body').css('overflow', 'auto'); }, 'hideOnOverlayClick': false });
             $('.lnkmail').bind('contextmenu', function (e) { return false; });

         });
    </script>
    
    <input type='hidden' id='hdnApiPath' value='<%: ApiPath %>' />
    <input type='hidden' id='hdnCategorySK' value='0' />
    <input type='hidden' id='hdnSrhRootPath' value="<%: RootPath %>" />
   
    
    
   

</asp:Content>
