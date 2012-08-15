<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="inputboxes.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Input.UKInput.InputBoxes" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
     
    <div id="page">
        <div id="guidance">
        
            <div class="midline">
				<div class="landscape">
					<img src="../../images/adr0012.png" alt="A form with labels and input controls for Line 1, Line 2, Town &#047; City, County and Postcode. Postcode is followed by a &#39;Find Postcode&#39; button." />
				</div>
            </div>
               
            <div class="line">
				<div class="illustration">
					<table class="datavalues">
					    <tr>
					        <td class="address">
					        3 boxes for input of all details 
					        up to and including the street name
					        </td>
					    </tr>
					    <tr>
					        <td>
					        1 box for input of the town or city
					        </td>
					    </tr>
					    <tr>
					        <td>
					        1 box for the input of the county
					        </td>
					    </tr>
					    <tr>
					        <td>
					        1 box for the input of the postcode
					        </td>
					    </tr>
					</table>
				</div>   
				<div class="guidetext">
					<p class="number">ADR-0011</p>
					<p>
                    Provide these text input boxes, in the 
                    stated order, for UK address input
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
			
        </div>
    </div>
</asp:Content>