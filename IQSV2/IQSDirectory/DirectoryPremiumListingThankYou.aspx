<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DirectoryPremiumListingThankYou.aspx.cs" Inherits="IQSDirectory.DirectoryPremiumListingThankYou" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
          <table>
                    
                    <tr style="height:80px;">
                        <td class="page2listing" colspan="2">
                            <img src="commonimages/premium.gif" alt='IQSDirectory'/>
                        </td>
                    </tr>
                    <tr style="height:40px;">
                        <td class="page2listingHeadTxt" colspan="2">Thank you! Information will be sent to you shortly. </td>
                    </tr>  
                     </table>                  
                 <table style="border-top:1px solid #666666;border-bottom:1px solid #666666;" cellspacing="5px">
                    <tr style="height:20px;">
                        <td class="page2listingTxt" colspan="2"><asp:Label ID="lblHeading" runat="server"></asp:Label></td>
                    </tr>
                    <tr style="height:15px;">
                        <td class="page2listingTxt" style="width: 227px">Suggested IQSDirectory Site:</td>
                        <td width="610" class="page2listingSubTxt"><asp:Label ID="lblCategoryName" runat="server"></asp:Label></td>
                    </tr>
                    <tr  style="height:15px;">
                      <td class="page2listingTxt" style="width: 227px">Company Name:</td>
                      <td width="610" class="page2listingSubTxt"><asp:Label ID="lblCompanyName" runat="server"></asp:Label></td>
                    </tr>
                    <tr  style="height:15px;">
                      <td class="page2listingTxt" style="width: 227px">Company Phone#:</td>
                      <td width="610" class="page2listingSubTxt"><asp:Label ID="lblCompanyPhone" runat="server"></asp:Label></td>
                    </tr>
                    <tr  style="height:15px;">
                      <td class="page2listingTxt" style="width: 227px">Company Web Site:</td>
                      <td width="610" class="page2listingSubTxt"><asp:Label ID="lblCompanyWebsite" runat="server"></asp:Label></td>
                    </tr>
                    <tr  style="height:15px;">
                      <td class="page2listingTxt" style="width: 227px">Contact:</td>
                      <td width="610" class="page2listingSubTxt"><asp:Label ID="lblContactname" runat="server"></asp:Label></td>
                    </tr>
                    <tr  style="height:15px;">
                      <td class="page2listingTxt" style="width: 227px"> Contact Title: </td>
                      <td width="610" class="page2listingSubTxt"><asp:Label ID="lblContacttitle" runat="server"></asp:Label></td>
                    </tr>
                    <tr  style="height:15px;">
                      <td class="page2listingTxt" style="width: 227px; height: 5px;">Submit:</td>
                      <td width="610" class="page2listingSubTxt" style="height: 5px">Submit</td>
                    </tr>
                    <tr  style="height:15px;" runat="server" visible="false" id="trAmount">
                      <td class="page2listingTxt" style="width: 227px">Total Amount:</td>
                      <td class="page2listingSubTxt"><asp:Label ID="lblTotalAmount" runat="server"></asp:Label></td>
                    </tr>
                    </table>
    <input type="hidden" runat="server" id="hdnCategoryname" name="hdnCategoryname" />
<input type="hidden" runat="server" id="hdnCompanyName" name="hdnCompanyName" />
<input type="hidden" runat="server" id="hdnCompanyPhone" name="hdnCompanyPhone" />
<input type="hidden" runat="server" id="hdnCompanyWebsite" name="hdnCompanyWebsite" />
<input type="hidden" runat="server" id="hdnContactname" name="hdnContactname" />
<input type="hidden" runat="server" id="hdnContacttitle" name="hdnContacttitle" />
<input type="hidden" runat="server" id="hdnTotalAmount" name="hdnTotalAmount" />

</asp:Content>
