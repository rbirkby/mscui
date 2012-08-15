<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="general.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Display.General" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
 <div id="page">
        <div id="guidance">           
            
			<div class="midline">
				<div class="illustration">
					<img src="../images/adr0002.png" alt="An address (18 Orchard Cottage, Ipswich, Northshire, NS33 8KR), with each element on a new line" />
				</div>
				<div class="guidetext">
					<p class="number">ADR-0006</p>
					<p>
                    Where part of an address is not available, do 
                    not display an empty string in its place
                    </p>
					<p class="recommended">Recommended</p>
                    <p class="note"><strong>Note </strong>This example has no value for Line 2 (Street Name)</p>
				</div>

                
			</div>
            
            <div class="midline">
				<div class="illustration">
					<img src="../images/adr0007.png" alt="A diagram of a postcode (NS33 8KR) in capitals, with an arrow pointing to the space between the first and second part" />
				</div>
				<div class="guidetext">
					<p class="number">ADR-0007</p>
					<p>
                    Display the postcode in all caps with a space between the 
                    first part (the outcode) and the second part (the incode)
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
			<div class="line">
				<div class="illustration">
					<img src="../images/adr0008.png" alt="A box labelled 'Address' and containing an address without any further labels" />
				</div>
				<div class="guidetext">
					<p class="number">ADR-0008</p>
					<p>
                    Do not display labels for individual address elements
                    </p>
					<p class="recommended">Recommended</p>
				</div>

			</div>
        </div>
    </div>
</asp:Content>