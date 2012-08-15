<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="inline.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Display.InLine" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           
            
			<div class="midline">
				<div class="landscape">
					<img src="../images/adr0001.png" alt="An address displayed on a single line with a comma and space between each address element" />
				</div>
				<div class="guidealone">
					<p class="number">ADR-0001</p>
					<p>
                    When displaying an address horizontally, only use a single 
                    comma and a single space, in that order, to delimit the 
                    different fields
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>

			<div class="line">
				<div class="illustration">
					<img src="../images/adr0004.png" alt="A truncated address on a single line: '18 Orchard Cottage, King's Road&#133;'" />
				</div>
				<div class="guidetext">
					<p class="number">ADR-0004</p>
					<p>
                    When truncating an address, add an ellipsis to indicate 
                    that the address is not displayed in full and, where 
                    appropriate, provide a means for the user to access the 
                    full address
                    </p>
					<p class="recommended">Recommended</p>
				</div>

			</div>

        </div>
    </div>
</asp:Content>