<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="dimensions.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Input.Finder.Dimensions" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
<div id="page">
        <div id="guidance">           

            <div class="midline">
				<div class="illustration">
					<img src="../../images/adr0034.png" alt="A postcode label, an input box that is 8 characters wide and a &#39;Find Address&#39; button" />
				</div>
				<div class="guidetext">
					<p class="number">ADR-0034</p>
					<p>
                    Set the length of the postcode input box to 8 characters
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>

			</div>
            <div class="line">
				<div class="illustration">
					<img src="../../images/adr0035.png" alt="A label and input box for &#39;House &#047; Building Number&#39; supplemented with a vertical arrow indicating the height of the text input box" />
				</div>
				<div class="guidetext">
					<p class="number">ADR-0035</p>
					<p>
                    Set the height of each text input box to the largest 
                    character height in the currently active display font, 
                    taking the user's settings into account
                    </p>
					<p class="recommended">Recommended</p>
				</div>

			</div>
        </div>
    </div>
</asp:Content>
